using Better.Extensions.Runtime;

namespace Better.Attributes.EditorAddons.Extensions
{
    public static class EnumSetterExtension
    {
        public static int CalculateCurrentValue(int currentValue, bool isFlag, int value, int everythingValue, int flagNone = EnumExtensions.FlagNone)
        {
            if (isFlag)
            {
                if (currentValue == flagNone)
                {
                    currentValue = value;
                }
                else if (value == flagNone)
                {
                    currentValue = value;
                }
                else if (value == everythingValue)
                {
                    currentValue = everythingValue;
                }
                else
                {
                    currentValue ^= value;
                }
            }
            else
            {
                currentValue = value;
            }

            return currentValue;
        }
    }
}