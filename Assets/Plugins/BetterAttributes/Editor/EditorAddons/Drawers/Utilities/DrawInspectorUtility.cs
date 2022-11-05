using System;
using System.Collections.Generic;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Comparers;
using BetterAttributes.EditorAddons.Drawers.DrawInspector;
using BetterAttributes.Runtime.Attributes.DrawInspector;
using Object = UnityEngine.Object;

namespace BetterAttributes.EditorAddons.Drawers.Utilities
{
    public class DrawInspectorUtility : BaseUtility<DrawInspectorUtility>
    {
        private protected override WrappersTypeCollection GenerateCollection()
        {
            return new WrappersTypeCollection()
            {
                {
                    typeof(DrawInspectorAttribute),
                    new Dictionary<Type, Type>(AssignableFromComparer.Instance) { { typeof(Object), typeof(DrawInspectorWrapper) } }
                }
            };
        }

        private protected override HashSet<Type> GenerateAvailable()
        {
            return new HashSet<Type>(AssignableFromComparer.Instance) { typeof(Object) };
        }
    }
}