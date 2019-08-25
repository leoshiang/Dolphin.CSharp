using System;
using System.Collections.Generic;

namespace Dolphin.Object
{
    public class ComparisonOptions : IComparisonOptions
    {
        private readonly List<string> _originalPropertyNames = new List<string>();

        public ComparisonOptions() : this(new List<string>())
        {
        }

        public ComparisonOptions(IEnumerable<string> propertyNames)
        {
            _originalPropertyNames.AddRange(propertyNames);
        }

        public HashSet<string> Options { get; } = new HashSet<string>();

        public ComparisonOptions Include(string propertyName)
        {
            EnsurePropertyExists(propertyName);
            Options.Add(propertyName);
            return this;
        }

        public ComparisonOptions Include(IEnumerable<string> propertyNames)
        {
            foreach (var x in propertyNames) Include(x);
            return this;
        }

        public ComparisonOptions Include(Func<string, bool> func)
        {
            foreach (var x in _originalPropertyNames)
                if (func(x))
                    Include(x);
            return this;
        }

        public ComparisonOptions Exclude(string propertyName)
        {
            EnsurePropertyExists(propertyName);
            Options.Remove(propertyName);
            return this;
        }

        public ComparisonOptions Exclude(IEnumerable<string> propertyNames)
        {
            foreach (var x in propertyNames) Exclude(x);
            return this;
        }

        public ComparisonOptions Exclude(Func<string, bool> func)
        {
            foreach (var x in _originalPropertyNames)
                if (func(x))
                    Exclude(x);
            return this;
        }

        private void EnsurePropertyExists(string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));
            if (!_originalPropertyNames.Contains(propertyName))
                throw new InvalidOperationException();
        }
    }
}