using BetterAttributes.EditorAddons.GizmoAttributes;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.Drawers.GizmoDrawers
{
    [CustomPropertyDrawer(typeof(GizmoAttribute))]
    [CustomPropertyDrawer(typeof(GizmoLocalAttribute))]
    public class GizmoDrawer : PropertyDrawer
    {
        private GizmoWrapper _gizmoWrapper;

        public GizmoDrawer()
        {
            Debug.Log("Constructed");
            Selection.selectionChanged += SelectionChanged;
            SceneView.duringSceneGui += OnSceneGUIDelegate;
        }

        private void OnSceneGUIDelegate(SceneView sceneView)
        {
            if (sceneView.drawGizmos)
            {
                _gizmoWrapper?.Apply(sceneView);
            }
        }

        private void SelectionChanged()
        {
            Selection.selectionChanged -= SelectionChanged;
            SceneView.duringSceneGui -= OnSceneGUIDelegate;
            Debug.Log("Deconstructed");
        }

        ~GizmoDrawer()
        {
            Selection.selectionChanged -= SelectionChanged;
            Debug.Log("Deconstructed");
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = property.propertyType;
            if (_gizmoWrapper == null)
            {
                if (GizmoDrawerUtility.ValidType(fieldType))
                {
                    _gizmoWrapper = GizmoDrawerUtility.GetWrapper(fieldType, attribute.GetType());
                    _gizmoWrapper.SetProperty(property);
                }
                else
                {
                    var label1 = new GUIContent(label);
                    EditorGUI.LabelField(position, label1,
                        new GUIContent($"{fieldType} not supported for {nameof(GizmoAttribute)}"));
                    return;
                }
            }
            
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label, true);
            if (EditorGUI.EndChangeCheck())
            {
                _gizmoWrapper?.SetProperty(property);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}