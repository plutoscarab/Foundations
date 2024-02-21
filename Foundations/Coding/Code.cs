
namespace Foundations.Coding;

/// <summary>
/// Represents a sequence of 0 to 64 bits
/// </summary>
public struct Code
{
    /// <summary>
    /// Zero-length code.
    /// </summary>
    public static readonly Code Empty = new Code(0, 0);

    /// <summary>
    /// Single '0' bit.
    /// </summary>
    public static readonly Code Zero = new Code(0, 1);

    /// <summary>
    /// Single '1' bit.
    /// </summary>
    public static readonly Code One = new Code(1, 1);

    private ulong bits;
    private int length;

    /// <summary>
    /// The bits. The unused most-significant bits are set to zero.
    /// </summary>
    public ulong Bits
    { get { return bits; } }

    /// <summary>
    /// The number of bits in the code
    /// </summary>
    public int Length
    { get { return length; } }

    /// <summary>
    /// Creates a Code object containing the specified bits and length.
    /// </summary>
    /// <param name="bits">The bits. The unused most-significant bits are set to zero.</param>
    /// <param name="length">The number of bits in the code.</param>
    public Code(ulong bits, int length)
    {
        if (length < 0 || length > 64)
            throw new Exception();

        if (length < 64) bits &= (1UL << length) - 1;
        this.bits = bits;
        this.length = length;
    }

    /// <summary>
    /// Creates a Code object containing the specified bits and length.
    /// </summary>
    /// <param name="bits">The bits. The unused most-significant bits are set to zero.</param>
    /// <param name="length">The number of bits in the code.</param>
    public Code(long bits, int length)
        : this((ulong)bits, length)
    {
    }

    /// <summary>
    /// Creates a code by concatenating existing codes.
    /// </summary>
    public Code(params Code[] codes)
    {
        int length = 0;
        int count = codes.Length;
        ulong bits = 0;

        for (int i = 0; i < count; i++)
        {
            bits <<= codes[i].Length;
            bits |= codes[i].Bits;
            length += codes[i].Length;
        }

        if (length > 64)
            throw new ArgumentOutOfRangeException();

        this.bits = bits;
        this.length = length;
    }

    /// <summary>
    /// Creates a code by appending a code to an existing one.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public Code Append(Code code)
    {
        return new Code(this, code);
    }

    /// <summary>
    /// Converts the bits of this instance to its equivalent string representation.
    /// </summary>
    /// <returns>Returns a string of '0' and '1' characters representing the code bits.</returns>
    public override string ToString()
    {
        var b = new StringBuilder();

        for (int i = 0; i < Length; i++)
        {
            if ((Bits & (1UL << (Length - i - 1))) == 0)
                b.Append('0');
            else
                b.Append('1');
        }

        return b.ToString();
    }

    /// <summary>
    /// Reverses the order of the bits in the code.
    /// </summary>
    /// <returns>A code of the same length with the bits in reverse order.</returns>
    public Code Reverse()
    {
        return new Code(ReverseBits(Bits, Length), Length);
    }

    /// <summary>
    /// Reverses the order of bits in a number.
    /// </summary>
    /// <param name="bits">The original bits, with zero's in the unused MSB's.</param>
    /// <param name="length">The number of bits to reverse.</param>
    /// <returns></returns>
    public static ulong ReverseBits(ulong bits, int length)
    {
        if (length < 1)
            throw new ArgumentOutOfRangeException();

        ulong temp = 0;

        while (length-- > 0)
        {
            temp = (temp << 1) | (bits & 1);
            bits >>= 1;
        }

        return temp;
    }

    /// <summary>
    /// Get take the leading bits from the <see cref="Code"/>.
    /// </summary>
    /// <param name="bitCount">The number of leading bits to return.</param>
    /// <param name="rest">The rest of the code with those leading bits removed.</param>
    public ulong TakeBits(int bitCount, out Code rest)
    {
        if (bitCount < 0 || bitCount > length)
            throw new ArgumentOutOfRangeException(nameof(bitCount));

        int newLength = length - bitCount;
        var result = bits >> newLength;
        rest = new Code(bits, newLength);
        return result;
    }

    /// <summary>
    /// Gets the code bits with 0's exchanged for 1's and vice versa.
    /// </summary>
    public ulong InvertedBits
    {
        get
        {
            if (length == 64) return ~bits;
            return ~bits & ((1UL << length) - 1);
        }
    }

    static char[] upperCaseLetters = Enumerable.Range(0, 65536)
        .Select(c => (char)c)
        .Where(c => char.IsUpper(c))
        .ToArray();

    internal void ThrowInvalidCode(Type type)
    {
        var name = type.Name;
        int u = name.IndexOfAny(upperCaseLetters, 1);
        if (u != -1) name = name.Substring(0, u) + " " + name.Substring(u);
        throw new ArgumentException($"Code {this} is not a valid {name} code.");
    }
}
