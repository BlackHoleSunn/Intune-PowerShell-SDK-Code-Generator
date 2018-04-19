﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
    using System.Net.Http;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.None)]
    public class Connect : PSCmdlet
    {
        public const string CmdletVerb = VerbsCommunications.Connect;
        public const string CmdletNoun = "MSGraph";

        private const string ParameterSetPSCredential = "PSCredential";
        private const string ParameterSetCertificate = "Certificate";

        [Parameter]
        public bool UsePPE { get; set; }

        //[Parameter(ParameterSetName = ParameterSetPSCredential, Mandatory = true)]
        //[ValidateNotNull]
        //public PSCredential PSCredential { get; set; }

        //[Parameter(ParameterSetName = ParameterSetCertificate, Mandatory = true)]
        //[ValidateNotNull]
        //public IClientAssertionCertificate Cert { get; set; }

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
                    authResult = GraphAuthentication.Auth(environmentParameters).GetAwaiter().GetResult();
                    break;
            }
        }
    }

    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.None)]
    public class GetMetadata : GetCmdlet
    {
        public const string CmdletVerb = VerbsCommon.Get;
        public const string CmdletNoun = "MSGraphMetadata";

        internal override string GetResourcePath()
        {
            return "$metadata";
        }

        internal override PSObject ReadResponse(string content)
        {
            // Return the raw response body
            return PSObject.AsPSObject(content);
        }
    }

    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.Low,
        DefaultParameterSetName = GetOrSearchCmdlet.OperationName)]
    public class GetNextPage : GetOrSearchCmdlet // we need the behavior of "Search"
    {
        public const string CmdletVerb = VerbsCommon.Get;
        public const string CmdletNoun = "MSGraphNextPage";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias(ODataConstants.SearchResultProperties.NextLink)]
        public string NextLink { get; set; }

        /// The properties in this section hide base classes' PowerShell parameters by
        /// redefining the properties without the [Parameter()] attribute
        #region Hidden Parameters
        
        public new string SchemaVersion { get; }
        public new string Select { get; }
        public new string Expand { get; }
        public new string Filter { get; }
        public new string OrderBy { get; }
        public new string Skip { get; }
        public new string Top { get; }

        #endregion Hidden Parameters

        internal override string GetResourcePath()
        {
            return this.NextLink;
        }
    }

    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.Medium,
        DefaultParameterSetName = ParameterSetSearchResult)]
    public class GetAllPages : PSCmdlet
    {
        public const string CmdletVerb = VerbsCommon.Get;
        public const string CmdletNoun = "MSGraphAllPages";

        private const string ParameterSetNextLink = "NextLink";
        private const string ParameterSetSearchResult = "SearchResult";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetNextLink, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [Alias(ODataConstants.SearchResultProperties.NextLink)]
        public string NextLink { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetSearchResult, ValueFromPipeline = true)]
        [ValidateNotNull]
        public PSObject SearchResult { get; set; }

        protected override sealed void ProcessRecord()
        {
            this.WriteDebug("Selected parameter set: " + this.ParameterSetName);

            // Get nextLink and values
            string nextLink = null;
            switch (this.ParameterSetName)
            {
                case ParameterSetNextLink: nextLink = this.NextLink; break;
                case ParameterSetSearchResult:
                    {
                        // Extract values and nextLink from search result
                        nextLink = GetNextLinkFromSearchResult(this.SearchResult);
                        PSObject[] values = GetValuesFromSearchResult(this.SearchResult);

                        // If the object does not have values or a nextLink, send it to the pipeline and exit the cmdlet
                        if (nextLink == null && values == null)
                        {
                            this.WriteObject(SearchResult);
                            return;
                        }
                        else if (values != null)
                        {
                            // We found values, so send them to the pipeline
                            this.WriteDebug($"Writing first page to pipeline");
                            foreach (var value in values)
                            {
                                this.WriteObject(value);
                            }
                        }
                    }
                    break;
                default: throw new PSArgumentException("Unable to determine parameter set");
            }

            // Iterate over each page and write the results to the pipeline
            string currentNextLink = nextLink;
            while (!string.IsNullOrEmpty(currentNextLink))
            {
                this.WriteDebug($"Starting page '{currentNextLink}'");
                // Create an invocation of the Get-MSGraphNextPage cmdlet
                // NOTE: we cannot just create an instance of "GetNextPage" and call "Invoke" on it, because it fails when run on PSCmdlet objects
                //ScriptBlock script = ScriptBlock.Create($"{GetNextPage.CmdletVerb}-{GetNextPage.CmdletNoun} -{nameof(GetNextPage.NextLink)} '{currentNextLink}'");

                // Call the Get-MSGraphNextPage cmdlet
                IEnumerable<PSObject> obj = null;
                PowerShell ps = PowerShell.Create(RunspaceMode.CurrentRunspace);
                ps.AddCommand($"{GetNextPage.CmdletVerb}-{GetNextPage.CmdletNoun}");
                ps.AddParameter(nameof(GetNextPage.NextLink), currentNextLink);
                ps.AddParameters(this.MyInvocation.BoundParameters
                    .Where(entry => !(entry.Key == nameof(this.NextLink) || entry.Key == nameof(this.SearchResult)))
                    .ToDictionary(entry => entry.Key, entry => entry.Value));
                obj = ps.Invoke();

                this.WriteDebug("Results: " + string.Join("\n", obj.Select(o => o.ToString())));

                // Check if we got any results back
                PSObject psObj = obj.Cast<PSObject>().SingleOrDefault();
                if (psObj != null)
                {
                    // Write the values to the pipeline
                    PSObject[] nextPage = GetValuesFromSearchResult(psObj);
                    if (nextPage != null)
                    {
                        // Output all the results in this page
                        foreach (PSObject result in nextPage)
                        {
                            this.WriteObject(result);
                        }
                    }

                    // Set the next link
                    currentNextLink = GetNextLinkFromSearchResult(psObj);
                }
                else
                {
                    currentNextLink = null;
                }
            }
        }

        private static string GetNextLinkFromSearchResult(PSObject searchResult)
        {
            if (searchResult == null)
            {
                throw new PSArgumentNullException(nameof(searchResult), "PowerShell validation failed while getting nextLink");
            }

            string nextLink = searchResult.Members[ODataConstants.SearchResultProperties.NextLink]?.Value?.ToString();
            return nextLink;
        }

        private static PSObject[] GetValuesFromSearchResult(PSObject searchResult)
        {
            if (searchResult == null)
            {
                throw new PSArgumentNullException(nameof(searchResult), "PowerShell validation failed while getting values");
            }

            PSObject wrappedArray = searchResult.Members[ODataConstants.SearchResultProperties.Value]?.Value as PSObject;
            PSObject[] values = wrappedArray?.BaseObject as PSObject[];

            return values;
        }
    }

    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.None)]
    public class GetSchemaVersion : PSCmdlet
    {
        public const string CmdletVerb = VerbsCommon.Get;
        public const string CmdletNoun = "MSGraphSchemaVersion";

        protected override sealed void ProcessRecord()
        {
            this.WriteObject(ODataPowerShellSDKCmdletBase.SchemaVersion);
        }
    }

    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.High)]
    public class SetSchemaVersion : PSCmdlet
    {
        public const string CmdletVerb = VerbsCommon.Set;
        public const string CmdletNoun = "MSGraphSchemaVersion";

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string SchemaVersion { get; set; }

        protected override sealed void ProcessRecord()
        {
            ODataPowerShellSDKCmdletBase.SchemaVersion = this.SchemaVersion;
        }
    }

    [Cmdlet(
        CmdletVerb, CmdletNoun,
        ConfirmImpact = ConfirmImpact.Low)]
    public class InvokeRequest : ODataPowerShellSDKCmdletBase
    {
        public const string CmdletVerb = VerbsLifecycle.Invoke;
        public const string CmdletNoun = "MSGraphRequest";

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string HttpMethod { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateUrl]
        public string Url { get; set; }

        // TODO: Document that this parameter can be a string, PSObject, Hashtable or an HttpContent object
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

        internal override PSObject ReadResponse(string content)
        {
            // Return the raw response body
            return PSObject.AsPSObject(content);
        }
    }
}
