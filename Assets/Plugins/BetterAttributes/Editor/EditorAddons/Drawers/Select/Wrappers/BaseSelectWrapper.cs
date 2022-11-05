using BetterAttributes.EditorAddons.Drawers.Utilities;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.Select.Wrappers
{
    public class BaseSelectWrapper : UtilityWrapper
    {
        private protected SerializedProperty _property;

        public override void Deconstruct()
        {
            _property = null;
        }

        public void SetProperty(SerializedProperty property)
        {
            _property = property;
        }
    }
}