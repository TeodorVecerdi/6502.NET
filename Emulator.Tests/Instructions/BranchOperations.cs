namespace Emulator.Tests.Instructions;

public class BranchOperations : CPUTest {
    // BCC (Branch if Carry Clear)
    [Theory]
    [InlineData(0x0003, 0x05, 0x0009, false, 3)]  // No Page Cross, Positive Offset
    [InlineData(0x000A, -0x05, 0x0006, false, 3)] // No Page Cross, Negative Offset
    [InlineData(0x00FE, 0x02, 0x0101, false, 4)]  // Page Cross, Positive Offset
    [InlineData(0x0002, -0x01, 0x0003, true, 2)]  // Carry Set (no branch)
    public void BCC(uint16_t initialPC, int8_t offset, uint16_t expectedPC, bool setCarryFlag, int expectedCycles) {
        m_CPU.PC = initialPC;
        m_CPU.Write(initialPC, (uint8_t)offset);
        m_CPU.Status.Carry = setCarryFlag;
        m_CPU.Cycles = 2;

        ExecuteInstruction(0x90, m_CPU); // Opcode for BCC

        Assert.Equal(expectedPC, m_CPU.PC);
        Assert.Equal(expectedCycles, m_CPU.Cycles);
    }

    // BCS (Branch if Carry Set)
    [Theory]
    [InlineData(0x0003, 0x05, 0x0009, true, 3)]   // No Page Cross, Positive Offset
    [InlineData(0x000A, -0x05, 0x0006, true, 3)]  // No Page Cross, Negative Offset
    [InlineData(0x00FE, 0x02, 0x0101, true, 4)]   // Page Cross, Positive Offset
    [InlineData(0x0002, -0x01, 0x0003, false, 2)] // Carry Clear (no branch)
    public void BCS(uint16_t initialPC, int8_t offset, uint16_t expectedPC, bool setCarryFlag, int expectedCycles) {
        m_CPU.PC = initialPC;
        m_CPU.Write(initialPC, (uint8_t)offset);
        m_CPU.Status.Carry = setCarryFlag;
        m_CPU.Cycles = 2;

        ExecuteInstruction(0xB0, m_CPU); // Opcode for BCS

        Assert.Equal(expectedPC, m_CPU.PC);
        Assert.Equal(expectedCycles, m_CPU.Cycles);
    }

    // BNE (Branch if Zero Clear)
    [Theory]
    [InlineData(0x0003, 0x05, 0x0009, false, 3)]  // No Page Cross, Positive Offset
    [InlineData(0x000A, -0x05, 0x0006, false, 3)] // No Page Cross, Negative Offset
    [InlineData(0x00FE, 0x02, 0x0101, false, 4)]  // Page Cross, Positive Offset
    [InlineData(0x0002, -0x01, 0x0003, true, 2)]  // Zero Set (no branch)
    public void BNE(uint16_t initialPC, int8_t offset, uint16_t expectedPC, bool setZeroFlag, int expectedCycles) {
        m_CPU.PC = initialPC;
        m_CPU.Write(initialPC, (uint8_t)offset);
        m_CPU.Status.Zero = setZeroFlag;
        m_CPU.Cycles = 2;

        ExecuteInstruction(0xD0, m_CPU); // Opcode for BNE

        Assert.Equal(expectedPC, m_CPU.PC);
        Assert.Equal(expectedCycles, m_CPU.Cycles);
    }

    // BEQ (Branch if Zero Set)
    [Theory]
    [InlineData(0x0003, 0x05, 0x0009, true, 3)]   // No Page Cross, Positive Offset
    [InlineData(0x000A, -0x05, 0x0006, true, 3)]  // No Page Cross, Negative Offset
    [InlineData(0x00FE, 0x02, 0x0101, true, 4)]   // Page Cross, Positive Offset
    [InlineData(0x0002, -0x01, 0x0003, false, 2)] // Zero Clear (no branch)
    public void BEQ(uint16_t initialPC, int8_t offset, uint16_t expectedPC, bool setZeroFlag, int expectedCycles) {
        m_CPU.PC = initialPC;
        m_CPU.Write(initialPC, (uint8_t)offset);
        m_CPU.Status.Zero = setZeroFlag;
        m_CPU.Cycles = 2;

        ExecuteInstruction(0xF0, m_CPU); // Opcode for BEQ

        Assert.Equal(expectedPC, m_CPU.PC);
        Assert.Equal(expectedCycles, m_CPU.Cycles);
    }

