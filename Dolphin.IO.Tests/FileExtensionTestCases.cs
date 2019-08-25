using System.Collections;

namespace Dolphin.IO.Tests
{
    public static class FileExtensionTestCases
    {
        public static IEnumerable 無效的檔案名稱字元陣列
        {
            get
            {
                yield return new[] {"\\"};
                yield return new[] {"//"};
                yield return new[] {":"};
                yield return new[] {"*"};
                yield return new[] {"?"};
                yield return new[] {"<"};
                yield return new[] {">"};
                yield return new[] {"|"};
                yield return new[] {"\""};
            }
        }

        public static IEnumerable 有效的副檔名列表() => new[] {".exe", ".txt", ".dll"};

        public static IEnumerable 有效的副檔名陣列() => new object[] {有效的副檔名列表()};

        public static IEnumerable 無效的檔案名稱字元列表() => new object[]
            {"\\", "//", ":", "*", "?", "\"", "<", ">", "|"};
    }
}