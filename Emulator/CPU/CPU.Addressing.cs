namespace Emulator;

public partial class CPU {
    private static bool IMP(CPU cpu) {
        cpu.m_Fetched = cpu.A;
        return false;
    }

    private static bool IMM(CPU cpu) {
        cpu.m_AbsoluteAddress = cpu.PC++;
        return false;
    }

    private static bool ZP0(CPU cpu) {
        cpu.m_AbsoluteAddress = cpu.Read(cpu.PC++);
        cpu.m_AbsoluteAddress &= 0x00FF;
        return false;
    }

    private static bool ZPX(CPU cpu) {
        cpu.m_AbsoluteAddress = (uint16_t)(cpu.Read(cpu.PC++) + cpu.X);
        cpu.m_AbsoluteAddress &= 0x00FF;
        return false;
    }

    private static bool ZPY(CPU cpu) {
        cpu.m_AbsoluteAddress = (uint16_t)(cpu.Read(cpu.PC++) + cpu.Y);
        cpu.m_AbsoluteAddress &= 0x00FF;
        return false;
    }

    private static bool REL(CPU cpu) {
        cpu.m_RelativeAddress = cpu.Read(cpu.PC++);
        if ((cpu.m_RelativeAddress & 0x80) != 0) {
            cpu.m_RelativeAddress |= 0xFF00;
        }

        return false;
    }

    private static bool ABS(CPU cpu) {
        uint16_t lowByte = cpu.Read(cpu.PC++);
        uint16_t highByte = cpu.Read(cpu.PC++);
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        return false;
    }

    private static bool ABX(CPU cpu) {
        uint16_t lowByte = cpu.Read(cpu.PC++);
        uint16_t highByte = cpu.Read(cpu.PC++);
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        cpu.m_AbsoluteAddress += cpu.X;

        return (cpu.m_AbsoluteAddress & 0xFF00) != highByte << 8;
    }

    private static bool ABY(CPU cpu) {
        uint16_t lowByte = cpu.Read(cpu.PC++);
        uint16_t highByte = cpu.Read(cpu.PC++);
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        cpu.m_AbsoluteAddress += cpu.Y;

        return (cpu.m_AbsoluteAddress & 0xFF00) != highByte << 8;
    }

    private static bool IND(CPU cpu) {
        uint16_t lowByte = cpu.Read(cpu.PC++);
        uint16_t highByte = cpu.Read(cpu.PC++);
        uint16_t pointer = (uint16_t)((highByte << 8) | lowByte);

        if (lowByte == 0x00FF) {
            // Page boundary hardware bug
            cpu.m_AbsoluteAddress = (uint16_t)((cpu.Read((uint16_t)(pointer & 0xFF00)) << 8) | cpu.Read(pointer));
        } else {
            cpu.m_AbsoluteAddress = (uint16_t)((cpu.Read((uint16_t)(pointer + 1)) << 8) | cpu.Read(pointer));
        }

        return false;
    }

    private static bool IZX(CPU cpu) {
        uint16_t pointer = (uint16_t)(cpu.Read(cpu.PC++) + cpu.X);
        uint16_t lowByte = cpu.Read((uint16_t)(pointer & 0x00FF));
        uint16_t highByte = cpu.Read((uint16_t)((pointer + 1) & 0x00FF));
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        return false;
    }

    private static bool IZY(CPU cpu) {
        uint16_t pointer = cpu.Read(cpu.PC++);
        uint16_t lowByte = cpu.Read((uint16_t)(pointer & 0x00FF));
        uint16_t highByte = cpu.Read((uint16_t)((pointer + 1) & 0x00FF));
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        cpu.m_AbsoluteAddress += cpu.Y;

        return (cpu.m_AbsoluteAddress & 0xFF00) != highByte << 8;
    }
}