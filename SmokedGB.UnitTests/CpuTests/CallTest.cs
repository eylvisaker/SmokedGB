using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class CallTest : CpuTest
    {
        [TestInitialize]
        public void Setup()
        {
            PC = 0x0100;
            SP = 0xdfff;
        }

        [TestMethod]
        public void Call()
        {
            PrepareOpCode(GameboyCpu.OpCode.CALL_nn, 0x23, 0x34);

            cpu.Step();

            Assert.AreEqual(0x3423, PC, "Program counter did not jump to the right location.");
            Assert.AreEqual(0x0103, PeekStack16(), "Stack contained ${0:X4} which was not the correct return address of ${1:X4}.", PeekStack16(), 0x0103);
        }

        [TestMethod]
        public void CallNZ_Success()
        {
            Flag_Z = false;

            PrepareOpCode(GameboyCpu.OpCode.CALL_NZ_nn, 0x23, 0x34);

            cpu.Step();

            Assert.AreEqual(0x3423, PC, "Program counter did not jump to the right location.");
            Assert.AreEqual(0x0103, PeekStack16(), "Stack contained ${0:X4} which was not the correct return address of ${1:X4}.", PeekStack16(), 0x0103);
        }

        [TestMethod]
        public void CallNZ_Fail()
        {
            Flag_Z = true;

            PrepareOpCode(GameboyCpu.OpCode.CALL_NZ_nn, 0x23, 0x34);

            cpu.Step();

            Assert.AreEqual(0x0103, PC, "Program counter jumped but should not have.");
            Assert.AreEqual(0xdfff, SP, "Stack pointer was moved but should not have been.");
        }

        [TestMethod]
        public void CallZ_Success()
        {
            Flag_Z = true;

            PrepareOpCode(GameboyCpu.OpCode.CALL_Z_nn, 0x23, 0x34);

            cpu.Step();

            Assert.AreEqual(0x3423, PC, "Program counter did not jump to the right location.");
            Assert.AreEqual(0x0103, PeekStack16(), "Stack contained ${0:X4} which was not the correct return address of ${1:X4}.", PeekStack16(), 0x0103);
        }

        [TestMethod]
        public void CallZ_Fail()
        {
            Flag_Z = false;

            PrepareOpCode(GameboyCpu.OpCode.CALL_Z_nn, 0x23, 0x34);

            cpu.Step();

            Assert.AreEqual(0x0103, PC, "Program counter jumped but should not have.");
            Assert.AreEqual(0xdfff, SP, "Stack pointer was moved but should not have been.");
        }

        [TestMethod]
        public void CallNC_Success()
        {
            Flag_C = false;

            PrepareOpCode(GameboyCpu.OpCode.CALL_NC_nn, 0x23, 0x34);

            cpu.Step();

            Assert.AreEqual(0x3423, PC, "Program counter did not jump to the right location.");
            Assert.AreEqual(0x0103, PeekStack16(), "Stack contained ${0:X4} which was not the correct return address of ${1:X4}.", PeekStack16(), 0x0103);
        }

        [TestMethod]
        public void CallNC_Fail()
        {
            Flag_C = true;

            PrepareOpCode(GameboyCpu.OpCode.CALL_NC_nn, 0x23, 0x34);

            cpu.Step();

            Assert.AreEqual(0x0103, PC, "Program counter jumped but should not have.");
            Assert.AreEqual(0xdfff, SP, "Stack pointer was moved but should not have been.");
        }

        [TestMethod]
        public void CallC_Success()
        {
            Flag_C = true;

            PrepareOpCode(GameboyCpu.OpCode.CALL_C_nn, 0x23, 0x34);

            cpu.Step();

            Assert.AreEqual(0x3423, PC, "Program counter did not jump to the right location.");
            Assert.AreEqual(0x0103, PeekStack16(), "Stack contained ${0:X4} which was not the correct return address of ${1:X4}.", PeekStack16(), 0x0103);
        }

        [TestMethod]
        public void CallC_Fail()
        {
            Flag_C = false;

            PrepareOpCode(GameboyCpu.OpCode.CALL_C_nn, 0x23, 0x34);

            cpu.Step();

            Assert.AreEqual(0x0103, PC, "Program counter jumped but should not have.");
            Assert.AreEqual(0xdfff, SP, "Stack pointer was moved but should not have been.");
        }
    }
}
