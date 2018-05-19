﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.GraphODataPowerShellSDKWriter.Utils
{
    using System;
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Models;
    using PowerShellGraphSDK.PowerShellCmdlets;

    public static class CSharpMethodHelper
    {
        public static CSharpMethod CreateGetResourcePathMethod(string url, bool isFunction = false)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            // Create the method definition
            string methodName = nameof(ODataCmdlet.GetResourcePath);
            Type returnType = typeof(string);
            string methodBody = isFunction
                ? $"return $\"{url}({{this.{nameof(GetCmdlet.GetFunctionUrlSegment)}()}})\";"
                : $"return $\"{url}\";";

            // Create the method object
            CSharpMethod result = new CSharpMethod(methodName, returnType, methodBody)
            {
                Override = true,
                AccessModifier = CSharpAccessModifier.Internal,
            };

            return result;
        }

        public static CSharpMethod CreateGetHttpMethodMethod(string httpMethod)
        {
            if (httpMethod == null)
            {
                throw new ArgumentNullException(nameof(httpMethod));
            }

            // Create the method definition
            string methodName = nameof(ODataCmdlet.GetHttpMethod);
            Type returnType = typeof(string);
            string methodBody = httpMethod;

            // Create the method object
            CSharpMethod result = new CSharpMethod(methodName, returnType, methodBody)
            {
                Override = true,
                AccessModifier = CSharpAccessModifier.Internal,
            };

            return result;
        }

        public static CSharpMethod CreateGetContentMethodForCreatingReference()
        {
            // Create the method definition
            string methodName = nameof(ODataCmdlet.GetContent);
            Type returnType = typeof(object);
            string methodBody = $"return this.{nameof(ODataCmdlet.GetODataIdContent)}({ODataConstants.RequestProperties.Id});";

            // Create the method object
            CSharpMethod result = new CSharpMethod(methodName, returnType, methodBody)
            {
                Override = true,
                AccessModifier = CSharpAccessModifier.Internal,
            };

            return result;
        }
    }
}
