
namespace Foundations.Coding;

/// <summary>
/// Encoding implementations.
/// </summary>
public static partial class Codes
{
    /// <summary>
    /// Rice code. Lower bits of value are preceded by unary-coded 
    /// value of upper bits.
    /// </summary>
    public static IBitEncoding Rice(int exponentOf2) => new Rice(exponentOf2);
}

/// <summary>
/// Rice code. Lower bits of value are preceded by unary-coded 
/// value of upper bits.
/// </summary>
public sealed partial class Rice : IBitEncoding
{
    private readonly int exponentOf2;
    private readonly int mask;
    private readonly int maxEncodable;

    internal Rice(int exponentOf2)
    {
        if (exponentOf2 < 1 || exponentOf2 > 31)
            throw new ArgumentOutOfRangeException();

        this.exponentOf2 = exponentOf2;
        mask = (1 << exponentOf2) - 1;
        maxEncodable = (63 - exponentOf2) * (mask + 1) + mask;
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
            return maxEncodable;
        }
    }

    /// <summary>
    /// Gets the code corresponding to a value.
    /// </summary>
    public Code GetCode(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MinEncodable, nameof(value));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxEncodable, nameof(value));

        return new Code(
            Codes.UnaryOnes.GetCode((value >> exponentOf2) + 1),
            new Code(value & mask, exponentOf2));
    }

    /// <summary>
    /// Reads an encoded value from a <see cref="BitReader"/>.
    /// </summary>
    public int Read(BitReader reader)
    {
        int u = Codes.UnaryOnes.Read(reader) - 1;
        return (int)reader.Read(exponentOf2) + (u << exponentOf2);
    }
}
