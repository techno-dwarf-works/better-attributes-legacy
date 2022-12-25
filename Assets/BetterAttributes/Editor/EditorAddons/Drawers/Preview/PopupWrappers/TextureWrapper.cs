using Better.Extensions.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Better.Attributes.EditorAddons.Drawers.Preview
{
    public class TextureWrapper : PreviewWrapper
    {
        private protected override Texture GenerateTexture(Object drawnObject, float size)
        {
            Texture2D externalTexture = null;
            if (drawnObject.Cast<Texture>(out var texture))
            {
                externalTexture = Texture2D.CreateExternalTexture(texture.width, texture.height, TextureFormat.RGB24,
                    false, false,
                    texture.GetNativeTexturePtr());
            }

            if (drawnObject.Cast<Texture2D>(out var texture2D))
            {
                externalTexture = texture2D;
            }

            if (externalTexture != null)
            {
                CreateSpriteRenderer(externalTexture);
                return _previewScene.GenerateTexture(size);
            }

            return null;
        }

        private void CreateSpriteRenderer(Texture2D texture)
        {
            var spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
            spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
            var textureSize = new Vector2(texture.width, texture.height);
            spriteRenderer.sprite = Sprite.Create(texture, new Rect(Vector2.zero, textureSize), Vector2.one / 2f);
            SceneManager.MoveGameObjectToScene(spriteRenderer.gameObject, _previewScene.PreviewScene);
        }

        private protected override void UpdateTexture()
        {
            _previewScene.Render();
        }
    }
}