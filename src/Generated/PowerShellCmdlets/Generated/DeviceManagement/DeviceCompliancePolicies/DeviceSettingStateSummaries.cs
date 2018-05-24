// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System.Management.Automation;

    /// <summary>
    ///     <para type="synopsis">Retrieves &quot;microsoft.graph.settingStateDeviceSummary&quot; objects.</para>
    ///     <para type="description">GET ~/deviceManagement/deviceCompliancePolicies/{deviceCompliancePolicyId}/deviceSettingStateSummaries</para>
    ///     <para type="description">Retrieves &quot;microsoft.graph.settingStateDeviceSummary&quot; objects in the &quot;deviceSettingStateSummaries&quot; collection.</para>
    ///     <para type="description">Compliance Setting State Device Summary</para>
    /// </summary>
    [Cmdlet("Get", "DeviceManagement_DeviceCompliancePolicies_DeviceSettingStateSummaries", DefaultParameterSetName = @"Search")]
    [ODataType("microsoft.graph.settingStateDeviceSummary")]
    public class Get_DeviceManagement_DeviceCompliancePolicies_DeviceSettingStateSummaries : GetOrSearchCmdlet
    {
        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.deviceCompliancePolicy&quot; object in the &quot;deviceCompliancePolicies&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.deviceCompliancePolicy&quot; object in the &quot;deviceCompliancePolicies&quot; collection")]
        public System.String deviceCompliancePolicyId { get; set; }

        /// <summary>
        ///     <para type="description">The ID for a &quot;microsoft.graph.settingStateDeviceSummary&quot; object in the &quot;deviceSettingStateSummaries&quot; collection</para>
        /// </summary>
        [Selectable]
        [Alias("id")]
        [ValidateNotNullOrEmpty]
        [Parameter(ParameterSetName = @"Get", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID for a &quot;microsoft.graph.settingStateDeviceSummary&quot; object in the &quot;deviceSettingStateSummaries&quot; collection")]
        public System.String deviceSettingStateSummaryId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;settingName&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Name of the setting</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Sortable]
        public System.String settingName { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;instancePath&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Name of the InstancePath for the setting</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Sortable]
        public System.String instancePath { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;unknownDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Unkown count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 unknownDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;notApplicableDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Not Applicable count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 notApplicableDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;compliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Compliant count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 compliantDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;remediatedDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Compliant count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 remediatedDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;nonCompliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device NonCompliant count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 nonCompliantDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;errorDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device error count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 errorDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;conflictDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device conflict error count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 conflictDeviceCount { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/deviceCompliancePolicies/{deviceCompliancePolicyId}/deviceSettingStateSummaries/{deviceSettingStateSummaryId ?? string.Empty}";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Removes a &quot;microsoft.graph.settingStateDeviceSummary&quot; object.</para>
    ///     <para type="description">DELETE ~/deviceManagement/deviceCompliancePolicies/{deviceCompliancePolicyId}/deviceSettingStateSummaries</para>
    ///     <para type="description">Removes a &quot;microsoft.graph.settingStateDeviceSummary&quot; object from the &quot;deviceSettingStateSummaries&quot; collection.</para>
    ///     <para type="description">Compliance Setting State Device Summary</para>
    /// </summary>
    [Cmdlet("Remove", "DeviceManagement_DeviceCompliancePolicies_DeviceSettingStateSummaries", ConfirmImpact = ConfirmImpact.High)]
    [ODataType("microsoft.graph.settingStateDeviceSummary")]
    public class Remove_DeviceManagement_DeviceCompliancePolicies_DeviceSettingStateSummaries : DeleteCmdlet
    {
        /// <summary>
        ///     <para type="description">The ID for a &quot;microsoft.graph.settingStateDeviceSummary&quot; object in the &quot;deviceSettingStateSummaries&quot; collection</para>
        /// </summary>
        [Selectable]
        [Alias("id")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID for a &quot;microsoft.graph.settingStateDeviceSummary&quot; object in the &quot;deviceSettingStateSummaries&quot; collection")]
        public System.String deviceSettingStateSummaryId { get; set; }

        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.deviceCompliancePolicy&quot; object in the &quot;deviceCompliancePolicies&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.deviceCompliancePolicy&quot; object in the &quot;deviceCompliancePolicies&quot; collection")]
        public System.String deviceCompliancePolicyId { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/deviceCompliancePolicies/{deviceCompliancePolicyId}/deviceSettingStateSummaries/{deviceSettingStateSummaryId}";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Creates a &quot;microsoft.graph.settingStateDeviceSummary&quot; object.</para>
    ///     <para type="description">POST ~/deviceManagement/deviceCompliancePolicies/{deviceCompliancePolicyId}/deviceSettingStateSummaries</para>
    ///     <para type="description">Adds a &quot;microsoft.graph.settingStateDeviceSummary&quot; object to the &quot;deviceSettingStateSummaries&quot; collection.</para>
    ///     <para type="description">Compliance Setting State Device Summary</para>
    /// </summary>
    [Cmdlet("New", "DeviceManagement_DeviceCompliancePolicies_DeviceSettingStateSummaries", ConfirmImpact = ConfirmImpact.Low, DefaultParameterSetName = @"#microsoft.graph.settingStateDeviceSummary")]
    [ODataType("microsoft.graph.settingStateDeviceSummary")]
    public class New_DeviceManagement_DeviceCompliancePolicies_DeviceSettingStateSummaries : PostCmdlet
    {
        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.deviceCompliancePolicy&quot; object in the &quot;deviceCompliancePolicies&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.deviceCompliancePolicy&quot; object in the &quot;deviceCompliancePolicies&quot; collection")]
        public System.String deviceCompliancePolicyId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;settingName&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Name of the setting</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;settingName&quot; property, of type &quot;Edm.String&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;settingName&quot; property, of type &quot;Edm.String&quot;.")]
        public System.String settingName { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;instancePath&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Name of the InstancePath for the setting</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;instancePath&quot; property, of type &quot;Edm.String&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;instancePath&quot; property, of type &quot;Edm.String&quot;.")]
        public System.String instancePath { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;unknownDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Unkown count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;unknownDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;unknownDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 unknownDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;notApplicableDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Not Applicable count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;notApplicableDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;notApplicableDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 notApplicableDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;compliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Compliant count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;compliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;compliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 compliantDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;remediatedDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Compliant count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;remediatedDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;remediatedDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 remediatedDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;nonCompliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device NonCompliant count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;nonCompliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;nonCompliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 nonCompliantDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;errorDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device error count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;errorDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;errorDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 errorDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;conflictDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device conflict error count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;conflictDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;conflictDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 conflictDeviceCount { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/deviceCompliancePolicies/{deviceCompliancePolicyId}/deviceSettingStateSummaries";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Updates a &quot;microsoft.graph.settingStateDeviceSummary&quot;.</para>
    ///     <para type="description">PATCH ~/deviceManagement/deviceCompliancePolicies/{deviceCompliancePolicyId}/deviceSettingStateSummaries</para>
    ///     <para type="description">Updates a &quot;microsoft.graph.settingStateDeviceSummary&quot; object in the &quot;deviceSettingStateSummaries&quot; collection.</para>
    ///     <para type="description">Compliance Setting State Device Summary</para>
    /// </summary>
    [Cmdlet("Update", "DeviceManagement_DeviceCompliancePolicies_DeviceSettingStateSummaries", ConfirmImpact = ConfirmImpact.Medium, DefaultParameterSetName = @"#microsoft.graph.settingStateDeviceSummary")]
    [ODataType("microsoft.graph.settingStateDeviceSummary")]
    public class Update_DeviceManagement_DeviceCompliancePolicies_DeviceSettingStateSummaries : PatchCmdlet
    {
        /// <summary>
        ///     <para type="description">The ID for a &quot;microsoft.graph.settingStateDeviceSummary&quot; object in the &quot;deviceSettingStateSummaries&quot; collection</para>
        /// </summary>
        [Selectable]
        [Alias("id")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID for a &quot;microsoft.graph.settingStateDeviceSummary&quot; object in the &quot;deviceSettingStateSummaries&quot; collection")]
        public System.String deviceSettingStateSummaryId { get; set; }

        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.deviceCompliancePolicy&quot; object in the &quot;deviceCompliancePolicies&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.deviceCompliancePolicy&quot; object in the &quot;deviceCompliancePolicies&quot; collection")]
        public System.String deviceCompliancePolicyId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;settingName&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Name of the setting</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;settingName&quot; property, of type &quot;Edm.String&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;settingName&quot; property, of type &quot;Edm.String&quot;.")]
        public System.String settingName { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;instancePath&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Name of the InstancePath for the setting</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;instancePath&quot; property, of type &quot;Edm.String&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;instancePath&quot; property, of type &quot;Edm.String&quot;.")]
        public System.String instancePath { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;unknownDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Unkown count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;unknownDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;unknownDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 unknownDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;notApplicableDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Not Applicable count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;notApplicableDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;notApplicableDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 notApplicableDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;compliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Compliant count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;compliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;compliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 compliantDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;remediatedDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device Compliant count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;remediatedDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;remediatedDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 remediatedDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;nonCompliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device NonCompliant count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;nonCompliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;nonCompliantDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 nonCompliantDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;errorDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device error count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;errorDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;errorDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 errorDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;conflictDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.settingStateDeviceSummary&quot; type.</para>
        ///     <para type="description">Device conflict error count for the setting</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.settingStateDeviceSummary", HelpMessage = @"The &quot;conflictDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;conflictDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 conflictDeviceCount { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/deviceCompliancePolicies/{deviceCompliancePolicyId}/deviceSettingStateSummaries/{deviceSettingStateSummaryId}";
        }
    }
}