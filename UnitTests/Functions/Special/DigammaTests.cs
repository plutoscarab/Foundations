
using System.CodeDom.Compiler;
using System.IO;
using System.Numerics;
using System.Reflection;
using Foundations.Types;

using static Foundations.Functions.Special;

namespace Foundations.Functions;

[TestClass]
public class DigammaTests
{
    // [TestMethod]
    public void GetDigammaChebyshevCoefficientsTest()
    {
        var c = GetDigammaChebyshevCoefficients(30);
        
        using var file = new IndentedTextWriter(File.CreateText(Path.Combine(Assembly.GetExecutingAssembly().Location,
            @"..\..\..\..\..\Foundations\Functions\DigammaChebyshevCoefficients.cs")));

        file.WriteLine();
        file.WriteLine("namespace Foundations.Functions;");
        file.WriteLine();
        file.WriteLine("public static partial class Special");
        file.WriteLine("{");
        file.Indent();
        file.WriteLine("private static readonly Quad[] DigammaChebyshevCoefficients =");
        file.WriteLine("[");
        file.Indent();

        foreach (var x in c)
        {
            var (neg, exp, hi, lo) = x;
            file.WriteLine($"new Quad({neg.ToString().ToLower()}, {exp}, 0x{hi:X}, 0x{lo:X}),\t// {x}");
        }

        file.Outdent();
        file.WriteLine("];");
        file.Outdent();
        file.WriteLine("}");
        file.Close();
    }

    [TestMethod]
    public void DigammaDoubleTest()
    {
        var p = Digamma(.25);
        p = Digamma(.75);
        p = Digamma(Constants.π);
        p = Digamma(1.0);
        p = Digamma(2.0);
        p = Digamma(3.0);
        p = Digamma(Constants.Sqrt2);
        var q = Digamma(QuadConstants.Sqrt2);
        var z = Digamma(new ComplexQuad(1, 1));
        var s = Digamma(new Complex(1, 1));
    }

    [TestMethod]
    public void HarmonicNumbersTest()
    {
        Sum<Quad> q = Quad.Zero;
        Quad h, hq;

        for (var i = 2; i < 100; i++)
        {
            q += Quad.One / (i - 1);
            h = Digamma((Quad)i) + QuadConstants.γ;
            hq = h - q.Value;
        }
    }
}