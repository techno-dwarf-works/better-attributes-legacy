using System;
using System.Collections.Generic;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.Base
{
    public abstract class BaseComparer<T, U> where T : IEqualityComparer<U>, new()
    {
        public static T Instance { get; } = new T();
    }

    public class TypeComparer : BaseComparer<TypeComparer, Type>, IEqualityComparer<Type>
    {
        public bool Equals(Type x, Type y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.IsAssignableFrom(y) || x == y) return true;
            return (y.IsInterface || y.IsAbstract) && x == typeof(Type);
        }

        public int GetHashCode(Type obj)
        {
            return 0;
        }
    }

    public class AssignableFromComparer : BaseComparer<AssignableFromComparer, Type>, IEqualityComparer<Type>
    {
        public bool Equals(Type x, Type y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            var isAssignableFrom = x.IsAssignableFrom(y);
            return isAssignableFrom || x == y;
        }

        public int GetHashCode(Type obj)
        {
            return 0;
        }
    }

    public class SerializedPropertyComparer : BaseComparer<SerializedPropertyComparer, SerializedProperty>, IEqualityComparer<SerializedProperty>
    {
        public bool Equals(SerializedProperty x, SerializedProperty y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.propertyPath == y.propertyPath;
        }

        public int GetHashCode(SerializedProperty obj)
        {
            return (obj.propertyPath != null ? obj.propertyPath.GetHashCode() : 0);
        }
    }
}