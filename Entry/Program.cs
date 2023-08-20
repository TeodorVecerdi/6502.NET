using System.Diagnostics;
using BenchmarkDotNet.Running;
using Emulator;

// BenchmarkRunner.Run<Benchmark>();
// return;

Bus bus = new();
CPU cpu = new();
bus.Connect(cpu);

const string testPath = "C:/dev/dotnet/Emulator6502/External/tests/6502_functional_test.bin";
const ushort testOffset = 0x000A;
const ushort testStartAddress = 0x0400;

byte[] testRom = File.ReadAllBytes(testPath);

bus.RAM.WriteBlock(testOffset, testRom);
bus.RAM[0xFFFC] = testStartAddress & 0x00FF;
bus.RAM[0xFFFD] = (testStartAddress & 0xFF00) >> 8;


bus.Reset();
while (!cpu.IsInstructionComplete()) bus.Clock();

long timestamp = Stopwatch.GetTimestamp();
while (true) {
    bus.Clock();

    if (cpu.PC == 0x3699 && cpu.IsInstructionComplete()) {
        break;
    }
}

TimeSpan elapsed = Stopwatch.GetElapsedTime(timestamp);
Console.WriteLine($" - Profiler -\n" +
                  $"{bus.SystemClockCounter} cycles in {elapsed.TotalMilliseconds}ms\n" +
                  $"Milliseconds per cycle: {elapsed.TotalMilliseconds / bus.SystemClockCounter}\n" +
                  $"Cycles per second: {bus.SystemClockCounter / elapsed.TotalSeconds} Hz; {bus.SystemClockCounter / elapsed.TotalSeconds / 1000000} MHz");