using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class DecimalAdjustTest : CpuTest
    {
        [TestInitialize]
        public void Initialize()
        {
            PrepareOpCode(GameboyCpu.OpCode.DAA);
        }

        [TestMethod]
        public void DAA_1()
        {
            A = 1;

            cpu.Step();

            Assert.AreEqual(0x01, A);
            VerifyFlags(H: false, C: false, Z: false);
        }

        [TestMethod]
        public void DAA_12()
        {
            A = 12;

            cpu.Step();

            Assert.AreEqual(0x12, A);
            VerifyFlags(H: false, C: false, Z: false);
        }

        [TestMethod]
        public void DAA_98()
        {
            A = 98;

            cpu.Step();

            Assert.AreEqual(0x98, A);
            VerifyFlags(H: false, C: false, Z: false);
        }

        [TestMethod]
        public void DAA_100()
        {
            A = 100;

            cpu.Step();

            Assert.AreEqual(0, A);
            VerifyFlags(Z: true, H: false, C: true);
        }
    }
}
