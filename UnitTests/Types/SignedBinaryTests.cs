
using System;
using System.Linq;
using Foundations.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations;

[TestClass]
public class SignedBinaryTests
{
    [TestMethod]
    public void AverageTest()
    {
        var a = new SignedBinaryStream(new Sequence<int>([1, 0, -1]));      // 3/8
        var b = new SignedBinaryStream(new Sequence<int>([0, 0, 0, 1]));    // 1/16
        var avg = SignedBinaryStream.Average(a, b).Take(10).ToArray();
        Assert.IsTrue(Enumerable.SequenceEqual([0, 1, 0, 0, -1, 0, 0, 0, 0, 0], avg));  // 7/32
    }

    [TestMethod]
    public void StreamFromDoubleTest()
    {
        for (var i = 0; i < 100; i++)
        {
            var d = Random.Shared.NextDouble() * 2 - 1;
            var s = new SignedBinaryStream(d);
            Assert.AreEqual(d, s.Value(52));
        }
    }

    [TestMethod]
    public void FromDoubleTest()
    {
        for (var i = 0; i < 100; i++)
        {
            var d = 2 / (Random.Shared.NextDouble() - .5);
            var s = new SignedBinary(d).Mantissa.Take(100).ToList();
        } 
    }

    [TestMethod]
    public void AddTest()
    {
        for (var i = 0; i < 100; i++)
        {
            var x = 2 / (Random.Shared.NextDouble() - .5);
            var y = 2 / (Random.Shared.NextDouble() - .5);
            var a = new SignedBinary(x);
            var b = new SignedBinary(y);
            var c = a + b;
            var v = c.Value(104);
            var z = (Rational)x + (Rational)y;
            Assert.AreEqual(z, v);
        }
    }

    [TestMethod]
    public void LinStreamTest()
    {
        for (var i = 0; i < 100; i++)
        {
            var a = Random.Shared.NextDouble() * 2 - 1;
            var u = Random.Shared.NextDouble() * 2 - 1;
            var v = (1 - Math.Abs(u)) * (Random.Shared.NextDouble() * 2 - 1);
            var e = (Rational)u * (Rational)a + (Rational)v;
            var l = SignedBinaryStream.Lin(u, new(a), v);
            Assert.AreEqual(e, l.Value(104));
        }
    }

    [TestMethod]
    public void LinTest()
    {
        for (var i = 0; i < 100; i++)
        {
            var a = 2 / (Random.Shared.NextDouble() - .5);
            var u = 2 / (Random.Shared.NextDouble() - .5);
            var v = 2 / (Random.Shared.NextDouble() - .5);
            var e = (Rational)u * (Rational)a + (Rational)v;
            var l = SignedBinary.Lin(u, new(a), v);
            Assert.AreEqual(e, l.Value(130));
        }
    }
}