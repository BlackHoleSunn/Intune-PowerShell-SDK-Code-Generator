// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System.Management.Automation;

    /// <summary>
    ///     <para type="synopsis">Retrieves &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; objects.</para>
    ///     <para type="description">GET ~/deviceManagement/deviceEnrollmentConfigurations/{deviceEnrollmentConfigurationId}/assignments</para>
    ///     <para type="description">Retrieves &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; objects in the &quot;assignments&quot; collection.</para>
    ///     <para type="description">The list of group assignments for the device configuration profile.</para>
    /// </summary>
    [Cmdlet("Get", "DeviceManagement_DeviceEnrollmentConfigurations_Assignments", DefaultParameterSetName = @"Search")]
    [ODataType("microsoft.graph.enrollmentConfigurationAssignment")]
    public class Get_DeviceManagement_DeviceEnrollmentConfigurations_Assignments : GetOrSearchCmdlet
    {
        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.deviceEnrollmentConfiguration&quot; object in the &quot;deviceEnrollmentConfigurations&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.deviceEnrollmentConfiguration&quot; object in the &quot;deviceEnrollmentConfigurations&quot; collection")]
        public System.String deviceEnrollmentConfigurationId { get; set; }

        /// <summary>
        ///     <para type="description">The ID for a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object in the &quot;assignments&quot; collection</para>
        /// </summary>
        [Selectable]
        [Alias("id")]
        [ValidateNotNullOrEmpty]
        [Parameter(ParameterSetName = @"Get", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID for a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object in the &quot;assignments&quot; collection")]
        public System.String assignmentId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;target&quot; property, of type &quot;microsoft.graph.deviceAndAppManagementAssignmentTarget&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; type.</para>
        /// </summary>
        [ODataType("microsoft.graph.deviceAndAppManagementAssignmentTarget")]
        [Selectable]
        [Sortable]
        public System.Object target { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/deviceEnrollmentConfigurations/{deviceEnrollmentConfigurationId}/assignments/{assignmentId ?? string.Empty}";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Removes a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object.</para>
    ///     <para type="description">DELETE ~/deviceManagement/deviceEnrollmentConfigurations/{deviceEnrollmentConfigurationId}/assignments</para>
    ///     <para type="description">Removes a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object from the &quot;assignments&quot; collection.</para>
    ///     <para type="description">The list of group assignments for the device configuration profile.</para>
    /// </summary>
    [Cmdlet("Remove", "DeviceManagement_DeviceEnrollmentConfigurations_Assignments", ConfirmImpact = ConfirmImpact.High)]
    [ODataType("microsoft.graph.enrollmentConfigurationAssignment")]
    public class Remove_DeviceManagement_DeviceEnrollmentConfigurations_Assignments : DeleteCmdlet
    {
        /// <summary>
        ///     <para type="description">The ID for a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object in the &quot;assignments&quot; collection</para>
        /// </summary>
        [Selectable]
        [Alias("id")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID for a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object in the &quot;assignments&quot; collection")]
        public System.String assignmentId { get; set; }

        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.deviceEnrollmentConfiguration&quot; object in the &quot;deviceEnrollmentConfigurations&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.deviceEnrollmentConfiguration&quot; object in the &quot;deviceEnrollmentConfigurations&quot; collection")]
        public System.String deviceEnrollmentConfigurationId { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/deviceEnrollmentConfigurations/{deviceEnrollmentConfigurationId}/assignments/{assignmentId}";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Creates a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object.</para>
    ///     <para type="description">POST ~/deviceManagement/deviceEnrollmentConfigurations/{deviceEnrollmentConfigurationId}/assignments</para>
    ///     <para type="description">Adds a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object to the &quot;assignments&quot; collection.</para>
    ///     <para type="description">The list of group assignments for the device configuration profile.</para>
    /// </summary>
    [Cmdlet("New", "DeviceManagement_DeviceEnrollmentConfigurations_Assignments", ConfirmImpact = ConfirmImpact.Low, DefaultParameterSetName = @"#microsoft.graph.enrollmentConfigurationAssignment")]
    [ODataType("microsoft.graph.enrollmentConfigurationAssignment")]
    public class New_DeviceManagement_DeviceEnrollmentConfigurations_Assignments : PostCmdlet
    {
        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.deviceEnrollmentConfiguration&quot; object in the &quot;deviceEnrollmentConfigurations&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.deviceEnrollmentConfiguration&quot; object in the &quot;deviceEnrollmentConfigurations&quot; collection")]
        public System.String deviceEnrollmentConfigurationId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;target&quot; property, of type &quot;microsoft.graph.deviceAndAppManagementAssignmentTarget&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; type.</para>
        /// </summary>
        [ODataType("microsoft.graph.deviceAndAppManagementAssignmentTarget")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.enrollmentConfigurationAssignment", HelpMessage = @"The &quot;target&quot; property, of type &quot;microsoft.graph.deviceAndAppManagementAssignmentTarget&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;target&quot; property, of type &quot;microsoft.graph.deviceAndAppManagementAssignmentTarget&quot;.")]
        public System.Object target { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/deviceEnrollmentConfigurations/{deviceEnrollmentConfigurationId}/assignments";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Updates a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot;.</para>
    ///     <para type="description">PATCH ~/deviceManagement/deviceEnrollmentConfigurations/{deviceEnrollmentConfigurationId}/assignments</para>
    ///     <para type="description">Updates a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object in the &quot;assignments&quot; collection.</para>
    ///     <para type="description">The list of group assignments for the device configuration profile.</para>
    /// </summary>
    [Cmdlet("Update", "DeviceManagement_DeviceEnrollmentConfigurations_Assignments", ConfirmImpact = ConfirmImpact.Medium, DefaultParameterSetName = @"#microsoft.graph.enrollmentConfigurationAssignment")]
    [ODataType("microsoft.graph.enrollmentConfigurationAssignment")]
    public class Update_DeviceManagement_DeviceEnrollmentConfigurations_Assignments : PatchCmdlet
    {
        /// <summary>
        ///     <para type="description">The ID for a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object in the &quot;assignments&quot; collection</para>
        /// </summary>
        [Selectable]
        [Alias("id")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID for a &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; object in the &quot;assignments&quot; collection")]
        public System.String assignmentId { get; set; }

        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.deviceEnrollmentConfiguration&quot; object in the &quot;deviceEnrollmentConfigurations&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.deviceEnrollmentConfiguration&quot; object in the &quot;deviceEnrollmentConfigurations&quot; collection")]
        public System.String deviceEnrollmentConfigurationId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;target&quot; property, of type &quot;microsoft.graph.deviceAndAppManagementAssignmentTarget&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.enrollmentConfigurationAssignment&quot; type.</para>
        /// </summary>
        [ODataType("microsoft.graph.deviceAndAppManagementAssignmentTarget")]
        [Selectable]
        [Parameter(ParameterSetName = @"#microsoft.graph.enrollmentConfigurationAssignment", HelpMessage = @"The &quot;target&quot; property, of type &quot;microsoft.graph.deviceAndAppManagementAssignmentTarget&quot;.")]
        [Parameter(ParameterSetName = @"ManualTypeSelection", HelpMessage = @"The &quot;target&quot; property, of type &quot;microsoft.graph.deviceAndAppManagementAssignmentTarget&quot;.")]
        public System.Object target { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/deviceEnrollmentConfigurations/{deviceEnrollmentConfigurationId}/assignments/{assignmentId}";
        }
    }
}