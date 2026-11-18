using System.IO;

namespace Foundations.Coding;

/// <summary>
/// Encoding implementations.
/// </summary>
public static partial class Codes
{
    /// <summary>
    /// Baer Code 0 for positive integers.
    /// </summary>
    public static readonly IBitEncoding BaerCode0 = new BaerCode0();
}

/// <summary>
/// Baer Code 0. A unary prefix is followed by a truncated binary code and a fixed-width suffix.
/// </summary>
public sealed partial class BaerCode0 : IBitEncoding
{
    private const int MaxSuffixBits = 29;
    private static readonly IBitEncoding leadingCode = Codes.TruncatedBinary(3);

    internal BaerCode0()
    {
    }

    /// <summary>
    /// Gets the minimum number that can be encoded.
    /// </summary>
    public int MinEncodable => 1;

    /// <summary>
    /// Gets the maximum number that can be encoded.
    /// </summary>
    public int MaxEncodable => int.MaxValue;

    /// <summary>
    /// Gets the code corresponding to a value.
    /// </summary>
    public Code GetCode(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MinEncodable, nameof(value));

        // Adding two turns Baer's recursive reduction into a right shift.
        // Keep a leading value of 3, 4, or 5 and retain each shifted-out bit.
        ulong adjusted = (ulong)value + 2;
        ulong leading = adjusted;
        int suffixBits = 0;

        while (leading > 5)
        {
            leading >>= 1;
            suffixBits++;
        }

        return new Code(
            Codes.UnaryOnes.GetCode(suffixBits + 1),
            leadingCode.GetCode((int)leading - 3),
            new Code(adjusted, suffixBits));
    }

    /// <summary>
    /// Reads an encoded value from a <see cref="BitReader"/>.
    /// </summary>
    public int Read(BitReader reader)
    {
        int suffixBits = 0;

        // Int32.MaxValue + 2 takes at most 29 shifts to reach 3, 4, or 5.
        while (reader.Read(1) != 0)
        {
            if (++suffixBits > MaxSuffixBits)
                throw new InvalidDataException("Encoded value exceeds Int32 bounds.");
        }

        ulong leading = (ulong)(leadingCode.Read(reader) + 3);
        ulong adjusted = (leading << suffixBits) | reader.Read(suffixBits);
        ulong value = adjusted - 2;

        if (value > int.MaxValue)
            throw new InvalidDataException("Encoded value exceeds Int32 bounds.");

        return (int)value;
    }
}
