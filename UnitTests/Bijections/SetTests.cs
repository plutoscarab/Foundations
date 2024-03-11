
using System.Collections.Generic;

namespace Foundations.Types;

[TestClass]
public class SetTests
{
    [TestMethod]
    public void NatFromEmptySet()
    {
        HashSet<Nat> set = [];
        Nat n = new(set);
        Assert.AreEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToEmptySet()
    {
        var set = Nat.Zero.ToSet();
        Assert.AreEqual(0, set.Count);
    }

    [TestMethod]
    public void NatFromSet()
    {
        HashSet<Nat> set = [1, 2, 3, 4, 5];
        Nat n = new(set);
        Assert.AreNotEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToSet()
    {
        var set = ((Nat)12345).ToSet();
        Assert.AreNotEqual(0, set.Count);
    }

    [TestMethod]
    public void SetRoundTripThroughNat()
    {
        HashSet<Nat> set = [1, 3, 5, 7, 9];
        Nat n = new(set);
        var set2 = n.ToSet();
        Assert.IsTrue(Enumerable.SequenceEqual(set, set2));
    }

    [TestMethod]
    public void NatRoundTripThroughSet()
    {
        Nat n = 8675309;
        var set = n.ToSet();
        Nat m = new(set);
        Assert.AreEqual(n, m);
    }

    [TestMethod]
    public void NatToSetInjective()
    {
        Random rand = new("NatToSetInjective".GetHashCode());
        HashSet<Nat> domain = new(Enumerable.Range(0, 10_000).Select(_ => (Nat)rand.Next(1_000_000)));
        HashSet<HashSet<Nat>> range = new(domain.Select(n => n.ToSet()), HashSet<Nat>.CreateSetComparer());
        Assert.AreEqual(domain.Count, range.Count);
    }

    [TestMethod]
    public void SetToNatInjective()
    {
        Random rand = new("SetToNatInjective".GetHashCode());
        HashSet<HashSet<Nat>> domain = new(Enumerable.Range(0, 10_000).Select(_ => MakeSet(rand)), HashSet<Nat>.CreateSetComparer());
        HashSet<Nat> range = new(domain.Select(set => new Nat(set)));
        Assert.AreEqual(domain.Count, range.Count);
    }

    private static HashSet<Nat> MakeSet(Random rand)
    {
        HashSet<Nat> set = [];

        while (rand.Next(5) > 0)
        {
            var x = 1 / Math.Pow(rand.NextDouble(), 10);

            if (x <= int.MaxValue)
                set.Add((int)x);
        }

        return set;
    }

    [TestMethod]
    public void AllSetsTest()
    {
        var sets = Nat.AllSets().Take(100).ToList();
        HashSet<HashSet<Nat>> set = new(sets, HashSet<Nat>.CreateSetComparer());
        Assert.AreEqual(sets.Count, set.Count);
        HashSet<Nat> nats = new(sets.Select(set => new Nat(set)));
        Assert.AreEqual(sets.Count, nats.Count);

        for (Nat n = 0; n < sets.Count; n++)
        {
            Assert.IsTrue(nats.Contains(n));
        }
    }
}