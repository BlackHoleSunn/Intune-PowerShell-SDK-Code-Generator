﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Models
{
    using System;

    /// <summary>
    /// An abstract representation of a PowerShell cmdlet.
    /// </summary>
    public class Cmdlet
    {
        /// <summary>
        /// The name of this cmdlet.
        /// </summary>
        public CmdletName Name { get; }

        /// <summary>
        /// The base type of this cmdlet in the generated output.
        /// </summary>
        public string BaseType { get; set; }

        /// <summary>
        /// The impact level of this cmdlet.
        /// This corresponds to the "ConfirmImpact" enum in the System.Management.Automation assembly.
        /// </summary>
        public CmdletImpactLevel ImpactLevel { get; }

        /// <summary>
        /// The HTTP method to be used when making the call.
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// The absolute or relative url to be used when making the call.  For relative URLs, the base
        /// URL will be the OData endpoint.  To use values obtained from parameters, this string should
        /// be formatted like an interpolated string with the parameter name as the variable name.
        /// For example, if this cmdlet had a parameter with the name "id", the CallUrl might look like:
        /// <para>
        /// <code>"/deviceAppManagement/mobileApps/{id}"</code>
        /// </para>
        /// </summary>
        public string CallUrl { get; set; }

        /// <summary>
        /// This cmdlet's parameter sets.
        /// </summary>
        public CmdletParameterSets ParameterSets { get; } = new CmdletParameterSets();

        /// <summary>
        /// Creates a new representation of a Graph SDK cmdlet.
        /// </summary>
        /// <param name="cmdletName">The name of the cmdlet</param>
        public Cmdlet(CmdletName cmdletName, CmdletImpactLevel cmdletImpactLevel)
        {
            this.Name = cmdletName ?? throw new ArgumentNullException(nameof(cmdletName));
            this.ImpactLevel = cmdletImpactLevel;
        }
    }
}
