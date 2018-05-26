// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK.PowerShellCmdlets
{
    using System.Management.Automation;

    /// <summary>
    ///     <para type="synopsis">Retrieves &quot;microsoft.graph.mobileAppCategory&quot; objects.</para>
    ///     <para type="description">GET ~/deviceAppManagement/mobileApps/{mobileAppId}/categories</para>
    ///     <para type="description">Retrieves &quot;microsoft.graph.mobileAppCategory&quot; objects in the &quot;categories&quot; collection.</para>
    ///     <para type="description">The list of categories for this app.</para>
    /// </summary>
    [Cmdlet("Get", "DeviceAppManagement_MobileApps_CategoryReferences", DefaultParameterSetName = @"Search")]
    [ODataType("microsoft.graph.mobileAppCategory")]
    public class Get_DeviceAppManagement_MobileApps_CategoryReferences : GetOrSearchCmdlet
    {
        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.mobileApp&quot; object in the &quot;mobileApps&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.mobileApp&quot; object in the &quot;mobileApps&quot; collection")]
        public System.String mobileAppId { get; set; }

        /// <summary>
        ///     <para type="description">The ID for a &quot;microsoft.graph.mobileAppCategory&quot; object in the &quot;categories&quot; collection</para>
        /// </summary>
        [Selectable]
        [Alias("id")]
        [ValidateNotNullOrEmpty]
        [Parameter(ParameterSetName = @"Get", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID for a &quot;microsoft.graph.mobileAppCategory&quot; object in the &quot;categories&quot; collection")]
        public System.String categoryId { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;displayName&quot; property, of type &quot;Edm.String&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.mobileAppCategory&quot; type.</para>
        ///     <para type="description">The name of the app category.</para>
        /// </summary>
        [ODataType("Edm.String")]
        [Selectable]
        [Sortable]
        public System.String displayName { get; set; }

        /// <summary>
        ///     <para type="description">The &quot;lastModifiedDateTime&quot; property, of type &quot;Edm.DateTimeOffset&quot;.</para>
        ///     <para type="description">This property is on the &quot;microsoft.graph.mobileAppCategory&quot; type.</para>
        ///     <para type="description">The date and time the mobileAppCategory was last modified.</para>
        /// </summary>
        [ODataType("Edm.DateTimeOffset")]
        [Selectable]
        [Sortable]
        public System.DateTimeOffset lastModifiedDateTime { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceAppManagement/mobileApps/{mobileAppId}/categories/{categoryId ?? string.Empty}";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Removes a reference from a &quot;mobileApp&quot; to a &quot;microsoft.graph.mobileAppCategory&quot; object.</para>
    ///     <para type="description">DELETE ~/deviceAppManagement/mobileApps/{mobileAppId}/categories</para>
    ///     <para type="description">Removes a reference from the specified &quot;mobileApp&quot; object to a &quot;category&quot;.</para>
    ///     <para type="description">The list of categories for this app.</para>
    /// </summary>
    [Cmdlet("Remove", "DeviceAppManagement_MobileApps_CategoryReferences", ConfirmImpact = ConfirmImpact.High)]
    [ODataType("microsoft.graph.mobileAppCategory")]
    public class Remove_DeviceAppManagement_MobileApps_CategoryReferences : DeleteCmdlet
    {
        /// <summary>
        ///     <para type="description">The ID for a &quot;microsoft.graph.mobileAppCategory&quot; object in the &quot;categories&quot; collection</para>
        /// </summary>
        [Selectable]
        [Alias("id")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID for a &quot;microsoft.graph.mobileAppCategory&quot; object in the &quot;categories&quot; collection")]
        public System.String categoryId { get; set; }

        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.mobileApp&quot; object in the &quot;mobileApps&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.mobileApp&quot; object in the &quot;mobileApps&quot; collection")]
        public System.String mobileAppId { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceAppManagement/mobileApps/{mobileAppId}/categories/{categoryId}/$ref";
        }
    }

    /// <summary>
    ///     <para type="synopsis">Creates a reference from a &quot;mobileApp&quot; to a &quot;microsoft.graph.mobileAppCategory&quot; object.</para>
    ///     <para type="description">POST ~/deviceAppManagement/mobileApps/{mobileAppId}/categories</para>
    ///     <para type="description">Creates a reference from the specified &quot;mobileApp&quot; object to a &quot;category&quot;.</para>
    ///     <para type="description">The list of categories for this app.</para>
    /// </summary>
    [Cmdlet("New", "DeviceAppManagement_MobileApps_CategoryReferences", ConfirmImpact = ConfirmImpact.Low)]
    [ODataType("microsoft.graph.mobileAppCategory")]
    public class New_DeviceAppManagement_MobileApps_CategoryReferences : PostReferenceToCollectionCmdlet
    {
        /// <summary>
        ///     <para type="description">The ID for a &quot;microsoft.graph.mobileAppCategory&quot; object in the &quot;categories&quot; collection</para>
        /// </summary>
        [Selectable]
        [Alias("id")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"The ID for a &quot;microsoft.graph.mobileAppCategory&quot; object in the &quot;categories&quot; collection")]
        public System.String categoryId { get; set; }

        /// <summary>
        ///     <para type="description">A required ID for referencing a &quot;microsoft.graph.mobileApp&quot; object in the &quot;mobileApps&quot; collection</para>
        /// </summary>
        [Selectable]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = @"A required ID for referencing a &quot;microsoft.graph.mobileApp&quot; object in the &quot;mobileApps&quot; collection")]
        public System.String mobileAppId { get; set; }

        internal override System.String GetResourcePath()
        {
            return $"deviceAppManagement/mobileApps/{mobileAppId}/categories/$ref";
        }

        internal override System.Object GetContent()
        {
            return this.GetODataIdContent(categoryId);
        }
    }
}