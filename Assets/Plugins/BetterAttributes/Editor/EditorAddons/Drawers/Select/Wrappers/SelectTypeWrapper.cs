using System;

namespace BetterAttributes.EditorAddons.Drawers.Select.Wrappers
{
    public class SelectTypeWrapper : BaseSelectWrapper
    {
        public void Update(Type value)
        {
            if (_property == null) return;
            _property.managedReferenceValue = value == null ? null : Activator.CreateInstance(value);
        }
    }
}