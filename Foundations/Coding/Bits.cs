
namespace Foundations.Coding;

/// <summary>
/// Various operations on bits within integer values.
/// </summary>
public static class Bits
{
    private static int[] bitCount =
    {
            0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4,
            1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5,
            1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,
            1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,
            3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,
            4, 5, 5, 6, 5, 6, 6, 7, 5, 6, 6, 7, 6, 7, 7, 8,
        };

    /// <summary>
    /// Count the number of set bits in a <see cref="System.Byte"/>.
    /// </summary>
    public static int Count(byte i)
    {
        return bitCount[i];
    }

    /// <summary>
    /// Count the number of set bits in a <see cref="System.SByte"/>.
    /// </summary>
    public static int Count(sbyte i)
    {
        return bitCount[(byte)i];
    }

    /// <summary>
    /// Count the number of set bits in a <see cref="System.Int16"/>.
    /// </summary>
    public static int Count(short i)
    {
        return bitCount[(byte)i] + bitCount[(byte)(i >> 8)];
    }

    /// <summary>
    /// Count the number of set bits in a <see cref="System.UInt16"/>.
    /// </summary>
    public static int Count(ushort i)
    {
        return bitCount[(byte)i] + bitCount[(byte)(i >> 8)];
    }

    /// <summary>
    /// Count the number of set bits in a <see cref="System.Int32"/>.
    /// </summary>
    public static int Count(int i)
    {
        return bitCount[(byte)i] +
            bitCount[(byte)(i >> 8)] +
            bitCount[(byte)(i >> 16)] +
            bitCount[(byte)(i >> 24)];
    }

    /// <summary>
    /// Count the number of set bits in a <see cref="System.UInt32"/>.
    /// </summary>
    public static int Count(uint i)
    {
        return bitCount[(byte)i] +
            bitCount[(byte)(i >> 8)] +
            bitCount[(byte)(i >> 16)] +
            bitCount[i >> 24];
    }

    /// <summary>
    /// Count the number of set bits in a <see cref="System.Int64"/>.
    /// </summary>
    public static int Count(long i)
    {
        return Count((uint)i) + Count((uint)(i >> 32));
    }

    /// <summary>
    /// Count the number of set bits in a <see cref="System.UInt64"/>.
    /// </summary>
    public static int Count(ulong i)
    {
        return Count((uint)i) + Count((uint)(i >> 32));
    }

    private static int[] parity =
    {
            0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0,
            1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1,
            1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1,
            0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0,
            1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1,
            0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0,
            0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0,
            1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1,
            1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1,
            0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0,
            0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0,
            1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1,
            0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0,
            1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1,
            1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1,
            0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0,
        };

    /// <summary>
    /// Determine if the number of set bits in a <see cref="System.Byte"/>
    /// is odd or even.
    /// </summary>
    /// <returns>Returns 0 if the number of set bits is even, otherwise returns 1.</returns>
    public static int Parity(byte i)
    {
        return parity[i];
    }

    /// <summary>
    /// Determine if the number of set bits in a <see cref="System.SByte"/>
    /// is odd or even.
    /// </summary>
    /// <returns>Returns 0 if the number of set bits is even, otherwise returns 1.</returns>
    public static int Parity(sbyte i)
    {
        return parity[(byte)i];
    }

    /// <summary>
    /// Determine if the number of set bits in a <see cref="System.Int16"/>
    /// is odd or even.
    /// </summary>
    /// <returns>Returns 0 if the number of set bits is even, otherwise returns 1.</returns>
    public static int Parity(short i)
    {
        i ^= (short)(i >> 8);
        return parity[(byte)i];
    }

    /// <summary>
    /// Determine if the number of set bits in a <see cref="System.UInt16"/>
    /// is odd or even.
    /// </summary>
    /// <returns>Returns 0 if the number of set bits is even, otherwise returns 1.</returns>
    public static int Parity(ushort i)
    {
        i ^= (ushort)(i >> 8);
        return parity[(byte)i];
    }

    /// <summary>
    /// Determine if the number of set bits in a <see cref="System.Int32"/>
    /// is odd or even.
    /// </summary>
    /// <returns>Returns 0 if the number of set bits is even, otherwise returns 1.</returns>
    public static int Parity(int i)
    {
        i ^= i >> 16;
        i ^= i >> 8;
        return parity[(byte)i];
    }

