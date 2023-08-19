namespace Emulator.Tests.Instructions;

public class RegisterTransferOperations : CPUTest {
    // Transfer Accumulator to X
    [Theory]
    [InlineData(0x7F, false, false)] // Transfer 0x7F, Z is not set, N is not set
    [InlineData(0x80, false, true)] // Transfer 0x80, Z is not set, N is set
    [InlineData(0x00, true, false)] // Transfer 0x00, Z is set, N is not set
    public void TAX(byte initialA, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        ExecuteInstruction(0xAA, m_CPU); // Opcode for TAX

        Assert.Equal(initialA, m_CPU.X);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    // Transfer Accumulator to Y
    [Theory]
    [InlineData(0x7F, false, false)]
    [InlineData(0x80, false, true)]
    [InlineData(0x00, true, false)]
    public void TAY(byte initialA, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        ExecuteInstruction(0xA8, m_CPU); // Opcode for TAY

        Assert.Equal(initialA, m_CPU.Y);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    // Transfer X to Accumulator
    [Theory]
    [InlineData(0x7F, false, false)]
    [InlineData(0x80, false, true)]
    [InlineData(0x00, true, false)]
    public void TXA(byte initialX, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.X = initialX;
        ExecuteInstruction(0x8A, m_CPU); // Opcode for TXA

        Assert.Equal(initialX, m_CPU.A);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    // Transfer Y to Accumulator
    [Theory]
    [InlineData(0x7F, false, false)]
    [InlineData(0x80, false, true)]
    [InlineData(0x00, true, false)]
    public void TYA(byte initialY, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.Y = initialY;
        ExecuteInstruction(0x98, m_CPU); // Opcode for TYA

        Assert.Equal(initialY, m_CPU.A);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }
}