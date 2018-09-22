using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class PopAFTest : CpuTest
    {
        public PopAFTest()
        {
            SP = 0xd000;
            PrepareOpCode(GameboyCpu.OpCode.POP_AF);
        }

        [Fact]
        public void PopAF()
        {
            memory[SP] = 0x00;
            memory[SP + 1] = 0x12;

            cpu.Step();

            AF.Should().Be(0x1200);
        }

        [Fact]
        public void PopAFClearF()
        {
            memory[SP] = 0xFF;
            memory[SP + 1] = 0x12;

            cpu.Step();

            AF.Should().Be(0x12F0);
        }
    }
}
