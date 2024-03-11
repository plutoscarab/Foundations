
namespace Foundations.Algebra;

[TestClass]
public class FieldPolynomialRingTests
{
    [TestMethod]
    public void DivideTest()
    {
        var ff = FiniteField.OfOrder(256);
        var r = new FieldPolynomialRing<FFValue>(ff);
        var p1 = r.Create(ff.Element(159), ff.Element(62), ff.Element(221));
        var p2 = r.Create(ff.Element(124), ff.Element(27));
        var d = FieldPolynomialRing<FFValue>.Divide(p1, p2);
        Assert.AreEqual(r.Create(ff.Element(133), ff.Element(217)), d);
    }

    [TestMethod]
    public void ModTest()
    {
        var ff = FiniteField.OfOrder(256);
        var r = new FieldPolynomialRing<FFValue>(ff);
        var p1 = r.Create(ff.Element(159), ff.Element(62), ff.Element(221));
        var p2 = r.Create(ff.Element(124), ff.Element(27));
        var m = FieldPolynomialRing<FFValue>.Mod(p1, p2);
        Assert.AreEqual(r.Create(ff.Element(108)), m);
    }

    [TestMethod]
    public void GCDTest()
    {
        var ff = FiniteField.OfOrder(256);
        var r = new FieldPolynomialRing<FFValue>(ff);
        var p1 = r.Create(ff.Element(159), ff.Element(62), ff.Element(221));
        var p2 = r.Create(ff.Element(124), ff.Element(27));
        var m = FieldPolynomialRing<FFValue>.GCD(p1, p2);
        Assert.AreEqual(r.Create(ff.Element(108)), m);
    }

    [TestMethod]
    public void FactorizeTest()
    {
        var ff = FiniteField.OfOrder(256);
        var r = new FieldPolynomialRing<FFValue>(ff);
        var p = r.Create(ff.Element(186), ff.Element(89), ff.Element(248), ff.Element(151));
        var fs = FieldPolynomialRing<FFValue>.Factorize(p).ToList();
        var f1 = r.Create(ff.Element(151));
        var f2 = r.Create(ff.Element(186), ff.Element(1));
        var f3 = r.Create(ff.Element(114), ff.Element(30), ff.Element(1));
        Assert.IsTrue(fs.Contains(f1));
        Assert.IsTrue(fs.Contains(f2));
        Assert.IsTrue(fs.Contains(f3));
        Assert.AreEqual(3, fs.Count);
        Assert.AreEqual(p, f1 * f2 * f3);
    }

    [TestMethod]
    public void FactorizeNoConstantTermTest()
    {
        var ff = FiniteField.OfOrder(256);
        var r = new FieldPolynomialRing<FFValue>(ff);
        var p = r.Create(ff.Zero, ff.Element(89), ff.Element(248), ff.Element(151));
        var fs = FieldPolynomialRing<FFValue>.Factorize(p).ToList();
        var f1 = r.Create(ff.Element(151));
        var f2 = r.Create(ff.Zero, ff.One);
        var f3 = r.Create(ff.Element(248), ff.One);
        var f4 = r.Create(ff.Element(92), ff.One);
        Assert.IsTrue(fs.Contains(f1));
        Assert.IsTrue(fs.Contains(f2));
        Assert.IsTrue(fs.Contains(f3));
        Assert.IsTrue(fs.Contains(f4));
        Assert.AreEqual(4, fs.Count);
        Assert.AreEqual(p, f1 * f2 * f3 * f4);
    }

    [TestMethod]
    public void FactorizeLinearFactorTest()
    {
        var ff = FiniteField.OfOrder(256);
        var r = new FieldPolynomialRing<FFValue>(ff);
        var p = r.Create(ff.Element(2), ff.One);
        var fs = FieldPolynomialRing<FFValue>.Factorize(p).ToList();
        Assert.AreEqual(1, fs.Count);
        Assert.AreEqual(p, fs[0]);
    }

    [TestMethod]
    public void FactorizeRandomTest()
    {
        var ff = FiniteField.OfOrder(256);
        var r = new FieldPolynomialRing<FFValue>(ff);
        var a = r.Create(ff.One, ff.One);
        var b = r.Create(ff.Element(2), ff.One);
        var p = a * b;
        var fs = FieldPolynomialRing<FFValue>.Factorize(p).ToList();
        Assert.AreEqual(2, fs.Count);
        Assert.IsTrue(fs.Contains(a));
        Assert.IsTrue(fs.Contains(b));
    }

    [TestMethod]
    public void FactorizeDDFBranchTest()
    {
        var ff = FiniteField.OfOrder(256);
        var r = new FieldPolynomialRing<FFValue>(ff);
        var p = r.Create(ff.Element(7), ff.Zero, ff.One);
        var fs = FieldPolynomialRing<FFValue>.Factorize(p).ToList();
        Assert.AreEqual(1, fs.Count);
    }
}