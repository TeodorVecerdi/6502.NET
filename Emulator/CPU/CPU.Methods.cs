using System.Text;
using Utilities;

namespace Emulator;

public partial class CPU {
    public void Connect(Bus? bus) {
        this.m_Bus = bus;
    }

    public uint8_t Read(uint16_t address) {
        return this.m_Bus!.Read(address, false);
    }

    public void Write(uint16_t address, uint8_t data) {
        this.m_Bus!.Write(address, data);
    }

    public void Reset() {
        // Reset program counter
        this.PC = (uint16_t)(this.Read(0xFFFC) | (this.Read(0xFFFD) << 8));

        // Reset registers
        this.A = 0x00;
        this.X = 0x00;
        this.Y = 0x00;
        this.SP = 0xFD;
        this.Status.Data = 0x00;

        // Reset internal variables
        this.m_Fetched = 0x00;
        this.m_AbsoluteAddress = 0x0000;
        this.m_RelativeAddress = 0x0000;
        var s = this.m_RelativeAddress;
        this.m_ClockCount = 0;

        // Resetting takes 7 cycles
        this.m_Cycles = 7;
    }

    public bool IsComplete() => this.m_Cycles == 0;

    public void InterruptRequest() => NotImplemented("CPU.InterruptRequest");
    public void NonMaskableInterrupt() => NotImplemented("CPU.NonMaskableInterrupt");

    public unsafe void Clock() {
        if (this.m_Cycles == 0) {
            this.m_Opcode = this.Read(this.PC);
            this.m_OpcodeAddress = this.PC;
            this.PC++;

            Instruction instruction = s_InstructionLookupTable[this.m_Opcode];
            this.m_Cycles = instruction.Cycles;

            bool requiresAdditionalCycle1 = instruction.Address(this);
            bool requiresAdditionalCycle2 = instruction.Execute(this);
            if (requiresAdditionalCycle1 && requiresAdditionalCycle2) {
                this.m_Cycles++;
            }
        }

        this.m_ClockCount++;
        this.m_Cycles--;
    }

    private uint8_t Fetch() {
        if (s_InstructionLookupTable[this.m_Opcode].AddressingMode != AddressingMode.IMP) {
            this.m_Fetched = this.Read(this.m_AbsoluteAddress);
        }

        return this.m_Fetched;
    }

    public Dictionary<uint16_t, string> Disassemble(uint16_t startAddress, uint16_t endAddress) {
        uint16_t address = startAddress;

        Dictionary<uint16_t, string> mapLines = new();

        StringBuilder lineBuilder = new();
        while (address < endAddress) {
            uint16_t lineAddress = address;

            lineBuilder.Clear();

            uint8_t opcode = this.m_Bus!.Read(address++, true);
            Instruction instruction = s_InstructionLookupTable[opcode];
            lineBuilder.Append(instruction.Name.ToString().PadRight(5).Blue().Stylize(GraphicsMode.BOLD));

            switch (instruction.AddressingMode) {
                case AddressingMode.IMP: {
                    break;
                }
                case AddressingMode.IMM: {
                    uint8_t value = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"#${value:X2}".DarkBlue());
                    break;
                }
                case AddressingMode.ZP0: {
                    uint8_t low = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"${low:X2}".DarkBlue());
                    break;
                }
                case AddressingMode.ZPX: {
                    uint8_t low = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"{"$".DarkBlue()}{low.ToString("X2").DarkBlue()}, X");
                    break;
                }
                case AddressingMode.ZPY: {
                    uint8_t low = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"{"$".DarkBlue()}{low.ToString("X2").DarkBlue()}, Y");
                    break;
                }
                case AddressingMode.REL: {
                    uint8_t offset = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"{"$".DarkBlue()}{offset.ToString("X2").DarkBlue()} [{"$".DarkBlue()}{(address + offset).ToString("X4").DarkBlue()}]");
                    break;
                }
                case AddressingMode.ABS: {
                    uint8_t low = this.m_Bus.Read(address++, true);
                    uint8_t high = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"${high:X2}{low:X2}".DarkBlue());
                    break;
                }
                case AddressingMode.ABX: {
                    uint8_t low = this.m_Bus.Read(address++, true);
                    uint8_t high = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"{$"${high:X2}{low:X2}".DarkBlue()}, X");
                    break;
                }
                case AddressingMode.ABY: {
                    uint8_t low = this.m_Bus.Read(address++, true);
                    uint8_t high = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"{$"${high:X2}{low:X2}".DarkBlue()}, Y");
                    break;
                }
                case AddressingMode.IND: {
                    uint8_t low = this.m_Bus.Read(address++, true);
                    uint8_t high = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"({$"${high:X2}{low:X2}".DarkBlue()})");
                    break;
                }
                case AddressingMode.IZX: {
                    uint8_t low = this.m_Bus.Read(address++, true);
                    lineBuilder.Append($"({$"${low:X2}".DarkBlue()}, X)");
                    break;
                }
                case AddressingMode.IZY: {
                    uint8_t low = this.m_Bus.Read(address++, true);
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

            mapLines.Add(lineAddress, lineBuilder.ToString());
        }

        return mapLines;
    }

    public override string ToString() {
        // string statusString = this.Status.ToString("F").Indent(4, false);
        string statusString = this.Status.ToString("S").IndentUsing("    Status = ", false);
        return $"CPU {{\n    PC     = 0x{this.PC:X4}\n    SP     = 0x{this.SP:X2}\n    A      = 0x{this.A:X2}\n    X      = 0x{this.X:X2}\n    Y      = 0x{this.Y:X2}\n    Status = {statusString}\n}}";
    }
}