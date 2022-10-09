using System;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public abstract class DropdownBase
    {
        protected DropdownBase(GUIContent content)
        {
            Content = content;
        }

        public GUIContent Content { get; }
        internal abstract bool Invoke(DropdownWindow downPopup);

        public bool Equals(string value)
        {
            return Content.text.Equals(value);
        }

        public abstract bool Contains(string searchText,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase);
    }
}