    /// <summary>
    /// Determine if the number of set bits in a <see cref="System.UInt32"/>
    /// is odd or even.
    /// </summary>
    /// <returns>Returns 0 if the number of set bits is even, otherwise returns 1.</returns>
    public static int Parity(uint i)
    {
        i ^= i >> 16;
        i ^= i >> 8;
        return parity[(byte)i];
    }

    /// <summary>
    /// Determine if the number of set bits in a <see cref="System.Int64"/>
    /// is odd or even.
    /// </summary>
    /// <returns>Returns 0 if the number of set bits is even, otherwise returns 1.</returns>
    public static int Parity(long i)
    {
        i ^= i >> 32;
        i ^= i >> 16;
        i ^= i >> 8;
        return parity[(byte)i];
    }

    /// <summary>
    /// Determine if the number of set bits in a <see cref="System.UInt32"/>
    /// is odd or even.
    /// </summary>
    /// <returns>Returns 0 if the number of set bits is even, otherwise returns 1.</returns>
    public static int Parity(ulong i)
    {
        i ^= i >> 32;
        i ^= i >> 16;
        i ^= i >> 8;
        return parity[(byte)i];
    }

    private static int[] floorLog2 =
    {
           -1, 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
            5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
            5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
            6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
            6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
            6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
            6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
        };

    /// <summary>
    /// Determine the exponent of the greatest power of 2 that is less than
    /// the value. This is also one less than the length of the binary representation
    /// of the number.
    /// </summary>
    public static int FloorLog2(byte i)
    {
        if (i == 0)
            throw new ArgumentOutOfRangeException();

        return floorLog2[i];
    }

    /// <summary>
    /// Determine the exponent of the greatest power of 2 that is less than
    /// the value. This is also one less than the length of the binary representation
    /// of the number.
    /// </summary>
    public static int FloorLog2(sbyte i)
    {
        if (i <= 0)
            throw new ArgumentOutOfRangeException();

        return floorLog2[i];
    }

    /// <summary>
    /// Determine the exponent of the greatest power of 2 that is less than
    /// the value. This is also one less than the length of the binary representation
    /// of the number.
    /// </summary>
    public static int FloorLog2(ushort i)
    {
        if (i == 0)
            throw new ArgumentOutOfRangeException();

        int n = floorLog2[(byte)(i >> 8)];
        if (n >= 0) return n + 8;
        return floorLog2[(byte)i];
    }

    /// <summary>
    /// Determine the exponent of the greatest power of 2 that is less than
    /// the value. This is also one less than the length of the binary representation
    /// of the number.
    /// </summary>
    public static int FloorLog2(short i)
    {
        if (i <= 0)
            throw new ArgumentOutOfRangeException();

        int n = floorLog2[(byte)(i >> 8)];
        if (n >= 0) return n + 8;
        return floorLog2[(byte)i];
    }

    /// <summary>
    /// Determine the exponent of the greatest power of 2 that is less than
    /// the value. This is also one less than the length of the binary representation
    /// of the number.
    /// </summary>
    public static int FloorLog2(uint i)
    {
        if (i == 0)
            throw new ArgumentOutOfRangeException();

        int n = floorLog2[i >> 24];
        if (n >= 0) return n + 24;
        n = floorLog2[(byte)(i >> 16)];
        if (n >= 0) return n + 16;
        n = floorLog2[(byte)(i >> 8)];
        if (n >= 0) return n + 8;
        return floorLog2[(byte)i];
    }

    /// <summary>
    /// Determine the exponent of the greatest power of 2 that is less than
    /// the value. This is also one less than the length of the binary representation
    /// of the number.
    /// </summary>
    public static int FloorLog2(int i)
    {
        if (i <= 0)
            throw new ArgumentOutOfRangeException();

        int n = floorLog2[i >> 24];
        if (n >= 0) return n + 24;
        n = floorLog2[(byte)(i >> 16)];
        if (n >= 0) return n + 16;
        n = floorLog2[(byte)(i >> 8)];
        if (n >= 0) return n + 8;
        return floorLog2[(byte)i];
    }

    /// <summary>
    /// Determine the exponent of the greatest power of 2 that is less than
    /// the value. This is also one less than the length of the binary representation
    /// of the number.
    /// </summary>
    public static int FloorLog2(ulong i)
    {
        if (i == 0)
            throw new ArgumentOutOfRangeException();

        if (i <= uint.MaxValue) return FloorLog2((uint)i);
        return FloorLog2((uint)(i >> 32)) + 32;
    }

