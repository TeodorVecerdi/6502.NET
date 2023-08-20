using System.Runtime.CompilerServices;

namespace Emulator;

public class Bus {
    public readonly Memory RAM = new(64 * KB);
    public uint64_t SystemClockCounter => this.m_SystemClockCounter;

    private uint64_t m_SystemClockCounter;
    // Devices
    private CPU? m_CPU;
    // private PPU m_PPU;
    // private Cartridge m_Cartridge;

    public void Connect(CPU cpu) {
        this.m_CPU = cpu;
        this.m_CPU.Connect(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public uint8_t CpuRead(uint16_t address, bool readOnly = false) {
        if (address >= 0x0000 && address <= 0xFFFF) {
            return this.RAM[address];
        }

        return 0x00;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CpuWrite(uint16_t address, uint8_t data) {
        if (address >= 0x0000 && address <= 0xFFFF) {
            this.RAM[address] = data;
        }
    }

    public void Clock() {
        this.m_CPU!.Clock();

        this.m_SystemClockCounter++;
    }

    public void Reset() {
        this.m_CPU!.Reset();
        this.m_SystemClockCounter = 0;
    }
}