using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class AddBTest : CpuTest
    {
        public AddBTest()
        {
            PrepareOpCode(GameboyCpu.OpCode.ADD_B);
        }

        [Fact]
        public void AddB()
        {
            A = 1;
            B = 2;

            cpu.Step();

            A.Should().Be(3);
            VerifyFlags(false, false, false, false);
        }

        [Fact]
        public void AddBHalfCarry()
        {
            A = 8;
            B = 8;

            cpu.Step();

            A.Should().Be(16);
            VerifyFlags(true, false, false, false);
        }

        [Fact]
        public void AddBLargeNoCarry()
        {
            registers.A = 0x11;
            registers.B = 0x22;

            cpu.Step();

            registers.A.Should().Be(0x33);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [Fact]
        public void AddBFullCarryAndHalfCarry()
        {
            registers.A = 0xFF;
            registers.B = 0x19;

            cpu.Step();

            registers.A.Should().Be(0x18);
            VerifyFlags(H: true, C: true, Z: false, N: false);
        }
        [Fact]
        public void AddBFullCarryNoHalfCarry()
        {
            registers.A = 0xF0;
            registers.B = 0x20;

            cpu.Step();

            registers.A.Should().Be(0x10);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void AddBZero()
        {
            registers.A = 0xFA;
            registers.B = 6;

            cpu.Step();

            registers.A.Should().Be(0);
            VerifyFlags(H: true, C: true, Z: true, N: false);
        }
    }
}
