namespace Emulator.Tests.Instructions;

public class StackOperations : CPUTest {
    [Fact]
    public void PHA() {
        m_CPU.PC = 0x0200;
        m_CPU.A = 0xAA;
        m_CPU.SP = 0xFD;
        ExecuteInstruction(0x48, m_CPU);  // Opcode for PHA

        Assert.Equal(0xAA, m_CPU.Read(0x01FD));
        Assert.Equal(0xFC, m_CPU.SP);
    }

    [Fact]
    public void PHP() {
        m_CPU.PC = 0x0200;
        m_CPU.Status.Data = 0b10001010;
        m_CPU.SP = 0xFD;
        ExecuteInstruction(0x08, m_CPU);  // Opcode for PHP

        // Since PHP always sets bit 5 and 4
        Assert.Equal(0b10111010, m_CPU.Read(0x01FD));
        Assert.Equal(0xFC, m_CPU.SP);
    }

    [Fact]
    public void PLA() {
        m_CPU.PC = 0x0200;
        m_CPU.SP = 0xFC;
        m_CPU.Write(0x01FD, 0xAA);
        ExecuteInstruction(0x68, m_CPU);  // Opcode for PLA

        Assert.Equal(0xAA, m_CPU.A);
        Assert.Equal(0xFD, m_CPU.SP);
    }

    [Fact]
    public void PLP() {
        m_CPU.PC = 0x0200;
        m_CPU.SP = 0xFC;
        m_CPU.Status.Data = ProcessorStatusFlags.BreakCommand;
        m_CPU.Write(0x01FD, 0b10111010);
        ExecuteInstruction(0x28, m_CPU);  // Opcode for PLP

        // Since PLP ignores bit 5 and 4, they should remain the same
        Assert.Equal(0b10011010 | ProcessorStatusFlags.BreakCommand, m_CPU.Status.Data);
        Assert.Equal(0xFD, m_CPU.SP);
    }

    [Theory]
    [InlineData(0x7F, 0x7F, false, false)] // Transfer 0x7F, no flags set
    [InlineData(0x80, 0x80, false, true)] // Transfer 0x80, N flag set due to bit 7
    [InlineData(0x00, 0x00, true, false)] // Transfer 0x00, Z flag set
    public void TSX(uint8_t initialSP, uint8_t expectedX, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.SP = initialSP;
        ExecuteInstruction(0xBA, m_CPU);  // Opcode for TSX

        Assert.Equal(expectedX, m_CPU.X);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    [Fact]
    public void TXS() {
        m_CPU.PC = 0x0200;
        m_CPU.X = 0x42;
        ExecuteInstruction(0x9A, m_CPU);  // Opcode for TXS

        Assert.Equal(0x42, m_CPU.SP);
    }

    // Test for SP wrapping behavior (assuming SP is a byte)
    [Fact]
    public void StackPointerWrapsWhenIncremented() {
        m_CPU.PC = 0x0200;
        m_CPU.SP = 0xFF;
        m_CPU.Write(0x0100, 0xAA);
        ExecuteInstruction(0x68, m_CPU);  // Opcode for PLA

        Assert.Equal(0xAA, m_CPU.A);
        Assert.Equal(0x00, m_CPU.SP);  // SP should wrap to 0x00
    }

    [Fact]
    public void StackPointerWrapsWhenDecremented() {
        m_CPU.PC = 0x0200;
        m_CPU.SP = 0x00;
        ExecuteInstruction(0x48, m_CPU);  // Opcode for PHA

        Assert.Equal(0xFF, m_CPU.SP);  // SP should wrap to 0xFF
    }
}