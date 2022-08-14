using BetterAttributes.Runtime.EditorAddons.GizmoAttributes;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.Drawers.GizmoDrawers
{
    [CustomPropertyDrawer(typeof(GizmoAttribute))]
    [CustomPropertyDrawer(typeof(GizmoLocalAttribute))]
    public class GizmoDrawer : PropertyDrawer
    {
        private GizmoWrapper _gizmoWrapper;
        private HideTransformButtonUtility _hideTransformDrawer;
        
        public GizmoDrawer()
        {
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
            _gizmoWrapper?.Deconstruct();
        }

        ~GizmoDrawer()
        {
            Selection.selectionChanged -= SelectionChanged;
            SceneView.duringSceneGui -= OnSceneGUIDelegate;
            _gizmoWrapper?.Deconstruct();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = property.propertyType;
            if (_gizmoWrapper == null)
            {
                if (GizmoDrawerUtility.ValidType(fieldType))
                {
                    _gizmoWrapper = GizmoDrawerUtility.GetWrapper(fieldType, attribute.GetType());
                    if(property.serializedObject.targetObject is MonoBehaviour)
                    {
                        _hideTransformDrawer = new HideTransformButtonUtility(property);
                    }
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

            if(_hideTransformDrawer != null)
            {
                _hideTransformDrawer.DrawHideTransformButton(position);
                position.y += EditorGUIUtility.singleLineHeight;
            }
            
            EditorGUI.PropertyField(PreparePropertyRect(position), property, label, true);

            if (EditorGUI.EndChangeCheck())
            {
                _gizmoWrapper.SetProperty(property);
            }

            if (GUI.Button(PrepareButtonRect(position), _gizmoWrapper.ShowInSceneView ? "Hide" : "Show"))
            {
                _gizmoWrapper.SwitchShowMode();
                SceneView.RepaintAll();
            }
        }

        private Rect PreparePropertyRect(Rect original)
        {
            var copy = original;
            copy.width *= 0.89f;
            return copy;
        }

        private Rect PrepareButtonRect(Rect original)
        {
            var copy = original;
            copy.x += copy.width * 0.9f;
            copy.width *= 0.1f;
            copy.height = EditorGUIUtility.singleLineHeight;
            return copy;
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.singleLineHeight;
        }
    }
}