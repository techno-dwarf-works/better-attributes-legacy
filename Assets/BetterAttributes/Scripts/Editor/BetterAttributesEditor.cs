using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BetterAttributes.EditorAddons;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class BetterAttributesEditor : Editor
    {
        private Dictionary<int, IEnumerable<KeyValuePair<MethodInfo, EditorButtonAttribute>>> _methodButtonsAttributes =
            new Dictionary<int, IEnumerable<KeyValuePair<MethodInfo, EditorButtonAttribute>>>();

        protected Object _bufferTarget;

        protected virtual void OnEnable()
        {
            _bufferTarget = target;
            var type = _bufferTarget.GetType();
            _methodButtonsAttributes = type.GetSortedMethodAttributes();
        }

        private void DrawButton(KeyValuePair<MethodInfo, EditorButtonAttribute> button,
            GUIStyle guiStyle)
        {
            var attribute = button.Value;
            var methodInfo = button.Key;

            if (GUILayout.Button(attribute.GetDisplayName(methodInfo.PrettyMemberName()), guiStyle))
                methodInfo.Invoke(_bufferTarget, attribute.InvokeParams);
        }

        private void DrawButtons(Dictionary<int, IEnumerable<KeyValuePair<MethodInfo, EditorButtonAttribute>>> buttons)
        {
            var guiStyle = new GUIStyle(GUI.skin.button)
            {
                stretchWidth = true,
                richText = true,
                wordWrap = true
            };

            foreach (var button in buttons)
            {
                if (button.Key == -1)
                {
                    var grouped = button.Value.GroupBy(key => key.Key, pair => pair.Value,
                        (info, attributes) =>
                            new KeyValuePair<MethodInfo,
                                IEnumerable<EditorButtonAttribute>>(info, attributes));
                    EditorGUILayout.BeginVertical();

                    foreach (var group in grouped)
                    {
                        EditorGUILayout.BeginHorizontal();

                        foreach (var attribute in group.Value)
                            DrawButton(new KeyValuePair<MethodInfo, EditorButtonAttribute>(group.Key, attribute),
                                guiStyle);
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndVertical();
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    foreach (var pair in button.Value) DrawButton(pair, guiStyle);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            DrawButtons(_methodButtonsAttributes);
            serializedObject.ApplyModifiedProperties();
        }
    }
}