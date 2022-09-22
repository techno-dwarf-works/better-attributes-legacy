using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class DropDownPopup : EditorWindow
    {
        private class DropDownItem
        {
            private readonly Action<object> _onSelect;
            private readonly object _object;
            public GUIContent Content { get; }
            public bool State { get; }

            public DropDownItem(GUIContent content, Action<object> onSelect, object data, bool state)
            {
                Content = content;
                State = state;
                _onSelect = onSelect;
                _object = data;
            }


            public void Invoke()
            {
                _onSelect?.Invoke(_object);
            }

            public bool Contains(string searchText, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
            {
                return Content.text.Contains(searchText, comparison);
            }
        }

        private readonly List<DropDownItem> _items = new List<DropDownItem>();
        private Vector2 _scrollPosition;
        private string _searchText;
        private Vector2 _display;
        private int _maxLines = 10;

        [DidReloadScripts]
        private static void OnDidReloadScripts()
        {
            CloseInstance();
        }

        public static void CloseInstance()
        {
            if (HasOpenInstances<DropDownPopup>())
            {
                var window = GetWindow<DropDownPopup>();
                window.ResetPopup();
                window.Close();
            }
        }

        public static DropDownPopup ShowWindow(Rect displayPosition)
        {
            var window = HasOpenInstances<DropDownPopup>()
                ? GetWindow<DropDownPopup>()
                : CreateInstance<DropDownPopup>();
            window.ResetPopup();
            window.ShowPopup();
            window.SetPosition(displayPosition);
            return window;
        }

        private void SetPosition(Rect displayPosition)
        {
            position = displayPosition;
            _display = displayPosition.position;
        }

        private void ResetPopup()
        {
            _items.Clear();
            _scrollPosition = Vector2.zero;
            _searchText = string.Empty;
        }

        public void AddItem(GUIContent content, bool on, Action<object> action, object data)
        {
            _items.Add(new DropDownItem(content, action, data, on));
        }

        private void OnLostFocus()
        {
            CloseInstance();
        }

        private void OnGUI()
        {
            var copy = new GUIStyle(Styles.FrameBox)
            {
                margin = new RectOffset(),
                padding = new RectOffset()
            };
            
            EditorGUILayout.BeginVertical(copy);
            var buttonStyle = DrawersHelper.GetButtonStyle();
            _searchText = DrawSearchField(_searchText);
            var buffer = SearchResult();

            var close = AnalyseUserInput(buffer);
            if (!close)
            {
                close = DrawItems(buffer, buttonStyle);
            }

            EditorGUILayout.EndVertical();

            if (!close) return;
            ResetPopup();
            Close();
        }

        private bool DrawItems(List<DropDownItem> buffer, GUIStyle buttonStyle)
        {
            _scrollPosition =
                EditorGUILayout.BeginScrollView(_scrollPosition, GUIStyle.none, GUI.skin.verticalScrollbar);

            EditorGUILayout.BeginVertical(Styles.DefaultContentMargins);
            var shouldClose = false;

            foreach (var item in buffer)
            {
                var bufferContent = item.Content;
                if (item.State)
                {
                    bufferContent.image = DrawersHelper.GetIcon(IconType.Checkmark);
                }

                ResolveSize(bufferContent, buttonStyle);
                if (!GUILayout.Button(bufferContent, buttonStyle)) continue;
                item.Invoke();
                shouldClose = true;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            return shouldClose;
        }

        private List<DropDownItem> SearchResult()
        {
            var buffer = string.IsNullOrEmpty(_searchText) || string.IsNullOrWhiteSpace(_searchText)
                ? _items
                : _items.Where(x => x.Contains(_searchText)).ToList();

            return buffer;
        }

        private bool AnalyseUserInput(List<DropDownItem> buffer)
        {
            var currentKeyCode = Event.current.keyCode;
            switch (currentKeyCode)
            {
                case KeyCode.Return:
                    var first = buffer.FirstOrDefault();
                    if (first != null)
                    {
                        first.Invoke();
                        return true;
                    }

                    break;
            }

            return false;
        }

        private void ResolveSize(GUIContent content, GUIStyle buttonStyle)
        {
            var width = Mathf.Max(position.width, buttonStyle.CalcSize(content).x + 50f);
            var height = Mathf.Max(position.height, EditorGUIUtility.singleLineHeight *
                (Mathf.Max(_maxLines, _items.Count) + 1) + DrawersHelper.SpaceHeight);
            var copy = position;
            copy.position = _display;
            copy.width = width;
            copy.height = height;
            position = copy;
        }

        private string DrawSearchField(string searchText)
        {
            EditorGUILayout.BeginHorizontal(Styles.DefaultContentMargins);
            searchText = GUILayout.TextField(searchText, Styles.ToolbarSearchField);

            var copy = string.IsNullOrEmpty(_searchText)
                ? Styles.ToolbarSearchFieldCancelButtonEmpty
                : Styles.ToolbarSearchFieldCancelButton;

            if (GUILayout.Button(GUIContent.none, copy))
            {
                searchText = string.Empty;
                EditorGUIUtility.editingTextField = false;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(DrawersHelper.SpaceHeight);
            return searchText;
        }
    }
}