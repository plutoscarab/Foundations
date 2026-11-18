
using Foundations.Types;

namespace Foundations;

[TestClass]
public class ContinuedFractionTests
{
    [TestMethod]
    public void UniformRandomKolmogorovSmirnovTest()
    {
        var sample = UniformRandomSample();
        var statistic = 0.0;

        for (var i = 0; i < sample.Length; i++)
        {
            var lowerDifference = sample[i] - (double)i / sample.Length;
            var upperDifference = (double)(i + 1) / sample.Length - sample[i];
            statistic = Math.Max(statistic, Math.Max(lowerDifference, upperDifference));
        }

        // The DKW bound gives a conservative, finite-sample 1% cutoff.
        var criticalValue = Math.Sqrt(Math.Log(200.0) / (2 * sample.Length));
        Console.WriteLine($"Kolmogorov-Smirnov D = {statistic:R}, cutoff = {criticalValue:R}");
        Assert.IsTrue(statistic < criticalValue,
            $"Kolmogorov-Smirnov D = {statistic:R} exceeds {criticalValue:R}.");
    }

    [TestMethod]
    public void UniformRandomCramerVonMisesTest()
    {
        var sample = UniformRandomSample();
        var statistic = 1.0 / (12 * sample.Length);

        for (var i = 0; i < sample.Length; i++)
        {
            var difference = sample[i] - (2.0 * i + 1) / (2 * sample.Length);
            statistic += difference * difference;
        }

        // Asymptotic 1% cutoff for a fully specified continuous distribution.
        // https://twa.ckirby.su.domains/papers/2010_Anderson-Darling.pdf
        const double criticalValue = 0.74346;
        Console.WriteLine($"Cramer-von Mises W^2 = {statistic:R}, cutoff = {criticalValue:R}");
        Assert.IsTrue(statistic < criticalValue,
            $"Cramer-von Mises W^2 = {statistic:R} exceeds {criticalValue:R}.");
    }

    [TestMethod]
    public void UniformRandomAndersonDarlingTest()
    {
        var sample = UniformRandomSample();
        var sum = 0.0;

        for (var i = 0; i < sample.Length; i++)
        {
            // A zero observation contributes an infinite statistic, without clipping.
            sum += (2.0 * i + 1) *
                (Math.Log(sample[i]) + Math.Log(1 - sample[sample.Length - 1 - i]));
        }

        var statistic = -sample.Length - sum / sample.Length;

        // Asymptotic 1% cutoff for a fully specified continuous distribution.
        // https://twa.ckirby.su.domains/papers/2010_Anderson-Darling.pdf
        const double criticalValue = 3.880;
        Console.WriteLine($"Anderson-Darling A^2 = {statistic:R}, cutoff = {criticalValue:R}");
        Assert.IsTrue(statistic < criticalValue,
            $"Anderson-Darling A^2 = {statistic:R} exceeds {criticalValue:R}.");
    }

    private static double[] UniformRandomSample()
    {
        const int seed = 42;
        const int sampleSize = 1000;
        const int terms = 10;
        var random = new Random(seed);
        var sample = new double[sampleSize];

        for (var i = 0; i < sample.Length; i++)
        {
            sample[i] = (double)ContinuedFraction.UniformRandom(random, terms);
            Assert.IsTrue(double.IsFinite(sample[i]) && sample[i] >= 0 && sample[i] < 1,
                $"Sample {i} is outside [0, 1): {sample[i]:R} (seed {seed}).");
        }

        Array.Sort(sample);
        return sample;
    }

    [TestMethod]
    public void ToStringTest()
    {
        ContinuedFraction cf = new([1, 2, 3, 4, 5]);
        Assert.AreEqual("[1; 2, 3, 4, 5]", cf.ToString());
    }

    [TestMethod]
    public void FromDoubleTest()
    {
        ContinuedFraction cf = new(1.75);
        Assert.AreEqual("[1; 1, 3]", cf.ToString());
    }

    [TestMethod]
    public void FromFloatTest()
    {
        ContinuedFraction cf = new(1.75f);
        Assert.AreEqual("[1; 1, 3]", cf.ToString());
    }

    [TestMethod]
    public void ToDigitsTest()
    {
        ContinuedFraction cf = new([1, 1, 3]);
        var digits = cf.ToDigits();
        Assert.AreEqual("1.75", digits);
    }

    [TestMethod]
    public void PowTest()
    {
        var x = new ContinuedFraction(0.9m).Pow(20);
        Assert.AreEqual(0.12157665459056928801m, (decimal)x);
    }
}