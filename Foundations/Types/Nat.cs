
namespace Foundations.Types;

public partial struct Nat :
    IEquatable<Nat>,
    IComparable<Nat>,
    IAdditiveIdentity<Nat, Nat>,
    IAdditionOperators<Nat, Nat, Nat>,
    IMultiplicativeIdentity<Nat, Nat>,
    IMultiplyOperators<Nat, Nat, Nat>,
    IComparisonOperators<Nat, Nat, bool>,
    IShiftOperators<Nat, int, Nat>,
    IBitwiseOperators<Nat, Nat, Nat>
{
    private static readonly Nat zero = new(0);

    private static readonly Nat one = new(1);

    private BigInteger b;

    public static Nat AdditiveIdentity => Zero;

    public static Nat MultiplicativeIdentity => One;

    public static Nat One => one;

    public static Nat Zero => zero;

    public static implicit operator BigInteger(Nat n) => n.b;

    public static explicit operator Nat(BigInteger n) => new(n);

    public static Nat operator +(Nat left, Nat right) => new(left.b + right.b);

    public static Nat operator -(Nat left, Nat right) => new(left.b - right.b);

    public static Nat operator *(Nat left, Nat right) => new(left.b * right.b);

    public static bool operator >(Nat left, Nat right) => left.b > right.b;

    public static bool operator >=(Nat left, Nat right) => left.b >= right.b;

    public static bool operator <(Nat left, Nat right) => left.b < right.b;

    public static bool operator <=(Nat left, Nat right) => left.b <= right.b;

    public static bool operator ==(Nat left, Nat right) => left.Equals(right);

    public static bool operator !=(Nat left, Nat right) => !(left == right);

    public static Nat operator <<(Nat value, int shiftAmount) => new(value.b << shiftAmount);

    public static Nat operator >>(Nat value, int shiftAmount) => new(value.b >> shiftAmount);

    public static Nat operator >>>(Nat value, int shiftAmount) => new(value.b >>> shiftAmount);

    public static Nat operator &(Nat left, Nat right) => new(left.b & right.b);

    public static Nat operator |(Nat left, Nat right) => new(left.b | right.b);

    public static Nat operator ^(Nat left, Nat right) => new(left.b ^ right.b);

    public static Nat operator ~(Nat value) => new(~value.b);

    public static Nat operator %(Nat left, Nat right) => new(left.b % right.b);

    public static Nat operator /(Nat left, Nat right) => new(left.b / right.b);

    public static Nat operator +(Nat value) => value;

    public static Nat operator ++(Nat value)
    {
        value.b++;
        return value;
    }

    public static Nat operator --(Nat value)
    {
        if (value.b.IsZero) throw new ArgumentOutOfRangeException(nameof(value));
        value.b--;
        return value;
    }

    public static IEnumerable<Nat> All()
    {
        var n = Zero;

        while (true)
        {
            yield return n;
            n++;
        }
    }

    private Nat(BigInteger n)
    {
        if (n.Sign < 0)
            throw new ArgumentOutOfRangeException(nameof(n));

        b = n;
    }

    public readonly bool IsZero => b.IsZero;

    public readonly bool IsEven => b.IsEven;

    public readonly bool Equals(Nat other) => b.Equals(other.b);

    public override readonly bool Equals(object obj) => obj is Nat n && Equals(n);

    public override readonly int GetHashCode() => b.GetHashCode();

    public override readonly string ToString() => b.ToString();

    public readonly int CompareTo(Nat other) => b.CompareTo(other.b);

    public readonly int GetByteCount() => b.GetByteCount();

    public static bool IsPow2(Nat value) => value.b.IsPowerOfTwo;

    public static Nat Log2(Nat value) => new(BigInteger.Log2(value.b));


    public static implicit operator Nat(sbyte n) => new(n);

    public static implicit operator Nat(byte n) => new(n);

    public static implicit operator Nat(short n) => new(n);

    public static implicit operator Nat(ushort n) => new(n);

    public static implicit operator Nat(int n) => new(n);

    public static implicit operator Nat(uint n) => new(n);

    public static implicit operator Nat(long n) => new(n);

    public static implicit operator Nat(ulong n) => new(n);

    public static explicit operator sbyte(Nat n) => (sbyte)n.b;

    public static explicit operator byte(Nat n) => (byte)n.b;

    public static explicit operator short(Nat n) => (short)n.b;

    public static explicit operator ushort(Nat n) => (ushort)n.b;

    public static explicit operator int(Nat n) => (int)n.b;

    public static explicit operator uint(Nat n) => (uint)n.b;

    public static explicit operator long(Nat n) => (long)n.b;

    public static explicit operator ulong(Nat n) => (ulong)n.b;

    public static explicit operator float(Nat n) => (float)n.b;

    public static explicit operator double(Nat n) => (double)n.b;

    public static explicit operator decimal(Nat n) => (decimal)n.b;

    public readonly byte[] ToByteArray() => b.ToByteArray();

    public static (Nat, Nat) DivRem(Nat p, Nat q)
    {
        var (div, rem) = BigInteger.DivRem(p.b, q.b);
        return (new(div), new(rem));
    }

    public static (Nat, int) DivRem(Nat p, int q)
    {
        var (div, rem) = BigInteger.DivRem(p.b, q);
        return (new(div), (int)rem);
    }

    public Nat(byte[] array)
    {
        BigInteger b = new(array ?? throw new ArgumentNullException(nameof(array)));
        if (b.Sign < 0) throw new ArgumentOutOfRangeException(nameof(array));
        this.b = b;
    }

    public static Nat Min(Nat a, Nat b) => a < b ? a : b;

    public static bool TryParse(string s, out Nat n)
    {
        n = Zero;

        if (s is null)
            return false;

        foreach (var ch in s)
        {
            if (ch == '_') 
                continue;

            if (!char.IsDigit(ch))
                return false;

            n = n * 10 + (int)char.GetNumericValue(ch);
        }

        return true;
    }

    public static Nat Parse(string s)
    {
        ArgumentNullException.ThrowIfNull(s, nameof(s));

        if (TryParse(s, out var n))
            return n;

        throw new ArgumentException("String only contain digits and underscore.", nameof(s));
    }

    public static Nat GreatestCommonDivisor(Nat p, Nat q)
    {
        return (Nat)BigInteger.GreatestCommonDivisor(p.b, q.b);
    }

    public static Nat Abs(Nat n) => n;
}