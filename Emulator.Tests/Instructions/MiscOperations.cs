namespace Emulator.Tests.Instructions;

public class MiscOperations : CPUTest {
    // NOP (No Operation)
    [Fact]
    public void NOP() {
        m_CPU.PC = 0x1000;
        m_CPU.A = 0xAA;
        m_CPU.X = 0x55;
        m_CPU.Y = 0x33;
        m_CPU.Status.Data = 0b10101010;

        uint8_t[] originalMemory = this.m_Bus.RAM.Data.ToArray();
        ExecuteInstruction(0xEA, m_CPU); // Opcode for NOP
        ExecuteInstruction(0x1C, m_CPU); // Another NOP opcode
        uint8_t[] newMemory = this.m_Bus.RAM.Data.ToArray();

        Assert.Equal(0x1000, m_CPU.PC);
        Assert.Equal(0xAA, m_CPU.A);
        Assert.Equal(0x55, m_CPU.X);
        Assert.Equal(0x33, m_CPU.Y);
        Assert.Equal(0b10101010, m_CPU.Status.Data);
        Assert.Equal(originalMemory, newMemory);
    }

    // XXX (Illegal Instruction)
    [Fact]
    public void XXX() {
        m_CPU.PC = 0x1000;
        m_CPU.A = 0xAA;
        m_CPU.X = 0x55;
        m_CPU.Y = 0x33;
        m_CPU.Status.Data = 0b10101010;

        uint8_t[] originalMemory = this.m_Bus.RAM.Data.ToArray();
        ExecuteInstruction(0x02, m_CPU); // One of the illegal opcodes
        uint8_t[] newMemory = this.m_Bus.RAM.Data.ToArray();

        Assert.Equal(0x1000, m_CPU.PC);
        Assert.Equal(0xAA, m_CPU.A);
        Assert.Equal(0x55, m_CPU.X);
        Assert.Equal(0x33, m_CPU.Y);
        Assert.Equal(0b10101010, m_CPU.Status.Data);
        Assert.Equal(originalMemory, newMemory);
    }
}
