
using Foundations.Coding;

namespace Foundations.Types;

/// <summary>
/// Polynomial over field GF(2) with maximum degree 63.
/// </summary>
public readonly partial struct SmallDegreePolyGF2
{
    /// <summary>
    /// Constant value 0.
    /// </summary>
    public static readonly SmallDegreePolyGF2 Zero = new(0UL);

    /// <summary>
    /// Constant value 1.
    /// </summary>
    public static readonly SmallDegreePolyGF2 One = new(1UL);

    /// <summary>
    /// The degree of the polynomial.
    /// </summary>
    public readonly int Degree => Coefficients == 0 ? 0 : Bits.FloorLog2(Coefficients);

    /// <summary>
    /// Number of terms in the polynomial.
    /// </summary>
    public readonly int TermCount => Bits.Count(Coefficients);

    /// <summary>
    /// Creates a <see cref="SmallDegreePolyGF2"/>. 
    /// </summary>
    /// <param name="exponents">The exponents of terms with non-zero coefficient.</param>
    public SmallDegreePolyGF2(params int[] exponents) : this(0UL)
    {
        ArgumentNullException.ThrowIfNull(exponents, nameof(exponents));

        ulong result = 0;

        foreach (var e in exponents)
        {
            if (e < 0 || e > 63) throw new ArgumentOutOfRangeException();
            result |= 1UL << e;
        }

        Coefficients = result;
    }

    /// <summary>
    /// Implementation of <see cref="object.ToString"/>.
    /// </summary>
    public override string ToString()
    {
        if (Coefficients == 0) return "0";

        var s = new StringBuilder();

        for (int i = 0; i <= Degree; i++)
        {
            if ((Coefficients & (1ul << i)) == 0) continue;
            if (s.Length > 0) s.Append(" + ");

            if (i == 0) s.Append('1');
            else s.Append("x" + i.ToSuperscript());
        }

        return s.ToString();
    }

    /// <summary>
    /// Addition operator.
    /// </summary>
    public static SmallDegreePolyGF2 operator +(SmallDegreePolyGF2 p, SmallDegreePolyGF2 q)
    {
        return new SmallDegreePolyGF2(p.Coefficients ^ q.Coefficients);
    }

    /// <summary>
    /// Right-shift operator.
    /// </summary>
    public static SmallDegreePolyGF2 operator >>(SmallDegreePolyGF2 p, int shift)
    {
        shift &= 63;
        return new SmallDegreePolyGF2(p.Coefficients >> shift);
    }

    /// <summary>
    /// Left-shift operator.
    /// </summary>
    public static SmallDegreePolyGF2 operator <<(SmallDegreePolyGF2 p, int shift)
    {
        shift &= 63;
        if (p.Degree + shift > 63) throw new OverflowException();
        return new SmallDegreePolyGF2(p.Coefficients << shift);
    }

    /// <summary>
    /// Dot product.
    /// </summary>
    public static SmallDegreePolyGF2 operator &(SmallDegreePolyGF2 p, SmallDegreePolyGF2 q)
    {
        return new SmallDegreePolyGF2(p.Coefficients & q.Coefficients);
    }

    /// <summary>
    /// Multiplication operator.
    /// </summary>
    public static SmallDegreePolyGF2 operator *(SmallDegreePolyGF2 p, SmallDegreePolyGF2 q)
    {
        if (p.Degree + q.Degree > 63)
            throw new OverflowException();

        ulong result = 0;
        ulong f = p.Coefficients;
        ulong g = q.Coefficients;

        for (int i = q.Degree; i > 0; i--)
        {
            if ((g & 1) != 0) result ^= f;
            f <<= 1;
            g >>= 1;
        }

        return new SmallDegreePolyGF2(result ^ f);
    }

    /// <summary>
    /// Division operator.
    /// </summary>
    public static SmallDegreePolyGF2 operator /(SmallDegreePolyGF2 p, SmallDegreePolyGF2 q)
    {
        return DivRem(p, q, out _);
    }

    /// <summary>
    /// Module operator.
    /// </summary>
    public static SmallDegreePolyGF2 operator %(SmallDegreePolyGF2 p, SmallDegreePolyGF2 q)
    {
        DivRem(p, q, out var r);
        return r;
    }

    /// <summary>
    /// Divide with remainder.
    /// </summary>
    /// <param name="p">Dividend.</param>
    /// <param name="q">Divisor.</param>
    /// <param name="rem">Remainder.</param>
    /// <returns>Quotient.</returns>
    public static SmallDegreePolyGF2 DivRem(SmallDegreePolyGF2 p, SmallDegreePolyGF2 q, out SmallDegreePolyGF2 rem)
    {
        if (q == Zero)
            throw new DivideByZeroException();

        if (p.Degree < q.Degree)
        {
            rem = p;
            return Zero;
        }

        int shift = p.Degree - q.Degree;
        var d = new SmallDegreePolyGF2(1ul << shift);
        var e = new SmallDegreePolyGF2(1ul << p.Degree);
        q <<= shift;
        var qu = new SmallDegreePolyGF2(0ul);

        while (shift-- >= 0)
        {
            if ((p & e) != Zero)
            {
                p += q;
                qu += d;
            }

            q >>= 1;
            d >>= 1;
            e >>= 1;
        }

        rem = p;
        return qu;
    }

    /// <summary>
    /// Increment operator.
    /// </summary>
    public static SmallDegreePolyGF2 operator ++(SmallDegreePolyGF2 p)
    {
        if (p.Coefficients == ulong.MaxValue) throw new OverflowException();
        return new SmallDegreePolyGF2(p.Coefficients + 1ul);
    }

    /// <summary>
    /// Decrement operator.
    /// </summary>
    public static SmallDegreePolyGF2 operator --(SmallDegreePolyGF2 p)
    {
        if (p.Coefficients == 0) throw new OverflowException();
        return new SmallDegreePolyGF2(p.Coefficients - 1ul);
    }

    /// <summary>
    /// Sequence of all polynomials over GF(2) of a certain degree.
    /// </summary>
    public static IEnumerable<SmallDegreePolyGF2> OfDegree(int degree)
    {
        for (ulong i = 0; i < (1ul << (degree - 1)); i++)
        {
            yield return new SmallDegreePolyGF2(i | (1ul << degree));
        }
    }

    /// <summary>
    /// Determines if this polynomial is irreducible (has no non-constant factors).
    /// </summary>
    public readonly bool IsIrreducible()
    {
        foreach (var d in Primes())
        {
            if (this % d == Zero)
                break;

            if ((d * d).Coefficients > Coefficients)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Sequence of irreducible polynomials over GF(2).
    /// </summary>
    public static IEnumerable<SmallDegreePolyGF2> Primes()
    {
        yield return new SmallDegreePolyGF2(2ul);   // x
        yield return new SmallDegreePolyGF2(3ul);   // x + 1
        yield return new SmallDegreePolyGF2(7ul);   // x² + x + 1

        for (ulong u = 9; u > 0; u += 2)
        {
            var p = new SmallDegreePolyGF2(u);
            if (p.IsIrreducible()) yield return p;
        }
    }

    /// <summary>
    /// Gets the irreducible factors of this polynomial.
    /// </summary>
    public IEnumerable<Factor<SmallDegreePolyGF2>> GetFactors()
    {
        if (this == Zero || this == One)
        {
            yield break;
        }

        foreach (var d in Primes())
        {
            var q = DivRem(this, d, out SmallDegreePolyGF2 r);

            if (r == Zero)
            {
                int k = 0;

                while (true)
                {
                    k++;
                    var temp = DivRem(q, d, out r);
                    if (r != Zero) break;
                    q = temp;
                }

                yield return new Factor<SmallDegreePolyGF2>(d, k);

                foreach (var f in q.GetFactors())
                {
                    yield return f;
                }

                yield break;
            }

            if ((d * d).Degree > Degree)
            {
                yield return new Factor<SmallDegreePolyGF2>(this, 1);
                yield break;
            }
        }
    }
}
