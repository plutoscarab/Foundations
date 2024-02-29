
namespace Foundations.Functions;

public static partial class Special
{
    public static ComplexQuad Zeta(ComplexQuad s)
    {
        var fs = Quad.Floor(s.Re);

        if (s == s.Re && s == fs && s.Re < 0)
        {
            if (s.Re == 2 * Quad.Floor(s.Re / 2))
                return (ComplexQuad)0;

            var k = -(int)fs;
            return (ComplexQuad)(Quad)((1 - 2 * (k & 1)) * Sequences.BernoulliNumbers().ElementAt(k + 1) / (k + 1));
        }
            
        var n = 100 + 2 * (int)ComplexQuad.Abs(s.Re) + 2 * (int)ComplexQuad.Abs(s.Im);

        if (s.Re < .5)
        {
            var gamma = Gamma(s);
            var tpp = ComplexQuad.Pow(2 * QuadConstants.PI, -s);
            var cos = ComplexQuad.Cos(QuadConstants.PI * s / 2);
            var chi = 2 * tpp * cos * gamma;
            return Zeta(1 - s) / chi;
        }
        
        var ek = new ComplexQuad[n + 1];
        ek[n] = 1;
        ComplexQuad e = 1;

        for (var j = n; j > 1; j--)
        {
            e = e * j / (n - j + 1);
            ek[j - 1] = ek[j] + e;
        }

        Sum<ComplexQuad> sum = new(0);

        for (var k = n + 1; k <= 2 * n; k++)
        {
            var term = (2 * (k & 1) - 1) * ek[k - n] * ComplexQuad.Exp(-s * ComplexQuad.Log((ComplexQuad)k));
            sum += term;
        }

        sum *= ComplexQuad.Pow((ComplexQuad)2, -n);

        for (var k = 1; k <= n; k++)
        {
            var term = (2 * (k & 1) - 1) * ComplexQuad.Exp(-s * ComplexQuad.Log((ComplexQuad)k));
            sum += term;
        }

        sum /= 1 - ComplexQuad.Pow(2, 1 - s);
        return (ComplexQuad)sum;
    }

    public static Complex Zeta(Complex s)
    {
        var fs = Math.Floor(s.Real);

        if (s == s.Real && s == fs && s.Real < 0)
        {
            if (s.Real == 2 * Math.Floor(s.Real / 2))
                return (Complex)0;

            var k = -(int)fs;
            return (Complex)(double)((1 - 2 * (k & 1)) * Sequences.BernoulliNumbers().ElementAt(k + 1) / (k + 1));
        }
            
        var n = 100 + 2 * (int)Complex.Abs(s.Real) + 2 * (int)Complex.Abs(s.Imaginary);

        if (s.Real < .5)
        {
            var gamma = Gamma(s);
            var tpp = Complex.Pow(2 * Math.PI, -s);
            var cos = Complex.Cos(Math.PI * s / 2);
            var chi = 2 * tpp * cos * gamma;
            return Zeta(1 - s) / chi;
        }
        
        var ek = new Complex[n + 1];
        ek[n] = 1;
        Complex e = 1;

        for (var j = n; j > 1; j--)
        {
            e = e * j / (n - j + 1);
            ek[j - 1] = ek[j] + e;
        }

        Sum<Complex> sum = new(0);

        for (var k = n + 1; k <= 2 * n; k++)
        {
            var term = (2 * (k & 1) - 1) * ek[k - n] * Complex.Exp(-s * Complex.Log((Complex)k));
            sum += term;
        }

        sum *= Complex.Pow((Complex)2, -n);

        for (var k = 1; k <= n; k++)
        {
            var term = (2 * (k & 1) - 1) * Complex.Exp(-s * Complex.Log((Complex)k));
            sum += term;
        }

        sum /= 1 - Complex.Pow(2, 1 - s);
        return (Complex)sum;
    }

