
using System.Numerics;
using Foundations.Types;

namespace Foundations.Functions;

[TestClass]
public class AGMTests
{
    [TestMethod]
    public void FloatTest()
    {
        var g = Special.AGM(1f, MathF.Sqrt(2));
        Assert.AreEqual(1.1981403f, g);
    }

    [TestMethod]
    public void DoubleTest()
    {
        var g = Special.AGM(1.0, Math.Sqrt(2));
        Assert.AreEqual(1.1981402347355923, g);
    }

    [TestMethod]
    public void QuadTest()
    {
        var g = Special.AGM(Quad.One, Quad.Sqrt(2));
        Assert.AreEqual(new Quad(false, 1, 0x995CA8C21B065D63, 0x3E8B9BD8606839E9), g);
    }

    [TestMethod]
    public void LimitedRationalTest()
    {
        var g = Special.AGM(LimitedRational.One, LimitedRational.Sqrt(2));
        Assert.AreEqual(new LimitedRational(new Rational(
            BigInteger.Parse("194557760987548564706757731580846236403705469755308996016253827103483936618923502135738335268689413929860971036879846889228135877448871011365604138170349255962815960645126455414699850545739088841001476408136696904193276209842304713349074793700721971"), 
            BigInteger.Parse("162383129576216868759943002891965576237721886568639300841104728092785450840225089745740086719104821434708869950472652995917941733462780683035386109800616680622511685068380352705513481575167405050421146655163946591272606484172022027231681259103886435"))), 
            g);
    }

    [TestMethod]
    public void ComplexTest()
    {
        var g = Special.AGM(Complex.ImaginaryOne, Complex.Sqrt(2));
        Assert.AreEqual(new Complex(0.7749651027053821, 0.6622731208559332), g);
    }

    [TestMethod]
    public void ComplexQuadTest()
    {
        var g = Special.AGM(ComplexQuad.I, ComplexQuad.Sqrt(2));
        Assert.AreEqual(new ComplexQuad(
            new Quad(false, 0, 0xC6641CEBA93089DF, 0x98AE25869D4618B7), 
            new Quad(false, 0, 0xA98ABB33189943DB, 0x484029074F9AE8C2)), 
            g);
    }
}