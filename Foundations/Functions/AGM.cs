
namespace Foundations.Functions;

using Foundations.Types;

public static partial class Special
{
    public static float AGM(float x, float y) => (float)AGM((double)x, (double)y);

    public static double AGM(double x, double y)
    {
        var i = 0;

        while (x != y && i++ < 100)
        {
            (var ox, var oy, x, y) = (x, y, (x + y) / 2, Math.Sqrt(x * y));
            (x, y) = ((x + y) / 2, Math.Sqrt(x * y));

            if (x == ox || y == oy || x == oy || y == ox)
                break;
        }

        return x;
    }

    public static Quad AGM(Quad x, Quad y)
    {
        var i = 0;

        while (x != y && i++ < 100)
        {
            (var ox, var oy, x, y) = (x, y, (x + y) / 2, Quad.Sqrt(x * y));
            (x, y) = ((x + y) / 2, Quad.Sqrt(x * y));

            if (x == ox || y == oy || x == oy || y == ox)
                break;
        }

        return x;
    }

    public static Complex AGM(Complex x, Complex y)
    {
        var i = 0;

        while (x != y && i++ < 100)
        {
            (var ox, var oy, x, y) = (x, y, (x + y) / 2, Complex.Sqrt(x * y));
            (x, y) = ((x + y) / 2, Complex.Sqrt(x * y));

            if (x == ox || y == oy || x == oy || y == ox)
                break;
        }

        return x;
    }

    public static ComplexQuad AGM(ComplexQuad x, ComplexQuad y)
    {
        var i = 0;

        while (x != y && i++ < 100)
        {
            (var ox, var oy, x, y) = (x, y, (x + y) / 2, ComplexQuad.Sqrt(x * y));
            (x, y) = ((x + y) / 2, ComplexQuad.Sqrt(x * y));

            if (x == ox || y == oy || x == oy || y == ox)
                break;
        }

        return x;
    }

    public static LimitedRational AGM(LimitedRational x, LimitedRational y)
    {
        (x, y) = ((x + y) / 2, LimitedRational.Sqrt(x * y));
        (x, y) = ((x + y) / 2, LimitedRational.Sqrt(x * y));
        (x, y) = ((x + y) / 2, LimitedRational.Sqrt(x * y));
        (x, y) = ((x + y) / 2, LimitedRational.Sqrt(x * y));
        (x, y) = ((x + y) / 2, LimitedRational.Sqrt(x * y));
        (x, y) = ((x + y) / 2, LimitedRational.Sqrt(x * y));
        (x, y) = ((x + y) / 2, LimitedRational.Sqrt(x * y));
        (x, y) = ((x + y) / 2, LimitedRational.Sqrt(x * y));
        var i = 0;

        while (x.ToString() != y.ToString() && i++ < 10)
        {
            (x, y) = ((x + y) / 2, LimitedRational.Sqrt(x * y));
        }
 
        return x;
    }
}
