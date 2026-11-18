
using System.Numerics;

namespace Foundations;

[TestClass]
public class CatalanNumbersTests
{
    [TestMethod]
    public void CatalanNumbersTest()
    {
        long[] expected = [1, 1, 2, 5, 14, 42, 132, 429, 1430, 4862, 16796];
        CollectionAssert.AreEqual(expected, Sequences.CatalanNumbers().Take(expected.Length).ToArray());
    }

    [TestMethod]
    public void CatalanNumbersInt64Test()
    {
        var numbers = Sequences.CatalanNumbers().ToArray();
        Assert.AreEqual(36, numbers.Length);
        Assert.AreEqual(3116285494907301262L, numbers[^1]);
    }

    [TestMethod]
    public void CatalanNumbersDoubleTest()
    {
        var numbers = Sequences.CatalanNumbersD().ToArray();
        Assert.AreEqual(520, numbers.Length);
        Assert.IsTrue(numbers.All(double.IsFinite));
        Assert.AreEqual(1.0, numbers[0]);
        Assert.AreEqual(1.0, numbers[1]);

        for (var i = 2; i < numbers.Length; i++)
            Assert.IsTrue(numbers[i] > numbers[i - 1]);
    }

    [TestMethod]
    public void CatalanNumbersBigTest()
    {
        var c100 = Sequences.CatalanNumbersB().ElementAt(100);
        Assert.AreEqual(BigInteger.Parse("896519947090131496687170070074100632420837521538745909320"), c100);
    }
}
