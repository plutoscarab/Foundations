
using System.Collections.Generic;
using System.Numerics;

namespace Foundations.Bijections;

[TestClass]
public class RationalTests
{
    [TestMethod]
    public void NatFromZero()
    {
        var n = Nat.FromRational(Nat.Zero, Nat.One);
        Assert.AreEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToZero()
    {
        var (p, q) = Nat.Zero.ToRational();
        Assert.AreEqual(Nat.Zero, p);
        Assert.AreEqual(Nat.One, q);
    }

    [TestMethod]
    public void NatFromRational()
    {
        (Nat p, Nat q) = (355, 113);
        var n = Nat.FromRational(p, q);
        Assert.AreNotEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToRational()
    {
        var (p, q) = ((Nat)12345).ToRational();
        Assert.AreNotEqual(Nat.Zero, p);
        Assert.AreNotEqual(Nat.Zero, q);
        var g = Nat.GreatestCommonDivisor(p, q);
        Assert.AreEqual(Nat.One, g);
    }

    [TestMethod]
    public void RationalRoundTripThroughNat()
    {
        (Nat p, Nat q) = (2721, 1001);
        var n = Nat.FromRational(p, q);
        var (p2, q2) = n.ToRational();
        Assert.AreEqual(p, p2);
        Assert.AreEqual(q, q2);
    }

    [TestMethod]
    public void NatRoundTripThroughRational()
    {
        Nat n = 8675309;
        var (p, q) = n.ToRational();
        var m = Nat.FromRational(p, q);
        Assert.AreEqual(n, m);
    }

    [TestMethod]
    public void NatToRationalInjective()
    {
        Random rand = new("NatToRationalInjective".GetHashCode());
        HashSet<Nat> domain = new(Enumerable.Range(0, 10_000).Select(_ => (Nat)rand.Next(1_000_000)));
        HashSet<(Nat, Nat)> range = new(domain.Select(n => n.ToRational()));
        Assert.AreEqual(domain.Count, range.Count);
    }

    [TestMethod]
    public void RationalToNatInjective()
    {
        Random rand = new("RationalToNatInjective".GetHashCode());
        HashSet<(Nat, Nat)> domain = new(Enumerable.Range(0, 10_000).Select(_ => MakeRational(rand)));
        HashSet<Nat> range = new(domain.Select(pq => Nat.FromRational(pq.Item1, pq.Item2)));
        Assert.AreEqual(domain.Count, range.Count);
    }

    private static (Nat, Nat) MakeRational(Random rand)
    {
        var p = (Nat)(BigInteger)(1 / Math.Pow(rand.NextDouble(), 5));
        var q = (Nat)(BigInteger)(1 / Math.Pow(rand.NextDouble(), 5));
        var g = Nat.GreatestCommonDivisor(p, q);
        return (p / g, q / g);
    }

    [TestMethod]
    public void AllRationalTest()
    {
        var bs = Nat.AllRational().Take(100).ToList();
        HashSet<(Nat, Nat)> set = new(bs);
        Assert.AreEqual(bs.Count, set.Count);
        HashSet<Nat> nats = new(bs.Select(pq => Nat.FromRational(pq.Item1, pq.Item2)));
        Assert.AreEqual(bs.Count, nats.Count);

        for (Nat n = 0; n < bs.Count; n++)
        {
            Assert.IsTrue(nats.Contains(n));
        }
    }
}