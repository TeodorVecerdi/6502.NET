using Utilities;

namespace Emulator;

public partial class CPU {
    public void Connect(Bus? bus) {
        this.m_Bus = bus;
    }

    public uint8_t Read(uint16_t address) {
        return this.m_Bus!.CpuRead(address, false);
    }

    public void Write(uint16_t address, uint8_t data) {
        this.m_Bus!.CpuWrite(address, data);
    }

    public void Reset() {
        // Reset program counter
        this.PC = (uint16_t)(this.Read(0xFFFC) | (this.Read(0xFFFD) << 8));

        // Reset registers
        this.A = 0x00;
        this.X = 0x00;
        this.Y = 0x00;
        this.SP = 0xFD;
        this.Status.Data = ProcessorStatusFlags.Unused;

        // Reset internal variables
        this.m_Fetched = 0x00;
        this.m_AbsoluteAddress = 0x0000;
        this.m_RelativeAddress = 0x0000;
        this.m_ClockCount = 0;

        // Resetting takes 7 cycles
        this.m_Cycles = 7;
    }

    public bool IsInstructionComplete() => this.m_Cycles == 0;

    public void InterruptRequest() {
        if (this.Status.InterruptDisable) return;

        // Push the program counter to the stack
        this.Write((uint16_t)(this.SP + 0x0100), (uint8_t)((this.PC >> 8) & 0x00FF));
        this.SP--;
        this.Write((uint16_t)(this.SP + 0x0100), (uint8_t)(this.PC & 0x00FF));
        this.SP--;

        // Push the status register to the stack
        this.Status.BreakCommand = false;
        this.Status.Unused = true;
        this.Status.InterruptDisable = true;
        this.Write((uint16_t)(this.SP + 0x0100), this.Status.Data);
        this.SP--;

        // Set the program counter to the address at 0xFFFE
        this.m_AbsoluteAddress = 0xFFFE;
        uint16_t lowByte = this.Read(this.m_AbsoluteAddress);
        uint16_t highByte = this.Read((uint16_t)(this.m_AbsoluteAddress + 1));
        this.PC = (uint16_t)((highByte << 8) | lowByte);

        // IRQs take 7 cycles
        this.m_Cycles = 7;
    }

    public void NonMaskableInterrupt() {
        // This is the same as an IRQ except it uses a different interrupt vector address
        // and it cannot be disabled by setting the interrupt disable flag. The NMI takes
        // 8 cycles to complete.

        // Push the program counter to the stack
        this.Write((uint16_t)(this.SP + 0x0100), (uint8_t)((this.PC >> 8) & 0x00FF));
        this.SP--;
        this.Write((uint16_t)(this.SP + 0x0100), (uint8_t)(this.PC & 0x00FF));
        this.SP--;

        // Push the status register to the stack
        this.Status.BreakCommand = false;
        this.Status.Unused = true;
        this.Status.InterruptDisable = true;
        this.Write((uint16_t)(this.SP + 0x0100), this.Status.Data);
        this.SP--;

        // Set the program counter to the address at 0xFFFA
        this.m_AbsoluteAddress = 0xFFFA;
        uint16_t lowByte = this.Read(this.m_AbsoluteAddress);
        uint16_t highByte = this.Read((uint16_t)(this.m_AbsoluteAddress + 1));
        this.PC = (uint16_t)((highByte << 8) | lowByte);

        // NMIs take 8 cycles
        this.m_Cycles = 8;
    }

    public unsafe void Clock() {
        if (this.m_Cycles == 0) {
            this.m_Opcode = this.Read(this.PC);
            this.m_OpcodeAddress = this.PC;
            this.PC++;

            Instruction instruction = InstructionLookupTable[this.m_Opcode];
            this.m_Cycles = instruction.Cycles;

            bool requiresAdditionalCycle1 = instruction.Address(this);
            bool requiresAdditionalCycle2 = instruction.Execute(this);
            if (requiresAdditionalCycle1 && requiresAdditionalCycle2) {
                this.m_Cycles++;
            }

            this.Status.Unused = true;
        }

        this.m_ClockCount++;
        this.m_Cycles--;
    }

    private uint8_t Fetch() {
        if (InstructionLookupTable[this.m_Opcode].AddressingMode != AddressingMode.IMP) {
            this.m_Fetched = this.Read(this.m_AbsoluteAddress);
        }

        return this.m_Fetched;
    }

    public Dictionary<uint16_t, string> Disassemble(uint16_t startAddress, uint16_t endAddress) {
        return Disassembler.Disassemble(this.m_Bus!.RAM.Data[startAddress..endAddress], startAddress);
    }

    public override string ToString() {
        // string statusString = this.Status.ToString("F").Indent(4, false);
        string statusString = this.Status.ToString("S").IndentUsing("    Status = ", false);
        return $"CPU {{\n    PC     = 0x{this.PC:X4}\n    SP     = 0x{this.SP:X2}\n    A      = 0x{this.A:X2}\n    X      = 0x{this.X:X2}\n    Y      = 0x{this.Y:X2}\n    Status = {statusString}\n}}";
    }
}