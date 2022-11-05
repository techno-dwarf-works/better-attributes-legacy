using System;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Base
{
    public abstract class FieldDrawer : PropertyDrawer
    {
        private protected FieldDrawer()
        {
            Selection.selectionChanged += OnSelectionChanged;
        }

        ~FieldDrawer()
        {
            Selection.selectionChanged -= OnSelectionChanged;
            Deconstruct();
        }

        private void OnSelectionChanged()
        {
            Selection.selectionChanged -= OnSelectionChanged;
            Deconstruct();
        }

        private protected abstract void Deconstruct();

        /// <summary>
        /// Internal method called by Unity
        /// Execution order:
        /// <example>
        /// if(!<see cref="PreDraw"/>) -> <see cref="DrawField"/> -> <see cref="PostDraw"/>
        /// </example>
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!PreDraw(ref position, property, label)) return;

            DrawField(position, property, label);

            PostDraw(position, property, label);
        }

        private protected virtual void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            var preparePropertyRect = PreparePropertyRect(position);
            EditorGUI.PropertyField(preparePropertyRect, property, label, true);
        }

        private protected abstract bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label);
        private protected abstract Rect PreparePropertyRect(Rect original);
        private protected abstract void PostDraw(Rect position, SerializedProperty property, GUIContent label);
    }
}