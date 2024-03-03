using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.DrawInspector;
using Better.Attributes.Runtime.DrawInspector;
using Better.EditorTools.EditorAddons.Comparers;
using Better.EditorTools.EditorAddons.Utilities;
using Better.EditorTools.EditorAddons.WrappersTypeCollection;
using Object = UnityEngine.Object;

namespace Better.Attributes.EditorAddons.Drawers.Utilities
{
    public class DrawInspectorUtility : BaseUtility<DrawInspectorUtility>
    {
        protected override BaseWrappersTypeCollection GenerateCollection()
        {
            return new WrappersTypeCollection()
            {
                {
                    typeof(DrawInspectorAttribute),
                    new Dictionary<Type, Type>(AssignableFromComparer.Instance) { { typeof(Object), typeof(DrawInspectorWrapper) } }
                }
            };
        }

        protected override HashSet<Type> GenerateAvailable()
        {
            return new HashSet<Type>(AssignableFromComparer.Instance) { typeof(Object) };
        }
    }
}