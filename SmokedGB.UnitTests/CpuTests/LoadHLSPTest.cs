using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.CpuTests
{
    [TestClass]
    public class LoadHLSPTest : CpuTest
    {
        [TestInitialize]
        public void Initialize()
        {
            SP = 0x2000;
        }

        [TestMethod]
        public void LD_HL_SP_Positive()
        {
            PrepareOpCode(GameboyCpu.OpCode.LDHL_SP_n, 0x0A);

            cpu.Step();

            Assert.AreEqual(0x200A, HL);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }


        [TestMethod]
        public void LD_HL_SP_Negative()
        {
            PrepareOpCode(GameboyCpu.OpCode.LDHL_SP_n, 0xFF);

            cpu.Step();

            Assert.AreEqual(0x1FFF, HL);
            VerifyFlags(H: false, C: false, Z: false, N: false);
        }
    }
}
