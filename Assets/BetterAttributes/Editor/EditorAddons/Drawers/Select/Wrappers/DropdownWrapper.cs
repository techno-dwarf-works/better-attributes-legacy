using Better.EditorTools;
using Better.EditorTools.Drawers.Base;
using Better.Tools.Runtime;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class DropdownWrapper : BaseSelectWrapper
    {
        public override bool SkipFieldDraw()
        {
            return true;
        }

        public override HeightCache GetHeight()
        {
            return HeightCache.GetFull(EditorGUI.GetPropertyHeight(_property, false));
        }

        public override void Update(object value)
        {
            if (!_property.Verify()) return;
            _property.SetValue(value);
        }

        public override object GetCurrentValue()
        {
            return _property.GetValue();
        }
    }
}