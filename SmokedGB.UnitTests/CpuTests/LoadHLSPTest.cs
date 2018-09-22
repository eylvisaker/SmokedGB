using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class LoadHLSPTest : CpuTest
    {
        public LoadHLSPTest()
        {
            SP = 0x2000;
        }

        [Fact]
        public void LD_HL_SP_Positive()
        {
            PrepareOpCode(GameboyCpu.OpCode.LDHL_SP_n, 0x0A);

            cpu.Step();

            HL.Should().Be(0x200A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }


        [Fact]
        public void LD_HL_SP_Negative()
        {
            PrepareOpCode(GameboyCpu.OpCode.LDHL_SP_n, 0xFF);

            cpu.Step();

            HL.Should().Be(0x1FFF);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }
    }
}
