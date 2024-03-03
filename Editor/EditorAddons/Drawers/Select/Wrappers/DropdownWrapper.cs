using Better.EditorTools.EditorAddons.Drawers.Base;
using Better.Extensions.EditorAddons;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class DropdownWrapper : BaseSelectWrapper
    {
        public override bool SkipFieldDraw()
        {
            return true;
        }

        public override HeightCacheValue GetHeight()
        {
            return HeightCacheValue.GetFull(EditorGUI.GetPropertyHeight(_property, false));
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