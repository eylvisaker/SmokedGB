using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class AddTest : CpuTest
    {
        [TestMethod]
        public void Add_b()
        {
            registers.A = 1;
            registers.B = 2;

            SetNextOpCode(GameboyCpu.OpCode.ADD_B);

            cpu.Step();

            Assert.AreEqual(3, registers.A);
            Assert.IsFalse(registers.Flag_H);
        }

    }
}
