using Better.Commons.EditorAddons.Extensions;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class DropdownWrapper : BaseSelectWrapper
    {

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