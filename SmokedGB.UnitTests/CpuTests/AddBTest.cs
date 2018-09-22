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

            Assert.AreEqual(3, A);
            VerifyFlags(false, false, false, false);
        }

        [Fact]
        public void AddBHalfCarry()
        {
            A = 8;
            B = 8;

            cpu.Step();

            Assert.AreEqual(16, A);
            VerifyFlags(true, false, false, false);
        }

        [Fact]
        public void AddBLargeNoCarry()
        {
            registers.A = 0x11;
            registers.B = 0x22;

            cpu.Step();

            Assert.AreEqual(0x33, registers.A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [Fact]
        public void AddBFullCarryAndHalfCarry()
        {
            registers.A = 0xFF;
            registers.B = 0x19;

            cpu.Step();

            Assert.AreEqual(0x18, registers.A);
            VerifyFlags(H: true, C: true, Z: false, N: false);
        }
        [Fact]
        public void AddBFullCarryNoHalfCarry()
        {
            registers.A = 0xF0;
            registers.B = 0x20;

            cpu.Step();

            Assert.AreEqual(0x10, registers.A);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void AddBZero()
        {
            registers.A = 0xFA;
            registers.B = 6;

            cpu.Step();

            Assert.AreEqual(0, registers.A);
            VerifyFlags(H: true, C: true, Z: true, N: false);
        }
    }
}