    public static Quad Zeta(Quad s)
    {
        var fs = Quad.Floor(s);

        if (s == fs && s < 0)
        {
            if (s == 2 * Quad.Floor(s / 2))
                return (Quad)0;

            var k = -(int)fs;
            return (Quad)(Quad)((1 - 2 * (k & 1)) * Sequences.BernoulliNumbers().ElementAt(k + 1) / (k + 1));
        }

        var n = 50 + (int)Quad.Abs(s);

        if (s < .5)
            return Zeta(1 - s) / (2 * Quad.Pow(2 * QuadConstants.PI, -s) * Quad.Cos(QuadConstants.PI * s / 2) * Gamma(s));
        
        var ek = new Quad[n + 1];
        ek[n] = 1;
        Quad e = 1;

        for (var j = n; j > 1; j--)
        {
            e = e * j / (n - j + 1);
            ek[j - 1] = ek[j] + e;
        }

        Sum<Quad> sum = new(0);

        for (var k = n + 1; k <= 2 * n; k++)
        {
            var term = (2 * (k & 1) - 1) * ek[k - n] * Quad.Exp(-s * Quad.Log((Quad)k));
            sum += term;
        }

        sum *= Quad.Pow((Quad)2, -n);

        for (var k = 1; k <= n; k++)
        {
            var term = (2 * (k & 1) - 1) * Quad.Exp(-s * Quad.Log((Quad)k));
            sum += term;
        }

        sum /= 1 - Quad.Pow(2, 1 - s);
        return (Quad)sum;
    }

    public static double Zeta(double s)
    {
        var fs = Math.Floor(s);

        if (s == fs && s < 0)
        {
            if (s == 2 * Math.Floor(s / 2))
                return (double)0;

            var k = -(int)fs;
            return (double)(double)((1 - 2 * (k & 1)) * Sequences.BernoulliNumbers().ElementAt(k + 1) / (k + 1));
        }

        var n = 50 + (int)Math.Abs(s);

        if (s < .5)
            return Zeta(1 - s) / (2 * Math.Pow(2 * Math.PI, -s) * Math.Cos(Math.PI * s / 2) * Gamma(s));
        
        var ek = new double[n + 1];
        ek[n] = 1;
        double e = 1;

        for (var j = n; j > 1; j--)
        {
            e = e * j / (n - j + 1);
            ek[j - 1] = ek[j] + e;
        }

        Sum<Double> sum = new(0);

        for (var k = n + 1; k <= 2 * n; k++)
        {
            var term = (2 * (k & 1) - 1) * ek[k - n] * Math.Exp(-s * Math.Log((double)k));
            sum += term;
        }

        sum *= Math.Pow((double)2, -n);

        for (var k = 1; k <= n; k++)
        {
            var term = (2 * (k & 1) - 1) * Math.Exp(-s * Math.Log((double)k));
            sum += term;
        }

        sum /= 1 - Math.Pow(2, 1 - s);
        return (double)sum;
    }

    public static float Zeta(float s)
    {
        var fs = MathF.Floor(s);

        if (s == fs && s < 0)
        {
            if (s == 2 * MathF.Floor(s / 2))
                return (float)0;

            var k = -(int)fs;
            return (float)(float)((1 - 2 * (k & 1)) * Sequences.BernoulliNumbers().ElementAt(k + 1) / (k + 1));
        }

        var n = 50 + (int)MathF.Abs(s);

        if (s < .5)
            return Zeta(1 - s) / (2 * MathF.Pow(2 * MathF.PI, -s) * MathF.Cos(MathF.PI * s / 2) * Gamma(s));
        
        var ek = new float[n + 1];
        ek[n] = 1;
        float e = 1;

        for (var j = n; j > 1; j--)
        {
            e = e * j / (n - j + 1);
            ek[j - 1] = ek[j] + e;
        }

        Sum<Single> sum = new(0);

        for (var k = n + 1; k <= 2 * n; k++)
        {
            var term = (2 * (k & 1) - 1) * ek[k - n] * MathF.Exp(-s * MathF.Log((float)k));
            sum += term;
        }

        sum *= MathF.Pow((float)2, -n);

        for (var k = 1; k <= n; k++)
        {
            var term = (2 * (k & 1) - 1) * MathF.Exp(-s * MathF.Log((float)k));
            sum += term;
        }

        sum /= 1 - MathF.Pow(2, 1 - s);
        return (float)sum;
    }

}

