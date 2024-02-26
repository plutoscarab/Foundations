
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Foundations.Types;

public readonly struct LimitedRational : INumber<LimitedRational>, IExponentialFunctions<LimitedRational>,
    ILogarithmicFunctions<LimitedRational>
{
    static LimitedRational()
    {
        zero = new(Rational.Zero, false);
        one = new(Rational.One, false);
        two = new(Rational.Two, false);
        nan = new(Rational.NaN, false);
        SetPrecision(235);
    }

    private static int digits;
    private static int precisionBytes;

    private static LimitedRational e;
    private static LimitedRational pi;
    private static LimitedRational tau;
    private static LimitedRational log2;
    private static LimitedRational log10;

    private static void SetPrecision(int precisionInDigits)
    {
        var bits = (int)Math.Ceiling((precisionInDigits + 5) * Math.Log2(10));
        precisionBytes = (bits + 7) / 8;
        digits = precisionInDigits;
        e = Compute(ContinuedFraction.E);
        pi = Compute(ContinuedFraction.Pi);
        tau = Compute(n => ContinuedFraction.Pi(n).Transform(0, 2, 1, 0));
        log2 = Compute(n => ContinuedFraction.Log(2, n));
        log10 = Compute(n => ContinuedFraction.Log(10, n));
    }

    private readonly Rational r;

    public LimitedRational(Rational r)
    {
        this.r = Normalize(r);
    }

    private LimitedRational(Rational r, bool _)
    {
        this.r = r;
    }

    private static readonly LimitedRational one;

    public static LimitedRational One => one;

    private static readonly LimitedRational two;

    public static LimitedRational Two => two;

    public static int Radix => 2;

    private static readonly LimitedRational zero;

    public static LimitedRational Zero => zero;

    public static LimitedRational AdditiveIdentity => zero;

    public static LimitedRational MultiplicativeIdentity => one;

    private static LimitedRational Compute(Func<int, ContinuedFraction> terms)
    {
        var p = precisionBytes;
        var k = Rational.One;

        foreach (var c in ContinuedFraction.Convergents(terms(p * 6)))
        {
            if (Math.Min(c.P.GetByteCount(), c.Q.GetByteCount()) > p)
                return new(k);

            k = c;
        }

        throw new InvalidOperationException();
    }

    public static LimitedRational E => e;

    public static LimitedRational Pi => pi;

    public static LimitedRational Tau => tau;


    public static bool IsCanonical(LimitedRational value) => true;

    public static bool IsComplexNumber(LimitedRational value) => false;

    public static bool IsEvenInteger(LimitedRational value) => value.r.Q == BigInteger.One && value.r.P.IsEven();

    public static bool IsFinite(LimitedRational value) => !value.r.IsInfinity && !value.r.IsNaN;

    public static bool IsImaginaryNumber(LimitedRational value) => false;

    public static bool IsInfinity(LimitedRational value) => value.r.IsInfinity;

    public static bool IsInteger(LimitedRational value) => value.r.Q == BigInteger.One;

    public static bool IsNaN(LimitedRational value) => value.r.IsNaN;

    public static bool IsNegative(LimitedRational value) => value.r < Rational.Zero;

    public static bool IsNegativeInfinity(LimitedRational value) => value.r.IsNegativeInfinity;

    public static bool IsNormal(LimitedRational value) => true;

    public static bool IsOddInteger(LimitedRational value) => value.r.Q == BigInteger.One && value.r.P.IsOdd();

    public static bool IsPositive(LimitedRational value) => value.r > Rational.Zero;

    public static bool IsPositiveInfinity(LimitedRational value) => value.r.IsPositiveInfinity;

    public static bool IsRealNumber(LimitedRational value) => !value.r.IsInfinity && !value.r.IsNaN;

    public static bool IsSubnormal(LimitedRational value) => false;

    public static bool IsZero(LimitedRational value) => value.r.IsZero;

    public static LimitedRational MaxMagnitude(LimitedRational x, LimitedRational y) => new(Rational.Max(x.r, y.r));

    public static LimitedRational MaxMagnitudeNumber(LimitedRational x, LimitedRational y) =>
        x.r.IsNaN ? y : y.r.IsNaN ? y : MaxMagnitude(x, y);

    public static LimitedRational MinMagnitude(LimitedRational x, LimitedRational y) => new(Rational.Min(x.r, y.r));

    public static LimitedRational MinMagnitudeNumber(LimitedRational x, LimitedRational y) =>
        x.r.IsNaN ? y : y.r.IsNaN ? y : MinMagnitude(x, y);

    public static LimitedRational Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public static LimitedRational Parse(string s, NumberStyles style, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public static LimitedRational Parse(ReadOnlySpan<char> s, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public static LimitedRational Parse(string s, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out LimitedRational result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out LimitedRational result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, [MaybeNullWhen(false)] out LimitedRational result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out LimitedRational result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<LimitedRational>.TryConvertFromChecked<TOther>(TOther value, out LimitedRational result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<LimitedRational>.TryConvertFromSaturating<TOther>(TOther value, out LimitedRational result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<LimitedRational>.TryConvertFromTruncating<TOther>(TOther value, out LimitedRational result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<LimitedRational>.TryConvertToChecked<TOther>(LimitedRational value, out TOther result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<LimitedRational>.TryConvertToSaturating<TOther>(LimitedRational value, out TOther result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<LimitedRational>.TryConvertToTruncating<TOther>(LimitedRational value, out TOther result)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(object obj) => obj is LimitedRational other ? CompareTo(other) : 0;

    public int CompareTo(LimitedRational other) => r.CompareTo(other.r);

    public bool Equals(LimitedRational other) => r.Equals(other.r);

    public string ToString(string format, IFormatProvider formatProvider)
    {
        throw new NotImplementedException();
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    private static Rational Normalize(Rational r)
    {
        var np = r.P.GetByteCount();
        var nq = r.Q.GetByteCount();
        var n = Math.Min(np, nq);
        var p = precisionBytes;

        if (np - nq > p)
            return r > Rational.Zero ? Rational.PositiveInfinity : Rational.NegativeInfinity;

        if (nq - np > p)
            return Rational.Zero;

        if (n > p)
        {
            var shift = (n - p) * 8;
            return new(r.P >> shift, r.Q >> shift);
        }

        return r;
    }

    public static LimitedRational operator +(LimitedRational value) => value;

    public static LimitedRational operator +(LimitedRational left, LimitedRational right) =>
        new(left.r + right.r);

    public static LimitedRational operator -(LimitedRational value) => new(-value.r);

    public static LimitedRational operator -(LimitedRational left, LimitedRational right) =>
        new(left.r - right.r);

    public static LimitedRational operator ++(LimitedRational value) =>
        new(value.r + Rational.One);

    public static LimitedRational operator --(LimitedRational value) =>
        new(value.r - Rational.One);

    public static LimitedRational operator *(LimitedRational left, LimitedRational right) =>
        new(left.r * right.r);

    public static LimitedRational operator /(LimitedRational left, LimitedRational right) =>
        new(left.r / right.r);

    public static LimitedRational operator %(LimitedRational left, LimitedRational right)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(LimitedRational left, LimitedRational right) => left.r == right.r;

    public static bool operator !=(LimitedRational left, LimitedRational right) => left.r != right.r;

    public static bool operator <(LimitedRational left, LimitedRational right) => left.r < right.r;

    public static bool operator >(LimitedRational left, LimitedRational right) => left.r > right.r;

    public static bool operator <=(LimitedRational left, LimitedRational right) => left.r <= right.r;

    public static bool operator >=(LimitedRational left, LimitedRational right) => left.r >= right.r;

    public override bool Equals([NotNullWhen(true)] object obj) => r.Equals(obj);

    public override int GetHashCode() => r.GetHashCode();

    public override string ToString() => r.ToString(10, digits);

    private static readonly LimitedRational nan;

    public static LimitedRational NaN => nan;

    public static LimitedRational Exp(LimitedRational r)
    {
        if (IsNaN(r))
            return NaN;

        if (IsNegative(r))
            return One / Exp(-r);

        var z = Truncate(r);
        var f = Frac(r);
        var ez = IntPower(E, z);
        var sum = One;
        var count = Zero;
        var fac = One;
        var power = One;

        while (true)
        {
            power *= f;
            count += One;
            fac *= count;
            var term = power / fac;

            if (term == Zero)
                break;

            sum += term;
        }

        sum *= ez;
        return sum;
    }

    public static LimitedRational Exp10(LimitedRational x) => Exp(x * log10);

    public static LimitedRational Exp2(LimitedRational x) => Exp(x * log2);

    public static BigInteger Truncate(LimitedRational x) => x.Truncate();

    public readonly BigInteger Truncate() => BigInteger.DivRem(r.P, r.Q).Quotient;

    public static LimitedRational Frac(LimitedRational x) => x.Frac();

    public readonly LimitedRational Frac() => this - new LimitedRational(Truncate());

    public static LimitedRational IntPower(LimitedRational x, BigInteger n) =>
        new(Rational.Pow(x.r, checked((int)n)));

    public readonly LimitedRational IntPower(BigInteger n) => throw new NotImplementedException();

    public static LimitedRational Abs(LimitedRational value) => new(Rational.Abs(value.r));

    public static LimitedRational Log(LimitedRational x)
    {
        if (IsNaN(x))
            return NaN;

        if (x <= Zero)
            return NaN;

        if (IsPositiveInfinity(x))
            return x;

        if (x == One)
            return Zero;

        var np = (int)x.r.P.GetBitLength();
        var nq = (int)x.r.Q.GetBitLength();

        if (np > nq)
            x /= new LimitedRational(BigInteger.Pow(2, np - nq));
        else if (nq > np)
            x *= new LimitedRational(BigInteger.Pow(2, nq - np));

        var e = new LimitedRational(np - nq) * log2;
        var y = (x - One) / (x + One);
        var y2 = y * y;
        var num = One;
        var den = One;
        var sum = One;

        while (true)
        {
            num *= y2;
            den += Two;
            var term = num / den;

            if (term == Zero)
                break;

            sum += term;
        }

        sum = Two * y * sum + e;
        return sum;
    }

    public static LimitedRational Log(LimitedRational x, LimitedRational newBase) => Log(x) / Log(newBase);

    public static LimitedRational Log10(LimitedRational x) => Log(x) / log10;

    public static LimitedRational Log2(LimitedRational x) => Log(x) / log2;

    public static implicit operator LimitedRational(double x) => new((Rational)x);

    public static implicit operator LimitedRational(float x) => new((Rational)x);

    public static implicit operator LimitedRational(decimal x) => new((Rational)x);

    public static implicit operator LimitedRational(int x) => new((Rational)x);

    public static implicit operator LimitedRational(long x) => new((Rational)x);

    public static implicit operator LimitedRational(ulong x) => new((Rational)x);

    public static explicit operator LimitedRational(Rational x) => new(x);
}