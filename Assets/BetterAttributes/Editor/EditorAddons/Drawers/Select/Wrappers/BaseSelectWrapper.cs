using System.Reflection;
using Better.Commons.EditorAddons.Drawers.Caching;
using Better.Commons.EditorAddons.Drawers.Utility;
using Better.Commons.EditorAddons.Extensions;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public abstract class BaseSelectWrapper : UtilityWrapper
    {
        protected SerializedProperty _property;
        protected FieldInfo _fieldInfo;

        public override void Deconstruct()
        {
            _property = null;
        }

        public abstract bool SkipFieldDraw();

        public abstract HeightCacheValue GetHeight();

        public abstract void Update(object value);

        public virtual void SetProperty(SerializedProperty property, FieldInfo fieldInfo)
        {
            _property = property;
            _fieldInfo = fieldInfo;
        }

        public virtual bool Verify()
        {
            return _property.Verify();
        }

        public abstract object GetCurrentValue();
    }
}