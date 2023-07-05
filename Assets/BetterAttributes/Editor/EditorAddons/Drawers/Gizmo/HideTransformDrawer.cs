using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    public class HideTransformButtonUtility
    {
        public void DrawHideTransformButton()
        {
            var text = UnityEditor.Tools.hidden ? "Show" : "Hide";
            if (GUILayout.Button($"{text} Transform handles"))
            {
                UnityEditor.Tools.hidden = !UnityEditor.Tools.hidden;
                SceneView.RepaintAll();
            }
        }
    }
}