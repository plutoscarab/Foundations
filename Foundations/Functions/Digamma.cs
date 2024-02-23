
namespace Foundations.Functions;

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
            r += d * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l + 2) - 1);
            if (old == r.Value) break;
        }

        K[0] = r.Value;
        r = Quad.Zero;

        for (var l = 1; true; l++)
        {
            var d = Binomial(-Quad.OneHalf, l);
            var old = r.Value;
            r += d * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l + 1) - 1);
            if (old == r.Value) break;
        }

        K[1] = r.Value;
        r = Quad.Zero;

        for (var l = 1; true; l++)
        {
            var d = Binomial(-Quad.OneHalf, l);
            var old = r.Value;
            r += d * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l) - 1);
            if (old == r.Value) break;
        }

        K[2] = 2 * r.Value - K[0];
        r = Quad.Zero;

        for (var l = 2; true; l++)
        {
            var d = Binomial(-Quad.OneHalf, l) + Binomial(Quad.OneHalf, l);
            var old = r.Value;
            r += d * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l - 1) - 1);
            if (old == r.Value) break;
        }

        K[3] = 2 * r.Value - K[1];
        r = Quad.Zero;

        for (var n = 2; n < K.Length - 2; n++)
        {
            for (var l = (n + 3) / 2; true; l++)
            {
                QuadSum d = Quad.Zero;

                for (var s = 0; s <= n + 1; s++)
                {
                    d += Binomial(n + Quad.One, s) * Binomial((s - 1) / Quad.Two, l);
                }

                var old = r.Value;
                r += d.Value * (1 - 2 * (l & 1)) * (Zeta(Quad.Two * l - n) - 1);
                if (old == r.Value) break;
            }

            K[n + 2] = 2 * r.Value - K[n];
            r = Quad.Zero;
        }

        var C = new Quad[K.Length - 1];
        C[0] = 1 - QuadConstants.γ - K[1];

        for (var n = 1; n < K.Length - 1; n++)
        {
            C[n] = (2 * (n & 1) - 1) * (K[n - 1] + K[n + 1]);
        }

        return C;
    }

    public static Double Digamma(Double x)
    {
        if (x < .5)
            return Digamma(1 - x) - Constants.π / Math.Tan(Constants.π * x);

        // Special values

        if (x == 1)
            return -Constants.γ;

        if (x == 2)
            return 1 - Constants.γ;

        if (x == 3)
            return 1.5 - Constants.γ;

        // Shift to asymptotic range.

        if (x < 20)
        {
            var N = 20 - (int)x;
            DoubleSum s = new(Digamma(x + N));

            for (var k = 0; k < N; k++)
            {
                s -= 1 / (x + k);
            }

            return s.Value;
        }

        DoubleSum sum = new(x.Log() - 1 / (2 * x));
        var xx = 1 / (x * x);
        var xp = xx;
        sum -= xp / 12;
        xp *= xx;
        sum += xp / 120;
        xp *= xx;
        sum -= xp / 252;
        xp *= xx;
        sum += xp / 240;
        xp *= xx;
        sum -= xp / 132;
        xp *= xx;
        sum += xp * 691 / 32760;
        xp *= xx;
        sum -= xp / 12;
        return sum.Value;
    }

    public static Complex Digamma(Complex x)
    {
        if (x.Real < .5)
            return Digamma(1 - x) - Constants.π / Complex.Tan(Constants.π * x);

        // Special values

        if (x == 1)
            return -Constants.γ;

        if (x == 2)
            return 1 - Constants.γ;

        if (x == 3)
            return 1.5 - Constants.γ;

        // Shift to asymptotic range.

        if (x.Abs() < 40)
        {
            var N = 40 - (int)x.Real;
            ComplexSum s = new(Digamma(x + N));

            for (var k = 0; k < N; k++)
            {
                s -= 1 / (x + k);
            }

            return s.Value;
        }

        ComplexSum sum = new(x.Log() - 1 / (2 * x));
        var xx = 1 / (x * x);
        var xp = xx;
        sum -= xp / 12;
        xp *= xx;
        sum += xp / 120;
        xp *= xx;
        sum -= xp / 252;
        xp *= xx;
        sum += xp / 240;
        xp *= xx;
        sum -= xp / 132;
        xp *= xx;
        sum += xp * 691 / 32760;
        xp *= xx;
        sum -= xp / 12;
        return sum.Value;
    }

    public static Quad Digamma(Quad x)
    {
        if (x < .5)
            return Digamma(1 - x) - QuadConstants.π / Quad.Tan(QuadConstants.π * x);

        // Special values

        if (x == 1)
            return -QuadConstants.γ;

        if (x == 2)
            return 1 - QuadConstants.γ;

        if (x == 3)
            return 1.5 - QuadConstants.γ;

        // Shift to asymptotic range.

        if (x < 250)
        {
            var N = 250 - (int)x;
            QuadSum s = new(Digamma(x + N));

            for (var k = 0; k < N; k++)
            {
                s -= 1 / (x + k);
            }

            return s.Value;
        }

        QuadSum sum = new(x.Log() - 1 / (2 * x));
        var xx = 1 / (x * x);
        var xp = xx;
        sum -= xp / 12;
        xp *= xx;
        sum += xp / 120;
        xp *= xx;
        sum -= xp / 252;
        xp *= xx;
        sum += xp / 240;
        xp *= xx;
        sum -= xp / 132;
        xp *= xx;
        sum += xp * 691 / 32760;
        xp *= xx;
        sum -= xp / 12;
        return sum.Value;
    }

    public static ComplexQuad Digamma(ComplexQuad x)
    {
        if (x.Real < .5)
            return Digamma(1 - x) - QuadConstants.π / ComplexQuad.Tan(QuadConstants.π * x);

        // Special values

        if (x == 1)
            return -QuadConstants.γ;

        if (x == 2)
            return 1 - QuadConstants.γ;

        if (x == 3)
            return 1.5 - QuadConstants.γ;

        // Shift to asymptotic range.

        if (x.Abs() < 250)
        {
            var N = 250 - (int)x.Real;
            ComplexQuadSum s = new(Digamma(x + N));

            for (var k = 0; k < N; k++)
            {
                s -= 1 / (x + k);
            }

            return s.Value;
        }

        ComplexQuadSum sum = new(x.Log() - 1 / (2 * x));
        var xx = 1 / (x * x);
        var xp = xx;
        sum -= xp / 12;
        xp *= xx;
        sum += xp / 120;
        xp *= xx;
        sum -= xp / 252;
        xp *= xx;
        sum += xp / 240;
        xp *= xx;
        sum -= xp / 132;
        xp *= xx;
        sum += xp * 691 / 32760;
        xp *= xx;
        sum -= xp / 12;
        return sum.Value;
    }
}
