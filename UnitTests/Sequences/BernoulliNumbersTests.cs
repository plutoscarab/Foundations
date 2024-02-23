
using System.Linq;
using System.Numerics;
using Foundations.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations;

[TestClass]
public class BernoulliNumbersTests
{
    [TestMethod]
    public void BernoulliTest()
    {
        var bs = Sequences.BernoulliNumbers().Take(10).ToList();
        Assert.AreEqual(Rational.One, bs[0]);
        Assert.AreEqual(Rational.OneHalf, bs[1]);
        Assert.AreEqual(new Rational(1, 6), bs[2]);
        Assert.AreEqual(Rational.Zero, bs[3]);
        Assert.AreEqual(new Rational(-1, 30), bs[4]);
        Assert.AreEqual(Rational.Zero, bs[5]);
        Assert.AreEqual(new Rational(1, 42), bs[6]);
        Assert.AreEqual(Rational.Zero, bs[7]);
        Assert.AreEqual(new Rational(-1, 30), bs[8]);
        Assert.AreEqual(Rational.Zero, bs[9]);
    }

    [TestMethod]
    public void B50Test()
    {
        var b50 = Sequences.BernoulliNumbers().ElementAt(50);
        Assert.AreEqual(new Rational(BigInteger.Parse("495057205241079648212477525"), 66), b50);
    }

    [TestMethod]
    public void BernoulliEvenIndexedTest()
    {
        var bs = Sequences.BernoulliEvenIndexed().Take(10).ToList();
        Assert.AreEqual(Rational.One, bs[0]);
        Assert.AreEqual(new Rational(1, 6), bs[1]);
        Assert.AreEqual(new Rational(-1, 30), bs[2]);
        Assert.AreEqual(new Rational(1, 42), bs[3]);
        Assert.AreEqual(new Rational(-1, 30), bs[4]);
        Assert.AreEqual(new Rational(5, 66), bs[5]);
        Assert.AreEqual(new Rational(-691, 2730), bs[6]);
        Assert.AreEqual(new Rational(7, 6), bs[7]);
        Assert.AreEqual(new Rational(-3617, 510), bs[8]);
        Assert.AreEqual(new Rational(43867, 798), bs[9]);
    }
}