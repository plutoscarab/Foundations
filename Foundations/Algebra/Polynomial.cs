
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Foundations.Algebra;

public class Polynomial<T> : IEquatable<Polynomial<T>>, IEqualityOperators<Polynomial<T>, Polynomial<T>, bool>
    where T : IEquatable<T>
{
    public readonly PolynomialRing<T> Ring;

    public readonly ImmutableList<Indeterminate> Indeterminates;

    public readonly ImmutableList<Monomial<T>> Terms;

    public readonly int Degree;

    internal Polynomial(PolynomialRing<T> ring, IEnumerable<Indeterminate> indeterminates, IEnumerable<Monomial<T>> terms)
    {
        Ring = ring;
        Indeterminates = [.. indeterminates];
        var ts = terms.Where(term => !term.Coefficient.Equals(ring.CoefficientRing.Zero)).OrderBy(term => term.Signature(Degree)).ToList();
        Degree = ts.Count != 0 ? ts.Max(t => t.Degree) : -1;
        Terms = [.. ts];
        var expFlag = new bool[Indeterminates.Count];

        for (var i = 0; i < Terms.Count; i++)
        {
            if (Terms[i].Exponents.Count != Indeterminates.Count)
            {
                throw new ArgumentException("Each term must include one exponent per indeterminate.");
            }

            for (var j = 0; j < Indeterminates.Count; j++)
            {
                expFlag[j] |= Terms[i].Exponents[j] > 0;
            }

            if (i > 0 && Terms[i].Degree < Terms[i - 1].Degree)
            {
                throw new ArgumentException("Terms must be in increasing degree order.");
            }

            for (var j = i + 1; j < Terms.Count; j++)
            {
                if (Terms[i].Exponents.SequenceEqual(Terms[j].Exponents))
                {
                    throw new ArgumentException($"Duplicate exponent signature in terms {i} and {j}.");
                }
            }
        }
/*
        if (trimIndeterminates)
        {
            var good = expFlag.Select((f, i) => (f, i)).Where(fi => fi.f).Select(fi => fi.i).ToList();

            if (good.Count < Indeterminates.Count)
            {
                Indeterminates = new(good.Select(i => Indeterminates[i]));
                Terms = new(Terms.Select(term => new Monomial<T>(term.Ring, term.Coefficient, good.Select(i => term.Exponents[i]).ToArray())));
            }
        }
*/        
    }

    internal Polynomial<T> TrimIndeterminates()
    {
        var expFlag = new bool[Indeterminates.Count];

        foreach (var term in Terms)
        {
            for (var j = 0; j < Indeterminates.Count; j++)
            {
                expFlag[j] |= term.Exponents[j] != 0;
            }
        }

        var good = expFlag.Select((f, i) => (f, i)).Where(fi => fi.f).Select(fi => fi.i).ToList();

        if (good.Count < Indeterminates.Count)
        {
            return new (Ring, good.Select(i => Indeterminates[i]), Terms.Select(term => new Monomial<T>(term.Ring, term.Coefficient, good.Select(i => term.Exponents[i]).ToArray())));
        }

        return this;
    }

    public Polynomial<T> WithIndeterminates(params Indeterminate[] indeterminates) =>
        new(Ring, indeterminates, Terms);

    public Polynomial<T> WithIndeterminates(IEnumerable<Indeterminate> indeterminates) =>
        new(Ring, indeterminates, Terms);

    internal Polynomial(PolynomialRing<T> ring, IEnumerable<Monomial<T>> terms)
        : this(ring, DefaultIndeterminates(terms), terms)
    {
    }

    private static IEnumerable<Indeterminate> DefaultIndeterminates(IEnumerable<Monomial<T>> terms)
    {
        var n = terms.Any() ? terms.Max(t => t.Exponents.Count) : 0;

        if (n > Indeterminate.DefaultInd.Count)
            throw new ArgumentException("Ran out of letters for default indeterminates.");

        return Enumerable.Range(0, n).Select(i => Indeterminate.DefaultInd[i]);
    }

    public static Polynomial<T> operator +(Polynomial<T> p) => p;

    public static Polynomial<T> operator -(Polynomial<T> p) => p.Ring.Negate(p);

    public static Polynomial<T> operator +(Polynomial<T> p, Polynomial<T> q) =>
        p.Ring.Add(p, q);

    public static Polynomial<T> operator -(Polynomial<T> p, Polynomial<T> q) =>
        p.Ring.Subtract(p, q);

    public static Polynomial<T> operator *(Polynomial<T> p, Polynomial<T> q) =>
        p.Ring.Multiply(p, q);

    public static Polynomial<T> operator *(Polynomial<T> p, Monomial<T> m) =>
        p.Ring.Multiply(p, m);

    public bool Equals(Polynomial<T> other) =>
        other is not null && other.Ring.Equals(Ring) && Enumerable.SequenceEqual(
            other.Terms.OrderBy(term => term.Signature(other.Degree)), 
            Terms.OrderBy(term => term.Signature(Degree)));

    public override bool Equals(object obj) => Equals(obj as Polynomial<T>);

    public override int GetHashCode() => HashCode.Combine(Ring, Terms);

    public override string ToString()
    {
        if (Terms.Count == 0)
        {
            return Ring.CoefficientRing.Zero.ToString()!;
        }

        var s = new StringBuilder();

        foreach (var term in Terms)
        {
            var coeff = term.Coefficient;

            if (s.Length > 0)
            {
                s.Append(' ');
            }

            if (Ring.CoefficientRing.ElementsHaveSign && Ring.CoefficientRing.Sign(coeff) < 0)
            {
                coeff = Ring.CoefficientRing.Negate(coeff);
                s.Append('−');

                if (s.Length > 1)
                {
                    s.Append(' ');
                }
            }
            else if (s.Length > 0)
            {
                s.Append("+ ");
            }

            if (term.Degree == 0 || !coeff.Equals(Ring.CoefficientRing.One))
            {
                var cs = coeff.ToString();

                if (cs!.Any(c => !char.IsLetterOrDigit(c)))
                    cs = $"({cs})";

                s.Append(cs);
            }

            for (var e = 0; e < term.Exponents.Count; e++)
            {
                var exp = term.Exponents[e];

                if (exp > 0)
                {
                    s.Append(Indeterminates[e]);

                    if (exp > 1)
                    {
                        s.Append(exp.ToSuperscript());
                    }
                }
            }
        }

        return s.ToString();
    }

    public Polynomial<T> AlignIndeterminates(List<Indeterminate> indeterminates)
    {
        var map = Indeterminates.Select(i => indeterminates.IndexOf(i)).ToList();
        var terms = new List<Monomial<T>>();

        foreach (var t in Terms)
        {
            var exp = new int[indeterminates.Count];

            for (var i = 0; i < t.Exponents.Count; i++)
            {
                exp[map[i]] = t.Exponents[i];
            }

            terms.Add(new(t.Ring, t.Coefficient, ImmutableList.Create(exp)));
        }

        return new(Ring, indeterminates, terms);
    }

    private T Scale(T value, int multiple)
    {
        if (Ring.CoefficientRing.HasIntegerRepresentation)
            return Ring.CoefficientRing.Multiply(value, Ring.CoefficientRing.FromInteger(multiple));

        if (multiple < 0)
            return Ring.CoefficientRing.Negate(Scale(value, -multiple));

        if (multiple == 0)
            return Ring.CoefficientRing.Zero;

        if (multiple == 1)
            return value;

        if ((multiple & 1) == 0)
            return Scale(Scale(value, multiple / 2), 2);

        return Ring.CoefficientRing.Multiply(value, Scale(value, multiple - 1));
    }

    public Polynomial<T> Derivative(Indeterminate indeterminate)
    {
        var i = Indeterminates.IndexOf(indeterminate);

        if (i == -1)
            i = Indeterminates.Select(n => n.Symbol).ToList().IndexOf(indeterminate.Symbol);

        if (i == -1)
            return Ring.Zero;

        var terms = Terms.Where(m => m.Exponents[i] > 0).Select(m => new Monomial<T>(m.Ring, 
            Scale(m.Coefficient, m.Exponents[i]), m.Exponents.Select((e, j) => i == j ? e - 1 : e).ToArray())).ToList();

        return new(Ring, Indeterminates, terms);
    }

    public static Polynomial<T> operator ^(Polynomial<T> p, int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
        if (n == 0) return (p ?? throw new ArgumentNullException(nameof(p))).Ring.One;
        if (n == 1) return p;

        if ((n & 1) == 0)
        {
            var h = (p ^ (n / 2));
            return h * h;
        }

        return p * (p ^ (n - 1));
    }

    public static Polynomial<T> operator +(Polynomial<T> p, T n) =>
        p + p.Ring.Create(p.Indeterminates, n);

    public static Polynomial<T> operator +(T n, Polynomial<T> p) =>
        p.Ring.Create(p.Indeterminates, n) + p;

    public static Polynomial<T> operator +(Polynomial<T> p, int n) =>
        p + p.Ring.Create(p.Indeterminates, p.Ring.CoefficientRing.FromInteger(n));

    public static Polynomial<T> operator +(int n, Polynomial<T> p) =>
        p.Ring.Create(p.Indeterminates, p.Ring.CoefficientRing.FromInteger(n)) + p;

    public static Polynomial<T> operator -(Polynomial<T> p, T n) =>
        p - p.Ring.Create(p.Indeterminates, n);

    public static Polynomial<T> operator -(T n, Polynomial<T> p) =>
        p.Ring.Create(p.Indeterminates, n) - p;

    public static Polynomial<T> operator -(Polynomial<T> p, int n) =>
        p - p.Ring.Create(p.Indeterminates, p.Ring.CoefficientRing.FromInteger(n));

    public static Polynomial<T> operator -(int n, Polynomial<T> p) =>
        p.Ring.Create(p.Indeterminates, p.Ring.CoefficientRing.FromInteger(n)) - p;

    public static Polynomial<T> operator *(Polynomial<T> p, T n) =>
        p * p.Ring.Create(p.Indeterminates, n);

    public static Polynomial<T> operator *(T n, Polynomial<T> p) =>
        p.Ring.Create(p.Indeterminates, n) * p;

    public static Polynomial<T> operator *(Polynomial<T> p, int n) =>
        p * p.Ring.Create(p.Indeterminates, p.Ring.CoefficientRing.FromInteger(n));

    public static Polynomial<T> operator *(int n, Polynomial<T> p) =>
        p.Ring.Create(p.Indeterminates, p.Ring.CoefficientRing.FromInteger(n)) * p;

    public static bool operator ==(Polynomial<T> left, Polynomial<T> right) => left is not null && left.TrimIndeterminates().Equals(right?.TrimIndeterminates());

    public static bool operator !=(Polynomial<T> left, Polynomial<T> right) => !(left == right);

    public string AsCode() => Terms.Count != 0 ? string.Join(" + ", Terms.OrderBy(term => term.Signature(Degree))
        .Select(term => term.AsCode(Indeterminates))) : Ring.Zero.ToString();

    public static bool operator ==(Polynomial<T> p, T n) => (p.Degree == 0 && p.Terms[0].Coefficient.Equals(n)) || (p.Degree == -1 && n.Equals(p.Ring.CoefficientRing.Zero));

    public static bool operator !=(Polynomial<T> p, T n) => !(p == n);

    public static bool operator ==(T n, Polynomial<T> p) => (p.Degree == 0 && p.Terms[0].Coefficient.Equals(n)) || (p.Degree == -1 && n.Equals(p.Ring.CoefficientRing.Zero));

    public static bool operator !=(T n, Polynomial<T> p) => !(p == n);
}
