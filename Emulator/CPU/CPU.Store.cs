namespace Emulator;

public ref partial struct CPU {
    public static void StoreByte(AddressingMode mode, Byte value, ref int32_t cycles, ref CPU cpu, ref Memory64K memory) {
        switch (mode) {
            case AddressingMode.ZeroPage: {
                Word effectiveAddress = memory.ReadByte(cpu.PC++);
                cycles--; // Accounting for the read cycle.
                memory.WriteByte(effectiveAddress, value);
                cycles--; // Accounting for the write cycle.
                break;
            }

            case AddressingMode.ZeroPageX: {
                Word effectiveAddress = (Word)((memory.ReadByte(cpu.PC++) + cpu.X) & 0xFF);
                cycles -= 2; // One for the read cycle and another one for adding X.
                memory.WriteByte(effectiveAddress, value);
                cycles--; // Accounting for the write cycle.
                break;
            }

            case AddressingMode.ZeroPageY: {
                Word effectiveAddress = (Word)((memory.ReadByte(cpu.PC++) + cpu.Y) & 0xFF);
                cycles -= 2; // One for the read cycle and another one for adding Y.
                memory.WriteByte(effectiveAddress, value);
                cycles--; // Accounting for the write cycle.
                break;
            }

            case AddressingMode.Absolute: {
                Word effectiveAddress = memory.ReadWord(cpu.PC);
                cpu.PC += 2;
                cycles -= 2; // Accounting for the read cycles.
                memory.WriteByte(effectiveAddress, value);
                cycles--; // Accounting for the write cycle.
                break;
            }

            case AddressingMode.AbsoluteX: {
                Word effectiveAddress = memory.ReadWord(cpu.PC);
                cpu.PC += 2;
                cycles -= 2; // Accounting for the read cycles

                effectiveAddress += cpu.X;
                cycles--; // Accounting for adding X.

                memory.WriteByte(effectiveAddress, value);
                cycles--; // Accounting for the write cycle.
                break;
            }

            case AddressingMode.AbsoluteY: {
                Word effectiveAddress = memory.ReadWord(cpu.PC);
                cpu.PC += 2;
                cycles -= 2; // Accounting for the read cycles

                effectiveAddress += cpu.Y;
                cycles--; // Accounting for adding Y.

                memory.WriteByte(effectiveAddress, value);
                cycles--; // Accounting for the write cycle.
                break;
            }

            case AddressingMode.IndirectX: {
                Word effectiveAddress = (Word)((memory.ReadByte(cpu.PC++) + cpu.X) & 0xFF);
                cycles -= 2; // One for the read cycle and another one for adding X.
                effectiveAddress = memory.ReadWord(effectiveAddress);
                cycles -= 2; // Accounting for the read cycles.
                memory.WriteByte(effectiveAddress, value);
                cycles--; // Accounting for the write cycle.
                break;
            }

            case AddressingMode.IndirectY: {
                Word effectiveAddress = memory.ReadByte(cpu.PC++);
                cycles--; // Accounting for the read cycle.
                effectiveAddress = memory.ReadWord(effectiveAddress);
                cycles -= 2; // Accounting for the read cycles.

                effectiveAddress += cpu.Y;
                cycles--; // Accounting for adding Y.

                memory.WriteByte(effectiveAddress, value);
                cycles--; // Accounting for the write cycle.
                break;
            }

            case AddressingMode.Implied:
            case AddressingMode.Immediate:
            case AddressingMode.Indirect:
            case AddressingMode.Relative:
            default: throw new InvalidOperationException($"Invalid addressing mode for StoreByte: {mode.ToMnemonic()}");
        }
    }
}