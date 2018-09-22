using FluentAssertions;
using Xunit;

namespace SmokedGB.UnitTests
{
    public class MemoryTest
    {
        [Fact]
        public void WriteShortTest()
        {
            byte[] buffer = new byte[50000];
            Rom rom = new Rom(buffer, null, "test.gb");
            rom.MemoryController.WriteShort(0x8000, 0x1234);

            rom.MemoryController[0x8000].Should().Be(0x34);
            rom.MemoryController[0x8001].Should().Be(0x12);
        }
    }
}
