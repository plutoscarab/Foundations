
namespace Foundations.Algebra;

[TestClass]
public class FFValueTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddWrongFieldTest()
    {
        var f1 = FiniteField.OfOrder(7);
        var f2 = FiniteField.OfOrder(11);
        var e1 = f1.Element(4);
        var _ = f2.Negate(e1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddMismatchedFieldsTest()
    {
        var f1 = FiniteField.OfOrder(7);
        var f2 = FiniteField.OfOrder(11);
        var e1 = f1.Element(4);
        var e2 = f2.Element(4);
        var _ = e1 + e2;
    }

    [TestMethod]
    public void UnaryAddTest()
    {
        var ff = FiniteField.OfOrder(128);
        var a = ff.Element(111);
        var c = +a;
        Assert.AreEqual(a, c);
    }

    [TestMethod]
    public void AddTest()
    {
        var ff = FiniteField.OfOrder(128);
        var a = ff.Element(111);
        var b = ff.Element(33);
        var c = a + b;
        Assert.AreEqual(ff.Element(78), c);
    }
}