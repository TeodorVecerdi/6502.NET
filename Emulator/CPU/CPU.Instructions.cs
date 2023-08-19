namespace Emulator;

public partial class CPU {
    internal static bool ADC(CPU cpu) {
        cpu.Fetch();

        cpu.m_Temp = (uint16_t)(cpu.A + cpu.m_Fetched + (uint16_t)(cpu.Status.Carry ? 1 : 0));

        cpu.Status.Carry = cpu.m_Temp > 0xFF;
        cpu.Status.Zero = (cpu.m_Temp & 0x00FF) == 0;
        cpu.Status.Overflow = (~(cpu.A ^ cpu.m_Fetched) & (cpu.A ^ cpu.m_Temp) & 0x0080) != 0;
        cpu.Status.Negative = (cpu.m_Temp & 0x80) != 0;

        cpu.A = (uint8_t)(cpu.m_Temp & 0x00FF);

        return true;
    }

    internal static bool AND(CPU cpu) => NotImplemented<bool>("AND");
    internal static bool ASL(CPU cpu) => NotImplemented<bool>("ASL");
    internal static bool BCC(CPU cpu) => NotImplemented<bool>("BCC");
    internal static bool BCS(CPU cpu) => NotImplemented<bool>("BCS");
    internal static bool BEQ(CPU cpu) => NotImplemented<bool>("BEQ");
    internal static bool BIT(CPU cpu) => NotImplemented<bool>("BIT");
    internal static bool BMI(CPU cpu) => NotImplemented<bool>("BMI");
    internal static bool BNE(CPU cpu) => NotImplemented<bool>("BNE");
    internal static bool BPL(CPU cpu) => NotImplemented<bool>("BPL");
    internal static bool BRK(CPU cpu) => NotImplemented<bool>("BRK");
    internal static bool BVC(CPU cpu) => NotImplemented<bool>("BVC");
    internal static bool BVS(CPU cpu) => NotImplemented<bool>("BVS");
    internal static bool CLC(CPU cpu) => NotImplemented<bool>("CLC");
    internal static bool CLD(CPU cpu) => NotImplemented<bool>("CLD");
    internal static bool CLI(CPU cpu) => NotImplemented<bool>("CLI");
    internal static bool CLV(CPU cpu) => NotImplemented<bool>("CLV");

    internal static bool CMP(CPU cpu) {
        cpu.Fetch();

        cpu.m_Temp = (uint16_t)(cpu.A - cpu.m_Fetched);
        cpu.Status.Carry = cpu.A >= cpu.m_Fetched;
        cpu.Status.Zero = (cpu.m_Temp & 0x00FF) == 0;
        cpu.Status.Negative = (cpu.m_Temp & 0x80) != 0;

        return true;
    }

    internal static bool CPX(CPU cpu) {
        cpu.Fetch();

        cpu.m_Temp = (uint16_t)(cpu.X - cpu.m_Fetched);
        cpu.Status.Carry = cpu.X >= cpu.m_Fetched;
        cpu.Status.Zero = (cpu.m_Temp & 0x00FF) == 0;
        cpu.Status.Negative = (cpu.m_Temp & 0x80) != 0;

        return false;
    }

    internal static bool CPY(CPU cpu) {
        cpu.Fetch();

        cpu.m_Temp = (uint16_t)(cpu.Y - cpu.m_Fetched);
        cpu.Status.Carry = cpu.Y >= cpu.m_Fetched;
        cpu.Status.Zero = (cpu.m_Temp & 0x00FF) == 0;
        cpu.Status.Negative = (cpu.m_Temp & 0x80) != 0;

        return false;
    }

    internal static bool DEC(CPU cpu) {
        cpu.Fetch();

        cpu.m_Temp = (uint16_t)(cpu.m_Fetched - 1);
        cpu.Write(cpu.m_AbsoluteAddress, (uint8_t)(cpu.m_Temp & 0x00FF));
        cpu.Status.Zero = (cpu.m_Temp & 0x00FF) == 0;
        cpu.Status.Negative = (cpu.m_Temp & 0x80) != 0;

        return false;
    }

    internal static bool DEX(CPU cpu) {
        cpu.X--;
        cpu.Status.Zero = cpu.X == 0x00;
        cpu.Status.Negative = (cpu.X & 0x80) != 0;
        return false;
    }

