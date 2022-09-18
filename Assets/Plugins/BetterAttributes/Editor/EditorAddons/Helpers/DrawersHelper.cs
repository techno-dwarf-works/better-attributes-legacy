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
        public static void HelpBox(string message, IconType type, bool useSpace = true)
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
        public static void HelpBox(string message, Rect position, IconType type)
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
            HelpBox(NotSupportedMessage(fieldName, fieldType, attributeType), position, IconType.ErrorMessage);
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
        public static void NotSupportedAttribute(string fieldName, Type fieldType, Type attributeType, bool useSpace = true)
        {
            HelpBox(NotSupportedMessage(fieldName, fieldType, attributeType), IconType.ErrorMessage, useSpace);
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
        private static void HelpBox(string message, IconType type, GUIStyle style, bool useSpace)
        {
            var icon = GetIconName(type);
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
        private static void HelpBox(string message, Rect position, IconType type, GUIStyle style)
        {
            var icon = GetIconName(type);
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
        public static string GetIconName(IconType type)
        {
            var icon = type switch
            {
                IconType.InfoMessage => "console.infoicon",
                IconType.WarningMessage => "console.warnicon",
                IconType.ErrorMessage => "console.erroricon",
                IconType.Info => "d__Help@2x",
                _ => ""
            };
            return icon;
        }
        
        /// <summary>
        /// Getting Icon Name from Unity Inspector
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Texture GetIcon(IconType type)
        {
            var icon = GetIconName(type);
            return EditorGUIUtility.IconContent(icon).image;
        }
    }

    internal enum IconType
    {
        /// <summary>
        ///   <para>Neutral message.</para>
        /// </summary>
        None,
        /// <summary>
        ///   <para>Info message.</para>
        /// </summary>
        InfoMessage,
        /// <summary>
        ///   <para>Warning message.</para>
        /// </summary>
        WarningMessage,
        /// <summary>
        ///   <para>Error message.</para>
        /// </summary>
        ErrorMessage,
        Info
    }
}