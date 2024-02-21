
namespace Foundations.Algebra;

public class ExtensionGF : FiniteField
{
    public readonly PrimeGF BaseField;

    private readonly FFValue zero;
    private readonly FFValue one;

    private readonly FFValue[] modulus;

    private readonly FFValue[][] powers;

    public override FFValue Zero => zero;

    public override FFValue One => one;

    public ImmutableList<FFValue> Modulus => ImmutableList.Create(modulus);

    private FFValue[] GetPoly(long n)
    {
        var p = new FFValue[Degree];

        for (var i = 0; n != 0 && i < Degree; i++)
        {
            p[i] = BaseField.Element(n % BaseField.Prime);
            n /= BaseField.Prime;
        }

        Trim(ref p);
        return p;
    }

    private FFValue FromPoly(FFValue[] p)
    {
        if (p.Length == 0)
            return new(this, 0);

        var n = p[^1].Value;

        for (var i = p.Length - 2; i >= 0; i--)
        {
            n = n * BaseField.Prime + p[i].Value;
        }

        return new(this, n);
    }

    private static FFValue[] Multiply(FFValue[] a, FFValue[] b)
    {
        var field = a[0].Field;
        var p = field.ZeroPoly(a.Length + b.Length);

        for (var i = 0; i < p.Length; i++)
            p[i] = field.Zero;

        for (var i = 0; i < a.Length; i++)
        {
            for (var j = 0; j < b.Length; j++)
            {
                p[i + j] += a[i] * b[j];
            }
        }

        Trim(ref p);
        return p;
    }

    private static (FFValue[] Quotient, FFValue[] Remainder) DivRem(FFValue[] n, FFValue[] d)
    {
        if (n.Length < d.Length)
        {
            return (Array.Empty<FFValue>(), n);
        }

        var r = (FFValue[])n.Clone();
        var q = new FFValue[n.Length - d.Length + 1];

        for (var i = n.Length - 1; i >= d.Length - 1; i--)
        {
            var k = q[i - d.Length + 1] = r[i] / d[^1];
            r[i] = n[0].Field.Zero;

            for (var j = 0; j < d.Length - 1; j++)
            {
                r[i - d.Length + 1 + j] -= d[j] * k;
            }
        }

        Trim(ref q);
        Trim(ref r);
        return (q, r);
    }

    private static FFValue[] Mod(FFValue[] n, FFValue[] d)
    {
        var (_, rem) = DivRem(n, d);
        return rem;
    }

    private static FFValue[] GCD(FFValue[] a, FFValue[] b)
    {
        while (b.Length > 0)
        {
            var (_, r) = DivRem(a, b);
            (a, b) = (b, r);
        }

        return a;
    }

    private static FFValue[] PowMod(FFValue[] p, long n, FFValue[] modulus)
    {
        var field = modulus[0].Field;
        if (n == 0) return [field.One];
        if (n == 1) return Mod(p, modulus);
        if ((n & 1) == 1) return Mod(Multiply(p, PowMod(p, n - 1, modulus)), modulus);
        var sqrt = PowMod(p, n / 2, modulus);
        return Mod(Multiply(sqrt, sqrt), modulus);
    }

    private static void Trim(ref FFValue[] arr)
    {
        var m = arr.Length - 1;
        while (m >= 0 && arr[m].Value == 0) --m;
        if (m < arr.Length - 1) Array.Resize(ref arr, m + 1);
    }

    private static void SubtractX(PrimeGF field, ref FFValue[] f)
    {
        if (f.Length > 1)
        {
            f[1] -= field.One;
        }
        else
        {
            Array.Resize(ref f, 2);
            f[1] = -field.Element(1);
            if (f[0].Field is null) f[0] = field.Zero;
        }

        Trim(ref f);
    }

    private static bool RabinTest(FFValue[] f)
    {
        var field = (PrimeGF)f[0].Field;
        var n = f.Length - 1;
        var ns = Sequences.PrimesInt32().TakeWhile(p => p * p <= n).Where(p => (n % p) == 0).Select(p => n / p).ToList();
        if (Primes.Contains(n)) ns.Add(1);

        foreach (var ni in ns)
        {
            var qn = Pow(field.Prime, ni);
            var mod = PowMod(new[] { field.Zero, field.One }, qn, f);
            SubtractX(field, ref mod);
            var g = GCD(f, mod);

            if (g.Length != 1)
            {
                return false;
            }
        }

        {
            var g = PowMod(new[] { field.Zero, field.One }, Pow(field.Prime, n), f);
            SubtractX(field, ref g);
            return g.Length == 0;
        }
    }

    public ExtensionGF(int characteristic, int degree) : this((PrimeGF)OfOrder(characteristic), Irreducibles((PrimeGF)OfOrder(characteristic), degree).First())
    {
    }

    internal ExtensionGF(PrimeGF baseField, FFValue[] modulus) : base(modulus.Length - 1, Pow(baseField.Order, modulus.Length - 1))
    {
        zero = new(this, 0);
        one = new(this, 1);
        BaseField = baseField;
        this.modulus = modulus;
        var degree = modulus.Length - 1;
        var ideal = modulus.Take(degree).Select(c => -c).ToArray();
        var m = (FFValue[])ideal.Clone();
        powers = new FFValue[2 * degree][];
        
        for (var i = degree; i < 2 * degree; i++)
        {
            powers[i] = (FFValue[])m.Clone();
            var lc = m[^1];
            Array.Copy(m, 0, m, 1, m.Length - 1);
            m[0] = BaseField.Zero;

            if (lc.Value != 0)
            {
                for (var j = 0; j < degree; j++)
                    m[j] += lc * ideal[j];
            }
        }
    }

