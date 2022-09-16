using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.PreviewDrawers
{
    public class TexturePopupWrapper : PopupPreviewWrapper
    {
        private Texture _texture;
        
        private protected override Texture GenerateTexture(Object drawnObject, float size)
        {
            return drawnObject as Texture;
        }
    }
}