    /// <summary>
    /// Determine the exponent of the greatest power of 2 that is less than
    /// the value. This is also one less than the length of the binary representation
    /// of the number.
    /// </summary>
    public static int FloorLog2(long i)
    {
        if (i <= 0)
            throw new ArgumentOutOfRangeException();

        if (i <= uint.MaxValue) return FloorLog2((uint)i);
        return FloorLog2((uint)(i >> 32)) + 32;
    }

    private static byte[] bytesReversed = new byte[]
    {
            0, 128, 64, 192, 32, 160, 96, 224, 16, 144, 80, 208, 48, 176, 112, 240,
            8, 136, 72, 200, 40, 168, 104, 232, 24, 152, 88, 216, 56, 184, 120, 248,
            4, 132, 68, 196, 36, 164, 100, 228, 20, 148, 84, 212, 52, 180, 116, 244,
            12, 140, 76, 204, 44, 172, 108, 236, 28, 156, 92, 220, 60, 188, 124, 252,
            2, 130, 66, 194, 34, 162, 98, 226, 18, 146, 82, 210, 50, 178, 114, 242,
            10, 138, 74, 202, 42, 170, 106, 234, 26, 154, 90, 218, 58, 186, 122, 250,
            6, 134, 70, 198, 38, 166, 102, 230, 22, 150, 86, 214, 54, 182, 118, 246,
            14, 142, 78, 206, 46, 174, 110, 238, 30, 158, 94, 222, 62, 190, 126, 254,
            1, 129, 65, 193, 33, 161, 97, 225, 17, 145, 81, 209, 49, 177, 113, 241,
            9, 137, 73, 201, 41, 169, 105, 233, 25, 153, 89, 217, 57, 185, 121, 249,
            5, 133, 69, 197, 37, 165, 101, 229, 21, 149, 85, 213, 53, 181, 117, 245,
            13, 141, 77, 205, 45, 173, 109, 237, 29, 157, 93, 221, 61, 189, 125, 253,
            3, 131, 67, 195, 35, 163, 99, 227, 19, 147, 83, 211, 51, 179, 115, 243,
            11, 139, 75, 203, 43, 171, 107, 235, 27, 155, 91, 219, 59, 187, 123, 251,
            7, 135, 71, 199, 39, 167, 103, 231, 23, 151, 87, 215, 55, 183, 119, 247,
            15, 143, 79, 207, 47, 175, 111, 239, 31, 159, 95, 223, 63, 191, 127, 255,
    };

    /// <summary>
    /// Reverse the order of bits in a <see cref="System.Byte"/>.
    /// </summary>
    public static byte Reverse(byte b)
    {
        return bytesReversed[b];
    }

    /// <summary>
    /// Reverse the order of bits in a <see cref="System.Int16"/>.
    /// </summary>
    public static ushort Reverse(ushort s)
    {
        return (ushort)(
            (bytesReversed[(byte)s] << 8) |
            bytesReversed[s >> 8]);
    }

    /// <summary>
    /// Reverse the order of bits in a <see cref="System.Int32"/>.
    /// </summary>
    public static uint Reverse(uint i)
    {
        return (uint)(
            (bytesReversed[i & 0xFF] << 24) |
            (bytesReversed[(i >> 8) & 0xFF] << 16) |
            (bytesReversed[(i >> 16) & 0xFF] << 8) |
            bytesReversed[i >> 24]);
    }

    /* this is slightly faster on x64 */
    //public static ulong Reverse(ulong i)
    //{
    //    i = ((i >> 1) & 0x5555555555555555) | ((i << 1) & 0xAAAAAAAAAAAAAAAA);
    //    i = ((i >> 2) & 0x3333333333333333) | ((i << 2) & 0xCCCCCCCCCCCCCCCC);
    //    i = ((i >> 4) & 0x0F0F0F0F0F0F0F0F) | ((i << 4) & 0xF0F0F0F0F0F0F0F0);
    //    i = ((i >> 8) & 0x00FF00FF00FF00FF) | ((i << 8) & 0xFF00FF00FF00FF00);
    //    i = ((i >> 16) & 0x0000FFFF0000FFFF) | ((i << 16) & 0xFFFF0000FFFF0000);
    //    return (i >> 32) | (i << 32);
    //}

