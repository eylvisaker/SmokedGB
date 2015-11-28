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
            PC = 0x100;
            PrepareOpCode(GameboyCpu.OpCode.DAA);
        }

        int ToBcd(int value)
        {
            int result = 0;
            int v = value;
            int digits = 0;

            while (v > 0)
            {
                int d = v % 10;
                d <<= 4 * digits;

                result |= d;
                v /= 10;
                digits++;
            }

            return result;
        }

        [TestMethod]
        public void DecimalAdjust_AllAddition()
        {
            StringBuilder failure = new StringBuilder();

            for(int i = 0; i < 100; i++)
            {
                for (int j = i; j < 100; j++)
                {
                    int bcd_i = ToBcd(i);
                    int bcd_j = ToBcd(j);
                    int result = bcd_i + bcd_j;

                    int sum = i + j;
                    int expected = ToBcd(sum);

                    A = (byte)result;
                    Flag_N = false;
                    Flag_H = (bcd_i & 0x0f) + (bcd_j & 0x0f) >= 0x10;
                    Flag_C = (bcd_i & 0xf0) + (bcd_j & 0xf0) + (Flag_H ? 0x10 : 0) >= 0x100;

                    cpu.Step();
                    PC = 0x100;

                    if ((byte)expected != A)
                    {
                        failure.AppendFormat("Failed on {0}+{1}={2}", i, j, sum);
                        failure.AppendLine();
                    }
                    if (sum >= 100 && Flag_C == false)
                    {
                        failure.AppendFormat("Carry flag not set on {0}+{1}={2}", i, j, sum);
                        failure.AppendLine();
                    }
                    
                }
            }

            Assert.IsTrue(failure.Length == 0, failure.ToString());
        }

        [TestMethod]
        public void DecimalAdjust_AllSubtraction()
        {
            StringBuilder failure = new StringBuilder();

            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < 100; i++)
                {
                    int bcd_i = ToBcd(i);
                    int bcd_j = ToBcd(j);
                    int result = bcd_i - bcd_j;

                    int sum = i - j + 100;
                    if (sum >= 100) sum -= 100;

                    int expected = ToBcd(sum);

                    A = (byte)result;
                    Flag_N = true;
                    Flag_H = (bcd_i & 0x0f) < (bcd_j & 0x0f);
                    Flag_C = (bcd_i & 0xf0) < (bcd_j & 0xf0) + (Flag_H ? 0x10 : 0);
                    bool C = Flag_C;

                    if (i == 0 && j == 1)
                    {
                        int k = 4;
                    }

                    cpu.Step();
                    PC = 0x100;

                    if ((byte)expected != A)
                    {
                        failure.AppendFormat("Failed on {0}-{1}={2}", i, j, sum);
                        failure.AppendLine();
                    }
                    if (C != Flag_C)
                    {
                        failure.AppendFormat("Carry flag not set on {0}-{1}={2}", i, j, sum);
                        failure.AppendLine();
                    }

                }
            }

            Assert.IsTrue(failure.Length == 0, failure.ToString());
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

        [TestMethod]
        public void DecimalAdjust_()
        {
            A = 0x33;
            Flag_C = true;
            Flag_H = true;

            cpu.Step();

            Assert.AreEqual(0x99, A);
        }
    }
}
