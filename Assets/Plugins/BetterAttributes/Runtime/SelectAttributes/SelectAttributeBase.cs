using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.SelectAttributes
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class SelectAttributeBase : PropertyAttribute
    {
        private readonly Type _type;
        public bool FindTypesRecursively { get; }

        public SelectAttributeBase(Type type, bool findTypesRecursively = false)
        {
            _type = type;
            FindTypesRecursively = findTypesRecursively;
        }

        public SelectAttributeBase(bool useTypeExplicitly = false)
        {
            FindTypesRecursively = useTypeExplicitly;
        }

        public Type GetFieldType()
        {
            return _type;
        }

        public SelectAttributeBase()
        {
        }
    }
}