    public static string PolyStr(IList<FFValue> p)
    {
        if (p.Count == 0)
        {
            return "0";
        }

        var s = new StringBuilder();

        for (var power = 0; power < p.Count; power++)
        {
            var coeff = p[power].Value;

            if (coeff != 0)
            {
                if (s.Length > 0)
                {
                    s.Append(' ');
                }

                if (power == 0)
                {
                    s.Append(coeff.ToString().Replace("-", "−"));
                }
                else
                {
                    if (s.Length > 0)
                    {
                        s.Append("+ ");
                    }

                    var abs = Math.Abs(coeff);

                    if (abs != 1)
                    {
                        s.Append(abs);
                    }

                    s.Append('α');

                    if (power > 1)
                    {
                        s.Append(power.ToSuperscript());
                    }
                }
            }
        }

        return s.ToString();
    }

    public override string ToString() =>
        $"𝔽{Order.ToSubscript()} = GF({BaseField.Prime}{Degree.ToSuperscript()}) ≅ 𝔽{BaseField.Prime.ToSubscript()}[α]/({PolyStr(modulus)})";

    public override FFValue Add(FFValue a, FFValue b)
    {
        var ap = GetPoly(a.Checked(this));
        var bp = GetPoly(b.Checked(this));

        if (ap.Length < bp.Length)
        {
            var l = ap.Length;
            Array.Resize(ref ap, bp.Length);

            for (var i = l; i < bp.Length; i++)
            {
                ap[i] = BaseField.Zero;
            }
        }

        if (bp.Length < ap.Length)
        {
            var l = bp.Length;
            Array.Resize(ref bp, ap.Length);

            for (var i = l; i < ap.Length; i++)
            {
                bp[i] = BaseField.Zero;
            }
        }

        for (var i = 0; i < ap.Length; i++)
        {
            ap[i] += bp[i];
        }

        Trim(ref ap);
        return FromPoly(ap);
    }

    public override FFValue Negate(FFValue x)
    {
        var p = GetPoly(x.Checked(this));

        for (var i = 0; i < p.Length; i++)
        {
            p[i] = -p[i];
        }

        return FromPoly(p);
    }

    public override FFValue Invert(FFValue x)
    {
        var b = GetPoly(x.Value);
        var a = modulus;
        var s = Zero;
        var t = One;

        while (b.Length > 1)
        {
            var (div, rem) = DivRem(a, b);
            var dp = FromPoly(div);
            (a, b, s, t) = (b, rem, t, Subtract(s, Multiply(t, dp)));
        }

        if (b.Length > 0 && b[0].Value != 1)
        {
            var tp = GetPoly(t.Value);

            for (var i = 0; i < tp.Length; i++)
            {
                tp[i] /= b[0];
            }

            t = FromPoly(tp);
        }

        return t;
    }

    public override FFValue Multiply(FFValue a, FFValue b)
    {
        if (a.Field.Order != Order || b.Field.Order != Order)
            throw new ArgumentException("Arguments must be from correct field.");

        if (a == Zero || b == Zero)
            return Zero;

        if (a == One)
            return b;

        if (b == One)
            return a;

        var ap = GetPoly(a.Value);
        var bp = GetPoly(b.Value);
        var result = Multiply(ap, bp);

        for (var i = Degree; i < result.Length; i++)
        {
            var c = result[i];

            if (c.Value == 0)
                continue;

            for (var j = 0; j < Degree; j++)
                result[j] += c * powers[i][j];
        }

        Array.Resize(ref result, Degree);
        Trim(ref result);
        return FromPoly(result);
    }

    internal override string ValueStr(FFValue value) => PolyStr(GetPoly(value.Value));

    internal static IEnumerable<FFValue[]> Candidates(PrimeGF field, int degree)
    {
        var n = degree;
        var p = field.Prime;

        // Binomials.
        if (p != 2)
        {
            for (var i = 1; i < p; i++)
            {
                var g = field.ZeroPoly(n);
                g[n] = field.One;
                g[0] = field.Element(i);
                yield return g;
            }
        }

        // Trinomials.
        for (var i = 1; i < n; i++)
            for (var c = 1; c < p; c++)
                for (var d = 1; d < p; d++)
                {
                    var g = field.ZeroPoly(n);
                    g[n] = field.One;
                    g[0] = field.Element(c);
                    g[i] = field.Element(d);
                    yield return g;
                }

        // Pentanomials.
        for (var i = 3; i < n; i++)
            for (var j = 2; j < i; j++)
                for (var k = 1; k < j; k++)
                    for (var c = 1; c < p; c++)
                        for (var d = 1; d < p; d++)
                            for (var e = 1; e < p; e++)
                                for (var f = 1; f < p; f++)
                                {
                                    var g = field.ZeroPoly(n);
                                    g[n] = field.One;
                                    g[0] = field.Element(c);
                                    g[i] = field.Element(d);
                                    g[j] = field.Element(e);
                                    g[k] = field.Element(f);
                                    yield return g;
                                }
    }

    public static IEnumerable<FFValue[]> Irreducibles(PrimeGF field, int degree) => 
        Candidates(field, degree).Where(RabinTest);

    public override bool Equals(Ring<FFValue> other) => other is ExtensionGF f && f.Order == Order;

    public override bool HasIntegerRepresentation => false;
}
