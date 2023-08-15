// See https://aka.ms/new-console-template for more information

using System.Text;
using Emulator;

CPU cpu = new();
Memory64K memory = new();
cpu.Reset(ref memory);


memory.WriteBlock(
    0x0000,

    0xA9, 0xFF, // LDA #$FF
    0xA9, 0x00, // LDA #$00
    0xA9, 0x69, // LDA #$69

    0xA6, 0x00, // LDX $00
    0xAC, 0x05, 0x00, // LDY $0005

    0x8D, 0xC0, 0x00, // STA $00C0
    0xAE, 0xC0, 0x00, // LDX $00C0
    0x8E, 0xC1, 0x00, // STX $00C1
    0x8C, 0xC2, 0x00, // STY $00C2

    // 0x34 + 0x35 = haha funny number
    0xA2, 0x34, // LDX #$34
    0x8E, 0xC3, 0x00, // STX $00C3
    0xA9, 0x35, // LDA #$35
    0x65, 0xC3, // ADC $C3
    0x8D, 0xC4, 0x00, // STA $00C4

    0x00
);

Console.WriteLine("LDA #$FF");
cpu.Execute(2, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

Console.WriteLine("LDA #$00");
cpu.Execute(2, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

Console.WriteLine("LDA #$69");
cpu.Execute(2, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

Console.WriteLine("LDX $00");
cpu.Execute(3, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

Console.WriteLine("LDY $0005");
cpu.Execute(4, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

Console.WriteLine("STA $0010");
cpu.Execute(4, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

Console.WriteLine("LDX $00C0");
cpu.Execute(4, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

Console.WriteLine("STX $00C1");
cpu.Execute(4, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

Console.WriteLine("STY $00C2");
cpu.Execute(4, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

Console.WriteLine(@"LDX #$34
STX $00C3
LDA #$35
ADC $C3
STA $00C4");
cpu.Execute(2 + 4 + 2 + 3 + 4, ref memory).Verify();
Console.WriteLine(cpu.ToString());
Console.WriteLine();

StringBuilder sb = new();
memory.DumpPages(..1, sb);
Console.WriteLine(sb.ToString());

internal static class Extensions {
    public static void Verify(this int cycles) {
        if (cycles != 0) {
            throw new InvalidOperationException($"Expected 0 cycles after executing, got {cycles}.");
        }
    }
}