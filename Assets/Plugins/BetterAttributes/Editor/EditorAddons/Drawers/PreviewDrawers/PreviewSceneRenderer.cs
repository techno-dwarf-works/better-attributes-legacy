using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterAttributes.EditorAddons.Drawers.PreviewDrawers
{
    public class PreviewSceneRenderer
    {
        private Camera _renderCamera;
        private float _cameraOffset = -1.5f;
        private float _lightIntensity = 0.5f;
        private float _cameraAspect = 1f;
        private float _angleStep = 20f;
        private GameObject _root;
        private Texture _texture;
        private Scene _previewScene;

        public Scene PreviewScene => _previewScene;

        public void Deconstruct()
        {
            EditorSceneManager.ClosePreviewScene(_previewScene);
            Object.DestroyImmediate(_texture);
        }

        public void Construct()
        {
            _previewScene = EditorSceneManager.NewPreviewScene();
        }

        public Texture GenerateTexture(float size)
        {
            _renderCamera = CreateCamera(_previewScene, size);

            var intSize = Mathf.RoundToInt(size);
            _texture = new Texture2D(intSize, intSize, TextureFormat.RGBA32, false);

            _root = _previewScene.GetRootGameObjects().First();
            var objectBounds = GetSceneBound(_root);
            ConfigureLight(_previewScene, objectBounds);
            PositionCamera(_renderCamera, objectBounds);
            return _texture;
        }

        private void ConfigureLight(Scene scene, Bounds objectBounds)
        {
            var light = new GameObject().AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = _lightIntensity;
            light.color = Color.white;
            var max = objectBounds.max;
            light.transform.forward = (objectBounds.center - max).normalized;
            SceneManager.MoveGameObjectToScene(light.gameObject, scene);
        }

        private void PositionCamera(Camera camera, Bounds objectBounds)
        {
            var min = objectBounds.min;
            var max = objectBounds.max;
            var corner = new Vector3(min.x, max.y, max.z);
            var transform = camera.transform;
            var cameraDirection = (objectBounds.center - corner).normalized;
            transform.position = corner + (cameraDirection * _cameraOffset);
            transform.forward = cameraDirection;
        }

        private Bounds GetSceneBound(GameObject root)
        {
            var bounds = new Bounds();
            var center = Vector3.zero;
            var count = 0;

            var renderers = root.GetComponentsInChildren<Renderer>(false);
            foreach (var renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
                center += renderer.bounds.center;
                count++;
            }

            center /= count;
            bounds.center = center;
            return bounds;
        }

        private Camera CreateCamera(Scene scene, float size)
        {
            var camera = new GameObject().AddComponent<Camera>();
            var intSize = Mathf.RoundToInt(size);
            camera.aspect = _cameraAspect;
            camera.orthographicSize = size / 2f;
            camera.backgroundColor = Color.gray;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.targetTexture = new RenderTexture(intSize, intSize, 16);
            camera.cameraType = CameraType.Preview;
            camera.scene = scene;
            SceneManager.MoveGameObjectToScene(camera.gameObject, scene);
            return camera;
        }

        public void Render()
        {
            _renderCamera.Render();
            Graphics.CopyTexture(_renderCamera.targetTexture, _texture);
            _root.transform.Rotate(Vector3.up, _angleStep * Time.deltaTime);
        }
    }
}