using System;
using System.Collections.Generic;

namespace Dolphin.Object
{
    public interface IComparisonOptions
    {
        HashSet<string> Options { get; }
        ComparisonOptions Include(string propertyName);
        ComparisonOptions Include(IEnumerable<string> propertyNames);
        ComparisonOptions Include(Func<string, bool> func);
        ComparisonOptions Exclude(string propertyName);
        ComparisonOptions Exclude(IEnumerable<string> propertyNames);
        ComparisonOptions Exclude(Func<string, bool> func);
    }
}