using System;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class DropDownItem : DropDownBase
    {
        private readonly Action<object> _onSelect;
        private readonly object _object;
        
        public DropDownItem(GUIContent content, bool state, Action<object> onSelect, object data) : base(content)
        {
            if (state)
            {
                Content.image = DrawersHelper.GetIcon(IconType.Checkmark);
            }
            _onSelect = onSelect;
            _object = data;
        }
        
        internal override bool Invoke(DropDownPopup downPopup)
        {
            _onSelect?.Invoke(_object);
            return true;
        }

        public override bool Contains(string searchText, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return Content.text.Contains(searchText, comparison);
        }
    }

    public abstract class DropDownBase
    {
        protected DropDownBase(GUIContent content)
        {
            Content = content;
        }

        public GUIContent Content { get; }
        internal abstract bool Invoke(DropDownPopup downPopup);

        public bool Equals(string value)
        {
            return Content.text.Equals(value);
        }

        public abstract bool Contains(string searchText,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase);
    }
}