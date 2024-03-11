
// Implementation of Coinductive Definitions and Real Numbers, BSc Final Year Project Report, Michael Herrmann, June 2009

using System.Collections;

namespace Foundations.Types;

public sealed class Sequence<T>(IEnumerable<T> Items) : IEnumerable<T>
{
    private readonly List<T> cached = [];
    private readonly IEnumerator<T> next = Items.GetEnumerator();

    private IEnumerable<T> Items()
    {
        var i = 0;

        while (true)
        {
            while (i >= cached.Count)
            {
                lock (next)
                {
                    if (!next.MoveNext())
                        yield break;

                    cached.Add(next.Current);
                }
            }

            yield return cached[i++];
        }
    }

    public IEnumerator<T> GetEnumerator() => Items().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Items().GetEnumerator();
}

/// <summary>
/// A computable real number in the range [-1, 1] defined by a sequence of signed binary digits in {-1, 0, 1}.
/// The value is sum(n=0 to inf, seq[n]·2⁻ⁿ⁻¹) = (seq[0] + value(seq[1..])) / 2.
/// </summary>
public struct SignedBinaryStream(Sequence<int> Digits) : IEnumerable<int>
{
    public SignedBinaryStream(double d) : this(new Sequence<int>(DigitsFrom(d)))
    {
    }

    public SignedBinaryStream(Rational r) : this(new Sequence<int>(DigitsFrom(r)))
    {
    }

    private static IEnumerable<int> DigitsFrom(Rational r)
    {
        if (r < -1 || r > 1)
            throw new ArgumentOutOfRangeException(nameof(r));

        while (r != Rational.Zero)
        {
            var s = GeneralizedSign(r, Rational.One / 4);
            yield return s;
            r = 2 * r - s;
        }
    }

    private static IEnumerable<int> DigitsFrom(double d)
    {
        if (d < -1 || d > 1)
            throw new ArgumentOutOfRangeException(nameof(d));

        return DigitsFrom((Rational)d);
    }

    public static int GeneralizedSign(Rational q, Rational ε)
    {
        if (q < -ε) return -1;
        if (q > ε) return 1;
        return 0;
    }

    private static IEnumerable<int> Average(IEnumerable<int> a, IEnumerable<int> b)
    {
        using var ea = a.GetEnumerator();
        using var eb = b.GetEnumerator();
        ea.MoveNext();
        var a1 = ea.Current;
        eb.MoveNext();
        var b1 = eb.Current;

        while (true)
        {
            ea.MoveNext();
            var a2 = ea.Current;
            eb.MoveNext();
            var b2 = eb.Current;
            var p = (Rational)(a1 + b1) / 4 + (Rational)(a2 + b2) / 8;
            var s = GeneralizedSign(p, Rational.One / 4);
            yield return s;
            a1 = GeneralizedSign(p - s / Rational.Two, Rational.One / 8);
            b1 = (int)(8 * p).P - 4 * s - a1;
        }
    }

    public static SignedBinaryStream Average(SignedBinaryStream a, SignedBinaryStream b)
    {
        return new(new Sequence<int>(Average(a.AsEnumerable(), b.AsEnumerable())));
    }

    private static IEnumerable<int> Lin(Rational u, IEnumerable<int> a, Rational v)
    {
        if (Rational.Abs(u) + Rational.Abs(v) > Rational.One)
            throw new ArgumentOutOfRangeException(null, "|u| + |v| > 1");

        using var ea = a.GetEnumerator();

        while (true)
        {
            ea.MoveNext();
            var a1 = ea.Current;
            ea.MoveNext();
            var a2 = ea.Current;
            var q = u * (a1 / Rational.Two + a2 / (Rational)4) + v;
            var l = GeneralizedSign(q, Rational.One / 4);
            yield return l;
            u /= 2;
            v = 2 * q - l;
        }
    }

    public static SignedBinaryStream Lin(Rational u, SignedBinaryStream a, Rational v)
    {
        return new(new Sequence<int>(Lin(u, a.AsEnumerable(), v)));
    }

    private readonly IEnumerable<int> Bits()
    {
        foreach (var digit in Digits)
        {
            if (digit < -1 || digit > 1)
                throw new ArgumentOutOfRangeException(nameof(Digits));

            yield return digit;
        }

        while (true)
            yield return 0;
    }

    public readonly IEnumerator<int> GetEnumerator() => Bits().GetEnumerator();

    readonly IEnumerator IEnumerable.GetEnumerator() => Bits().GetEnumerator();

    public override readonly string ToString()
    {
        var digits = Bits().Take(11).ToArray();

        if (digits.Length < 11)
            return string.Join(", ", digits);

        return string.Join(", ", digits.Take(10)) + ", ...";
    }

    public static SignedBinaryStream operator +(SignedBinaryStream x) => x;

    public static SignedBinaryStream operator -(SignedBinaryStream x) =>
        new(new Sequence<int>(x.Select(d => -d)));

    public readonly Rational Value(int precision)
    {
        Rational p = 1;
        Rational s = 0;

        foreach (var digit in Bits().Take(precision))
        {
            p /= 2;
            s += digit * p;
        }

        return s;
    }
}

public struct SignedBinary(int exponent, SignedBinaryStream mantissa)
{
    public readonly int Exponent => exponent;

    public readonly SignedBinaryStream Mantissa => mantissa;

    public SignedBinary(SignedBinaryStream mantissa) : this(0, mantissa)
    {
    }

    public SignedBinary(double d) : this(ExponentFrom(d), MantissaFrom(d))
    {
    }

    private static int ExponentFrom(double d)
    {
        return Math.ILogB(d) + 1;
    }

    private static SignedBinaryStream MantissaFrom(double d)
    {
        d = Math.ScaleB(d, -ExponentFrom(d));
        return new(d);
    }

    public override readonly string ToString()
    {
        if (exponent == 0) return "[" + mantissa + "]";
        return "2" + exponent.ToSuperscript() + " [" + mantissa + "]";
    }

    public static SignedBinary operator +(SignedBinary x, SignedBinary y)
    {
        var (e, a) = (x.Exponent, x.Mantissa);
        var (f, b) = (y.Exponent, y.Mantissa);
        var m = Math.Max(e, f);
        Sequence<int> dx = new(Enumerable.Repeat(0, m - e).Concat(a));
        Sequence<int> dy = new(Enumerable.Repeat(0, m - f).Concat(b));
        return new(m + 1, SignedBinaryStream.Average(new(dx), new(dy)));
    }

    public readonly Rational Value(int precision) =>
        Mantissa.Value(precision) * Rational.Pow(2, Exponent);

    private static int CeilLog2(Rational s) =>
        s <= 1 ? 0 : 1 + CeilLog2(s / 2);

    public static SignedBinary Lin(Rational u, SignedBinary x, Rational v)
    {
        var n = CeilLog2(Rational.Abs(u) + Rational.Abs(v));
        var u_ = u / Rational.Pow(2, n);
        var v_ = v / Rational.Pow(2, n + x.Exponent);
        return new(x.Exponent + n, SignedBinaryStream.Lin(u_, x.Mantissa, v_));
    }
}