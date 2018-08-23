﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Intune.PowerShellGraphSDK
{
    using System;

    /// <summary>
    /// Indicates that the given property is used to specify a type cast in the URL.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal class TypeCastParameterAttribute : Attribute
    {
    }
}
