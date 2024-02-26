
using System;
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

    [TestMethod]
    public void SqrtTest()
    {
        var x = LimitedRational.Sqrt(55555555);
        var y = x * x;
        var s = y.ToString();
    }

    [TestMethod]
    public void CbrtTest()
    {
        var x = LimitedRational.Cbrt(55555555);
        var y = x * x * x;
        var s = y.ToString();
    }

    [TestMethod]
    public void HypotTest()
    {
        LimitedRational a = new(55555);
        LimitedRational b = new(34567);
        var h = LimitedRational.Hypot(a, b);
        var z = h * h - a * a - b * b;
        var s = z.ToString();
    }

    [TestMethod]
    public void RootNTest()
    {
        LimitedRational x = new(98765);
        var y = LimitedRational.RootN(x, 5);
        var z = x - y * y * y * y * y;
        var s = z.ToString();
    }

    [TestMethod]
    public void PowTest()
    {
        var x = LimitedRational.Pow(LimitedRational.Pi, LimitedRational.E);
        var z = x.ToString();
    }

    [TestMethod]
    public void SinTest()
    {
        var x = LimitedRational.Sin(55555);
        var s = x.ToString();
    }

    [TestMethod]
    public void CosTest()
    {
        var x = LimitedRational.Cos(55555);
        var y = LimitedRational.Sin(55555);
        var z = x * x + y * y - 1;
        var s = z.ToString();
    }

    [TestMethod]
    public void TanTest()
    {
        var x = LimitedRational.Tan(55555);
        var s = x.ToString();
    }

    [TestMethod]
    public void AtanTest()
    {
        var x = LimitedRational.Atan(3);
        var z = LimitedRational.Tan(x) - 3;
        var s = z.ToString();
    }

    [TestMethod]
    public void AcosTest()
    {
        var x = LimitedRational.Acos(0.3m);
        var z = LimitedRational.Cos(x) - 0.3m;
        var s = z.ToString();
    }

    [TestMethod]
    public void AsinTest()
    {
        var x = LimitedRational.Asin(0.3m);
        var z = LimitedRational.Sin(x) - 0.3m;
        var s = z.ToString();
    }

    [TestMethod]
    public void TanhTest()
    {
        var x = LimitedRational.Tanh(-0.4m);
        var s = x.ToString();
    }

    [TestMethod]
    public void AtanhTest()
    {
        var x = LimitedRational.Atanh(-0.4m);
        var y = LimitedRational.Tanh(x);
        var s = y.ToString();
    }

    [TestMethod]
    public void CoshTest()
    {
        var x = LimitedRational.Cosh(0.4m);
        var s = x.ToString();
    }

    [TestMethod]
    public void AcoshTest()
    {
        var x = LimitedRational.Acosh(1.4m);
        var y = LimitedRational.Cosh(x);
    }

    [TestMethod]
    public void SinhTest()
    {
        var x = LimitedRational.Sinh(0.4m);
        var y = LimitedRational.Cosh(0.4m);
        var z = y * y - x * x - 1;
    }

    [TestMethod]
    public void AsinhTest()
    {
        var x = LimitedRational.Asinh(1.4m);
        var y = LimitedRational.Sinh(x);
    }
}