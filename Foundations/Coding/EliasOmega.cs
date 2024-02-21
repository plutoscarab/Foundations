
namespace Foundations.Coding;

/// <summary>
/// Encoding implementations.
/// </summary>
public static partial class Codes
{
    /// <summary>
    /// EliasOmega code. Value is preceded by EliasOmega-encoded bit length.
    /// </summary>
    public static readonly IBitEncoding EliasOmega = new EliasOmega();
}

/// <summary>
/// EliasOmega code. Value is preceded by EliasOmega-encoded bit length.
/// </summary>
public sealed partial class EliasOmega : IBitEncoding
{
    internal EliasOmega()
    {
    }

    /// <summary>
    /// Gets the minimum number that can be encoded.
    /// </summary>
    public int MinEncodable
    {
        get
        {
            return 1;
        }
    }

    /// <summary>
    /// Gets the minimum number that can be encoded.
    /// </summary>
    public int MaxEncodable
    {
        get
        {
            return int.MaxValue;
        }
    }

    /// <summary>
    /// Gets the code corresponding to a value.
    /// </summary>
    public Code GetCode(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MinEncodable, nameof(value));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxEncodable, nameof(value));

        ulong bits = 0;
        int length = 1;

        while (value > 1)
        {
            bits |= (ulong)value << length;
            value = Bits.FloorLog2(value);
            length += value + 1;
        }

        return new Code(bits, length);
    }

    /// <summary>
    /// Gets the value corresponding to a code.
    /// </summary>
    public int Read(BitReader reader)
    {
        int value = 1;

        while (reader.Read(1) != 0)
        {
            value = (int)(reader.Read(value) | (1UL << value));
        }

        return value;
    }
}
