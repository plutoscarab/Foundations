
namespace Foundations.Algebra;

[TestClass]
public class ExtensionGFTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var ff = FiniteField.OfOrder(27);
        Assert.AreEqual(27, ff.Order);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ConstructorComposite()
    {
        _ = FiniteField.OfOrder(35);
    }

    [TestMethod]
    public void AddZeroRight()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(5);
        var c = a + ff.Zero;
        Assert.AreEqual(a, c);
    }

    [TestMethod]
    public void AddZeroLeft()
    {
        var ff = FiniteField.OfOrder(27);
        var b = ff.Element(22);
        var c = ff.Zero + b;
        Assert.AreEqual(b, c);
    }

    [TestMethod]
    public void AddTest()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(12);
        var b = ff.Element(2);
        var c = a + b;
        Assert.AreEqual(ff.Element(14), c);
    }

    [TestMethod]
    public void AdditionCommutative()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(19);
        var b = ff.Element(9);
        var c = a + b;
        Assert.AreEqual(b + a, c);
    }

    [TestMethod]
    public void MultiplyZeroRight()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(26);
        var c = a * ff.Zero;
        Assert.AreEqual(ff.Zero, c);
    }

    [TestMethod]
    public void MultiplyZeroLeft()
    {
        var ff = FiniteField.OfOrder(27);
        var b = ff.Element(16);
        var c = ff.Zero * b;
        Assert.AreEqual(ff.Zero, c);
    }

    [TestMethod]
    public void MultiplyOneRight()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(6);
        var c = a * ff.One;
        Assert.AreEqual(a, c);
    }

    [TestMethod]
    public void MultiplyOneLeft()
    {
        var ff = FiniteField.OfOrder(27);
        var b = ff.Element(23);
        var c = ff.One * b;
        Assert.AreEqual(b, c);
    }

    [TestMethod]
    public void MultiplyTest()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(13);
        var b = ff.Element(3);
        var c = a * b;
        Assert.AreEqual(ff.Element(17), c);
    }

    [TestMethod]
    public void MultiplyCommutative()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(20);
        var b = ff.Element(10);
        var c = a * b;
        Assert.AreEqual(b * a, c);
    }

    [TestMethod]
    public void NegateTest()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(1);
        var c = ff.Negate(a);
        Assert.AreEqual(ff.Element(2), c);
    }

    [TestMethod]
    public void NegateAddTest()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(18);
        var b = ff.Negate(a);
        var c = a + b;
        Assert.AreEqual(ff.Zero, c);
    }

    [TestMethod]
    public void ValueStrTest()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(8);
        var s = a.ToString();
        Assert.AreEqual("2 + 2α", s);
    }

    [TestMethod]
    public void HasIntegerRepresentationTest()
    {
        var ff = FiniteField.OfOrder(27);
        Assert.AreEqual(false, ff.HasIntegerRepresentation);
    }

    [TestMethod]
    public void FromIntegerTest()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.FromInteger(25);
        var c = ff.Element(25);
        Assert.AreEqual(c, a);
    }

    [TestMethod]
    public void ToStringTest()
    {
        var ff = FiniteField.OfOrder(256);
        var s = ff.ToString();
        Assert.IsTrue(s.Contains("GF(2⁸)"));
    }

    [TestMethod]
    public void InvertTest()
    {
        var ff = FiniteField.OfOrder(27);

        foreach (var a in ff)
        {
            if (a == ff.Zero)
                continue;

            var b = ff.Invert(a);
            var c = a * b;
            Assert.AreEqual(ff.One, c);
        }
    }

    [TestMethod]
    public void PowZeroTest()
    {
        var ff = FiniteField.OfOrder(27);
        var a = ff.Element(6);
        var c = ff.Pow(a, 0);
        Assert.AreEqual(ff.One, c);
    }
    
    [TestMethod]
    public void ZeroStringTest()
    {
        var ff = FiniteField.OfOrder(27);
        var s = ff.Zero.ToString();
        Assert.AreEqual("0", s);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MultiplyMismatchTest()
    {
        var f1 = FiniteField.OfOrder(27);
        var f2 = FiniteField.OfOrder(25);
        var e1 = f1.Element(11);
        var e2 = f2.Element(11);
        _ = f1.Multiply(e1, e2);
    }

    [TestMethod]
    public void EqualsTest()
    {
        var f1 = FiniteField.OfOrder(27);
        Ring<FFValue> f2 = FiniteField.OfPrimePower(3, 3);
        Assert.IsTrue(f1.Equals(f2));
    }

    [TestMethod]
    public void EqualsNotPrimeGFTest()
    {
        var f1 = FiniteField.OfOrder(27);
        Ring<FFValue> f2 = FiniteField.OfPrimePower(2, 11);
        Assert.IsFalse(f1.Equals(f2));
    }
}