
// https://oeis.org/A027641 over https://oeis.org/A027642

using Foundations.Types;

namespace Foundations;

public static partial class Sequences
{
    public static IEnumerable<Rational> BernoulliNumbers()
    {
        yield return Rational.One;
        List<Rational> list = [Rational.OneHalf];
        BigInteger d = 2;

        while (true)
        {
            yield return list[0];
            ++d;
            list.Add(new(1, d));

            for (var i = list.Count - 2; i >= 0; i--)
            {
                list[i] = (i + 1) * (list[i] - list[i + 1]);
            }
        }
    }

    public static IEnumerable<Rational> BernoulliEvenIndexed()
    {
        yield return Rational.One;
        List<Rational> list = [Rational.OneHalf];
        BigInteger d = 2;

        while (true)
        {
            ++d;

            if (d.IsEven)
                yield return list[0];
                
            list.Add(new(1, d));

            for (var i = list.Count - 2; i >= 0; i--)
            {
                list[i] = (i + 1) * (list[i] - list[i + 1]);
            }
        }
    }
}