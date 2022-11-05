using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Comparers;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.Select
{
    public class SelectedItem<T>
    {
        public SelectedItem(SerializedProperty property, T data)
        {
            Property = property;
            Data = data;
        }

        public SerializedProperty Property { get; }
        public T Data { get; }

        public bool Equals(SelectedItem<T> other)
        {
            return SerializedPropertyComparer.Instance.Equals(Property, other.Property) && Data.Equals(other.Data);
        }

        public bool Equals(SelectedItem<object> other)
        {
            return SerializedPropertyComparer.Instance.Equals(Property, other.Property) && Data.Equals(other.Data);
        }
    }
}