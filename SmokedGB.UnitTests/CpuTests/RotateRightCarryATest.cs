using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class RotateRightCarryATest : CpuTest
    {
        public RotateRightCarryATest()
        {
            PrepareOpCode(GameboyCpu.OpCode.RRCA);
        }

        [Fact]
        public void RRCA_NoCarry()
        {
            Flag_C = false;
            A = 0x22;

            cpu.Step();

            A.Should().Be(0x11);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [Fact]
        public void RRCA_Carry()
        {
            Flag_C = false;
            A = 0x11;

            cpu.Step();

            A.Should().Be(0x88);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void RRCA_WithCarry()
        {
            Flag_C = false;
            A = 1;

            cpu.Step();
            A.Should().Be(128);

            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void RRCA_Zero()
        {
            Flag_C = true;
            A = 0;

            cpu.Step();

            A.Should().Be(0);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }
    }
}
