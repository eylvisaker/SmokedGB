using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class ShiftRightIntoCarryATest : CpuTest
    {
        [TestInitialize]
        public void Init()
        {
            PrepareOpCode(GameboyCpu.OpCodeCB.SRL_A);
        }

        [TestMethod]
        public void SRL_A_1()
        {
            A = 1;

            cpu.Step();

            Assert.AreEqual(0, A);
            VerifyFlags(H: false, C: true, Z: true, N: false);
        }

        [TestMethod]
        public void SRL_A_128()
        {
            A = 128;

            cpu.Step();

            Assert.AreEqual(64, A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }


        [TestMethod]
        public void SRL_A_255()
        {
            A = 255;

            cpu.Step();

            Assert.AreEqual(127, A);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }
    }
}
