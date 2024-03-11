
namespace Foundations.Types;

public readonly struct Sum<T>(T value) where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>,
    IUnaryNegationOperators<T, T>, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>, IAdditiveIdentity<T, T>,
    IEqualityOperators<T, T, bool>
{
    public readonly T Value = value;

    private readonly T Error = T.AdditiveIdentity;

    public Sum(T value, T error) : this(value)
    {
        Error = error;
    }

    public static Sum<T> operator +(Sum<T> sum, T other)
    {
        other -= sum.Error;
        var total = sum.Value + other;
        return new(total, (total - sum.Value) - other);
    }

    public static Sum<T> operator -(Sum<T> sum, T other)
    {
        return sum + (-other);
    }

    public static Sum<T> operator *(Sum<T> sum, T other)
    {
        return new(sum.Value * other, sum.Error * other);
    }

    public static Sum<T> operator /(Sum<T> sum, T other)
    {
        return new(sum.Value / other, sum.Error / other);
    }

    public static implicit operator Sum<T>(T value) => new(value);

    public static explicit operator T(Sum<T> sum) => sum.Value;

    public bool Equals(Sum<T> other) => Value == other.Value;
}
