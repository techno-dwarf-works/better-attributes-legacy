using System;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Headers
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class IconHeaderAttribute : PropertyAttribute
    {
        public string Path { get; }
        public bool UseResources { get; set; }

        public IconHeaderAttribute(string path)
        {
            Path = path;
        }
    }
}