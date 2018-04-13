﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Behaviors
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Models;
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Utils;

    /// <summary>
    /// The behavior to convert a Resource into a TextFile.
    /// </summary>
    public static class ResourceToCSharpFileConversionBehavior
    {
        /// <summary>
        /// Converts a Resource into a CSharpFile.
        /// </summary>
        /// <param name="resource">The resource</param>
        /// <returns>The converted CSharpFile.</returns>
        public static CSharpFile ToCSharpFile(this Resource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            // Get the C# file details
            CSharpFile cSharpFile = new CSharpFile(resource.RelativeFilePath + ".cs")
            {
                Usings = CSharpFileHelper.GetDefaultUsings(),
                Classes = resource.CreateClasses(),
            };

            return cSharpFile;
        }

        private static IEnumerable<CSharpClass> CreateClasses(this Resource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            // Convert each cmdlet to a C# class
            foreach (Cmdlet cmdlet in resource.Cmdlets)
            {
                yield return cmdlet.ToCSharpClass();
            }
        }

        private static CSharpClass ToCSharpClass(this Cmdlet cmdlet)
        {
            if (cmdlet == null)
            {
                throw new ArgumentNullException(nameof(cmdlet));
            }

            // Create the result object
            CSharpClass result = new CSharpClass($"{cmdlet.Name.Verb}_{cmdlet.Name.Noun}")
            {
                AccessModifier = CSharpAccessModifier.Public,
                BaseType = cmdlet.BaseType.ToCSharpString(),
                Attributes = cmdlet.CreateAttributes(),
                Properties = cmdlet.CreateProperties(),
                Methods = cmdlet.CreateMethods(),
            };

            return result;
        }

        private static IEnumerable<CSharpAttribute> CreateAttributes(this Cmdlet cmdlet)
        {
            if (cmdlet == null)
            {
                throw new ArgumentNullException(nameof(cmdlet));
            }

            // "[Cmdlet]" attribute
            yield return CSharpClassAttributeHelper.CreateCSharpClassCmdletAttribute(cmdlet.Name, cmdlet.ImpactLevel);
        }

        private static IEnumerable<CSharpMethod> CreateMethods(this Cmdlet cmdlet)
        {
            if (cmdlet == null)
            {
                throw new ArgumentNullException(nameof(cmdlet));
            }

            // "GetResourcePath()" method override
            yield return CSharpMethodHelper.GetResourcePath(cmdlet.CallUrl);
        }

        private static IEnumerable<CSharpProperty> CreateProperties(this Cmdlet cmdlet)
        {
            if (cmdlet == null)
            {
                throw new ArgumentNullException(nameof(cmdlet));
            }

            // We need a mapping of (parameter -> parameter sets) instead of (parameter set -> parameters)
            IReadOnlyDictionary<CmdletParameter, IEnumerable<CmdletParameterSet>> parameters = cmdlet.ParameterSets.GetParameters();

            // Create a property per parameter
            foreach (var entry in parameters)
            {
                CmdletParameter parameter = entry.Key;
                IEnumerable<CmdletParameterSet> parameterSets = entry.Value;

                // Create the property
                yield return new CSharpProperty(parameter.Name, parameter.Type)
                {
                    AccessModifier = CSharpAccessModifier.Public,
                    Attributes = parameter.CreateAttributes(parameterSets),
                };
            }
        }

        private static IEnumerable<CSharpAttribute> CreateAttributes(this CmdletParameter parameter, IEnumerable<CmdletParameterSet> parameterSets)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }
            if (parameterSets == null)
            {
                throw new ArgumentNullException(nameof(parameterSets));
            }

            // ParameterSetSwitch attribute
            if (parameter.ParameterSetSelectorName != null)
            {
                yield return CSharpPropertyAttributeHelper.CreateCSharpPropertyParameterSetSwitchAttribute(parameter.ParameterSetSelectorName);
            }

            foreach (CmdletParameterSet parameterSet in parameterSets)
            {
                // Parameter attribute
                yield return CSharpPropertyAttributeHelper.CreateCSharpPropertyParameterAttribute(
                    parameterSet.Name,
                    parameter.Mandatory,
                    parameter.ValueFromPipeline,
                    parameter.ValueFromPipelineByPropertyName);
            }
        }
    }
}
