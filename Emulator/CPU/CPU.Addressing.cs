namespace Emulator;

public partial class CPU {
    internal static bool IMP(CPU cpu) {
        cpu.m_Fetched = cpu.A;
        return false;
    }

    internal static bool IMM(CPU cpu) {
        cpu.m_AbsoluteAddress = cpu.PC++;
        return false;
    }

    internal static bool ZP0(CPU cpu) {
        cpu.m_AbsoluteAddress = (uint16_t)(cpu.Read(cpu.PC++) & 0x00FF);
        return false;
    }

    internal static bool ZPX(CPU cpu) {
        cpu.m_AbsoluteAddress = (uint16_t)((cpu.Read(cpu.PC++) + cpu.X) & 0x00FF);
        return false;
    }

    internal static bool ZPY(CPU cpu) {
        cpu.m_AbsoluteAddress = (uint16_t)((cpu.Read(cpu.PC++) + cpu.Y) & 0x00FF);
        return false;
    }

    internal static bool REL(CPU cpu) {
        cpu.m_RelativeAddress = cpu.Read(cpu.PC++);
        if ((cpu.m_RelativeAddress & 0x80) != 0) {
            cpu.m_RelativeAddress |= 0xFF00;
        }

        return false;
    }

    internal static bool ABS(CPU cpu) {
        uint8_t lowByte = cpu.Read(cpu.PC++);
        uint8_t highByte = cpu.Read(cpu.PC++);
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        return false;
    }

    internal static bool ABX(CPU cpu) {
        uint8_t lowByte = cpu.Read(cpu.PC++);
        uint8_t highByte = cpu.Read(cpu.PC++);
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        cpu.m_AbsoluteAddress += cpu.X;

        return (cpu.m_AbsoluteAddress & 0xFF00) != highByte << 8;
    }

    internal static bool ABY(CPU cpu) {
        uint8_t lowByte = cpu.Read(cpu.PC++);
        uint8_t highByte = cpu.Read(cpu.PC++);
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        cpu.m_AbsoluteAddress += cpu.Y;

        return (cpu.m_AbsoluteAddress & 0xFF00) != highByte << 8;
    }

    internal static bool IND(CPU cpu) {
        uint8_t lowByte = cpu.Read(cpu.PC++);
        uint8_t highByte = cpu.Read(cpu.PC++);
        uint16_t pointer = (uint16_t)((highByte << 8) | lowByte);

        if (lowByte == 0x00FF) {
            // Page boundary hardware bug
            cpu.m_AbsoluteAddress = (uint16_t)((cpu.Read((uint16_t)(pointer & 0xFF00)) << 8) | cpu.Read(pointer));
        } else {
            cpu.m_AbsoluteAddress = (uint16_t)((cpu.Read((uint16_t)(pointer + 1)) << 8) | cpu.Read(pointer));
        }

        return false;
    }

    internal static bool IZX(CPU cpu) {
        uint16_t pointer = (uint16_t)(cpu.Read(cpu.PC++) + cpu.X);
        uint8_t lowByte = cpu.Read((uint16_t)(pointer & 0x00FF));
        uint8_t highByte = cpu.Read((uint16_t)((pointer + 1) & 0x00FF));
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        return false;
    }

    internal static bool IZY(CPU cpu) {
        uint16_t pointer = cpu.Read(cpu.PC++);
        uint8_t lowByte = cpu.Read((uint16_t)(pointer & 0x00FF));
        uint8_t highByte = cpu.Read((uint16_t)((pointer + 1) & 0x00FF));
        cpu.m_AbsoluteAddress = (uint16_t)((highByte << 8) | lowByte);
        cpu.m_AbsoluteAddress += cpu.Y;

        return (cpu.m_AbsoluteAddress & 0xFF00) != highByte << 8;
    }
}