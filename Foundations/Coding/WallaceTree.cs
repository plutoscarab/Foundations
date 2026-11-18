
using System.IO;

namespace Foundations.Coding;

/// <summary>
/// Encoding implementations.
/// </summary>
public static partial class Codes
{
    /// <summary>
    /// Wallace tree code for non-negative integers, ordered by length and then lexicographically.
    /// </summary>
    public static readonly IBitEncoding WallaceTree = new WallaceTree();
}

/// <summary>
/// Wallace tree code. A full binary tree is written in preorder with one for a fork
/// and zero for a leaf. The value zero is the single-leaf tree, encoded as zero.
/// </summary>
public sealed partial class WallaceTree : IBitEncoding
{
    private static readonly ImmutableArray<BigInteger> catalans =
        Sequences.CatalanNumbersB().TakeWhile(c => c <= int.MaxValue).ToImmutableArray();
        
    private static readonly ImmutableArray<ImmutableArray<BigInteger>> counts = PrecomputeCounts();

    internal WallaceTree()
    {
    }

    /// <summary>
    /// Gets the minimum number that can be encoded.
    /// </summary>
    public int MinEncodable => 0;

    /// <summary>
    /// Gets the maximum number that can be encoded.
    /// </summary>
    public int MaxEncodable => int.MaxValue;

    /// <summary>
    /// Gets the code corresponding to a value.
    /// </summary>
    public Code GetCode(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);

        BigInteger rank = value;
        var forks = 0;

        while (rank >= catalans[forks])
            rank -= catalans[forks++];

        var leaves = forks;
        var remainingForks = forks;
        ulong bits = 0;

        // Paths start at (forks, forks). The final leaf is appended separately.
        while (leaves > 0)
        {
            var zeroCount = counts[leaves - 1][remainingForks];
            bits <<= 1;

            if (rank < zeroCount)
            {
                leaves--;
            }
            else
            {
                bits |= 1;
                rank -= zeroCount;
                remainingForks--;
            }
        }

        return new Code(bits << 1, 2 * forks + 1);
    }

    /// <summary>
    /// Reads an encoded value from a <see cref="BitReader"/>.
    /// </summary>
    public int Read(BitReader reader)
    {
        ulong bits = 0;
        var slots = 1;
        var forks = 0;
        var length = 0;

        while (slots > 0)
        {
            var bit = reader.Read(1);
            bits = (bits << 1) | bit;
            length++;

            if (bit == 0)
            {
                slots--;
            }
            else
            {
                if (++forks >= catalans.Length)
                    throw new InvalidDataException("Encoded value exceeds Int32 bounds.");

                slots++;
            }
        }

        BigInteger rank = 0;

        for (var i = 0; i < forks; i++)
            rank += catalans[i];

        var leaves = forks;
        var remainingForks = forks;

        // The last zero terminates the tree and contributes nothing to its rank.
        for (var i = length - 1; i > 0; i--)
        {
            if ((bits & (1UL << i)) == 0)
            {
                leaves--;
            }
            else
            {
                rank += counts[leaves - 1][remainingForks];
                remainingForks--;
            }
        }

        if (rank > int.MaxValue)
            throw new InvalidDataException("Encoded value exceeds Int32 bounds.");

        return (int)rank;
    }

    private static ImmutableArray<ImmutableArray<BigInteger>> PrecomputeCounts()
    {
        var table = ImmutableArray.CreateBuilder<ImmutableArray<BigInteger>>(catalans.Length);

        for (var leaves = 0; leaves < catalans.Length; leaves++)
        {
            var row = ImmutableArray.CreateBuilder<BigInteger>(leaves + 2);
            row.Add(BigInteger.One);

            for (var i = 1; i <= leaves; i++)
                row.Add(table[leaves - 1][i] + row[i - 1]);

            // The extra zero covers a fork count one greater than the leaf count.
            row.Add(BigInteger.Zero);
            table.Add(row.MoveToImmutable());
        }

        return table.MoveToImmutable();
    }
}