    internal static bool DEY(CPU cpu) {
        cpu.Y--;
        cpu.Status.Zero = cpu.Y == 0x00;
        cpu.Status.Negative = (cpu.Y & 0x80) != 0;
        return false;
    }

    internal static bool EOR(CPU cpu) => NotImplemented<bool>("EOR");
    internal static bool INC(CPU cpu) {
        cpu.Fetch();

        cpu.m_Temp = (uint16_t)(cpu.m_Fetched + 1);
        cpu.Write(cpu.m_AbsoluteAddress, (uint8_t)(cpu.m_Temp & 0x00FF));
        cpu.Status.Zero = (cpu.m_Temp & 0x00FF) == 0;
        cpu.Status.Negative = (cpu.m_Temp & 0x80) != 0;

        return false;
    }

    internal static bool INX(CPU cpu) {
        cpu.X++;
        cpu.Status.Zero = cpu.X == 0x00;
        cpu.Status.Negative = (cpu.X & 0x80) != 0;
        return false;
    }

    internal static bool INY(CPU cpu) {
        cpu.Y++;
        cpu.Status.Zero = cpu.Y == 0x00;
        cpu.Status.Negative = (cpu.Y & 0x80) != 0;
        return false;
    }

    internal static bool JMP(CPU cpu) => NotImplemented<bool>("JMP");
    internal static bool JSR(CPU cpu) => NotImplemented<bool>("JSR");

    internal static bool LDA(CPU cpu) {
        cpu.A = cpu.Fetch();
        cpu.Status.Zero = cpu.A == 0x00;
        cpu.Status.Negative = (cpu.A & 0x80) != 0;
        return true;
    }

    internal static bool LDX(CPU cpu) {
        cpu.X = cpu.Fetch();
        cpu.Status.Zero = cpu.X == 0x00;
        cpu.Status.Negative = (cpu.X & 0x80) != 0;
        return true;
    }

    internal static bool LDY(CPU cpu) {
        cpu.Y = cpu.Fetch();
        cpu.Status.Zero = cpu.Y == 0x00;
        cpu.Status.Negative = (cpu.Y & 0x80) != 0;
        return true;
    }

    internal static bool LSR(CPU cpu) => NotImplemented<bool>("LSR");
    internal static bool NOP(CPU cpu) {
        // https://github.com/OneLoneCoder/olcNES/blob/master/Part%232%20-%20CPU/olc6502.cpp#L1192
        switch (cpu.m_Opcode) {
            case 0x1C:
            case 0x3C:
            case 0x5C:
            case 0x7C:
            case 0xDC:
            case 0xFC:
                return true;
        }

        return false;
    }

    internal static bool ORA(CPU cpu) => NotImplemented<bool>("ORA");
    internal static bool PHA(CPU cpu) => NotImplemented<bool>("PHA");
    internal static bool PHP(CPU cpu) => NotImplemented<bool>("PHP");
    internal static bool PLA(CPU cpu) => NotImplemented<bool>("PLA");
    internal static bool PLP(CPU cpu) => NotImplemented<bool>("PLP");
    internal static bool ROL(CPU cpu) => NotImplemented<bool>("ROL");
    internal static bool ROR(CPU cpu) => NotImplemented<bool>("ROR");
    internal static bool RTI(CPU cpu) => NotImplemented<bool>("RTI");
    internal static bool RTS(CPU cpu) => NotImplemented<bool>("RTS");

    internal static bool SBC(CPU cpu) {
        cpu.Fetch();

        // Using 2's complement arithmetic, subtraction is equivalent to adding the inverse.
        // Subtracting a byte B from A is same as A + ~B + 1
        // So we invert the fetched byte and treat the carry flag as the +1.
        uint16_t value = (uint16_t)(cpu.m_Fetched ^ 0x00FF);

        // Then we can use similar logic to ADC for the rest of the operation.
        cpu.m_Temp = (uint16_t)(cpu.A + value + (uint16_t)(cpu.Status.Carry ? 1 : 0));
        cpu.Status.Carry = (cpu.m_Temp & 0xFF00) != 0;
        cpu.Status.Zero = (cpu.m_Temp & 0x00FF) == 0;
        cpu.Status.Overflow = (~(cpu.A ^ value) & (cpu.A ^ cpu.m_Temp) & 0x0080) != 0;
        cpu.Status.Negative = (cpu.m_Temp & 0x80) != 0;

        cpu.A = (uint8_t)(cpu.m_Temp & 0x00FF);

        return true;
    }

