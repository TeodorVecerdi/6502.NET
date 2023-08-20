namespace Emulator.Tests.Instructions;

public class InterruptOperations : CPUTest {
    // BRK (Break)
    [Fact]
    public void BRK() {
        m_CPU.PC = 0x0200;
        m_CPU.SP = 0xFF;
        m_CPU.Status.Data = 0b1010_1010;
        m_CPU.Write(0xFFFE, 0x34); // Low byte of interrupt handler address
        m_CPU.Write(0xFFFF, 0x12); // High byte of interrupt handler address

        ExecuteInstruction(0x00, m_CPU); // BRK opcode

        Assert.Equal(0x1234, m_CPU.PC);                // PC should point to the interrupt handler address
        Assert.Equal(0xFC, m_CPU.SP);                  // Stack should have had 3 bytes pushed to it
        Assert.Equal(0x02, m_CPU.Read(0x01FF));        // High byte of return address - 1 should be pushed to the stack
        Assert.Equal(0x01, m_CPU.Read(0x01FE));        // Low byte of return address - 1 should be pushed to the stack
        Assert.Equal(0b1011_1010, m_CPU.Read(0x01FD)); // Status register with Break command flag set should be pushed to the stack
        Assert.True(m_CPU.Status.Unused);              // Unused bit should be set
        Assert.True(m_CPU.Status.InterruptDisable);    // Interrupt disable flag should be set
    }

    // RTI (Return from Interrupt)
    [Fact]
    public void RTI() {
        const uint8_t initialStatus  = 0b1011_1010 |  ProcessorStatusFlags.BreakCommand | ProcessorStatusFlags.Unused;
        const uint8_t expectedStatus = 0b1011_1010 & ~ProcessorStatusFlags.BreakCommand | ProcessorStatusFlags.Unused;

        m_CPU.PC = 0x0200;
        m_CPU.SP = 0xFC;
        m_CPU.Write(0x01FD, initialStatus); // Status register with Break command flag set
        m_CPU.Write(0x01FE, 0x01);          // Low byte of return address
        m_CPU.Write(0x01FF, 0x02);          // High byte of return address

        ExecuteInstruction(0x40, m_CPU); // RTI opcode

        Assert.Equal(0x0201, m_CPU.PC);                  // PC should point to the return address
        Assert.Equal(0xFF, m_CPU.SP);                    // Stack should have had 3 bytes popped from it
        Assert.Equal(expectedStatus, m_CPU.Status.Data); // Status register should be set to the value that was pushed to the stack
    }
}