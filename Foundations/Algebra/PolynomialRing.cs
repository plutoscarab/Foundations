
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

namespace Foundations.Algebra;

public class PolynomialRing<T> : Ring<Polynomial<T>>, IEquatable<PolynomialRing<T>>
    where T : IEquatable<T>
{
    public Ring<T> CoefficientRing => ring;
    private readonly Ring<T> ring;
    private readonly Polynomial<T> zero;
    private readonly Polynomial<T> one;
    internal readonly Monomial<T> ZeroMonomial;

    public PolynomialRing(Ring<T> coefficientRing)
    {
        ring = coefficientRing;
        zero = Create(Array.Empty<Monomial<T>>());
        one = Create(ring.One);
        ZeroMonomial = new(coefficientRing, coefficientRing.Zero, 0);
    }

    public Polynomial<T> Create(IEnumerable<Indeterminate> indeterminates, IEnumerable<Monomial<T>> terms) =>
        new(this, indeterminates, terms);

    public Polynomial<T> Create(IEnumerable<Monomial<T>> terms) => new(this, terms);

    public Polynomial<T> Create(IEnumerable<Indeterminate> indeterminates, params Monomial<T>[] terms) =>
        new(this, indeterminates, terms.ToList());

    public Polynomial<T> Create(params Monomial<T>[] terms) => new(this, terms.ToList());

    public Polynomial<T> Create(Indeterminate indeterminate, IEnumerable<T> coefficients) =>
        new(this, new[] { indeterminate }, coefficients.Select((c, i) => new Monomial<T>(ring, c, i)).ToList());

    public Polynomial<T> Create(IEnumerable<T> coefficients) =>
        new(this, coefficients.Select((c, i) => new Monomial<T>(ring, c, i)).ToList());

    public Polynomial<T> Create(Indeterminate indeterminate, params T[] coefficients) =>
        new(this, new[] { indeterminate }, coefficients.Select((c, i) => new Monomial<T>(ring, c, i)).ToList());

    public Polynomial<T> Create(IEnumerable<Indeterminate> indeterminates, T constant) =>
        new(this, indeterminates, new[] { new Monomial<T>(ring, constant, new int[indeterminates.Count()]) });

    public Polynomial<T> Create(params T[] coefficients) =>
        new(this, coefficients.Select((c, i) => new Monomial<T>(ring, c, i)).ToList());

    public Polynomial<T> Create(T constant) =>
        constant.Equals(ring.Zero) ? Zero : new(this, new[] { new Monomial<T>(ring, constant, 0) });

    static readonly ConcurrentDictionary<string, Indeterminate> parameterIndeterminates = new();

    public Polynomial<T> Create(LambdaExpression expr)
    {
        var ind = expr.Parameters.Select(p => parameterIndeterminates.GetOrAdd(p.Name!, s => new Indeterminate(s))).ToArray();
        var map = expr.Parameters.Select((p, i) => (p, i)).ToDictionary(p => p.p.Name!, p => p.i);
        var body = expr.Body;
        var terms = GetTerms(map, body);
        return Create(ind, terms);
    }

    private static readonly ConcurrentDictionary<string, Indeterminate> indeterminateCache = new();

    public Polynomial<T> Create(string symbol)
    {
        var ind = new[] { indeterminateCache.GetOrAdd(symbol, s => new Indeterminate(s)) };
        var terms = new[] { new Monomial<T>(CoefficientRing, CoefficientRing.One, new[] { 1 } )};
        return new(this, ind, terms);
    }

    static readonly MethodInfo powMethod = typeof(IntExtensions).GetMethod("Pow")!;

    private List<Monomial<T>> GetTerms(Dictionary<string, int> map, Expression expr)
    {
        switch (expr)
        {
            case BinaryExpression b:
                var left = GetTerms(map, b.Left);
                var right = GetTerms(map, b.Right);

                switch (b.NodeType)
                {
                    case ExpressionType.Add:
                        return GetTerms(map, b.Left).Concat(GetTerms(map, b.Right)).ToList();
                    case ExpressionType.Subtract:
                        return GetTerms(map, b.Left).Concat(GetTerms(map, b.Right).Select(t => new Monomial<T>(t.Ring, t.Ring.Negate(t.Coefficient), t.Exponents))).ToList();
                    case ExpressionType.Multiply:
                        if (left.Count == 1 && right.Count == 1)
                        {
                            var exponents = new int[map.Count];
                            for (var i = 0; i < map.Count; i++)
                                exponents[i] = left[0].Exponents[i] + right[0].Exponents[i];
                            return new List<Monomial<T>> { new(CoefficientRing, CoefficientRing.Multiply(left[0].Coefficient, right[0].Coefficient), exponents) };
                        }
                        throw new NotImplementedException();
                }
                throw new NotImplementedException();

            case MethodCallExpression call:
                if (call.Method == powMethod && call.Arguments[0] is ParameterExpression p && call.Arguments[1] is ConstantExpression c && c.Value is int n)
                {
                    var exponents = new int[map.Count];
                    exponents[map[p.Name!]] = n;
                    return new List<Monomial<T>> { new(CoefficientRing, CoefficientRing.One, exponents) };
                }
                throw new NotImplementedException();

            case ConstantExpression k:
                if (k.Value is int kn)
                    return new List<Monomial<T>> { new(CoefficientRing, CoefficientRing.FromInteger(kn), new int[map.Count]) };
                throw new NotImplementedException();

            case ParameterExpression px:
                var pex = new int[map.Count];
                pex[map[px.Name!]] = 1;
                return new List<Monomial<T>> { new(CoefficientRing, CoefficientRing.One, pex) };

            default:
                throw new NotImplementedException();
        }
    }

    public Polynomial<T> Create(Expression<Func<int, int>> expr) => Create(expr as LambdaExpression);

    public Polynomial<T> Create(Expression<Func<int, int, int>> expr) => Create(expr as LambdaExpression);

    public Polynomial<T> Create(Expression<Func<int, int, int, int>> expr) => Create(expr as LambdaExpression);

    public override Polynomial<T> Zero => zero;

    public override Polynomial<T> One => one;

    public override bool ElementsHaveSign => false;

    private (Polynomial<T> A, Polynomial<T> B) AlignIndeterminates(Polynomial<T> a, Polynomial<T> b)
    {
        if (!a.Indeterminates.SequenceEqual(b.Indeterminates))
        {
            var ind = a.Indeterminates.Concat(b.Indeterminates).Distinct().OrderBy(i => i.Id).ToList();
            var sym = ind.Select(i => i.Symbol).Distinct();

            if (sym.Count() < ind.Count)
                throw new ArgumentException("Distinct indeterminates must have distinct symbols.");

            a = a.AlignIndeterminates(ind);
            b = b.AlignIndeterminates(ind);
        }

        return (a, b);
    }

    private IEnumerable<(ImmutableList<Indeterminate> Ind, ImmutableList<int> Exponents, T A, T B)> AlignTerms(Polynomial<T> a, Polynomial<T> b)
    {
        (a, b) = AlignIndeterminates(a, b);
        var degree = Math.Max(a.Degree, b.Degree);
        var ae = a.Terms.OrderBy(term => term.Signature(degree)).GetEnumerator();
        var an = ae.MoveNext();
        var be = b.Terms.OrderBy(term => term.Signature(degree)).GetEnumerator();
        var bn = be.MoveNext();

        while (an && bn)
        {
            switch (ae.Current.Signature(degree).CompareTo(be.Current.Signature(degree)))
            {
                case -1:
                    yield return (a.Indeterminates, ae.Current.Exponents, ae.Current.Coefficient, ring.Zero);
                    an = ae.MoveNext();
                    break;

                case 0:
                    yield return (a.Indeterminates, ae.Current.Exponents, ae.Current.Coefficient, be.Current.Coefficient);
                    an = ae.MoveNext();
                    bn = be.MoveNext();
                    break;

                case 1:
                    yield return (a.Indeterminates, be.Current.Exponents, ring.Zero, be.Current.Coefficient);
                    bn = be.MoveNext();
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        while (an)
        {
            yield return (a.Indeterminates, ae.Current.Exponents, ae.Current.Coefficient, ring.Zero);
            an = ae.MoveNext();
        }

        while (bn)
        {
            yield return (a.Indeterminates, be.Current.Exponents, ring.Zero, be.Current.Coefficient);
            bn = be.MoveNext();
        }
    }

    public override Polynomial<T> Add(Polynomial<T> a, Polynomial<T> b)
    {
        var aligned = AlignTerms(a, b).ToList();
        var ind = aligned.Count != 0 ? aligned.First().Ind : [];
        var terms = aligned.Select(t => (t.Exponents, C: ring.Add(t.A, t.B))).Where(t => !t.C!.Equals(ring.Zero))
            .Select(t => new Monomial<T>(ring, t.C, t.Exponents)).ToList();
        return new(this, ind, terms);
    }

    public override Polynomial<T> Multiply(Polynomial<T> a, Polynomial<T> b)
    {
        (a, b) = AlignIndeterminates(a, b);

        var m = from ta in a.Terms
                from tb in b.Terms
                let exp = ImmutableList.Create(ta.Exponents.Zip(tb.Exponents).Select(e => e.First + e.Second).ToArray())
                select new Monomial<T>(ring, ring.Multiply(ta.Coefficient, tb.Coefficient), exp);

        var ms = m.ToList();
        var degree = ms.Count != 0 ? ms.Max(t => t.Degree) : 0;
        var gs = m.GroupBy(t => t.Signature(degree)).ToList();

        var terms = gs
            .Select(g => new Monomial<T>(ring,
                g.Select(t => t.Coefficient).Aggregate(ring.Add), g.First().Exponents))
            .Where(t => !t.Coefficient.Equals(ring.Zero))
            .ToList();

        return new(this, a.Indeterminates, terms);
    }

    public Polynomial<T> Multiply(Polynomial<T> p, Monomial<T> m)
    {
        var terms = p.Terms.Select(t => new Monomial<T>(m.Ring, m.Ring.Multiply(m.Coefficient, t.Coefficient), 
            m.Exponents.Zip(t.Exponents).Select(e => e.First + e.Second).ToArray())).ToArray();

        return new(p.Ring, p.Indeterminates, terms);
    }

    public override Polynomial<T> Negate(Polynomial<T> x) => new(this, x.Indeterminates, x.Terms.Select(t => -t).ToList());

    public bool Equals(PolynomialRing<T> other) => other is not null;

    public override bool Equals(Ring<Polynomial<T>> other) => Equals(other as PolynomialRing<T>);

    public override bool Equals(object obj) => Equals(obj as Polynomial<T>);

    public override int GetHashCode() => HashCode.Combine(ring, nameof(PolynomialRing<T>));

    public override Polynomial<T> RandomSmallNonzeroElement(Random rand)
    {
        var count = (int)(2 - Math.Log(rand.NextDouble()));
        var terms = new List<Monomial<T>>();

        for (var i = 0; terms.Count < count; i++)
        {
            if (rand.Next(3) > 0)
            {
                ++PolynomialRingDepth.Depth;
                var c = CoefficientRing.RandomSmallNonzeroElement(rand);
                --PolynomialRingDepth.Depth;
                var m = new Monomial<T>(CoefficientRing, c, i);
                terms.Add(m);
            }
        }

        return Create([Indeterminate.DefaultInd[PolynomialRingDepth.Depth]], terms);
    }

    public override bool HasIntegerRepresentation => CoefficientRing.HasIntegerRepresentation;

    public override Polynomial<T> FromInteger(BigInteger n) => Create(CoefficientRing.FromInteger(n));

    public MultivariatePolynomialRing<T> AsMultivariate() => new(CoefficientRing);

    public IEnumerable<Polynomial<T>> All(params Indeterminate[] indeterminates) =>
        Enumerable.Range(1, int.MaxValue - 1).SelectMany(s => All(s, indeterminates));

    private IEnumerable<Polynomial<T>> All(int score, params Indeterminate[] indeterminates)
    {
        foreach (var coeff in WithScore(score))
        {
            var terms = coeff.Zip(AllExponents(indeterminates.Length)).Select(t => new Monomial<T>(CoefficientRing, t.First, t.Second));
            yield return Create(indeterminates, terms);
        }

        yield break;
    }

    public IEnumerable<(Polynomial<T>, Polynomial<T>)> AllPairs(params Indeterminate[] indeterminates) =>
        from score in Enumerable.Range(2, int.MaxValue - 1)
        from score1 in Enumerable.Range(1, score - 1)
        let score2 = score - score1
        from p in All(score1, indeterminates)
        from q in All(score2, indeterminates)
        select (p, q);

    private static IEnumerable<int[]> AllExponents(int count) =>
        Enumerable.Range(0, int.MaxValue).SelectMany(d => WithDegree(count, d));

    private static IEnumerable<int[]> WithDegree(int count, int degree)
    {
        if (count == 1)
        {
            yield return new[] { degree };
            yield break;
        }

        foreach (var c in Enumerable.Range(0, degree + 1))
        {
            var e = new int[count];
            e[^1] = c;

            foreach (var w in WithDegree(count - 1, degree - c))
            {
                Array.Copy(w, 0, e, 0, w.Length);
            }

            yield return e;
        }
    }

    private IEnumerable<T[]> WithScore(int score) =>
        from count in Enumerable.Range(0, score)
        from poly in WithTotalAndTermCount(score - count, count)
        select poly;

    private IEnumerable<T[]> WithTotalAndTermCount(int coeffTotal, int count) =>
        (
            from coeff in Enumerable.Range(1, coeffTotal - 1)
            from d in Enumerable.Range(0, count)
            from poly in WithTotalAndTermCount(coeffTotal - coeff, d)
            select ring.ElementsHaveSign ? [AddTerm(poly, count, coeff), AddTerm(poly, count, -coeff)] :
                new[] { AddTerm(poly, count, coeff) } 
        )
        .SelectMany(_ => _)
        .Concat(ring.ElementsHaveSign ? [Monomial(count, coeffTotal), Monomial(count, -coeffTotal)] : [Monomial(count, coeffTotal)]);

    private T[] Monomial(int index, int coeff)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
        var result = new T[index + 1];

        for (var i = 0; i < index; i++)
            result[i] = CoefficientRing.Zero;

        result[index] = ring.FromInteger(coeff);
        return result;
    }

    private T[] AddTerm(T[] poly, int index, int coeff)
    {
        ArgumentNullException.ThrowIfNull(poly, nameof(poly));
        ArgumentOutOfRangeException.ThrowIfLessThan(index, poly.Length, nameof(index));
        var result = new T[index + 1];
        Array.Copy(poly, result, poly.Length);

        for (var i = poly.Length; i < index; i++)
            result[i] = CoefficientRing.Zero;

        result[index] = ring.FromInteger(coeff);
        return result;
    }
}

internal class PolynomialRingDepth
{
    [ThreadStatic]
    public static int Depth;
}

public class MultivariatePolynomialRing<T> : PolynomialRing<T>
    where T : IEquatable<T>
{
    internal MultivariatePolynomialRing(Ring<T> coefficientRing) : base(coefficientRing)
    {
    }

    public override Polynomial<T> RandomSmallNonzeroElement(Random rand)
    {
        var ind = new[] { Indeterminate.DefaultInd[0], Indeterminate.DefaultInd[1] };
        var count = (int)(2 - Math.Log(rand.NextDouble()));
        var terms = new List<Monomial<T>>();
        var e = Enumerable.Range(0, 1000).SelectMany(d => Enumerable.Range(0, d + 1).ToList().Permute(rand).Select(i => new int[] { i, d - i }).ToArray());
        var ee = e.GetEnumerator();

        for (var i = 0; terms.Count < count; i++)
        {
            ee.MoveNext();

            if (rand.Next(3) > 0)
            {
                ++PolynomialRingDepth.Depth;
                var c = CoefficientRing.RandomSmallNonzeroElement(rand);
                --PolynomialRingDepth.Depth;
                var m = new Monomial<T>(CoefficientRing, c, ee.Current);
                terms.Add(m);
            }
        }

        return Create(ind, terms);
    }
}