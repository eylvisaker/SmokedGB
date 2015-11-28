using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class AddHLBCTest : CpuTest
    {
        [TestInitialize]
        public void Init()
        {
            PrepareOpCode(GameboyCpu.OpCode.ADD_HL_BC);
        }

        [TestMethod]
        public void AddHLBC_NoCarry()
        {
            Flag_Z = false;

            HL = 0x1234;
            BC = 0x2233;

            cpu.Step();

            Assert.AreEqual(0x3467, HL);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [TestMethod]
        public void AddHLBC_HalfCarry()
        {
            Flag_Z = false;

            HL = 0x1834;
            BC = 0x2833;

            cpu.Step();

            Assert.AreEqual(0x4067, HL);
            VerifyFlags(H: true, C: false, Z: false, N: false);
        }

        [TestMethod]
        public void AddHLBC_LargeButNoHalfCarry()
        {
            Flag_Z = false;

            HL = 0x08ff;
            BC = 0x0001;

            cpu.Step();

            Assert.AreEqual(0x0900, HL);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [TestMethod]
        public void AddHLBC_FullCarry()
        {
            Flag_Z = false;

            HL = 0x8134;
            BC = 0x8233;

            cpu.Step();

            Assert.AreEqual(0x0367, HL);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void AddHLBC_FullAndHalfCarry()
        {
            Flag_Z = false;

            HL = 0x8834;
            BC = 0x8A33;

            cpu.Step();

            Assert.AreEqual(0x1267, HL);
            VerifyFlags(H: true, C: true, Z: false, N: false);
        }
    }
}
