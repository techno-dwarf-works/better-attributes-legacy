using System;
using System.Collections.Generic;
using System.Linq;
using BetterAttributes.Runtime.Tree;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class DropDownPopup : EditorWindow
    {
        private Vector2 _scrollPosition;
        private string _searchText;
        private Vector2 _display;
        private int _maxLines = 10;

        private TreeNode<DropDownBase> _currentNode;
        private Stack<TreeNode<DropDownBase>> _previousItems = new Stack<TreeNode<DropDownBase>>();

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
            _currentNode = null;
            _previousItems.Clear();
            _scrollPosition = Vector2.zero;
            _searchText = string.Empty;
        }

        public void SetItems(DropDownCollection collection)
        {
            ResetPopup();
            SetCurrentDrawItems(collection);
        }

        internal void SetCurrentDrawItems(TreeNode<DropDownBase> node)
        {
            if (_currentNode != null)
            {
                _previousItems.Push(_currentNode);
            }

            _currentNode = node;
        }

        private void OnLostFocus()
        {
            CloseInstance();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            _searchText = DropDownGUI.DrawSearchField(_searchText, false);
            var buffer = SearchResult(_currentNode, _searchText);

            var hasParent = _currentNode.Parent != null;
            DropDownGUI.DrawHeader(hasParent ? _currentNode.Value.Content : new GUIContent("Available Types"),
                hasParent, OnBackClicked);
            var close = AnalyseUserInput(buffer);
            if (!close)
            {
                close = DrawItems(buffer, Styles.Button);
            }

            EditorGUILayout.EndVertical();

            if (!close) return;
            ResetPopup();
            Close();
        }

        private void OnBackClicked()
        {
            _currentNode = _previousItems.Pop();
        }

        private void Update()
        {
            Repaint();
        }

        private bool DrawItems(List<TreeNode<DropDownBase>> buffer, GUIStyle buttonStyle)
        {
            _scrollPosition =
                EditorGUILayout.BeginScrollView(_scrollPosition, GUIStyle.none, GUI.skin.verticalScrollbar);

            EditorGUILayout.BeginVertical(GUIStyle.none);
            var shouldClose = false;

            var size = Vector2.zero;
            foreach (var item in buffer)
            {
                var bufferContent = item.Value.Content;
                size = Vector2.Max(buttonStyle.CalcSize(bufferContent), size);
                if (DropDownGUI.DrawItem(item, item.Children.Count > 0, true))
                    shouldClose = item.Value.Invoke(this);
            }

            ResolveSize(size, _currentNode.Children.Count);

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            return shouldClose;
        }

        private List<TreeNode<DropDownBase>> SearchResult(TreeNode<DropDownBase> rootNode, string searchText)
        {
            var buffer = string.IsNullOrEmpty(searchText) || string.IsNullOrWhiteSpace(searchText)
                ? rootNode.Children.ToList()
                : rootNode.Children.Where(node => RecursiveSearch(node, searchText)).ToList();

            return buffer;
        }

        private bool RecursiveSearch(TreeNode<DropDownBase> node, string searchText)
        {
            var isFound = node.Value.Contains(searchText);
            if (node.Children.Count > 0)
            {
                isFound = isFound || node.Children.Any(x => RecursiveSearch(x, searchText));
            }

            return isFound;
        }

        private bool AnalyseUserInput(List<TreeNode<DropDownBase>> buffer)
        {
            if (Event.current.type != EventType.KeyDown)
            {
                return false;
            }

            var currentKeyCode = Event.current.keyCode;
            switch (currentKeyCode)
            {
                case KeyCode.Return:
                    var first = buffer.FirstOrDefault();
                    if (first != null)
                    {
                        return first.Value.Invoke(this);
                    }
                    break;
                case KeyCode.Escape:
                    if(string.IsNullOrEmpty(_searchText))
                    {
                        if (_previousItems.Count > 0)
                        {
                            OnBackClicked();
                            return _previousItems.Count <= 0;
                        }

                        return true;
                    }
                    else
                    {
                        _searchText = string.Empty;
                        DropDownGUI.ClearSearchField();
                    }
                    break;
            }

            return false;
        }

        private void ResolveSize(Vector2 size, int currentItems)
        {
            var width = Mathf.Max(position.width, size.x + 50f);
            var height = Mathf.Max(position.height, EditorGUIUtility.singleLineHeight *
                (Mathf.Max(_maxLines, currentItems) + 1) + DrawersHelper.SpaceHeight);
            var copy = position;
            copy.position = _display;
            copy.width = width;
            copy.height = height;
            position = copy;
        }
    }
}