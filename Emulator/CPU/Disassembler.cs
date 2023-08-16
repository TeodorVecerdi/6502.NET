using System.Text;

namespace Emulator;

public static class Disassembler {
    public static Dictionary<uint16_t, string> Disassemble(ReadOnlySpan<uint8_t> code, uint16_t codeOffset) {
        Dictionary<uint16_t, string> mapLines = new();
        uint16_t offset = 0x0000;
        StringBuilder lineBuilder = new();

        while (offset < code.Length) {
            uint16_t lineOffset = offset;
            lineBuilder.Clear();

            uint8_t opcode = code[offset++];
            Instruction instruction = CPU.InstructionLookupTable[opcode];
            lineBuilder.Append(instruction.Name.ToString().PadRight(5).Blue().Stylize(GraphicsMode.BOLD));

            switch (instruction.AddressingMode) {
                case AddressingMode.IMP: {
                    break;
                }
                case AddressingMode.IMM: {
                    uint8_t value = code[offset++];
                    lineBuilder.Append($"#${value:X2}".DarkBlue());
                    break;
                }
                case AddressingMode.ZP0: {
                    uint8_t low = code[offset++];
                    lineBuilder.Append($"${low:X2}".DarkBlue());
                    break;
                }
                case AddressingMode.ZPX: {
                    uint8_t low = code[offset++];
                    lineBuilder.Append($"{"$".DarkBlue()}{low.ToString("X2").DarkBlue()}, X");
                    break;
                }
                case AddressingMode.ZPY: {
                    uint8_t low = code[offset++];
                    lineBuilder.Append($"{"$".DarkBlue()}{low.ToString("X2").DarkBlue()}, Y");
                    break;
                }
                case AddressingMode.REL: {
                    uint8_t relOffset = code[offset++];
                    lineBuilder.Append($"{"$".DarkBlue()}{relOffset.ToString("X2").DarkBlue()} [{"$".DarkBlue()}{(codeOffset + offset + relOffset).ToString("X4").DarkBlue()}]");
                    break;
                }
                case AddressingMode.ABS: {
                    uint8_t low = code[offset++];
                    uint8_t high = code[offset++];
                    lineBuilder.Append($"${high:X2}{low:X2}".DarkBlue());
                    break;
                }
                case AddressingMode.ABX: {
                    uint8_t low = code[offset++];
                    uint8_t high = code[offset++];
                    lineBuilder.Append($"{$"${high:X2}{low:X2}".DarkBlue()}, X");
                    break;
                }
                case AddressingMode.ABY: {
                    uint8_t low = code[offset++];
                    uint8_t high = code[offset++];
                    lineBuilder.Append($"{$"${high:X2}{low:X2}".DarkBlue()}, Y");
                    break;
                }
                case AddressingMode.IND: {
                    uint8_t low = code[offset++];
                    uint8_t high = code[offset++];
                    lineBuilder.Append($"({$"${high:X2}{low:X2}".DarkBlue()})");
                    break;
                }
                case AddressingMode.IZX: {
                    uint8_t low = code[offset++];
                    lineBuilder.Append($"({$"${low:X2}".DarkBlue()}, X)");
                    break;
                }
                case AddressingMode.IZY: {
                    uint8_t low = code[offset++];
                    lineBuilder.Append($"({$"${low:X2}".DarkBlue()}), Y");
                    break;
                }
                default: {
                    throw new ArgumentOutOfRangeException(nameof(instruction.AddressingMode), instruction.AddressingMode, "Invalid addressing mode");
                }
            }

            int lineLength = lineBuilder.Length;
            for (int i = lineLength; i < (Colorizer.IsColorOutputEnabled ? 71 : 18); i++) {
                lineBuilder.Append(' ');
            }

            lineBuilder.Append($"{{{instruction.AddressingMode.ToString()}}}".Yellow());
            mapLines[(uint16_t)(lineOffset + codeOffset)] = lineBuilder.ToString();
        }

        return mapLines;
    }
}