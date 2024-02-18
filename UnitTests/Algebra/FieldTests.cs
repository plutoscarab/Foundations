
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.Algebra;

[TestClass]
public class FieldTests
{
    [TestMethod]
    public void MultiplicativeGroupTest()
    {
        var ff = FiniteField.OfOrder(4);
        var g = ff.MultiplicativeGroup;
        var a = ff.Element(2);
        var b = ff.Element(3);
        var c = g.Op(a, b);
        Assert.AreEqual(ff.Element(1), c);
    }
}