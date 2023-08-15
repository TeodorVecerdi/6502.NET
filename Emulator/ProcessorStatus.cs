using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Emulator;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct ProcessorStatus : IFormattable {
    private uint8_t m_Flags;

    public bool Carry {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (m_Flags & 0x01) != 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => m_Flags = (byte)(value ? m_Flags | 0x01 : m_Flags & ~0x01);
    }

    public bool Zero {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (m_Flags & 0x02) != 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => m_Flags = (byte)(value ? m_Flags | 0x02 : m_Flags & ~0x02);
    }

    public bool InterruptDisable {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (m_Flags & 0x04) != 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => m_Flags = (byte)(value ? m_Flags | 0x04 : m_Flags & ~0x04);
    }

    public bool DecimalMode {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (m_Flags & 0x08) != 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => m_Flags = (byte)(value ? m_Flags | 0x08 : m_Flags & ~0x08);
    }

    public bool BreakCommand {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (m_Flags & 0x10) != 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => m_Flags = (byte)(value ? m_Flags | 0x10 : m_Flags & ~0x10);
    }

    // There's an unused bit here, represented in some documentation as `U` for "Unused".
    // It's always set and we won't represent it in this struct.

    public bool Overflow {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (m_Flags & 0x40) != 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => m_Flags = (byte)(value ? m_Flags | 0x40 : m_Flags & ~0x40);
    }

    public bool Negative {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (m_Flags & 0x80) != 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => m_Flags = (byte)(value ? m_Flags | 0x80 : m_Flags & ~0x80);
    }

    public override string ToString() => ToString("F");

    public string ToString(string? format, IFormatProvider? formatProvider = null) {
        if (string.IsNullOrEmpty(format)) format = "F";

        return format.ToUpperInvariant() switch {
            "S" => $"C Z I D B - V N\n{(this.Carry ? '1' : '0')} {(this.Zero ? '1' : '0')} {(this.InterruptDisable ? '1' : '0')} {(this.DecimalMode ? '1' : '0')} {(this.BreakCommand ? '1' : '0')} 1 {(this.Overflow ? '1' : '0')} {(this.Negative ? '1' : '0')}",
            "F" => $"{{\n    Carry = {this.Carry},\n    Zero = {this.Zero},\n    InterruptDisable = {this.InterruptDisable},\n    DecimalMode = {this.DecimalMode},\n    BreakCommand = {this.BreakCommand},\n    Overflow = {this.Overflow},\n    Negative = {this.Negative}\n}}",
            _ => this.m_Flags.ToString(format, formatProvider),
        };
    }
}