using Better.Extensions.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Better.Attributes.EditorAddons.Drawers.Preview
{
    public class SpriteWrapper : PreviewWrapper
    {
        private protected override Texture GenerateTexture(Object drawnObject, float size)
        {
            if (drawnObject.Cast<Sprite>(out var sprite))
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