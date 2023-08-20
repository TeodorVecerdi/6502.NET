namespace Emulator.Tests.Instructions;

public class StatusFlagOperations : CPUTest {
    [Fact]
    public void CLC() {
        m_CPU.Status.Carry = true;
        ExecuteInstruction(0x18, m_CPU);  // Opcode for CLC
        Assert.False(m_CPU.Status.Carry);
    }

    [Fact]
    public void CLD() {
        m_CPU.Status.DecimalMode = true;
        ExecuteInstruction(0xD8, m_CPU);  // Opcode for CLD
        Assert.False(m_CPU.Status.DecimalMode);
    }

    [Fact]
    public void CLI() {
        m_CPU.Status.InterruptDisable = true;
        ExecuteInstruction(0x58, m_CPU);  // Opcode for CLI
        Assert.False(m_CPU.Status.InterruptDisable);
    }

    [Fact]
    public void CLV() {
        m_CPU.Status.Overflow = true;
        ExecuteInstruction(0xB8, m_CPU);  // Opcode for CLV
        Assert.False(m_CPU.Status.Overflow);
    }

    [Fact]
    public void SEC() {
        m_CPU.Status.Carry = false;
        ExecuteInstruction(0x38, m_CPU);  // Opcode for SEC
        Assert.True(m_CPU.Status.Carry);
    }

    [Fact]
    public void SED() {
        m_CPU.Status.DecimalMode = false;
        ExecuteInstruction(0xF8, m_CPU);  // Opcode for SED
        Assert.True(m_CPU.Status.DecimalMode);
    }

    [Fact]
    public void SEI() {
        m_CPU.Status.InterruptDisable = false;
        ExecuteInstruction(0x78, m_CPU);  // Opcode for SEI
        Assert.True(m_CPU.Status.InterruptDisable);
    }
}
