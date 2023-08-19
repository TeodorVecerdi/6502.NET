namespace Emulator.Tests.Instructions;

public class IncrementDecrementOperations : CPUTest {
    [Theory]
    [InlineData(0x05, 0x06, false, false)] // Normal increment
    [InlineData(0x7F, 0x80, false, true)] // 7F + 1 = 80, N flag set
    [InlineData(0xFF, 0x00, true, false)] // 0xFF + 1 = 0x00, Z flag set
    public void INC(uint8_t initialValue, uint8_t expectedValue, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x00);
        m_CPU.Write(0x0201, 0x01);
        m_CPU.Write(0x0100, initialValue);
        ExecuteInstruction(0xEE, m_CPU);
        uint8_t actualValue = m_CPU.Read(0x0100);

        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
    }

    [Theory]
    [InlineData(0x05, 0x06, false, false)] // Normal increment
    [InlineData(0x7F, 0x80, false, true)] // 7F + 1 = 80, N flag set
    [InlineData(0xFF, 0x00, true, false)] // 0xFF + 1 = 0x00, Z flag set
    public void INX(uint8_t initialX, uint8_t expectedValue, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.X = initialX;
        ExecuteInstruction(0xE8, m_CPU);

        Assert.Equal(expectedValue, m_CPU.X);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
    }

    [Theory]
    [InlineData(0x05, 0x06, false, false)] // Normal increment
    [InlineData(0x7F, 0x80, false, true)] // 7F + 1 = 80, N flag set
    [InlineData(0xFF, 0x00, true, false)] // 0xFF + 1 = 0x00, Z flag set
    public void INY(uint8_t initialY, uint8_t expectedValue, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.Y = initialY;
        ExecuteInstruction(0xC8, m_CPU);

        Assert.Equal(expectedValue, m_CPU.Y);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
    }

    [Theory]
    [InlineData(0x05, 0x04, false, false)] // Normal decrement
    [InlineData(0x00, 0xFF, false, true)] // 0 - 1 = 0xFF, N flag set
    [InlineData(0x01, 0x00, true, false)] // 1 - 1 = 0x00, Z flag set
    public void DEC(uint8_t initialValue, uint8_t expectedValue, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x00);
        m_CPU.Write(0x0201, 0x01);
        m_CPU.Write(0x0100, initialValue);
        ExecuteInstruction(0xCE, m_CPU);
        uint8_t actualValue = m_CPU.Read(0x0100);

        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
    }

    [Theory]
    [InlineData(0x05, 0x04, false, false)] // Normal decrement
    [InlineData(0x00, 0xFF, false, true)] // 0 - 1 = 0xFF, N flag set
    [InlineData(0x01, 0x00, true, false)] // 1 - 1 = 0x00, Z flag set
    public void DEX(uint8_t initialX, uint8_t expectedValue, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.X = initialX;
        ExecuteInstruction(0xCA, m_CPU);

        Assert.Equal(expectedValue, m_CPU.X);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
    }

    [Theory]
    [InlineData(0x05, 0x04, false, false)] // Normal decrement
    [InlineData(0x00, 0xFF, false, true)] // 0 - 1 = 0xFF, N flag set
    [InlineData(0x01, 0x00, true, false)] // 1 - 1 = 0x00, Z flag set
    public void DEY(uint8_t initialY, uint8_t expectedValue, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.Y = initialY;
        ExecuteInstruction(0x88, m_CPU);

        Assert.Equal(expectedValue, m_CPU.Y);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
    }
}