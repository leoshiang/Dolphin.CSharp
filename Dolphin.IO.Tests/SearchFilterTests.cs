using NUnit.Framework;

namespace Dolphin.IO.Tests
{
    [TestFixture]
    public class SearchFilterTests
    {
        [Test]
        public void 分次加入目錄條件_不符合條件的目錄應該不接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeDirectory(DirectoryTestCases.目錄列表[0]);
            filter.IncludeDirectory(DirectoryTestCases.目錄列表[1]);
            filter.IncludeDirectory(DirectoryTestCases.目錄列表[2]);

            // Act
            var result = filter.Match(new[] {"bin"});

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void 分次加入目錄條件_符合條件的目錄應該被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeDirectory(@"C:\contents");
            filter.IncludeDirectory(@"C:\doc");
            filter.IncludeDirectory(@"C:\scripts");

            // Act
            var result = filter.Match(new[] {@"C:\contents\test.dat"});

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void 分次加入副檔名條件_不符合條件的附檔名應該不接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeExtension(".exe");
            filter.IncludeExtension(".bat");
            filter.IncludeExtension(".cmd");

            // Act
            var result = filter.Match(new[] {"file.xxx"});

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void 分次加入副檔名條件_符合條件的附檔名應該被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeExtension(".exe");
            filter.IncludeExtension(".bat");
            filter.IncludeExtension(".cmd");

            // Act
            var result = filter.Match(new[] {"file.exe"});

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void 加入空的目錄排除條件_應該拋出例外()
        {
            Assert.Throws<InvalidDirectoryFilterException>(() =>
            {
                var filter = new SearchFilter();
                filter.ExcludeDirectory(string.Empty);
            });
        }

        [Test]
        public void 加入空的目錄接受條件_應該拋出例外()
        {
            Assert.Throws<InvalidDirectoryFilterException>(() =>
            {
                var filter = new SearchFilter();
                filter.IncludeDirectory(string.Empty);
            });
        }

        [Test]
        public void 加入空的副檔名排除條件_應該拋出例外()
        {
            Assert.Throws<InvalidFileExtensionFilterException>(() =>
            {
                var filter = new SearchFilter();
                filter.ExcludeExtension(string.Empty);
            });
        }

        [Test]
        public void 加入空的副檔名接受條件_應該拋出例外()
        {
            Assert.Throws<InvalidFileExtensionFilterException>(() =>
            {
                var filter = new SearchFilter();
                filter.IncludeExtension(string.Empty);
            });
        }

        [Test]
        public void 目錄先加入排除條件再加入接受條件_符合條件的目錄應該被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.ExcludeDirectory(@"C:\contents");
            filter.IncludeDirectory(@"C:\contents");

            // Act
            var result = filter.Match(new[] {@"C:\contents\test.dat"});

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void 目錄先加入接受條件再加入排除條件_符合條件的目錄應該不被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeDirectory(@"C:\contents");
            filter.ExcludeDirectory(@"C:\contents");

            // Act
            var result = filter.Match(new[] {@"C:\contents\test.dat"});

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void 目錄先批次加入排除條件再加入接受條件_符合條件的目錄應該被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.ExcludeDirectories(new[] {@"C:\contents", @"C:\doc"});
            filter.IncludeDirectories(new[] {@"C:\contents", @"C:\doc"});

            // Act
            var result = filter.Match(new[] {@"C:\contents\test.dat"});

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void 目錄先批次加入接受條件再加入排除條件_符合條件的目錄應該不被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeDirectories(new[] {@"C:\contents", @"C:\doc"});
            filter.ExcludeDirectories(new[] {@"C:\contents", @"C:\doc"});

            // Act
            var result = filter.Match(new[] {@"C:\contents\test.dat"});

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void 批次加入多個目錄接受條件_不符合條件的目錄應該不接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeDirectories(new[] {"doc", "scripts", "contents"});

            // Act
            var result = filter.Match(new[] {@"bin\test.data"});

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void 批次加入多個目錄接受條件_符合條件的目錄應該被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeDirectories(new[] {@"C:\doc", @"C:\scripts", @"C:\contents"});

            // Act
            var result = filter.Match(new[] {@"C:\doc\test.txt"});

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void 批次加入多個副檔名接受條件_符合條件的附檔名應該被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeExtensions(new[] {".exe", ".bat", ".cmd"});

            // Act
            var result = filter.Match(new[] {"file.exe"});

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void 批次加入空的目錄排除條件_應該拋出例外()
        {
            Assert.Throws<InvalidDirectoryFilterException>(() =>
            {
                var filter = new SearchFilter();
                filter.ExcludeDirectories(new[] {string.Empty, @"C:\scripts"});
            });
        }

        [Test]
        public void 批次加入空的目錄接受條件_應該拋出例外()
        {
            Assert.Throws<InvalidDirectoryFilterException>(() =>
            {
                var filter = new SearchFilter();
                filter.IncludeDirectories(new[] {string.Empty, @"C:\scripts"});
            });
        }

        [Test]
        public void 批次加入空的副檔名排除條件_應該拋出例外()
        {
            Assert.Throws<InvalidFileExtensionFilterException>(() =>
            {
                var filter = new SearchFilter();
                filter.ExcludeExtensions(new[] {string.Empty, ".exe"});
            });
        }

        [Test]
        public void 批次加入副檔名條件_只要有一個副檔名條件是空的_應該拋出例外()
        {
            Assert.Throws<InvalidFileExtensionFilterException>(() =>
            {
                var filter = new SearchFilter();
                filter.IncludeExtensions(new[] {"exe", "doc", string.Empty});
            });
        }

        [Test]
        public void 副檔名條件先加入排除條件再加入接受條件_符合條件的副檔名應該被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.ExcludeExtension(".exe");
            filter.IncludeExtension(".exe");

            // Act
            var result = filter.Match(new[] {"file.exe"});

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void 副檔名條件先加入接受條件再加入排除條件_符合條件的副檔名應該不被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeExtension(".exe");
            filter.ExcludeExtension(".exe");

            // Act
            var result = filter.Match(new[] {"file.exe"});

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void 副檔名條件先批次加入接受條件再加入排除條件_符合條件的副檔名應該不被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeExtensions(new[] {".exe", ".bat", ".cmd"});
            filter.ExcludeExtensions(new[] {".exe", ".bat", ".cmd"});

            // Act
            var result = filter.Match(new[] {"file.exe"});

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void 副檔名條件批次加入排除條件再加入接受條件_符合條件的副檔名應該被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.ExcludeExtensions(new[] {".exe", ".doc"});
            filter.IncludeExtensions(new[] {".exe", ".doc"});

            // Act
            var result = filter.Match(new[] {"file.exe"});

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void 副檔名條件批次加入接受條件再加入排除條件_符合條件的副檔名應該不被接受()
        {
            // Arrange
            var filter = new SearchFilter();
            filter.IncludeExtensions(new[] {".exe", ".doc"});
            filter.ExcludeExtensions(new[] {".exe", ".doc"});

            // Act
            var result = filter.Match(new[] {"file.exe"});

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        [TestCaseSource(typeof(FileExtensionTestCases), nameof(FileExtensionTestCases.有效的副檔名列表))]
        public void 當沒有條件時_任何副檔名都應該被接受(string extension)
        {
            // Arrange
            var filter = new SearchFilter();

            // Act
            var result = filter.Match(new[] {$"filename.{extension}"});

            // Assert
            Assert.That(result, Is.Not.Empty);
        }
    }
}