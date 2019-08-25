using System.Collections.Generic;

namespace Dolphin.IO
{
    public interface ISearchFilter
    {
        ISearchFilter ExcludeDirectories(string[] directories);
        ISearchFilter ExcludeDirectory(string directory);
        ISearchFilter ExcludeExtension(string extension);
        ISearchFilter ExcludeExtensions(string[] extensions);
        ISearchFilter IncludeDirectories(string[] directories);
        ISearchFilter IncludeDirectory(string directory);
        ISearchFilter IncludeExtension(string extension);
        ISearchFilter IncludeExtensions(string[] extensions);
        IEnumerable<string> Match(IEnumerable<string> files);
    }
}