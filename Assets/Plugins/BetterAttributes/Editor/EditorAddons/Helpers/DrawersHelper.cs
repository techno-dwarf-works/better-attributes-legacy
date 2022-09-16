using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BetterAttributes.EditorAddons.Helpers
{
    internal static class DrawersHelper
    {
        private static float _spaceHeight = EditorGUIUtility.singleLineHeight;

        /// <summary>
        /// Override for default Inspector HelpBox with RTF text
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="useSpace"></param>
        public static void HelpBox(string message, MessageType type, bool useSpace = true)
        {
            var style = new GUIStyle(EditorStyles.helpBox) { richText = true, fontSize = 11 };
            HelpBox(message, type, style, useSpace);
        }

        /// <summary>
        /// Override for default Inspector HelpBox with RTF text
        /// </summary>
        /// <param name="message"></param>
        /// <param name="position"></param>
        /// <param name="type"></param>
        public static void HelpBox(string message, Rect position, MessageType type)
        {
            var style = new GUIStyle(EditorStyles.helpBox) { richText = true, fontSize = 11, wordWrap = true };
            HelpBox(message, position, type, style);
        }

        /// <summary>
        /// Not supported Inspector HelpBox with RTF text
        /// </summary>
        /// <param name="position"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="attributeType"></param>
        public static void NotSupportedAttribute(Rect position, string fieldName, Type fieldType, Type attributeType)
        {
            HelpBox(NotSupportedMessage(fieldName, fieldType, attributeType), position, MessageType.Error);
        }

        private static string NotSupportedMessage(string fieldName, Type fieldType, Type attributeType)
        {
            return
                $"Field {FormatBold(fieldName)} with {FormatBold(fieldType.Name)} not supported for {FormatBold(attributeType.Name)}";
        }

        /// <summary>
        /// Not supported Inspector HelpBox with RTF text
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="attributeType"></param>
        public static void NotSupportedAttribute(string fieldName, Type fieldType, Type attributeType)
        {
            HelpBox(NotSupportedMessage(fieldName, fieldType, attributeType), MessageType.Error);
        }

        public static string FormatBold(string text)
        {
            return $"<b>{text}</b>";
        }

        public static string FormatItalic(string text)
        {
            return $"<i>{text}</i>";
        }

        public static string FormatBoldItalic(string text)
        {
            return FormatBold(FormatItalic(text));
        }

        /// <summary>
        /// Override for default Inspector HelpBox with style
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="style"></param>
        /// <param name="useSpace"></param>
        private static void HelpBox(string message, MessageType type, GUIStyle style, bool useSpace)
        {
            var icon = IconName(type);
            if (useSpace)
            {
                EditorGUILayout.Space(_spaceHeight);
            }
            EditorGUILayout.LabelField(GUIContent.none, EditorGUIUtility.TrTextContentWithIcon(message, icon), style);
        }

        /// <summary>
        /// Override for default Inspector HelpBox with style
        /// </summary>
        /// <param name="message"></param>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <param name="style"></param>
        private static void HelpBox(string message, Rect position, MessageType type, GUIStyle style)
        {
            var icon = IconName(type);
            var withIcon = EditorGUIUtility.TrTextContentWithIcon(message, icon);
            position.height = style.CalcHeight(withIcon, position.width);
            EditorGUI.LabelField(position, GUIContent.none, withIcon, style);
        }

        public static int SelectionGrid(int selected, string[] texts, int xCount, GUIStyle style,
            params GUILayoutOption[] options)
        {
            var bufferSelected = selected;
            GUILayout.BeginVertical();
            var count = 0;
            var isHorizontal = false;

            for (var index = 0; index < texts.Length; index++)
            {
                var text = texts[index];

                if (count == 0)
                {
                    GUILayout.BeginHorizontal();
                    isHorizontal = true;
                }

                count++;
                if (GUILayout.Toggle(bufferSelected == index, text, new GUIStyle(style), options))
                    bufferSelected = index;

                if (count == xCount)
                {
                    GUILayout.EndHorizontal();
                    count = 0;
                    isHorizontal = false;
                }
            }

            if (isHorizontal) GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            return bufferSelected;
        }

        /// <summary>
        /// Getting Icon Name from Unity Inspector
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string IconName(MessageType type)
        {
            var icon = type switch
            {
                MessageType.Info => "console.infoicon",
                MessageType.Warning => "console.warnicon",
                MessageType.Error => "console.erroricon",
                _ => ""
            };
            return icon;
        }
    }
}