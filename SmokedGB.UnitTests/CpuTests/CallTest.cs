using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class CallTest : CpuTest
    {
        public CallTest()
        {
            PC = 0x0100;
            SP = 0xdfff;
        }

        [Fact]
        public void Call()
        {
            PrepareOpCode(GameboyCpu.OpCode.CALL_nn, 0x23, 0x34);

            cpu.Step();

            PC.Should().Be(0x3423, "Program counter did not jump to the right location.");
            PeekStack16().Should().Be(0x0103);
        }

        [Fact]
        public void CallNZ_Success()
        {
            Flag_Z = false;

            PrepareOpCode(GameboyCpu.OpCode.CALL_NZ_nn, 0x23, 0x34);

            cpu.Step();

            PC.Should().Be(0x3423, "Program counter did not jump to the right location.");
            PeekStack16().Should().Be(0x0103);
        }

        [Fact]
        public void CallNZ_Fail()
        {
            Flag_Z = true;

            PrepareOpCode(GameboyCpu.OpCode.CALL_NZ_nn, 0x23, 0x34);

            cpu.Step();

            PC.Should().Be(0x0103, "Program counter jumped but should not have.");
            SP.Should().Be(0xdfff, "Stack pointer was moved but should not have been.");
        }

        [Fact]
        public void CallZ_Success()
        {
            Flag_Z = true;

            PrepareOpCode(GameboyCpu.OpCode.CALL_Z_nn, 0x23, 0x34);

            cpu.Step();

            PC.Should().Be(0x3423, "Program counter did not jump to the right location.");
            PeekStack16().Should().Be(0x0103);
        }

        [Fact]
        public void CallZ_Fail()
        {
            Flag_Z = false;

            PrepareOpCode(GameboyCpu.OpCode.CALL_Z_nn, 0x23, 0x34);

            cpu.Step();

            PC.Should().Be(0x0103, "Program counter jumped but should not have.");
            SP.Should().Be(0xdfff, "Stack pointer was moved but should not have been.");
        }

        [Fact]
        public void CallNC_Success()
        {
            Flag_C = false;

            PrepareOpCode(GameboyCpu.OpCode.CALL_NC_nn, 0x23, 0x34);

            cpu.Step();

            PC.Should().Be(0x3423, "Program counter did not jump to the right location.");
            PeekStack16().Should().Be(0x0103);
        }

        [Fact]
        public void CallNC_Fail()
        {
            Flag_C = true;

            PrepareOpCode(GameboyCpu.OpCode.CALL_NC_nn, 0x23, 0x34);

            cpu.Step();

            PC.Should().Be(0x0103, "Program counter jumped but should not have.");
            SP.Should().Be(0xdfff, "Stack pointer was moved but should not have been.");
        }

        [Fact]
        public void CallC_Success()
        {
            Flag_C = true;

            PrepareOpCode(GameboyCpu.OpCode.CALL_C_nn, 0x23, 0x34);

            cpu.Step();

            PC.Should().Be(0x3423, "Program counter did not jump to the right location.");
            PeekStack16().Should().Be(0x0103);
        }

        [Fact]
        public void CallC_Fail()
        {
            Flag_C = false;

            PrepareOpCode(GameboyCpu.OpCode.CALL_C_nn, 0x23, 0x34);

            cpu.Step();

            PC.Should().Be(0x0103, "Program counter jumped but should not have.");
            SP.Should().Be(0xdfff, "Stack pointer was moved but should not have been.");
        }
    }
}
