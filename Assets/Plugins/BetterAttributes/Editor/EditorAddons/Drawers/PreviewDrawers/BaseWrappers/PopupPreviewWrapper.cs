using System.Threading;
using System.Threading.Tasks;
using BetterAttributes.EditorAddons.Helpers;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.PreviewDrawers
{
    public abstract class PopupPreviewWrapper : BasePreviewWrapper
    {
        private bool _isMouseDown;

        private CancellationTokenSource _cancellation;
        private Vector2 _currentMousePosition;
        private Vector2 _currentScreenMousePosition;
        private protected readonly PreviewSceneRenderer _previewScene;

        private protected virtual void UpdateTexture()
        {
        }

        protected PopupPreviewWrapper()
        {
            _previewScene = new PreviewSceneRenderer();
        }

        ~PopupPreviewWrapper()
        {
            Deconstruct();
        }

        public override void Deconstruct()
        {
            EditorPopup.CloseInstance();
            _previewScene?.Deconstruct();
            _cancellation?.Cancel(false);
            _cancellation = null;
            _isMouseDown = false;
        }

        private protected abstract Texture GenerateTexture(Object drawnObject, float size);

        public override void OnGUI(Rect position, SerializedProperty serializedProperty, float size)
        {
            if (!ValidateObject(serializedProperty.objectReferenceValue))
            {
                return;
            }

            _currentScreenMousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            _currentMousePosition = Event.current.mousePosition;
            CheckInteraction(position, serializedProperty, size);
        }

        private async void UpdateTextureLoop(CancellationToken cancellationToken)
        {
            await Task.Yield();
            while (_isMouseDown && !cancellationToken.IsCancellationRequested)
            {
                UpdateTexture();
                EditorPopup.UpdatePosition(_currentScreenMousePosition);
                await Task.Yield();
                if (cancellationToken.IsCancellationRequested) break;
            }
        }

        private void CheckInteraction(Rect position, SerializedProperty serializedProperty, float size)
        {
            switch (Event.current.type)
            {
                case EventType.MouseDown when position.Contains(_currentMousePosition):
                    MouseDownCase(serializedProperty, size);
                    break;
                case EventType.MouseUp:
                case EventType.MouseDrag when !position.Contains(_currentMousePosition):
                    Deconstruct();
                    break;
            }
        }

        private void MouseDownCase(SerializedProperty serializedProperty, float size)
        {
            if (!_isMouseDown)
            {
                _previewScene.Construct();
                EditorPopup.Initialize(GenerateTexture(serializedProperty.objectReferenceValue, size),
                    new Rect(_currentScreenMousePosition, Vector2.one * size));
                _isMouseDown = true;

                _cancellation = new CancellationTokenSource();
                UpdateTextureLoop(_cancellation.Token);
            }
        }
    }
}