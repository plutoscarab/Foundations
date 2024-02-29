
using System.Runtime.InteropServices;

namespace Foundations;

[StructLayout(LayoutKind.Explicit)]
internal struct DoubleBytes
{
    [FieldOffset(0)]
    public double Double;
    [FieldOffset(0)]
    public ulong Bytes;
    [FieldOffset(0)]
    public byte Byte0;
    [FieldOffset(1)]
    public byte Byte1;
    [FieldOffset(2)]
    public byte Byte2;
    [FieldOffset(3)]
    public byte Byte3;
    [FieldOffset(4)]
    public byte Byte4;
    [FieldOffset(5)]
    public byte Byte5;
    [FieldOffset(6)]
    public byte Byte6;
    [FieldOffset(7)]
    public byte Byte7;
    [FieldOffset(0)]
    public ushort Word0;
    [FieldOffset(2)]
    public ushort Word1;
    [FieldOffset(4)]
    public ushort Word2;
    [FieldOffset(6)]
    public ushort Word3;

    public DoubleBytes(double d)
    {
        Bytes = Word0 = Word1 = Word2 = Word3 = Byte0 = Byte1 = Byte2 = Byte3 = Byte4 = Byte5 = Byte6 = Byte7 = 0;
        Double = d;
    }

    public DoubleBytes(byte[] bytes)
    {
        Double = Bytes = Word0 = Word1 = Word2 = Word3 = 0;
        Byte0 = bytes[0]; Byte1 = bytes[1]; Byte2 = bytes[2]; Byte3 = bytes[3];
        Byte4 = bytes[4]; Byte5 = bytes[5]; Byte6 = bytes[6]; Byte7 = bytes[7];
    }
}

