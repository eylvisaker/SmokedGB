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
        public void DecimalAdjust_15_27()
        {
            A = 0x15 + 0x27;
            Flag_C = false;
            Flag_H = false;
            Flag_N = false;

            cpu.Step();

            Assert.AreEqual(0x42, A);
        }

        [Ignore]
        [TestMethod]
        public void DecimalAdjust_15_27_C()
        {
            A = 0x15 + 0x27;
            Flag_C = true;
            Flag_H = false;
            Flag_N = false;

            cpu.Step();

            Assert.AreEqual(0x42, A);
        }

    }
}
