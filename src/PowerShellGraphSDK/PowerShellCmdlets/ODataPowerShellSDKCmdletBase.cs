﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Reflection;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The common behavior between all OData PowerShell SDK cmdlets.
    /// </summary>
    /// <remarks>
    /// Overridable methods are executed in this order:
    /// <list type="number">
    ///     <listheader>
    ///         <term>Method</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><see cref="GetResourcePath"/></term>
    ///         <description>Gets the relative URL of the OData resource</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GetUrlQueryOptions"/></term>
    ///         <description>Gets the query options for the call</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GetHttpMethod"/></term>
    ///         <description>Gets the HTTP method to use when making the call</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GetContentType"/></term>
    ///         <description>Gets the MIME type for the content in the request body</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GetContent"/></term>
    ///         <description>Gets the request body for the HTTP call</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="ProceedWithCall(string, string, IDictionary{string, string}, object, string)"/></term>
    ///         <description>Determines whether or not to proceed with the call</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="WriteContent(object)"/></term>
    ///         <description>Creates an <see cref="HttpContent"/> object from the result of <see cref="GetContent"/></description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="ReadResponse(string)"/></term>
    ///         <description>Converts the HTTP response body into a native PowerShell object</description>
    ///     </item>
    /// </list>
    /// </remarks>
    public abstract class ODataPowerShellSDKCmdletBase : PSCmdlet, IDynamicParameters
    {
        /// <summary>
        /// The Graph schema version to use when making a Graph call.
        /// </summary>
        public static string SchemaVersion { get; set; } = "v1.0";

        /// <summary>
        /// The defined dynamic parameters.
        /// </summary>
        protected RuntimeDefinedParameterDictionary DynamicParameters = null;

        /// <summary>
        /// The method that the PowerShell runtime will call.  This is the entry point for the cmdlet.
        /// </summary>
        protected override sealed void ProcessRecord()
        {
            try
            {
                // Try to run the cmdlet behavior
                this.Run();
            }
            catch (Exception ex)
            {
                // Write any errors to the console
                this.WriteError(ex);
            }
        }

        #region Overridable

        /// <summary>
        /// Returns the HTTP method to be used for the network call.  This method should never return null.
        /// </summary>
        /// <remarks>
        /// This method returns "GET" if it is not overridden.
        /// </remarks>
        /// <returns>The HTTP method.</returns>
        internal virtual string GetHttpMethod()
        {
            return "GET";
        }

        /// <summary>
        /// Returns the path to the resource.  This may be either a relative or absolute URL.  This method should never return null.
        /// </summary>
        /// <returns>The path to the resource.</returns>
        internal abstract string GetResourcePath();

        /// <summary>
        /// Returns a mapping of query options to their values.  This method should never return null.
        /// Implementations of this method should first call <code>base.GetUrlQueryOptions()</code> and then
        /// add additional query options to the result.
        /// </summary>
        /// <remarks>
        /// The keys should be the full query option name (i.e. WITH the "$" prefix for "$select", "$expand", etc.).
        /// </remarks>
        /// <returns>The mapping of query options to their values.</returns>
        internal virtual IDictionary<string, string> GetUrlQueryOptions()
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Returns the content to be sent in the network call.  This method may return null if there is no content to send.
        /// </summary>
        /// <remarks>
        /// This method returns null if it is not overridden.
        /// </remarks>
        /// <returns>The request content.</returns>
        internal virtual object GetContent()
        {
            return null;
        }

        /// <summary>
        /// Returns the content (MIME) type for the HTTP request.
        /// </summary>
        /// <remarks>
        /// This method returns "application/json" if it is not overridden.
        /// </remarks>
        /// <returns>The request's content type.</returns>
        internal virtual string GetContentType()
        {
            return "application/json";
        }

        /// <summary>
        /// A confirmation step that the call should proceed to be made.  This method allows for derived types to have an opportunity to
        /// stop execution without throwing an exception.
        /// </summary>
        /// <param name="httpMethod">The evaluated HTTP method</param>
        /// <param name="resourcePath">The evaluated OData resource path</param>
        /// <param name="queryOptions">The evaluated query options</param>
        /// <param name="content">The evaluated content</param>
        /// <param name="contentType">The evaluated MIME type</param>
        /// <returns>True if the cmdlet should proceed with making the HTTP call, otherwise false.</returns>
        internal virtual bool ProceedWithCall(
            string httpMethod,
            string resourcePath,
            IDictionary<string, string> queryOptions,
            object content,
            string contentType)
        {
            return true;
        }

        /// <summary>
        /// Converts the content obtained from the <see cref="GetContent"/> method into an <see cref="HttpContent"/> object.
        /// </summary>
        /// <remarks>
        /// This method defaults to converting the object to a JSON string and then wrapping it in a <see cref="StringContent"/> object.
        /// </remarks>
        /// <param name="content">The content to be converted</param>
        /// <returns>The converted HttpContent object.</returns>
        internal virtual HttpContent WriteContent(object content)
        {
            string contentString = JsonUtils.WriteJson(content);
            HttpContent result = new StringContent(contentString);
            return result;
        }

        /// <summary>
        /// Converts the body of an HTTP response to a C# object.
        /// </summary>
        /// <remarks>
        /// This method defaults to assuming a JSON response body, and then converting it to a <see cref="PSObject"/> instance.
        /// </remarks>
        /// <param name="content">The HTTP response body</param>
        /// <returns>The converted object.</returns>
        internal virtual object ReadResponse(string content)
        {
            JToken jsonToken = JsonUtils.ReadJson(content);
            return JsonUtils.ToPowerShellObject(jsonToken);
        }

        /// <summary>
        /// The parameters that are added at runtime.
        /// </summary>
        /// <returns>A <see cref="RuntimeDefinedParameterDictionary"/>.</returns>
        public virtual object GetDynamicParameters()
        {
            this.DynamicParameters = new RuntimeDefinedParameterDictionary();
            return this.DynamicParameters;
        }

        #endregion Overridable

        #region Helpers

        /// <summary>
        /// Creates mappings from property names to property values for all bound properties for the current invocation of this cmdlet.
        /// </summary>
        /// <param name="filter">The filter for properties to be included in the result (if it evaluates to true, the property is included)</param>
        /// <returns>The mappings from property names to property values.</returns>
        internal IDictionary<string, object> CreateDictionaryFromBoundProperties(Func<PropertyInfo, bool> filter = null)
        {
            // Get the properties that were set by the user in this invocation of the PowerShell cmdlet
            IEnumerable<PropertyInfo> boundProperties = this.GetBoundProperties(false, filter);

            // Create a dictionary of the values for these properties
            IDictionary<string, object> result = new Dictionary<string, object>();
            foreach (PropertyInfo propInfo in boundProperties)
            {
                result.Add(propInfo.Name, propInfo.GetValue(this));
            }

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
        internal IEnumerable<PropertyInfo> GetProperties(bool includeInherited = true, Func<PropertyInfo, bool> filter = null)
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

            return result;
        }

        #endregion Helpers

        #region Private

        /// <summary>
        /// Writes an exception to the PowerShell console.  If the exception does not represent a PowerShell error,
        /// it will be wrapped in a PowerShell error object before being written to the console.
        /// </summary>
        /// <param name="ex">The exception to write to the console</param>
        private void WriteError(Exception ex)
        {
            ErrorRecord errorRecord;
            if (ex is IContainsErrorRecord powerShellError)
            {
                errorRecord = powerShellError.ErrorRecord;
            }
            else
            {
                errorRecord = new ErrorRecord(
                    ex,
                    PSGraphSDKException.ErrorPrefix + "UnknownError",
                    ErrorCategory.OperationStopped,
                    null);
            }

            this.WriteError(errorRecord);
        }

        private AuthenticationResult Auth(EnvironmentParameters environmentParameters)
        {
            AuthenticationResult authResult = environmentParameters?.AuthResult;
            string cmdletName = $"{PowerShellCmdlets.Connect.CmdletVerb}-{PowerShellCmdlets.Connect.CmdletNoun}";
            if (authResult == null)
            {
                // User has not authenticated
                throw new PSAuthenticationError(
                    new InvalidOperationException($"Not authenticated.  Please use the '{cmdletName}' cmdlet to authenticate."),
                    "NotAuthenticated",
                    ErrorCategory.AuthenticationError,
                    null);
            }
            else if (authResult.ExpiresOn <= DateTimeOffset.Now)
            {
                // Expired token
                throw new PSAuthenticationError(
                    new InvalidOperationException($"Authentication has expired.  Please use the '{cmdletName}' cmdlet to authenticate."),
                    "AuthenticationExpired",
                    ErrorCategory.AuthenticationError,
                    authResult);
            }

            return authResult;
        }

        /// <summary>
        /// Runs a cmdlet.
        /// </summary>
        internal void Run()
        {
            // Get the environment parameters
            EnvironmentParameters environmentParameters = GraphAuthentication.EnvironmentParameters;

            // Auth
            AuthenticationResult authResult = this.Auth(environmentParameters);

            // Get all the evaluated values
            string httpMethodString = this.GetHttpMethod();
            string resourcePath = this.GetResourcePath();
            IDictionary<string, string> queryOptions = this.GetUrlQueryOptions();
            object contentObject = this.GetContent();
            string contentType = this.GetContentType();

            // Determine whether or not the call should proceed
            if (!this.ProceedWithCall(
                httpMethodString,
                resourcePath,
                queryOptions,
                contentObject,
                contentType))
            {
                return;
            }

            // Build the URL
            string requestUrl;
            if (resourcePath == null)
            {
                throw new PSGraphSDKException(
                    new ArgumentNullException(nameof(this.GetResourcePath)),
                    "NullResourceUrl",
                    ErrorCategory.InvalidArgument,
                    resourcePath);
            }
            // Remove the leading slash if it exists so relative URLs don't get treated as absolute URLs
            string baseAddress = environmentParameters.ResourceBaseAddress?.Trim('/');
            if (Uri.IsWellFormedUriString(resourcePath, UriKind.Absolute))
            {
                // Since this is an absolute URL, it must be the full request URL
                requestUrl = resourcePath;
            }
            else if (Uri.IsWellFormedUriString(resourcePath, UriKind.Relative))
            {
                // Sanitize the URL
                //resourcePath = WebUtility.UrlEncode(resourcePath);

                // Get the full base URL
                string baseUrlWithSchema = $"{baseAddress}/{SchemaVersion}";

                // Append the relative URL to the base URL
                requestUrl = $"{baseUrlWithSchema}/{resourcePath}";
            }
            else
            {
                throw new PSGraphSDKException(
                    new ArgumentException("A valid URL could not be constructed - ensure that the provided resource path is a valid relative or absolute URL", nameof(this.GetResourcePath)),
                    "InvalidResourceUrl",
                    ErrorCategory.InvalidArgument,
                    resourcePath);
            }

            // Add the query options to the URL
            if (queryOptions != null && queryOptions.Any())
            {
                string queryOptionsString;
                if (requestUrl.Contains("?"))
                {
                    // Query options already exist in the URL
                    queryOptionsString = "&";
                }
                else
                {
                    // Query options don't already exist in the URL
                    queryOptionsString = "?";
                }

                // Construct the query options string
                queryOptionsString += string.Join("&", queryOptions.Select((entry) => $"{entry.Key}={entry.Value}"));

                // Append the query options to the URL
                requestUrl += queryOptionsString;
            }

            // Get HTTP method
            if (string.IsNullOrWhiteSpace(httpMethodString))
            {
                throw new PSGraphSDKException(
                    new ArgumentNullException(nameof(this.GetHttpMethod)),
                    "InvalidHttpMethod",
                    ErrorCategory.InvalidArgument,
                    httpMethodString);
            }
            HttpMethod httpMethod = new HttpMethod(httpMethodString);

            // Get content
            HttpContent content = null;
            if (contentObject != null)
            {
                // Create the HttpContent object if there is any content
                content = this.WriteContent(contentObject);

                // Set the content type
                content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            }

            // Prepare the HTTP request
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, requestUrl);
            string requestContent = null; // need to evaluate this before making the call otherwise the content object will get disposed
            if (content != null)
            {
                // Get the content before making the call
                requestMessage.Content = content;
                requestContent = requestMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

            // Debug request message
            this.WriteDebug(
$@"
-----------
| Request |
-----------
{requestMessage.Method.Method} {requestMessage.RequestUri.ToString()}
{requestContent ?? "<No request content>"}
");

            // Make the HTTP request
            HttpResponseMessage responseMessage = httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();
            string responseContent = responseMessage.Content?.ReadAsStringAsync().GetAwaiter().GetResult();

            // Debug response message
            this.WriteDebug(
$@"
------------
| Response |
------------
{responseContent ?? "<No response content>"}
");

            // Handle the result
            if (responseMessage.IsSuccessStatusCode)
            {
                // Get the result
                object cmdletResult = null;
                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    cmdletResult = this.ReadResponse(responseContent);
                }

                // Write the result to the pipeline
                if (cmdletResult != null)
                {
                    this.WriteObject(cmdletResult);
                }
            }
            else
            {
                // Build the error message
                string errorMessage = $"{(int)responseMessage.StatusCode} {responseMessage.ReasonPhrase}";
                if (responseContent != null)
                {
                    errorMessage += Environment.NewLine + responseContent;
                }

                // Convert request message to a PowerShell object
                PSObject powerShellRequestObject = new PSObject();
                powerShellRequestObject.Members.Add(new PSNoteProperty("HttpMethod", requestMessage.Method.Method));
                powerShellRequestObject.Members.Add(new PSNoteProperty("URL", requestMessage.RequestUri.ToString()));
                PSObject powerShellRequestObjectHeaders = new PSObject();
                foreach (var header in requestMessage.Headers)
                {
                    powerShellRequestObjectHeaders.Members.Add(new PSNoteProperty(header.Key, string.Join(",", header.Value)));
                }
                powerShellRequestObject.Members.Add(new PSNoteProperty("Headers", powerShellRequestObjectHeaders));
                if (requestContent != null)
                {
                    powerShellRequestObject.Members.Add(new PSNoteProperty("Content", requestContent));
                }

                // Convert response message to a PowerShell object
                PSObject powerShellResponseObject = new PSObject();
                powerShellResponseObject.Members.Add(new PSNoteProperty("HttpStatusCode", (int)responseMessage.StatusCode));
                powerShellResponseObject.Members.Add(new PSNoteProperty("HttpStatusPhrase", responseMessage.ReasonPhrase));
                PSObject powerShellResponseObjectHeaders = new PSObject();
                foreach (var header in responseMessage.Headers)
                {
                    powerShellResponseObjectHeaders.Members.Add(new PSNoteProperty(header.Key, string.Join(",", header.Value)));
                }
                powerShellResponseObject.Members.Add(new PSNoteProperty("Headers", powerShellResponseObjectHeaders));
                powerShellResponseObject.Members.Add(new PSNoteProperty("Content", responseContent));

                // Create PowerShell error object
                PSObject powerShellErrorObject = new PSObject();
                powerShellErrorObject.Members.Add(new PSNoteProperty("Request", powerShellRequestObject));
                powerShellErrorObject.Members.Add(new PSNoteProperty("Response", powerShellResponseObject));

                throw new PSGraphSDKException(
                    new HttpRequestException(errorMessage),
                    "HttpRequestError",
                    ErrorCategory.ConnectionError,
                    powerShellErrorObject);
            }
        }

        #endregion Private
    }
}
