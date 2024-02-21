
namespace Foundations.Coding;

/// <summary>
/// Encoding implementations.
/// </summary>
public static partial class Codes
{
    /// <summary>
    /// Levenshtein code. Value is preceded by bit length of value
    /// recursively, and then preceded by unary-coded number of steps
    /// needed to reach 0.
    /// </summary>
    public static readonly IBitEncoding Levenshtein = new Levenshtein();
}

/// <summary>
/// Levenshtein code. Value is preceded by bit length of value
/// recursively, and then preceded by unary-coded number of steps
/// needed to reach 0.
/// </summary>
public sealed partial class Levenshtein : IBitEncoding
{
    internal Levenshtein()
    {
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
        int length = 0;
        int steps = 0;

        while (value != 0)
        {
            int f = Bits.FloorLog2(value);
            value ^= 1 << f;
            bits |= (ulong)value << length;
            length += f;
            steps++;
            value = f;
        }

        bits |= ((1UL << steps) - 1) << (length + 1);
        length += steps + 1;
        return new Code(bits, length);
    }

    /// <summary>
    /// Reads an encoded value from a <see cref="BitReader"/>.
    /// </summary>
    public int Read(BitReader reader)
    {
        int u = Codes.UnaryOnes.Read(reader) - 1;
        if (u == 0) return 0;
        int value = 1;

        while (--u > 0)
        {
            value = (int)reader.Read(value) | (1 << value);
        }

        return value;
    }
}
