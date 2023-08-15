using System.Runtime.InteropServices;

namespace Emulator;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public ref partial struct CPU {
    [FieldOffset(0)]
    public Word PC;

    [FieldOffset(2)]
    public Byte SP;

    [FieldOffset(3)]
    public Byte A;

    [FieldOffset(4)]
    public Byte X;

    [FieldOffset(5)]
    public Byte Y;

    [FieldOffset(6)]
    public ProcessorStatus Status;
}