using BetterAttributes.EditorAddons.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterAttributes.EditorAddons.Drawers.Preview
{
    public class SpriteWrapper : PreviewWrapper
    {
        private protected override Texture GenerateTexture(Object drawnObject, float size)
        {
            if (drawnObject.Is<Sprite>(out var sprite))
            {
                var spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
                spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
                spriteRenderer.sprite = sprite;
                SceneManager.MoveGameObjectToScene(spriteRenderer.gameObject, _previewScene.PreviewScene);
                return _previewScene.GenerateTexture(size);
            }

            return null;
        }
        
        private protected override void UpdateTexture()
        {
            _previewScene.Render();
        }
    }
}