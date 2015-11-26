using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}