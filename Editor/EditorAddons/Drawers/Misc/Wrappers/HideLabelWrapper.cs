using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Helpers;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Misc.Wrappers
{
    public class HideLabelWrapper : MiscWrapper
    {
        public override void PreDraw(Rect position, GUIContent label)
        {
            label.text = string.Empty;
        }

        public override void PostDraw()
        {
        }

        public override void DrawField(Rect rect, GUIContent label)
        {
            var item = new GUIContent(label);
            item.text = string.Empty;
            if (!_property.hasVisibleChildren)
            {
                EditorGUI.PropertyField(rect, _property, item, true);
                return;
            }

            var enumerator = _property.GetEnumerator();
            var copy = new Rect(rect);
            while (enumerator.MoveNext())
            {
                if (!(enumerator.Current is SerializedProperty prop)) continue;

                var propertyHeight = EditorGUI.GetPropertyHeight(prop, true);
                copy.height = propertyHeight;

                EditorGUIHelpers.PropertyFieldSafe(copy, prop, item);
                copy.y += propertyHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }

        public override HeightCache GetHeight(GUIContent label)
        {
            var height = 0f;
            if (!_property.hasVisibleChildren)
            {
                height = EditorGUI.GetPropertyHeight(_property, label, true);
                return HeightCache.GetFull(height);
            }

            var enumerator = _property.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var prop = enumerator.Current as SerializedProperty;
                if (prop == null) continue;
                height += EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            return HeightCache.GetFull(height);
        }
    }
}