    internal static bool SEC(CPU cpu) => NotImplemented<bool>("SEC");
    internal static bool SED(CPU cpu) => NotImplemented<bool>("SED");
    internal static bool SEI(CPU cpu) => NotImplemented<bool>("SEI");

    internal static bool STA(CPU cpu) {
        cpu.Write(cpu.m_AbsoluteAddress, cpu.A);
        return false;
    }

    internal static bool STX(CPU cpu) {
        cpu.Write(cpu.m_AbsoluteAddress, cpu.X);
        return false;
    }

    internal static bool STY(CPU cpu) {
        cpu.Write(cpu.m_AbsoluteAddress, cpu.Y);
        return false;
    }

    internal static bool TAX(CPU cpu) => NotImplemented<bool>("TAX");
    internal static bool TAY(CPU cpu) => NotImplemented<bool>("TAY");
    internal static bool TSX(CPU cpu) => NotImplemented<bool>("TSX");
    internal static bool TXA(CPU cpu) => NotImplemented<bool>("TXA");
    internal static bool TXS(CPU cpu) => NotImplemented<bool>("TXS");
    internal static bool TYA(CPU cpu) => NotImplemented<bool>("TYA");

    // Illegal/unofficial opcodes
    internal static bool XXX(CPU cpu) => false;

