using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public static class ComponentExtension
    {
        public static bool IsTargetComponent(this SerializedProperty property, out Component component)
        {
            component = null;
            if (property.serializedObject.targetObject is Component inner)
            {
                component = inner;
                return true;
            }

            return false;
        }

        public static bool Is<T>(this Object obj, out T value)
        {
            value = default;
            if (obj is T converter)
            {
                value = converter;
                return true;
            }

            return false;
        }
    }
}