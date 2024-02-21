
namespace Foundations;

public partial struct Nat
{
    public static Nat FromRational(Nat p, Nat q)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(Zero, q, nameof(q));
        List<Nat> list = [];

        while (!q.IsZero)
        {
            var (div, rem) = DivRem(p, q);
            if (list.Count > 0) --div;
            list.Add(div);
            (p, q) = (q, rem);
        }

        if (list.Count > 1)
            list[^1]--;

        return new Nat(list, 2) - 1;
    }

    public readonly (Nat, Nat) ToRational()
    {
        var list = (this + 1).ToList(2);

        if (list.Count > 1)
            list[^1]++;

        for (var i = 1; i < list.Count; i++)
            list[i]++;

        Nat p = 0, q = 1, r = 1, s = 0;

        foreach (var d in list)
        {
            (p, q) = (q, q * d + p);
            (r, s) = (s, s * d + r);
        }

        return (q, s);
    }

    public static IEnumerable<(Nat, Nat)> AllRational() =>
        All().Select(n => n.ToRational());
}