    /// <summary>
    /// Reverse the order of bits in a <see cref="System.Int64"/>.
    /// </summary>
    public static ulong Reverse(ulong i)
    {
        return
            ((ulong)bytesReversed[(byte)i] << 56) |
            ((ulong)bytesReversed[(byte)(i >> 8)] << 48) |
            ((ulong)bytesReversed[(byte)(i >> 16)] << 40) |
            ((ulong)bytesReversed[(byte)(i >> 24)] << 32) |
            ((ulong)bytesReversed[(byte)(i >> 32)] << 24) |
            ((ulong)bytesReversed[(byte)(i >> 40)] << 16) |
            ((ulong)bytesReversed[(byte)(i >> 48)] << 8) |
            ((ulong)bytesReversed[(byte)(i >> 56)]);
    }

    /// <summary>
    /// Determine if a <see cref="System.Byte"/> is an exact power of 2.
    /// </summary>
    public static bool IsPowerOf2(byte i)
    {
        return i != 0 && (i & (i - 1)) == 0;
    }

    /// <summary>
    /// Determine if a <see cref="System.SByte"/> is an exact power of 2.
    /// </summary>
    public static bool IsPowerOf2(sbyte i)
    {
        return i > 0 && (i & (i - 1)) == 0;
    }

    /// <summary>
    /// Determine if a <see cref="System.UInt16"/> is an exact power of 2.
    /// </summary>
    public static bool IsPowerOf2(ushort i)
    {
        return i != 0 && (i & (i - 1)) == 0;
    }

    /// <summary>
    /// Determine if a <see cref="System.Int16"/> is an exact power of 2.
    /// </summary>
    public static bool IsPowerOf2(short i)
    {
        return i > 0 && (i & (i - 1)) == 0;
    }

    /// <summary>
    /// Determine if a <see cref="System.UInt32"/> is an exact power of 2.
    /// </summary>
    public static bool IsPowerOf2(uint i)
    {
        return i != 0 && (i & (i - 1)) == 0;
    }

    /// <summary>
    /// Determine if a <see cref="System.Int32"/> is an exact power of 2.
    /// </summary>
    public static bool IsPowerOf2(int i)
    {
        return i > 0 && (i & (i - 1)) == 0;
    }

    /// <summary>
    /// Determine if a <see cref="System.UInt64"/> is an exact power of 2.
    /// </summary>
    public static bool IsPowerOf2(ulong i)
    {
        return i != 0 && (i & (i - 1)) == 0;
    }

    /// <summary>
    /// Determine if a <see cref="System.Int64"/> is an exact power of 2.
    /// </summary>
    public static bool IsPowerOf2(long i)
    {
        return i > 0 && (i & (i - 1)) == 0;
    }

    /// <summary>
    /// Bitwise rotation operator.
    /// </summary>
    public static uint RotateRight(uint i, int shift)
    {
        return (i >> shift) | (i << -shift);
    }

    /// <summary>
    /// Bitwise rotation operator.
    /// </summary>
    public static uint RotateLeft(uint i, int shift)
    {
        return (i << shift) | (i >> -shift);
    }

    /// <summary>
    /// Bitwise rotation operator.
    /// </summary>
    public static ulong RotateRight(ulong i, int shift)
    {
        return (i >> shift) | (i << -shift);
    }

    /// <summary>
    /// Bitwise rotation operator.
    /// </summary>
    public static ulong RotateLeft(ulong i, int shift)
    {
        return (i << shift) | (i >> -shift);
    }

    /// <summary>
    /// Select the bits from one of two values according to selection bits.
    /// </summary>
    /// <param name="a">Value to select bits from when selector bit is 0.</param>
    /// <param name="b">Value to select bits from when selector bit is 1.</param>
    /// <param name="selector">Selector bits.</param>
    public static sbyte Choose(sbyte a, sbyte b, sbyte selector)
    {
        return (sbyte)(a ^ ((a ^ b) & selector));
    }

    /// <summary>
    /// Select the bits from one of two values according to selection bits.
    /// </summary>
    /// <param name="a">Value to select bits from when selector bit is 0.</param>
    /// <param name="b">Value to select bits from when selector bit is 1.</param>
    /// <param name="selector">Selector bits.</param>
    public static byte Choose(byte a, byte b, byte selector)
    {
        return (byte)(a ^ ((a ^ b) & selector));
    }

    /// <summary>
    /// Select the bits from one of two values according to selection bits.
    /// </summary>
    /// <param name="a">Value to select bits from when selector bit is 0.</param>
    /// <param name="b">Value to select bits from when selector bit is 1.</param>
    /// <param name="selector">Selector bits.</param>
    public static short Choose(short a, short b, short selector)
    {
        return (short)(a ^ ((a ^ b) & selector));
    }

