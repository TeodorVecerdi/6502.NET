namespace Emulator;

public readonly struct Instruction : IEquatable<Instruction> {
    public static readonly Instruction Invalid = new((InstructionKind)0xFF, (AddressingMode)0xFF);

    public readonly InstructionKind Kind;
    public readonly AddressingMode AddressingMode;

    private Instruction(InstructionKind kind, AddressingMode addressingMode) {
        this.Kind = kind;
        this.AddressingMode = addressingMode;
    }

    public override string ToString() => $"{this.Kind.ToMnemonic()}, {this.AddressingMode.ToMnemonic()}";

    public static bool TryDecode(Byte opcode, out Instruction instruction) {
        instruction = opcode switch {
            0xEA => new Instruction(InstructionKind.NOP, AddressingMode.Implied),

            // LDA
            0xA9 => new Instruction(InstructionKind.LDA, AddressingMode.Immediate),
            0xA5 => new Instruction(InstructionKind.LDA, AddressingMode.ZeroPage),
            0xB5 => new Instruction(InstructionKind.LDA, AddressingMode.ZeroPageX),
            0xAD => new Instruction(InstructionKind.LDA, AddressingMode.Absolute),
            0xBD => new Instruction(InstructionKind.LDA, AddressingMode.AbsoluteX),
            0xB9 => new Instruction(InstructionKind.LDA, AddressingMode.AbsoluteY),
            0xA1 => new Instruction(InstructionKind.LDA, AddressingMode.IndirectX),
            0xB1 => new Instruction(InstructionKind.LDA, AddressingMode.IndirectY),

            // LDX
            0xA2 => new Instruction(InstructionKind.LDX, AddressingMode.Immediate),
            0xA6 => new Instruction(InstructionKind.LDX, AddressingMode.ZeroPage),
            0xB6 => new Instruction(InstructionKind.LDX, AddressingMode.ZeroPageY),
            0xAE => new Instruction(InstructionKind.LDX, AddressingMode.Absolute),
            0xBE => new Instruction(InstructionKind.LDX, AddressingMode.AbsoluteY),

            // LDY
            0xA0 => new Instruction(InstructionKind.LDY, AddressingMode.Immediate),
            0xA4 => new Instruction(InstructionKind.LDY, AddressingMode.ZeroPage),
            0xB4 => new Instruction(InstructionKind.LDY, AddressingMode.ZeroPageX),
            0xAC => new Instruction(InstructionKind.LDY, AddressingMode.Absolute),
            0xBC => new Instruction(InstructionKind.LDY, AddressingMode.AbsoluteX),

            // STA
            0x85 => new Instruction(InstructionKind.STA, AddressingMode.ZeroPage),
            0x95 => new Instruction(InstructionKind.STA, AddressingMode.ZeroPageX),
            0x8D => new Instruction(InstructionKind.STA, AddressingMode.Absolute),
            0x9D => new Instruction(InstructionKind.STA, AddressingMode.AbsoluteX),
            0x99 => new Instruction(InstructionKind.STA, AddressingMode.AbsoluteY),
            0x81 => new Instruction(InstructionKind.STA, AddressingMode.IndirectX),
            0x91 => new Instruction(InstructionKind.STA, AddressingMode.IndirectY),

            // STX
            0x86 => new Instruction(InstructionKind.STX, AddressingMode.ZeroPage),
            0x96 => new Instruction(InstructionKind.STX, AddressingMode.ZeroPageY),
            0x8E => new Instruction(InstructionKind.STX, AddressingMode.Absolute),

            // STY
            0x84 => new Instruction(InstructionKind.STY, AddressingMode.ZeroPage),
            0x94 => new Instruction(InstructionKind.STY, AddressingMode.ZeroPageX),
            0x8C => new Instruction(InstructionKind.STY, AddressingMode.Absolute),

            // ADC
            0x69 => new Instruction(InstructionKind.ADC, AddressingMode.Immediate),
            0x65 => new Instruction(InstructionKind.ADC, AddressingMode.ZeroPage),
            0x75 => new Instruction(InstructionKind.ADC, AddressingMode.ZeroPageX),
            0x6D => new Instruction(InstructionKind.ADC, AddressingMode.Absolute),
            0x7D => new Instruction(InstructionKind.ADC, AddressingMode.AbsoluteX),
            0x79 => new Instruction(InstructionKind.ADC, AddressingMode.AbsoluteY),
            0x61 => new Instruction(InstructionKind.ADC, AddressingMode.IndirectX),
            0x71 => new Instruction(InstructionKind.ADC, AddressingMode.IndirectY),

            _ => Invalid,
        };

        return instruction != Invalid;
    }

    public bool Equals(Instruction other) => this.Kind == other.Kind && this.AddressingMode == other.AddressingMode;
    public override bool Equals(object? obj) => obj is Instruction other && Equals(other);
    public override int GetHashCode() => HashCode.Combine((int)this.Kind, (int)this.AddressingMode);
    public static bool operator ==(Instruction left, Instruction right) => left.Equals(right);
    public static bool operator !=(Instruction left, Instruction right) => !left.Equals(right);
}