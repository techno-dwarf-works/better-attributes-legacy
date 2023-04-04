using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Select.Wrappers;
using Better.Attributes.Runtime.Select;
using Better.EditorTools.Comparers;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Utilities;

namespace Better.Attributes.EditorAddons.Drawers.Utilities
{
    public class SelectUtility : BaseUtility<SelectUtility>
    {
        protected override WrappersTypeCollection GenerateCollection()
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

        protected override HashSet<Type> GenerateAvailable()
        {
            return new HashSet<Type>(TypeComparer.Instance)
            {
                typeof(Enum),
                typeof(Type)
            };
        }
    }
}