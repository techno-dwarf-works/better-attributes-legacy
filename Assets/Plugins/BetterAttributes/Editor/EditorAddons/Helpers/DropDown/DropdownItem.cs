using System;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class DropdownItem : DropdownBase
    {
        private readonly Action<object> _onSelect;
        private readonly object _object;

        public DropdownItem(GUIContent content, bool state, Action<object> onSelect, object data) : base(content)
        {
            if (state)
            {
                Content.image = DrawersHelper.GetIcon(IconType.Checkmark);
            }

            _onSelect = onSelect;
            _object = data;
        }

        internal override bool Invoke(DropdownWindow downPopup)
        {
            _onSelect?.Invoke(_object);
            return true;
        }
    }
}