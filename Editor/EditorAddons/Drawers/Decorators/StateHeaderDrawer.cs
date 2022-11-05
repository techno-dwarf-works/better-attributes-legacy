using BetterAttributes.Runtime.Attributes.Headers;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Decorators
{
    [CustomPropertyDrawer(typeof(StateHeaderAttribute))]
    internal sealed class StateHeaderDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            position.yMin += EditorGUIUtility.singleLineHeight * 0.5f;
            position = EditorGUI.IndentedRect(position);
            GUI.Label(position, (attribute as HeaderAttribute)?.header, EditorStyles.boldLabel);
        }

        public override float GetHeight()
        {
            return EditorGUIUtility.singleLineHeight * 1.5f;
        }
    }
}