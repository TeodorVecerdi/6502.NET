namespace Emulator;

public readonly unsafe struct Instruction {
    public readonly InstructionKind Name;
    public readonly AddressingMode AddressingMode;
    public readonly uint8_t Cycles;
    public readonly delegate*<CPU, bool> Execute;
    public readonly delegate*<CPU, bool> Address;

    public Instruction(InstructionKind name, AddressingMode addressingMode, uint8_t cycles, delegate*<CPU, bool> execute, delegate*<CPU, bool> address) {
        this.Name = name;
        this.AddressingMode = addressingMode;
        this.Cycles = cycles;
        this.Execute = execute;
        this.Address = address;
    }
}