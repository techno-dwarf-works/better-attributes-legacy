using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Base;
using Better.Attributes.EditorAddons.Drawers.Comparers;
using Better.Attributes.EditorAddons.Drawers.Select.Wrappers;
using Better.Attributes.Runtime.Select;

namespace Better.Attributes.EditorAddons.Drawers.Utilities
{
    public class SelectUtility : BaseUtility<SelectUtility>
    {
        private protected override WrappersTypeCollection GenerateCollection()
        {
            return new WrappersTypeCollection(TypeComparer.Instance)
            {
                {
                    typeof(SelectImplementationAttribute), new Dictionary<Type, Type>(TypeComparer.Instance)
                    {
                        { typeof(Type), typeof(SelectTypeWrapper) }
                    }
                },
                {
                    typeof(SelectEnumAttribute), new Dictionary<Type, Type>(TypeComparer.Instance)
                    {
                        { typeof(Enum), typeof(SelectEnumWrapper) }
                    }
                }
            };
        }

        private protected override HashSet<Type> GenerateAvailable()
        {
            return new HashSet<Type>(TypeComparer.Instance)
            {
                typeof(Enum),
                typeof(Type)
            };
        }
    }
}