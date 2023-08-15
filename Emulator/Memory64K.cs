using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Emulator;

[StructLayout(LayoutKind.Sequential)]
public readonly partial struct Memory64K {
    private const int32_t SIZE = 1024 * 64;
    private readonly Byte[] m_Data = new Byte[SIZE];

    public Memory64K() {}

    public Byte this[Word address] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.m_Data[address];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this.m_Data[address] = value;
    }

    private Byte this[int address] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.m_Data[address];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this.m_Data[address] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Byte ReadByte(Word address) => this[address];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Word ReadWord(Word address) => (Word)(this[address] | (this[address + 1] << 8));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteByte(Word address, Byte value) => this[address] = value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteWord(Word address, Word value) {
        this[address] = (Byte)(value & 0xFF);
        this[address + 1] = (Byte)(value >> 8);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteBlock(Word startingAddress, params Byte[] data) => data.CopyTo(this.m_Data, startingAddress);
}