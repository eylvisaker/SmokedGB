using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class ShiftRightIntoCarryATest : CpuTest
    {
        public ShiftRightIntoCarryATest()
        {
            PrepareOpCode(GameboyCpu.OpCodeCB.SRL_A);
        }

        [Fact]
        public void SRL_A_1()
        {
            A = 1;

            cpu.Step();

            A.Should().Be(0);
            VerifyFlags(H: false, C: true, Z: true, N: false);
        }

        [Fact]
        public void SRL_A_128()
        {
            A = 128;

            cpu.Step();

            A.Should().Be(64);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }


        [Fact]
        public void SRL_A_255()
        {
            A = 255;

            cpu.Step();

            A.Should().Be(127);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }
    }
}
