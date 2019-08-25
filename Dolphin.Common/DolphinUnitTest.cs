using System.IO;
using NUnit.Framework;

namespace Dolphin.Common
{
    public class DolphinUnitTest
    {
        private static readonly string BasePath = TestContext.CurrentContext.TestDirectory;

        protected string GetAbsoluteTestFilePath(string relativePath) =>
            Path.Combine(BasePath, relativePath);
    }
}