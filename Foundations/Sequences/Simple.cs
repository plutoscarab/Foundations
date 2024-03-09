
namespace Foundations;

public static partial class Sequences
{
    public static IEnumerable<long> NonNegativeIntegers()
    {
        var n = 0L;

        while (n >= 0)
        {
            yield return n;
            n++;
        }
    }

    public static IEnumerable<long> Evens()
    {
        var n = 0L;

        while (n >= 0)
        {
            yield return n;
            n += 2;
        }
    }

    public static IEnumerable<long> Odds()
    {
        var n = 1L;

        while (n >= 0)
        {
            yield return n;
            n += 2;
        }
    }

    public static IEnumerable<long> Squares()
    {
        var n = 0L;

        foreach (var odd in Odds())
        {
            yield return n;
            n += odd;
            if (n < 0) yield break;
        }
    }

    public static IEnumerable<long> MultiplesOf(int k)
    {
        var n = 0L;

        while (n >= 0)
        {
            yield return n;
            n += k;
        }
    }

    public static IEnumerable<long> PartialSums(IEnumerable<long> sequence)
    {
        var n = 0L;

        foreach (var s in sequence)
        {
            yield return n;
            n += s;
        }
    }
}