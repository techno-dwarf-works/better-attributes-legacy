using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.PreviewDrawers
{
    public class AssetPopupWrapper : PopupPreviewWrapper
    {
        private protected override Texture GenerateTexture(Object drawnObject, float size)
        {
            if (!PrefabUtility.IsPartOfPrefabAsset(drawnObject))
            {
                return null;
            }
            
            var gameObject = (drawnObject as Component)?.gameObject;
            var originalSource = PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);
            var path = AssetDatabase.GetAssetPath(originalSource);
            PrefabUtility.LoadPrefabContentsIntoPreviewScene(path, _previewScene.PreviewScene);

            return _previewScene.GenerateTexture(size);
        }

        private protected override void UpdateTexture()
        {
            _previewScene.Render();
        }
    }
}