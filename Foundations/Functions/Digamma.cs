
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
        Sum<Quad> r = Quad.Zero;
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
                Sum<Quad> d = Quad.Zero;

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
        if (x < (Double).5)
            return Digamma(1 - x) - Constants.π / Math.Tan(Constants.π * x);

        // Special values

        if (x == 1)
            return -Constants.γ;

        if (x == 2)
            return 1 - Constants.γ;

        if (x == 3)
            return (Double)1.5 - Constants.γ;

        // Shift to asymptotic range.

        if (x < 20)
        {
            var N = 20 - (int)x;
            Sum<Double> s = new(Digamma(x + N));

            for (var k = 0; k < N; k++)
            {
                s -= 1 / (x + k);
            }

            return s.Value;
        }

        Sum<Double> sum = new(Math.Log(x) - 1 / (2 * x));
        var xx = 1 / (x * x);
        var xp = xx;
        sum -= xp * Sequences.BernoulliTableOverN[2];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[4];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[6];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[8];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[10];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[12];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[14];
        return sum.Value;
    }

    public static Complex Digamma(Complex x)
    {
        if (x.Real < (Double).5)
            return Digamma(1 - x) - Constants.π / Complex.Tan(Constants.π * x);

        // Special values

        if (x == 1)
            return -Constants.γ;

        if (x == 2)
            return 1 - Constants.γ;

        if (x == 3)
            return (Double)1.5 - Constants.γ;

        // Shift to asymptotic range.

        if (x.Abs() < 40)
        {
            var N = 40 - (int)x.Real;
            Sum<Complex> s = new(Digamma(x + N));

            for (var k = 0; k < N; k++)
            {
                s -= 1 / (x + k);
            }

            return s.Value;
        }

        Sum<Complex> sum = new(Complex.Log(x) - 1 / (2 * x));
        var xx = 1 / (x * x);
        var xp = xx;
        sum -= xp * Sequences.BernoulliTableOverN[2];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[4];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[6];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[8];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[10];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[12];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverN[14];
        return sum.Value;
    }

    public static Quad Digamma(Quad x)
    {
        if (x < (Quad).5)
            return Digamma(1 - x) - QuadConstants.π / Quad.Tan(QuadConstants.π * x);

        // Special values

        if (x == 1)
            return -QuadConstants.γ;

        if (x == 2)
            return 1 - QuadConstants.γ;

        if (x == 3)
            return (Quad)1.5 - QuadConstants.γ;

        // Shift to asymptotic range.

        if (x < 250)
        {
            var N = 250 - (int)x;
            Sum<Quad> s = new(Digamma(x + N));

            for (var k = 0; k < N; k++)
            {
                s -= 1 / (x + k);
            }

            return s.Value;
        }

        Sum<Quad> sum = new(Quad.Log(x) - 1 / (2 * x));
        var xx = 1 / (x * x);
        var xp = xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[2];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[4];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[6];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[8];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[10];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[12];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[14];
        return sum.Value;
    }

    public static ComplexQuad Digamma(ComplexQuad x)
    {
        if (x.Real < (Quad).5)
            return Digamma(1 - x) - QuadConstants.π / ComplexQuad.Tan(QuadConstants.π * x);

        // Special values

        if (x == 1)
            return -QuadConstants.γ;

        if (x == 2)
            return 1 - QuadConstants.γ;

        if (x == 3)
            return (Quad)1.5 - QuadConstants.γ;

        // Shift to asymptotic range.

        if (x.Abs() < 250)
        {
            var N = 250 - (int)x.Real;
            Sum<ComplexQuad> s = new(Digamma(x + N));

            for (var k = 0; k < N; k++)
            {
                s -= 1 / (x + k);
            }

            return s.Value;
        }

        Sum<ComplexQuad> sum = new(ComplexQuad.Log(x) - 1 / (2 * x));
        var xx = 1 / (x * x);
        var xp = xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[2];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[4];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[6];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[8];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[10];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[12];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNQuad[14];
        return sum.Value;
    }

    public static Decimal Digamma(Decimal x)
    {
        if (x < (Decimal).5)
            return Digamma(1 - x) - DecimalConstants.π / MathM.Tan(DecimalConstants.π * x);

        // Special values

        if (x == 1)
            return -DecimalConstants.γ;

        if (x == 2)
            return 1 - DecimalConstants.γ;

        if (x == 3)
            return (Decimal)1.5 - DecimalConstants.γ;

        // Shift to asymptotic range.

        if (x < 40)
        {
            var N = 40 - (int)x;
            Sum<Decimal> s = new(Digamma(x + N));

            for (var k = 0; k < N; k++)
            {
                s -= 1 / (x + k);
            }

            return s.Value;
        }

        Sum<Decimal> sum = new(MathM.Log(x) - 1 / (2 * x));
        var xx = 1 / (x * x);
        var xp = xx;
        sum -= xp * Sequences.BernoulliTableOverNDecimal[2];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNDecimal[4];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNDecimal[6];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNDecimal[8];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNDecimal[10];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNDecimal[12];
        xp *= xx;
        sum -= xp * Sequences.BernoulliTableOverNDecimal[14];
        return sum.Value;
    }
}
