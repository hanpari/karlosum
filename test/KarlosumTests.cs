using System.IO;
using System.Reflection;
using System;
using Xunit;
using Karlosum;
using static System.Environment;

namespace test
{
    public class UnitTest1
    {



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
            string path = "../../../testdir/files/" + filename;
            Assert.True(File.Exists(path));
        }

    }
}
