namespace Emulator;

public ref partial struct CPU {
    public int32_t Execute(int32_t cycles, ref Memory64K memory) {
        while (cycles > 0) {
            Byte opcode = FetchByte(AddressingMode.Immediate, ref cycles, ref this, ref memory);
            if (!Instruction.TryDecode(opcode, out Instruction instruction)) {
                throw new InvalidOperationException($"Unknown opcode: ${opcode:X2}");
            }

            switch (instruction.Kind) {
                case InstructionKind.LDA:
                    ExecuteLoadInstruction(instruction.AddressingMode, ref cycles, out this.A, ref this, ref memory);
                    break;
                case InstructionKind.LDX:
                    ExecuteLoadInstruction(instruction.AddressingMode, ref cycles, out this.X, ref this, ref memory);
                    break;
                case InstructionKind.LDY:
                    ExecuteLoadInstruction(instruction.AddressingMode, ref cycles, out this.Y, ref this, ref memory);
                    break;

                case InstructionKind.STA:
                    ExecuteStoreInstruction(instruction.AddressingMode, ref cycles, this.A, ref this, ref memory);
                    break;
                case InstructionKind.STX:
                    ExecuteStoreInstruction(instruction.AddressingMode, ref cycles, this.X, ref this, ref memory);
                    break;
                case InstructionKind.STY:
                    ExecuteStoreInstruction(instruction.AddressingMode, ref cycles, this.Y, ref this, ref memory);
                    break;

                case InstructionKind.ADC: {
                    Byte value = FetchByte(instruction.AddressingMode, ref cycles, ref this, ref memory);

                    int32_t sum = this.A + value + (this.Status.Carry ? 1 : 0);
                    Byte result = (Byte)sum;

                    this.SetZeroAndNegativeFlags(result);
                    this.Status.Carry = sum > 255;
                    this.Status.Overflow = ((this.A ^ result) & (value ^ result) & 0x80) != 0;

                    this.A = result;
                    break;
                }

                default: throw new NotImplementedException($"Instruction [{instruction.ToString()}] is not implemented.");
            }

        }

        return cycles;
    }

    private static void ExecuteLoadInstruction(AddressingMode addressingMode, ref int32_t cycles, out Byte register, ref CPU cpu, ref Memory64K memory) {
        Byte value = FetchByte(addressingMode, ref cycles, ref cpu, ref memory);
        register = value;
        cpu.SetZeroAndNegativeFlags(value);
    }

    private static void ExecuteStoreInstruction(AddressingMode addressingMode, ref int32_t cycles, Byte register, ref CPU cpu, ref Memory64K memory) {
        StoreByte(addressingMode, register, ref cycles, ref cpu, ref memory);
    }
}