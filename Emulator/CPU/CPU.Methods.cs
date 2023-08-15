using Utilities;

namespace Emulator;

public ref partial struct CPU {
    public void Reset(ref Memory64K memory) {
        this.Status.InterruptDisable = true;

        this.SP = 0xFD;
        this.PC = memory.ReadWord(0xFFFC);
    }

    public override string ToString() {
        // string statusString = this.Status.ToString("F").Indent(4, false);
        string statusString = this.Status.ToString("S").IndentUsing("    Status = ", false);
        return $"CPU {{\n    PC     = 0x{this.PC:X4}\n    SP     = 0x{this.SP:X2}\n    A      = 0x{this.A:X2}\n    X      = 0x{this.X:X2}\n    Y      = 0x{this.Y:X2}\n    Status = {statusString}\n}}";
    }

    private void SetZeroAndNegativeFlags(Byte value) {
        this.Status.Zero = value == 0;
        this.Status.Negative = (value & 0x80) != 0;
    }
}