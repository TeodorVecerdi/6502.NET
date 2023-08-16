using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Utilities;

public static class Prelude {
    [DoesNotReturn, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T NotImplemented<T>(string method) => throw new NotImplementedException($"Method [{method}] is not implemented yet.");

    [DoesNotReturn, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotImplemented(string method) => throw new NotImplementedException($"Method [{method}] is not implemented yet.");
}
