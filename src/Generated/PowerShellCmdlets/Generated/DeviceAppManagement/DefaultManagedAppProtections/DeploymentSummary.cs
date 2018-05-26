// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System.Management.Automation;

    /// <summary>
    ///     <para type="synopsis">Retrieves the &quot;deploymentSummary&quot; object.</para>
    ///     <para type="description">GET ~/deviceAppManagement/defaultManagedAppProtections/{defaultManagedAppProtectionId}/deploymentSummary</para>
    ///     <para type="description">Retrieves the &quot;deploymentSummary&quot; object (which is of type &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot;).</para>
    ///     <para type="description">Navigation property to deployment summary of the configuration.</para>
    /// </summary>
    [Cmdlet("Get", "DeviceAppManagement_DefaultManagedAppProtections_DeploymentSummary", DefaultParameterSetName = @"Get")]
    [ODataType("microsoft.graph.managedAppPolicyDeploymentSummary")]
    public class Get_DeviceAppManagement_DefaultManagedAppProtections_DeploymentSummary : GetCmdlet
    {
        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.defaultManagedAppProtection&quot; object in the &quot;defaultManagedAppProtections&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.defaultManagedAppProtection&quot; object in the &quot;defaultManagedAppProtections&quot; collection")]
        public System.String defaultManagedAppProtectionId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;displayName&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Sortable]
        public System.String displayName { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;configurationDeployedUserCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 configurationDeployedUserCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;lastRefreshTime&quot; property, of type &quot;Edm.DateTimeOffset&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("Edm.DateTimeOffset")]
        [Selectable]
        [Sortable]
        public System.DateTimeOffset lastRefreshTime { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;configurationDeploymentSummaryPerApp&quot; property, of type &quot;microsoft.graph.managedAppPolicyDeploymentSummaryPerApp&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("microsoft.graph.managedAppPolicyDeploymentSummaryPerApp")]
        [Selectable]
        public System.Object[] configurationDeploymentSummaryPerApp { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;version&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        ///     <para type="description">Version of the entity.</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Sortable]
        public System.String version { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceAppManagement/defaultManagedAppProtections/{defaultManagedAppProtectionId}/deploymentSummary";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Removes a &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; object.</para>
    ///     <para type="description">DELETE ~/deviceAppManagement/defaultManagedAppProtections/{defaultManagedAppProtectionId}/deploymentSummary</para>
    ///     <para type="description">Removes the &quot;deploymentSummary&quot; object (which is of type &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot;).</para>
    ///     <para type="description">Navigation property to deployment summary of the configuration.</para>
    /// </summary>
    [Cmdlet("Remove", "DeviceAppManagement_DefaultManagedAppProtections_DeploymentSummary", ConfirmImpact = ConfirmImpact.High)]
    [ODataType("microsoft.graph.managedAppPolicyDeploymentSummary")]
    public class Remove_DeviceAppManagement_DefaultManagedAppProtections_DeploymentSummary : DeleteCmdlet
    {
        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.defaultManagedAppProtection&quot; object in the &quot;defaultManagedAppProtections&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.defaultManagedAppProtection&quot; object in the &quot;defaultManagedAppProtections&quot; collection")]
        public System.String defaultManagedAppProtectionId { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceAppManagement/defaultManagedAppProtections/{defaultManagedAppProtectionId}/deploymentSummary";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Creates a &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; object.</para>
    ///     <para type="description">POST ~/deviceAppManagement/defaultManagedAppProtections/{defaultManagedAppProtectionId}/deploymentSummary</para>
    ///     <para type="description">Creates the &quot;deploymentSummary&quot; object (which is of type &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot;).</para>
    ///     <para type="description">Navigation property to deployment summary of the configuration.</para>
    /// </summary>
    [Cmdlet("New", "DeviceAppManagement_DefaultManagedAppProtections_DeploymentSummary", ConfirmImpact = ConfirmImpact.Low, DefaultParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary")]
    [ODataType("microsoft.graph.managedAppPolicyDeploymentSummary")]
    public class New_DeviceAppManagement_DefaultManagedAppProtections_DeploymentSummary : PostCmdlet
    {
        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.defaultManagedAppProtection&quot; object in the &quot;defaultManagedAppProtections&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.defaultManagedAppProtection&quot; object in the &quot;defaultManagedAppProtections&quot; collection")]
        public System.String defaultManagedAppProtectionId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;displayName&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;displayName&quot; property, of type &quot;Edm.String&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;displayName&quot; property, of type &quot;Edm.String&quot;.")]
        public System.String displayName { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;configurationDeployedUserCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;configurationDeployedUserCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;configurationDeployedUserCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 configurationDeployedUserCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;lastRefreshTime&quot; property, of type &quot;Edm.DateTimeOffset&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("Edm.DateTimeOffset")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;lastRefreshTime&quot; property, of type &quot;Edm.DateTimeOffset&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;lastRefreshTime&quot; property, of type &quot;Edm.DateTimeOffset&quot;.")]
        public System.DateTimeOffset lastRefreshTime { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;configurationDeploymentSummaryPerApp&quot; property, of type &quot;microsoft.graph.managedAppPolicyDeploymentSummaryPerApp&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("microsoft.graph.managedAppPolicyDeploymentSummaryPerApp")]
        [Selectable]
        [AllowEmptyCollection]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;configurationDeploymentSummaryPerApp&quot; property, of type &quot;microsoft.graph.managedAppPolicyDeploymentSummaryPerApp&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;configurationDeploymentSummaryPerApp&quot; property, of type &quot;microsoft.graph.managedAppPolicyDeploymentSummaryPerApp&quot;.")]
        public System.Object[] configurationDeploymentSummaryPerApp { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;version&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        ///     <para type="description">Version of the entity.</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;version&quot; property, of type &quot;Edm.String&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;version&quot; property, of type &quot;Edm.String&quot;.")]
        public System.String version { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceAppManagement/defaultManagedAppProtections/{defaultManagedAppProtectionId}/deploymentSummary";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Updates a &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot;.</para>
    ///     <para type="description">PATCH ~/deviceAppManagement/defaultManagedAppProtections/{defaultManagedAppProtectionId}/deploymentSummary</para>
    ///     <para type="description">Updates the &quot;deploymentSummary&quot; object (which is of type &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot;).</para>
    ///     <para type="description">Navigation property to deployment summary of the configuration.</para>
    /// </summary>
    [Cmdlet("Update", "DeviceAppManagement_DefaultManagedAppProtections_DeploymentSummary", ConfirmImpact = ConfirmImpact.Medium, DefaultParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary")]
    [ODataType("microsoft.graph.managedAppPolicyDeploymentSummary")]
    public class Update_DeviceAppManagement_DefaultManagedAppProtections_DeploymentSummary : PatchCmdlet
    {
        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.defaultManagedAppProtection&quot; object in the &quot;defaultManagedAppProtections&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.defaultManagedAppProtection&quot; object in the &quot;defaultManagedAppProtections&quot; collection")]
        public System.String defaultManagedAppProtectionId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;displayName&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;displayName&quot; property, of type &quot;Edm.String&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;displayName&quot; property, of type &quot;Edm.String&quot;.")]
        public System.String displayName { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;configurationDeployedUserCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;configurationDeployedUserCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;configurationDeployedUserCount&quot; property, of type &quot;Edm.Int32&quot;.")]
        public System.Int32 configurationDeployedUserCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;lastRefreshTime&quot; property, of type &quot;Edm.DateTimeOffset&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("Edm.DateTimeOffset")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;lastRefreshTime&quot; property, of type &quot;Edm.DateTimeOffset&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;lastRefreshTime&quot; property, of type &quot;Edm.DateTimeOffset&quot;.")]
        public System.DateTimeOffset lastRefreshTime { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;configurationDeploymentSummaryPerApp&quot; property, of type &quot;microsoft.graph.managedAppPolicyDeploymentSummaryPerApp&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        /// </summary>
        [ODataType("microsoft.graph.managedAppPolicyDeploymentSummaryPerApp")]
        [Selectable]
        [AllowEmptyCollection]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;configurationDeploymentSummaryPerApp&quot; property, of type &quot;microsoft.graph.managedAppPolicyDeploymentSummaryPerApp&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;configurationDeploymentSummaryPerApp&quot; property, of type &quot;microsoft.graph.managedAppPolicyDeploymentSummaryPerApp&quot;.")]
        public System.Object[] configurationDeploymentSummaryPerApp { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;version&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedAppPolicyDeploymentSummary&quot; type.</para>
        ///     <para type="description">Version of the entity.</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.managedAppPolicyDeploymentSummary", HelpMessage = @"The &quot;version&quot; property, of type &quot;Edm.String&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;version&quot; property, of type &quot;Edm.String&quot;.")]
        public System.String version { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceAppManagement/defaultManagedAppProtections/{defaultManagedAppProtectionId}/deploymentSummary";
        }
    }
}