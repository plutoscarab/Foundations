
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

    public static List<int> GetDigits(this sbyte value, int radix)
    {
        if (value == 0)
            return [0];

        if (value < 0)
            value = (sbyte)(-value);
            
        List<int> digits = [];
        var r = (sbyte)radix;

        while (value != 0)
        {
            (var old, value) = (value, (sbyte)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class ByteExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this byte value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this byte value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this byte value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this byte value, int radix)
    {
        if (value == 0)
            return [0];

            
        List<int> digits = [];
        var r = (byte)radix;

        while (value != 0)
        {
            (var old, value) = (value, (byte)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class Int16Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this short value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this short value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this short value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this short value, int radix)
    {
        if (value == 0)
            return [0];

        if (value < 0)
            value = (short)(-value);
            
        List<int> digits = [];
        var r = (short)radix;

        while (value != 0)
        {
            (var old, value) = (value, (short)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class UInt16Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this ushort value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this ushort value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this ushort value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this ushort value, int radix)
    {
        if (value == 0)
            return [0];

            
        List<int> digits = [];
        var r = (ushort)radix;

        while (value != 0)
        {
            (var old, value) = (value, (ushort)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class Int32Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this int value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this int value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this int value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this int value, int radix)
    {
        if (value == 0)
            return [0];

        if (value < 0)
            value = (int)(-value);
            
        List<int> digits = [];
        var r = (int)radix;

        while (value != 0)
        {
            (var old, value) = (value, (int)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class UInt32Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this uint value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this uint value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this uint value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this uint value, int radix)
    {
        if (value == 0)
            return [0];

            
        List<int> digits = [];
        var r = (uint)radix;

        while (value != 0)
        {
            (var old, value) = (value, (uint)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class Int64Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this long value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this long value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this long value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this long value, int radix)
    {
        if (value == 0)
            return [0];

        if (value < 0)
            value = (long)(-value);
            
        List<int> digits = [];
        var r = (long)radix;

        while (value != 0)
        {
            (var old, value) = (value, (long)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class UInt64Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this ulong value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this ulong value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this ulong value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this ulong value, int radix)
    {
        if (value == 0)
            return [0];

            
        List<int> digits = [];
        var r = (ulong)radix;

        while (value != 0)
        {
            (var old, value) = (value, (ulong)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class Int128Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this Int128 value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this Int128 value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this Int128 value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this Int128 value, int radix)
    {
        if (value == 0)
            return [0];

        if (value < 0)
            value = (Int128)(-value);
            
        List<int> digits = [];
        var r = (Int128)radix;

        while (value != 0)
        {
            (var old, value) = (value, (Int128)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class UInt128Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this UInt128 value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this UInt128 value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this UInt128 value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this UInt128 value, int radix)
    {
        if (value == 0)
            return [0];

            
        List<int> digits = [];
        var r = (UInt128)radix;

        while (value != 0)
        {
            (var old, value) = (value, (UInt128)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class IntPtrExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this nint value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this nint value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this nint value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this nint value, int radix)
    {
        if (value == 0)
            return [0];

        if (value < 0)
            value = (nint)(-value);
            
        List<int> digits = [];
        var r = (nint)radix;

        while (value != 0)
        {
            (var old, value) = (value, (nint)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class UIntPtrExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this nuint value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this nuint value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this nuint value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this nuint value, int radix)
    {
        if (value == 0)
            return [0];

            
        List<int> digits = [];
        var r = (nuint)radix;

        while (value != 0)
        {
            (var old, value) = (value, (nuint)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}

public static partial class BigIntegerExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this BigInteger value) => (value & 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this BigInteger value) => (value & 1) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Parity(this BigInteger value) => 1 - 2 * (int)(value & 1);

    public static List<int> GetDigits(this BigInteger value, int radix)
    {
        if (value == 0)
            return [0];

            
        List<int> digits = [];
        var r = (BigInteger)radix;

        while (value != 0)
        {
            (var old, value) = (value, (BigInteger)(value / r));
            var d = (int)(old - value * r);
            digits.Insert(0, d);
        }

        return digits;
    }
}
