using System.Reflection;
using Better.EditorTools;
using Better.EditorTools.Utilities;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public abstract class BaseSelectWrapper : UtilityWrapper
    {
        private protected SerializedProperty _property;
        private FieldInfo _fieldInfo;

        public override void Deconstruct()
        {
            _property = null;
        }

        public abstract bool SkipFieldDraw();
        public abstract float GetHeight();

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