
namespace Foundations.Functions;

using static Foundations.QuadConstants;

public static partial class Special
{
    /// <summary>
    /// From Appendix E of CHEBYSHEV SERIES EXPANSION OF INVERSE POLYNOMIALS
    /// by RICHARD J. MATHAR
    /// at https://arxiv.org/pdf/math/0403344.pdf
    /// </summary>
    public static Quad[] GetDigammaChebyshevCoefficients(int count)
    {
        QuadSum r = Quad.Zero;
        var K = new Quad[count + 1];

        for (var l = 0; true; l++)
        {
            var d = Binomial(-Quad.OneHalf, l);
            var old = r.Value;
            r.Add(d * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l + 2) - 1));
            if (old == r.Value) break;
        }

        K[0] = r.Value;
        r = Quad.Zero;

        for (var l = 1; true; l++)
        {
            var d = Binomial(-Quad.OneHalf, l);
            var old = r.Value;
            r.Add(d * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l + 1) - 1));
            if (old == r.Value) break;
        }

        K[1] = r.Value;
        r = Quad.Zero;

        for (var l = 1; true; l++)
        {
            var d = Binomial(-Quad.OneHalf, l);
            var old = r.Value;
            r.Add(d * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l) - 1));
            if (old == r.Value) break;
        }

        K[2] = 2 * r.Value - K[0];
        r = Quad.Zero;

        for (var l = 2; true; l++)
        {
            var d = Binomial(-Quad.OneHalf, l) + Binomial(Quad.OneHalf, l);
            var old = r.Value;
            r.Add(d * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l - 1) - 1));
            if (old == r.Value) break;
        }

        K[3] = 2 * r.Value - K[1];
        r.Value = 0;

        for (var n = 2; n < K.Length - 2; n++)
        {
            for (var l = (n + 3) / 2; true; l++)
            {
                QuadSum d = Quad.Zero;

                for (var s = 0; s <= n + 1; s++)
                {
                    d.Add(Binomial(n + Quad.One, s) * Binomial((s - 1) / Quad.Two, l));
                }

                var old = r.Value;
                r.Add(d.Value * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l - n) - 1));
                if (old == r.Value) break;
            }

            K[n + 2] = 2 * r.Value - K[n];
            r.Value = 0;
        }

        var C = new Quad[K.Length - 1];
        C[0] = 1 - QuadConstants.γ - K[1];

        for (var n = 1; n < K.Length - 1; n++)
        {
            C[n] = (2 * (n & 1) - 1) * (K[n - 1] + K[n + 1]);
        }

        return C;
    }

    public static Quad Digamma(Quad x)
    {
        // Chebyshev polynomial representation of ψ(x + 2) requires 1 ≤ x ≤ 3

        if (x < .5)
            return Digamma(1 - x) - π / Quad.Tan(π * x);

        if (x < 1)
            return Digamma(1 + x) - 1 / x;

        if (x > 3)
            return .5 * (Digamma(x / 2) + Digamma((x + 1) / 2)) + Ln2;

        // Special values

        if (x == 1)
            return -γ;

        if (x == 2)
            return 1 - γ;

        if (x == 3)
            return 1.5 - γ;
            
        x -= 2;
        Quad t0 = 1;
        var t1 = x;
        var sum = new QuadSum(0.0);

        foreach (var k in DigammaChebyshevCoefficients)
        {
            sum.Add(k * t0);
            (t0, t1) = (t1, 2 * x * t1 - t0);
        }

        return sum.Value;
    }

    public static double Digamma(double x) =>
        (double)Digamma((Quad)x);
}
