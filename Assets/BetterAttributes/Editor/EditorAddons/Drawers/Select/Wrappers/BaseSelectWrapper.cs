using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Select.SetupStrategies;
using Better.Attributes.Runtime.Select;
using Better.Commons.EditorAddons.Drawers.Caching;
using Better.Commons.EditorAddons.Drawers.Utility;
using Better.Commons.EditorAddons.Extensions;
using Better.Commons.Runtime.Drawers.Attributes;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public abstract class BaseSelectWrapper : UtilityWrapper
    {
        protected SerializedProperty _property;
        protected FieldInfo _fieldInfo;
        protected MultiPropertyAttribute _attribute;
        protected SetupStrategy _setupStrategy;

        public override void Deconstruct()
        {
            _property = null;
        }

        public abstract bool SkipFieldDraw();

        public abstract HeightCacheValue GetHeight();

        public abstract void Update(object value);

        public virtual void Setup(SerializedProperty property, FieldInfo fieldInfo, MultiPropertyAttribute attribute, SetupStrategy setupStrategy)
        {
            _property = property;
            _fieldInfo = fieldInfo;
            _attribute = attribute;
            _setupStrategy = setupStrategy;
        }

        public virtual bool Verify()
        {
            return _property.Verify();
        }

        public abstract object GetCurrentValue();
    }
}