using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class EditorPopup : EditorWindow
    {
        private Texture _texture;

        public static void Initialize(Texture texture, Rect position)
        {
            var window = HasOpenInstances<EditorPopup>() ? GetWindow<EditorPopup>() : CreateInstance<EditorPopup>();
            window.position = position;
            window._texture = texture;
            window.ShowPopup();
        }

        private void Update()
        {
            Repaint();
        }

        private void OnGUI()
        {
            if (_texture != null)
                GUI.DrawTexture(new Rect(0, 0, position.width, position.height), _texture, ScaleMode.ScaleToFit, true);
        }

        public static void CloseInstance()
        {
            if (!HasOpenInstances<EditorPopup>()) return;
            var window = GetWindow<EditorPopup>();
            window.Close();
        }

        public static void UpdatePosition(Vector2 newPosition)
        {
            if (!HasOpenInstances<EditorPopup>()) return;
            var window = GetWindow<EditorPopup>();
            var rect = window.position;
            rect.position = newPosition;
            window.position = rect;
        }
    }
}