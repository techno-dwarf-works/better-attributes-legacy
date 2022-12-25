using Better.Attributes.EditorAddons.Drawers.Utilities;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
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