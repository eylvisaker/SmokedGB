﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmokedGB.UnitTests.Fakes;

namespace SmokedGB.UnitTests.CpuTests
{
    public abstract class CpuTest
    {
        protected GameboyCpu cpu;
        protected Registers registers;
        protected FakeMemoryController memory;

        public CpuTest()
        {
            memory = new FakeMemoryController();

            cpu = new GameboyCpu();
            registers = cpu.registers;

            cpu.Initialize(memory, false);
        }

        protected void PrepareNextOpCode(GameboyCpu.OpCode opcode)
        {
            memory[registers.PC] = (byte)opcode;
        }

        protected void VerifyFlags(bool? H = null, bool? C = null, bool? Z = null, bool? N = null)
        {
            if (H != null) Assert.AreEqual(H, registers.Flag_H, "Flag H was not expected value.");
            if (C != null) Assert.AreEqual(C, registers.Flag_C, "Flag C was not expected value.");
            if (Z != null) Assert.AreEqual(Z, registers.Flag_Z, "Flag Z was not expected value.");
            if (N != null) Assert.AreEqual(N, registers.Flag_N, "Flag N was not expected value.");
        }

        public byte A { get { return registers.A; } set { registers.A = value; } }
        public byte B { get { return registers.B; } set { registers.B = value; } }
        public byte C { get { return registers.C; } set { registers.C = value; } }
        public byte D { get { return registers.D; } set { registers.D = value; } }

        public bool Flag_C { get { return registers.Flag_C; } set { registers.Flag_C = value; } }
        public bool Flag_H { get { return registers.Flag_H; } set { registers.Flag_H = value; } }
        public bool Flag_Z { get { return registers.Flag_Z; } set { registers.Flag_Z = value; } }
        public bool Flag_N { get { return registers.Flag_N; } set { registers.Flag_N = value; } }
    }
}