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

    public ushort OpcodeAddress => this.m_OpcodeAddress;
}