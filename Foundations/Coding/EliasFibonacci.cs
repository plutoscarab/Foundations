
namespace Foundations.Coding;

/// <summary>
/// Encoding implementations.
/// </summary>
public static partial class Codes
{
    /// <summary>
    /// EliasFibonacci code. Value is precided by Fibonacci-coded bit length.
    /// </summary>
    public static readonly IBitEncoding EliasFibonacci = new EliasFibonacci();
}

/// <summary>
/// EliasFibonacci code. Value is precided by Fibonacci-coded bit length.
/// </summary>
public sealed partial class EliasFibonacci : IBitEncoding
{
    internal EliasFibonacci()
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

        int b = Bits.FloorLog2(value);
        var f = Codes.Fibonacci.GetCode(b + 1);
        ulong bits = (uint)value | (f.Bits << b);
        int length = b + f.Length;
        return new Code(bits, length);
    }

    /// <summary>
    /// Reads an encoded value from a <see cref="BitReader"/>.
    /// </summary>
    public int Read(BitReader reader)
    {
        int f = Codes.Fibonacci.Read(reader) - 1;
        int value = (int)reader.Read(f);
        return value | (1 << f);
    }
}
