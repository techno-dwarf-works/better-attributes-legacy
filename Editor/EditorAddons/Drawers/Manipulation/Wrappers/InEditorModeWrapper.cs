using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Manipulation.Wrappers
{
    public class InEditorModeWrapper : ManipulateWrapper
    {
        protected override bool IsConditionSatisfied()
        {
            return !EditorApplication.isPlaying;
        }
    }
}