using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class RotateRightATest : CpuTest
    {
        [TestInitialize]
        public void Init()
        {
            PrepareOpCode(GameboyCpu.OpCode.RRA);
        }

        [TestMethod]
        public void RRA_NoCarry()
        {
            Flag_C = false;
            A = 0x22;

            cpu.Step();

            Assert.AreEqual(0x11, A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [TestMethod]
        public void RRA_Carry()
        {
            Flag_C = false;
            A = 0x11;

            cpu.Step();

            Assert.AreEqual(8, A);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void RRA_Zero()
        {
            Flag_C = false;
            A = 1;

            cpu.Step();
            Assert.AreEqual(0, A);

            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void RRA_WithCarry()
        {
            Flag_C = true;
            A = 0;

            cpu.Step();

            Assert.AreEqual(0x80, A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }
    }
}
