
using System.Collections;

namespace Foundations.Algebra;

public abstract class FiniteField : Field<FFValue>, IEnumerable<FFValue>, IEquatable<FiniteField>
{
    public readonly int Degree;

    public readonly long Order;

    protected FiniteField(int degree, long order)
    {
        Degree = degree;
        Order = order;
    }

    public IEnumerator<FFValue> GetEnumerator() => AllValues().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public override int GetHashCode() => Order.GetHashCode();

    public FFValue Element(long value) => value >= 0 && value < Order ? new(this, value) :
        throw new ArgumentOutOfRangeException(nameof(value));

    public FFValue this[long index] => Element(index);

    private static readonly ConcurrentDictionary<long, FiniteField> cache = new();

    private IEnumerable<FFValue> AllValues()
    {
        for (var i = 0; i < Order; i++)
            yield return Element(i);
    }

    protected static long Pow(long b, int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
        if (n == 0) return 1;
        if (n == 1) return b;

        if ((n & 1) == 0)
        {
            var m = Pow(b, n / 2);
            return checked(m * m);
        }

        return checked(b * Pow(b, n - 1));
    }

    public static FiniteField OfOrder(long order) => cache.GetOrAdd(order, OfOrderInternal(order));
    public static FiniteField OfPrimePower(int characteristic, int power) => OfOrder(Pow(characteristic, power));

    private static FiniteField OfOrderInternal(long order)
    {
        if (!Primes.IsPrimePower(order, out var p, out var power))
            throw new ArgumentException(nameof(order));

        if (power == 1)
            return new PrimeGF(p);

        if (p == 2)
            return new BinaryGF(power);

        return new ExtensionGF(p, power);
    }

    public FFValue[] ZeroPoly(int degree)
    {
        var arr = new FFValue[degree + 1];

        for (var i = 0; i <= degree; i++)
            arr[i] = Zero;

        return arr;
    }

    public override bool ElementsHaveSign => false;

    public override FFValue RandomSmallNonzeroElement(Random rand)
    {
        while (true)
        {
            var x = 1 - Math.Log(rand.NextDouble()) * Order;

            if (x < Order) 
                return Element((long)x);
        }
    }

    public override bool Equals(object obj) => Equals(obj as FiniteField);

    public bool Equals(FiniteField other) => other is not null && Order == other.Order;

    public override FFValue FromInteger(BigInteger n) => new(this, (long)n);
}
