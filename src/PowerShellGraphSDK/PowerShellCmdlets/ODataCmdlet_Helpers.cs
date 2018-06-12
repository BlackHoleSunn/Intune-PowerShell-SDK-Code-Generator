﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
    using System.Reflection;
    using System.Text;

    public abstract partial class ODataCmdlet
    {
        internal string BuildUrl(string resourcePath)
        {
            // Check that we have a valid base address
            string baseAddress = CurrentEnvironmentParameters.GraphBaseAddress;
            if (!Uri.IsWellFormedUriString(baseAddress, UriKind.Absolute))
            {
                throw new PSGraphSDKException(
                    new ArgumentException($"Invalid base URL - use the '{UpdateEnvironmentParameters.CmdletVerb}-{UpdateEnvironmentParameters.CmdletNoun}' cmdlet to set it to a valid URL", "BaseUrl"),
                    "InvalidBaseUrl",
                    ErrorCategory.InvalidArgument,
                    baseAddress);
            }

            // Get the full base URL
            string baseUrlWithSchema = $"{baseAddress.TrimEnd('/')}/{CurrentEnvironmentParameters.SchemaVersion}";

            // TODO: Sanitize the URL
            //resourcePath = WebUtility.UrlEncode(resourcePath);

            // Append the relative URL to the base URL
            string result = $"{baseUrlWithSchema}/{resourcePath.Trim('/')}";

            return result;
        }

        /// <summary>
        /// Gets the properties that are bound (set by the user) in the current invocation of this cmdlet.
        /// </summary>
        /// <param name="includeInherited">Whether or not to include inherited properties</param>
        /// <param name="filter">The filter for the properties to include in the result (if it evaluates to true, the property is included)</param>
        /// <returns>The properties that are bound in the current invocation of this cmdlet.</returns>
        internal IEnumerable<PropertyInfo> GetBoundProperties(bool includeInherited = true, Func<PropertyInfo, bool> filter = null)
        {
            // Get the cmdlet's properties
            IEnumerable<PropertyInfo> cmdletProperties = this.GetProperties(includeInherited, filter);

            // Get only the properties that were set from PowerShell
            IEnumerable<string> boundParameterNames = this.MyInvocation.BoundParameters.Keys;
            IEnumerable<PropertyInfo> boundProperties = cmdletProperties.Where(prop => boundParameterNames.Contains(prop.Name));

            return boundProperties;
        }

        /// <summary>
        /// Gets all the properties declared on this class.
        /// </summary>
        /// <param name="includeInherited">Whether or not to include inherited properties (defaults to true)</param>
        /// <param name="filter">The filter for the properties to include in the result (if it evaluates to true, the property is included)</param>
        /// <returns>The properties that are defined on this cmdlet.</returns>
        internal IEnumerable<PropertyInfo> GetProperties(bool includeInherited, Func<PropertyInfo, bool> filter = null)
        {
            bool useLazyPath = includeInherited == false && filter == null;
            if (useLazyPath && this._properties != null)
            {
                // Shortcut path to avoid re-evaluating properties for the most set of parameters
                return this._properties;
            }
            else
            {
                // Create the binding flags
                BindingFlags bindingFlags =
                    BindingFlags.Instance | // ignore static/const properties
                    BindingFlags.Public; // only include public properties
                if (!includeInherited)
                {
                    bindingFlags |= BindingFlags.DeclaredOnly; // ignore inherited properties
                }

                // Get the properties on this cmdlet
                IEnumerable<PropertyInfo> result = this.GetType().GetProperties(bindingFlags);

                // Apply filter if necessary
                if (filter != null)
                {
                    result = result.Where(filter);
                }

                // Store evaluated properties in case this method gets called again with the same parameters
                if (useLazyPath)
                {
                    this._properties = result;
                }

                return result;
            }
        }
        private IEnumerable<PropertyInfo> _properties = null;

        /// <summary>
        /// Creates a JSON string from the given properties.
        /// </summary>
        /// <param name="properties">The properties on this cmdlet object which should be serialized into the JSON string</param>
        /// <param name="oDataType">The OData type (full name) to be included in the JSON string</param>
        /// <returns>The JSON string.</returns>
        internal string WriteJsonFromProperties(IEnumerable<PropertyInfo> properties, string oDataType = null)
        {
            // We need to build the JSON string manually in order to account for special handling of primitive Edm (OData) types.
            // See the spec for more details: http://docs.oasis-open.org/odata/odata/v4.0/errata03/os/complete/part3-csdl/odata-v4.0-errata03-os-part3-csdl-complete.html#_The_edm:Documentation_Element
            StringBuilder jsonString = new StringBuilder();
            jsonString.AppendLine("{");

            bool isFirst = true;

            // Add the OData type property into the request body
            if (oDataType != null)
            {
                jsonString.Append($"    \"{ODataConstants.RequestProperties.ODataType}\": \"{oDataType}\"");
                isFirst = false;
            }

            // Add the properties into the body
            foreach (PropertyInfo property in properties)
            {
                if (!isFirst)
                {
                    jsonString.AppendLine(",");
                }
                isFirst = false;

                string propertyName = property.Name;
                object propertyValue = property.GetValue(this); // get the value for the given property on this instance of the cmdlet

                // Unwrap PowerShell objects
                if (propertyValue is PSObject psObj)
                {
                    propertyValue = psObj.BaseObject;
                }

                // Get the type of this property
                string propertyODataType = property.GetODataTypeName();

                // Convert the value into a JSON string
                string propertyValueString = propertyValue.ToODataString(propertyODataType, isArray: property.PropertyType.IsArray);
                jsonString.Append($"    \"{propertyName}\": {propertyValueString}");
            }
            jsonString.AppendLine();
            jsonString.Append("}");

            // Return the JSON string
            return jsonString.ToString();
        }

        /// <summary>
        /// Creates the content object for a POST/PUT "$ref" call.
        /// </summary>
        /// <param name="referenceUrl">The URL which can be used to retrieve the object that is being referenced</param>
        /// <returns>The content object for a POST/PUT "$ref" call.</returns>
        internal IDictionary<string, string> GetReferenceRequestContent(string referenceUrl)
        {
            // Return the object which is to be serialized into the body of the request
            return new Dictionary<string, string>()
            {
                { ODataConstants.RequestProperties.ODataId, $"{referenceUrl}" }
            };
        }

        /// <summary>
        /// Processes a result object.
        /// </summary>
        /// <param name="obj">The result object to process</param>
        internal void ProcessResultObject(object obj)
        {
            if (obj is PSObject psObj)
            {
                // Track the properties to hide
                ICollection<string> propertiesToHide = new List<string>();

                // Add alias for the "id" property if it exists on the result object
                string objectId = psObj.Properties[ODataConstants.SearchResultProperties.Id]?.Value as string;
                if (objectId != null)
                {
                    // Get the name of the alias property
                    string idPropertyName = this.GetType().GetCustomAttribute<ResourceIdPropertyNameAttribute>()?.PropertyName;
                    if (idPropertyName != null)
                    {
                        // Create the alias
                        psObj.Properties.Add(new PSAliasProperty(idPropertyName, ODataConstants.SearchResultProperties.Id));

                        // Hide this property
                        propertiesToHide.Add(idPropertyName);
                    }
                }

                // Get the type name without the leading "#"
                string typeName = psObj.Properties[ODataConstants.SearchResultProperties.ODataType]?.Name?.TrimStart('#');
                if (string.IsNullOrEmpty(typeName))
                {
                    typeName = this.GetType().GetCustomAttribute<ODataTypeAttribute>(false)?.FullName;
                }

                // Add the type name to the list of PowerShell type names for this object
                if (typeName != null)
                {
                    psObj.TypeNames.Insert(0, typeName);

                    // Get this cmdlet's noun
                    string cmdletNoun = this.GetCmdletNoun();

                    // If this cmdlet retrieves resources that can be referenced, add a property to represent the URL which can be used to retrieve this object
                    if (ReferencePathGenerator.TryGetFromCache(cmdletNoun, out ReferencePathGenerator pathGenerator))
                    {
                        // Get the full resource URL
                        string referencePath = pathGenerator.GenerateResourcePath(objectId);
                        string referenceUrl = this.BuildUrl(referencePath);

                        // Get the name of the reference parameter
                        string propertyName = ODataTypeUtils.GetReferenceUrlParameterName(typeName);

                        // Add this reference URL as a property
                        PSNoteProperty psNoteProperty = new PSNoteProperty(propertyName, referenceUrl);
                        psObj.Properties.Add(psNoteProperty);

                        // Hide this property
                        propertiesToHide.Add(propertyName);
                    }
                }

                // Hide the properties we have been tracking
                psObj.SetDefaultProperties(prop => !propertiesToHide.Contains(prop.Name));
            }
        }

        internal string GetCmdletNoun()
        {
            return this.GetType().GetCmdletNoun();
        }
    }
}
