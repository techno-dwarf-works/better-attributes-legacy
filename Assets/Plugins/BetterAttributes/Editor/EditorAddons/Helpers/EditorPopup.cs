using System;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Helpers
{
    public class EditorPopup : EditorWindow
    {
        private Texture _texture;
        private bool _needUpdate = true;
        private bool _destroyTexture;
        public event Action Closed;
        public event Action FocusLost;

        public event Action Destroyed;

        public static EditorPopup Initialize(Texture texture, Rect position, bool needUpdate,
            bool destroyTexture = false)
        {
            var window = HasOpenInstances<EditorPopup>() ? GetWindow<EditorPopup>() : CreateInstance<EditorPopup>();
            window.position = position;
            window._texture = texture;
            window._needUpdate = needUpdate;
            window._destroyTexture = destroyTexture;
            window.ShowPopup();
            return window;
        }

        public static EditorPopup InitializeAsWindow(Texture texture, Rect position, bool needUpdate,
            bool destroyTexture = false)
        {
            var window = HasOpenInstances<EditorPopup>() ? GetWindow<EditorPopup>() : CreateInstance<EditorPopup>();
            window.position = position;
            window._texture = texture;
            window._needUpdate = needUpdate;
            window._destroyTexture = destroyTexture;
            window.ShowUtility();
            return window;
        }

        private void Update()
        {
            if (_needUpdate)
                Repaint();
        }

        private void OnGUI()
        {
            if (_texture != null)
                GUI.DrawTexture(new Rect(0, 0, position.width, position.height), _texture, ScaleMode.ScaleToFit, true);
        }

        private void OnLostFocus()
        {
            FocusLost?.Invoke();
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke();
        }

        public static void CloseInstance()
        {
            if (!HasOpenInstances<EditorPopup>()) return;
            var window = GetWindow<EditorPopup>();
            window.Closed?.Invoke();
            if (window._destroyTexture)
            {
                Destroy(window._texture);
            }

            window.Close();
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            var rect = position;
            rect.position = newPosition;
            position = rect;
        }
    }
}