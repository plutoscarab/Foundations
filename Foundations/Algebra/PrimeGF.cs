
namespace Foundations.Algebra;

public class PrimeGF : FiniteField
{
    private readonly FFValue zero;
    private readonly FFValue one;
    internal readonly ImmutableList<FFValue> inverse;

    public readonly int Prime;

    public PrimeGF(int prime) : base(1, prime)
    {
        if (!Primes.Contains(prime))
            throw new ArgumentException(null, nameof(prime));

        Prime = prime;
        zero = Element(0);
        one = Element(1);
        var inv = new FFValue[Prime];

        foreach (var n in this)
        {
            var b = n.Value;

            if (b == 0)
            {
                continue;
            }

            if (b == 1)
            {
                inv[b] = one;
                continue;
            }

            long a = Prime;
            long s = 0;
            long t = 1;

            while (b != 1)
            {
                (a, b, s, t) = (b, a % b, t, s - t * (a / b));
            }

            inv[n.Value] = Element((t + Prime) % Prime);
        }

        inverse = [.. inv];
    }

    public override FFValue Zero => zero;
    public override FFValue Negate(FFValue x) => x.Checked(this) == 0 ? Element(0) : Element(Prime - x.Value);
    public override FFValue Add(FFValue a, FFValue b) => Element((a.Checked(this) + b.Checked(this)) % Prime);

    public override FFValue One => one;
    public override FFValue Invert(FFValue x) => x.Checked(this) == 0 ? throw new DivideByZeroException() : inverse[(int)x.Value];
    public override FFValue Multiply(FFValue a, FFValue b) => Element((a.Checked(this) * b.Checked(this)) % Prime);

    public override string ToString() => $"𝔽{Prime.ToSubscript()} ≅ ℤ/{Prime}ℤ";
    internal override string ValueStr(FFValue value) => value.Value.ToString();

    public override bool Equals(Ring<FFValue> other) => other is PrimeGF f && f.Order == Order;

    public override bool HasIntegerRepresentation => true;

    public override FFValue FromInteger(BigInteger n) => new(this, (long)n);
}
