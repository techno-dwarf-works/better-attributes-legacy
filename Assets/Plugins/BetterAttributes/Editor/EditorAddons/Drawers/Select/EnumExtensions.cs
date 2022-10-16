using System;
using System.Collections.Generic;
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
            return Equals(eEnum, eEnum.GetType().EverythingFlag());
        }

        public static List<int> GetAllValues(this Type enumType)
        {
            var list = new List<int>();
            var enumValues = Enum.GetValues(enumType);
            foreach (var value in enumValues)
            {
                var v = ((Enum)value).ToFlagInt();
                list.Add(v);
            }

            return list;
        }

        public static bool IsFlagNone(this Enum eEnum)
        {
            return eEnum.ToFlagInt() == FlagNone;
        }
        
        public static Enum EverythingFlag(this Type enumType)
        {
            if (!ValidateEnum(enumType, out var exception))
            {
                throw exception;
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

        private static bool ValidateEnum(Type enumType, out Exception exception)
        {
            if (!enumType.IsEnum)
            {
                if (enumType.GetCustomAttribute<FlagsAttribute>() == null)
                {
                    exception = new Exception($"{enumType.Name} must have {nameof(FlagsAttribute)}");
                }

                exception = new Exception("Type must be Enum");
                return false;
            }

            exception = null;
            return true;
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