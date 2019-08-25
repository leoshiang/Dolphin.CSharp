using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;

namespace Dolphin.Dropbox.Tests
{
    [TestFixture]
    public class DropboxTests
    {
        [Test]
        public void 讀取個人資料目錄應該成功()
        {
            // Arrange
            var home = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"c:\myfile.txt", new MockFileData("Testing is meh.")},
                {@"c:\demo\jQuery.js", new MockFileData("some js")},
                {@"c:\demo\image.gif", new MockFileData(new byte[] {0x12, 0x34, 0x56, 0xd2})}
            });

            var dropbox = new Dropbox(fileSystem);

            var personalDirectory = dropbox.GetPersonalDirectory();
            Console.WriteLine(personalDirectory);
            Assert.That(personalDirectory, Is.Not.Empty);
        }
    }
}