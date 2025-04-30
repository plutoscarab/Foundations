using System.Diagnostics;

namespace Foundations;

[TestClass]
public class ScanTests
{
    [TestMethod]
    public void ScanTest()
    {
        int[] s = [3, 1, 4, 1, 5, 9];
        var r = s.Scan(7, (a, b) => a - b);
        int[] e = [7, 4, 3, -1, -2, -7, -16];
        Assert.IsTrue(Enumerable.SequenceEqual(e, r));
    }

    [TestMethod]
    public void ScanNoInitialTest()
    {
        int[] s = [3, 1, 4, 1, 5, 9];
        var r = s.Scan((a, b) => a - b);
        int[] e = [3, 2, -2, -3, -8, -17];
        Assert.IsTrue(Enumerable.SequenceEqual(e, r));
    }

    [TestMethod]
    public void ScanSumTest()
    {
        int[] s = [3, 1, 4, 1, 5, 9];
        var r = s.ScanSum(7);
        int[] e = [7, 10, 11, 15, 16, 21, 30];
        Assert.IsTrue(Enumerable.SequenceEqual(e, r));
    }

    [TestMethod]
    public void ScanSumNoInitialTest()
    {
        int[] s = [3, 1, 4, 1, 5, 9];
        var r = s.ScanSum();
        int[] e = [3, 4, 8, 9, 14, 23];
        Assert.IsTrue(Enumerable.SequenceEqual(e, r));
    }

    [TestMethod]
    public void ScanProductTest()
    {
        int[] s = [3, 1, 4, 1, 5, 9];
        var r = s.ScanProduct(7);
        int[] e = [7, 21, 21, 84, 84, 420, 3780];
        Assert.IsTrue(Enumerable.SequenceEqual(e, r));
    }

    [TestMethod]
    public void ScanProductNoInitialTest()
    {
        int[] s = [3, 1, 4, 1, 5, 9];
        var r = s.ScanProduct();
        int[] e = [3, 3, 12, 12, 60, 540];
        Assert.IsTrue(Enumerable.SequenceEqual(e, r));
    }

    [TestMethod]
    public void FactorialTest()
    {
        var s = Sequences.PositiveIntegers().Take(5);
        var r = s.ScanProduct().ToList();
        long[] e = [1, 2, 6, 24, 120];
        Assert.IsTrue(Enumerable.SequenceEqual(e, r));
    }
}