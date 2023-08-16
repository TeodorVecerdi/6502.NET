using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Emulator;

[StructLayout(LayoutKind.Sequential)]
public readonly partial struct Memory64K {
    private const int32_t SIZE = 64 * 1024;
    private readonly uint8_t[] m_Data = new uint8_t[SIZE];

    public ReadOnlySpan<uint8_t> Data => this.m_Data;

    public Memory64K() {}

    public uint8_t this[uint16_t address] {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => this.m_Data[address];
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        set => this.m_Data[address] = value;
    }

    private uint8_t this[int32_t address] {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => this.m_Data[address];
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        set => this.m_Data[address] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public uint8_t Read(uint16_t address) => this[address];

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public uint16_t Read16(uint16_t address) {
        return (uint16_t)(this[address] | (this[(uint8_t)(address + 1)] << 8));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Write(uint16_t address, uint8_t value) => this[address] = value;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Write16(uint16_t address, uint16_t value) {
        this[address] = (uint8_t)(value & 0xFF);
        this[(uint8_t)(address + 1)] = (uint8_t)(value >> 8);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void WriteBlock(uint16_t offset, params uint8_t[] data) => data.CopyTo(this.m_Data, offset);
}