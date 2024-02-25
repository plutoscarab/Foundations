
using System.Globalization;

namespace Foundations.Types;

/// <summary>
/// A ratio of two integers.
/// </summary>
public readonly struct Rational : IEquatable<Rational>, IComparable<Rational>
{
    /// <summary>
    /// The constant 0.
    /// </summary>
    public static readonly Rational Zero = CreateRaw(0, 1);

    /// <summary>
    /// The constant 1.
    /// </summary>
    public static readonly Rational One = CreateRaw(1, 1);

    /// <summary>
    /// The constant 1/2.
    /// </summary>
    public static readonly Rational OneHalf = CreateRaw(1, 2);

    /// <summary>
    /// The constant 2.
    /// </summary>
    public static readonly Rational Two = CreateRaw(2, 1);

    /// <summary>
    /// Not a rational number.
    /// </summary>
    public static readonly Rational NaN = new Rational();

    /// <summary>
    /// Positive infinity.
    /// </summary>
    public static readonly Rational PositiveInfinity = CreateRaw(1, 0);

    /// <summary>
    /// Negative infinity.
    /// </summary>
    public static readonly Rational NegativeInfinity = CreateRaw(-1, 0);

    /// <summary>
    /// Negative zero.
    /// </summary>
    public static readonly Rational NegativeZero = CreateRaw(0, -1);

    private static readonly Rational decimalMax = (BigInteger)decimal.MaxValue;
    private static readonly Rational decimalMin = (BigInteger)decimal.MinValue;

    readonly BigInteger p;
    readonly BigInteger q;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="p">Numerator.</param>
    /// <param name="q">Denominator.</param>
    public Rational(BigInteger p, BigInteger q)
    {
        var g = BigInteger.GreatestCommonDivisor(p, q);

        if (g.IsZero)
        {
            this.p = 0;
            this.q = 0;
            return;
        }

        if (!g.IsOne)
        {
            p /= g;
            q /= g;
        }

        this.p = p;
        this.q = q;
    }

    private Rational(BigInteger p, BigInteger q, bool check)
    {
        this.p = p;
        this.q = q;
    }

    private static Rational CreateRaw(BigInteger p, BigInteger q)
    {
        return new Rational(p, q, false);
    }

    /// <summary>
    /// Numerator.
    /// </summary>
    public BigInteger P { get { return p; } }

    /// <summary>
    /// Denominator.
    /// </summary>
    public BigInteger Q { get { return q; } }

    /// <summary>
    /// Indicates whether this <see cref="Rational"/> is zero.
    /// </summary>
    public bool IsZero { get { return p.IsZero && !q.IsZero; } }

    /// <summary>
    /// Indicates whether this <see cref="Rational"/> is NaN.
    /// </summary>
    public bool IsNaN { get { return p.IsZero && q.IsZero; } }

    /// <summary>
    /// Indicates whether this <see cref="Rational"/> is positive or negative infinity.
    /// </summary>
    public bool IsInfinity { get { return !p.IsZero && q.IsZero; } }

    /// <summary>
    /// Indicates whether this <see cref="Rational"/> is positive infinity.
    /// </summary>
    public bool IsPositiveInfinity { get { return p.Sign > 0 && q.IsZero; } }

    /// <summary>
    /// Indicates whether this <see cref="Rational"/> is negative infinity.
    /// </summary>
    public bool IsNegativeInfinity { get { return p.Sign < 0 && q.IsZero; } }

    /// <summary>
    /// Indicates whether this <see cref="Rational"/> is negative zero.
    /// </summary>
    public bool IsNegativeZero { get { return p.IsZero && q.Sign < 0; } }

    /// <summary>
    /// Gets a string representation of this <see cref="Rational"/>.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (q.IsZero)
        {
            switch (p.Sign)
            {
                case 0: return "NaN";
                case 1: return "inf";
                case -1: return "-inf";
            }
        }

        if (q.IsOne) return p.ToString();
        if (IsNegativeZero) return "-0";
        return string.Format("{0}/{1}", p, q);
    }

    /// <summary>
    /// Gets a radix-based representation of this <see cref="Rational"/> .
    /// </summary>
    public readonly string ToString(int radix, int places)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(radix, 2, nameof(radix));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(radix, 36, nameof(radix));
        var (p, q) = (P, Q);
        StringBuilder s = new();

        if (p < 0) 
        {
            s.Append('-');
            p = -p;
        }

        var (d, r) = BigInteger.DivRem(p, q);
        var whole = d;
        var ws = d.ToString(radix);

        if (r == 0)
        {
            s.Append(ws);
            return s.ToString();
        }

        List<int> digits = [];
        
        while (p != 0 && places-- >= 0)
        {
            (d, r) = BigInteger.DivRem(r * radix, q);
            digits.Add((int)d);
        }

        if (digits[^1] * 2 >= radix)
        {
            var i = digits.Count - 2;

            while (true)
            {
                if (++digits[i] < radix)
                    break;

                digits[i] = 0;
                --i;

                if (i < 0)
                {
                    whole += whole.Sign;
                    break;
                }
            }
        }

        foreach (var digit in whole.GetDigits(radix))
            s.Append(Constants.Base36Digits[digit]);

        s.Append('.');

        for (var i = 0; i < digits.Count - 1; i++)
            s.Append(Constants.Base36Digits[digits[i]]);

        return s.ToString();
    }

    /// <summary>
    /// Determine whether this <see cref="Rational"/> is equal to another value.
    /// </summary>
    public override readonly bool Equals(object obj)
    {
        if (obj is Rational rational) return Equals(rational);
        return false;
    }

    /// <summary>
    /// Get hash code.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        int h = 1466494808;
        h = HashHelper.Mixer(h + P.GetHashCode());
        h = HashHelper.Mixer(h + Q.GetHashCode());
        return HashHelper.Finisher(h);
    }

    /// <summary>
    /// Indicates whether this <see cref="Rational"/> is equal to another.
    /// </summary>
    public readonly bool Equals(Rational r)
    {
        return this == r;
    }

    /// <summary>
    /// Implicitly casts a <see cref="Int64"/> to a <see cref="Rational"/>.
    /// </summary>
    public static implicit operator Rational(long n)
    {
        return CreateRaw(n, BigInteger.One);
    }

    /// <summary>
    /// Implicitly casts a <see cref="BigInteger"/> to a <see cref="Rational"/>.
    /// </summary>
    /// <param name="p"></param>
    public static implicit operator Rational(BigInteger p)
    {
        return CreateRaw(p, BigInteger.One);
    }

    /// <summary>
    /// Addition operator.
    /// </summary>
    public static Rational operator +(Rational a, Rational b)
    {
        return new Rational(a.p * b.q + a.q * b.p, a.q * b.q);
    }

    /// <summary>
    /// Subtraction operator.
    /// </summary>
    public static Rational operator -(Rational a, Rational b)
    {
        return new Rational(a.p * b.q - a.q * b.p, a.q * b.q);
    }

    /// <summary>
    /// Negation operator.
    /// </summary>
    public static Rational operator -(Rational r)
    {
        return new Rational(-r.p, r.q);
    }

    /// <summary>
    /// Multiplication operator.
    /// </summary>
    public static Rational operator *(Rational a, Rational b)
    {
        return new Rational(a.p * b.p, a.q * b.q);
    }

    /// <summary>
    /// Division operator.
    /// </summary>
    public static Rational operator /(Rational a, Rational b)
    {
        return new Rational(a.p * b.q, a.q * b.p);
    }

    /// <summary>
    /// Equality operator.
    /// </summary>
    public static bool operator ==(Rational a, Rational b)
    {
        return a.p.Equals(b.p) && a.q.Equals(b.q);
    }

    /// <summary>
    /// Inequality operator.
    /// </summary>
    public static bool operator !=(Rational a, Rational b)
    {
        return !a.p.Equals(b.p) || !a.q.Equals(b.q);
    }

    /// <summary>
    /// Less-than operator.
    /// </summary>
    public static bool operator <(Rational a, Rational b)
    {
        return Rational.Compare(a, b) < 0;
    }

    /// <summary>
    /// Less-than-or-equal-to operator.
    /// </summary>
    public static bool operator <=(Rational a, Rational b)
    {
        return Rational.Compare(a, b) <= 0;
    }

    /// <summary>
    /// Greater-than operator.
    /// </summary>
    public static bool operator >(Rational a, Rational b)
    {
        return Rational.Compare(a, b) > 0;
    }

    /// <summary>
    /// Greater-than-or-equal-to operator.
    /// </summary>
    public static bool operator >=(Rational a, Rational b)
    {
        return Rational.Compare(a, b) >= 0;
    }

    /// <summary>
    /// 1 / r.
    /// </summary>
    public static Rational Reciprocal(Rational r)
    {
        return CreateRaw(r.q, r.p);
    }

    /// <summary>
    /// |r|
    /// </summary>
    public static Rational Abs(Rational r)
    {
        return CreateRaw(BigInteger.Abs(r.p), BigInteger.Abs(r.q));
    }

    /// <summary>
    /// Gets the sign of a - b.
    /// </summary>
    public static int Compare(Rational a, Rational b)
    {
        Rational diff = a - b;
        if (diff.IsNaN) throw new InvalidOperationException("Values are not comparable.");
        return Rational.Sign(diff);
    }

    /// <summary>
    /// Gets the sign of a <see cref="Rational"/>.
    /// </summary>
    public static int Sign(Rational r)
    {
        if (r.IsNaN) throw new ArgumentException("NaN has no sign.", nameof(r));
        if (r.IsPositiveInfinity) return 1;
        if (r.IsNegativeInfinity) return -1;
        return r.p.Sign;
    }

    /// <summary>
    /// Gets the lesser of a and b.
    /// </summary>
    public static Rational Min(Rational a, Rational b)
    {
        return a < b ? a : b;
    }

    /// <summary>
    /// Gets the greater of a and b.
    /// </summary>
    public static Rational Max(Rational a, Rational b)
    {
        return a > b ? a : b;
    }

    /// <summary>
    /// Gets a <see cref="Rational"/> raised to an integer power.
    /// </summary>
    public static Rational Pow(Rational r, int n)
    {
        return CreateRaw(BigInteger.Pow(r.p, n), BigInteger.Pow(r.q, n));
    }

    /// <summary>
    /// Implicitly casts a <see cref="System.Single"/> to a <see cref="Rational"/>.
    /// The result is a Rational identical to the original Single when the Rational 
    /// is converted to a Single, but is not necessarily exactly equal to the Single as-is.
    /// </summary>
    public static implicit operator Rational(float f)
    {
        return FromFloatingPoint(f);
    }

    /// <summary>
    /// Implicitly casts a <see cref="System.Double"/> to a <see cref="Rational"/>.
    /// The result is a Rational identical to the original Double when the Rational 
    /// is converted to a Double, but is not necessarily exactly equal to the Double as-is.
    /// </summary>
    public static implicit operator Rational(double d)
    {
        return FromFloatingPoint(d);
    }

    /// <summary>
    /// Implicitly cast a <see cref="System.Decimal"/> to a <see cref="Rational"/>.
    /// </summary>
    public static implicit operator Rational(decimal d)
    {
        Rational r = Zero;

        foreach (var c in ContinuedFraction.Convergents(d))
        {
            r = c;
            if (d == (decimal)c) break;
        }

        return r;
    }

    /// <summary>
    /// Gets the greatest integer less than or equal to a <see cref="Rational"/>.
    /// </summary>
    public static BigInteger Floor(Rational r)
    {
        if (r.q < 0) r = CreateRaw(-r.p, -r.q);
        if (r.q == 1) return r.p;
        if (r.p == 0) return 0;
        BigInteger div = BigInteger.DivRem(BigInteger.Abs(r.p), r.q, out BigInteger rem);
        if (r.p > 0) return div;
        return div - 1;
    }

    /// <summary>
    /// Gets the least integer greater than or equal to a <see cref="Rational"/>.
    /// </summary>
    public static BigInteger Ceiling(Rational r)
    {
        if (r.q < 0) r = CreateRaw(-r.p, -r.q);
        if (r.q == 1) return r.p;
        if (r.p == 0) return 0;
        BigInteger div = BigInteger.DivRem(BigInteger.Abs(r.p), r.q, out BigInteger rem);
        if (r.p < 0) return -div;
        return div + 1;
    }

    /// <summary>
    /// Round toward zero.
    /// </summary>
    public static BigInteger Truncate(Rational r)
    {
        if (r < 0) return Ceiling(r);
        return Floor(r);
    }

    /// <summary>
    /// Gets the fractional portion of a <see cref="Rational"/>, or the value modulo 1.
    /// </summary>
    public static Rational Frac(Rational r)
    {
        return r - Rational.Floor(r);
    }

    /// <summary>
    /// Round a <see cref="Rational"/> to the nearest integer.
    /// </summary>
    public static BigInteger Round(Rational r)
    {
        return Rational.Floor(r + OneHalf);
    }

    /// <summary>
    /// Gets the binary digit representation of the fractional portion of a <see cref="Rational"/>.
    /// </summary>
    public static IEnumerable<int> FractionalDigits(Rational r)
    {
        r = Rational.Frac(r);

        while (true)
        {
            if (r < OneHalf)
            {
                yield return 0;
                r *= 2;
            }
            else
            {
                yield return 1;
                r = r * 2 - 1;
            }

            if (r == 0) break;
        }
    }

    /// <summary>
    /// Compares this <see cref="Rational"/> to another.
    /// </summary>
    public readonly int CompareTo(Rational other)
    {
        return Rational.Compare(this, other);
    }

    /// <summary>
    /// Gets the base b digit representation of this <see cref="Rational"/>.
    /// </summary>
    public readonly IEnumerable<int> BaseExpansion(int b)
    {
        if (this < Zero || this >= One)
            throw new InvalidOperationException("BaseExpansion can only be used for values in range [0, 1).");

        Rational r = this;

        while (true)
        {
            r *= b;
            BigInteger f = Rational.Floor(r);
            r -= f;
            if (r == 0) break;
            yield return (int)f;
        }
    }

    /// <summary>
    /// Explicitly casts a <see cref="Rational"/> to a <see cref="System.Double"/>.
    /// </summary>
    public static explicit operator double(Rational r)
    {
        return (double)r.p / (double)r.q;
    }

    /// <summary>
    /// Explicitly casts a <see cref="Rational"/> to a <see cref="System.Decimal"/>.
    /// </summary>
    /// <param name="r"></param>
    public static explicit operator decimal(Rational r)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(r, decimalMin, nameof(r));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(r, decimalMax, nameof(r));

        var t = Truncate(r);
        r = Abs(r - t);

        decimal d = (decimal)t;
        decimal f = t >= 0 ? 1m : -1m;

        foreach (var digit in r.BaseExpansion(10))
        {
            var g = f / 10m;

            if (g == 0m)
            {
                if (digit < 5)
                    break;

                d += f;
                break;
            }

            f = g;
            d += f * digit;
        }

        return d;
    }

    /// <summary>
    /// Explicitly casts a <see cref="Rational"/> to a <see cref="Foundations.Quad"/>.
    /// </summary>
    public static explicit operator Quad(Rational r)
    {
        var t = Truncate(r);
        r = Abs(r - t);

        Quad d = (Quad)t;
        Quad f = t >= 0 ? 1 : -1;

        foreach (var digit in r.BaseExpansion(10))
        {
            var g = f / 10m;

            if (g == 0m)
            {
                if (digit < 5)
                    break;

                d += f;
                break;
            }

            f = g;
            d += f * digit;
        }

        return d;
    }

    /// <summary>
    /// Increment operator.
    /// </summary>
    public static Rational operator ++(Rational r)
    {
        return new Rational(r.p + r.q, r.q);
    }

    /// <summary>
    /// Decrement operator.
    /// </summary>
    public static Rational operator --(Rational r)
    {
        return new Rational(r.p - r.q, r.q);
    }

    /// <summary>
    /// Create a rational approximation of a decimal number.
    /// </summary>
    /// <result>
    /// The result
    /// is the <see cref="Rational"/> with the smallest possible denominator
    /// that equals the specified number when rounded to the same number of
    /// significant digits. For example Approximate(0.1m) returns 1/7, and
    /// Approximate(0.10m) returns 1/10.
    /// </result>
    public static Rational Approximate(decimal dec)
    {
        return Approximate(dec.ToString());
    }

    /// <summary>
    /// Create a rational approximation of a decimal number.
    /// </summary>
    /// <result>
    /// The result
    /// is the <see cref="Rational"/> with the smallest possible denominator
    /// that equals the specified number when rounded to the same number of
    /// significant digits. For example Approximate("0.1") returns 1/7 because
    /// 1/7 is between 0.05 and 0.15, whereas Approximate("0.10") returns 1/10.
    /// </result>
    public static Rational Approximate(string dec)
    {
        if (string.IsNullOrWhiteSpace(dec))
            throw new ArgumentException("String is null, empty, or whitespace.", nameof(dec));

        if (BigInteger.TryParse(dec, out BigInteger p))
        {
            return p;
        }

        int i = dec.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        if (i == -1)
            throw new ArgumentException("String does not contain a decimal point (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) but is not a decimal integer.", nameof(dec));

        dec = string.Concat(dec.AsSpan(0, i), dec.AsSpan(i + 1));
        i = dec.Length - i + 1;

        if (!BigInteger.TryParse(dec, out p))
            throw new ArgumentException("String is not a decimal integer or a decimal number containing a decimal point.", nameof(dec));

        var q = BigInteger.Pow(10, i);
        bool negative = p < BigInteger.Zero;
        if (negative) p = -p;
        p *= 10;

        Rational
            val = new(p, q),
            min = new(p - 5, q),
            max = new(p + 5, q),
            lo = Zero,
            hi = PositiveInfinity;

        while (true)
        {
            Rational med = new Rational(lo.p + hi.p, lo.q + hi.q);

            if (med >= min && med < max)
                return negative ? -med : med;

            if (med < val)
            {
                var n = Abs((min * lo.q - lo.p) / (min * hi.q - hi.p));
                BigInteger k = n.p / n.q;
                lo = new Rational(lo.p + k * hi.p, lo.q + k * hi.q);
            }
            else
            {
                var n = Abs((max * hi.q - hi.p) / (max * lo.q - lo.p));
                BigInteger k = n.p / n.q;
                hi = new Rational(hi.p + k * lo.p, hi.q + k * lo.q);
            }
        }
    }

    /// <summary>
    /// Returns the next rational number in the Moshe Newman enumeration of the Calkin Wilf tree.
    /// (Eventually lists all positive rational numbers if you start with zero.)
    /// </summary>
    public readonly Rational Next()
    {
        return Next(this);
    }

    /// <summary>
    /// Returns the next rational number in the Calkin Wilf tree.
    /// (Eventually lists all positive rational numbers if you start with zero.)
    /// </summary>
    /// <remarks>Uses the Moshe Newman recurrence relation.</remarks>
    public static Rational Next(Rational x)
    {
        return Reciprocal(Floor(x) * Two + One - x);
    }

    public static Rational Best(double d) => Best(FromFloatingPoint(d));

    public static Rational Best(float f) => Best(FromFloatingPoint(f));

    private static Rational Best(Rational r) => Best(new(r.P * 2 - 1, r.Q * 2), new Rational(r.P * 2 + 1, r.Q * 2));

    /// <summary>
    /// Gets the rational value with the smallest denominator that lies between a given min and max value.
    /// </summary>
    public static Rational Best(Rational lo, Rational hi)
    {
        var clo = ((ContinuedFraction)lo).ToList();
        var chi = ((ContinuedFraction)hi).ToList();
        var matching = clo.Zip(chi).TakeWhile(_ => _.First == _.Second).Count();
        var cf = clo.Take(matching).ToList();
        var min = BigInteger.Min(clo[matching], chi[matching]);
        cf.Add(min + 1);
        new ContinuedFraction(cf).ToRatio(out var r);
        return r;
    }

    public static (long Mantissa, short Exponent) DecomposeFloatingPoint(float d)
    {
        if (float.IsNaN(d) || float.IsInfinity(d))
            throw new ArgumentException(nameof(d));

        if (d == 0.0f)
            return (0, 0);

        int n = BitConverter.SingleToInt32Bits(d);
        bool negative = n < 0;
        int exp = (int)(n >> 23) & 0xFF;
        int mantissa = n & 0x7FFFFF;

        if (exp == 0)
        {
            exp = -126;
        }
        else
        {
            mantissa |= 0x800000;
            exp -= 127;
        }

        exp -= 23;
        int numerator = mantissa;
        if (negative) numerator *= -1;
        return (numerator, (short)exp);
    }

    public static Rational FromFloatingPoint(float d)
    {
        if (float.IsNaN(d))
            return NaN;

        if (float.IsPositiveInfinity(d))
            return PositiveInfinity;

        if (float.IsNegativeInfinity(d))
            return NegativeInfinity;

        if (d == 0.0f)
            return Zero;

        int n = BitConverter.SingleToInt32Bits(d);
        bool negative = n < 0;
        int exp = (int)(n >> 23) & 0xFF;
        int mantissa = n & 0x7FFFFF;

        if (exp == 0)
        {
            exp = -126;
        }
        else
        {
            mantissa |= 0x800000;
            exp -= 127;
        }

        exp -= 23;
        int numerator = mantissa;
        if (negative) numerator *= -1;

        if (exp >= 0)
            return new(numerator * BigInteger.Pow(2, exp), 1, false);

        return new(numerator, BigInteger.Pow(2, -exp), false);
    }

    public static (long Mantissa, short Exponent) DecomposeFloatingPoint(double d)
    {
        if (double.IsNaN(d) || double.IsInfinity(d))
            throw new ArgumentException(nameof(d));

        if (d == 0.0)
            return (0, 0);

        long n = BitConverter.DoubleToInt64Bits(d);
        bool negative = n < 0;
        int exp = (int)(n >> 52) & 0x7FF;
        long mantissa = n & 0xFFFFFFFFFFFFF;

        if (exp == 0)
        {
            exp = -1022;
        }
        else
        {
            mantissa |= 0x10000000000000;
            exp -= 1023;
        }

        exp -= 52;
        long numerator = mantissa;
        if (negative) numerator *= -1;
        return (numerator, (short)exp);
    }

    public static Rational FromFloatingPoint(double d)
    {
        if (double.IsNaN(d))
            return NaN;

        if (double.IsPositiveInfinity(d))
            return PositiveInfinity;

        if (double.IsNegativeInfinity(d))
            return NegativeInfinity;

        if (d == 0.0)
            return Zero;

        long n = BitConverter.DoubleToInt64Bits(d);
        bool negative = n < 0;
        int exp = (int)(n >> 52) & 0x7FF;
        long mantissa = n & 0xFFFFFFFFFFFFF;

        if (exp == 0)
        {
            exp = -1022;
        }
        else
        {
            mantissa |= 0x10000000000000;
            exp -= 1023;
        }

        exp -= 52;
        long numerator = mantissa;
        if (negative) numerator *= -1;

        if (exp >= 0)
            return new(numerator * BigInteger.Pow(2, exp), 1, false);

        return new(numerator, BigInteger.Pow(2, -exp), false);
    }
}
