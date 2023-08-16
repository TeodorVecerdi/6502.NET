using System.Runtime.CompilerServices;

namespace Emulator;

public enum AddressingMode : uint8_t {
    IMP,
    IMM,
    ZP0,
    ZPX,
    ZPY,
    REL,
    ABS,
    ABX,
    ABY,
    IND,
    IZX,
    IZY,
}

internal static class AddressingModeExtensions {
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static string ToDescription(this AddressingMode mode) => mode switch {
        AddressingMode.IMP => "Implied",
        AddressingMode.IMM => "Immediate",
        AddressingMode.ZP0 => "ZeroPage",
        AddressingMode.ZPX => "ZeroPage,X",
        AddressingMode.ZPY => "ZeroPage,Y",
        AddressingMode.REL => "Relative",
        AddressingMode.ABS => "Absolute",
        AddressingMode.ABX => "Absolute,X",
        AddressingMode.ABY => "Absolute,Y",
        AddressingMode.IND => "Indirect",
        AddressingMode.IZX => "(Indirect,X)",
        AddressingMode.IZY => "(Indirect),Y",
        _ => throw new NotImplementedException(),
    };
}