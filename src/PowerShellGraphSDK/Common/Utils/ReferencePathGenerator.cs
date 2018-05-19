﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace PowerShellGraphSDK
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using PowerShellGraphSDK.PowerShellCmdlets;

    internal class ReferencePathGenerator
    {
        /// <summary>
        /// The cache of <see cref="ODataCmdlet"/> to <see cref="ReferencePathGenerator"/> objects.
        /// </summary>
        internal static IDictionary<Type, ReferencePathGenerator> Cache { get; } = new Dictionary<Type, ReferencePathGenerator>();

        /// <summary>
        /// An instance of the cmdlet type that we should use to get the resource URL.
        /// </summary>
        private ODataCmdlet _cmdletInstance;

        /// <summary>
        /// The "id" property on the cmdlet instance that we need to set before getting the resource URL.
        /// </summary>
        private PropertyInfo _idProperty;

        internal ReferencePathGenerator(ODataCmdlet cmdletInstance)
        {
            this._cmdletInstance = cmdletInstance ?? throw new ArgumentNullException(nameof(cmdletInstance));

            // Get the type of this cmdlet
            Type cmdletType = this._cmdletInstance.GetType();

            // Get the "id" property
            this._idProperty = cmdletType.GetProperty(ODataConstants.RequestProperties.Id);
        }

        /// <summary>
        /// Generates a resource path from the given object ID.
        /// </summary>
        /// <param name="id">The ID of the object being referenced</param>
        /// <returns>The generated URL.</returns>
        internal string GenerateResourcePath(string id)
        {
            // Set the ID
            this._idProperty?.SetValue(this._cmdletInstance, id);

            // Create the relative resource path
            string path = this._cmdletInstance.GetResourcePath();

            return path;
        }
    }
}
