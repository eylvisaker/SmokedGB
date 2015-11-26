using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class RotateRightCarryATest : CpuTest
    {
        [TestInitialize]
        public void Init()
        {
            PrepareNextOpCode(GameboyCpu.OpCode.RRC_A);
        }

        [TestMethod]
        public void RRCANoCarry()
        {
            Flag_C = false;
            A = 0x22;

            cpu.Step();

            Assert.AreEqual(0x11, A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [TestMethod]
        public void RRCACarry()
        {
            Flag_C = false;
            A = 0x11;

            cpu.Step();

            Assert.AreEqual(0x88, A);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void RRCAZero()
        {
            Flag_C = false;
            A = 1;

            cpu.Step();
            Assert.AreEqual(128, A);

            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void RRCAWithCarry()
        {
            Flag_C = true;
            A = 0;

            cpu.Step();

            Assert.AreEqual(0, A);
            VerifyFlags(H: false, C: false, Z: true, N: false);
        }
    }
}