    /// <summary>
    /// Select the bits from one of two values according to selection bits.
    /// </summary>
    /// <param name="a">Value to select bits from when selector bit is 0.</param>
    /// <param name="b">Value to select bits from when selector bit is 1.</param>
    /// <param name="selector">Selector bits.</param>
    public static ushort Choose(ushort a, ushort b, ushort selector)
    {
        return (ushort)(a ^ ((a ^ b) & selector));
    }

    /// <summary>
    /// Select the bits from one of two values according to selection bits.
    /// </summary>
    /// <param name="a">Value to select bits from when selector bit is 0.</param>
    /// <param name="b">Value to select bits from when selector bit is 1.</param>
    /// <param name="selector">Selector bits.</param>
    public static int Choose(int a, int b, int selector)
    {
        return a ^ ((a ^ b) & selector);
    }

    /// <summary>
    /// Select the bits from one of two values according to selection bits.
    /// </summary>
    /// <param name="a">Value to select bits from when selector bit is 0.</param>
    /// <param name="b">Value to select bits from when selector bit is 1.</param>
    /// <param name="selector">Selector bits.</param>
    public static uint Choose(uint a, uint b, uint selector)
    {
        return a ^ ((a ^ b) & selector);
    }

    /// <summary>
    /// Select the bits from one of two values according to selection bits.
    /// </summary>
    /// <param name="a">Value to select bits from when selector bit is 0.</param>
    /// <param name="b">Value to select bits from when selector bit is 1.</param>
    /// <param name="selector">Selector bits.</param>
    public static long Choose(long a, long b, long selector)
    {
        return a ^ ((a ^ b) & selector);
    }

    /// <summary>
    /// Select the bits from one of two values according to selection bits.
    /// </summary>
    /// <param name="a">Value to select bits from when selector bit is 0.</param>
    /// <param name="b">Value to select bits from when selector bit is 1.</param>
    /// <param name="selector">Selector bits.</param>
    public static ulong Choose(ulong a, ulong b, ulong selector)
    {
        return a ^ ((a ^ b) & selector);
    }

    private static readonly int[] countOfLeadingZeros = new int[256]
    {
            8,
            7,
            6, 6,
            5, 5, 5, 5,
            4, 4, 4, 4, 4, 4, 4, 4,
            3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
    };

    /// <summary>
    /// Gets the count of leading 0 bits in the value.
    /// </summary>
    public static int CountOfLeadingZeros(byte i)
    {
        return countOfLeadingZeros[i];
    }

    /// <summary>
    /// Gets the count of leading 0 bits in the value.
    /// </summary>
    public static int CountOfLeadingZeros(sbyte i)
    {
        return countOfLeadingZeros[(byte)i];
    }

    /// <summary>
    /// Gets the count of leading 0 bits in the value.
    /// </summary>
    public static int CountOfLeadingZeros(ushort i)
    {
        int n = countOfLeadingZeros[i >> 8];
        if (n < 8) return n;
        return 8 + CountOfLeadingZeros((byte)i);
    }

    /// <summary>
    /// Gets the count of leading 0 bits in the value.
    /// </summary>
    public static int CountOfLeadingZeros(short i)
    {
        int n = countOfLeadingZeros[(ushort)i >> 8];
        if (n < 8) return n;
        return 8 + CountOfLeadingZeros((byte)i);
    }

    /// <summary>
    /// Gets the count of leading 0 bits in the value.
    /// </summary>
    public static int CountOfLeadingZeros(uint i)
    {
        int n = countOfLeadingZeros[i >> 24];
        if (n < 8) return n;
        n = countOfLeadingZeros[i >> 16];
        if (n < 8) return 8 + n;
        n = countOfLeadingZeros[i >> 8];
        if (n < 8) return 16 + n;
        return 24 + CountOfLeadingZeros((byte)i);
    }

    /// <summary>
    /// Gets the count of leading 0 bits in the value.
    /// </summary>
    public static int CountOfLeadingZeros(int i)
    {
        return CountOfLeadingZeros((uint)i);
    }

    /// <summary>
    /// Gets the count of leading 0 bits in the value.
    /// </summary>
    public static int CountOfLeadingZeros(ulong i)
    {
        int n = CountOfLeadingZeros((uint)(i >> 32));
        if (n < 32) return n;
        return 32 + CountOfLeadingZeros((uint)i);
    }

    /// <summary>
    /// Gets the count of leading 0 bits in the value.
    /// </summary>
    public static int CountOfLeadingZeros(long i)
    {
        return CountOfLeadingZeros((ulong)i);
    }
}
