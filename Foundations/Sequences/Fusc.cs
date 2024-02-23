
// https://oeis.org/A002487

using Foundations.Types;

namespace Foundations;

public static partial class Sequences
{
    /// <summary>
    /// Stern-Brocot sequence 0, 1, 1, 2, 1, 3, 2, 3, 1, 4, 3, 5, 2, 5, 3...
    /// </summary>
    public static IEnumerable<int> Fusc()
    {
        yield return 0;
        yield return 1;
        yield return 1;
        var n = 1;

        foreach (var f in Fusc().Skip(2))
        {
            yield return n + f;
            yield return f;
            n = f;
        }
    }

    public static IEnumerable<Rational> Rationals()
    {
        var p = Fusc().First();

        foreach (var q in Fusc().Skip(1))
        {
            yield return new(p, q);
            p = q;
        }
    }
}
