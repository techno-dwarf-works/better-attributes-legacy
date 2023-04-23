using Better.EditorTools;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    public class HideTransformButtonUtility
    {
        public void DrawHideTransformButton()
        {
            var text = Tools.hidden ? "Show" : "Hide";
            if (GUILayout.Button($"{text} Transform handles"))
            {
                Tools.hidden = !Tools.hidden;
                SceneView.RepaintAll();
            }
        }
    }
}