
namespace Foundations.Functions;

public static partial class Special
{
    // Lanczos coefficients
    static readonly Quad p0 = new(false, 2, 0xA06C98FFB1382CB2, 0xBE520FD70E4D7564);
    static readonly Quad p1 = new(false, 30, 0xA326DDA46DCFE4A1, 0xE76554111473B031);
    static readonly Quad p2 = new(true, 33, 0x8281BBD0EA668635, 0x3CE767DB00F34B54);
    static readonly Quad p3 = new(false, 34, 0xB9C9DEA0D9B863C2, 0x8DAE04BA924CC52F);
    static readonly Quad p4 = new(true, 35, 0x9B399D8491486539, 0x33980B70DEE1BCEA);
    static readonly Quad p5 = new(false, 35, 0xA91BCCCF30BEE1C2, 0x516030844F14CFE8);
    static readonly Quad p6 = new(true, 34, 0xFC705AF24827B24C, 0xE08CF667D4813249);
    static readonly Quad p7 = new(false, 34, 0x8410838F64C1DB62, 0xFB4EC8547570E4E0);
    static readonly Quad p8 = new(true, 32, 0xC2EAA1E4695D4B22, 0xC1CD5F633E6A108C);
    static readonly Quad p9 = new(false, 30, 0xC99151248C6E1118, 0xF57EFFE1A2AB4C16);
    static readonly Quad p10 = new(true, 28, 0x8F59967D396ADE32, 0x22CA4662E0E811AB);
    static readonly Quad p11 = new(false, 25, 0x87C97C5DCCF36DB0, 0xEC607F0FF2E178EE);
    static readonly Quad p12 = new(true, 21, 0xA314E0FAE7573522, 0x9AEA5042F4B27016);
    static readonly Quad p13 = new(false, 16, 0xE6BB3310312DEF36, 0x498E0D0ADB3F704F);
    static readonly Quad p14 = new(true, 11, 0xAC1305F2DC28DF93, 0x421DF7D7A272F6AC);
    static readonly Quad p15 = new(false, 4, 0xE343649A4D050E85, 0x1A8391AB2B909A0B);
    static readonly Quad p16 = new(true, -4, 0xC622C284BE256785, 0xBA66AAB948534DC2);
    static readonly Quad p17 = new(false, -14, 0x8344CEE40816BD2E, 0xAE7BD8F4F19FD00F);
    static readonly Quad p18 = new(true, -29, 0x96F623F1C11A5BE8, 0xDEA18EB7256AA8BF);
    static readonly Quad p19 = new(false, -52, 0xF3FC8CBF561AC665, 0x747563D22D23EC75);
    const double g = 19.375;

    /// <summary>
    /// Calculate the natural logarithm of the gamma function.
    /// </summary>
    public static ComplexQuad LogGamma(ComplexQuad z)
    {
        if (z.Re > 1e15)
            return ComplexQuad.NaN;

        if (ComplexQuad.IsInteger(z))
            return ComplexQuad.Log(Gamma(z));

        if (z.Re < Quad.Zero)
            return ComplexQuad.Log(QuadConstants.π / ComplexQuad.Sin(QuadConstants.π * z)) - LogGamma(Quad.One - z);

        if (z.Re < Quad.One)
            return LogGamma(z + Quad.One) - ComplexQuad.Log(z);

        ComplexQuad s = p0 + p1 / z + p2 / (z + 1) + p3 / (z + 2) + p4 / (z + 3) +
            p5 / (z + 4) + p6 / (z + 5) + p7 / (z + 6) + p8 / (z + 7) + p9 / (z + 8) +
            p10 / (z + 9) + p11 / (z + 10) + p12 / (z + 11) + p13 / (z + 12) +
            p14 / (z + 13) + p15 / (z + 14) + p16 / (z + 15) + p17 / (z + 16) +
            p18 / (z + 17) + p19 / (z + 18);

        ComplexQuad r = z + (g - 0.5);
        r = ComplexQuad.Log(s) + (z - 0.5) * ComplexQuad.Log(r) - r;
        r.Re.SignificantBits = 106;
        r.Im.SignificantBits = 106;
        return r;
    }

    /// <summary>
    /// Calculate the gamma function.
    /// </summary>
    public static ComplexQuad Gamma(ComplexQuad z)
    {
        Quad rr = Quad.Round(z.Re);

        if (z.Im == Quad.Zero && z.Re == rr)
        {
            if (rr <= Quad.Zero)
                return ComplexQuad.NaN;

            if (rr > Quad.OneHalf)
                return ComplexQuad.Factorial(z.Re - 1);
        }

        if (z.Re < Quad.OneHalf)
        {
            var pi = QuadConstants.π;
            var piz = pi * z;
            var sin = ComplexQuad.Sin(piz);
            var omz = Quad.One - z;
            var gamma = Gamma(omz);
            var g = pi / (sin * gamma);
            return g;
        }

        Sum<ComplexQuad> s = new();
        s += p0;
        s += p1 / z;
        s += p2 / (z + 1);
        s += p3 / (z + 2);
        s += p4 / (z + 3);
        s += p5 / (z + 4);
        s += p6 / (z + 5);
        s += p7 / (z + 6);
        s += p8 / (z + 7);
        s += p9 / (z + 8);
        s += p10 / (z + 9);
        s += p11 / (z + 10);
        s += p12 / (z + 11);
        s += p13 / (z + 12);
        s += p14 / (z + 13);
        s += p15 / (z + 14);
        s += p16 / (z + 15);
        s += p17 / (z + 16);
        s += p18 / (z + 17);
        s += p19 / (z + 18);

        ComplexQuad r = z + (g - 0.5);
        r = s.Value * ComplexQuad.Pow(r, z - 0.5) / ComplexQuad.Exp(r);
        r.Re.SignificantBits = 106;
        r.Im.SignificantBits = 106;
        return r;
    }

    public static Quad LogGamma(Quad x) => LogGamma((ComplexQuad)x).Re;

    public static double LogGamma(double x) => (double)LogGamma((Quad)x);

    public static float LogGamma(float x) => (float)LogGamma((double)x);

    public static Half LogGamma(Half x) => (Half)LogGamma((double)x);

    public static Complex Gamma(Complex z) => (Complex)Gamma((ComplexQuad)z);

    public static Quad Gamma(Quad x) => Gamma((ComplexQuad)x).Re;

    public static double Gamma(double x) => (double)Gamma((Quad)x);

    public static float Gamma(float x) => (float)Gamma((double)x);

    public static Half Gamma(Half x) => (Half)Gamma((double)x);

    public static ComplexQuad Beta(ComplexQuad z1, ComplexQuad z2)
    {
        return Gamma(z1) * Gamma(z2) / Gamma(z1 + z2);
    }
}