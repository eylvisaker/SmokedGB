using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class ResetTest : CpuTest
    {
        public ResetTest()
        {
            PC = 0x0100;
            SP = 0xdfff;
        }

        void VerifyReset(GameboyCpu.OpCode opcode, ushort targetAddr)
        {
            PrepareOpCode(opcode);

            cpu.Step();

            Assert.AreEqual(targetAddr, PC, "Program counter did not jump to the right location.");
            Assert.AreEqual(0x0101, PeekStack16(), "Stack contained ${0:X4} which was not the correct return address of ${1:X4}.", PeekStack16(), 0x0103);
        }

        [Fact]
        public void Reset00()
        {
            VerifyReset(GameboyCpu.OpCode.RST_00, 0x0000);
        }

        [Fact]
        public void Reset08()
        {
            VerifyReset(GameboyCpu.OpCode.RST_08, 0x0008);
        }

        [Fact]
        public void Reset10()
        {
            VerifyReset(GameboyCpu.OpCode.RST_10, 0x0010);
        }

        [Fact]
        public void Reset18()
        {
            VerifyReset(GameboyCpu.OpCode.RST_18, 0x0018);
        }

        [Fact]
        public void Reset20()
        {
            VerifyReset(GameboyCpu.OpCode.RST_20, 0x0020);
        }

        [Fact]
        public void Reset28()
        {
            VerifyReset(GameboyCpu.OpCode.RST_28, 0x0028);
        }

        [Fact]
        public void Reset30()
        {
            VerifyReset(GameboyCpu.OpCode.RST_30, 0x0030);
        }

        [Fact]
        public void Reset38()
        {
            VerifyReset(GameboyCpu.OpCode.RST_38, 0x0038);
        }

    }
}
