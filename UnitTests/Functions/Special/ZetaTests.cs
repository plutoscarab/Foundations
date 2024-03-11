
using static Foundations.Functions.Special;
using Foundations.Types;

namespace Foundations.Functions;

[TestClass]
public class ZetaTests
{
    [TestMethod]
    public void ZetaTest()
    {
        for (var n = 2; n <= 16; n += 2)
        {
            var z = Zeta(1.0 * n);
        }

        for (var n = 3; n <= 15; n += 2)
        {
            var z = Zeta(1.0 * n);
        }

        for (var n = 1; n <= 15; n += 2)
        {
            var z = Zeta(-1.0 * n);
        }

        for (var n = 9; n <= 9; n++)
        {
            var g = Gamma(new ComplexQuad(-n, -n));
            var s = Zeta(new ComplexQuad(-n, -n));
        }
    }
}