
namespace Foundations;

public readonly struct ComplexQuadSum(ComplexQuad value)
{
    public readonly ComplexQuad Value = value;

    private readonly ComplexQuad Error = (ComplexQuad)0;

    public ComplexQuadSum(ComplexQuad value, ComplexQuad error) : this(value)
    {
        Error = error;
    }

    public static ComplexQuadSum operator +(ComplexQuadSum sum, ComplexQuad other)
    {
        other -= sum.Error;
        var total = sum.Value + other;
        return new(total, (total - sum.Value) - other);
    }

    public static ComplexQuadSum operator -(ComplexQuadSum sum, ComplexQuad other)
    {
        return sum + (-other);
    }

    public static ComplexQuadSum operator *(ComplexQuadSum sum, ComplexQuad other)
    {
        return new(sum.Value * other, sum.Error * other);
    }

    public static ComplexQuadSum operator /(ComplexQuadSum sum, ComplexQuad other)
    {
        return new(sum.Value / other, sum.Error / other);
    }

    public static implicit operator ComplexQuadSum(ComplexQuad value) => new(value);

    public static explicit operator ComplexQuad(ComplexQuadSum sum) => sum.Value;
}

public readonly struct ComplexSum(Complex value)
{
    public readonly Complex Value = value;

    private readonly Complex Error = (Complex)0;

    public ComplexSum(Complex value, Complex error) : this(value)
    {
        Error = error;
    }

    public static ComplexSum operator +(ComplexSum sum, Complex other)
    {
        other -= sum.Error;
        var total = sum.Value + other;
        return new(total, (total - sum.Value) - other);
    }

    public static ComplexSum operator -(ComplexSum sum, Complex other)
    {
        return sum + (-other);
    }

    public static ComplexSum operator *(ComplexSum sum, Complex other)
    {
        return new(sum.Value * other, sum.Error * other);
    }

    public static ComplexSum operator /(ComplexSum sum, Complex other)
    {
        return new(sum.Value / other, sum.Error / other);
    }

    public static implicit operator ComplexSum(Complex value) => new(value);

    public static explicit operator Complex(ComplexSum sum) => sum.Value;
}

public readonly struct QuadSum(Quad value)
{
    public readonly Quad Value = value;

    private readonly Quad Error = (Quad)0;

    public QuadSum(Quad value, Quad error) : this(value)
    {
        Error = error;
    }

    public static QuadSum operator +(QuadSum sum, Quad other)
    {
        other -= sum.Error;
        var total = sum.Value + other;
        return new(total, (total - sum.Value) - other);
    }

    public static QuadSum operator -(QuadSum sum, Quad other)
    {
        return sum + (-other);
    }

    public static QuadSum operator *(QuadSum sum, Quad other)
    {
        return new(sum.Value * other, sum.Error * other);
    }

    public static QuadSum operator /(QuadSum sum, Quad other)
    {
        return new(sum.Value / other, sum.Error / other);
    }

    public static implicit operator QuadSum(Quad value) => new(value);

    public static explicit operator Quad(QuadSum sum) => sum.Value;
}

public readonly struct DoubleSum(Double value)
{
    public readonly Double Value = value;

    private readonly Double Error = (Double)0;

    public DoubleSum(Double value, Double error) : this(value)
    {
        Error = error;
    }

    public static DoubleSum operator +(DoubleSum sum, Double other)
    {
        other -= sum.Error;
        var total = sum.Value + other;
        return new(total, (total - sum.Value) - other);
    }

    public static DoubleSum operator -(DoubleSum sum, Double other)
    {
        return sum + (-other);
    }

    public static DoubleSum operator *(DoubleSum sum, Double other)
    {
        return new(sum.Value * other, sum.Error * other);
    }

    public static DoubleSum operator /(DoubleSum sum, Double other)
    {
        return new(sum.Value / other, sum.Error / other);
    }

    public static implicit operator DoubleSum(Double value) => new(value);

    public static explicit operator Double(DoubleSum sum) => sum.Value;
}

public readonly struct SingleSum(Single value)
{
    public readonly Single Value = value;

    private readonly Single Error = (Single)0;

    public SingleSum(Single value, Single error) : this(value)
    {
        Error = error;
    }

    public static SingleSum operator +(SingleSum sum, Single other)
    {
        other -= sum.Error;
        var total = sum.Value + other;
        return new(total, (total - sum.Value) - other);
    }

    public static SingleSum operator -(SingleSum sum, Single other)
    {
        return sum + (-other);
    }

    public static SingleSum operator *(SingleSum sum, Single other)
    {
        return new(sum.Value * other, sum.Error * other);
    }

    public static SingleSum operator /(SingleSum sum, Single other)
    {
        return new(sum.Value / other, sum.Error / other);
    }

    public static implicit operator SingleSum(Single value) => new(value);

    public static explicit operator Single(SingleSum sum) => sum.Value;
}
