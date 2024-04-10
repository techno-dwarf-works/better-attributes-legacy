﻿using Better.Commons.EditorAddons.Enums;
using Better.Commons.EditorAddons.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Better.Attributes.EditorAddons.Drawers.Preview
{
    public class AssetWrapper : PreviewWrapper
    {
        private bool _objectChecked;
        private bool _state;

        private protected override bool ValidateObject(Object drawnObject)
        {
            var baseValid = base.ValidateObject(drawnObject);
            if (!baseValid)
            {
                return false;
            }

            if (!(drawnObject is Component component))
            {
                ExtendedGUIUtility.HelpBox(
                    $"Preview not available for type: {ExtendedGUIUtility.FormatBold(drawnObject.GetType().Name)}",
                    IconType.ErrorMessage, false);
                return false;
            }

            if (!_objectChecked)
            {
                _state = component.gameObject.GetComponentsInChildren<Renderer>().Length > 0;
                _objectChecked = _state;
            }

            if (!_state)
            {
                ExtendedGUIUtility.HelpBox(
                    $"Preview not available for objects without {ExtendedGUIUtility.FormatBold($"{nameof(Renderer)}s")} in children",
                    IconType.ErrorMessage, false);
                return false;
            }

            return true;
        }

        public override void IsObjectUpdated(bool objectChanged)
        {
            base.IsObjectUpdated(objectChanged);
            if (objectChanged)
            {
                _objectChecked = false;
            }
        }

        private protected override Texture GenerateTexture(Object drawnObject, float size)
        {
            var gameObject = (drawnObject as Component)?.gameObject;
            if (PrefabUtility.IsPartOfPrefabAsset(drawnObject))
            {
                var path = AssetDatabase.GetAssetPath(gameObject);
                PrefabUtility.LoadPrefabContentsIntoPreviewScene(path, _previewScene.PreviewScene);
            }
            else
            {
                var copy = Object.Instantiate(gameObject);
                SceneManager.MoveGameObjectToScene(copy, _previewScene.PreviewScene);
            }

            return _previewScene.GenerateTexture(size);
        }

        private protected override void UpdateTexture()
        {
            _previewScene.Render();
        }
    }
}