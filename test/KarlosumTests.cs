using System.IO;
using System.Reflection;
using System;
using Xunit;
using Karlosum;
using static System.Environment;

namespace test
{
    public class KarlosumUnitTest
    {
        private string root;
        private string testDir;

        public KarlosumUnitTest()
        {
            root = "../../../";
            testDir = Path.Join(root, "testdir");
        }



        [Fact]
        public void Basic()
        {
            byte[] bs = { 0, 0 };
            Assert.Equal("0000", bs.ToHexadecimalString());

        }

        [Theory]
        [InlineData("harmless.txt")]
        [InlineData("executable.exe")]
        [InlineData("recursive/deeper_file.dat")]
        public void TestFilesExist(string filename)
        {
            string path = Path.Join(testDir, "files", filename);
            Assert.True(File.Exists(path));
        }

        [Fact]
        public void TestGeneratedName()
        {
            Assert.Equal(
                $"{Environment.MachineName}.txt",
                Extensions.GenerateOutputFileName()
            );
        }

    }
}
