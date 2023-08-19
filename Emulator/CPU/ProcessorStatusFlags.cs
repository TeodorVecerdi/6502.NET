namespace Emulator;

public static class ProcessorStatusFlags {
    public const uint8_t Carry = 0b0000_0001;
    public const uint8_t Zero = 0b0000_0010;
    public const uint8_t InterruptDisable = 0b0000_0100;
    public const uint8_t DecimalMode = 0b0000_1000;
    public const uint8_t BreakCommand = 0b0001_0000;
    public const uint8_t Unused = 0b0010_0000;
    public const uint8_t Overflow = 0b0100_0000;
    public const uint8_t Negative = 0b1000_0000;
}