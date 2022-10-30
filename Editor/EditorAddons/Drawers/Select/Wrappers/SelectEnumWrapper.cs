using System;
using BetterExtensions.Runtime.Extension;

namespace BetterAttributes.EditorAddons.Drawers.Select.Wrappers
{
    public class SelectEnumWrapper : BaseSelectWrapper
    {
        private bool _isFlag;

        public void Update(Enum value)
        {
            if (_property == null) return;
            var enumType = value.GetType();
            if (!_isFlag)
            {
                _property.intValue = value.ToFlagInt();
            }
            else
            {
                Enum currentEnum;
#if UNITY_2021_1_OR_NEWER
                currentEnum = (Enum)Enum.ToObject(enumType, _property.enumValueFlag);
#else
                currentEnum = (Enum)Enum.ToObject(enumType, _property.intValue);
#endif

                if (value.IsFlagNone())
                {
                    currentEnum = value;
                }
                else
                {
                    currentEnum = currentEnum.HasFlag(value) ? currentEnum.Remove(value) : currentEnum.Add(value);
                }
#if UNITY_2021_1_OR_NEWER
                _property.enumValueFlag = currentEnum.ToFlagInt();
#else
                _property.intValue = currentEnum.ToFlagInt();
#endif
            }
        }

        public void SetIsFlag(bool isFlag)
        {
            _isFlag = isFlag;
        }
    }
}