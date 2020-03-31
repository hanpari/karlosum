using System;
using Xunit;
using Karlosum;


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
    }
}
