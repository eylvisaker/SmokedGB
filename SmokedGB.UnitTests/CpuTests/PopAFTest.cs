using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class PopAFTest : CpuTest
    {
        [TestInitialize]
        public void Init()
        {
            SP = 0xd000;
            PrepareOpCode(GameboyCpu.OpCode.POP_AF);
        }

        [TestMethod]
        public void PopAF()
        {
            memory[SP] = 0x00;
            memory[SP + 1] = 0x12;

            cpu.Step();

            Assert.AreEqual(0x1200, AF);
        }

        [TestMethod]
        public void PopAFClearF()
        {
            memory[SP] = 0xFF;
            memory[SP + 1] = 0x12;

            cpu.Step();

            Assert.AreEqual(0x12F0, AF);
        }
    }
}
