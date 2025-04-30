
namespace Foundations.Algorithms;

[TestClass]
public class PSLQTests
{
    [TestMethod]
    public void AlgebraicNumberTest()
    {
        var y = (3 + Math.Sqrt(41)) / 8;
        var x = new[] { 1.0, y, y * y };
        var a = PSLQ.Find(x);
        Assert.IsTrue(Enumerable.SequenceEqual(a, [2, 3, -4]));
    }

    [TestMethod]
    public void PolynomialFactorizationTest()
    {
        // polynomial 40 x^4 - 62 x^3 + 40 x^2 - 13 x - 2
        int[] a = [-2, -13, 40, -62, 40];

        // one of its roots
        var x = (4 + Math.Sqrt(26)) / 10;

        // find quadratic polynomial factor candidate 10 x^2 - 8 x - 1
        var b = PSLQ.Find([1, x, x * x]);
        Assert.IsTrue(Enumerable.SequenceEqual(b, [-1, -8, 10]));
    }
}