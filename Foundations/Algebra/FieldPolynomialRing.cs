
using System.Diagnostics;

namespace Foundations.Algebra;

public class FieldPolynomialRing<T>(Field<T> coefficientField) : PolynomialRing<T>(coefficientField)
    where T : IEquatable<T>
{
    public readonly Field<T> CoefficientField = coefficientField;

    public static (Polynomial<T> Quotient, Polynomial<T> Remainder) DivMod(Polynomial<T> left, Polynomial<T> right)
    {
        var ring = left.Ring;

        if (ring is not FieldPolynomialRing<T> f || right.Ring is not FieldPolynomialRing<T>)
            throw new NotSupportedException("Polynomials must be over fields, not just rings.");

        var field = f.CoefficientField;

        if (!ring.Equals(right.Ring))
            throw new NotSupportedException("Polynomials must be over the same field.");

        if (left.Indeterminates.Concat(right.Indeterminates).Distinct().Count() > 1)
            throw new NotImplementedException("This method only implements univariate polynomials.");

        List<Monomial<T>> quotient = [];

        while (left.Degree >= right.Degree)
        {
            var lc = left.Terms[^1];
            var rc = right.Terms[^1];
            var c = field.Divide(lc.Coefficient, rc.Coefficient);
            var e = lc.Exponents[0] - (rc.Exponents.IsEmpty ? 0 : rc.Exponents[0]);
            var m = new Monomial<T>(field, c, e);
            quotient.Add(m);
            var s = right * m;
            left -= s;
        }

        return (new Polynomial<T>(ring, right.Indeterminates, quotient), left);
    }

    public static Polynomial<T> GCD(Polynomial<T> a, Polynomial<T> b)
    {
        if (b.Terms.Count == 0)
            return b;

        while (b.Terms.Count > 0)
        {
            (var d, var r) = DivMod(a, b);
            if (!(d * b + r).Equals(a))
                Debugger.Break();
            (a, b) = (b, r);
        }

        return a;
    }

    public static Polynomial<T> Divide(Polynomial<T> left, Polynomial<T> right)
    {
        var (q, _) = DivMod(left, right);
        return q;
    }

    public static Polynomial<T> Mod(Polynomial<T> left, Polynomial<T> right)
    {
        var (_, r) = DivMod(left, right);
        return r;
    }

    public static IEnumerable<Polynomial<T>> Factorize(Polynomial<T> f)
    {
        if (f.Ring is not FieldPolynomialRing<T> pf || pf.CoefficientField is not FiniteField ff)
            throw new ArgumentException("Polynomial must be over a finite field, not just a ring.");

        var q = ff.Order;
        var stack = new Stack<Polynomial<T>>();

        // Factor out leading coefficient.
        if (!f.Terms[^1].Coefficient.Equals(pf.CoefficientRing.One))
        {
            var c = pf.Create(f.Indeterminates, f.Terms[^1].Coefficient);
            yield return c;
            f = Divide(f, c);
        }

        // Factor out powers of x if there is no degree-0 coefficient.
        var x = pf.Create(f.Indeterminates, new Monomial<T>(pf.CoefficientRing, pf.CoefficientRing.One, 1));

        if (f.Terms[0].Degree > 0)
        {
            var xd = pf.Create(f.Indeterminates, new Monomial<T>(pf.CoefficientRing, pf.CoefficientRing.One, 
                f.Terms[0].Degree));

            yield return xd;
            f = Divide(f, xd);
        }

        stack.Push(f);

        while (stack.Count != 0)
        {
            f = stack.Pop();

            // Linear factor.
            if (f.Degree < 2)
            {
                yield return f;
                continue;
            }

            // Square-free factorization.
            var df = f.Derivative(f.Indeterminates[0]);
            var g = Monic(GCD(f, df));

            if (g.Degree > 0)
            {
                stack.Push(g);
                stack.Push(Divide(f, g));
                continue;
            }

            // Distinct-degree factorization.
            var gs = DistinctDegreeFactorization(f);

            if (!gs[^1].Equals(pf.One))
            {
                yield return f;
                continue;
            }

            // Equal-degree factorization.
            for (var d = 1; d <= gs.Count; d++)
            {
                var gi = gs[d - 1];

                if (gi.Equals(pf.One))
                    continue;

                while (gi.Degree > d)
                {
                    var aterms = Enumerable.Range(0, d + 1).Select(j => new Monomial<T>(pf.CoefficientRing,
                            pf.CoefficientRing.FromInteger((long)(Random.Shared.NextDouble() * q)), j)).ToList();

                    Polynomial<T> a = new(pf, f.Indeterminates, aterms);

                    if (a.Degree < 1)
                        continue;

                    g = Monic(GCD(a, gi));

                    if (g.Degree > 0)
                    {
                        yield return g;
                        gi = Divide(gi, g);
                        continue;
                    }

                    var m = ((int)BigInteger.Pow(q, d) - 1) / 2;
                    var apow = pf.ModPow(a, m, gi);
                    apow += pf.One.WithIndeterminates(f.Indeterminates);

                    if (apow.Degree >= 0)
                    {
                        g = Monic(GCD(apow, gi));

                        if (g.Degree > 0)
                        {
                            if (g.Degree == d)
                                yield return g;
                            else
                                stack.Push(g);

                            gi = Divide(gi, g);
                            continue;
                        }
                    }
                }

                yield return gi;
            }
        }
    }

    private static Polynomial<T> Monic(Polynomial<T> p)
    {
        if (p.Equals(p.Ring.Zero))
            return p;

        var lc = p.Terms[^1].Coefficient;

        if (lc.Equals(p.Ring.CoefficientRing.One))
            return p;

        var pf = p.Ring as FieldPolynomialRing<T>;
        var c = pf!.Create(p.Indeterminates, lc);
        return Divide(p, c);
    }

    public Polynomial<T> ModPow(Polynomial<T> p, BigInteger n, Polynomial<T> modulus)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n));

        if (n == 0)
            return p.Ring.One;

        if (n == 1)
            return Mod(p, modulus);

        if (n.IsEven)
        {
            var h = ModPow(p, n / 2, modulus);
            return Mod(h * h, modulus);
        }

        return Mod(p * ModPow(p, n - 1, modulus), modulus);
    }

    public Polynomial<T> ModPow(Polynomial<T> value, List<(int, int)> backReferences, Polynomial<T> modulus)
    {
        List<Polynomial<T>> temp = [value];
        Polynomial<T> t = value;
        var i = 0;

        foreach (var (a, b) in backReferences)
        {
            if (a == i && b == i)
                t *= t;
            else
                t = temp[a] * (b == i ? t : temp[b]);

            t = Mod(t, modulus);
            temp.Add(t);
            ++i;
        }

        return temp[^1];
    }

    public static List<Polynomial<T>> DistinctDegreeFactorization(Polynomial<T> f)
    {
        var pf = f.Ring as FieldPolynomialRing<T>;
        var q = (f.Ring.CoefficientRing as FiniteField)!.Order;
        var x = pf!.Create(f.Indeterminates, new Monomial<T>(pf.CoefficientRing, pf.CoefficientRing.One, 1));
        var n = f.Degree;
        List<Polynomial<T>> result = [];
        
        for (var i = 1; i <= n; i++)
        {
            if (f.Equals(pf.One))
            {
                result.Add(f);
                continue;
            }

            var power = BigInteger.Pow(q, i);
            var s = pf.ModPow(x, power, f) - x;
            var g = s.Equals(pf.Zero) ? f : Monic(GCD(f, s));
            result.Add(g);
            f = Divide(f, g);
        }

        return result;
    }

    public static Polynomial<T> Divide(Polynomial<T> p, T n)
    {
        if (p is not FieldPolynomialRing<T> f) 
            throw new ArgumentException(null, nameof(p));

        var terms = p.Terms.Select(term => new Monomial<T>(term.Ring, f.CoefficientField.Divide(term.Coefficient, n), 
            term.Exponents));

        return f.Create(p.Indeterminates, terms);
    }
}