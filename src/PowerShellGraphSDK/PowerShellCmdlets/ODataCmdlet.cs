﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using System.Net.Http;

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
    ///         <term><see cref="GetHttpMethod"/></term>
    ///         <description>Gets the HTTP method to use when making the call</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GetResourcePath"/></term>
    ///         <description>Gets the relative URL of the OData resource</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GetUrlQueryOptions"/></term>
    ///         <description>Gets the query options for the call</description>
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
    ///         <term><see cref="GetHeaders"/></term>
    ///         <description>Gets the request headers to be added to the HTTP call</description>
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
    public abstract partial class ODataCmdlet : PSCmdlet, IDynamicParameters
    {
        /// <summary>
        /// The defined dynamic parameters.
        /// </summary>
        protected RuntimeDefinedParameterDictionary DynamicParameters = new RuntimeDefinedParameterDictionary();

        /// <summary>
        /// The currently selected environment parameters
        /// </summary>
        internal static EnvironmentParameters CurrentEnvironmentParameters = GraphAuthentication.EnvironmentParameters;

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
    }
}
