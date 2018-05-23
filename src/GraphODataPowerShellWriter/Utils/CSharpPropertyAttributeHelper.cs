﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.GraphODataPowerShellSDKWriter.Utils
{
    using System;
    using System.Collections;
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

        // ODataType
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

        // DerivedType
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

        // ValidateSet
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

            return new CSharpAttribute(nameof(PS.ParameterAttribute), arguments.ToArray());
        }

        // ParameterSetSwitch
        public static CSharpAttribute CreateParameterSetSwitchAttribute(string parameterSetName)
        {
            if (parameterSetName == null)
            {
                throw new ArgumentNullException(nameof(parameterSetName));
            }

            return new CSharpAttribute(nameof(ParameterSetSelectorAttribute), $"@\"{parameterSetName}\"");
        }

        // ValidateType
        private static readonly IEnumerable<Type> _defaultAllowedTypes = new Type[]
        {
            // Primitive OData types
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(byte),
            typeof(char),
            typeof(string),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),

            // Complex types
            typeof(PS.PSObject),
            typeof(Hashtable),
            typeof(IDictionary<string, object>),
        };
        private static readonly IEnumerable<Type> _defaultAllowedArrayTypes = _defaultAllowedTypes.Select(type => type.MakeArrayType());
        public static CSharpAttribute CreateValidateTypeAttribute(bool isArray)
        {
            if (isArray)
            {
                return CreateValidateTypeAttribute(_defaultAllowedArrayTypes);
            }
            else
            {
                return CreateValidateTypeAttribute(_defaultAllowedTypes);
            }
        }
        public static CSharpAttribute CreateValidateTypeAttribute(IEnumerable<Type> types)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }
            if (!types.Any())
            {
                throw new ArgumentException("List of types cannot be empty", nameof(types));
            }

            // Convert the list of types to type names wrapped in "typeof()"
            IEnumerable<string> typeNames = types.Select(type => $"typeof({type.ToCSharpString()})");

            // Put the arguments on separate lines to make it more readable if we have more than 1 type
            return new CSharpAttribute(nameof(ValidateTypeAttribute), typeNames.ToArray())
            {
                MultiLineArguments = typeNames.Count() > 1,
            };
        }
    }
}
