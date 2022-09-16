﻿using System;
using System.Diagnostics;

namespace BetterAttributes.Runtime.SelectAttributes
{
    /// <summary>
    /// Attribute for Implementation selection in Inspector.
    /// Use in pair with [SerializeReference] Attribute.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectImplementationAttribute : SelectAttributeBase
    {
        public SelectImplementationAttribute(Type type) : base(type)
        {
        }

        public SelectImplementationAttribute()
        {
        }
    }
}
