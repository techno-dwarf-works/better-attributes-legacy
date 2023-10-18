using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Extensions
{
    public static class GUIContentExtension
    {
        public static float GetMaxWidth(this GUIContent label)
        {
            if (string.IsNullOrEmpty(label.text))
            {
                return 0;
            }

            return EditorGUIUtility.labelWidth;
        }
    }
}