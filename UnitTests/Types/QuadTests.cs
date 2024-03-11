
namespace Foundations;

[TestClass]
public class QuadTests
{
    [TestMethod]
    public void QuadNegativeToStringTest()
    {
        var s = new Quad(-1).ToString();
        Assert.AreEqual("-1", s);
        s = new Quad(-99).ToString();
        Assert.AreEqual("-99", s);
        s = new Quad(-9.99).ToString();
        Assert.IsTrue(s.StartsWith("-9.9900000"));
        s = new Quad(-9e99).ToString();
        Assert.IsTrue(s.StartsWith("-8.9999999"));
        s = new Quad("-9.99").ToString();
        Assert.AreEqual("-9.99", s);
        s = new Quad("-9^+99").ToString();
        Assert.AreEqual("-9^+99", s);
    }

    [TestMethod]
    public void QuadZeroToStringTest()
    {
        Assert.AreEqual("0", Quad.Zero.ToString());
    }

    [TestMethod]
    public void QuadRoundingToStringTest()
    {
        var x = Quad.One / 6;
        var s = x.ToString();
        Assert.IsTrue(s.StartsWith("1.6666"));
        Assert.IsTrue(s.EndsWith("6667^-1"));
    }
}