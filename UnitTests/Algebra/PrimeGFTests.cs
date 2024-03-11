
namespace Foundations.Algebra;

[TestClass]
public class PrimeGFTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        PrimeGF ff = new(127);
        Assert.AreEqual(127, ff.Order);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ConstructorComposite()
    {
        PrimeGF ff = new(128);
    }

    [TestMethod]
    public void AddZeroRight()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(55);
        var c = a + ff.Zero;
        Assert.AreEqual(a, c);
    }

    [TestMethod]
    public void AddZeroLeft()
    {
        PrimeGF ff = new(127);
        var b = ff.Element(6);
        var c = ff.Zero + b;
        Assert.AreEqual(b, c);
    }

    [TestMethod]
    public void AddTest()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(84);
        var b = ff.Element(108);
        var c = a + b;
        Assert.AreEqual(ff.Element(192 - 127), c);
    }

    [TestMethod]
    public void AdditionCommutative()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(35);
        var b = ff.Element(113);
        var c = a + b;
        Assert.AreEqual(b + a, c);
    }

    [TestMethod]
    public void MultiplyZeroRight()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(64);
        var c = a * ff.Zero;
        Assert.AreEqual(ff.Zero, c);
    }

    [TestMethod]
    public void MultiplyZeroLeft()
    {
        PrimeGF ff = new(127);
        var b = ff.Element(15);
        var c = ff.Zero * b;
        Assert.AreEqual(ff.Zero, c);
    }

    [TestMethod]
    public void MultiplyOneRight()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(93);
        var c = a * ff.One;
        Assert.AreEqual(a, c);
    }

    [TestMethod]
    public void MultiplyOneLeft()
    {
        PrimeGF ff = new(127);
        var b = ff.Element(44);
        var c = ff.One * b;
        Assert.AreEqual(b, c);
    }

    [TestMethod]
    public void MultiplyTest()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(122);
        var b = ff.Element(73);
        var c = a * b;
        Assert.AreEqual(ff.Element(16), c);
    }

    [TestMethod]
    public void MultiplyCommutative()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(24);
        var b = ff.Element(102);
        var c = a * b;
        Assert.AreEqual(b * a, c);
    }

    [TestMethod]
    public void NegateTest()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(53);
        var c = ff.Negate(a);
        Assert.AreEqual(ff.Element(127 - 53), c);
    }

    [TestMethod]
    public void NegateAddTest()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(4);
        var b = ff.Negate(a);
        var c = a + b;
        Assert.AreEqual(ff.Zero, c);
    }

    [TestMethod]
    public void ToStringTest()
    {
        PrimeGF ff = new(127);
        var s = ff.ToString();
        Assert.IsTrue(s.Contains('ℤ'));
    }

    [TestMethod]
    public void ValueStrTest()
    {
        PrimeGF ff = new(127);
        var a = ff.Element(82);
        var s = a.ToString();
        Assert.AreEqual("82", s);
    }

    [TestMethod]
    public void EqualsTest()
    {
        PrimeGF f1 = new(127);
        Ring<FFValue> f2 = new PrimeGF(127);
        Assert.IsTrue(f1.Equals(f2));
    }

    [TestMethod]
    public void EqualsNotPrimeGFTest()
    {
        PrimeGF f1 = new(127);
        Ring<FFValue> f2 = FiniteField.OfPrimePower(11, 2);
        Assert.IsFalse(f1.Equals(f2));
    }

    [TestMethod]
    public void HasIntegerRepresentationTest()
    {
        PrimeGF ff = new(127);
        Assert.AreEqual(true, ff.HasIntegerRepresentation);
    }

    [TestMethod]
    public void FromIntegerTest()
    {
        PrimeGF ff = new(127);
        var a = ff.FromInteger(33);
        var c = ff.Element(33);
        Assert.AreEqual(c, a);
    }
}