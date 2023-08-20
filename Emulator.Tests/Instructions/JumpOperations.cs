namespace Emulator.Tests.Instructions;

public class JumpOperations : CPUTest {
    // JMP (Jump) Absolute
    [Fact]
    public void JMP_Absolute() {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x34);
        m_CPU.Write(0x0201, 0x12);

        ExecuteInstruction(0x4C, m_CPU);  // Absolute mode opcode for JMP

        Assert.Equal(0x1234, m_CPU.PC);
    }

    // JMP (Jump) Indirect (without page boundary crossing)
    [Fact]
    public void JMP_Indirect() {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x34);
        m_CPU.Write(0x0201, 0x12);
        m_CPU.Write(0x1234, 0x78);
        m_CPU.Write(0x1235, 0x56);

        ExecuteInstruction(0x6C, m_CPU);  // Indirect mode opcode for JMP

        Assert.Equal(0x5678, m_CPU.PC);
    }

    // JMP (Jump) Indirect (with page boundary crossing)
    [Fact]
    public void JMP_Indirect_PageBoundaryCross() {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0xFF);
        m_CPU.Write(0x0201, 0x12);
        m_CPU.Write(0x12FF, 0x78);
        m_CPU.Write(0x1300, 0x56);
        m_CPU.Write(0x1200, 0x34);

        ExecuteInstruction(0x6C, m_CPU);  // Indirect mode opcode for JMP

        Assert.Equal(0x3478, m_CPU.PC);
    }

    // JSR (Jump to Subroutine)
    [Fact]
    public void JSR() {
        m_CPU.PC = 0x0200;
        m_CPU.SP = 0xFF;
        m_CPU.Write(0x0200, 0x34);
        m_CPU.Write(0x0201, 0x12);

        ExecuteInstruction(0x20, m_CPU);  // Absolute mode opcode for JSR

        Assert.Equal(0x1234, m_CPU.PC);
        Assert.Equal(0xFD, m_CPU.SP); // SP is decremented by 2
        Assert.Equal(0x02, m_CPU.Read(0x01FF)); // Return address - 1 is pushed to the stack
        Assert.Equal(0x01, m_CPU.Read(0x01FE));
    }

    // RTS (Return from Subroutine)
    [Fact]
    public void RTS() {
        m_CPU.PC = 0x0200;
        m_CPU.SP = 0xFD;
        m_CPU.Write(0x01FF, 0x12);
        m_CPU.Write(0x01FE, 0x34);

        ExecuteInstruction(0x60, m_CPU);  // Absolute mode opcode for RTS

        Assert.Equal(0x1235, m_CPU.PC); // Should be return address + 1
        Assert.Equal(0xFF, m_CPU.SP); // SP is incremented by 2
    }
}