// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System.Management.Automation;

    /// <summary>
    ///     <para type="synopsis">Retrieves the &quot;managedDeviceOverview&quot; object.</para>
    ///     <para type="description">GET ~/deviceManagement/managedDeviceOverview</para>
    ///     <para type="description">Retrieves the &quot;managedDeviceOverview&quot; object (which is of type &quot;microsoft.graph.managedDeviceOverview&quot;).</para>
    ///     <para type="description">Device overview</para>
    /// </summary>
    [Cmdlet("Get", "DeviceManagement_ManagedDeviceOverviewReference", DefaultParameterSetName = @"Get")]
    [ODataType("microsoft.graph.managedDeviceOverview")]
    public class Get_DeviceManagement_ManagedDeviceOverviewReference : GetCmdlet
    {
        /// <summary>
        ///     <para type="description">The &quot;enrolledDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedDeviceOverview&quot; type.</para>
        ///     <para type="description">Total enrolled device count. Does not include PC devices managed via Intune PC Agent</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 enrolledDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;mdmEnrolledCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedDeviceOverview&quot; type.</para>
        ///     <para type="description">The number of devices enrolled in MDM</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 mdmEnrolledCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;dualEnrolledDeviceCount&quot; property, of type &quot;Edm.Int32&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedDeviceOverview&quot; type.</para>
        ///     <para type="description">The number of devices enrolled in both MDM and EAS</para>
        /// </summary>
        [ODataType("Edm.Int32")]
        [Selectable]
        [Sortable]
        public System.Int32 dualEnrolledDeviceCount { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;deviceOperatingSystemSummary&quot; property, of type &quot;microsoft.graph.deviceOperatingSystemSummary&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedDeviceOverview&quot; type.</para>
        ///     <para type="description">Device operating system summary.</para>
        /// </summary>
        [ODataType("microsoft.graph.deviceOperatingSystemSummary")]
        [Selectable]
        [Sortable]
        public System.Object deviceOperatingSystemSummary { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;deviceExchangeAccessStateSummary&quot; property, of type &quot;microsoft.graph.deviceExchangeAccessStateSummary&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.managedDeviceOverview&quot; type.</para>
        ///     <para type="description">Distribution of Exchange Access State in Intune</para>
        /// </summary>
        [ODataType("microsoft.graph.deviceExchangeAccessStateSummary")]
        [Selectable]
        [Sortable]
        public System.Object deviceExchangeAccessStateSummary { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/managedDeviceOverview";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Removes a reference from a &quot;deviceManagement&quot; to a &quot;microsoft.graph.managedDeviceOverview&quot; object.</para>
    ///     <para type="description">DELETE ~/deviceManagement/managedDeviceOverview</para>
    ///     <para type="description">Removes a reference from the &quot;deviceManagement&quot; object to a &quot;managedDeviceOverview&quot;.</para>
    ///     <para type="description">Device overview</para>
    /// </summary>
    [Cmdlet("Remove", "DeviceManagement_ManagedDeviceOverviewReference", ConfirmImpact = ConfirmImpact.High)]
    [ODataType("microsoft.graph.managedDeviceOverview")]
    public class Remove_DeviceManagement_ManagedDeviceOverviewReference : DeleteCmdlet
    {
        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/managedDeviceOverview/$ref";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Creates a reference from a &quot;deviceManagement&quot; to a &quot;microsoft.graph.managedDeviceOverview&quot; object.</para>
    ///     <para type="description">PUT ~/deviceManagement/managedDeviceOverview</para>
    ///     <para type="description">Creates a reference from the &quot;deviceManagement&quot; object to a &quot;managedDeviceOverview&quot;.</para>
    ///     <para type="description">Device overview</para>
    /// </summary>
    [Cmdlet("New", "DeviceManagement_ManagedDeviceOverviewReference", ConfirmImpact = ConfirmImpact.Low)]
    [ODataType("microsoft.graph.managedDeviceOverview")]
    public class New_DeviceManagement_ManagedDeviceOverviewReference : PutReferenceToEntityCmdlet
    {
        internal override System.String GetResourcePath()
        {
            return $"deviceManagement/managedDeviceOverview/$ref";
        }
    }
}