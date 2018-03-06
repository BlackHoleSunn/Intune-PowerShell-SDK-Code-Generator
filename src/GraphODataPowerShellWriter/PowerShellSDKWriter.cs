﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace GraphODataPowerShellTemplateWriter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Behaviors;
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Models;
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Utils;
    using Vipr.Core;
    using Vipr.Core.CodeModel;
    using Vipr.Core.CodeModel.Vocabularies.Capabilities;

    public class PowerShellSDKWriter : IOdcmWriter
    {
        public const string CmdletPrefix = "Graph";

        /// <summary>
        /// Implementation which is provided to Vipr for transforming an ODCM model into the PowerShell SDK.
        /// </summary>
        /// <param name="model">The ODCM model provided by Vipr</param>
        /// <returns>The TextFile objects representing the generated SDK.</returns>
        public IEnumerable<TextFile> GenerateProxy(OdcmModel model)
        {
            return GenerateTestOutput(model);
            //return GeneratePowerShellSDK(model);
        }

        /// <summary>
        /// Generates the PowerShell SDK from the given ODCM model.
        /// </summary>
        /// <param name="model">The ODCM model</param>
        /// <returns>The TextFile objects representing the generated SDK.</returns>
        public static IEnumerable<TextFile> GeneratePowerShellSDK(OdcmModel model)
        {
            // Convert ODCM model into intermediate types
            OdcmModelProcessingBehavior processingBehavior = new OdcmModelProcessingBehavior(model, CmdletPrefix);
            IEnumerable<Resource> resources = processingBehavior.ConvertToResources();

            // Generate the text files by inserting data from the intermediate types into templates
            IEnumerable<TextFile> outputFiles = resources.Select(resource => resource.ToTextFile());

            // Return the generated files
            return outputFiles;
        }

        private static IEnumerable<TextFile> GenerateTestOutput(OdcmModel model)
        {
            string output = string.Empty;
            int indentLevel = 0;

            output += "Entity Container: " + model.EntityContainer.FullName + " - " + model.EntityContainer.Namespace.Name;
            //output += "\n" + StringUtils.DefaultSingleIndentString + "Properties:\n" + string.Join("\n", model.EntityContainer.Properties.Select(prop => prop.Name + " (" + prop.Type.FullName + ")").OrderBy(val => val));
            //output += "\n" + StringUtils.DefaultSingleIndentString + "Methods:\n" + string.Join("\n", model.EntityContainer.Methods.Select(prop => prop.FullName));
            //output += "\n\n";

            indentLevel++;
            foreach (OdcmProperty property in model.EntityContainer.Properties)
            {
                output += "\n" + StringUtils.Indent(indentLevel, property.Name);
                indentLevel++;

                

                indentLevel--;
            }
            indentLevel--;

            output += "\n\n";

            // Namespaces
            foreach (OdcmNamespace @namespace in model.Namespaces)
            {
                output += "\n" + StringUtils.IndentPrefix(indentLevel) + "== " + @namespace.Name + " ==";

                // Types
                if (@namespace.Types.Any())
                {
                    output += "\n" + StringUtils.IndentPrefix(indentLevel) + "Types:";
                    indentLevel++;
                    foreach (OdcmType type in @namespace.Types)
                    {
                        output += "\n" + StringUtils.IndentPrefix(indentLevel) + " - " + type.FullName;
                    }
                    indentLevel--;
                }

                // Type definitions
                if (@namespace.TypeDefinitions.Any())
                {
                    output += "\n" + StringUtils.IndentPrefix(indentLevel) + "Type Definitions:";
                    indentLevel++;
                    foreach (OdcmTypeDefinition typeDefinition in @namespace.TypeDefinitions)
                    {
                        output += "\n" + StringUtils.IndentPrefix(indentLevel) + " - " + typeDefinition.FullName + " : " + typeDefinition.BaseType.FullName;
                    }
                    indentLevel--;
                }

                // Enums
                if (@namespace.Enums.Any())
                {
                    output += "\n" + StringUtils.IndentPrefix(indentLevel) + "Enums:";
                    indentLevel++;
                    foreach (OdcmEnum @enum in @namespace.Enums)
                    {
                        output += "\n" + StringUtils.IndentPrefix(indentLevel) + "= " + @enum.FullName + " =";
                        indentLevel++;
                        foreach (OdcmEnumMember enumMember in @enum.Members)
                        {
                            output += "\n" + StringUtils.IndentPrefix(indentLevel) + " - " + enumMember.Name;
                        }
                        indentLevel--;
                    }
                    indentLevel--;
                }

                // Classes
                if (@namespace.Classes.Any())
                {
                    output += "\n" + StringUtils.IndentPrefix(indentLevel) + "Classes:";
                    indentLevel++;
                    foreach (OdcmClass @class in @namespace.Classes)
                    {
                        output += "\n" + StringUtils.IndentPrefix(indentLevel) + "= " + @class.Name + " =";

                        // Structural Properties
                        IEnumerable<OdcmProperty> structuralProperties = @class.StructuralProperties();
                        if (@class.StructuralProperties().Any())
                        {
                            output += "\n" + StringUtils.IndentPrefix(indentLevel) + "Properties:";
                            indentLevel++;
                            foreach (OdcmProperty property in structuralProperties.OrderBy(val => val.Name))
                            {
                                string propertyName = property.Name;
                                string propertyType = property.Type.FullName;
                                output += "\n" + StringUtils.IndentPrefix(indentLevel) + $"{propertyName}\t{propertyType}";
                            }
                            indentLevel--;
                        }

                        // Navigation Properties
                        IEnumerable<OdcmProperty> navigationProperties = @class.NavigationProperties();
                        if (navigationProperties.Any())
                        {
                            output += "\n" + StringUtils.IndentPrefix(indentLevel) + "Navigation Properties:";
                            indentLevel++;
                            foreach (OdcmProperty property in navigationProperties)
                            {
                                string propertyName = property.Name;
                                string propertyType = property.Type.FullName;
                                output += "\n" + StringUtils.IndentPrefix(indentLevel) + $"{propertyName}\t{propertyType}";
                            }
                            indentLevel--;
                        }

                        // Methods
                        if (@class.Methods.Any())
                        {
                            output += "\n" + StringUtils.IndentPrefix(indentLevel) + "Methods:";
                            indentLevel++;
                            foreach (OdcmMethod method in @class.Methods)
                            {
                                string returnType = "void";
                                if (method.ReturnType != null)
                                {
                                    returnType = method.ReturnType.FullName;
                                }
                                string methodName = method.Name;
                                string parameters = string.Join(", ", method.Parameters.Select(param => param.Type.FullName));
                                string methodDescription = method.LongDescription;
                                output += "\n" + StringUtils.IndentPrefix(indentLevel) + $"{returnType} {methodName}({parameters})";
                                if (!string.IsNullOrWhiteSpace(methodDescription))
                                {
                                    output += "\n" + StringUtils.IndentPrefix(indentLevel + 1) + methodDescription;
                                }
                            }
                            indentLevel--;
                        }
                    }
                    indentLevel--;
                }

                output += "\n";
            }

            Console.WriteLine(output);
            yield return new TextFile("test.txt", output);
        }
    }
}
