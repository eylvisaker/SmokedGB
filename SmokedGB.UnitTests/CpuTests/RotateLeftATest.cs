using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class RotateLeftATest : CpuTest
    {
        public RotateLeftATest()
        {
            PrepareOpCode(GameboyCpu.OpCode.RLA);
        }

        [Fact]
        public void RLANoCarry()
        {
            Flag_C = false;
            A = 0x22;

            cpu.Step();

            A.Should().Be(0x44);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [Fact]
        public void RLACarry()
        {
            Flag_C = false;
            A = 0x88;

            cpu.Step();

            A.Should().Be(0x10);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void RLAZero()
        {
            Flag_C = false;
            A = 0x80;

            cpu.Step();
            A.Should().Be(0);

            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void RLAWithCarry()
        {
            Flag_C = true;
            A = 0;

            cpu.Step();

            A.Should().Be(0x01);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }
    }
}
