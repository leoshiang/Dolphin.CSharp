using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace Dolphin.IO
{
    public class FileEnumerator : IFileEnumerator
    {
        private readonly IFileSystem _fileSystem;

        public FileEnumerator(IFileSystem fileSystem) => _fileSystem = fileSystem;

        public IEnumerable<string> FindFiles(string directory, ISearchFilter filter)
        {
            var files = FindCandidateFiles(directory);
            return filter.Match(files);
        }

        private IEnumerable<string> FindCandidateFiles(string directory) =>
            _fileSystem.Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories);
    }
}