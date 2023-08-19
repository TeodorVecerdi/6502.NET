#nullable enable

namespace Emulator.Tests;

public abstract unsafe class CPUTest {
    // Helper method to execute an instruction
    protected static void ExecuteInstruction(uint8_t opcode, CPU cpu) {
        Instruction instruction = InstructionLookupTable[opcode];
        instruction.Address(cpu);
        instruction.Execute(cpu);
    }

    protected readonly Bus m_Bus;
    protected readonly CPU m_CPU;

    protected CPUTest() {
        this.m_CPU = new CPU();
        this.m_Bus = new Bus();
        this.m_Bus.Connect(this.m_CPU);
    }
}