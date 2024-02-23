
using System.Formats.Asn1;

namespace Foundations.Functions;

public static partial class Special
{
    /// <summary>
    /// Calculate the binomial coefficient.
    /// </summary>
    public static ComplexQuad Binomial(ComplexQuad n, ComplexQuad r)
    {
        var ni = ComplexQuad.Round(n);
        var ri = ComplexQuad.Round(r);

        if (n.Im == 0 && n == ni && r.Im == 0 && r == ri)
        {
            if (r.Re < 0 || r.Re > n.Re)
                return 0;
        }

        var gn = Gamma(n + 1);
        var gr = Gamma(r + 1);
        var gnr = Gamma(n - r + 1);

        if (ComplexQuad.IsNaN(gr))
            if (ComplexQuad.IsNaN(gnr))
                return ComplexQuad.Zero;
            else
                return ComplexQuad.NaN;
        else if (ComplexQuad.IsNaN(gnr))
            return ComplexQuad.Zero;

        var c = gn / (gr * gnr);

        if (ComplexQuad.IsInteger(n) && ComplexQuad.IsInteger(r))
            c = c.Round();

        return c;
    }

    public static Complex Binomial(Complex n, Complex r)
    {
        return (Complex)Binomial((ComplexQuad)n, (ComplexQuad)r);
    }

    public static Quad Binomial(Quad n, Quad r)
    {
        return Binomial((ComplexQuad)n, (ComplexQuad)r).Re;
    }

    public static double Binomial(double n, double r)
    {
        return (double)Binomial((Quad)n, (Quad)r);
    }

    public static float Binomial(float n, float r)
    {
        return (float)Binomial((double)n, (double)r);
    }

    public static Nat Binomial(Nat n, Nat k)
    {
        if (n < k) return Nat.Zero;
        if (k == 0) return Nat.One;
        k = Nat.Min(k, n - k);
        if (k == 1) return n;
        Nat i = 0, p = 1;

        while (true)
        {
            if (i >= k)
                return p;

            (i, p) = (i + 1, (n - i) * p / (i + 1));
        }
    }

    public static BigInteger Binomial(BigInteger n, BigInteger k)
    {
        if (n < 0) return (n.IsEven ? 1 : -1) * Binomial(-n, k);
        if (n < k) return BigInteger.Zero;
        if (k == 0) return BigInteger.One;
        k = BigInteger.Min(k, n - k);
        if (k == 1) return n;
        BigInteger i = 0, p = 1;

        while (true)
        {
            if (i >= k)
                return p;

            (i, p) = (i + 1, (n - i) * p / (i + 1));
        }
    }

    public static long Binomial(long n, long k)
    {
        if (n < 0) return (1 - 2 * (n & 1)) * Binomial(-n, k);
        if (n < k) return 0;
        if (k == 0) return 0;
        k = Math.Min(k, n - k);
        if (k == 1) return n;
        long i = 0, p = 1;

        while (true)
        {
            if (i >= k)
                return p;

            (i, p) = checked((i + 1, (n - i) * p / (i + 1)));
        }
    }

    public static int Binomial(int n, int k)
    {
        if (n < 0) return (1 - 2 * (n & 1)) * Binomial(-n, k);
        if (n < k) return 0;
        k = Math.Min(k, n - k);
        if (k == 0) return 0;
        if (k == 1) return n;
        int i = 0, p = 1;

        while (true)
        {
            if (i >= k)
                return p;

            (i, p) = checked((i + 1, (n - i) * p / (i + 1)));
        }
    }
}