namespace Emulator;

public class Bus {
    public readonly Memory64K RAM = new();
    public uint64_t SystemClockCounter;

    // Devices
    private CPU? m_CPU;

    public void Connect(CPU cpu) {
        this.m_CPU = cpu;
        this.m_CPU.Connect(this);
    }

    public uint8_t Read(uint16_t address, bool readOnly = false) {
        if (address >= 0x0000 && address <= 0xFFFF) {
            return this.RAM[address];
        }

        return 0x00;
    }

    public void Write(uint16_t address, uint8_t data) {
        if (address >= 0x0000 && address <= 0xFFFF) {
            this.RAM[address] = data;
        }
    }

    public void Clock() {
        this.m_CPU!.Clock();

        this.SystemClockCounter++;
    }

    public void Reset() {
        this.m_CPU!.Reset();
        this.SystemClockCounter = 0;
    }
}