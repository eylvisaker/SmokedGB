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

            Assert.AreEqual(0x44, A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [Fact]
        public void RLCA_Carry()
        {
            Flag_C = false;
            A = 0x88;

            cpu.Step();

            Assert.AreEqual(0x11, A);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void RLCA_WithCarry()
        {
            Flag_C = false;
            A = 0x80;

            cpu.Step();
            Assert.AreEqual(1, A);

            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [Fact]
        public void RLCA_Zero()
        {
            Flag_C = true;
            A = 0;

            cpu.Step();

            Assert.AreEqual(0, A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }
    }
}
