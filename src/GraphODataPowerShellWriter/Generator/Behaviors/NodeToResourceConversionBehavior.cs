﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Behaviors
{
    using System;
    using System.Collections.Generic;
    using Inflector;
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Models;
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Utils;
    using Vipr.Core.CodeModel;

    /// <summary>
    /// The behavior to convert an ODCM tree to a collection of resources.
    /// </summary>
    public static class NodeToResourceConversionBehavior
    {
        /// <summary>
        /// Converts an OData route to a resource.
        /// </summary>
        /// <param name="node">The ODCM node which represents the OData route</param>
        /// <returns>The resource that was generated from the OData route.</returns>
        public static Resource ConvertToResource(this OdcmNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            // Get ODCM property
            OdcmProperty property = node.OdcmProperty;

            // Calculate route
            ODataRoute oDataRoute = new ODataRoute(node);

            // Create file system path
            string fileSystemPath = oDataRoute.ToRelativeFilePathString();

            // Create a resource
            Resource resource = new Resource(fileSystemPath);

            // Convert each ODCM property into a set of cmdlets
            IEnumerable<Cmdlet> cmdlets = property.GetCmdlets(oDataRoute);

            // Add the cmdlets to the resource
            resource.AddAll(cmdlets);

            return resource;
        }

        /// <summary>
        /// Creates a set of cmdlets for an ODCM property.
        /// </summary>
        /// <param name="property">The ODCM property</param>
        /// <param name="oDataRoute">The route to the resource (with ID placeholders)</param>
        /// <returns>The cmdlets.</returns>
        private static IEnumerable<Cmdlet> GetCmdlets(this OdcmProperty property, ODataRoute oDataRoute)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            if (oDataRoute == null)
            {
                throw new ArgumentNullException(nameof(oDataRoute));
            }

            // Get/Search
            yield return property.CreateGetCmdlet(oDataRoute);

            // Post
            if (property.Projection.SupportsInsert())
            {
                // TODO: Support POST
                //yield return property.CreatePostCmdlet(oDataRoute);
            }

            // Patch
            if (property.Projection.SupportsUpdate())
            {
                // TODO: Support PATCH
                //yield return property.CreatePatchCmdlet(oDataRoute);
            }

            // Patch navigation property
            if (property.Projection.SupportsUpdateLink())
            {
                // TODO: Support PATCH on navigation properties
                //yield return property.CreateNavigationPatchCmdlet(oDataRoute);
            }

            // Delete
            if (property.Projection.SupportsDelete() && property.IsEnumeration())
            {
                yield return property.CreateDeleteCmdlet(oDataRoute);
            }

            // Delete navigation property
            if (property.Projection.SupportsDeleteLink())
            {
                // TODO: Support DELETE on navigation properties
                //yield return property.CreateNavigationDeleteCmdlet(oDataRoute);
            }
        }

        private static Cmdlet CreateGetCmdlet(this OdcmProperty property, ODataRoute oDataRoute)
        {
            Cmdlet result = new Cmdlet(new CmdletName("Get", property.GetCmdletName()), CmdletImpactLevel.Low)
            {
                HttpMethod = "GET",
            };

            // Check whether this route represents a single resource or an enumeration
            string oDataRouteString = oDataRoute.ToODataRouteString();
            if (property.IsEnumeration())
            {
                // Create parameter set
                CmdletParameterSet parameterSet = new CmdletParameterSet("Get");

                // Add ID parameter
                string idParameterName = "id";
                parameterSet.Add(new CmdletParameter(idParameterName, typeof(string)));

                // Add parameter set to cmdlet
                result.ParameterSets.Add(parameterSet);

                // Set the call URL to use this parameter
                result.CallUrl = $"{oDataRouteString}/{{{idParameterName} ?? string.Empty}}";

                // Since the property is an enumeration, allow "search" as well as "get"
                result.BaseType = CmdletBaseTypes.GetOrSearch;
            }
            else
            {
                result.CallUrl = oDataRouteString;
                result.BaseType = CmdletBaseTypes.Get;
            }

            return result;
        }

        private static Cmdlet CreateDeleteCmdlet(this OdcmProperty property, ODataRoute oDataRoute)
        {
            Cmdlet result = new Cmdlet(new CmdletName("Remove", property.GetCmdletName()), CmdletImpactLevel.High)
            {
                HttpMethod = "DELETE",
                BaseType = CmdletBaseTypes.Delete,
            };

            // Check whether this route represents a single resource or an enumeration
            string oDataRouteString = oDataRoute.ToODataRouteString();
            if (property.IsEnumeration())
            {
                // Add ID parameter
                string idParameterName = "id";
                result.ParameterSets.DefaultParameterSet.Add(new CmdletParameter(idParameterName, typeof(string)));
                result.CallUrl = $"{oDataRouteString}/{{{idParameterName}}}";
            }
            else
            {
                result.CallUrl = oDataRouteString;
            }

            return result;
        }

        private static string GetCmdletName(this OdcmProperty property)
        {
            string result = property.Name.Singularize() ?? property.Name;
            result = result.Pascalize();

            return result;
        }
    }
}
