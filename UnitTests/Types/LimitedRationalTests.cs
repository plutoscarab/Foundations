
using System;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.Types;

[TestClass]
public class LimitedRationalTests
{
    [TestMethod]
    public void CreateConstantsTest()
    {
        var k = Rational.One;

        foreach (var c in ContinuedFraction.Convergents(ContinuedFraction.E(1000)))
        {
            if (Math.Min(c.P.GetByteCount(), c.Q.GetByteCount()) > 100)
                break;

            k = c;
        }
        
        var pb = k.P.ToByteArray();
        var qb = k.Q.ToByteArray();
        var s = "new(new(new([" + string.Join(",", pb) + "]), new([" + string.Join(",", qb) + "])))";

        foreach (var c in ContinuedFraction.Convergents(ContinuedFraction.Pi(1000)))
        {
            if (Math.Min(c.P.GetByteCount(), c.Q.GetByteCount()) > 100)
                break;

            k = c;
        }
        
        pb = k.P.ToByteArray();
        qb = k.Q.ToByteArray();
        s = "new(new(new([" + string.Join(",", pb) + "]), new([" + string.Join(",", qb) + "])))";
        
        pb = (k.P * 2).ToByteArray();
        qb = k.Q.ToByteArray();
        s = "new(new(new([" + string.Join(",", pb) + "]), new([" + string.Join(",", qb) + "])))";
    }

    [TestMethod]
    public void ExpTest()
    {
        var x = LimitedRational.Exp(new(Rational.OneHalf));
        var s = x.ToString();
        x = LimitedRational.Exp(LimitedRational.Pi);
        s = x.ToString();
        x = LimitedRational.Exp(new(-Rational.Two - Rational.OneHalf));
        s = x.ToString();
        x = LimitedRational.Exp(1.2345m);
        s = x.ToString();
        x = LimitedRational.Exp10(1.2345m);
        s = x.ToString();
        x = LimitedRational.Exp2(1.2345m);
        s = x.ToString();
    }

    [TestMethod]
    public void LogTest()
    {
        var x = LimitedRational.Log(new(Rational.OneHalf));
        var s = x.ToString();
        x = LimitedRational.Log(LimitedRational.Pi);
        s = x.ToString();
        x = LimitedRational.Log(new(Rational.Two + Rational.OneHalf));
        s = x.ToString();
        x = LimitedRational.Log(1.2345m);
        s = x.ToString();
        x = LimitedRational.Log10(1_000_000m);
        s = x.ToString();
        x = LimitedRational.Log2(Math.Pow(2, -50));
        s = x.ToString();
    }
}