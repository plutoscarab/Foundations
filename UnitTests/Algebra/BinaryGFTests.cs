
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.Algebra;

[TestClass]
public class BinaryGFTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        BinaryGF ff = new(8);
        Assert.AreEqual(8, ff.Degree);
    }

    [TestMethod]
    public void AddZeroRight()
    {
        BinaryGF ff = new(8);
        var a = ff.Element(0xC8);
        var c = a + ff.Zero;
        Assert.AreEqual(a, c);
    }

    [TestMethod]
    public void AddZeroLeft()
    {
        BinaryGF ff = new(8);
        var b = ff.Element(0x89);
        var c = ff.Zero + b;
        Assert.AreEqual(b, c);
    }

    [TestMethod]
    public void AddTest()
    {
        BinaryGF ff = new(8);
        var a = ff.Element(0xC8);
        var b = ff.Element(0x89);
        var c = a + b;
        Assert.AreEqual(ff.Element(0x41), c);
    }

    [TestMethod]
    public void AdditionCommutative()
    {
        BinaryGF ff = new(8);
        var a = ff.Element(0xC8);
        var b = ff.Element(0x89);
        var c = a + b;
        Assert.AreEqual(b + a, c);
    }

    [TestMethod]
    public void MultiplyZeroRight()
    {
        BinaryGF ff = new(8);
        var a = ff.Element(0xC8);
        var c = a * ff.Zero;
        Assert.AreEqual(ff.Zero, c);
    }

    [TestMethod]
    public void MultiplyZeroLeft()
    {
        BinaryGF ff = new(8);
        var b = ff.Element(0xC8);
        var c = ff.Zero * b;
        Assert.AreEqual(ff.Zero, c);
    }

    [TestMethod]
    public void MultiplyOneRight()
    {
        BinaryGF ff = new(8);
        var a = ff.Element(0xC8);
        var c = a * ff.One;
        Assert.AreEqual(a, c);
    }

    [TestMethod]
    public void MultiplyOneLeft()
    {
        BinaryGF ff = new(8);
        var b = ff.Element(0xC8);
        var c = ff.One * b;
        Assert.AreEqual(b, c);
    }

    [TestMethod]
    public void MultiplyTest()
    {
        BinaryGF ff = new(8);
        var a = ff.Element(0xC8);
        var b = ff.Element(0x89);
        var c = a * b;
        Assert.AreEqual(ff.Element(0x69), c);
    }

    [TestMethod]
    public void MultiplyCommutative()
    {
        BinaryGF ff = new(8);
        var a = ff.Element(0xC8);
        var b = ff.Element(0x89);
        var c = a * b;
        Assert.AreEqual(b * a, c);
    }

    [TestMethod]
    public void NegateTest()
    {
        BinaryGF ff = new(8);
        var a = ff.Element(0xC8);
        var c = ff.Negate(a);
        Assert.AreEqual(ff.Element(0xC8), c);
    }

    [TestMethod]
    public void NegateAddTest()
    {
        BinaryGF ff = new(8);
        var a = ff.Element(0xC8);
        var b = ff.Negate(a);
        var c = a + b;
        Assert.AreEqual(ff.Zero, c);
    }
}