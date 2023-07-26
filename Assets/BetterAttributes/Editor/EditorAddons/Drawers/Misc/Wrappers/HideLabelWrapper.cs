using Better.EditorTools.Comparers;
using Better.EditorTools.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Misc.Wrappers
{
    public class HideLabelWrapper : MiscWrapper
    {
        public override void PreDraw(ref Rect position, GUIContent label)
        {
            if (!_property.hasVisibleChildren)
            {
                label.text = string.Empty;
            }
        }

        public override void PostDraw()
        {
        }

        public override void DrawField(Rect rect, GUIContent label)
        {
            if (!_property.hasVisibleChildren)
            {
                EditorGUI.PropertyField(rect, _property, label, true);
                return;
            }

            var enumerator = _property.GetEnumerator();
            var copy = new Rect(rect);
            while (enumerator.MoveNext())
            {
                var prop = enumerator.Current as SerializedProperty;
                if (prop == null) continue;
                //Add your treatment to the current child property...
                var propertyHeight = EditorGUI.GetPropertyHeight(prop, true);
                copy.height = propertyHeight;
                EditorGUI.PropertyField(copy, prop, true);
                copy.y += propertyHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }

        public override HeightCache GetHeight(GUIContent label)
        {
            var height = 0f;
            if (!_property.hasVisibleChildren)
            {
                height = EditorGUI.GetPropertyHeight(_property, label, true);
                label.text = string.Empty;
                return HeightCache.GetFull(height);
            }

            var enumerator = _property.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var prop = enumerator.Current as SerializedProperty;
                if (prop == null) continue;
                //Add your treatment to the current child property...
                height += EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            return HeightCache.GetFull(height);
        }
    }
}