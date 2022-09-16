using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterAttributes.EditorAddons.Drawers.PreviewDrawers
{
    public class SpritePopupWrapper : PopupPreviewWrapper
    {
        private protected override Texture GenerateTexture(Object drawnObject, float size)
        {
            if (drawnObject is Sprite sprite)
            {
                var spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                SceneManager.MoveGameObjectToScene(spriteRenderer.gameObject, _previewScene.PreviewScene);
                return _previewScene.GenerateTexture(size);
            }

            return null;
        }
    }
}