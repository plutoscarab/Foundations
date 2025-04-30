
namespace Foundations;

public static partial class Sequences
{
    public static IEnumerable<T> Scan<T>(this IEnumerable<T> sequence, T initial, Func<T, T, T> aggregator)
    {
        var n = initial;
        yield return n;

        foreach (var s in sequence)
        {
            n = aggregator(n, s);
            yield return n;
        }
    }

    public static IEnumerable<T> Scan<T>(this IEnumerable<T> sequence, Func<T, T, T> aggregator)
    {
        using var e = sequence.GetEnumerator();
        if (!e.MoveNext()) yield break;
        var n = e.Current;
        yield return n;

        while (e.MoveNext())
        {
            n = aggregator(n, e.Current);
            yield return n;
        }
    }

    public static IEnumerable<T> ScanSum<T>(this IEnumerable<T> sequence, T initial) 
        where T : IAdditionOperators<T, T, T>
    {
        var n = initial;
        yield return n;

        foreach (var s in sequence)
        {
            n += s;
            yield return n;
        }
    }

    public static IEnumerable<T> ScanSum<T>(this IEnumerable<T> sequence) where T : IAdditionOperators<T, T, T>
    {
        using var e = sequence.GetEnumerator();
        if (!e.MoveNext()) yield break;
        var n = e.Current;
        yield return n;

        while (e.MoveNext())
        {
            n += e.Current;
            yield return n;
        }
    }

    public static IEnumerable<T> ScanProduct<T>(this IEnumerable<T> sequence, T initial) 
        where T : IMultiplyOperators<T, T, T>
    {
        var n = initial;
        yield return n;

        foreach (var s in sequence)
        {
            n *= s;
            yield return n;
        }
    }

    public static IEnumerable<T> ScanProduct<T>(this IEnumerable<T> sequence) where T : IMultiplyOperators<T, T, T>
    {
        using var e = sequence.GetEnumerator();
        if (!e.MoveNext()) yield break;
        var n = e.Current;
        yield return n;

        while (e.MoveNext())
        {
            n *= e.Current;
            yield return n;
        }
    }
}