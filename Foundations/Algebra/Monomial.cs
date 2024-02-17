
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Foundations.Algebra;

public class Monomial<T> : IEquatable<Monomial<T>>
    where T : IEquatable<T>
{
    public readonly Ring<T> Ring;

    public readonly T Coefficient;
    
    public readonly ImmutableList<int> Exponents;

    public int Degree => Exponents.Sum();

    public Monomial(Ring<T> ring, T coefficient, ImmutableList<int> exponents)
    {
        Ring = ring;
        Coefficient = coefficient;
        Exponents = exponents;

        if (exponents.Any(e => e < 0))
            throw new ArgumentOutOfRangeException(nameof(exponents), "Negative exponents not allowed.");
    }

    public Monomial(Ring<T> ring, T coefficient, int[] exponents)
        : this(ring, coefficient, ImmutableList.Create(exponents))
    {
    }

    public Monomial(Ring<T> ring, T coefficient, int exponent)
        : this(ring, coefficient, new[] { exponent })
    {}

    public static Monomial<T> operator -(Monomial<T> m) =>
        new(m.Ring, m.Ring.Negate(m.Coefficient), m.Exponents);

    public override int GetHashCode() => HashCode.Combine(Ring, Coefficient, Exponents);

    internal long Signature(int degree)
    {
        var sig = Degree;

        foreach (var e in Exponents.Reverse())
        {
            sig = sig * (degree + 1) + e;
        }

        return sig;
    }

    public override string ToString() =>
        Coefficient.ToString() + "·" + string.Join("·", Exponents.Select(e => e.ToSuperscript()));

    public bool Equals(Monomial<T> other) => other is not null && other.Ring.Equals(Ring) && 
        other.Coefficient.Equals(Coefficient) && Enumerable.SequenceEqual(other.Exponents, Exponents);

    public override bool Equals(object obj) => Equals(obj as Monomial<T>);

    internal string AsCode(ImmutableList<Indeterminate> indeterminates)
    {
        StringBuilder s = new();
        s.Append(Coefficient);

        for (var i = 0; i < Exponents.Count; i++)
        {
            if (Exponents[i] == 0)
                continue;

            s.Append(" * ");

            if (Exponents[i] == 1)
            {
                s.Append(indeterminates[i]);
            }
            else
            {
                s.Append($"({indeterminates[i]} ^ {Exponents[i]})");
            }
        }

        return s.ToString();
    }
}