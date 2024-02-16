
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Foundations.Bijections;

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

    public static Nat FromSet(HashSet<Nat> set, int dilution = DefaultDilution) => new(SetToList(set), dilution);

    public readonly HashSet<Nat> ToSet(int dilution = DefaultDilution) => ListToSet(ToList(dilution));

    public static IEnumerable<HashSet<Nat>> AllSets(int dilution = DefaultDilution) =>
        All().Select(n => n.ToSet(dilution));
}