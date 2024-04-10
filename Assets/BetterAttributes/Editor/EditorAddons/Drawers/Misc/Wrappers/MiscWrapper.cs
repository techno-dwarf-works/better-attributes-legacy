using System.Reflection;
using Better.Attributes.Runtime.Misc;
using Better.Commons.EditorAddons.Drawers.Caching;
using Better.Commons.EditorAddons.Drawers.Utility;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Misc.Wrappers
{
    public abstract class MiscWrapper : UtilityWrapper
    {
        protected SerializedProperty _property;
        protected MiscAttribute _attribute;
        protected FieldInfo _fieldInfo;

        public override void Deconstruct()
        {
            
        }

        public virtual void SetProperty(SerializedProperty property, FieldInfo fieldInfo, MiscAttribute attribute)
        {
            _property = property;
            _attribute = attribute;
            _fieldInfo = fieldInfo;
        }

        public abstract void PreDraw(Rect position, GUIContent label);
        public abstract void DrawField(Rect rect, GUIContent label);
        public abstract void PostDraw();
        public abstract HeightCacheValue GetHeight(GUIContent label);
    }
}