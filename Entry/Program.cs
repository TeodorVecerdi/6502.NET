using System.Text;
using Emulator;

Bus bus = new();
CPU cpu = new();

bus.Connect(cpu);


bus.RAM.WriteBlock(
    offset: 0x8000,

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

// Program for calculating 3 * 10:
// A2 0A 8E 00 00 A2 03 8E 01 00 AC 00 00 A9 00 18 6D 01 00 88 D0 FA 8D 02 00 EA EA EA

bus.RAM[0xFFFC] = 0x00;
bus.RAM[0xFFFD] = 0x80;


Dictionary<ushort, string> code = cpu.Disassemble(0x8000, 0x8023);

Console.WriteLine();
Console.WriteLine(" - Disassembly -\n");
Console.WriteLine(string.Join('\n', code.Select(kvp => $" {kvp.Key.ToString("X4").Cyan()}    {kvp.Value}")));
Console.WriteLine();


bus.Reset();
Execute(7); // shouldn't do anything

try {
    while (true) {
        bus.Clock();

        if (cpu.IsComplete()) {
            Console.WriteLine($" {cpu.OpcodeAddress.ToString("X4").Cyan()}    {code[cpu.OpcodeAddress]}");
            Console.WriteLine(cpu.ToString());
        }
    }
} finally {
    Console.WriteLine();
    Console.WriteLine(cpu.ToString());
    Console.WriteLine();

    // Dump RAM
    StringBuilder sb = new();

    sb.AppendLine(" - Zero Page -\n");
    bus.RAM.DumpPage(0x00, sb);

    sb.AppendLine();

    sb.AppendLine(" - Program Page -\n");
    bus.RAM.DumpPage(0x8000 / 0x100, sb);

    Console.WriteLine(sb.ToString());
}


/*
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
*/


void Execute(int cycles) {
    for (int i = 0; i < cycles; i++) {
        bus.Clock();
    }

    if (!cpu.IsComplete()) {
        throw new Exception("CPU did not complete in the expected number of cycles.");
    }
}