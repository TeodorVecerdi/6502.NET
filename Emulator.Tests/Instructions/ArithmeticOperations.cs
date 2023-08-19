namespace Emulator.Tests.Instructions;

public class ArithmeticOperations : CPUTest {
    [Theory]
    [InlineData(0x20, 0x10, false, 0x30, false, false, false, false)] // Simple ADC
    [InlineData(0x00, 0x00, false, 0x00, true, false, false, false)] // ADC resulting in zero
    [InlineData(0xFF, 0x01, true, 0x01, false, false, false, true)] // ADC with carry
    [InlineData(0x40, 0x40, false, 0x80, false, true, true, false)] // ADC with overflow
    public void ADC(uint8_t initialA, uint8_t valueToLoad, bool initialCarry, uint8_t expectedResult, bool expectZ, bool expectV, bool expectN, bool expectC) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        m_CPU.Status.Carry = initialCarry;
        m_CPU.Write(0x0200, valueToLoad);
        ExecuteInstruction(0x69, m_CPU);

        Assert.Equal(expectedResult, m_CPU.A);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectV, m_CPU.Status.Overflow);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectC, m_CPU.Status.Carry);
    }

    [Theory]
    [InlineData(0x20, 0x10, true, 0x10, false, false, false, true)] // Simple SBC
    [InlineData(0x20, 0x20, true, 0x00, true, false, false, true)] // SBC resulting in zero
    [InlineData(0x20, 0x21, false, 0xFE, false, false, true, false)] // SBC with borrow
    [InlineData(0x80, 0x01, true, 0x7F, false, true, false, true)] // SBC with overflow
    public void SBC(uint8_t initialA, uint8_t valueToLoad, bool initialCarry, uint8_t expectedResult, bool expectZ, bool expectV, bool expectN, bool expectC) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        m_CPU.Status.Carry = initialCarry;
        m_CPU.Write(0x0200, valueToLoad);
        ExecuteInstruction(0xE9, m_CPU);

        Assert.Equal(expectedResult, m_CPU.A);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectV, m_CPU.Status.Overflow);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectC, m_CPU.Status.Carry);
    }

    [Theory]
    [InlineData(0x50, 0x50, true, false, true)] // Equal values, Zero flag set
    [InlineData(0x60, 0x50, false, false, true)] // Accumulator is greater
    [InlineData(0x40, 0x50, false, true, false)] // Accumulator is smaller
    public void CMP(uint8_t initialA, uint8_t valueToLoad, bool expectZ, bool expectN, bool expectC) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        m_CPU.Write(0x0200, valueToLoad);
        ExecuteInstruction(0xC9, m_CPU);

        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectC, m_CPU.Status.Carry);
    }

    [Theory]
    [InlineData(0x50, 0x50, true, false, true)] // Equal values, Zero flag set
    [InlineData(0x60, 0x50, false, false, true)] // X is greater
    [InlineData(0x40, 0x50, false, true, false)] // X is smaller
    public void CPX(uint8_t initialX, uint8_t valueToLoad, bool expectZ, bool expectN, bool expectC) {
        m_CPU.PC = 0x0200;
        m_CPU.X = initialX;
        m_CPU.Write(0x0200, valueToLoad);
        ExecuteInstruction(0xE0, m_CPU);

        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectC, m_CPU.Status.Carry);
    }

    [Theory]
    [InlineData(0x50, 0x50, true, false, true)] // Equal values, Zero flag set
    [InlineData(0x60, 0x50, false, false, true)] // Y is greater
    [InlineData(0x40, 0x50, false, true, false)] // Y is smaller
    public void CPY(uint8_t initialY, uint8_t valueToLoad, bool expectZ, bool expectN, bool expectC) {
        m_CPU.PC = 0x0200;
        m_CPU.Y = initialY;
        m_CPU.Write(0x0200, valueToLoad);
        ExecuteInstruction(0xC0, m_CPU);

        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectC, m_CPU.Status.Carry);
    }
}