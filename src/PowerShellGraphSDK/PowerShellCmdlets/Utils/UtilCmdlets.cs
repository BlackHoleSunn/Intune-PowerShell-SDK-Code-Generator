﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Management.Automation;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    /// <summary>
    /// <para type="description">Authenticates with Graph.</para>
    /// </summary>
    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.None)]
    public class Connect : PSCmdlet
    {
        /// <summary>
        /// Cmdlet name's verb.
        /// </summary>
        public const string CmdletVerb = VerbsCommunications.Connect;

        /// <summary>
        /// Cmdlet name's noun.
        /// </summary>
        public const string CmdletNoun = "MSGraph";

        private const string ParameterSetPSCredential = "PSCredential";
        private const string ParameterSetCertificate = "Certificate";

        /// <summary>
        /// <para type="description">Whether or not this cmdlet should return the access token that was obtained.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter AccessToken { get; set; }

        /// <summary>
        /// <para type="description">Whether or not to use Graph PPE.</para>
        /// </summary>
        [Parameter]
        public bool UsePPE { get; set; }

        //[Parameter(ParameterSetName = ParameterSetPSCredential, Mandatory = true)]
        //[ValidateNotNull]
        //public PSCredential PSCredential { get; set; }

        //[Parameter(ParameterSetName = ParameterSetCertificate, Mandatory = true)]
        //[ValidateNotNull]
        //public IClientAssertionCertificate Cert { get; set; }

        /// <summary>
        /// Run the cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Get the environment parameters
            EnvironmentParameters environmentParameters = UsePPE
                ? EnvironmentParameters.PPE
                : EnvironmentParameters.Prod;

            // Auth
            AuthenticationResult authResult;
            switch (this.ParameterSetName)
            {
                case ParameterSetPSCredential:
                    // TODO: Implement PSCredential auth
                    throw new PSNotImplementedException();
                case ParameterSetCertificate:
                    // TODO: Implement Certificate auth
                    throw new PSNotImplementedException();
                default:
                    authResult = AuthUtils.Auth(environmentParameters).GetAwaiter().GetResult();
                    break;
            }

            // Return the access token
            if (this.AccessToken)
            {
                this.WriteObject(authResult.AccessToken);
            }
        }
    }

    /// <summary>
    /// <para type="description">Gets the $metadata for the currently selected Graph schema.</para>
    /// </summary>
    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.None)]
    public class GetMetadata : GetCmdlet
    {
        /// <summary>
        /// Cmdlet name's verb.
        /// </summary>
        public const string CmdletVerb = VerbsCommon.Get;

        /// <summary>
        /// Cmdlet name's noun.
        /// </summary>
        public const string CmdletNoun = "MSGraphMetadata";

        internal override string GetResourcePath()
        {
            return "$metadata";
        }

        internal override object ReadResponse(string content)
        {
            // Return the raw response body
            return content;
        }
    }

    /// <summary>
    /// <para type="description">Gets the next page of a search result if an @odata.nextLink is provided.</para>
    /// </summary>
    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.Low,
        DefaultParameterSetName = GetOrSearchCmdlet.OperationName)]
    public class GetNextPage : GetOrSearchCmdlet // we need the behavior of "Search"
    {
        /// <summary>
        /// Cmdlet name's verb.
        /// </summary>
        public const string CmdletVerb = VerbsCommon.Get;

        /// <summary>
        /// Cmdlet name's noun.
        /// </summary>
        public const string CmdletNoun = "MSGraphNextPage";

        /// <summary>
        /// <para type="description">The value provided in the search result in the "@odata.nextLink" property.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias(ODataConstants.SearchResultProperties.ODataNextLink)]
        public string NextLink { get; set; }

        // The properties in this section hide base classes' PowerShell parameters by
        // redefining the properties without adding the [Parameter] attribute
        #region Hidden Parameters
        
        /// <summary>
        /// Hides $filter.
        /// </summary>
        public new string Filter { get; set; }

        /// <summary>
        /// Hides $skip.
        /// </summary>
        public new int? Skip { get; set; }

        /// <summary>
        /// Hides $top.
        /// </summary>
        public new int? Top { get; set; }

        /// <summary>
        /// Hides the "odata.maxpagesize" header.
        /// </summary>
        public new int? MaxPageSize { get; set; }

        #endregion Hidden Parameters

        internal override string GetResourcePath()
        {
            return this.NextLink;
        }
    }

    /// <summary>
    /// <para type="description">Gets the name of the currently selected Graph schema version.</para>
    /// </summary>
    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.None)]
    public class GetSchemaVersion : PSCmdlet
    {
        /// <summary>
        /// Cmdlet name's verb.
        /// </summary>
        public const string CmdletVerb = VerbsCommon.Get;

        /// <summary>
        /// Cmdlet name's noun.
        /// </summary>
        public const string CmdletNoun = "MSGraphSchemaVersion";

        /// <summary>
        /// Runs the cmdlet.
        /// </summary>
        protected override sealed void ProcessRecord()
        {
            this.WriteObject(ODataCmdlet.CurrentEnvironmentParameters.SchemaVersion);
        }
    }

    /// <summary>
    /// <para type="description">Selectes a new Graph schema version.</para>
    /// </summary>
    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.High)]
    public class UpdateSchemaVersion : PSCmdlet
    {
        /// <summary>
        /// Cmdlet name's verb.
        /// </summary>
        public const string CmdletVerb = VerbsData.Update;

        /// <summary>
        /// Cmdlet name's noun.
        /// </summary>
        public const string CmdletNoun = "MSGraphSchemaVersion";

        /// <summary>
        /// <para type="description">The name of the Graph schema version to select.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SchemaVersion { get; set; }

        /// <summary>
        /// Runs the cmdlet.
        /// </summary>
        protected override sealed void ProcessRecord()
        {
            ODataCmdlet.CurrentEnvironmentParameters.SchemaVersion = this.SchemaVersion;
        }
    }

    /// <summary>
    /// <para type="description">Gets the name of the currently selected Graph schema version.</para>
    /// </summary>
    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.None)]
    public class GetBaseUrl : PSCmdlet
    {
        /// <summary>
        /// Cmdlet name's verb.
        /// </summary>
        public const string CmdletVerb = VerbsCommon.Get;

        /// <summary>
        /// Cmdlet name's noun.
        /// </summary>
        public const string CmdletNoun = "MSGraphBaseUrl";

        /// <summary>
        /// Runs the cmdlet.
        /// </summary>
        protected override sealed void ProcessRecord()
        {
            this.WriteObject(ODataCmdlet.CurrentEnvironmentParameters.ResourceBaseAddress);
        }
    }

    /// <summary>
    /// <para type="description">Selectes a new Graph schema version.</para>
    /// </summary>
    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.High)]
    public class UpdateBaseUrl : PSCmdlet
    {
        /// <summary>
        /// Cmdlet name's verb.
        /// </summary>
        public const string CmdletVerb = VerbsData.Update;

        /// <summary>
        /// Cmdlet name's noun.
        /// </summary>
        public const string CmdletNoun = "MSGraphBaseUrl";

        /// <summary>
        /// <para type="description">The name of the Graph schema version to select.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        [ValidateUrl]
        public string Url { get; set; }

        /// <summary>
        /// Runs the cmdlet.
        /// </summary>
        protected override sealed void ProcessRecord()
        {
            if (!Uri.IsWellFormedUriString(this.Url, UriKind.Absolute))
            {
                throw new PSGraphSDKException(
                    new ArgumentException($"Invalid URL", nameof(this.Url)),
                    "InvalidBaseUrl",
                    ErrorCategory.InvalidArgument,
                    this.Url);
            }

            ODataCmdlet.CurrentEnvironmentParameters.ResourceBaseAddress = this.Url;
        }
    }

    /// <summary>
    /// <para type="description">Sends a custom request with the currently active authentication token.</para>
    /// </summary>
    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.Low)]
    public class InvokeRequest : ODataCmdlet
    {
        /// <summary>
        /// Cmdlet name's verb.
        /// </summary>
        public const string CmdletVerb = VerbsLifecycle.Invoke;

        /// <summary>
        /// Cmdlet name's noun.
        /// </summary>
        public const string CmdletNoun = "MSGraphRequest";

        /// <summary>
        /// <para type="description">The HTTP method to use when making the request.</para>
        /// </summary>
        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string HttpMethod { get; set; }

        /// <summary>
        /// <para type="description">The URL to send the request to.</para>
        /// </summary>
        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateUrl]
        public string Url { get; set; }

        /// <summary>
        /// <para type="description">The headers that should be sent with the request.</para>
        /// <para type="description">The authentication token (i.e. the "Bearer" header) is automatically added to the request.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateType(typeof(PSObject), typeof(Hashtable))]
        public object Headers { get; set; }

        /// <summary>
        /// <para type="description">The content that should be sent in the body of the request.</para>
        /// <para type="description">PSObject, Hashtable and HttpContent values will be serialized as JSON, and strings will be sent as-is</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateType(typeof(string), typeof(PSObject), typeof(Hashtable), typeof(HttpContent))]
        public object Content { get; set; }

        internal override string GetHttpMethod()
        {
            return this.HttpMethod;
        }

        internal override string GetResourcePath()
        {
            return this.Url;
        }

        internal override HttpRequestHeaders GetHeaders()
        {
            HttpRequestHeaders headers = base.GetHeaders();
            if (this.Headers != null)
            {
                IDictionary<string, IEnumerable<string>> headerPairs = new Dictionary<string, IEnumerable<string>>();
                if (this.Headers is PSObject psHeaders)
                {
                    foreach (PSPropertyInfo prop in psHeaders.Properties)
                    {
                        if (prop.Value == null)
                        {
                            throw new PSArgumentNullException(nameof(this.Headers), $"The value for the header '{prop.Name}' cannot be null");
                        }

                        if (prop.Value is string propValue)
                        {
                            headerPairs.Add(prop.Name, new string[] { propValue });
                        }
                        else if (prop.Value is IEnumerable<string> propValues)
                        {
                            headerPairs.Add(prop.Name, propValues);
                        }
                        else
                        {
                            throw new PSArgumentException($"The header '{prop.Name}' has an invalid value - all header values must be strings or arrays of strings", nameof(this.Headers));
                        }
                    }
                }
                else if (this.Headers is Hashtable hashHeaders)
                {
                    foreach (object key in hashHeaders.Keys)
                    {
                        if (key == null)
                        {
                            throw new PSArgumentNullException(nameof(this.Headers), "Keys in the 'Headers' hashtable cannot be null");
                        }

                        if (key is string keyStr)
                        {
                            object headerValue = hashHeaders[key];
                            if (headerValue == null)
                            {
                                throw new PSArgumentNullException(nameof(this.Headers), $"The value for the header '{keyStr}' cannot be null");
                            }

                            if (headerValue is string propValue)
                            {
                                headerPairs.Add(keyStr, new string[] { propValue });
                            }
                            else if (headerValue is IEnumerable<string> propValues)
                            {
                                headerPairs.Add(keyStr, propValues);
                            }
                            else
                            {
                                throw new PSArgumentException($"The header '{keyStr}' has an invalid value - all header values must be strings or arrays of strings", nameof(this.Headers));
                            }
                        }
                        else
                        {
                            throw new PSArgumentException($"Key in the 'Headers' hashtable strings - the provided key is of type '{key.GetType()}'");
                        }
                    }
                }

                foreach (KeyValuePair<string, IEnumerable<string>> entry in headerPairs)
                {
                    headers.Add(entry.Key, entry.Value);
                }
            }

            return headers;
        }

        internal override object GetContent()
        {
            return this.Content;
        }

        internal override HttpContent WriteContent(object content)
        {
            // If there's no content, return null
            if (content == null)
            {
                return null;
            }

            // HttpContent
            if (content is HttpContent contentHttp)
            {
                return contentHttp;
            }

            // String
            if (content is string contentString)
            {
                return new StringContent(contentString);
            }

            // Hashtable or PSObject
            if (content is Hashtable || content is PSObject)
            {
                // Convert the object into JSON
                string contentJson = JsonUtils.WriteJson(content);

                // Return the string as HttpContent
                return new StringContent(contentJson);
            }

            // We should have returned before here
            throw new PSArgumentException($"Unknown content type: '{this.Content.GetType()}'", nameof(this.Content));
        }

        internal override object ReadResponse(string content)
        {
            // Try to deserialize the body
            try
            {
                return base.ReadResponse(content);
            }
            catch
            {
                // Return the raw response body
                return PSObject.AsPSObject(content);
            }
        }
    }
}
