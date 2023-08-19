namespace Emulator.Tests;

public class AddressingModes : CPUTest {
    [Fact]
    public void IMP_FetchesAccumulatorValue() {
        m_CPU.A = 0x42; // Set the accumulator to some value.
        IMP(m_CPU);
        Assert.Equal(0x42, m_CPU.Fetched); // Ensure that the fetched value is indeed the accumulator's value.
    }

    [Fact]
    public void IMM_AddressCorrectly() {
        this.m_CPU.PC = 0x0200;
        IMM(this.m_CPU);
        Assert.Equal(0x0200, this.m_CPU.AbsoluteAddress);
        Assert.Equal(0x0201, this.m_CPU.PC);
    }

    [Fact]
    public void ZP0_AddressCorrectly() {
        this.m_CPU.PC = 0x0200;
        this.m_CPU.Write(0x0200, 0x50); // This is a mock. You might need to set up the method to write to memory in CPU class
        ZP0(this.m_CPU);
        Assert.Equal(0x0050, this.m_CPU.AbsoluteAddress);
        Assert.Equal(0x0201, this.m_CPU.PC);
    }

    [Fact]
    public void ZPX_AddressCorrectly() {
        this.m_CPU.PC = 0x0200;
        this.m_CPU.X = 0x05;
        this.m_CPU.Write(0x0200, 0x50);
        ZPX(this.m_CPU);
        Assert.Equal(0x0055, this.m_CPU.AbsoluteAddress);
        Assert.Equal(0x0201, this.m_CPU.PC);
    }

    [Fact]
    public void ZPY_AddressCorrectly() {
        m_CPU.PC = 0x0200;
        m_CPU.Y = 0x05;
        m_CPU.Write(0x0200, 0x50);
        ZPY(m_CPU);
        Assert.Equal(0x0055, m_CPU.AbsoluteAddress);
        Assert.Equal(0x0201, m_CPU.PC);
    }

    [Fact]
    public void REL_PositiveRelative() {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x10); // Positive relative offset
        REL(m_CPU);
        Assert.Equal(0x0010, m_CPU.RelativeAddress);
    }

    [Fact]
    public void REL_NegativeRelative() {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0xFE); // Negative relative offset (-2 in two's complement)
        REL(m_CPU);
        Assert.Equal(0xFFFE, m_CPU.RelativeAddress);
    }

    [Fact]
    public void ABS_AddressCorrectly() {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x56);
        m_CPU.Write(0x0201, 0x34);
        ABS(m_CPU);
        Assert.Equal(0x3456, m_CPU.AbsoluteAddress);
        Assert.Equal(0x0202, m_CPU.PC);
    }

    [Fact]
    public void ABX_AddressCorrectly_NoPageBoundary() {
        this.m_CPU.PC = 0x0200;
        this.m_CPU.X = 0x10;
        this.m_CPU.Write(0x0200, 0x00);
        this.m_CPU.Write(0x0201, 0x02);
        ABX(this.m_CPU);

        Assert.Equal(0x0210, this.m_CPU.AbsoluteAddress); // 0x0200 + 0x10
        Assert.Equal(0x0202, this.m_CPU.PC);
    }

    [Fact]
    public void ABX_AddressCorrectly_PageBoundaryCrossed() {
        this.m_CPU.PC = 0x0200;
        this.m_CPU.X = 0xFF;
        this.m_CPU.Write(0x0200, 0x01);
        this.m_CPU.Write(0x0201, 0x02);
        bool pageBoundaryCrossed = ABX(this.m_CPU);

        Assert.True(pageBoundaryCrossed);
        Assert.Equal(0x0300, this.m_CPU.AbsoluteAddress); // 0x0201 + 0xFF
        Assert.Equal(0x0202, this.m_CPU.PC);
    }

    [Fact]
    public void ABY_AddressCorrectly_NoPageBoundary() {
        m_CPU.PC = 0x0200;
        m_CPU.Y = 0x10;
        m_CPU.Write(0x0200, 0x00);
        m_CPU.Write(0x0201, 0x02); // This will set the address to be 0x0200
        ABY(m_CPU);
        Assert.Equal(0x0210, m_CPU.AbsoluteAddress); // 0x0200 + 0x10
        Assert.Equal(0x0202, m_CPU.PC);
    }

    [Fact]
    public void ABY_AddressCorrectly_PageBoundaryCrossed() {
        m_CPU.PC = 0x0200;
        m_CPU.Y = 0xFF;
        m_CPU.Write(0x0200, 0x01);
        m_CPU.Write(0x0201, 0x02); // This will set the address to be 0x0201
        bool pageBoundaryCrossed = ABY(m_CPU);
        Assert.True(pageBoundaryCrossed);
        Assert.Equal(0x0300, m_CPU.AbsoluteAddress); // 0x0201 + 0xFF
        Assert.Equal(0x0202, m_CPU.PC);
    }

    [Fact]
    public void IND_AddressCorrectly_WithoutBug() {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0x02);
        m_CPU.Write(0x0201, 0x02); // Points to address 0x0202
        m_CPU.Write(0x0202, 0x78);
        m_CPU.Write(0x0203, 0x56); // Address stored here is 0x5678
        IND(m_CPU);
        Assert.Equal(0x5678, m_CPU.AbsoluteAddress);
    }

    [Fact]
    public void IND_AddressCorrectly_WithBug() {
        m_CPU.PC = 0x0200;
        m_CPU.Write(0x0200, 0xFF);
        m_CPU.Write(0x0201, 0x03); // Points to address 0x03FF

        // Setting the data at the pointer's address and at the buggy wrap address
        m_CPU.Write(0x03FF, 0x78);
        m_CPU.Write(0x0400, 0x59); // Address stored here is 0x5978
        m_CPU.Write(0x0300, 0x56); // Address stored here is 0x5678

        IND(m_CPU);
        Assert.Equal(0x5678, m_CPU.AbsoluteAddress);
    }

    [Fact]
    public void IZX_AddressCorrectly() {
        m_CPU.PC = 0x0200;
        m_CPU.X = 0x02;
        m_CPU.Write(0x0200, 0x02); // Points to address 0x0004
        m_CPU.Write(0x0004, 0x78);
        m_CPU.Write(0x0005, 0x56); // Address stored here is 0x5678
        IZX(m_CPU);
        Assert.Equal(0x5678, m_CPU.AbsoluteAddress);
    }

    [Fact]
    public void IZY_AddressCorrectly_NoPageBoundary() {
        m_CPU.PC = 0x0200;
        m_CPU.Y = 0x10;
        m_CPU.Write(0x0200, 0x02); // Points to address 0x0002
        m_CPU.Write(0x0002, 0x10);
        m_CPU.Write(0x0003, 0x02); // Address stored here is 0x0210
        IZY(m_CPU);
        Assert.Equal(0x0220, m_CPU.AbsoluteAddress); // 0x0210 + 0x10
    }

    [Fact]
    public void IZY_AddressCorrectly_PageBoundaryCrossed() {
        m_CPU.PC = 0x0200;
        m_CPU.Y = 0xFF;
        m_CPU.Write(0x0200, 0x02); // Points to address 0x0002
        m_CPU.Write(0x0002, 0x02);
        m_CPU.Write(0x0003, 0x02); // Address stored here is 0x0202
        bool pageBoundaryCrossed = IZY(m_CPU);
        Assert.True(pageBoundaryCrossed);
        Assert.Equal(0x0301, m_CPU.AbsoluteAddress); // 0x0202 + 0xFF
    }
}