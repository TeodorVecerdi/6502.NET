namespace Emulator.Tests.Instructions;

public class LoadStoreInstructions : CPUTest {
    // Load Accumulator and test flags
    [Theory]
    [InlineData(0x80, true, false)] // LDA Immediate with Negative flag
    [InlineData(0x00, false, true)] // LDA Immediate with Zero flag
    [InlineData(0x42, false, false)] // LDA Immediate with a standard byte
    public void LDA_LoadsAccumulatorAndSetsFlags(uint8_t value, bool expectN, bool expectZ) {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, value);
        ExecuteInstruction(0xA9, m_CPU);
        Assert.Equal(value, m_CPU.A);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
    }

    // Same logic for X and Y registers
    [Theory]
    [InlineData(0x80, true, false)]
    [InlineData(0x00, false, true)]
    [InlineData(0x42, false, false)]
    public void LDX_LoadsXRegisterAndSetsFlags(uint8_t value, bool expectN, bool expectZ) {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, value);
        ExecuteInstruction(0xA2, m_CPU);
        Assert.Equal(value, m_CPU.X);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
    }

    [Theory]
    [InlineData(0x80, true, false)]
    [InlineData(0x00, false, true)]
    [InlineData(0x42, false, false)]
    public void LDY_LoadsYRegisterAndSetsFlags(uint8_t value, bool expectN, bool expectZ) {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, value);
        ExecuteInstruction(0xA0, m_CPU);
        Assert.Equal(value, m_CPU.Y);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
    }

    // Store Accumulator
    [Fact]
    public void STA_StoresAccumulator() {
        m_CPU.A = 0x42;
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x34);
        m_CPU.Write(0x0201, 0x12);
        ExecuteInstruction(0x8D, m_CPU);
        Assert.Equal(0x42, m_CPU.Read(0x1234));
    }

    // Store X Register
    [Fact]
    public void STX_StoresXRegister() {
        m_CPU.X = 0x42;
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x34);
        m_CPU.Write(0x0201, 0x12);
        ExecuteInstruction(0x8E, m_CPU);
        Assert.Equal(0x42, m_CPU.Read(0x1234));
    }

    // Store Y Register
    [Fact]
    public void STY_StoresYRegister() {
        m_CPU.Y = 0x42;
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x34);
        m_CPU.Write(0x0201, 0x12);
        ExecuteInstruction(0x8C, m_CPU);
        Assert.Equal(0x42, m_CPU.Read(0x1234));
    }
}