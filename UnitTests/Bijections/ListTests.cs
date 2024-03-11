
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Foundations.Types;

[TestClass]
public class ListTests
{
    [TestMethod]
    public void NatFromEmptyList()
    {
        List<Nat> list = [];
        Nat n = new(list);
        Assert.AreEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToEmptyList()
    {
        var list = Nat.Zero.ToList();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void NatFromList()
    {
        List<Nat> list = [1, 2, 3, 4, 5];
        Nat n = new(list);
        Assert.AreNotEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToList()
    {
        var list = ((Nat)12345).ToList();
        Assert.AreNotEqual(0, list.Count);
    }

    [TestMethod]
    public void ListRoundTripThroughNat()
    {
        List<Nat> list = [1, 3, 5, 7, 9];
        Nat n = new(list);
        var list2 = n.ToList();
        Assert.IsTrue(Enumerable.SequenceEqual(list, list2));
    }

    [TestMethod]
    public void NatRoundTripThroughList()
    {
        Nat n = 8675309;
        var list = n.ToList();
        Nat m = new(list);
        Assert.AreEqual(n, m);
    }

    [TestMethod]
    public void NatToListInjective()
    {
        Random rand = new("NatToListInjective".GetHashCode());
        HashSet<Nat> domain = new(Enumerable.Range(0, 10_000).Select(_ => (Nat)rand.Next(1_000_000)));
        HashSet<List<Nat>> range = new(domain.Select(n => n.ToList()), ListEqualityComparer.Instance);
        Assert.AreEqual(domain.Count, range.Count);
    }

    class ListEqualityComparer : IEqualityComparer<List<Nat>>
    {
        public static readonly ListEqualityComparer Instance = new();

        public bool Equals(List<Nat> x, List<Nat> y) =>
            Enumerable.SequenceEqual(x, y);

        public int GetHashCode([DisallowNull] List<Nat> obj) =>
            obj.Count == 0 ? 0 : obj.Take(5).Select(o => o.GetHashCode()).Aggregate(HashCode.Combine);
    }

    [TestMethod]
    public void ListToNatInjective()
    {
        Random rand = new("ListToNatInjective".GetHashCode());
        HashSet<List<Nat>> domain = new(Enumerable.Range(0, 10_000).Select(_ => MakeList(rand)), ListEqualityComparer.Instance);
        HashSet<Nat> range = new(domain.Select(list => new Nat(list)));
        Assert.AreEqual(domain.Count, range.Count);
    }

    private static List<Nat> MakeList(Random rand)
    {
        List<Nat> list = [];

        while (rand.Next(5) > 0)
        {
            var x = 1 / Math.Pow(rand.NextDouble(), 10);

            if (x <= int.MaxValue)
                list.Add((int)x);
        }

        return list;
    }

    [TestMethod]
    public void ListSelectorTest()
    {
        var list = Nat.Parse("0118_999_81199_9119_725_____6").ToList(n => n.ToString());
        Assert.IsTrue(list is not null);
    }

    [TestMethod]
    public void AllListsTest()
    {
        const int bias = 2;
        var lists = Nat.AllLists(bias).Take(100).ToList();
        HashSet<List<Nat>> set = new(lists, ListEqualityComparer.Instance);
        Assert.AreEqual(lists.Count, set.Count);
        HashSet<Nat> nats = new(lists.Select(list => new Nat(list, bias)));
        Assert.AreEqual(lists.Count, nats.Count);

        for (Nat n = 0; n < lists.Count; n++)
        {
            Assert.IsTrue(nats.Contains(n));
        }
    }
}