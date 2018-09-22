using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class AddSPTest : CpuTest
    {
        public AddSPTest()
        {
            Flag_H = false;
            Flag_C = false;

            PrepareOpCode(GameboyCpu.OpCode.ADD_SP_n, 0x78);
        }

        [Fact]
        public void AddSP()
        {
            SP = 0x500;

            cpu.Step();

            SP.Should().Be(0x578);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [Fact]
        public void AddSP_Negative()
        {
            SP = 0x500;
            memory[PC + 1] = 0x80;

            cpu.Step();

            SP.Should().Be(0x480);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [Fact]
        public void AddSP_Carry()
        {
            SP = 0x5F0;
            memory[PC + 1] = 0x10;

            cpu.Step();

            SP.Should().Be(0x600);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void AddSP_HalfCarry()
        {
            SP = 0x50F;
            memory[PC + 1] = 0x01;

            cpu.Step();

            SP.Should().Be(0x510);
            VerifyFlags(H: true, C: false, Z: false, N: false);
        }

        [Fact]
        public void AddSP_BothCarry()
        {
            SP = 0x5FF;
            memory[PC + 1] = 0x11;

            cpu.Step();

            SP.Should().Be(0x610);
            VerifyFlags(H: true, C: true, Z: false, N: false);
        }
    }
}
