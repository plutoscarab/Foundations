
using static Foundations.Functions.Special;

namespace Foundations.Types;

public partial struct Nat
{
    public Nat((Nat, Nat) tuple)
    {
        var (x, y) = tuple;
        b = (x + (x + y + 1) * (x + y) / 2).b;
    }

    public Nat((Nat, Nat, Nat) tuple)
    {
        var (x, y, z) = tuple;
        b = (x + (x + y + 1) * (x + y) / 2 + (x + y + z + 2) * (x + y + z + 1) * (x + y + z) / 6).b;
    }

    public Nat((Nat, Nat, Nat, Nat) tuple)
    {
        var (x, y, z, w) = tuple;
        b = (x + Binomial(x + y + 1, 2) + Binomial(x + y + z + 2, 3) + Binomial(x + y + z + w + 3, 4)).b;
    }

    public readonly void Deconstruct(out Nat item1, out Nat item2)
    {
        (item1, item2) = ((Nat, Nat))this;
    }

    public readonly void Deconstruct(out Nat item1, out Nat item2, out Nat item3)
    {
        var list = BinomialDigits(3, this);
        item1 = list[2];
        item2 = list[1] - list[2] - 1;
        item3 = list[0] - list[1] - 1;
    }

    public readonly void Deconstruct(out Nat item1, out Nat item2, out Nat item3, out Nat item4)
    {
        var list = BinomialDigits(4, this);
        item1 = list[3];
        item2 = list[2] - list[3] - 1;
        item3 = list[1] - list[2] - 1;
        item4 = list[0] - list[1] - 1;
    }

    public readonly void Deconstruct(out Nat item1, out Nat item2, out Nat item3, out Nat item4, out Nat item5)
    {
        var list = BinomialDigits(5, this);
        item1 = list[4];
        item2 = list[3] - list[4] - 1;
        item3 = list[2] - list[3] - 1;
        item4 = list[1] - list[2] - 1;
        item5 = list[0] - list[1] - 1;
    }

    private static Nat UpperBinomial(Nat k, Nat n)
    {
        var to = k;

        while (Binomial(to, k) <= n + k)
        {
            to *= 2;
        }

        var from = to / 2;

        while (from != to)
        {
            var mid = (from + to) / 2;

            if (Binomial(mid, k) > n)
                to = mid;
            else
                from = mid + 1;
        }

        return from;
    }

    private static List<Nat> BinomialDigits(Nat k, Nat n)
    {
        List<Nat> list = [];

        while (!k.IsZero)
        {
            var m = UpperBinomial(k, n) - 1;
            list.Add(m);
            n -= Binomial(m, k--);
        }

        return list;
    }

    public readonly Nat[] ToTuple(int length)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(length, 1, nameof(length));

        switch (length)
        {
            case 1:
                return [this];

            case 2:
                var (a, b) = this;
                return [a, b];
        }

        var list = BinomialDigits(length, this);
        var arr = new Nat[length];
        arr[0] = list[^1];

        for (var i = 1; i < length; i++)
        {
            arr[i] = list[length - 1 - i] - list[length - i] - 1;
        }

        return arr;
    }

    public static Nat FromTuple(Nat[] tuple)
    {
        ArgumentNullException.ThrowIfNull(tuple, nameof(tuple));
        ArgumentOutOfRangeException.ThrowIfLessThan(tuple.Length, 1, nameof(tuple.Length));

        switch (tuple.Length)
        {
            case 1:
                return tuple[0];

            case 2:
                return new Nat((tuple[0], tuple[1]));
        }

        var t = tuple[0];
        var sum = t;

        for (var i = 1; i < tuple.Length; i++)
        {
            t += tuple[i] + 1;
            sum += Binomial(t, i + 1);
        }

        return sum;
    }

    public static Nat Isqrt(Nat x)
    {
        if (x <= 1)
            return x;

        BigInteger q = 1, r = 0, t, v = x;

        while (q <= v)
            q <<= 2;

        while (q > 1)
        {
            q >>= 2;
            t = v - r - q;
            r >>= 1;
            if (t >= 0) { v = t; r += q; }
        }

        return new(r);
    }

    public static implicit operator (Nat, Nat)(Nat n)
    {
        if (n.b.IsZero) return (0, 0);
        var w = (Isqrt(8 * n + 1) - 1) / 2;
        var t = w * (w + 1) / 2;
        var y = n - t;
        return (y, w - y);
    }

    public static implicit operator (Nat, Nat, Nat)(Nat n)
    {
        var (a, b, c) = n;
        return (a, b, c);
    }

    public static implicit operator (Nat, Nat, Nat, Nat)(Nat n)
    {
        var (a, b, c, d) = n;
        return (a, b, c, d);
    }

    public static implicit operator (Nat, Nat, Nat, Nat, Nat)(Nat n)
    {
        var (a, b, c, d, e) = n;
        return (a, b, c, d, e);
    }

    public static IEnumerable<(Nat, Nat)> AllPairs() => All().Select(n => ((Nat, Nat))n);

    public static IEnumerable<(Nat, Nat, Nat)> AllTriples() => All().Select(n => ((Nat, Nat, Nat))n);

    public static IEnumerable<Nat[]> AllTuples(int length) => All().Select(n => n.ToTuple(length));
}