/// <summary>
/// Quadruple-precision floating-point number. 144 bits of precision with one sign
/// bit, 16 exponent bits, and 127 mantissa bits.
/// </summary>
public struct Quad : IEquatable<Quad>, IComparable<Quad>, IAdditionOperators<Quad, Quad, Quad>, 
    ISubtractionOperators<Quad, Quad, Quad>, IUnaryNegationOperators<Quad, Quad>, IMultiplyOperators<Quad, Quad, Quad>, 
    IDivisionOperators<Quad, Quad, Quad>, IAdditiveIdentity<Quad, Quad>, IEqualityOperators<Quad, Quad, bool>
{
    const ulong msb = 0x8000000000000000;

    bool negative;
    short exponent;
    Mantissa mantissa;
    int signif; // number of significant bits

    /// <summary>
    /// Represents a value that is not a number (NaN).
    /// </summary>
    public static readonly Quad NaN = new(double.NaN);

    /// <summary>
    /// Represents positive infinity.
    /// </summary>
    public static readonly Quad PositiveInfinity = new(double.PositiveInfinity);

    /// <summary>
    /// Represents negative infinity.
    /// </summary>
    public static readonly Quad NegativeInfinity = new(double.NegativeInfinity);

    /// <summary>
    /// Represents the smallest possible Real greater than zero.
    /// </summary>
    public static readonly Quad Epsilon = new(false, short.MinValue, msb, 0);

    /// <summary>
    /// Represents the most positive valid value of Real.
    /// </summary>
    public static readonly Quad MaxValue = new(false, short.MaxValue - 1, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF);

    /// <summary>
    /// Represents the most negative valid value of Real.
    /// </summary>
    public static readonly Quad MinValue = new(true, short.MaxValue - 1, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF);

    /// <summary>
    /// Represents zero.
    /// </summary>
    public static readonly Quad Zero = new(false, short.MinValue, 0, 0);

    /// <summary>
    /// Represents one.
    /// </summary>
    public static readonly Quad One = new(1);
    public static readonly Quad NegativeOne = new(-1);

    /// <summary>
    /// Represents two.
    /// </summary>
    public static readonly Quad Two = new(2);
    public static readonly Quad OneHalf = new(0.5);

    /// <summary>
    /// Represents the natural logarithmic base.
    /// </summary>
    public static readonly Quad E = new(0x4000, 0xadf8, 0x5458, 0xa2bb, 0x4a9a, 0xafdc, 0x5620, 0x273d, 0x3cf1);

    /// <summary>
    /// Represents the ratio of a circle's circumference to its diameter in Euclidean geometry.
    /// </summary>
    public static readonly Quad OneOverLn10 = new(false, -1, 0xde5bd8a937287195, 0x355baaafad33dc6e);

    public static readonly Quad Omega = new(false, 0, 0x91304d7c74b2ba5e, 0xafddaa6286dc28e2);

    public Quad() : this(0)
    {
    }

    private Quad(ushort Word8, ushort Word7, ushort Word6, ushort Word5, ushort Word4,
        ushort Word3, ushort Word2, ushort Word1, ushort Word0)
    {
        negative = Word8 > short.MaxValue;
        exponent = (short)((Word8 & 0x7FFF) - 16382);
        ulong mantHi = (ulong)((ulong)Word4 | ((ulong)Word5 << 16) | ((ulong)Word6 << 32) | ((ulong)Word7 << 48));
        ulong mantLo = (ulong)((ulong)Word0 | ((ulong)Word1 << 16) | ((ulong)Word2 << 32) | ((ulong)Word3 << 48));
        mantissa = new Mantissa(mantHi, mantLo);
        signif = 128;
    }

    public Quad(bool negative, short exponent, ulong mantHi, ulong mantLo)
    {
        this.negative = negative;
        this.exponent = exponent;
        mantissa = new Mantissa(mantHi, mantLo);

        if (mantissa != Mantissa.Zero)
            this.exponent -= (short)this.mantissa.Normalize();
            
        signif = 128;
    }

    public void Deconstruct(out bool negative, out short exponent, out ulong mantHi, out ulong mantLo)
    {
        negative = this.negative;
        exponent = this.exponent;
        mantHi = mantissa.Hi;
        mantLo = mantissa.Lo;
    }

    public Quad(long i)
    {
        signif = 128;

        if (i == 0)
        {
            negative = false;
            exponent = short.MinValue;
            mantissa = 0;
            return;
        }

        negative = i < 0;
        exponent = 64;

        if (i > long.MinValue)
            mantissa = new Mantissa((ulong)Math.Abs(i), 0);
        else
            mantissa = new Mantissa((ulong)long.MaxValue + 1UL, 0);

        exponent -= (short)mantissa.Normalize();
    }

    public static implicit operator Quad(long i)
    {
        return new Quad(i);
    }

    public Quad(ulong u)
    {
        negative = false;
        signif = 128;

        if (u == 0)
        {
            exponent = short.MinValue;
            mantissa = 0;
            return;
        }

        exponent = 64;
        mantissa = new Mantissa(u, 0);
        exponent -= (short)mantissa.Normalize();
    }

    public Quad(double d)
    {
        signif = 128;

        if (double.IsNaN(d))
        {
            negative = false;
            exponent = short.MaxValue;
            mantissa = Mantissa.MaxValue;
        }
        else if (double.IsPositiveInfinity(d))
        {
            negative = false;
            exponent = short.MaxValue;
            mantissa = new Mantissa(msb, 0);
        }
        else if (double.IsNegativeInfinity(d))
        {
            negative = true;
            exponent = short.MaxValue;
            mantissa = new Mantissa(msb, 0);
        }
        else if (d == 0)
        {
            negative = false;
            exponent = short.MinValue;
            mantissa = 0;
        }
        else
        {
            DoubleBytes db = new(d);
            negative = d < 0;
            exponent = (short)(((db.Word3 >> 4) & 0x7FF) - 0x3FE);
            ulong mantHi;

            if (exponent > -0x3FE)
                mantHi = (db.Bytes << 11) | msb;
            else
            {
                mantHi = db.Bytes << 12;
                if (mantHi != 0)
                {
                    while ((mantHi & msb) == 0)
                    {
                        mantHi <<= 1;
                        exponent--;
                    }
                }
            }

            mantissa = new Mantissa(mantHi, 0);
        }
    }

    public Quad(decimal d) : this(d.ToString())
    {
    }

    public Quad(BigInteger b) : this(b.ToString())
    {
    }

    public Quad(string s)
        : this(s, 10)
    {
    }

    public Quad(string s, int numberBase)
    {
        const string digits = "0123456789ABCDEF";

        ArgumentNullException.ThrowIfNull(s, nameof(s));
        ArgumentOutOfRangeException.ThrowIfEqual(s.Length, 0, nameof(s.Length));
        ArgumentOutOfRangeException.ThrowIfLessThan(numberBase, 2, nameof(numberBase));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(numberBase, 16, nameof(numberBase));

        int b = numberBase;
        Quad n = 0, f = 1;
        int i = 0;

        if (s[i] == '-')
        {
            i++;
        }

        int D = -1, E = -1;
        int exp = 0;
        
        for (; i < s.Length; i++)
        {
            if (s[i] == '^')
            {
                if (E != -1) 
                    throw new FormatException();

                E = i;
                i++;

                if (i >= s.Length) 
                    throw new FormatException();

                if (s[i] != '+' && s[i] != '-') 
                    throw new FormatException();
            }
            else if (s[i] == '.')
            {
                if (D != -1 || E != -1) 
                    throw new FormatException();

                D = i;
            }
            else
            {
                int d = digits.IndexOf(s[i]);

                if (d == -1 || d >= b) 
                    throw new FormatException();

                if (E != -1)
                {
                    exp = exp * b + d;
                }
                else if (D != -1)
                {
                    f /= b;
                    n += f * d;
                }
                else
                {
                    n = n * b + d;
                }
            }
        }

        if (E != -1)
        {
            if (s[E + 1] == '-')
                exp *= -1;
            n *= Quad.Pow(b, exp);
        }

        if (s[0] == '-')
            n *= -1;

        negative = n.negative;
        exponent = n.exponent;
        mantissa = n.mantissa;
        signif = 128;
    }

    public static implicit operator Quad(ulong u) => new(u);

    public static implicit operator Quad(int i) => new((long)i);

    public static explicit operator Quad(string s) => new(s);

    public static implicit operator Quad(double d) => new(d);

    public static implicit operator Quad(decimal d) => new(d);

    public static implicit operator Quad(BigInteger b) => new(b);

    public static explicit operator int(Quad r)
    {
        r = Truncate(r);
        ArgumentOutOfRangeException.ThrowIfLessThan(r, int.MinValue, nameof(r));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(r, int.MaxValue, nameof(r));
        return (int)(r.mantissa >> (128 - r.exponent)).Lo * (r.negative ? -1 : 1);
    }

    public static bool IsNaN(Quad r) => r.exponent == short.MaxValue && !IsInfinity(r);

    public static bool IsInfinity(Quad r) => IsPositiveInfinity(r) || IsNegativeInfinity(r);

    public static bool IsPositiveInfinity(Quad r) => r == PositiveInfinity;

    public static bool IsNegativeInfinity(Quad r) => r == NegativeInfinity;

    public static bool IsZero(Quad r) => r.exponent == short.MinValue && r.mantissa == Mantissa.Zero;

    public readonly bool IsZero() => IsZero(this);

    public static bool IsNegative(Quad r) => r.negative;

    public readonly bool IsNegative() => IsNegative(this);

    public static bool operator ==(Quad a, Quad b)
    {
        if (IsZero(a) && IsZero(b)) return true;    // +0 == -0
        return a.negative == b.negative && a.exponent == b.exponent && a.mantissa == b.mantissa;
    }

    public static bool operator !=(Quad a, Quad b)
    {
        return !(a == b);
    }

    public readonly override bool Equals(object o) => o is Quad q && this == q;

    public readonly override int GetHashCode() => HashCode.Combine(mantissa, exponent, negative);

    public readonly override string ToString() => ToString(10);

    public static Quad Parse(string s) => new(s);

    public static bool TryParse(string s, out Quad q)
    {
        try
        {
            q = Parse(s);
            return true;
        }
        catch
        {
            q = default;
            return false;
        }
    }

    public readonly string ToString(int radix)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(radix, 2, nameof(radix));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(radix, 36, nameof(radix));

        if (IsNaN(this))
            return double.NaN.ToString();

        if (IsNegativeInfinity(this))
            return double.NegativeInfinity.ToString();

        if (IsPositiveInfinity(this))
            return double.PositiveInfinity.ToString();

        if (IsZero(this))
            return "0";

        if (exponent == 0 && mantissa == Mantissa.Zero &negative == false && signif == 0)
            return "uninitialized";

        Quad nb = radix;
        StringBuilder s = new(45);

        if (negative)
            s.Append('-');

        Quad abs = Abs(this);
        int exp;

        if (this == Zero)
            exp = 0;
        else
            exp = (int)Ceiling(Log(abs, nb)) - 1;

        Quad div = IntPower(nb, exp);
        Quad mant = abs / div;

        if ((int)mant >= radix)
        {
            exp++;
            mant /= nb;
        }

        int dec = 1;

        if (exp >= 0 && exp <= 20)
        {
            dec += exp;
            exp = 0;
        }

        int trailz = 0;
        bool emitdec = false;
        const int len = 38;
        var digits = new int[len];

        for (var i = 0; i < len; i++)
        {
            int digit = (int)mant;
            digits[i] = digit;
            mant = nb * Frac(mant);
        }

        if (digits[^1] * 2 >= radix)
        {
            for (var i = len - 2; i >= 0; i--)
            {
                if (++digits[i] < radix)
                    break;

                digits[i] = 0;

                if (i == 0)
                {
                    Array.Resize(ref digits, len + 1);
                    Array.Copy(digits, 0, digits, 1, len);
                    digits[0] = 1;
                    dec++;
                }
            }
        }

        for (var i = 0; i < len - 1; i++)
        {
            var digit = digits[i];

            if (digit == 0 && dec <= 0)
            {
                trailz++;
            }
            else
            {
                if (emitdec)
                {
                    s.Append('.');
                    emitdec = false;
                }

                while (trailz > 0)
                {
                    s.Append('0');
                    trailz--;
                }

                s.Append("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[digit]);

                if (--dec == 0)
                    emitdec = true;
            }
        }

        if (exp != 0)
        {
            s.Append('^');
            if (exp > 0) s.Append('+'); else s.Append('-');

            foreach (var d in exp.GetDigits(radix))
                s.Append(Constants.Base36Digits[d]);
        }

        return s.ToString();
    }

    public static implicit operator ulong(Quad r)
    {
        r = r.Floor();
        ArgumentOutOfRangeException.ThrowIfLessThan(r, (Quad)ulong.MinValue, nameof(r));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(r, (Quad)ulong.MaxValue, nameof(r));

        return r.mantissa.Hi >> (64 - r.exponent);
    }

    public static implicit operator double(Quad r)
    {
        if (IsNaN(r))
            return double.NaN;

        if (r.exponent < -0x3FE - 56)
            return 0.0;

        if (r.exponent > 0x400)
            return r.negative ? double.NegativeInfinity : double.PositiveInfinity;

        while (r.exponent < -0x3FE)
        {
            r.exponent++;
            r.mantissa.ShiftRight();
        }

        DoubleBytes db = new();

        if (r.exponent == -0x3FE)
        {
            db.Word3 = (ushort)((r.negative ? 0x8000 : 0) | ((r.exponent + 0x3FE) << 4) | ((byte)(r.mantissa.Hi >> 60) & 0xF));
            db.Word2 = (ushort)(r.mantissa.Hi >> 44);
            db.Word1 = (ushort)(r.mantissa.Hi >> 28);
            db.Word0 = (ushort)(r.mantissa.Hi >> 12);
        }
        else
        {
            db.Word3 = (ushort)((r.negative ? 0x8000 : 0) | ((r.exponent + 0x3FE) << 4) | ((byte)(r.mantissa.Hi >> 59) & 0xF));
            db.Word2 = (ushort)(r.mantissa.Hi >> 43);
            db.Word1 = (ushort)(r.mantissa.Hi >> 27);
            db.Word0 = (ushort)(r.mantissa.Hi >> 11);
        }

        return db.Double;
    }

    public static Quad operator -(Quad r)
    {
        if (IsNaN(r))
            return NaN;

        Quad temp = r;
        temp.negative = !temp.negative;
        return temp;
    }

    public static Quad operator +(Quad a, Quad b)
    {
        if (a.negative != b.negative)
        {
            b.negative = !b.negative;
            return a - b;
        }

        if (IsNaN(a) || IsNaN(b))
            return NaN;

        Quad r = a;

        if (b.exponent > a.exponent)
        {
            a = b; b = r; r = a;
        }

        r.signif = Math.Min(a.signif, b.signif);
        int shift = a.exponent - b.exponent;

        if (shift > 127)
            return a;

        if (a.mantissa.IsZero())
            return Zero;

        b.mantissa.ShiftRight(shift);

        r.mantissa = a.mantissa + b.mantissa;

        if (r.mantissa.Overflow)
        {
            r.mantissa.ShiftRightWithMsb();
            if (r.exponent == short.MaxValue)
                return r.negative ? NegativeInfinity : PositiveInfinity;
            r.exponent++;
        }

        return r;
    }

    private void Normalize()
    {
        if (mantissa == Mantissa.Zero)
        {
            negative = false;
            exponent = short.MinValue;
            mantissa = Mantissa.Zero;
            return;
        }

        int exp = exponent - mantissa.Normalize();

        if (exp < short.MinValue)
        {
            negative = false;
            exponent = short.MinValue;
            mantissa = Mantissa.Zero;
            return;
        }

        exponent = (short)exp;
    }

    public static Quad operator -(Quad a, Quad b)
    {
        if (a.negative != b.negative)
            return a + (-b);

        if (IsNaN(a) || IsNaN(b))
            return NaN;

        if (a == b)
            return Zero;

        bool negate = false;
        Quad r = a;

        if (b.exponent > a.exponent)
        {
            a = b; b = r; r = a;
            negate = true;
        }

        int shift = a.exponent - b.exponent;
        
        if (shift > 127)
            return negate ? -a : a;

        if (shift == 0 && b.mantissa > a.mantissa)
        {
            a = b; b = r; r = a;
            negate = true;
        }

        r.signif = Math.Min(a.signif, b.signif);

        if (a.mantissa.IsZero())
            return Zero;

        b.mantissa.ShiftRight(shift);

        r.mantissa = a.mantissa - b.mantissa;
        r.Normalize();

        if (negate)
            r.negative = !r.negative;

        return r;
    }

    public static Quad operator *(Quad a, Quad b)
    {
        if (IsNaN(a) || IsNaN(b))
            return NaN;

        if (a == Zero || b == Zero)
            return Zero;

        if (IsInfinity(a) || IsInfinity(b))
            return a.negative == b.negative ? PositiveInfinity : NegativeInfinity;

        Quad r;
        r.signif = Math.Min(a.signif, b.signif);
        r.negative = a.negative != b.negative;
        int exponent = a.exponent + b.exponent;
        r.mantissa = Mantissa.Multiply(a.mantissa.Hi, b.mantissa.Hi);
        r.mantissa += Mantissa.Multiply(a.mantissa.Lo, b.mantissa.Hi) >> 64;
        bool carry = r.mantissa.Overflow;
        r.mantissa += Mantissa.Multiply(a.mantissa.Hi, b.mantissa.Lo) >> 64;
        carry |= r.mantissa.Overflow;

        if (carry)
        {
            exponent++;
            r.mantissa.ShiftRightWithMsb();
        }

        exponent -= (short)r.mantissa.Normalize();

        if (exponent >= short.MaxValue)
            return r.negative ? NegativeInfinity : PositiveInfinity;

        if (exponent <= short.MinValue)
            return Zero;

        r.exponent = (short)exponent;
        return r;
    }

    private static void Multiply128(ulong x, ulong y, out ulong hi, out ulong lo)
    {
        if (x == 0 || y == 0)
        {
            hi = lo = 0;
            return;
        }

        uint a = (uint)(x >> 32);
        uint b = (uint)x;
        uint c = (uint)(y >> 32);
        uint d = (uint)y;
        ulong l = (ulong)b * d;
        ulong m1 = (ulong)a * d;
        ulong m2 = (ulong)b * c;
        ulong m = m1 + m2;
        ulong h = (ulong)a * c;
        lo = l + (m << 32);
        hi = h + (m >> 32);

        if (lo < l) 
            hi++;

        if (m < m1 || m < m2) 
            hi += 0x100000000;
    }

    public static Quad operator /(Quad a, Quad b)
    {
        if (IsNaN(a) || IsNaN(b))
            return NaN;

        if (b == Zero)
            return NaN;

        if (IsInfinity(a) && IsInfinity(b))
            return NaN;

        if (IsInfinity(b))
            return Zero;

        if (IsInfinity(a))
            return a.negative == b.negative ? a : -a;

        if (a == Zero)
            return Zero;

        Quad r;
        r.signif = Math.Min(a.signif, b.signif);
        r.negative = a.negative != b.negative;
        int exponent = a.exponent - b.exponent + 1;
        r.mantissa = Mantissa.Zero;
        exponent -= a.mantissa.Normalize();
        exponent -= b.mantissa.Normalize();
        Mantissa mask = new(msb, 0);

        for (int i = 0; i < 128; i++)
        {
            if (a.mantissa >= b.mantissa)
            {
                r.mantissa |= mask;
                a.mantissa -= b.mantissa;
            }
            mask.ShiftRight();
            b.mantissa.ShiftRight();
        }

        exponent -= (short)r.mantissa.Normalize();

        if (exponent >= short.MaxValue)
            return r.negative ? NegativeInfinity : PositiveInfinity;

        if (exponent <= short.MinValue)
            return Zero;

        r.exponent = (short)exponent;
        return r;
    }

    public static bool operator <(Quad a, Quad b)
    {
        if (IsNaN(a) || IsNaN(b))
            return false;

        if (a == b)
            return false;

        if (a == Zero)
            return !b.negative;

        if (b == Zero)
            return a.negative;

        if (a.negative != b.negative)
            return a.negative;

        if (a.exponent > b.exponent)
            return a.negative;

        if (a.exponent < b.exponent)
            return !a.negative;

        return (a.mantissa > b.mantissa) == a.negative;
    }

    public static bool operator >(Quad b, Quad a)
    {
        if (IsNaN(a) || IsNaN(b))
            return false;

        if (a == b)
            return false;

        if (a == Zero)
            return !b.negative;

        if (b == Zero)
            return a.negative;

        if (a.negative != b.negative)
            return a.negative;

        if (a.exponent > b.exponent)
            return a.negative;

        if (a.exponent < b.exponent)
            return !a.negative;

        return (a.mantissa > b.mantissa) == a.negative;
    }

    public static bool operator >=(Quad a, Quad b)
    {
        return !(a < b);
    }

    public static bool operator <=(Quad a, Quad b)
    {
        return !(a > b);
    }

    private void Signif(int n)
    {
        signif = Math.Min(n, signif);
    }

    public int SignificantBits
    {
        readonly get
        {
            return signif;
        }
        set
        {
            Signif(Math.Max(0, value));
        }
    }

    public static Quad AdditiveIdentity => Zero;

    public static Quad Sqrt(Quad r)
    {
        if (r.negative)
            return NaN;

        if (IsNaN(r))
            return NaN;

        if (IsInfinity(r))
            return r;

        if (IsZero(r))
            return Zero;

        short exponent = r.exponent;
        r.exponent = 0;
        Quad x = r;

        while (true)
        {
            Quad y = x;
            x += r / x;
            x.exponent--;

            if (x.exponent == y.exponent && x.mantissa.Hi == y.mantissa.Hi)
            {
                long diff = (long)(x.mantissa.Lo - y.mantissa.Lo);

                if (diff != long.MinValue && Math.Abs(diff) < 5)
                    break;
            }
        }

        x.exponent = (short)(exponent >> 1);

        if ((exponent & 1) != 0)
        {
            x *= QuadConstants.Sqrt2;
        }

        x.Signif(122);
        return x;
    }

    public readonly Quad Sqrt() => Sqrt(this);

    public static Quad Abs(Quad r)
    {
        if (IsNaN(r))
            return NaN;

        r.negative = false;
        return r;
    }

    public readonly Quad Abs() => Abs(this);

    public static Quad Truncate(Quad r)
    {
        if (IsNaN(r) || IsInfinity(r))
            return r;

        if (r.exponent <= 0)
            return Zero;

        if (r.exponent >= 128)
            return r;

        Mantissa mask = (Mantissa.One << (128 - r.exponent)) - Mantissa.One;
        r.mantissa &= ~mask;
        return r;
    }

    public readonly Quad Truncate() => Truncate(this);

    public static Quad Ceiling(Quad r)
    {
        if (IsNaN(r) || IsInfinity(r))
            return r;

        if (r.negative)
            return Truncate(r);

        if (r.exponent <= 0)
            return r > Zero ? One : Zero;

        if (r.exponent >= 128)
            return r;

        Mantissa mask = (Mantissa.One << (128 - r.exponent)) - Mantissa.One;
        bool frac = (r.mantissa & mask) != 0;
        r.mantissa &= ~mask;
        return frac ? r + One : r;
    }

    public readonly Quad Ceiling() => Ceiling(this);

    public static Quad Floor(Quad r)
    {
        if (IsNaN(r) || IsInfinity(r))
            return r;

        if (!r.negative)
            return Truncate(r);

        if (r.exponent <= 0)
            return r > Zero ? Zero : NegativeOne;

        if (r.exponent >= 128)
            return r;

        Mantissa mask = (Mantissa.One << (128 - r.exponent)) - Mantissa.One;
        bool frac = (r.mantissa & mask) != 0;
        r.mantissa &= ~mask;
        return frac ? r - One : r;
    }

    public readonly Quad Floor() => Floor(this);

    public static Quad Frac(Quad r) => r - Truncate(r);

    public readonly Quad Frac() => Frac(this);

    public static bool IsInteger(Quad r) => r == Truncate(r);

    public readonly bool IsInteger() => IsInteger(this);

    public static bool IsOdd(Quad r)
    {
        if (!IsInteger(r))
            return false;

        if (r.exponent <= 0)
            return true;

        if (r.exponent >= 128)
            return false;

        return (r.mantissa & (Mantissa.One << (128 - r.exponent))) != Mantissa.Zero;
    }

    public readonly bool IsOdd() => IsOdd(this);

    public static Quad Round(Quad r)
    {
        Quad f = Frac(r);
        r = Floor(r + OneHalf);

        if (Abs(f) == OneHalf && IsOdd(r))
                r -= One;

        return r;
    }

    public readonly Quad Round() => Round(this);

    public static Quad Min(Quad a, Quad b)
    {
        return a < b ? a : b;
    }

    public static Quad Max(Quad a, Quad b)
    {
        return a > b ? a : b;
    }

    public static Quad Sign(Quad r)
    {
        if (IsNaN(r))
            return NaN;

        if (r == Zero)
            return 0;

        return r.negative ? NegativeOne : One;
    }

    public readonly Quad Sign() => Sign(this);

    static readonly byte[] randBytes = new byte[16];

    public static Quad NextReal(Random rand)
    {
        rand.NextBytes(randBytes);
        Quad r;
        r.negative = false;
        r.exponent = 0;
        ulong hi = BitConverter.ToUInt64(randBytes, 0);
        ulong lo = BitConverter.ToUInt64(randBytes, 8);

        if (hi == 0 && lo == 0)
            return Zero;

        r.mantissa = new Mantissa(hi, lo);
        r.signif = 128;
        r.Normalize();
        return r;
    }

    internal static Quad Random(Random rand)
    {
        rand.NextBytes(randBytes);
        Quad r;
        r.negative = rand.Next(2) == 1;
        r.exponent = (short)rand.Next(short.MinValue + 1, short.MaxValue);
        ulong hi = BitConverter.ToUInt64(randBytes, 0);
        ulong lo = BitConverter.ToUInt64(randBytes, 8);
        r.mantissa = new Mantissa(hi | msb, lo);
        r.signif = 128;
        return r;
    }

    public static Quad Log(Quad r)
    {
        if (IsNaN(r))
            return NaN;

        if (r.negative)
            return NaN;

        if (IsZero(r))
            return NaN;

        if (IsPositiveInfinity(r))
            return r;

        if (r == One)
            return Zero;

        Quad e = r.exponent * QuadConstants.Ln2;
        r.exponent = 0;
        Quad y = (r - One) / (r + One);
        Quad y2 = y * y;
        Quad num = One;
        Quad den = One;
        Quad sum = One;

        while (true)
        {
            num *= y2;
            den += Two;
            Quad term = num / den;
            if (term == Zero || term.exponent < sum.exponent - 128)
                break;
            sum += term;
        }

        sum = Two * y * sum + e;
        sum.Signif(122);
        return sum;
    }

    public readonly Quad Log() => Log(this);

    public static Quad Log(Quad a, Quad b) => Log(a) / Log(b);

    public static Quad Log10(Quad r) => Log(r) * OneOverLn10;

    public readonly Quad Log10() => Log10(this);

    public static Quad Log2(Quad r) => Log(r) * QuadConstants.OverLn2;

    public readonly Quad Log2() => Log2(this);

    private static Quad IntPower(Quad r, Quad n)
    {
        if (n == 0)
            return One;

        if (n < 0)
            return One / IntPower(r, -n);

        if (IsOdd(n))
            return r * IntPower(r, n - One);

        n.exponent--;
        r = IntPower(r, n);
        return r * r;
    }

    private static Quad expMax = new(22712);

    private static Quad negExpMax = new(-22713);

    public static Quad Exp(Quad r)
    {
        if (IsNaN(r))
            return NaN;

        if (r > expMax)
            return PositiveInfinity;

        if (r < -expMax)
            return Zero;

        if (r.exponent < -88)
            return One;

        Quad z = Truncate(r);
        Quad f = Frac(r);
        Quad ez = IntPower(E, z);
        Quad sum = One;
        Quad count = Zero;
        Quad fac = One;
        Quad power = One;

        while (true)
        {
            power *= f;
            count += One;
            fac *= count;
            Quad term = power / fac;
            if (term == Zero || term.exponent < sum.exponent - 128)
                break;
            sum += term;
        }

        sum *= ez;
        sum.Signif(119);
        return sum;
    }

    public readonly Quad Exp() => Exp(this);

    public static Quad Mod(Quad a, Quad b)
    {
        Quad q = a / b;
        q = b * Truncate(q);
        return a - q;
    }

    public readonly Quad Mod(Quad b) => Mod(this, b);

    public static Quad Sin(Quad r)
    {
        if (r.exponent > 63)
            return r;

        bool neg = r.negative;
        r.negative = false;
        r = Mod(r, QuadConstants.Twoπ);

        if (r > QuadConstants.π)
        {
            neg = !neg;
            r -= QuadConstants.π;
        }

        Quad r2 = -(r * r);
        Quad num = r;
        Quad den = One;
        Quad count = One;
        Quad sum = r;

        while (true)
        {
            num *= r2;
            count += One;
            den *= count;
            count += One;
            den *= count;
            Quad term = num / den;
            if (term == Zero || term.exponent < sum.exponent - 128)
                break;
            sum += term;
        }

        r = sum;
        
        if (neg) 
            r.negative = true;

        r.Signif(122);
        return r;
    }

    public readonly Quad Sin() => Sin(this);

    public static Quad Cos(Quad r) => Sin(QuadConstants.Halfπ - r);

    public readonly Quad Cos() => Cos(this);

    public static Quad Tan(Quad r)
    {
        return Sin(r) / Cos(r);
    }

    public readonly Quad Tan() => Tan(this);

    public static Quad Asin(Quad r)
    {
        Quad a = Abs(r);

        if (a > 1)
            return NaN;

        if (a == 1)
            return r.Sign() * QuadConstants.Halfπ;

        return r.Sign() * Atan(a / Sqrt(One - a * a));
    }

    public readonly Quad Asin() => Asin(this);

    public static Quad Acos(Quad r)
    {
        if (Abs(r) > 1)
            return NaN;

        if (Abs(r) == 1)
            return Zero;

        if (r < 0)
            return QuadConstants.Halfπ + Asin(r);

        return QuadConstants.Halfπ - Asin(r);
    }

    public readonly Quad Acos() => Acos(this);

    public static Quad Atan(Quad r)
    {
        Quad a = Abs(r);

        if (a > One)
            return r.Sign() * (QuadConstants.Halfπ - Atan(1 / a));

        if (a == One)
            return r > Zero ? QuadConstants.π / 4 : -QuadConstants.π / 4;

        if (a > QuadConstants.Sqrt2 - One)
            return r.Sign() * (QuadConstants.π / 4 - Atan((1 - a) / (1 + a)));

        if (a.exponent < -64)
            return a;

        Quad sum = r;
        Quad r2 = -r * r;
        Quad power = r;
        int n = 1;

        while (true)
        {
            n += 2;
            power *= r2;
            Quad term = power / n;

            if (term == Zero || term.exponent < sum.exponent - 128)
                break;

            sum += term;
        }

        return sum;
    }

    public readonly Quad Atan() => Atan(this);

    public static Quad Atan2(Quad y, Quad x)
    {
        if (IsNaN(y) || IsNaN(x))
            return NaN;

        if (x == Zero)
            if (y == Zero)
                return NaN;
            else
                return y.Sign() * QuadConstants.Halfπ;

        if (y == Zero)
            return x > Zero ? Zero : QuadConstants.π;

        if (x > 0)
            return Atan(y / x);

        return y.Sign() * (QuadConstants.π - Abs(Atan(y / x)));
    }

    public static Quad Pow(Quad a, Quad b)
    {
        if (IsZero(b)) 
            return One;

        if (IsZero(a)) 
            return Zero;

        return Exp(b * Log(a));
    }

    public static Quad Pow(Quad a, int b)
    {
        if (b == 0) 
            return One;

        if (IsZero(a)) 
            return Zero;

        if (b < 0) 
            return Quad.One / Pow(a, -b);

        if ((b & 1) == 0)
        {
            Quad p = Pow(a, b / 2);
            return p * p;
        }

        return a * Pow(a, b - 1);
    }

    public readonly Quad Pow(Quad b) => Pow(this, b);

    public static Quad Sinh(Quad r)
    {
        if (r.exponent < -42)
            return r;

        r = Exp(r);
        if (IsInfinity(r))
            return PositiveInfinity;
        if (r == Zero)
            return NegativeInfinity;

        r -= One / r;
        r.exponent--;
        return r;
    }

    public readonly Quad Sinh() => Sinh(this);

    public static Quad Cosh(Quad r)
    {
        r = Exp(r);
        if (IsInfinity(r))
            return PositiveInfinity;
        if (r == Zero)
            return NegativeInfinity;

        r += One / r;
        r.exponent--;
        return r;
    }

    public readonly Quad Cosh() => Cosh(this);

    public static Quad Tanh(Quad r)
    {
        if (IsNaN(r))
            return NaN;

        if (r.exponent < -42)
            return r;

        r.exponent++;
        r = Exp(r);
        if (IsInfinity(r))
            return One;
        if (r == Zero)
            return NegativeOne;

        r = (r - One) / (r + One);
        return r;
    }

    public readonly Quad Tanh() => Tanh(this);

    internal Quad Mask(int wordSize)
    {
        if (exponent <= 0 || exponent >= 128 + wordSize)
            return Zero;

        Quad r = Truncate();

        while (r.exponent > wordSize)
        {
            r.mantissa <<= 1;
            r.exponent--;
        }

        r.Normalize();
        return r;
    }

    public readonly int CompareTo(Quad other) =>
        this == other ? 0 :
        this < other ? -1 :
        +1;

    public readonly bool Equals(Quad other) => this == other;
}
