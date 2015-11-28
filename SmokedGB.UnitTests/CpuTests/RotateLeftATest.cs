using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class RotateLeftATest : CpuTest
    {
        [TestInitialize]
        public void Initialize()
        {
            PrepareOpCode(GameboyCpu.OpCode.RLA);
        }

        [TestMethod]
        public void RLANoCarry()
        {
            Flag_C = false;
            A = 0x22;

            cpu.Step();

            Assert.AreEqual(0x44, A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [TestMethod]
        public void RLACarry()
        {
            Flag_C = false;
            A = 0x88;

            cpu.Step();

            Assert.AreEqual(0x10, A);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void RLAZero()
        {
            Flag_C = false;
            A = 0x80;

            cpu.Step();
            Assert.AreEqual(0, A);

            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void RLAWithCarry()
        {
            Flag_C = true;
            A = 0;

            cpu.Step();

            Assert.AreEqual(0x01, A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }
    }
}
