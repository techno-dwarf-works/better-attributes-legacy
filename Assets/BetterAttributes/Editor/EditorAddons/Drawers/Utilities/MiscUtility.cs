using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Misc.Wrappers;
using Better.Attributes.Runtime.Misc;
using Better.EditorTools.Comparers;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Utilities;

namespace Better.Attributes.EditorAddons.Drawers.Utilities
{
    public class MiscUtility : BaseUtility<MiscUtility>
    {
        protected override BaseWrappersTypeCollection GenerateCollection()
        {
            return new WrappersTypeCollection(TypeComparer.Instance)
            {
                
                {
                    typeof(HideLabelAttribute), new Dictionary<Type, Type>(AnyTypeComparer.Instance)
                    {
                        { typeof(Type), typeof(HideLabelWrapper) }
                    }
                },

                {
                    typeof(EnumButtonsAttribute), new Dictionary<Type, Type>(AssignableFromComparer.Instance)
                    {
                        { typeof(Enum), typeof(EnumButtonsWrapper) }
                    }
                },

                {
                    typeof(CustomTooltipAttribute), new Dictionary<Type, Type>(AnyTypeComparer.Instance)
                    {
                        { typeof(Type), typeof(CustomToolTipWrapper) }
                    }
                },
            };
        }

        protected override HashSet<Type> GenerateAvailable()
        {
            return new HashSet<Type>(AnyTypeComparer.Instance)
            {
                typeof(Enum),
                typeof(Type)
            };
        }
    }
}