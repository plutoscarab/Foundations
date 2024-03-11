
using System.Collections.Generic;
using System.Numerics;

namespace Foundations.Bijections;

[TestClass]
public class IntegerTests
{
    [TestMethod]
    public void NatFromZero()
    {
        var b = BigInteger.Zero;
        var n = Nat.FromSigned(b);
        Assert.AreEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToZero()
    {
        var b = Nat.Zero.ToSigned();
        Assert.AreEqual(BigInteger.Zero, b);
    }

    [TestMethod]
    public void NatFromPositive()
    {
        BigInteger b = 123;
        var n = Nat.FromSigned(b);
        Assert.AreNotEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatFromNegative()
    {
        BigInteger b = -123;
        var n = Nat.FromSigned(b);
        Assert.AreNotEqual(Nat.Zero, n);
    }

    [TestMethod]
    public void NatToSigned()
    {
        var b = ((Nat)12345).ToSigned();
        Assert.AreNotEqual(BigInteger.Zero, b);
    }

    [TestMethod]
    public void SignedRoundTripThroughNat()
    {
        BigInteger b = -12345;
        var n = Nat.FromSigned(b);
        var b2 = n.ToSigned();
        Assert.AreEqual(b, b2);
    }

    [TestMethod]
    public void NatRoundTripThroughSigned()
    {
        Nat n = 8675309;
        var b = n.ToSigned();
        var m = Nat.FromSigned(b);
        Assert.AreEqual(n, m);
    }

    [TestMethod]
    public void NatToSignedInjective()
    {
        Random rand = new("NatToSignedInjective".GetHashCode());
        HashSet<Nat> domain = new(Enumerable.Range(0, 10_000).Select(_ => (Nat)rand.Next(1_000_000)));
        HashSet<BigInteger> range = new(domain.Select(n => n.ToSigned()));
        Assert.AreEqual(domain.Count, range.Count);
    }

    [TestMethod]
    public void SignedToNatInjective()
    {
        Random rand = new("SignedToNatInjective".GetHashCode());
        HashSet<BigInteger> domain = new(Enumerable.Range(0, 10_000).Select(_ => (BigInteger)(1 / Math.Pow(rand.NextDouble(), 10))));
        HashSet<Nat> range = new(domain.Select(Nat.FromSigned));
        Assert.AreEqual(domain.Count, range.Count);
    }

    [TestMethod]
    public void AllSignedTest()
    {
        var bs = Nat.AllSigned().Take(100).ToList();
        HashSet<BigInteger> set = new(bs);
        Assert.AreEqual(bs.Count, set.Count);
        HashSet<Nat> nats = new(bs.Select(Nat.FromSigned));
        Assert.AreEqual(bs.Count, nats.Count);

        for (Nat n = 0; n < bs.Count; n++)
        {
            Assert.IsTrue(nats.Contains(n));
        }
    }
}