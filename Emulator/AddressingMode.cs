using System.Runtime.CompilerServices;

namespace Emulator;

public enum AddressingMode : uint8_t {
    Implied = 0x00,
    Immediate = 0x01,
    ZeroPage = 0x02,
    ZeroPageX = 0x03,
    ZeroPageY = 0x04,
    Absolute = 0x05,
    AbsoluteX = 0x06,
    AbsoluteY = 0x07,
    Indirect = 0x08,
    IndirectX = 0x09,
    IndirectY = 0x0A,
    Relative = 0x0B,
}

internal static class AddressingModeExtensions {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToMnemonic(this AddressingMode mode) => mode switch {
        AddressingMode.Implied => "Implied",
        AddressingMode.Immediate => "Immediate",
        AddressingMode.ZeroPage => "ZeroPage",
        AddressingMode.ZeroPageX => "ZeroPage,X",
        AddressingMode.ZeroPageY => "ZeroPage,Y",
        AddressingMode.Absolute => "Absolute",
        AddressingMode.AbsoluteX => "Absolute,X",
        AddressingMode.AbsoluteY => "Absolute,Y",
        AddressingMode.Indirect => "Indirect",
        AddressingMode.IndirectX => "(Indirect,X)",
        AddressingMode.IndirectY => "(Indirect),Y",
        AddressingMode.Relative => "Relative",
        _ => throw new NotImplementedException(),
    };
}