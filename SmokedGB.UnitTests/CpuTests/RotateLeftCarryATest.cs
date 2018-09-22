using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class RotateLeftCarryATest : CpuTest
    {
        public RotateLeftCarryATest()
        {
            PrepareOpCode(GameboyCpu.OpCode.RLCA);
        }

        [Fact]
        public void RLCA_NoCarry()
        {
            Flag_C = false;
            A = 0x22;

            cpu.Step();

            A.Should().Be(0x44);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [Fact]
        public void RLCA_Carry()
        {
            Flag_C = false;
            A = 0x88;

            cpu.Step();

            A.Should().Be(0x11);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void RLCA_WithCarry()
        {
            Flag_C = false;
            A = 0x80;

            cpu.Step();
            A.Should().Be(1);

            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void RLCA_Zero()
        {
            Flag_C = true;
            A = 0;

            cpu.Step();

            A.Should().Be(0);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }
    }
}
