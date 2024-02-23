
namespace Foundations;

/// <summary>
/// A quad-precision complex number.
/// </summary>
public struct ComplexQuad
{
    public Quad Re;
    public Quad Im;

    public static readonly ComplexQuad Zero = new(Quad.Zero);
    public static readonly ComplexQuad One = new(Quad.One);
    public static readonly ComplexQuad OneHalf = new(Quad.OneHalf);

    /// <summary>
    /// Create a number equal to zero.
    /// </summary>
    public ComplexQuad()
        : this((Quad)0)
    {
    }

    /// <summary>
    /// Create a number with an imaginary part of zero.
    /// </summary>
    /// <param name="d">The real part.</param>
    public ComplexQuad(Quad d)
        : this(d, 0.0)
    {
    }

    /// <summary>
    /// Create a number with specified real and imaginary parts.
    /// </summary>
    public ComplexQuad(Quad re, Quad im)
    {
        Re = re;
        Im = im;
    }

    /// <summary>
    /// Create a copy of another number.
    /// </summary>
    public ComplexQuad(ComplexQuad c)
    {
        Re = c.Re;
        Im = c.Im;
    }

    public ComplexQuad(Complex c)
    {
        Re = c.Real;
        Im = c.Imaginary;
    }

    /// <summary>
    /// Create a real number based on a base-10 string representation
    /// of the number. "^" character is used for the power-of-10 exponent.
    /// </summary>
    public ComplexQuad(string s)
    {
        Re = new Quad(s);
        Im = 0;
    }

    /// <summary>
    /// Not a number.
    /// </summary>
    public static ComplexQuad NaN
    {
        get
        {
            return new ComplexQuad(Quad.NaN, Quad.NaN);
        }
    }

    /// <summary>
    /// Square root of -1
    /// </summary>
    public static ComplexQuad I
    {
        get
        {
            return new ComplexQuad(0, 1);
        }
    }

    /// <summary>
    /// Covert from a UInt64 value.
    /// </summary>
    public static implicit operator ComplexQuad(ulong u)
    {
        if (u == 0)
            return Zero;

        return new Quad(false, 64, u, 0);
    }

    /// <summary>
    /// Convert from a double-precision floating-point value.
    /// </summary>
    public static implicit operator ComplexQuad(double d)
    {
        return new ComplexQuad((Quad)d);
    }

    /// <summary>
    /// Convert from a double-precision complex value.
    /// </summary>
    public static implicit operator ComplexQuad(Complex z)
    {
        return new ComplexQuad(z);
    }

    /// <summary>
    /// Convert from a quad-precision floating-point value.
    /// </summary>
    public static implicit operator ComplexQuad(Quad r)
    {
        return new ComplexQuad(r);
    }

    public static explicit operator int(ComplexQuad z)
    {
        if (z.Im != 0)
            throw new InvalidOperationException();

        return (int)z.Re;
    }

    public static explicit operator Complex(ComplexQuad z)
    {
        return new(z.Re, z.Im);
    }

    /// <summary>
    /// Check if a number is not-a-number. Returns true if
    /// either the real or imaginary parts are NaN.
    /// </summary>
    public static bool IsNaN(ComplexQuad n)
    {
        return Quad.IsNaN(n.Re) || Quad.IsNaN(n.Im);
    }

    /// <summary>
    /// Check if either the real or imaginary part is
    /// infinite.
    /// </summary>
    public static bool IsInfinity(ComplexQuad n)
    {
        return Quad.IsInfinity(n.Re) || Quad.IsInfinity(n.Im);
    }

    private static ulong NegateInt(ulong i)
    {
        return unchecked((i ^ 0xFFFFFFFFFFFFFFFF) + 1L);
    }

