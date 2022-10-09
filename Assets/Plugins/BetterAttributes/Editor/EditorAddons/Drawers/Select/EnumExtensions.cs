using System;
using System.Reflection;

namespace BetterAttributes.EditorAddons.Drawers.Select
{
    internal static class EnumExtensions
    {
        public const int FlagNone = 0;
        
        public static int ToFlagInt(this Enum a)
        {
            if (Enum.GetUnderlyingType(a.GetType()) != typeof(ulong))
                return (int)Convert.ToInt64(a);
            else
                return (int)Convert.ToUInt64(a);
        }

        public static bool IsFlagAll(this Enum eEnum)
        {
            return Equals(eEnum, eEnum.GetType().AllFlags());
        }

        public static bool IsFlagNone(this Enum eEnum)
        {
            return eEnum.ToFlagInt() == 0;
        }
        
        public static Enum AllFlags(this Type enumType)
        {
            if (!enumType.IsEnum)
            {
                if (enumType.GetCustomAttribute<FlagsAttribute>() == null)
                {
                    throw new Exception($"{enumType.Name} must have {nameof(FlagsAttribute)}");
                }
                throw new Exception("Type must be Enum");
            }
            
            long newValue = 0;
            var enumValues = Enum.GetValues(enumType);
            foreach (var value in enumValues)
            {
                long v = (long)Convert.ChangeType(value, TypeCode.Int64);
                if(v == 1 || v % 2 == 0)
                {
                    newValue |= v; 
                }
            }
            return (Enum)Enum.ToObject(enumType , newValue);
        }
        
        public static Enum Add(this Enum type, Enum value) {
            return (Enum)Enum.ToObject(type.GetType(), type.ToFlagInt() | value.ToFlagInt());   
        }
        
        public static Enum Remove(this Enum type, Enum value)
        {
            return (Enum)Enum.ToObject(type.GetType(), type.ToFlagInt() & ~value.ToFlagInt());
        }
    }
}