    public static readonly unsafe Instruction[] InstructionLookupTable = {
        new(InstructionKind.BRK, AddressingMode.IMM, 7, &BRK, &IMM), new(InstructionKind.ORA, AddressingMode.IZX, 6, &ORA, &IZX), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 3, &NOP, &IMP), new(InstructionKind.ORA, AddressingMode.ZP0, 3, &ORA, &ZP0), new(InstructionKind.ASL, AddressingMode.ZP0, 5, &ASL, &ZP0), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), new(InstructionKind.PHP, AddressingMode.IMP, 3, &PHP, &IMP), new(InstructionKind.ORA, AddressingMode.IMM, 2, &ORA, &IMM), new(InstructionKind.ASL, AddressingMode.IMP, 2, &ASL, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.ORA, AddressingMode.ABS, 4, &ORA, &ABS), new(InstructionKind.ASL, AddressingMode.ABS, 6, &ASL, &ABS), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), 
        new(InstructionKind.BPL, AddressingMode.REL, 2, &BPL, &REL), new(InstructionKind.ORA, AddressingMode.IZY, 5, &ORA, &IZY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.ORA, AddressingMode.ZPX, 4, &ORA, &ZPX), new(InstructionKind.ASL, AddressingMode.ZPX, 6, &ASL, &ZPX), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), new(InstructionKind.CLC, AddressingMode.IMP, 2, &CLC, &IMP), new(InstructionKind.ORA, AddressingMode.ABY, 4, &ORA, &ABY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.ORA, AddressingMode.ABX, 4, &ORA, &ABX), new(InstructionKind.ASL, AddressingMode.ABX, 7, &ASL, &ABX), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), 
        new(InstructionKind.JSR, AddressingMode.ABS, 6, &JSR, &ABS), new(InstructionKind.AND, AddressingMode.IZX, 6, &AND, &IZX), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.BIT, AddressingMode.ZP0, 3, &BIT, &ZP0), new(InstructionKind.AND, AddressingMode.ZP0, 3, &AND, &ZP0), new(InstructionKind.ROL, AddressingMode.ZP0, 5, &ROL, &ZP0), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), new(InstructionKind.PLP, AddressingMode.IMP, 4, &PLP, &IMP), new(InstructionKind.AND, AddressingMode.IMM, 2, &AND, &IMM), new(InstructionKind.ROL, AddressingMode.IMP, 2, &ROL, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.BIT, AddressingMode.ABS, 4, &BIT, &ABS), new(InstructionKind.AND, AddressingMode.ABS, 4, &AND, &ABS), new(InstructionKind.ROL, AddressingMode.ABS, 6, &ROL, &ABS), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), 
        new(InstructionKind.BMI, AddressingMode.REL, 2, &BMI, &REL), new(InstructionKind.AND, AddressingMode.IZY, 5, &AND, &IZY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.AND, AddressingMode.ZPX, 4, &AND, &ZPX), new(InstructionKind.ROL, AddressingMode.ZPX, 6, &ROL, &ZPX), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), new(InstructionKind.SEC, AddressingMode.IMP, 2, &SEC, &IMP), new(InstructionKind.AND, AddressingMode.ABY, 4, &AND, &ABY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.AND, AddressingMode.ABX, 4, &AND, &ABX), new(InstructionKind.ROL, AddressingMode.ABX, 7, &ROL, &ABX), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), 
        new(InstructionKind.RTI, AddressingMode.IMP, 6, &RTI, &IMP), new(InstructionKind.EOR, AddressingMode.IZX, 6, &EOR, &IZX), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 3, &NOP, &IMP), new(InstructionKind.EOR, AddressingMode.ZP0, 3, &EOR, &ZP0), new(InstructionKind.LSR, AddressingMode.ZP0, 5, &LSR, &ZP0), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), new(InstructionKind.PHA, AddressingMode.IMP, 3, &PHA, &IMP), new(InstructionKind.EOR, AddressingMode.IMM, 2, &EOR, &IMM), new(InstructionKind.LSR, AddressingMode.IMP, 2, &LSR, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.JMP, AddressingMode.ABS, 3, &JMP, &ABS), new(InstructionKind.EOR, AddressingMode.ABS, 4, &EOR, &ABS), new(InstructionKind.LSR, AddressingMode.ABS, 6, &LSR, &ABS), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), 
        new(InstructionKind.BVC, AddressingMode.REL, 2, &BVC, &REL), new(InstructionKind.EOR, AddressingMode.IZY, 5, &EOR, &IZY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.EOR, AddressingMode.ZPX, 4, &EOR, &ZPX), new(InstructionKind.LSR, AddressingMode.ZPX, 6, &LSR, &ZPX), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), new(InstructionKind.CLI, AddressingMode.IMP, 2, &CLI, &IMP), new(InstructionKind.EOR, AddressingMode.ABY, 4, &EOR, &ABY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.EOR, AddressingMode.ABX, 4, &EOR, &ABX), new(InstructionKind.LSR, AddressingMode.ABX, 7, &LSR, &ABX), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), 
        new(InstructionKind.RTS, AddressingMode.IMP, 6, &RTS, &IMP), new(InstructionKind.ADC, AddressingMode.IZX, 6, &ADC, &IZX), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 3, &NOP, &IMP), new(InstructionKind.ADC, AddressingMode.ZP0, 3, &ADC, &ZP0), new(InstructionKind.ROR, AddressingMode.ZP0, 5, &ROR, &ZP0), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), new(InstructionKind.PLA, AddressingMode.IMP, 4, &PLA, &IMP), new(InstructionKind.ADC, AddressingMode.IMM, 2, &ADC, &IMM), new(InstructionKind.ROR, AddressingMode.IMP, 2, &ROR, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.JMP, AddressingMode.IND, 5, &JMP, &IND), new(InstructionKind.ADC, AddressingMode.ABS, 4, &ADC, &ABS), new(InstructionKind.ROR, AddressingMode.ABS, 6, &ROR, &ABS), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), 
        new(InstructionKind.BVS, AddressingMode.REL, 2, &BVS, &REL), new(InstructionKind.ADC, AddressingMode.IZY, 5, &ADC, &IZY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.ADC, AddressingMode.ZPX, 4, &ADC, &ZPX), new(InstructionKind.ROR, AddressingMode.ZPX, 6, &ROR, &ZPX), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), new(InstructionKind.SEI, AddressingMode.IMP, 2, &SEI, &IMP), new(InstructionKind.ADC, AddressingMode.ABY, 4, &ADC, &ABY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.ADC, AddressingMode.ABX, 4, &ADC, &ABX), new(InstructionKind.ROR, AddressingMode.ABX, 7, &ROR, &ABX), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), 
        new(InstructionKind.XXX, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.STA, AddressingMode.IZX, 6, &STA, &IZX), new(InstructionKind.XXX, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), new(InstructionKind.STY, AddressingMode.ZP0, 3, &STY, &ZP0), new(InstructionKind.STA, AddressingMode.ZP0, 3, &STA, &ZP0), new(InstructionKind.STX, AddressingMode.ZP0, 3, &STX, &ZP0), new(InstructionKind.XXX, AddressingMode.IMP, 3, &XXX, &IMP), new(InstructionKind.DEY, AddressingMode.IMP, 2, &DEY, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.TXA, AddressingMode.IMP, 2, &TXA, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.STY, AddressingMode.ABS, 4, &STY, &ABS), new(InstructionKind.STA, AddressingMode.ABS, 4, &STA, &ABS), new(InstructionKind.STX, AddressingMode.ABS, 4, &STX, &ABS), new(InstructionKind.XXX, AddressingMode.IMP, 4, &XXX, &IMP), 
        new(InstructionKind.BCC, AddressingMode.REL, 2, &BCC, &REL), new(InstructionKind.STA, AddressingMode.IZY, 6, &STA, &IZY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), new(InstructionKind.STY, AddressingMode.ZPX, 4, &STY, &ZPX), new(InstructionKind.STA, AddressingMode.ZPX, 4, &STA, &ZPX), new(InstructionKind.STX, AddressingMode.ZPY, 4, &STX, &ZPY), new(InstructionKind.XXX, AddressingMode.IMP, 4, &XXX, &IMP), new(InstructionKind.TYA, AddressingMode.IMP, 2, &TYA, &IMP), new(InstructionKind.STA, AddressingMode.ABY, 5, &STA, &ABY), new(InstructionKind.TXS, AddressingMode.IMP, 2, &TXS, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 5, &NOP, &IMP), new(InstructionKind.STA, AddressingMode.ABX, 5, &STA, &ABX), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), 
        new(InstructionKind.LDY, AddressingMode.IMM, 2, &LDY, &IMM), new(InstructionKind.LDA, AddressingMode.IZX, 6, &LDA, &IZX), new(InstructionKind.LDX, AddressingMode.IMM, 2, &LDX, &IMM), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), new(InstructionKind.LDY, AddressingMode.ZP0, 3, &LDY, &ZP0), new(InstructionKind.LDA, AddressingMode.ZP0, 3, &LDA, &ZP0), new(InstructionKind.LDX, AddressingMode.ZP0, 3, &LDX, &ZP0), new(InstructionKind.XXX, AddressingMode.IMP, 3, &XXX, &IMP), new(InstructionKind.TAY, AddressingMode.IMP, 2, &TAY, &IMP), new(InstructionKind.LDA, AddressingMode.IMM, 2, &LDA, &IMM), new(InstructionKind.TAX, AddressingMode.IMP, 2, &TAX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.LDY, AddressingMode.ABS, 4, &LDY, &ABS), new(InstructionKind.LDA, AddressingMode.ABS, 4, &LDA, &ABS), new(InstructionKind.LDX, AddressingMode.ABS, 4, &LDX, &ABS), new(InstructionKind.XXX, AddressingMode.IMP, 4, &XXX, &IMP), 
        new(InstructionKind.BCS, AddressingMode.REL, 2, &BCS, &REL), new(InstructionKind.LDA, AddressingMode.IZY, 5, &LDA, &IZY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), new(InstructionKind.LDY, AddressingMode.ZPX, 4, &LDY, &ZPX), new(InstructionKind.LDA, AddressingMode.ZPX, 4, &LDA, &ZPX), new(InstructionKind.LDX, AddressingMode.ZPY, 4, &LDX, &ZPY), new(InstructionKind.XXX, AddressingMode.IMP, 4, &XXX, &IMP), new(InstructionKind.CLV, AddressingMode.IMP, 2, &CLV, &IMP), new(InstructionKind.LDA, AddressingMode.ABY, 4, &LDA, &ABY), new(InstructionKind.TSX, AddressingMode.IMP, 2, &TSX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &XXX, &IMP), new(InstructionKind.LDY, AddressingMode.ABX, 4, &LDY, &ABX), new(InstructionKind.LDA, AddressingMode.ABX, 4, &LDA, &ABX), new(InstructionKind.LDX, AddressingMode.ABY, 4, &LDX, &ABY), new(InstructionKind.XXX, AddressingMode.IMP, 4, &XXX, &IMP), 
        new(InstructionKind.CPY, AddressingMode.IMM, 2, &CPY, &IMM), new(InstructionKind.CMP, AddressingMode.IZX, 6, &CMP, &IZX), new(InstructionKind.XXX, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.CPY, AddressingMode.ZP0, 3, &CPY, &ZP0), new(InstructionKind.CMP, AddressingMode.ZP0, 3, &CMP, &ZP0), new(InstructionKind.DEC, AddressingMode.ZP0, 5, &DEC, &ZP0), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), new(InstructionKind.INY, AddressingMode.IMP, 2, &INY, &IMP), new(InstructionKind.CMP, AddressingMode.IMM, 2, &CMP, &IMM), new(InstructionKind.DEX, AddressingMode.IMP, 2, &DEX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.CPY, AddressingMode.ABS, 4, &CPY, &ABS), new(InstructionKind.CMP, AddressingMode.ABS, 4, &CMP, &ABS), new(InstructionKind.DEC, AddressingMode.ABS, 6, &DEC, &ABS), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), 
        new(InstructionKind.BNE, AddressingMode.REL, 2, &BNE, &REL), new(InstructionKind.CMP, AddressingMode.IZY, 5, &CMP, &IZY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.CMP, AddressingMode.ZPX, 4, &CMP, &ZPX), new(InstructionKind.DEC, AddressingMode.ZPX, 6, &DEC, &ZPX), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), new(InstructionKind.CLD, AddressingMode.IMP, 2, &CLD, &IMP), new(InstructionKind.CMP, AddressingMode.ABY, 4, &CMP, &ABY), new(InstructionKind.NOP, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.CMP, AddressingMode.ABX, 4, &CMP, &ABX), new(InstructionKind.DEC, AddressingMode.ABX, 7, &DEC, &ABX), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), 
        new(InstructionKind.CPX, AddressingMode.IMM, 2, &CPX, &IMM), new(InstructionKind.SBC, AddressingMode.IZX, 6, &SBC, &IZX), new(InstructionKind.XXX, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.CPX, AddressingMode.ZP0, 3, &CPX, &ZP0), new(InstructionKind.SBC, AddressingMode.ZP0, 3, &SBC, &ZP0), new(InstructionKind.INC, AddressingMode.ZP0, 5, &INC, &ZP0), new(InstructionKind.XXX, AddressingMode.IMP, 5, &XXX, &IMP), new(InstructionKind.INX, AddressingMode.IMP, 2, &INX, &IMP), new(InstructionKind.SBC, AddressingMode.IMM, 2, &SBC, &IMM), new(InstructionKind.NOP, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 2, &SBC, &IMP), new(InstructionKind.CPX, AddressingMode.ABS, 4, &CPX, &ABS), new(InstructionKind.SBC, AddressingMode.ABS, 4, &SBC, &ABS), new(InstructionKind.INC, AddressingMode.ABS, 6, &INC, &ABS), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), 
        new(InstructionKind.BEQ, AddressingMode.REL, 2, &BEQ, &REL), new(InstructionKind.SBC, AddressingMode.IZY, 5, &SBC, &IZY), new(InstructionKind.XXX, AddressingMode.IMP, 2, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 8, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.SBC, AddressingMode.ZPX, 4, &SBC, &ZPX), new(InstructionKind.INC, AddressingMode.ZPX, 6, &INC, &ZPX), new(InstructionKind.XXX, AddressingMode.IMP, 6, &XXX, &IMP), new(InstructionKind.SED, AddressingMode.IMP, 2, &SED, &IMP), new(InstructionKind.SBC, AddressingMode.ABY, 4, &SBC, &ABY), new(InstructionKind.NOP, AddressingMode.IMP, 2, &NOP, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), new(InstructionKind.XXX, AddressingMode.IMP, 4, &NOP, &IMP), new(InstructionKind.SBC, AddressingMode.ABX, 4, &SBC, &ABX), new(InstructionKind.INC, AddressingMode.ABX, 7, &INC, &ABX), new(InstructionKind.XXX, AddressingMode.IMP, 7, &XXX, &IMP), 
    };
}