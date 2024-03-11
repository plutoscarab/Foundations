
namespace Foundations.Types;

public partial struct Nat
{
    public static HashSet<Nat> ListToSet(List<Nat> list)
    {
        BigInteger n = -1;
        HashSet<Nat> set = [];

        foreach (var item in list)
        {
            n = n + 1 + item.b;
            set.Add(new(n));
        }

        return set;
    }

    public static List<Nat> SetToList(HashSet<Nat> set)
    {
        BigInteger n = -1;
        List<Nat> list = [];

        foreach (var item in set.OrderBy(t => t))
        {
            list.Add(new(item.b - n - 1));
            n = item.b;
        }

        return list;
    }

    public Nat(HashSet<Nat> set, int bias = DefaultBias) : this(SetToList(set), bias)
    {
    }

    public readonly HashSet<Nat> ToSet(int bias = DefaultBias) => ListToSet(ToList(bias));

    public static IEnumerable<HashSet<Nat>> AllSets(int bias = DefaultBias) =>
        All().Select(n => n.ToSet(bias));
}