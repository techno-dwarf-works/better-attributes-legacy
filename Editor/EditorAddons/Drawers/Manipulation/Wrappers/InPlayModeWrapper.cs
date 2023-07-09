using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Manipulation.Wrappers
{
    public class InPlayModeWrapper : ManipulateWrapper
    {
        protected override bool IsConditionSatisfied()
        {
            return EditorApplication.isPlaying;
        }
    }
}