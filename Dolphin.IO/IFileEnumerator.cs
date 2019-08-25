using System.Collections.Generic;

namespace Dolphin.IO
{
    public interface IFileEnumerator
    {
        IEnumerable<string> FindFiles(string directory, ISearchFilter filter);
    }
}