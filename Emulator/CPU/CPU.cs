namespace Emulator;

public partial class CPU {
    public uint16_t PC;
    public uint8_t SP;
    public uint8_t A;
    public uint8_t X;
    public uint8_t Y;
    public ProcessorStatus Status;

    private Bus? m_Bus;

    private uint8_t m_Fetched;
    private uint16_t m_Temp;
    private uint16_t m_AbsoluteAddress;
    private uint16_t m_RelativeAddress;
    private uint8_t m_Opcode;
    private uint16_t m_OpcodeAddress;
    private uint8_t m_Cycles;
    private uint64_t m_ClockCount;

    public uint16_t OpcodeAddress => this.m_OpcodeAddress;

    internal uint8_t Fetched => this.m_Fetched;
    internal uint16_t Temp => this.m_Temp;
    internal uint16_t AbsoluteAddress => this.m_AbsoluteAddress;
    internal uint16_t RelativeAddress => this.m_RelativeAddress;
    internal uint8_t Opcode {
        get => this.m_Opcode;
        set => this.m_Opcode = value;
    }
    internal uint8_t Cycles => this.m_Cycles;
    internal uint64_t ClockCount => this.m_ClockCount;
}