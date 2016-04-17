
/*
Rational.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.UnitTests.Types
{
    [TestClass]
    public class RationalTests
    {
        [TestMethod]
        public void RationalToDecimal()
        {
            var r = new Rational(1, 7);
            var d = (decimal)r;
            Assert.AreEqual(1m / 7m, d);
        }

        [TestMethod]
        public void ApproximateString()
        {
            Assert.AreEqual(new Rational(1, 7), Rational.Approximate("0.1"));
            Assert.AreEqual(new Rational(1, 10), Rational.Approximate("0.10"));
            Assert.AreEqual(new Rational(2, 17), Rational.Approximate("0.12"));
            Assert.AreEqual(new Rational(10, 89), Rational.Approximate("0.112"));
            Assert.AreEqual(new Rational(859, 7730), Rational.Approximate("0.111125"));
            Assert.AreEqual(new Rational(8890, 80009), Rational.Approximate("0.111112"));
            Assert.AreEqual(new Rational(91, 272), Rational.Approximate(".33456"));
            Assert.AreEqual(new Rational(22, 7), Rational.Approximate("3.14"));
        }

        [TestMethod]
        public void ApproximateDecimal()
        {
            Assert.AreEqual(new Rational(1, 7), Rational.Approximate(.1m));
            Assert.AreEqual(new Rational(1, 10), Rational.Approximate(.10m));
            Assert.AreEqual(new Rational(2, 17), Rational.Approximate(.12m));
            Assert.AreEqual(new Rational(10, 89), Rational.Approximate(.112m));
            Assert.AreEqual(new Rational(859, 7730), Rational.Approximate(.111125m));
            Assert.AreEqual(new Rational(8890, 80009), Rational.Approximate(.111112m));
            Assert.AreEqual(new Rational(91, 272), Rational.Approximate(.33456m));
            Assert.AreEqual(new Rational(22, 7), Rational.Approximate(3.14m));
        }
    }
}