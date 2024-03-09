
using System;
using System.Linq;
using Foundations.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations;

[TestClass]
public class ContinuedFractionTests
{
    [TestMethod]
    public void UniformRandomTest()
    {
        const int k = 100;
        var χ2 = 0.0;

        for (var i = 0; i < k; i++)
        {
            var u = (double)ContinuedFraction.UniformRandom(Random.Shared, 10);
            var v = (double)ContinuedFraction.UniformRandom(Random.Shared, 10);
            var y = Math.Sqrt(-2 * Math.Log(u)) * Math.Cos(Math.PI * v);
            χ2 += y * y;
        }

        Assert.IsTrue(χ2 > 73.361);
        Assert.IsTrue(χ2 < 128.422);
    }

    [TestMethod]
    public void ToStringTest()
    {
        ContinuedFraction cf = new([1, 2, 3, 4, 5]);
        Assert.AreEqual("[1; 2, 3, 4, 5]", cf.ToString());
    }

    [TestMethod]
    public void FromDoubleTest()
    {
        ContinuedFraction cf = new(1.75);
        Assert.AreEqual("[1; 1, 3]", cf.ToString());
    }

    [TestMethod]
    public void FromFloatTest()
    {
        ContinuedFraction cf = new(1.75f);
        Assert.AreEqual("[1; 1, 3]", cf.ToString());
    }

    [TestMethod]
    public void ToDigitsTest()
    {
        ContinuedFraction cf = new([1, 1, 3]);
        var digits = cf.ToDigits();
        Assert.AreEqual("1.75", digits);
    }

    [TestMethod]
    public void PowTest()
    {
        var x = new ContinuedFraction(0.9m).Pow(20);
        Assert.AreEqual(0.12157665459056928801m, (decimal)x);
    }
}