namespace Emulator.Tests.Instructions;

public class ShiftOperations : CPUTest {
    // Arithmetic Shift Left
    [Theory]
    [InlineData(0x01, false, false, false)] // 0x01 << 1 = 0x02, Carry not set, Zero not set, Negative not set
    [InlineData(0x80, true, false, true)]  // 0x80 << 1 = 0x00, Carry set, Zero set, Negative not set
    [InlineData(0x00, true, false, false)]  // 0x00 << 1 = 0x00, Carry not set, Zero set, Negative not set
    [InlineData(0x81, false, false, true)]  // 0x81 << 1 = 0x02, Carry set, Zero not set, Negative not set
    public void ASL(byte initialA, bool expectZ, bool expectN, bool expectC) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        ExecuteInstruction(0x0A, m_CPU);  // Accumulator mode opcode for ASL

        Assert.Equal((uint8_t)(initialA << 1), m_CPU.A);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectC, m_CPU.Status.Carry);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    // Logical Shift Right
    [Theory]
    [InlineData(0x02, false, false, false)] // 0x02 >> 1 = 0x01, Carry not set, Zero not set, Negative not set
    [InlineData(0x01, true, false, true)]  // 0x01 >> 1 = 0x00, Carry set, Zero set, Negative not set
    [InlineData(0x00, true, false, false)]  // 0x00 >> 1 = 0x00, Carry not set, Zero set, Negative not set
    [InlineData(0x81, false, false, true)]  // 0x81 >> 1 = 0x40, Carry set, Zero not set, Negative not set
    public void LSR(byte initialA, bool expectZ, bool expectN, bool expectC) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        ExecuteInstruction(0x4A, m_CPU);  // Accumulator mode opcode for LSR

        Assert.Equal((uint8_t)(initialA >> 1), m_CPU.A);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectC, m_CPU.Status.Carry);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    // Rotate Left
    [Theory]
    [InlineData(0x01, false, false, false, false)] // 0x01 << 1 = 0x02, Carry not set, Zero not set, Negative not set
    [InlineData(0x80, false, true, false, true)]  // 0x80 << 1 = 0x00, Carry set, Zero set, Negative not set
    [InlineData(0x00, false, true, false, false)]  // 0x00 << 1 = 0x00, Carry not set, Zero set, Negative not set
    [InlineData(0x81, false, false, false, true)]  // 0x81 << 1 = 0x02, Carry set, Zero not set, Negative not set

    [InlineData(0x01, true, false, false, false)]  // 0x01 << 1 = 0x02, Carry not set, Zero not set, Negative not set
    [InlineData(0x80, true, false, false, true)]  // 0x80 << 1 = 0x01, Carry set, Zero not set, Negative not set
    [InlineData(0x00, true, false, false, false)]  // 0x00 << 1 = 0x01, Carry not set, Zero not set, Negative not set
    [InlineData(0x81, true, false, false, true)]  // 0x81 << 1 = 0x02, Carry set, Zero not set, Negative not set
    public void ROL(byte initialA, bool initialCarry, bool expectZ, bool expectN, bool expectC) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        m_CPU.Status.Carry = initialCarry;
        ExecuteInstruction(0x2A, m_CPU);  // Accumulator mode opcode for ROL

        Assert.Equal((uint8_t)((initialA << 1) | (initialCarry ? 1 : 0)), m_CPU.A);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectC, m_CPU.Status.Carry);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }

    // Rotate Right
    [Theory]
    [InlineData(0x02, false, false, false, false)] // 0x02 >> 1 = 0x01, Carry not set, Zero not set, Negative not set
    [InlineData(0x01, false, true, false, true)]  // 0x01 >> 1 = 0x00, Carry set, Zero set, Negative not set
    [InlineData(0x00, false, true, false, false)]  // 0x00 >> 1 = 0x00, Carry not set, Zero set, Negative not set
    [InlineData(0x81, false, false, false, true)]  // 0x81 >> 1 = 0x40, Carry set, Zero not set, Negative not set

    [InlineData(0x02, true, false, true, false)]  // 0x02 >> 1 = 0x81, Carry not set, Zero not set, Negative set
    [InlineData(0x01, true, false, true, true)]  // 0x01 >> 1 = 0x80, Carry set, Zero not set, Negative set
    [InlineData(0x00, true, false, true, false)]  // 0x00 >> 1 = 0x80, Carry not set, Zero not set, Negative set
    [InlineData(0x81, true, false, true, true)]  // 0x81 >> 1 = 0xC0, Carry set, Zero not set, Negative set
    public void ROR(byte initialA, bool initialCarry, bool expectZ, bool expectN, bool expectC) {
        m_CPU.PC = 0x0200;
        m_CPU.A = initialA;
        m_CPU.Status.Carry = initialCarry;
        ExecuteInstruction(0x6A, m_CPU);  // Accumulator mode opcode for ROR

        Assert.Equal((uint8_t)((initialA >> 1) | (initialCarry ? 0x80 : 0x00)), m_CPU.A);
        Assert.Equal(expectZ, m_CPU.Status.Zero);
        Assert.Equal(expectC, m_CPU.Status.Carry);
        Assert.Equal(expectN, m_CPU.Status.Negative);
    }
}
