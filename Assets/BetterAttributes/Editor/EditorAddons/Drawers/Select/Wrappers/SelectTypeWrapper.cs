using System;
using Better.Commons.EditorAddons.Extensions;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class SelectTypeWrapper : BaseSelectWrapper
    {

        public override void Update(object value)
        {
            if (!_property.Verify()) return;
            var typeValue = (Type)value;
            _property.managedReferenceValue = typeValue == null ? null : Activator.CreateInstance(typeValue);
        }

        public override object GetCurrentValue()
        {
            return _property.GetManagedType();
        }
    }
}