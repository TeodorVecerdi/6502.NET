namespace Emulator;

public ref partial struct CPU {
    public static Byte FetchByte(AddressingMode mode, ref int32_t cycles, ref CPU cpu, ref Memory64K memory) {
        switch (mode) {
            case AddressingMode.Immediate: {
                // For immediate mode, the data is directly after the opcode.
                Byte value = memory.ReadByte(cpu.PC++);
                cycles--; // Accounting for the read cycle.
                return value;
            }

            case AddressingMode.ZeroPage: {
                Word effectiveAddress = memory.ReadByte(cpu.PC++);
                cycles--; // Accounting for the read cycle.
                Byte value = memory.ReadByte(effectiveAddress);
                cycles--; // Accounting for the read cycle.
                return value;
            }

            case AddressingMode.ZeroPageX: {
                Word effectiveAddress = (Word)((memory.ReadByte(cpu.PC++) + cpu.X) & 0xFF);
                cycles -= 2; // One for the read cycle and another one for adding X.
                Byte value = memory.ReadByte(effectiveAddress);
                cycles--; // Accounting for the read cycle.
                return value;
            }

            case AddressingMode.ZeroPageY: {
                Word effectiveAddress = (Word)((memory.ReadByte(cpu.PC++) + cpu.Y) & 0xFF);
                cycles -= 2; // One for the read cycle and another one for adding Y.
                Byte value = memory.ReadByte(effectiveAddress);
                cycles--; // Accounting for the read cycle.
                return value;
            }

            case AddressingMode.Absolute: {
                Word effectiveAddress = memory.ReadWord(cpu.PC);
                cpu.PC += 2;
                cycles -= 2; // Accounting for the read cycles.
                Byte value = memory.ReadByte(effectiveAddress);
                cycles--; // Accounting for the read cycle.
                return value;
            }

            case AddressingMode.AbsoluteX: {
                Word effectiveAddress = memory.ReadWord(cpu.PC);
                cpu.PC += 2;
                cycles -= 2; // Accounting for the read cycles.
                if ((effectiveAddress & 0xFF00) != ((effectiveAddress + cpu.X) & 0xFF00)) {
                    cycles--; // Accounting for page boundary crossing.
                }

                effectiveAddress += cpu.X;
                Byte value = memory.ReadByte(effectiveAddress);
                cycles--; // Accounting for the read cycle.
                return value;
            }

            case AddressingMode.AbsoluteY: {
                Word effectiveAddress = memory.ReadWord(cpu.PC);
                cpu.PC += 2;
                cycles -= 2; // Accounting for the read cycles.
                if ((effectiveAddress & 0xFF00) != ((effectiveAddress + cpu.Y) & 0xFF00)) {
                    cycles--; // Accounting for page boundary crossing.
                }

                effectiveAddress += cpu.Y;
                Byte value = memory.ReadByte(effectiveAddress);
                cycles--; // Accounting for the read cycle.
                return value;
            }

            case AddressingMode.IndirectX: {
                Byte zeroPageAddress = (Byte)(memory.ReadByte(cpu.PC++) + cpu.X);
                cycles -= 2; // One for the read cycle and another one for adding X.
                Word effectiveAddress = memory.ReadWord(zeroPageAddress);
                cycles -= 2; // Accounting for the read cycles.
                Byte value = memory.ReadByte(effectiveAddress);
                cycles--; // Accounting for the read cycle.
                return value;
            }

            case AddressingMode.IndirectY: {
                Byte zeroPageAddress = memory.ReadByte(cpu.PC++);
                cycles--; // Accounting for the read cycle.
                Word effectiveAddress = memory.ReadWord(zeroPageAddress);
                cycles -= 2; // Accounting for the read cycles.
                if ((effectiveAddress & 0xFF00) != ((effectiveAddress + cpu.Y) & 0xFF00)) {
                    cycles--; // Accounting for page boundary crossing.
                }

                effectiveAddress += cpu.Y;
                Byte value = memory.ReadByte(effectiveAddress);
                cycles--; // Accounting for the read cycle.
                return value;
            }

            case AddressingMode.Implied:
            case AddressingMode.Indirect:
            case AddressingMode.Relative:
            default: throw new InvalidOperationException($"Invalid addressing mode for FetchByte: {mode.ToMnemonic()}");
        }
    }
}