using System.Net;
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
        private readonly string root;
        private readonly string testDir;
        private readonly string outputDir;
        private readonly string inputDir;

        private readonly string outputFile;

        public KarlosumUnitTest()
        {
            root = "../../../";
            testDir = Path.Join(root, "testdir");
            outputDir = Path.Join(testDir, "output");
            inputDir = Path.Join(testDir, "input");
            outputFile = Path.Join(outputDir, Extensions.GenerateOutputFileName());

            // We have to delete the existing result file
            // or we'll get an exception.
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }




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
            string path = Path.Join(inputDir, filename);
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

        [Fact]
        public void TestCLIFunctionality()
        {
        }

    }
}
