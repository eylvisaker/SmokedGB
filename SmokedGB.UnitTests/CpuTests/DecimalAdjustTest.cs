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

        [TestMethod]
        public void DecimalAdjust_15()
        {
            A = 0x0f;
            Flag_C = false;
            Flag_H = false;
            Flag_N = false;

            cpu.Step();

            Assert.AreEqual(0x15, A);
        }

        [TestMethod]
        public void DecimalAdjust_16()
        {
            A = 0x10;
            Flag_C = false;
            Flag_H = true;
            Flag_N = false;

            cpu.Step();

            Assert.AreEqual(0x16, A);
        }

        [TestMethod]
        public void DecimalAdjust_70_C()
        {
            A = 0x10;
            Flag_C = true;
            Flag_H = false;
            Flag_N = false;

            cpu.Step();

            Assert.AreEqual(0x70, A);
        }

        [TestMethod]
        public void DecimalAdjust_N_90()
        {
            A = 0xf0;
            Flag_C = true;
            Flag_H = false;
            Flag_N = true;

            cpu.Step();

            Assert.AreEqual(0x90, A);
            VerifyFlags(H: false, C: true, Z: false, N: true);
        }

        [TestMethod]
        public void DecimalAdjust_N_8()
        {
            A = 0x0e;
            Flag_C = false;
            Flag_H = true;
            Flag_N = true;

            cpu.Step();

            Assert.AreEqual(0x08, A);
            VerifyFlags(H: false, C: false, Z: false, N: true);
        }
    }
}
