using System.IO;

namespace Foundations.Coding;

/// <summary>
/// Encoding implementations.
/// </summary>
public static partial class Codes
{
    /// <summary>
    /// Baer Code -1 for positive integers.
    /// </summary>
    public static readonly IBitEncoding BaerCodeNeg1 = new BaerCodeNeg1();
}

/// <summary>
/// Baer Code -1. The value one is encoded as zero. Larger values start with one,
/// followed by Baer Code 0 for the value minus one.
/// </summary>
public sealed partial class BaerCodeNeg1 : IBitEncoding
{
    internal BaerCodeNeg1()
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

        if (value == 1)
            return Code.Zero;

        return new Code(Code.One, Codes.BaerCode0.GetCode(value - 1));
    }

    /// <summary>
    /// Reads an encoded value from a <see cref="BitReader"/>.
    /// </summary>
    public int Read(BitReader reader)
    {
        if (reader.Read(1) == 0)
            return 1;

        int value = Codes.BaerCode0.Read(reader);
        if (value == int.MaxValue)
            throw new InvalidDataException("Encoded value exceeds Int32 bounds.");

        return value + 1;
    }
}
