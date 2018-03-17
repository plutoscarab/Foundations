
/*
DecimalConstantsTests.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations
{
    /// <summary />
    [TestClass]
    public class DecimalConstantsTests
    {
        /// <summary />
        [TestMethod]
        public void PiTest()
        {
            var d = DecimalConstants.π - DecimalConstants.Sqrtπ * DecimalConstants.Sqrtπ;
            Assert.AreEqual(0m, d / 4m);
            d = DecimalConstants.π / 2m - DecimalConstants.SqrtHalfπ * DecimalConstants.SqrtHalfπ;
            Assert.AreEqual(0m, d / 4m);
            d = DecimalConstants.π * 2m - DecimalConstants.Sqrt2π * DecimalConstants.Sqrt2π;
            Assert.AreEqual(0m, d / 4m);
            var s = DecimalConstants.π;
            var t = s;

            for (int i = 1; t != 0m; i++)
            {
                t *= -DecimalConstants.π * DecimalConstants.π / (2 * i) / (2 * i + 1);
                s += t;
            }

            Assert.AreEqual(0, s / 10m);
        }

        [TestMethod]
        public void ETest()
        {
            var s = 1m;
            var d = 1m;

            for (int i = 1; i < 28; i++)
            {
                d *= i;
                s += 1m / d;
            }

            Assert.AreEqual(s, DecimalConstants.e);
        }

        /// <summary />
        [TestMethod]
        public void PhiTest()
        {
            Assert.AreEqual(DecimalConstants.φ, (DecimalConstants.Sqrt5 + 1m) / 2m);
            var d = 5m - DecimalConstants.Sqrt5 * DecimalConstants.Sqrt5;
            Assert.AreEqual(0m, d / 4m);
        }

        /// <summary />
        [TestMethod]
        public void Sqrt2Test()
        {
            Assert.AreEqual(2m, DecimalConstants.Sqrt2 * DecimalConstants.Sqrt2);
        }

        /// <summary />
        [TestMethod]
        public void GammaTest()
        {
            var s = -DecimalConstants.γ;

            for (int i = 1; i < 10000; i++)
            {
                s += 1m / i;
            }

            s -= (decimal)Math.Log(9999);
            Assert.IsTrue(s < 0.0001m);
            Assert.IsTrue(s > -0.0001m);
        }
    }
}