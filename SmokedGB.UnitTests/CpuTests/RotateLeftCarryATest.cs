using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class RotateLeftCarryATest : CpuTest
    {
        [TestInitialize]
        public void Init()
        {
            PrepareOpCode(GameboyCpu.OpCode.RLC_A);
        }

        [TestMethod]
        public void RLCANoCarry()
        {
            Flag_C = false;
            A = 0x22;

            cpu.Step();

            Assert.AreEqual(0x44, A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [TestMethod]
        public void RLCACarry()
        {
            Flag_C = false;
            A = 0x88;

            cpu.Step();

            Assert.AreEqual(0x11, A);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void RLCAZero()
        {
            Flag_C = false;
            A = 0x80;

            cpu.Step();
            Assert.AreEqual(1, A);

            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void RLCAWithCarry()
        {
            Flag_C = true;
            A = 0;

            cpu.Step();

            Assert.AreEqual(0, A);
            VerifyFlags(H: false, C: false, Z: true, N: false);
        }
    }
}
