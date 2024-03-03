using System;
using System.Collections.Generic;
using System.Reflection;
using Better.Attributes.EditorAddons.Extensions;
using Better.Attributes.Runtime.Misc;
using Better.EditorTools.EditorAddons.Drawers.Base;
using Better.EditorTools.EditorAddons.Helpers;
using Better.Extensions.Runtime;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Misc.Wrappers
{
    public class EnumButtonsWrapper : MiscWrapper
    {
        private List<Tuple<int, GUIContent>> _buttonsLabels;
        private GUIStyle _buttonStyle = Styles.Button;
        private Enum _enum;
        private bool _isFlag;
        private int _everythingValue;

        public override void SetProperty(SerializedProperty property, FieldInfo fieldInfo, MiscAttribute attribute)
        {
            base.SetProperty(property, fieldInfo, attribute);
            var enumType = _fieldInfo.FieldType;
            
            if (enumType.IsArrayOrList())
            {
                enumType = enumType.GetCollectionElementType();
            }
            
            _isFlag = enumType.GetCustomAttribute<FlagsAttribute>() != null;
            _everythingValue = EnumUtility.EverythingFlag(enumType).ToFlagInt();

            var elements = Enum.GetValues(fieldInfo.FieldType);
            _buttonsLabels = new List<Tuple<int, GUIContent>>(elements.Length);
            foreach (Enum element in elements)
            {
                _buttonsLabels.Add(new Tuple<int, GUIContent>(element.ToFlagInt(), new GUIContent(element.ToString())));
            }
        }

        public override void PreDraw(Rect position, GUIContent label)
        {
        }

        public override void DrawField(Rect rect, GUIContent label)
        {
            var width = 0f;
            var maxLabelWidth = label.GetMaxWidth();
            var labelRect = new Rect(rect)
            {
                width = maxLabelWidth,
                height = EditorGUIUtility.singleLineHeight
            };
            EditorGUI.LabelField(labelRect, label, EditorStyles.label);
            DrawEnumButtons(rect, maxLabelWidth, width);
        }

        private void DrawEnumButtons(Rect rect, float maxLabelWidth, float width)
        {
            var buttonRect = new Rect(rect);
            buttonRect.height = EditorGUIUtility.singleLineHeight;
            buttonRect.x += maxLabelWidth;
            buttonRect.width -= maxLabelWidth;

            var copy = new Rect(buttonRect);
            var inspectorWidth = GetCurrentViewWidth(maxLabelWidth);
            var currentValue = _property.intValue;
            foreach (var button in _buttonsLabels)
            {
                _buttonStyle.CalcMinMaxWidth(button.Item2, out _, out var maxWidth);
                if (width + maxWidth > inspectorWidth)
                {
                    width = 0;
                    copy.y += EditorGUIUtility.singleLineHeight;
                    copy.x = buttonRect.x;
                }

                copy.width = maxWidth;

                if (GUI.Toggle(copy, currentValue == button.Item1, button.Item2, _buttonStyle))
                {
                    if (currentValue != button.Item1)
                    {
                        _property.intValue = EnumCalculator.CalculateCurrentValue(currentValue, _isFlag, button.Item1, _everythingValue);
                        _property.serializedObject.ApplyModifiedProperties();
                    }
                }

                copy.x += maxWidth;
                width += maxWidth;
            }
        }

        private static float GetCurrentViewWidth(float maxLabelWidth)
        {
            return EditorGUIUtility.currentViewWidth - maxLabelWidth - EditorGUIUtility.singleLineHeight * 2f;
        }

        public override HeightCacheValue GetHeight(GUIContent label)
        {
            var maxLabelWidth = label.GetMaxWidth();
            var inspectorWidth = GetCurrentViewWidth(maxLabelWidth);
            var width = 0f;
            var linesCount = 1;
            foreach (var button in _buttonsLabels)
            {
                _buttonStyle.CalcMinMaxWidth(button.Item2, out _, out var maxWidth);
                if (width + maxWidth > inspectorWidth)
                {
                    width = 0;
                    linesCount++;
                }

                width += maxWidth;
            }

            return HeightCacheValue.GetFull(linesCount * EditorGUIUtility.singleLineHeight).Force();
        }

        public override void PostDraw()
        {
        }
    }
}