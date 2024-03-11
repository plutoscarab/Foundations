
using System.Numerics;

namespace Foundations.Algebra;

[TestClass]
public class RingZTests
{
    [TestMethod]
    public void InstanceTest()
    {
        var r = RingZ.Instance;
        Assert.IsTrue(r.ElementsHaveSign);
    }

    [TestMethod]
    public void ZeroSignTest()
    {
        var r = RingZ.Instance;
        Assert.AreEqual(0, r.Sign(BigInteger.Zero));
    }

    [TestMethod]
    public void PositiveSignTest()
    {
        var r = RingZ.Instance;
        Assert.AreEqual(1, r.Sign(BigInteger.One * 2));
    }

    [TestMethod]
    public void NegativeSignTest()
    {
        var r = RingZ.Instance;
        Assert.AreEqual(-1, r.Sign(BigInteger.MinusOne * 2));
    }

    [TestMethod]
    public void AddTest()
    {
        var r = RingZ.Instance;
        BigInteger a = 123;
        BigInteger b = 456;
        var c = r.Add(a, b);
        Assert.AreEqual(a + b, c);
    }

    [TestMethod]
    public void NegateTest()
    {
        var r = RingZ.Instance;
        BigInteger a = 123;
        var c = r.Negate(a);
        Assert.AreEqual(-a, c);
    }

    [TestMethod]
    public void SubtractTest()
    {
        var r = RingZ.Instance;
        BigInteger a = 123;
        BigInteger b = 456;
        var c = r.Subtract(a, b);
        Assert.AreEqual(a - b, c);
    }

    [TestMethod]
    public void ZeroTest()
    {
        var r = RingZ.Instance;
        Assert.AreEqual(BigInteger.Zero, r.Zero);
    }

    [TestMethod]
    public void OneTest()
    {
        var r = RingZ.Instance;
        Assert.AreEqual(BigInteger.One, r.One);
    }

    [TestMethod]
    public void MultiplyTest()
    {
        var r = RingZ.Instance;
        BigInteger a = 123;
        BigInteger b = 456;
        var c = r.Multiply(a, b);
        Assert.AreEqual(a * b, c);
    }

    [TestMethod]
    public void AdditiveGroupTest()
    {
        var r = RingZ.Instance;
        var g = r.AdditiveGroup;
        BigInteger a = 123;
        BigInteger b = 456;
        var c = g.Op(a, b);
        Assert.AreEqual(a + b, c);
    }

    [TestMethod]
    public void SignPositiveTest()
    {
        var r = RingZ.Instance;
        BigInteger a = 123;
        var c = r.Sign(a);
        Assert.AreEqual(1, c);
    }

    [TestMethod]
    public void SignNegativeTest()
    {
        var r = RingZ.Instance;
        BigInteger a = -123;
        var c = r.Sign(a);
        Assert.AreEqual(-1, c);
    }

    [TestMethod]
    public void SignZeroTest()
    {
        var r = RingZ.Instance;
        BigInteger a = 0;
        var c = r.Sign(a);
        Assert.AreEqual(0, c);
    }

    [TestMethod]
    public void PowTest()
    {
        var r = RingZ.Instance;
        BigInteger a = 123;
        var c = r.Pow(a, 7);
        Assert.AreEqual(a * a * a * a * a * a * a, c);
    }
}