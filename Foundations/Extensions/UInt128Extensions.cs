
using System.Runtime.CompilerServices;

namespace Foundations;

public static class UInt128Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Lo(this UInt128 u) => (ulong)u;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Hi(this UInt128 u) => (ulong)(u >> 64);
}