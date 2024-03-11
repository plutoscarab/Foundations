
/*
CarlsonSymmetric.cs
*/

using System.Numerics;

using static Foundations.Functions.CarlsonSymmetric;

namespace Foundations.UnitTests.Functions
{
    [TestClass]
    public class CarlsonSymmetricTests
    {
        [TestMethod]
        public void CarlsonSymmetricRF()
        {
            // B. C. Carlson (1995) Numerical computation of real or complex elliptic integrals.  Numer. Algorithms 10 (1-2),  pp. 13–26. 
            var i = Complex.ImaginaryOne;

            var data = new double[]
            {
            //     X      Y      Z            RF
            //   re im  re im  re im  re                 im
                 1, 0,  2, 0,  0, 0,  1.3110287771461,   0,
                 0, 1,  0,-1,  0, 0,  1.8540746773014,   0,
                .5, 0,  1, 0,  0, 0,  1.8540746773014,   0,
                -1, 1,  0, 1,  0, 0,  0.79612586584234, -1.2138566698365,
                 2, 0,  3, 0,  4, 0,  0.58408284167715,  0,
                 0, 1,  0,-1,  2, 0,  1.0441445654064,   0,
                -1, 1,  0, 1,  1,-1,  0.93912050218619, -0.53296252018635,
            };

            for (int j = 0; j < data.Length; j += 8)
            {
                Complex
                    x = new Complex(data[j], data[j + 1]),
                    y = new Complex(data[j + 2], data[j + 3]),
                    z = new Complex(data[j + 4], data[j + 5]),
                    expected = new Complex(data[j + 6], data[j + 7]),
                    actual = RF(x, y, z);

                double
                    err = Complex.Abs(actual - expected);

                Assert.IsTrue(err < 1e-10);
            }
        }

        [TestMethod]
        public void CarlsonSymmetricRFInfinity()
        {
            var d = RF(0.0, 5, 0);
            Assert.IsTrue(double.IsInfinity(d));
            var c = RF(3, Complex.Zero, 0);
            Assert.IsTrue(double.IsInfinity(c.Real));
            Assert.IsTrue(double.IsInfinity(RF(4.0, 0, 0)));
            Assert.IsTrue(double.IsInfinity(RF(0.0, 0, 3)));
            Assert.IsTrue(double.IsInfinity(RF(0, Complex.Zero, 2).Real));
            Assert.IsTrue(double.IsInfinity(RF(Complex.Zero, 1, 0).Real));
        }

        [TestMethod]
        public void CarlsonSymmetricRD()
        {
            var i = Complex.ImaginaryOne;

            var data = new double[]
            {
            //     X      Y      Z            RD
            //   re im   re im  re  im    re                 im
                 0,  0,  2,  0,  1,  0,   1.7972103521034,   0,
                 2,  0,  3,  0,  4,  0,   0.16510527294261,  0,
                 0,  1,  0, -1,  2,  0,   0.65933854154220,  0,
                 0,  0,  0,  1,  0, -1,   1.2708196271910,   2.7811120159521,
                 0,  0, -1,  1,  0,  1,  -1.8577235439239,  -0.96193450888839,
                -2, -1, 0, -1,  -1,  1,   1.8249027393704,  -1.2218475784827,
            };

            for (int j = 0; j < data.Length; j += 8)
            {
                Complex
                    x = new Complex(data[j], data[j + 1]),
                    y = new Complex(data[j + 2], data[j + 3]),
                    z = new Complex(data[j + 4], data[j + 5]),
                    expected = new Complex(data[j + 6], data[j + 7]),
                    actual = RD(x, y, z);

                double
                    err = Complex.Abs(actual - expected);

                Assert.IsTrue(err < 1e-9);
            }
        }

        [TestMethod]
        public void CarlsonSymmetricRJ()
        {
            var i = Complex.ImaginaryOne;

            var data = new double[]
            {
            //     X      Y      Z       P              RD
            //   re im   re im   re im   re im     re                 im
                 0, 0,   1,  0,  2, 0,   3,  0,   0.77688623778582,  0,
                 2, 0,   3,  0,  4, 0,   5,  0,   0.14297579661757,  0,
                 2, 0,   3,  0,  4, 0,  -1,  1,   0.13613945827771, -0.38207561624427,
                 0, 1,   0, -1,  0, 0,   2,  0,   1.6490011662711,   0,
                -1, 1,  -1, -1,  1, 0,   2,  0,   0.94148358841220,  0,
                 0, 1,   0, -1,  0, 0,   1, -1,   1.8260115229009,   1.2290661908643,
                -1, 1,  -1, -1,  1, 0,  -3,  1,  -0.61127970812028, -1.0684038390007,
                -1, 1,  -2, -1,  0, -1, -1,  1,   1.8249027393704,  -1.2218475784827,
            };

            for (int j = 0; j < data.Length; j += 10)
            {
                Complex
                    x = new Complex(data[j], data[j + 1]),
                    y = new Complex(data[j + 2], data[j + 3]),
                    z = new Complex(data[j + 4], data[j + 5]),
                    p = new Complex(data[j + 6], data[j + 7]),
                    expected = new Complex(data[j + 8], data[j + 9]),
                    actual = RJ(x, y, z, p);

                double
                    err = Complex.Abs(actual - expected);

                Assert.IsTrue(err < 1e-9);
            }
        }
    }
}
