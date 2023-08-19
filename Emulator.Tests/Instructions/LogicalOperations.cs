namespace Emulator.Tests.Instructions;

public class LogicalOperations : CPUTest {
    // Logical AND
    [Theory]
    [InlineData(0x21, 0x11, false, false)] // 0x21 AND 0x11 = 0x01, Z is not set, N is not set
    [InlineData(0x80, 0x01, true, false)] // 0x80 AND 0x01 = 0x00, Z is set, N is not set
    [InlineData(0x80, 0x80, false, true)] // 0x80 AND 0x80 = 0x80, Z is not set, N is set
    public void AND(byte initialA, byte valueToLoad, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        m_CPU.Write(0x0200, valueToLoad);
        ExecuteInstruction(0x29, m_CPU); // Immediate mode opcode for AND

        Assert.Equal(initialA & valueToLoad, m_CPU.A);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    // Exclusive OR
    [Theory]
    [InlineData(0x21, 0x11, false, false)]
    [InlineData(0x80, 0x80, true, false)]
    [InlineData(0x80, 0x01, false, true)]
    public void EOR(byte initialA, byte valueToLoad, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        m_CPU.Write(0x0200, valueToLoad);
        ExecuteInstruction(0x49, m_CPU); // Immediate mode opcode for EOR

        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    // Logical OR
    [Theory]
    [InlineData(0x21, 0x11, false, false)]
    [InlineData(0x00, 0x00, true, false)]
    [InlineData(0x80, 0x01, false, true)]
    public void ORA(byte initialA, byte valueToLoad, bool expectZ, bool expectN) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        m_CPU.Write(0x0200, valueToLoad);
        ExecuteInstruction(0x09, m_CPU); // Immediate mode opcode for ORA

        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    // Bit Test
    [Theory]
    [InlineData(0x21, 0x11, false, false, false)] // 0x21 AND 0x11 = 0x01, Z is not set, N is not set, V is not set
    [InlineData(0x80, 0x01, true, false, false)] // 0x80 AND 0x01 = 0x00, Z is set, N is not set, V is not set
    [InlineData(0x80, 0x80, false, true, false)] // 0x80 AND 0x80 = 0x80, Z is not set, N is set, V is not set
    public void BIT(byte initialA, byte valueToLoad, bool expectZ, bool expectN, bool expectV) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        m_CPU.Write(0x0200, 0x20);
        m_CPU.Write(0x0020, valueToLoad);
        ExecuteInstruction(0x24, m_CPU); // Zero Page mode opcode for BIT

        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectN, m_CPU.Status.Negative);
        Assert.Equal(expectV, m_CPU.Status.Overflow);
    }
}