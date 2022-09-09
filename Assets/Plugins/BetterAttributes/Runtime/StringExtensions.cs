using System.Text.RegularExpressions;

namespace BetterAttributes.Runtime
{
    public static class StringExtensions
    {
        internal static string PrettyCamelCase(this string input)
        {
            return Regex.Replace(input.Replace("_", ""), "((?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z]))", " $1").Trim();
        }

        internal static string ToTitleCase(this string input)
        {
            return Regex.Replace(input, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
        }
    }
}