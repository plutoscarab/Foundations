
// https://oeis.org/A000108

namespace Foundations;

public static partial class Sequences
{
    /// <summary>
    /// Catalan numbers, 1, 1, 2, 5, 14, 42, 132, etc., starting at C(0).
    /// Ends when the next value exceeds Int64.MaxValue.
    /// </summary>
    public static IEnumerable<long> CatalanNumbers()
    {
        foreach (var c in CatalanNumbersB())
        {
            if (c > long.MaxValue) yield break;
            yield return (long)c;
        }
    }

    /// <summary>
    /// Catalan numbers as doubles, starting at C(0).
    /// Ends when the next value rounds to positive infinity.
    /// </summary>
    public static IEnumerable<double> CatalanNumbersD()
    {
        foreach (var c in CatalanNumbersB())
        {
            var d = (double)c;
            if (double.IsInfinity(d)) yield break;
            yield return d;
        }
    }

    /// <summary>
    /// Catalan numbers with arbitrary precision, starting at C(0).
    /// </summary>
    public static IEnumerable<BigInteger> CatalanNumbersB()
    {
        BigInteger c = 1, n = 0;

        while (true)
        {
            yield return c;
            c = c * 2 * (2 * n + 1) / (n + 2);
            n++;
        }
    }
}
