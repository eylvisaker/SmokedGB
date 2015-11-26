using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class AddBTest : CpuTest
    {
        [TestInitialize]
        public void Initialize()
        {
            PrepareNextOpCode(GameboyCpu.OpCode.ADD_B);
        }

        public void VerifyFlags(bool? H = null, bool? C = null, bool? Z = null, bool? N = null)
        {
            if (H != null) Assert.AreEqual(H, registers.Flag_H, "Flag H was not expected value.");
            if (C != null) Assert.AreEqual(C, registers.Flag_C, "Flag C was not expected value.");
            if (Z != null) Assert.AreEqual(Z, registers.Flag_Z, "Flag Z was not expected value.");
            if (N != null) Assert.AreEqual(N, registers.Flag_N, "Flag N was not expected value.");
        }

        [TestMethod]
        public void AddB()
        {
            registers.A = 1;
            registers.B = 2;

            cpu.Step();

            Assert.AreEqual(3, registers.A);
            VerifyFlags(false, false, false, false);
        }

        [TestMethod]
        public void AddBHalfCarry()
        {
            registers.A = 8;
            registers.B = 8;

            cpu.Step();

            Assert.AreEqual(16, registers.A);
            VerifyFlags(true, false, false, false);
        }

        [TestMethod]
        public void AddBLargeNoCarry()
        {
            registers.A = 0x11;
            registers.B = 0x22;

            cpu.Step();

            Assert.AreEqual(0x33, registers.A);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }

        [TestMethod]
        public void AddBFullCarryAndHalfCarry()
        {
            registers.A = 0xFF;
            registers.B = 0x19;

            cpu.Step();

            Assert.AreEqual(0x18, registers.A);
            VerifyFlags(H: true, C: true, Z: false, N: false);
        }
        [TestMethod]
        public void AddBFullCarryNoHalfCarry()
        {
            registers.A = 0xF0;
            registers.B = 0x20;

            cpu.Step();

            Assert.AreEqual(0x10, registers.A);
            VerifyFlags(H: false, C: true, Z: false, N: false);
        }

        [TestMethod]
        public void AddBZero()
        {
            registers.A = 0xFA;
            registers.B = 6;

            cpu.Step();

            Assert.AreEqual(0, registers.A);
            VerifyFlags(H: true, C: true, Z: true, N: false);
        }
    }
}
