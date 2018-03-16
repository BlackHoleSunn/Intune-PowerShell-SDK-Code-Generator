﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Behaviors
{
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Models;
    using System;
    using System.Collections.Generic;
    using Vipr.Core.CodeModel;

    /// <summary>
    /// The behavior to convert an ODCM tree to a collection of resources.
    /// </summary>
    public static class NodeToResourceConversionBehavior
    {
        /// <summary>
        /// Converts an ODCM node to a resource.
        /// </summary>
        /// <param name="rootNode">The ODCM node</param>
        /// <returns>The resource that was generated from the ODCM node.</returns>
        public static Resource ConvertToResource(this OdcmNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            throw new NotImplementedException();
        }

        private static Resource GetResource(this OdcmNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            // TODO: Create a resource

            // TODO: Call "GetCmdlets()" to convert each ODCM object into a set of cmdlets

            throw new NotImplementedException();
        }

        private static IEnumerable<Cmdlet> GetCmdlets(this OdcmObject obj, string baseUrl)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }

            // Get/Search
            Cmdlet getCmdlet = new Cmdlet(new CmdletName("Get", obj.Name))
            {
                HttpMethod = "GET",
                CallUrl = baseUrl + obj.Name + "/",
            };
            // TODO: Add parameters to GET and SEARCH
            yield return getCmdlet;

            // Post
            if (obj.Projection.SupportsInsert())
            {
                // TODO: Support POST
                //yield return postCmdlet;
            }

            // Patch
            if (obj.Projection.SupportsUpdate())
            {
                // TODO: Support PATCH
                //yield return patchCmdlet;
            }

            // Patch navigation property
            if (obj.Projection.SupportsUpdateLink())
            {
                // TODO: Support PATCH on navigation properties
                //yield return patchLinkCmdlet;
            }

            // Delete
            if (obj.Projection.SupportsDelete())
            {
                // TODO: Support DELETE
                //yield return deleteCmdlet;
            }

            // Delete navigation property
            if (obj.Projection.SupportsDeleteLink())
            {
                // TODO: Support DELETE on navigation properties
                //yield return deleteLinkCmdlet;
            }
        }
    }
}
