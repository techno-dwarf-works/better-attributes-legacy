using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.DrawInspector
{
    public static class ReorderableListHelpers
    {
        private static System.Reflection.MethodInfo _repaintInspectors = null;

        //TODO: Need to find better way to refresh ReorderableList
        public static void RepaintAllInspectors(SerializedProperty property)
        {
            if (_repaintInspectors == null)
            {
                var inspWin = typeof(UnityEditorInternal.ReorderableList).Assembly.GetType("UnityEditorInternal.ReorderableList");
                _repaintInspectors = inspWin.GetMethod("InvalidateParentCaches",
                    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            }

            if (_repaintInspectors != null) _repaintInspectors.Invoke(null, new object[] { property.propertyPath });
        }
    }
}