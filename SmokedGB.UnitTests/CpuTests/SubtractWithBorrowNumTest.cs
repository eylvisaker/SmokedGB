using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmokedGB.UnitTests.CpuTests
{
    public class SubtractWithBorrowNumTest : CpuTest
    {
        public SubtractWithBorrowNumTest()
        {
            Flag_C = false;
        }

        [Fact]
        public void SBC_A_n_Zero()
        {
            PrepareOpCode(GameboyCpu.OpCode.SBC_n, 0);

            A = 0x10;

            cpu.Step();

            Assert.AreEqual(0x10, A);
            VerifyFlags(H: false, C: false, Z: false, N: true);
        }

        [Fact]
        public void SBC_A_n_SmallWithoutBorrow()
        {
            PrepareOpCode(GameboyCpu.OpCode.SBC_n, 3);

            A = 0x07;

            cpu.Step();

            Assert.AreEqual(0x4, A);
            VerifyFlags(H: false, C: false, Z: false, N: true);
        }

        [Fact]
        public void SBC_A_n_SmallWithBorrow()
        {
            PrepareOpCode(GameboyCpu.OpCode.SBC_n, 3);

            Flag_C = true;
            A = 0x07;

            cpu.Step();

            Assert.AreEqual(0x3, A);
            VerifyFlags(H: false, C: false, Z: false, N: true);
        }


        [Fact]
        public void SBC_A_n_ToNegativeWithoutBorrow()
        {
            PrepareOpCode(GameboyCpu.OpCode.SBC_n, 3);

            A = 0x1;

            cpu.Step();

            Assert.AreEqual(0xFE, A);
            VerifyFlags(H: true, C: true, Z: false, N: true);
        }

        [Fact]
        public void SBC_A_n_ToNegativeWithBorrow()
        {
            PrepareOpCode(GameboyCpu.OpCode.SBC_n, 3);

            A = 0x1;
            Flag_C = true;

            cpu.Step();

            Assert.AreEqual(0xFD, A);
            VerifyFlags(H: true, C: true, Z: false, N: true);
        }

        [Fact]
        public void SBC_A_n_HalfBorrow()
        {
            PrepareOpCode(GameboyCpu.OpCode.SBC_n, 0xf);

            A = 0xe;

            cpu.Step();

            Assert.AreEqual(0xFF, A);
            VerifyFlags(H: true, C: true, Z: false, N: true);
        }

        [Fact]
        public void SBC_A_n_HalfBorrowWithBorrow()
        {
            PrepareOpCode(GameboyCpu.OpCode.SBC_n, 0xf);

            A = 0xf;
            Flag_C = true;

            cpu.Step();

            Assert.AreEqual(0xFF, A);
            VerifyFlags(H: true, C: true, Z: false, N: true);
        }

        [Fact]
        public void SBC_A_n_ByteMax()
        {
            PrepareOpCode(GameboyCpu.OpCode.SBC_n, 0xff);

            A = 0xe;
            Flag_C = true;

            cpu.Step();

            Assert.AreEqual(0xe, A);
            VerifyFlags(H: true, C: true, Z: false, N: true);
        }
    }
}