    // BPL (Branch if Negative Clear)
    [Theory]
    [InlineData(0x0003, 0x05, 0x0009, false, 3)]  // No Page Cross, Positive Offset
    [InlineData(0x000A, -0x05, 0x0006, false, 3)] // No Page Cross, Negative Offset
    [InlineData(0x00FE, 0x02, 0x0101, false, 4)]  // Page Cross, Positive Offset
    [InlineData(0x0002, -0x01, 0x0003, true, 2)]  // Negative Set (no branch)
    public void BPL(uint16_t initialPC, int8_t offset, uint16_t expectedPC, bool setNegativeFlag, int expectedCycles) {
        m_CPU.PC = initialPC;
        m_CPU.Write(initialPC, (uint8_t)offset);
        m_CPU.Status.Negative = setNegativeFlag;
        m_CPU.Cycles = 2;

        ExecuteInstruction(0x10, m_CPU);  // Opcode for BPL

        Assert.Equal(expectedPC, m_CPU.PC);
        Assert.Equal(expectedCycles, m_CPU.Cycles);
    }

    // BMI (Branch if Negative Set)
    [Theory]
    [InlineData(0x0003, 0x05, 0x0009, true, 3)]   // No Page Cross, Positive Offset
    [InlineData(0x000A, -0x05, 0x0006, true, 3)]  // No Page Cross, Negative Offset
    [InlineData(0x00FE, 0x02, 0x0101, true, 4)]   // Page Cross, Positive Offset
    [InlineData(0x0002, -0x01, 0x0003, false, 2)] // Negative Clear (no branch)
    public void BMI(uint16_t initialPC, int8_t offset, uint16_t expectedPC, bool setNegativeFlag, int expectedCycles) {
        m_CPU.PC = initialPC;
        m_CPU.Write(initialPC, (uint8_t)offset);
        m_CPU.Status.Negative = setNegativeFlag;
        m_CPU.Cycles = 2;

        ExecuteInstruction(0x30, m_CPU);  // Opcode for BMI

        Assert.Equal(expectedPC, m_CPU.PC);
        Assert.Equal(expectedCycles, m_CPU.Cycles);
    }

    // BVC (Branch if Overflow Clear)
    [Theory]
    [InlineData(0x0003, 0x05, 0x0009, false, 3)]  // No Page Cross, Positive Offset
    [InlineData(0x000A, -0x05, 0x0006, false, 3)] // No Page Cross, Negative Offset
    [InlineData(0x00FE, 0x02, 0x0101, false, 4)]  // Page Cross, Positive Offset
    [InlineData(0x0002, -0x01, 0x0003, true, 2)]  // Overflow Set (no branch)
    public void BVC(uint16_t initialPC, int8_t offset, uint16_t expectedPC, bool setOverflowFlag, int expectedCycles) {
        m_CPU.PC = initialPC;
        m_CPU.Write(initialPC, (uint8_t)offset);
        m_CPU.Status.Overflow = setOverflowFlag;
        m_CPU.Cycles = 2;

        ExecuteInstruction(0x50, m_CPU);  // Opcode for BVC

        Assert.Equal(expectedPC, m_CPU.PC);
        Assert.Equal(expectedCycles, m_CPU.Cycles);
    }

    // BVS (Branch if Overflow Set)
    [Theory]
    [InlineData(0x0003, 0x05, 0x0009, true, 3)]   // No Page Cross, Positive Offset
    [InlineData(0x000A, -0x05, 0x0006, true, 3)]  // No Page Cross, Negative Offset
    [InlineData(0x00FE, 0x02, 0x0101, true, 4)]   // Page Cross, Positive Offset
    [InlineData(0x0002, -0x01, 0x0003, false, 2)] // Overflow Clear (no branch)
    public void BVS(uint16_t initialPC, int8_t offset, uint16_t expectedPC, bool setOverflowFlag, int expectedCycles) {
        m_CPU.PC = initialPC;
        m_CPU.Write(initialPC, (uint8_t)offset);
        m_CPU.Status.Overflow = setOverflowFlag;
        m_CPU.Cycles = 2;

        ExecuteInstruction(0x70, m_CPU);  // Opcode for BVS

        Assert.Equal(expectedPC, m_CPU.PC);
        Assert.Equal(expectedCycles, m_CPU.Cycles);
    }
}