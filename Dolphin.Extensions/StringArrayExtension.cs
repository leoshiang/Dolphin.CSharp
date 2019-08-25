using System.Collections.Generic;
using System.Linq;

namespace Dolphin.Extensions
{
    public static class StringArrayExtension
    {
        public static void AddStrings(this List<string> array, string[] source)
        {
            var validItems = source.Where(x => !x.IsNullOrEmpty() && !array.Contains(x)).Distinct();
            array.AddRange(validItems);
        }
    }
}