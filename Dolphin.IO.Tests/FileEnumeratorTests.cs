using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using NUnit.Framework;

namespace Dolphin.IO.Tests
{
    [TestFixture]
    public class FileEnumeratorTests
    {
        [Test]
        public void 在檔案系統中搜尋exe應回傳檔案完整路徑()
        {
            // Arrange
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"C:\Windows\bfsvc.exe", new MockFileData("")},
                {@"C:\Windows\win.ini", new MockFileData("")},
                {@"C:\Windows\winhlp32.exe", new MockFileData("")}
            });
            var enumerator = new FileEnumerator(fileSystem);
            var filter = new SearchFilter().IncludeExtension(".exe");

            // Act
            var result = enumerator.FindFiles(@"C:\Windows", filter);

            // Assert
            Assert.IsTrue(result.Contains(@"C:\Windows\bfsvc.exe"));
        }
    }
}