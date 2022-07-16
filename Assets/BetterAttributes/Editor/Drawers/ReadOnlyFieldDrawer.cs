using BetterAttributes.EditorAddons;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.Drawers
{
    [CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
    public class ReadOnlyFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var previousGUIState = GUI.enabled;
            // Disabling edit for property
            GUI.enabled = false;
            // Drawing Property
            EditorGUI.PropertyField(position, property, label, true);
            // Setting old GUI enabled value
            GUI.enabled = previousGUIState;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}