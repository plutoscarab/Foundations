
namespace Foundations.Coding;

/// <summary>
/// Encoding implementations.
/// </summary>
public static partial class Codes
{
    /// <summary>
    /// FixedWidth code. Values are encoded as-is, prepended with leading
    /// '0' bits to form a fixed-width code.
    /// </summary>
    public static IBitEncoding FixedWidth(int bitCount) => new FixedWidth(bitCount);
}

/// <summary>
/// FixedWidth code. Values are encoded as-is, prepended with leading
/// '0' bits to form a fixed-width code.
/// </summary>
public sealed partial class FixedWidth : IBitEncoding
{
    int bitCount;

    internal FixedWidth(int bitCount)
    {
        if (bitCount < 1 || bitCount > 31) throw new ArgumentOutOfRangeException();
        this.bitCount = bitCount;
    }

    /// <summary>
    /// Gets the minimum number that can be encoded.
    /// </summary>
    public int MinEncodable
    {
        get
        {
            return 0;
        }
    }

    /// <summary>
    /// Gets the minimum number that can be encoded.
    /// </summary>
    public int MaxEncodable
    {
        get
        {
            return (1 << bitCount) - 1;
        }
    }

    /// <summary>
    /// Gets the code corresponding to a value.
    /// </summary>
    public Code GetCode(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MinEncodable, nameof(value));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxEncodable, nameof(value));

        return new Code(value, bitCount);
    }

    /// <summary>
    /// Gets the value corresponding to a code.
    /// </summary>
    public int Read(BitReader reader)
    {
        return (int)reader.Read(bitCount);
    }
}
