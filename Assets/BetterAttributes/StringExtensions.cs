using System.Text.RegularExpressions;

namespace BetterAttributes
{
    public static class StringExtensions
    {
        public static string PrettyCamelCase(this string input)
        {
            return Regex.Replace(input.Replace("_", ""), "((?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z]))", " $1").Trim();
        }

        public static string ToTitleCase(this string input)
        {
            return Regex.Replace(input, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
        }
    }
}