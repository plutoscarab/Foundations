
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Foundations.Types;

public readonly struct LimitedRational : INumber<LimitedRational>, IExponentialFunctions<LimitedRational>,
    ILogarithmicFunctions<LimitedRational>, IRootFunctions<LimitedRational>, IPowerFunctions<LimitedRational>,
    ITrigonometricFunctions<LimitedRational>, IHyperbolicFunctions<LimitedRational>, 
    IModulusOperators<LimitedRational, LimitedRational, LimitedRational>
{
    static LimitedRational()
    {
        zero = new(0, false);
        one = new(1, false);
        negativeOne = new(-1, false);
        two = new(2, false);
        three = new(3, false);
        nan = new(Rational.NaN, false);
        positiveInfinity = new(Rational.PositiveInfinity, false);
        negativeInfinity = new(Rational.NegativeInfinity, false);
        SetPrecision(235);
    }

    private static int digits;
    private static int precisionBytes;

    private static void SetPrecision(int precisionInDigits)
    {
        var bits = (int)Math.Ceiling((precisionInDigits + 15) * Math.Log2(10));
        precisionBytes = (bits + 7) / 8;
        digits = precisionInDigits;
        e = Compute(ContinuedFraction.E);
        pi = Compute(ContinuedFraction.Pi);
        tau = Compute(n => ContinuedFraction.Pi(n).Transform(0, 2, 1, 0));
        halfPi = Compute(n => ContinuedFraction.Pi(n).Transform(0, 1, 2, 0));
        quarterPi = Compute(n => ContinuedFraction.Pi(n).Transform(0, 1, 4, 0));
        negativeQuarterPi = -quarterPi;
        log2 = Compute(n => ContinuedFraction.Log(2, n));
        log10 = Compute(n => ContinuedFraction.Log(10, n));
        sqrt2Minus1 = Compute(n => ContinuedFraction.Sqrt(2, 2 * n).Transform(-1, 1, 1, 0));
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

    private static readonly LimitedRational zero;
    private static readonly LimitedRational one;
    private static readonly LimitedRational negativeOne;
    private static readonly LimitedRational two;
    private static readonly LimitedRational three;
    private static readonly LimitedRational nan;
    private static readonly LimitedRational positiveInfinity;
    private static readonly LimitedRational negativeInfinity;
    private static LimitedRational e;
    private static LimitedRational pi;
    private static LimitedRational tau;
    private static LimitedRational halfPi;
    private static LimitedRational quarterPi;
    private static LimitedRational negativeQuarterPi;
    private static LimitedRational log2;
    private static LimitedRational log10;
    private static LimitedRational sqrt2Minus1;

    public static LimitedRational Zero => zero;
    public static LimitedRational One => one;
    public static LimitedRational NegativeOne => negativeOne;
    public static LimitedRational Two => two;
    public static LimitedRational Three => three;
    public static LimitedRational PositiveInfinity => positiveInfinity;
    public static LimitedRational NegativeInfinity => negativeInfinity;
    public static LimitedRational NaN => nan;
    public static LimitedRational E => e;
    public static LimitedRational Pi => pi;
    public static LimitedRational Tau => tau;
    public static LimitedRational HalfPi => halfPi;
    public static LimitedRational QuarterPi => quarterPi;
    public static LimitedRational MinusQuarterPi => negativeQuarterPi;
    public static LimitedRational Sqrt2Minus1 => sqrt2Minus1;

    public static int Radix => 2;

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
        var (_, r) = DivRem(left, right);
        return r;
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

    public readonly LimitedRational IntPower(BigInteger n) => IntPower(this, n);

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

    public static LimitedRational Pow(LimitedRational x, LimitedRational y)
    {
        if (IsZero(y))
            return One;

        if (IsZero(x))
            return Zero;

        if (IsInteger(y))
            return IntPower(x, Truncate(y));

        return Exp(y * Log(x));
    }

    public static LimitedRational Cbrt(LimitedRational x) => Exp(Log(x) / Three);

    public static LimitedRational Hypot(LimitedRational x, LimitedRational y) => Sqrt(x * x + y * y);

    public static LimitedRational RootN(LimitedRational x, int n) =>
        n switch
        {
            0 => One,
            1 => x,
            -1 => One / x,
            _ => Exp(Log(x) / n),
        };

    public static LimitedRational Sqrt(LimitedRational x) => Exp(Log(x) / Two);

    public static LimitedRational Acos(LimitedRational x)
    {
        if (Abs(x) > 1)
            return NaN;

        if (Abs(x) == 1)
            return Zero;

        if (x < 0)
            return HalfPi + Asin(x);

        return HalfPi - Asin(x);
    }

    public static LimitedRational AcosPi(LimitedRational x) => Acos(x) / Pi;

    public static LimitedRational Asin(LimitedRational x)
    {
        var a = Abs(x);

        if (a > 1)
            return NaN;

        if (a == 1)
            return Sign(x) * HalfPi;

        return Sign(x) * Atan(a / Sqrt(One - a * a));
    }

    public static LimitedRational AsinPi(LimitedRational x) => Asin(x) / Pi;
    
    public static int Sign(LimitedRational x) => Rational.Sign(x.r);

    public static LimitedRational Atan(LimitedRational x)
    {
        var a = Abs(x);

        if (a > One)
            return Sign(x) * (HalfPi - Atan(One / a));

        if (a == One)
            return x > Zero ? QuarterPi : MinusQuarterPi;

        if (a > Sqrt2Minus1)
            return Sign(x) * (QuarterPi - Atan((One - a) / (One + a)));

        var sum = x;
        var r2 = -x * x;
        var power = x;
        var n = 1;

        while (true)
        {
            n += 2;
            power *= r2;
            var term = power / n;

            if (term == Zero)
                return sum;

            sum += term;
        }
    }

    public static LimitedRational AtanPi(LimitedRational x) => Atan(x) / Pi;

    public static LimitedRational Cos(LimitedRational x)
    {
        x %= Tau;

        if (x > Pi) 
            x -= Tau;

        var sum = One;
        var xx = -x * x;
        int n = 0;
        var term = One;

        while (true)
        {
            term *= xx / ((n + 1) * (n + 2));

            if (term == Zero)
                return sum;

            sum += term;
            n += 2;
        }
    }

    public static LimitedRational CosPi(LimitedRational x) => Cos(x * Pi);

    public static LimitedRational operator /(LimitedRational x, int n) => new(x.r / n);

    public static (BigInteger, LimitedRational) DivRem(LimitedRational x, LimitedRational y)
    {
        var d = Rational.Floor(x.r / y.r);
        var r = x - (LimitedRational)d * y;
        return (d, r);
    }

    public static LimitedRational Sin(LimitedRational x)
    {
        x %= Tau;

        if (x > Pi) 
            x -= Tau;

        var sum = x;
        var xx = -x * x;
        int n = 1;
        var term = x;

        while (true)
        {
            term *= xx / ((n + 1) * (n + 2));

            if (term == Zero)
                return sum;

            sum += term;
            n += 2;
        }
    }

    public static (LimitedRational Sin, LimitedRational Cos) SinCos(LimitedRational x) => (Sin(x), Cos(x));

    public static (LimitedRational SinPi, LimitedRational CosPi) SinCosPi(LimitedRational x) => (SinPi(x), CosPi(x));

    public static LimitedRational SinPi(LimitedRational x) => Sin(x * Pi);

    public static LimitedRational Tan(LimitedRational x)
    {
        var (sin, cos) = SinCos(x);
        return sin / cos;
    }

    public static LimitedRational TanPi(LimitedRational x) => Tan(x * Pi);

    public static LimitedRational Acosh(LimitedRational x)
    {
        if (IsNaN(x))
            return NaN;

        if (IsNegativeInfinity(x))
            return NaN;

        if (IsPositiveInfinity(x))
            return x;

        if (x.r.P.Sign < 0)
            return NaN;

        if (x.r.P < x.r.Q)
            return NaN;

        return Log(x + Sqrt(x * x - One));
    }

    public static LimitedRational Asinh(LimitedRational x)
    {
        if (IsNaN(x))
            return NaN;

        if (IsInfinity(x))
            return x;

        return Log(x + Sqrt(x * x + One));
    }

    public static LimitedRational Atanh(LimitedRational x)
    {
        if (Abs(x) >= One)
            return NaN;

        return Log((One + x) / (One - x)) / Two;
    }

    public static LimitedRational Cosh(LimitedRational x)
    {
        x = Exp(x);

        if (IsInfinity(x))
            return PositiveInfinity;
            
        if (x == Zero)
            return NegativeInfinity;

        x += One / x;
        return x / Two;
    }

    public static LimitedRational Sinh(LimitedRational x)
    {
        x = Exp(x);

        if (IsInfinity(x))
            return PositiveInfinity;

        if (x == Zero)
            return NegativeInfinity;

        x -= One / x;
        return x / Two;
    }

    public static LimitedRational Tanh(LimitedRational x)
    {
        if (IsNaN(x))
            return NaN;

        if (x == Zero)
            return Zero;

        x = Exp(2 * x);

        if (IsInfinity(x))
            return One;

        if (x == Zero)
            return NegativeOne;

        x = (x - One) / (x + One);
        return x;
    }

    public static implicit operator LimitedRational(double x) => new((Rational)x);

    public static implicit operator LimitedRational(float x) => new((Rational)x);

    public static implicit operator LimitedRational(decimal x) => new((Rational)x);

    public static implicit operator LimitedRational(int x) => new((Rational)x);

    public static implicit operator LimitedRational(long x) => new((Rational)x);

    public static implicit operator LimitedRational(ulong x) => new((Rational)x);

    public static explicit operator LimitedRational(Rational x) => new(x);

    public static explicit operator LimitedRational(BigInteger x) => new(x);
}