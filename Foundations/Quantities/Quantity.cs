
namespace Foundations;

/// <summary>
/// The type of physical quantities with units.
/// </summary>
public record struct Quantity(double Value, SI Units)
{
    public static Quantity operator *(Quantity quantity, SI units) =>
        new(quantity.Value, quantity.Units * units);

    public static Quantity operator /(Quantity quantity, SI units) =>
        new(quantity.Value, quantity.Units / units);

    public static Quantity operator *(Quantity a, Quantity b) =>
        new(a.Value * b.Value, a.Units * b.Units);

    public static Quantity operator /(Quantity a, Quantity b) =>
        new(a.Value / b.Value, a.Units / b.Units);

    public static Quantity operator +(Quantity a, Quantity b) =>
        new(a.Value + b.Value, a.Units * (a.Units == b.Units ? b.Units : throw new ArgumentException("Unit mismatch.")));

    public static implicit operator Quantity(double value) =>
        new(value, SI.Dimensionless);

    public override readonly string ToString()
    {
        if (Units == SI.Dimensionless)
            return Value.ToString();

        return $"{Value} {Units}";
    }
}
