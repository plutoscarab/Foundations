
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Foundations.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations;

[TestClass]
public class BernoulliNumbersTests
{
    [TestMethod]
    public void BernoulliTest()
    {
        var bs = Sequences.BernoulliNumbers().Take(10).ToList();
        Assert.AreEqual(Rational.One, bs[0]);
        Assert.AreEqual(-Rational.OneHalf, bs[1]);
        Assert.AreEqual(new Rational(1, 6), bs[2]);
        Assert.AreEqual(Rational.Zero, bs[3]);
        Assert.AreEqual(new Rational(-1, 30), bs[4]);
        Assert.AreEqual(Rational.Zero, bs[5]);
        Assert.AreEqual(new Rational(1, 42), bs[6]);
        Assert.AreEqual(Rational.Zero, bs[7]);
        Assert.AreEqual(new Rational(-1, 30), bs[8]);
        Assert.AreEqual(Rational.Zero, bs[9]);
    }

    [TestMethod]
    public void B50Test()
    {
        var b50 = Sequences.BernoulliNumbers().ElementAt(50);
        Assert.AreEqual(new Rational(BigInteger.Parse("495057205241079648212477525"), 66), b50);
    }

    [TestMethod]
    public void BernoulliEvenIndexedTest()
    {
        var bs = Sequences.BernoulliEvenIndexed().Take(10).ToList();
        Assert.AreEqual(Rational.One, bs[0]);
        Assert.AreEqual(new Rational(1, 6), bs[1]);
        Assert.AreEqual(new Rational(-1, 30), bs[2]);
        Assert.AreEqual(new Rational(1, 42), bs[3]);
        Assert.AreEqual(new Rational(-1, 30), bs[4]);
        Assert.AreEqual(new Rational(5, 66), bs[5]);
        Assert.AreEqual(new Rational(-691, 2730), bs[6]);
        Assert.AreEqual(new Rational(7, 6), bs[7]);
        Assert.AreEqual(new Rational(-3617, 510), bs[8]);
        Assert.AreEqual(new Rational(43867, 798), bs[9]);
    }

    // [TestMethod]
    public void BernoulliTable()
    {
        var path = Path.Combine(Assembly.GetExecutingAssembly().Location, "..\\..\\..\\..\\..\\Foundations\\Sequences\\BernoulliTable.cs");
        using var file = new IndentedTextWriter(File.CreateText(path));
        ((StreamWriter)file.InnerWriter).AutoFlush = true;
        file.WriteLine();
        file.WriteLine("namespace Foundations;");
        file.WriteLine();
        file.WriteLine("public static partial class Sequences");
        file.WriteLine("{");
        file.Indent();
        file.WriteLine("public static readonly ImmutableList<double> BernoulliTable =");
        file.WriteLine("[");
        file.Indent();
        List<string> list = [];
        List<Quad> qs = [];
        string line = "";

        foreach (var b in Sequences.BernoulliEvenIndexedQuad().Take(500))
        {
            var s = b.ToString();

            if (!double.TryParse(s.Replace("^", "E"), out var d) || double.IsInfinity(d))
                break;

            list.Add(s);
            qs.Add(b);
            var sd = d.ToString("R");

            if (line.Length + sd.Length > 106)
            {
                file.WriteLine(line);
                line = "";
            }

            line += sd + ", ";
            
            if (list.Count == 1)
                line += "-0.5, ";
            else
                line += "0.0, ";
        }

        file.WriteLine(line);
        file.Outdent();
        file.WriteLine("];");
        file.WriteLine();
        line = "";
        file.WriteLine("public static readonly ImmutableList<double> BernoulliTableOverN =");
        file.WriteLine("[");
        file.Indent();
        var n = 0;

        foreach (var b in list)
        {
            var sd = n == 0 ? "double.NaN" : (double.Parse(b.Replace("^", "E")) / n).ToString("R");

            if (line.Length + sd.Length > 106)
            {
                file.WriteLine(line);
                line = "";
            }

            line += sd + ", ";

            if (n == 0)
                line += "-0.5, ";
            else
                line += "0.0, ";

            n += 2;
        }

        file.WriteLine(line);
        file.Outdent();
        file.WriteLine("];");
        file.WriteLine();
        file.WriteLine("public static readonly ImmutableList<decimal> BernoulliTableDecimal =");
        file.WriteLine("[");
        file.Indent();
        var style = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent;
        n = 0;
        line = "";

        foreach (var b in list)
        {
            if (b.EndsWith("+30"))
                break;

            var sd = decimal.Parse(b.Replace("^", "E"), style).ToString() + "m";

            if (line.Length + sd.Length > 106)
            {
                file.WriteLine(line);
                line = "";
            }

            line += sd + ", ";

            if (n == 0)
                line += "-0.5m, ";
            else
                line += "0.0m, ";

            n += 2;
        }

        file.WriteLine(line);
        file.Outdent();
        file.WriteLine("];");
        file.WriteLine();
        file.WriteLine("public static readonly ImmutableList<decimal> BernoulliTableOverNDecimal =");
        file.WriteLine("[");
        file.Indent();
        n = 0;
        line = "";

        foreach (var b in list)
        {
            if (b.EndsWith("+30"))
                break;
                
            var sd = n == 0 ? "decimal.MaxValue" : (decimal.Parse(b.Replace("^", "E"), style) / n).ToString() + "m";

            if (line.Length + sd.Length > 106)
            {
                file.WriteLine(line);
                line = "";
            }

            line += sd + ", ";

            if (n == 0)
                line += "-0.5m, ";
            else
                line += "0.0m, ";

            n += 2;
        }

        file.WriteLine(line);
        file.Outdent();
        file.WriteLine("];");
        file.WriteLine();
        file.WriteLine("public static readonly ImmutableList<Quad> BernoulliTableQuad =");
        file.WriteLine("[");
        file.Indent();

        foreach (var b in list)
        {
            file.Write($"new(\"{b}\"), ");
            
            if (b == "1")
                file.WriteLine("-Quad.OneHalf,");
            else
                file.WriteLine("Quad.Zero,");
        }

        file.Outdent();
        file.WriteLine("];");
        file.WriteLine();
        n = 0;
        file.WriteLine("public static readonly ImmutableList<Quad> BernoulliTableOverNQuad =");
        file.WriteLine("[");
        file.Indent();

        foreach (var b in qs)
        {
            if (n == 0)
            {
                file.WriteLine($"Quad.NaN, -Quad.OneHalf,");
            }
            else
            {
                file.WriteLine($"new(\"{b / n}\"), Quad.Zero,");
            }

            n += 2;
        }

        file.Outdent();
        file.WriteLine("];");
        file.Outdent();
        file.WriteLine("}");
    }
}