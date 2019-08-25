using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dolphin.Extensions;

namespace Dolphin.IO
{
    public class SearchFilter : ISearchFilter
    {
        private readonly List<string> _excludedDirectories = new List<string>();
        private readonly List<string> _excludedExtensions = new List<string>();
        private readonly List<string> _includedDirectories = new List<string>();
        private readonly List<string> _includedExtensions = new List<string>();

        public ISearchFilter ExcludeDirectories(string[] directories)
        {
            if (directories.Any(x => x.IsNullOrEmpty()))
                throw new InvalidDirectoryFilterException();
            _excludedDirectories.AddStrings(directories);
            _includedDirectories.RemoveAll(x => _excludedDirectories.Contains(x));
            return this;
        }

        public ISearchFilter ExcludeDirectory(string directory)
        {
            if (directory.IsNullOrEmpty()) throw new InvalidDirectoryFilterException();
            if (!_excludedDirectories.Contains(directory)) _excludedDirectories.Add(directory);
            _includedExtensions.Remove(directory);
            return this;
        }

        public ISearchFilter ExcludeExtension(string extension)
        {
            if (extension.IsNullOrEmpty()) throw new InvalidFileExtensionFilterException();
            if (!_excludedExtensions.Contains(extension)) _excludedExtensions.Add(extension);
            _includedExtensions.Remove(extension);
            return this;
        }

        public ISearchFilter ExcludeExtensions(string[] extensions)
        {
            if (extensions.Any(x => x.IsNullOrEmpty()))
                throw new InvalidFileExtensionFilterException();
            _excludedExtensions.AddStrings(extensions);
            _includedExtensions.RemoveAll(x => _excludedExtensions.Contains(x));
            return this;
        }

        public ISearchFilter IncludeDirectories(string[] directories)
        {
            if (directories.Any(x => x.IsNullOrEmpty()))
                throw new InvalidDirectoryFilterException();
            _includedDirectories.AddStrings(directories);
            _excludedDirectories.RemoveAll(x => _includedDirectories.Contains(x));
            return this;
        }

        public ISearchFilter IncludeDirectory(string directory)
        {
            if (directory.IsNullOrEmpty()) throw new InvalidDirectoryFilterException();
            if (!_includedDirectories.Contains(directory)) _includedDirectories.Add(directory);
            _excludedDirectories.Remove(directory);
            return this;
        }

        public ISearchFilter IncludeExtension(string extension)
        {
            if (extension.IsNullOrEmpty()) throw new InvalidFileExtensionFilterException();
            if (!_includedExtensions.Contains(extension)) _includedExtensions.Add(extension);
            _excludedExtensions.Remove(extension);
            return this;
        }

        public ISearchFilter IncludeExtensions(string[] extensions)
        {
            if (extensions.Any(x => x.IsNullOrEmpty()))
                throw new InvalidFileExtensionFilterException();
            _includedExtensions.AddStrings(extensions);
            _excludedExtensions.RemoveAll(x => _includedExtensions.Contains(x));
            return this;
        }

        public IEnumerable<string> Match(IEnumerable<string> files) =>
            (from file in files
                let directory = Path.GetDirectoryName(file)
                let extension = Path.GetExtension(file)
                let matched = IsDirectoryMatched(directory) && IsExtensionMatched(extension)
                where matched
                select file).ToList();

        private bool IsDirectoryMatched(string directory)
        {
            var isIncluded =
                !_includedDirectories.Any() || _includedDirectories.Contains(directory);
            return isIncluded && !_excludedDirectories.Contains(directory);
        }

        private bool IsExtensionMatched(string extension)
        {
            var isIncluded = !_includedExtensions.Any() || _includedExtensions.Contains(extension);
            return isIncluded && !_excludedExtensions.Contains(extension);
        }
    }
}