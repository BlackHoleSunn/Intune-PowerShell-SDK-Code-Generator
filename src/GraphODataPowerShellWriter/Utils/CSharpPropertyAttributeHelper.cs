﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.GraphODataPowerShellSDKWriter.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Graph.GraphODataPowerShellSDKWriter.Generator.Models;
    using PowerShellGraphSDK;
    using PS = System.Management.Automation;

    public static class CSharpPropertyAttributeHelper
    {
        // ValidateNotNull
        private static readonly CSharpAttribute _validateNotNullAttribute = new CSharpAttribute(nameof(PS.ValidateNotNullAttribute));
        public static CSharpAttribute CreateValidateNotNullAttribute() => _validateNotNullAttribute;

        // ValidateNotNullOrEmpty
        private static readonly CSharpAttribute _validateNotNullOrEmptyAttribute = new CSharpAttribute(nameof(PS.ValidateNotNullOrEmptyAttribute));
        public static CSharpAttribute CreateValidateNotNullOrEmptyAttribute() => _validateNotNullOrEmptyAttribute;

        // AllowEmptyCollection
        private static readonly CSharpAttribute _allowEmptyCollectionAttribute = new CSharpAttribute(nameof(PS.AllowEmptyCollectionAttribute));
        public static CSharpAttribute CreateAllowEmptyCollectionAttribute() => _allowEmptyCollectionAttribute;

        // Expandable
        private static readonly CSharpAttribute _selectableAttribute = new CSharpAttribute(nameof(SelectableAttribute));
        public static CSharpAttribute CreateSelectableAttribute() => _selectableAttribute;

        // Expandable
        private static readonly CSharpAttribute _expandableAttribute = new CSharpAttribute(nameof(ExpandableAttribute));
        public static CSharpAttribute CreateExpandableAttribute() => _expandableAttribute;

        // Sortable
        private static readonly CSharpAttribute _sortableAttribute = new CSharpAttribute(nameof(SortableAttribute));
        public static CSharpAttribute CreateSortableAttribute() => _sortableAttribute;

        public static CSharpAttribute CreateODataTypeAttribute(string oDataTypeFullName)
        {
            if (oDataTypeFullName == null)
            {
                throw new ArgumentNullException(nameof(oDataTypeFullName));
            }

            CSharpAttribute result = new CSharpAttribute(nameof(ODataTypeAttribute))
            {
                Arguments = $"\"{oDataTypeFullName}\"".SingleObjectAsEnumerable(),
            };

            return result;
        }

        // Derived type attribute
        public static CSharpAttribute CreateDerivedTypeAttribute(string derivedTypeFullName)
        {
            if (derivedTypeFullName == null)
            {
                throw new ArgumentNullException(nameof(derivedTypeFullName));
            }

            CSharpAttribute result = new CSharpAttribute(nameof(DerivedTypeAttribute))
            {
                Arguments = $"\"{derivedTypeFullName}\"".SingleObjectAsEnumerable(),
            };

            return result;
        }

        // ValidateSet attribute
        public static CSharpAttribute CreateValidateSetAttribute(IEnumerable<string> validValues)
        {
            if (validValues == null)
            {
                throw new ArgumentNullException(nameof(validValues));
            }

            CSharpAttribute result = new CSharpAttribute(nameof(PS.ValidateSetAttribute))
            {
                Arguments = validValues.Select(value => $"@\"{value}\""),
            };

            return result;
        }

        // Parameter
        public static CSharpAttribute CreateParameterAttribute(
            string parameterSetName = null,
            bool mandatory = false,
            bool valueFromPipeline = false,
            bool valueFromPipelineByPropertyName = false,
            string helpMessage = null)
        {
            ICollection<string> arguments = new List<string>();
            if (parameterSetName != null)
            {
                arguments.Add($"{nameof(PS.ParameterAttribute.ParameterSetName)} = @\"{parameterSetName}\"");
            }
            if (mandatory)
            {
                arguments.Add($"{nameof(PS.ParameterAttribute.Mandatory)} = true");
            }
            if (valueFromPipeline)
            {
                arguments.Add($"{nameof(PS.ParameterAttribute.ValueFromPipeline)} = true");
            }
            if (valueFromPipelineByPropertyName)
            {
                arguments.Add($"{nameof(PS.ParameterAttribute.ValueFromPipelineByPropertyName)} = true");
            }
            if (helpMessage != null)
            {
                arguments.Add($"{nameof(PS.ParameterAttribute.HelpMessage)} = @\"{helpMessage}\"");
            }

            return new CSharpAttribute(nameof(PS.ParameterAttribute), arguments);
        }

        // ParameterSetSwitch
        public static CSharpAttribute CreateParameterSetSwitchAttribute(string parameterSetName)
        {
            if (parameterSetName == null)
            {
                throw new ArgumentNullException(nameof(parameterSetName));
            }

            return new CSharpAttribute(nameof(ParameterSetSelectorAttribute), new string[] { $"@\"{parameterSetName}\"" });
        }
    }
}
