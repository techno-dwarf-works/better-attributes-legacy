using System;
using System.Collections.Generic;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Comparers;
using BetterAttributes.EditorAddons.Drawers.Select.Wrappers;
using BetterAttributes.Runtime.Attributes.Select;

namespace BetterAttributes.EditorAddons.Drawers.Utilities
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