    /// <summary>
    /// Change the sign of the real and imaginary parts of the number.
    /// </summary>
    public void Negate()
    {
        Re *= -1;
        Im *= -1;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is ComplexQuad)) return false;
        return this == (ComplexQuad)obj;
    }

    public override int GetHashCode()
    {
        return Re.GetHashCode() * Im.GetHashCode();
    }

    public static bool operator ==(ComplexQuad a, ComplexQuad b)
    {
        if ((object)a == null && (object)b == null) return true;
        if ((object)a == null || (object)b == null) return false;
        return a.Re == b.Re && a.Im == b.Im;
    }

    public static bool operator !=(ComplexQuad a, ComplexQuad b)
    {
        if ((object)a == null && (object)b == null) return false;
        if ((object)a == null || (object)b == null) return true;
        return a.Re != b.Re && a.Im != b.Im;
    }

    public static bool operator >(ComplexQuad a, ComplexQuad b)
    {
        if ((object)a == null || (object)b == null) return false;

        if (a.Im != Quad.Zero || b.Im != Quad.Zero)
            return false;

        return a.Re > b.Re;
    }

    public static bool operator <(ComplexQuad a, ComplexQuad b)
    {
        if ((object)a == null || (object)b == null) return false;

        if (a.Im != Quad.Zero || b.Im != Quad.Zero)
            return false;

        return a.Re < b.Re;
    }

    public static bool operator >=(ComplexQuad a, ComplexQuad b)
    {
        if ((object)a == null || (object)b == null) return false;
        return !(a < b);
    }

    public static bool operator <=(ComplexQuad a, ComplexQuad b)
    {
        if ((object)a == null || (object)b == null) return false;
        return !(a > b);
    }

    public static ComplexQuad operator +(ComplexQuad a, ComplexQuad b)
    {
        return new ComplexQuad(a.Re + b.Re, a.Im + b.Im);
    }

    public static ComplexQuad operator -(ComplexQuad a, ComplexQuad b)
    {
        return new ComplexQuad(a.Re - b.Re, a.Im - b.Im);
    }

    public static ComplexQuad operator -(ComplexQuad x)
    {
        return new ComplexQuad(-x.Re, -x.Im);
    }

    public static ComplexQuad operator *(ComplexQuad a, ComplexQuad b)
    {
        return new ComplexQuad(
            a.Re * b.Re - a.Im * b.Im,
            a.Re * b.Im + a.Im * b.Re);
    }

    public static ComplexQuad operator /(ComplexQuad a, ComplexQuad b)
    {
        if (b.Re.Abs() >= b.Im.Abs())
        {
            Quad dc = b.Im / b.Re;
            Quad r = b.Re + b.Im * dc;
            return new ComplexQuad(
                (a.Re + a.Im * dc) / r,
                (a.Im - a.Re * dc) / r);
        }
        else
        {
            Quad cd = b.Re / b.Im;
            Quad r = b.Re * cd + b.Im;
            return new ComplexQuad(
                (a.Re * cd + a.Im) / r,
                (a.Im * cd - a.Re) / r);
        }
    }

    /// <summary>
    /// Calculate the square root of the number.
    /// </summary>
    public ComplexQuad Sqrt()
    {
        Quad d = Quad.Sqrt(Re * Re + Im * Im);
        Quad re, im;
        if (Re >= Quad.Zero)
        {
            re = Quad.Sqrt((d + Re) / 2);
            im = Im == Quad.Zero ? Quad.Zero : Im / Quad.Two * Quad.Sqrt(Quad.Two / (d + Re));
        }
        else if (Im >= Quad.Zero)
        {
            re = Quad.Abs(Im) / Quad.Two * Quad.Sqrt(Quad.Two / (d - Re));
            im = Quad.Sqrt((d - Re) / Quad.Two);
        }
        else
        {
            re = Quad.Abs(Im) / Quad.Two * Quad.Sqrt(Quad.Two / (d - Re));
            im = -Quad.Sqrt((d - Re) / Quad.Two);
        }
        return new ComplexQuad(re, im);
    }

    /// <summary>
    /// Calculate the square root of a number.
    /// </summary>
    public static ComplexQuad Sqrt(ComplexQuad x)
    {
        return x.Sqrt();
    }

    /// <summary>
    /// Calculate the arithmetic-geometric mean of the number and 1.
    /// See http://mathworld.wolfram.com/Arithmetic-GeometricMean.html
    /// </summary>
    /// <returns></returns>
    public ComplexQuad AGM()
    {
        if (IsNaN(this))
            return NaN;

        if (this == ComplexQuad.Zero)
            return ComplexQuad.Zero;

        ComplexQuad a = ComplexQuad.One;
        ComplexQuad b = new(this);

        Quad e = new Quad("1^-36");
        int i = 20;
        while (i-- > 0)
        {
            ComplexQuad c = (a + b) / Quad.Two;
            if ((Abs(a - c) / Abs(c)) < e) break;
            b = Sqrt(a * b);
            a = c;
        }

        return a;
    }

    /// <summary>
    /// Calculate the arithmetic-geometric mean of a number and 1.
    /// See http://mathworld.wolfram.com/Arithmetic-GeometricMean.html
    /// </summary>
    /// <returns></returns>
    public static ComplexQuad AGM(ComplexQuad x)
    {
        return x.AGM();
    }

    /// <summary>
    /// Calculate e raised to the power of a number.
    /// </summary>
    public static ComplexQuad Exp(ComplexQuad x)
    {
        if (x.Im == Quad.Zero)
            return Quad.Exp(x.Re);

        return Quad.Exp(x.Re) * Cis(x.Im);
    }

    /// <summary>
    /// Calculate the logarithm of the number.
    /// </summary>
    public ComplexQuad Log()
    {
        Quad rr = Re * Re;
        Quad ii = Im * Im;
        Quad re;
        if (rr > 512 * ii)
        {
            re = Quad.Log(Quad.Abs(Re)) + Quad.Log(1 + ii / rr) / Quad.Two;
        }
        else
        {
            re = Quad.Log(Quad.Abs(Im)) + Quad.Log(1 + rr / ii) / Quad.Two;
        }
        Quad im = Quad.Atan2(Im, Re);
        return new ComplexQuad(re, im);
    }

    /// <summary>
    /// Calculate the natural logarithm of a number.
    /// </summary>
    public static ComplexQuad Log(ComplexQuad x)
    {
        return x.Log();
    }

    /// <summary>
    /// Calculate the sine of a number.
    /// </summary>
    public static ComplexQuad Sin(ComplexQuad x)
    {
        return x.Sin();
    }

    /// <summary>
    /// Calculate the sine of the number.
    /// </summary>
    public ComplexQuad Sin()
    {
        if (Im == Quad.Zero)
            return Quad.Sin(Re);
        Quad eb = Quad.Exp(Im);
        Quad emb = Quad.One / eb;
        Quad re = (eb + emb) / Quad.Two * Quad.Sin(Re);
        Quad im = (eb - emb) / Quad.Two * Quad.Cos(Re);
        return new ComplexQuad(re, im);
    }

    /// <summary>
    /// Calculate the inverse sine of a number.
    /// </summary>
    public static ComplexQuad Asin(ComplexQuad x)
    {
        return x.Asin();
    }

    /// <summary>
    /// Calculate the inverse sine of the number.
    /// </summary>
    public ComplexQuad Asin()
    {
        if (Im == Quad.Zero)
            return Quad.Asin(Re);
        return -I * Log(I * Re - Im + Sqrt(Quad.One - Re * Re + Im * Im - Quad.Two * I * Re * Im));
    }

    /// <summary>
    /// Calculate the hyperbolic sine of a number.
    /// </summary>
    public static ComplexQuad Sinh(ComplexQuad x)
    {
        return x.Sinh();
    }

    /// <summary>
    /// Calculate the hyperbolic sine of the number.
    /// </summary>
    public ComplexQuad Sinh()
    {
        if (Im == Quad.Zero)
            return Quad.Sinh(Re);
        return I * Sin(Im - I * Re);
    }

    /// <summary>
    /// Calculate the inverse hyperbolic sine of a number.
    /// </summary>
    public static ComplexQuad Asinh(ComplexQuad x)
    {
        return x.Asinh();
    }

    /// <summary>
    /// Calculate the inverse hyperbolic sine of the number.
    /// </summary>
    public ComplexQuad Asinh()
    {
        if (Im == Quad.Zero)
            return Quad.Log(Re + Quad.Sqrt(Quad.One + Re * Re));
        return Log(Re + I * Im + Sqrt(Quad.One + Re * Re - Im * Im + Quad.Two * I * Re * Im));
    }

    /// <summary>
    /// Calculate the cosine of a number.
    /// </summary>
    public static ComplexQuad Cos(ComplexQuad x)
    {
        return x.Cos();
    }

    /// <summary>
    /// Calculate the cosine of the number.
    /// </summary>
    public ComplexQuad Cos()
    {
        if (Im == Quad.Zero)
            return Quad.Cos(Re);
        Quad eb = Quad.Exp(Im);
        Quad emb = Quad.One / eb;
        Quad re = (emb + eb) / Quad.Two * Quad.Cos(Re);
        Quad im = (emb - eb) / Quad.Two * Quad.Sin(Re);
        return new ComplexQuad(re, im);
    }

    /// <summary>
    /// Calculate the inverse cosine of a number.
    /// </summary>
    public static ComplexQuad Acos(ComplexQuad x)
    {
        return x.Acos();
    }

    /// <summary>
    /// Calculate the inverse cosine of the number.
    /// </summary>
    public ComplexQuad Acos()
    {
        if (Im == Quad.Zero)
            return Quad.Acos(Re);
        return -I * Log(Re + I * Im + Sqrt(Re * Re - Im * Im - Quad.One + Quad.Two * I * Re * Im));
    }

    /// <summary>
    /// Calculate the hyperbolic cosine of a number.
    /// </summary>
    public static ComplexQuad Cosh(ComplexQuad x)
    {
        return x.Cosh();
    }

    /// <summary>
    /// Calculate the hyperbolic cosine of the number.
    /// </summary>
    public ComplexQuad Cosh()
    {
        if (Im == Quad.Zero)
            return Quad.Cosh(Re);
        return Cos(Im - I * Re);
    }

    /// <summary>
    /// Calculate the inverse hyperbolic cosine of a number.
    /// </summary>
    public static ComplexQuad Acosh(ComplexQuad x)
    {
        return x.Acosh();
    }

    /// <summary>
    /// Calculate the inverse hyperbolic cosine of the number.
    /// </summary>
    public ComplexQuad Acosh()
    {
        if (Im == Quad.Zero)
            return Quad.Log(Re + Quad.Sqrt(Re * Re - 1));
        return Log(Re + I * Im + Sqrt(Re * Re - Im * Im - Quad.One + Quad.Two * I * Re * Im));
    }

    /// <summary>
    /// Calculate the tangent of a number.
    /// </summary>
    public static ComplexQuad Tan(ComplexQuad x)
    {
        return x.Tan();
    }

    /// <summary>
    /// Calculate the tangent of the number.
    /// </summary>
    public ComplexQuad Tan()
    {
        if (Im == Quad.Zero)
            return Quad.Tan(Re);
        return Sin() / Cos();
    }

    /// <summary>
    /// Calculate the inverse tangent of a number.
    /// </summary>
    public static ComplexQuad Atan(ComplexQuad x)
    {
        return x.Atan();
    }

    /// <summary>
    /// Calculate the inverse tangent of the number.
    /// </summary>
    public ComplexQuad Atan()
    {
        if (Im == Quad.Zero)
            return Quad.Atan(Re);
        Quad a2 = Re * Re;
        Quad b2 = Im * Im;
        return -I / Quad.Two * Log((Quad.One - a2 - b2 + Quad.Two * I * Re) / (Quad.One + a2 + b2 + Quad.Two * Im));
    }

    /// <summary>
    /// Calculate the hyperbolic tangent of a number.
    /// </summary>
    public static ComplexQuad Tanh(ComplexQuad x)
    {
        return x.Tanh();
    }

    /// <summary>
    /// Calculate the hyperbolic tangent of the number.
    /// </summary>
    public ComplexQuad Tanh()
    {
        if (Im == Quad.Zero)
            return Quad.Tanh(Re);
        return Sinh() / Cosh();
    }

    /// <summary>
    /// Calculate the inverse hyperbolic tangent of a number.
    /// </summary>
    public static ComplexQuad Atanh(ComplexQuad x)
    {
        return x.Atanh();
    }

    /// <summary>
    /// Calculate the inverse hyperbolic tangent of the number.
    /// </summary>
    public ComplexQuad Atanh()
    {
        if (Im == Quad.Zero)
            return Quad.Log((Quad.One + Re) / (Quad.One - Re)) / Quad.Two;
        Quad a2 = Re * Re;
        Quad b2 = Im * Im;
        return -Quad.OneHalf * Log((Quad.OneHalf - a2 - b2 - Quad.Two * I * Im) / (Quad.One + a2 + b2 + Quad.Two * Re));
    }

    /// <summary>
    /// Raise the number to a power.
    /// </summary>
    public ComplexQuad Pow(ComplexQuad exponent)
    {
        ComplexQuad x = exponent;

        if (x == Zero)
            return One;

        if (this == Zero)
            return Zero;

        if (Im == Quad.Zero && x.Im == Quad.Zero && (Re >= Quad.Zero || x.Re == Quad.Round(x.Re)))
            return Quad.Pow(Re, x.Re);

        return Exp(x * Log());
    }

    /// <summary>
    /// Calculate a number raised to the power of another number.
    /// </summary>
    public static ComplexQuad Pow(ComplexQuad @base, ComplexQuad exponent)
    {
        return @base.Pow(exponent);
    }

    /// <summary>
    /// Return a number with real and imaginary parts equal to the
    /// sign (+1, 0, or -1) of the corresponding parts of this number.
    /// </summary>
    public static ComplexQuad Sign(ComplexQuad x)
    {
        return x.Sign();
    }

    /// <summary>
    /// Return a number with real and imaginary parts equal to the
    /// sign (+1, 0, or -1) of the corresponding parts of this number.
    /// </summary>
    public ComplexQuad Sign()
    {
        ComplexQuad n = new(this);
        n.Re = Quad.Sign(n.Re);
        n.Im = Quad.Sign(n.Im);
        return n;
    }

    /// <summary>
    /// Calculate the absolute magnitude of a number.
    /// </summary>
    public static Quad Abs(ComplexQuad n)
    {
        return n.Abs();
    }

    /// <summary>
    /// Calculate the absolute magnitude of the number.
    /// </summary>
    public Quad Abs()
    {
        // n.Re = Quad.Sqrt(n.Re * n.Re + n.Im * n.Im);

        if (Im == Quad.Zero)
        {
            return Quad.Abs(Re);
        }

        Quad A = Re.Abs();
        Quad B = Im.Abs();

        if (A >= B)
        {
            Quad d = B / A;
            return A * Quad.Sqrt(Quad.One + d * d);
        }

        {
            Quad d = A / B;
            return B * Quad.Sqrt(Quad.One + d * d);
        }
    }

    /// <summary>
    /// Calculate the fractional part of a number. The real
    /// and imaginary parts are considered separately.
    /// </summary>
    /// <returns>
    /// Returns values in the range [0,1) for non-negative values
    /// and (-1,0] for negative values.
    /// </returns>
    public static ComplexQuad Frac(ComplexQuad x)
    {
        return x.Frac();
    }

    /// <summary>
    /// Calculate the fractional part of the number. The real
    /// and imaginary parts are considered separately.
    /// </summary>
    /// <returns>
    /// Returns values in the range [0,1) for non-negative values
    /// and (-1,0] for negative values.
    /// </returns>
    public ComplexQuad Frac()
    {
        ComplexQuad n = new(this);
        n.Re -= Quad.Truncate(n.Re);
        n.Im -= Quad.Truncate(n.Im);
        return n;
    }

    /// <summary>
    /// Calculates the angle in the complex plane between the
    /// positive real axis and the ray from the origin to the
    /// specified number.
    /// </summary>
    public static ComplexQuad Arg(ComplexQuad x)
    {
        return x.Arg();
    }

    /// <summary>
    /// Calculates the angle in the complex plane between the
    /// positive real axis and the ray from the origin to the
    /// number.
    /// </summary>
    public ComplexQuad Arg()
    {
        ComplexQuad n = new(this);
        n.Re = Quad.Atan2(n.Im, n.Re);
        n.Im = Quad.Zero;
        return n;
    }

    /// <summary>
    /// Calculates the complex exponential of a number.
    /// </summary>
    public static ComplexQuad Cis(ComplexQuad x)
    {
        return x.Cis();
    }

    /// <summary>
    /// Calculates the complex exponential of the number.
    /// </summary>
    public ComplexQuad Cis()
    {
        return Cos(this) + I * Sin(this);
    }

    ///// <summary>
    ///// Calculate the binary OR of two numbers. The binary
    ///// representation of the real and imaginary components
    ///// are considered separately, and the OR operation is
    ///// performed after the radix points are aligned.
    ///// The result is negative if either number is negative.
    ///// </summary>
    //public static ComplexQuad operator |(ComplexQuad a, ComplexQuad b)
    //{
    //    return new ComplexQuad(a.Re | b.Re, a.Im | b.Im);
    //}

    ///// <summary>
    ///// Calculate the binary AND of two numbers. The binary
    ///// representation of the real and imaginary components
    ///// are considered separately, and the AND operation is
    ///// performed after the radix points are aligned.
    ///// The result is negative if both numbers are negative.
    ///// </summary>
    //public static ComplexQuad operator &(ComplexQuad a, ComplexQuad b)
    //{
    //    return new ComplexQuad(a.Re & b.Re, a.Im & b.Im);
    //}

    ///// <summary>
    ///// Calculate the binary XOR of two numbers. The binary
    ///// representation of the real and imaginary components
    ///// are considered separately, and the XOR operation is
    ///// performed after the radix points are aligned.
    ///// The result is negative if the two values have
    ///// opposite signs.
    ///// </summary>
    //public static ComplexQuad operator ^(ComplexQuad a, ComplexQuad b)
    //{
    //    return new ComplexQuad(a.Re ^ b.Re, a.Im ^ b.Im);
    //}

    /// <summary>
    /// Calculate the greatest integer that is less than
    /// or equal to a given number. Quad and imaginary components
    /// are considered separately.
    /// </summary>
    public static ComplexQuad Floor(ComplexQuad x)
    {
        return x.Floor();
    }

    /// <summary>
    /// Calculate the greatest integer that is less than
    /// or equal to the number. Quad and imaginary components
    /// are considered separately.
    /// </summary>
    public ComplexQuad Floor()
    {
        return new ComplexQuad(Quad.Floor(Re), Quad.Floor(Im));
    }

    /// <summary>
    /// Calculate the smallest integer that is greater than
    /// or equal to a given number. Quad and imaginary components
    /// are considered separately.
    /// </summary>
    public static ComplexQuad Ceiling(ComplexQuad x)
    {
        return x.Ceiling();
    }

    /// <summary>
    /// Calculate the smallest integer that is greater than
    /// or equal to the number. Quad and imaginary components
    /// are considered separately.
    /// </summary>
    public ComplexQuad Ceiling()
    {
        return new ComplexQuad(Quad.Ceiling(Re), Quad.Ceiling(Im));
    }

    /// <summary>
    /// Calculate the integer (non-fractional) portion of a
    /// number. Quad and imaginary components are considered
    /// separately.
    /// </summary>
    public static ComplexQuad Truncate(ComplexQuad x)
    {
        return x.Truncate();
    }

    /// <summary>
    /// Calculate the integer (non-fractional) portion of the
    /// number. Quad and imaginary components are considered
    /// separately.
    /// </summary>
    public ComplexQuad Truncate()
    {
        return new ComplexQuad(Quad.Truncate(Re), Quad.Truncate(Im));
    }

    /// <summary>
    /// Round a number to the nearest integer. ComplexQuads half-way
    /// between integers are rounded to the nearest even integer.
    /// Quad and imaginary components are considered separately.
    /// </summary>
    public static ComplexQuad Round(ComplexQuad x)
    {
        return x.Round();
    }

    /// <summary>
    /// Round the number to the nearest integer. ComplexQuads half-way
    /// between integers are rounded to the nearest even integer.
    /// Quad and imaginary components are considered separately.
    /// </summary>
    public ComplexQuad Round()
    {
        return new ComplexQuad(Quad.Round(Re), Quad.Round(Im));
    }

    /// <summary>
    /// Calculate the remainder when one number is divided by
    /// another number.
    /// </summary>
    /// <returns>Returns a - b * Truncate(a / b)</returns>
    public static ComplexQuad operator %(ComplexQuad a, ComplexQuad b)
    {
        return a - b * (a / b).Truncate();
    }

    const int factorialMax = 3209;
    ComplexQuad[] factorialTable;

    /// <summary>
    /// Return the factorial of a real integer. For complex or
    /// non-integer arguments, use Gamma(n + 1) instead.
    /// </summary>
    public ComplexQuad Factorial()
    {
        if (Im != Quad.Zero)
            return NaN;

        if (Re < Quad.Zero)
            return NaN;

        Quad r = Quad.Round(Re);
        if (Re != r)
            return NaN;

        if (Re > factorialMax + Quad.OneHalf)
            return NaN;

        if (factorialTable == null)
        {
            factorialTable = new ComplexQuad[factorialMax + 1];
            ComplexQuad f = factorialTable[0] = Quad.One;
            for (int i = 1; i <= factorialMax; i++)
                f = factorialTable[i] = i * f;
        }

        return new ComplexQuad(factorialTable[(int)r]);
    }

    /// <summary>
    /// Return the factorial of a real integer. For complex or
    /// non-integer arguments, use Gamma(n + 1) instead.
    /// </summary>
    public static ComplexQuad Factorial(ComplexQuad n)
    {
        return n.Factorial();
    }

    const int doubleFactorialMax = 5909;
    ComplexQuad[] doubleFactorialTable;

    /// <summary>
    /// Return the double factorial of a real integer. For complex 
    /// or non-integer arguments, use Gamma(n + 1) instead.
    /// </summary>
    public ComplexQuad DoubleFactorial()
    {
        if (Im != Quad.Zero)
            return NaN;

        if (Re < Quad.Zero)
            return NaN;

        Quad r = Quad.Round(Re);
        if (Re != r)
            return NaN;

        if (Re > doubleFactorialMax + Quad.OneHalf)
            return NaN;

        if (doubleFactorialTable == null)
        {
            doubleFactorialTable = new ComplexQuad[doubleFactorialMax + 1];
            ComplexQuad even = doubleFactorialTable[0] = Quad.One;
            ComplexQuad odd = doubleFactorialTable[1] = Quad.One;
            for (int i = 2; i <= doubleFactorialMax; i += 2)
            {
                even = doubleFactorialTable[i] = i * even;
                if (i < doubleFactorialMax)
                    odd = doubleFactorialTable[i + 1] = (i + 1) * odd;
            }
        }

        return new ComplexQuad(doubleFactorialTable[(int)r]);
    }

    /// <summary>
    /// Return the double factorial of a real integer. For complex 
    /// or non-integer arguments, use Gamma(n + 1) instead.
    /// </summary>
    public static ComplexQuad DoubleFactorial(ComplexQuad n)
    {
        return n.DoubleFactorial();
    }

    private static ComplexQuad ErfSmallZ(ComplexQuad z)
    {
        ComplexQuad sum = z;
        ComplexQuad term = z;
        ComplexQuad zz = z * z;
        bool odd = true;
        int n = 0;
        while (true)
        {
            term *= zz / ++n;
            ComplexQuad old = new(sum);
            if (odd)
                sum -= term / (2 * n + 1);
            else
                sum += term / (2 * n + 1);
            odd = !odd;
            if (sum == old)
                break;
        }
        return sum * 2 / QuadConstants.Sqrtπ;
    }

    private static ComplexQuad ErfLargeZ(ComplexQuad x)
    {
        ComplexQuad xx = x * x;
        ComplexQuad t = ComplexQuad.Zero;
        for (int k = 60; k >= 1; k--)
        {
            t = (k - ComplexQuad.OneHalf) / (1 + k / (xx + t));
        }
        return (1 - Exp(-xx + Log(x)) / (xx + t) / QuadConstants.Sqrtπ) * x.Sign();
    }

    /// <summary>
    /// Calculate the error function.
    /// </summary>
    public static ComplexQuad Erf(ComplexQuad z)
    {
        if (z == ComplexQuad.Zero)
            return ComplexQuad.Zero;

        if (z.Abs() < 6.5)
            return ErfSmallZ(z);

        return ErfLargeZ(z);
    }

    /// <summary>
    /// Calculate the error function.
    /// </summary>
    public ComplexQuad Erf()
    {
        return Erf(this);
    }

    /// <summary>
    /// Determine whether a number is a real-valued integer.
    /// </summary>
    public static bool IsInteger(ComplexQuad n)
    {
        return n.Im.IsZero() && n.Re == Quad.Round(n.Re);
    }

    /// <summary>
    /// Determine whether the number is a real-valued integer.
    /// </summary>
    public bool IsInteger()
    {
        return IsInteger(this);
    }

    /// <summary>
    /// Determine whether a number is real-valued (has no imaginary component).
    /// </summary>
    public static bool IsQuad(ComplexQuad n)
    {
        return n.Im.IsZero();
    }

    /// <summary>
    /// Determine whether the number is real-valued (has no imaginary component).
    /// </summary>
    public bool IsQuad()
    {
        return this.Im.IsZero();
    }

    internal ComplexQuad Mask(int wordSize)
    {
        return new ComplexQuad(this.Re.Mask(wordSize));
    }

    public override readonly string ToString() => $"<{Re}; {Im}>";
}
