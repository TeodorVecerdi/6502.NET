#nullable enable

using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using Emulator;

namespace Entry;

[MemoryDiagnoser(false)]
public class Benchmark {
    private readonly CPU m_CPU = new();
    private readonly Bus m_Bus = new();
    private long m_IterationStartTimestamp;

    [GlobalSetup]
    public void Setup() {
        this.m_Bus.Connect(this.m_CPU);

        const string testPath = "C:/dev/dotnet/Emulator6502/External/tests/6502_functional_test.bin";
        const ushort testOffset = 0x000A;
        const ushort testStartAddress = 0x0400;

        byte[] testRom = File.ReadAllBytes(testPath);

        this.m_Bus.RAM.WriteBlock(testOffset, testRom);
        this.m_Bus.RAM[0xFFFC] = testStartAddress & 0x00FF;
        this.m_Bus.RAM[0xFFFD] = (testStartAddress & 0xFF00) >> 8;
    }

    [IterationSetup]
    public void IterationSetup() {
        this.m_Bus.Reset();
        while (!this.m_CPU.IsInstructionComplete()) this.m_Bus.Clock();
    }

    [IterationCleanup]
    public void IterationCleanup() {
        /*
        TimeSpan elapsed = Stopwatch.GetElapsedTime(this.m_IterationStartTimestamp);
        Console.WriteLine();
        Console.WriteLine($" - Profiler -\n" +
                          $"{this.m_Bus.SystemClockCounter} cycles in {elapsed.TotalMilliseconds}ms\n" +
                          $"Milliseconds per cycle: {elapsed.TotalMilliseconds / this.m_Bus.SystemClockCounter}\n" +
                          $"Cycles per second: {this.m_Bus.SystemClockCounter / elapsed.TotalSeconds} Hz; {this.m_Bus.SystemClockCounter / elapsed.TotalSeconds / 1000000} MHz");
    */
    }

    [Benchmark]
    public void RunFunctionalTest() {
        // this.m_IterationStartTimestamp = Stopwatch.GetTimestamp();
        while (true) {
            this.m_Bus.Clock();

            if (this.m_CPU.PC == 0x3699 && this.m_CPU.IsInstructionComplete()) {
                break;
            }
        }
    }
}