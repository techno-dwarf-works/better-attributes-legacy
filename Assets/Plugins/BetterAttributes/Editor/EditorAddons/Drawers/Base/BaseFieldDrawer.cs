using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Base
{
    public abstract class BaseMultiFieldDrawer<T> : BaseFieldDrawer where T : UtilityWrapper
    {
        private protected WrapperCollection<T> _wrappers;

        private protected abstract WrapperCollection<T> GenerateCollection();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _wrappers ??= GenerateCollection();
            base.OnGUI(position, property, label);
        }
    }
    
    public abstract class BaseFieldDrawer : PropertyDrawer
    {
        private protected BaseFieldDrawer()
        {
            Selection.selectionChanged += OnSelectionChanged;
        }

        ~BaseFieldDrawer()
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

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!PreDraw(ref position, property, label)) return;

            DrawField(position, property, label);

            PostDraw(position, property, label);
        }

        private protected void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(PreparePropertyRect(position), property, label, true);
        }

        private protected abstract bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label);
        private protected abstract Rect PreparePropertyRect(Rect position);
        private protected abstract void PostDraw(Rect position, SerializedProperty property, GUIContent label);
    }
}