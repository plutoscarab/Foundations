
using System.Runtime.CompilerServices;

namespace Foundations;

public static partial class SByteExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this sbyte value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this sbyte value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this sbyte value) => 1 - 2 * (int)(value & 1);
}

public static partial class ByteExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this byte value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this byte value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this byte value) => 1 - 2 * (int)(value & 1);
}

public static partial class Int16Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this short value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this short value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this short value) => 1 - 2 * (int)(value & 1);
}

public static partial class UInt16Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this ushort value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this ushort value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this ushort value) => 1 - 2 * (int)(value & 1);
}

public static partial class Int32Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this int value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this int value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this int value) => 1 - 2 * (int)(value & 1);
}

public static partial class UInt32Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this uint value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this uint value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this uint value) => 1 - 2 * (int)(value & 1);
}

public static partial class Int64Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this long value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this long value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this long value) => 1 - 2 * (int)(value & 1);
}

public static partial class UInt64Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this ulong value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this ulong value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this ulong value) => 1 - 2 * (int)(value & 1);
}

public static partial class Int128Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this Int128 value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this Int128 value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this Int128 value) => 1 - 2 * (int)(value & 1);
}

public static partial class UInt128Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this UInt128 value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this UInt128 value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this UInt128 value) => 1 - 2 * (int)(value & 1);
}

public static partial class IntPtrExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this nint value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this nint value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this nint value) => 1 - 2 * (int)(value & 1);
}

public static partial class UIntPtrExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this nuint value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this nuint value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this nuint value) => 1 - 2 * (int)(value & 1);
}
