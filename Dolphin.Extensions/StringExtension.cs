using System.ComponentModel;
using System.Globalization;

namespace Dolphin.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string target) => string.IsNullOrEmpty(target);

        public static T TryParse<T>(this string inValue)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T) converter.ConvertFromString(null, CultureInfo.InvariantCulture, inValue);
        }

        public static string Repeat(char chatToRepeat, int repeat) =>
            new string(chatToRepeat, repeat);
    }
}