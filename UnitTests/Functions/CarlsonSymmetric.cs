
/*
CarlsonSymmetric.cs

Copyright (c) 2016 Pluto Scarab Software. All Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

using static Foundations.Functions.Numerics.CarlsonSymmetric;

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
            var d = RF(0, 5, 0);
            Assert.IsTrue(double.IsInfinity(d));
            var c = RF(3, Complex.Zero, 0);
            Assert.IsTrue(double.IsInfinity(c.Real));
            Assert.IsTrue(double.IsInfinity(RF(4, 0, 0)));
            Assert.IsTrue(double.IsInfinity(RF(0, 0, 3)));
            Assert.IsTrue(double.IsInfinity(RF(0, Complex.Zero, 2).Real));
            Assert.IsTrue(double.IsInfinity(RF(Complex.Zero, 1, 0).Real));
        }
    }
}
