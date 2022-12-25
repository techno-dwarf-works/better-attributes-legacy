using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Base;
using Better.Attributes.EditorAddons.Drawers.Comparers;
using Better.Attributes.EditorAddons.Drawers.DrawInspector;
using Better.Attributes.Runtime.DrawInspector;
using Object = UnityEngine.Object;

namespace Better.Attributes.EditorAddons.Drawers.Utilities
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