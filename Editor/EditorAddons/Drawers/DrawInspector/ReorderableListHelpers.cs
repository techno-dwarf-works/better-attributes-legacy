using System;
using System.Collections.Generic;
using System.Linq;
using Better.EditorTools;
using Better.EditorTools.Comparers;
using Better.Extensions.Runtime;
using Better.Tools.Runtime;
using UnityEditor;
using UnityEditorInternal;

namespace Better.Attributes.EditorAddons.Drawers.DrawInspector
{
    public static class ReorderableListHelpers
    {
        private static System.Reflection.MethodInfo _repaintInspectors = null;
        private static List<WeakReference> _instances;

        //TODO: Need to find better way to refresh ReorderableList
        public static void RepaintAllInspectors(SerializedProperty property)
        {
            if (_repaintInspectors == null)
            {
                var inspWin = typeof(ReorderableList);
                _repaintInspectors = inspWin.GetMethod("InvalidateParentCaches", BetterEditorDefines.MethodFlags);
            }

            if (_repaintInspectors != null) _repaintInspectors.Invoke(null, new object[] { property.propertyPath });
        }

        public static void SetEditable(ReorderableList list, bool value)
        {
            if (list == null) return;
            typeof(ReorderableList).GetField("m_IsEditable", BetterEditorDefines.FieldsFlags)?.SetValue(list, value);
        }

        public static ReorderableList GetReorderableListFor(SerializedProperty property)
        {
            if (!property.Verify()) return null;
            var inspWin = typeof(ReorderableList);
            _instances = (List<WeakReference>)inspWin.GetField("s_Instances", BetterEditorDefines.MethodFlags)?.GetValue(null);

            if (_instances != null)
            {
                foreach (var reference in _instances.Where(reference => reference.IsAlive))
                {
                    if (!(reference.Target is ReorderableList list)) continue;
                    if (property.GetPropertyParentList().FastEquals(list.serializedProperty.propertyPath))
                        return list;
                }
            }

            return null;
        }
    }
}