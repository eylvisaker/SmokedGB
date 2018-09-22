using Moq;
using SmokedGB.UnitTests.Fakes;

namespace SmokedGB.UnitTests.CpuTests
{
    public abstract class CpuTest
    {
        protected GameboyCpu cpu;
        protected Registers registers;
        protected FakeMemoryController memory;

        protected int nextOpCodeWriteTo;

        public CpuTest()
        {
            memory = new FakeMemoryController();

            cpu = new GameboyCpu();
            registers = cpu.registers;

            cpu.Initialize(memory, false);
        }

        protected void PrepareOpCode(GameboyCpu.OpCode opcode, params byte[] args)
        {
            memory[registers.PC + nextOpCodeWriteTo] = (byte)opcode;

            nextOpCodeWriteTo++;

            foreach (var arg in args)
            {
                memory[registers.PC + nextOpCodeWriteTo] = arg;

                nextOpCodeWriteTo++;
            }
        }

        protected void PrepareOpCode(GameboyCpu.OpCodeCB opcodeCB, params byte[] args)
        {
            PrepareOpCode(GameboyCpu.OpCode.OpCodeCB);
            PrepareOpCode((GameboyCpu.OpCode)opcodeCB, args);
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

        public ushort AF { get { return registers.AF; } set { registers.AF = value; } }
        public ushort HL { get { return registers.HL; } set { registers.HL = value; } }
        public ushort BC { get { return registers.BC; } set { registers.BC = value; } }
        public ushort DE { get { return registers.DE; } set { registers.DE = value; } }
        public ushort SP { get { return registers.SP; } set { registers.SP = value; } }
        public ushort PC { get { return registers.PC; } set { registers.PC = value; } }

        public bool Flag_C { get { return registers.Flag_C; } set { registers.Flag_C = value; } }
        public bool Flag_H { get { return registers.Flag_H; } set { registers.Flag_H = value; } }
        public bool Flag_Z { get { return registers.Flag_Z; } set { registers.Flag_Z = value; } }
        public bool Flag_N { get { return registers.Flag_N; } set { registers.Flag_N = value; } }


        /// <summary>
        /// Peeks into the stack and returns a 16-bit value
        /// </summary>
        /// <param name="depth">Number of bytes deep into the stack to peek.</param>
        /// <returns></returns>
        protected ushort PeekStack16(int depth = 0)
        {
            int lb = PeekStack(depth);
            int hb = PeekStack(depth + 1);
            int result = (hb << 8) + lb;

            return (ushort)result;
        }

        /// <summary>
        /// Peeks into the stack and returns an 8-bit value.
        /// </summary>
        /// <param name="depth">Number of bytes deep into the stack to peek.</param>
        /// <returns></returns>
        protected byte PeekStack(int depth = 0)
        {
            return memory[SP + depth];
        }
    }
}