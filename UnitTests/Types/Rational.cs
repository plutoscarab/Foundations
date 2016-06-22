
/*
Rational.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.RandomNumbers;
using Foundations.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

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
            Assert.AreEqual(new Rational(1, 9), Rational.Approximate(.11m));
            Assert.AreEqual(new Rational(8890, 80009), Rational.Approximate(.111112m));
            Assert.AreEqual(new Rational(859, 7730), Rational.Approximate(.111125m));
            Assert.AreEqual(new Rational(10, 89), Rational.Approximate(.112m));
            Assert.AreEqual(new Rational(2, 17), Rational.Approximate(.12m));
            Assert.AreEqual(new Rational(1, 8), Rational.Approximate(.13m));
            Assert.AreEqual(new Rational(1, 7), Rational.Approximate(.14m));
            Assert.AreEqual(new Rational(2, 13), Rational.Approximate(.15m));
            Assert.AreEqual(new Rational(3, 19), Rational.Approximate(.16m));
            Assert.AreEqual(new Rational(1, 6), Rational.Approximate(.17m));
            Assert.AreEqual(new Rational(2, 11), Rational.Approximate(.18m));
            Assert.AreEqual(new Rational(3, 16), Rational.Approximate(.19m));
            Assert.AreEqual(new Rational(91, 272), Rational.Approximate(.33456m));
            Assert.AreEqual(new Rational(22, 7), Rational.Approximate(3.14m));
        }

        [TestMethod]
        public void ApproximateRandom()
        {
            var random = new Generator("ApproximateRandom");

            for (int i = 0; i < 100; i++)
            {
                var dec = (0.5m / random.Decimal() - 1m).ToString();
                dec = dec.Substring(0, dec.IndexOf('.') + 6);
                var x = decimal.Parse(dec);
                var min = x - 0.000005m;
                var max = x + 0.000005m;
                int d = 1;
                int n;

                while (true)
                {
                    n = (int)Math.Round(d * x);
                    var y = n / (decimal)d;
                    if (y >= min && y < max) break;
                    d++;
                }

                Assert.AreEqual(new Rational(n, d), Rational.Approximate(dec));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApproximateNull()
        {
            Rational.Approximate(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApproximateEmpty()
        {
            Rational.Approximate("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApproximateWhitespace()
        {
            Rational.Approximate(" ");
        }

        [TestMethod]
        public void ApproximateInteger()
        {
            var n = BigInteger.Pow(3, 100);
            var r = Rational.Approximate(n.ToString());
            Assert.AreEqual(r.P, n);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApproximateNonNumberWithDecimal()
        {
            var r = Rational.Approximate("a.b");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApproximateNonNumberWithoutDecimal()
        {
            var r = Rational.Approximate("ab");
        }

        [TestMethod]
        public void RationalFromPQ()
        {
            var r = new Rational(51, 17);
            Assert.AreEqual((BigInteger)3, r.P);
            Assert.AreEqual((BigInteger)1, r.Q);
        }

        [TestMethod]
        public void RationalIs()
        {
            Assert.IsTrue(new Rational(0, 5).IsZero);
            Assert.IsTrue(Rational.NegativeZero.IsNegativeZero);
            Assert.IsTrue((Rational.One / Rational.Zero).IsInfinity);
            Assert.IsTrue((Rational.One / Rational.NegativeZero).IsNegativeInfinity);
            Assert.IsTrue(Rational.PositiveInfinity.IsPositiveInfinity);
            Assert.IsTrue((Rational.Zero / Rational.Zero).IsNaN);
        }

        [TestMethod]
        public void RationalIsnt()
        {
            Assert.IsFalse(new Rational(1, 5).IsZero);
            Assert.IsFalse(Rational.Zero.IsNegativeZero);
            Assert.IsFalse(Rational.One.IsNegativeZero);
            Assert.IsFalse(Rational.Zero.IsInfinity);
            Assert.IsFalse(Rational.One.IsInfinity);
            Assert.IsFalse(Rational.PositiveInfinity.IsNegativeInfinity);
            Assert.IsFalse(Rational.NegativeInfinity.IsPositiveInfinity);
            Assert.IsFalse(Rational.Zero.IsNaN);
            Assert.IsFalse(Rational.One.IsNaN);
            Assert.IsFalse(Rational.PositiveInfinity.IsNaN);
        }

        [TestMethod]
        public void RationalToString()
        {
            Assert.AreEqual("1/3", new Rational(1, 3).ToString());
            Assert.AreEqual("-1/3", new Rational(-1, 3).ToString());
            Assert.AreEqual("inf", Rational.PositiveInfinity.ToString());
            Assert.AreEqual("-inf", Rational.NegativeInfinity.ToString());
            Assert.AreEqual("3", ((Rational)3).ToString());
            Assert.AreEqual("0", Rational.Zero.ToString());
            Assert.AreEqual("-0", Rational.NegativeZero.ToString());
            Assert.AreEqual("NaN", Rational.NaN.ToString());
        }

        [TestMethod]
        public void RationalEqualsNonRational()
        {
            Assert.IsFalse(Rational.One.Equals("house"));
        }

        [TestMethod]
        public void RationalHashCode()
        {
            Assert.AreNotEqual(Rational.Zero.GetHashCode(), Rational.One.GetHashCode());
        }

        [TestMethod]
        public void RationalAdd()
        {
            Assert.AreEqual(new Rational(22, 15), new Rational(2, 3) + new Rational(4, 5));
        }

        [TestMethod]
        public void RationalNotEquals()
        {
            Assert.IsFalse(new Rational(1, 3) != new Rational(2, 6));
            Assert.IsTrue(new Rational(1, 3) != new Rational(3, 6));
        }

        [TestMethod]
        public void RationalLessOrEqual()
        {
            Assert.IsTrue(new Rational(1, 2) <= new Rational(1, 2));
            Assert.IsTrue(new Rational(1, 2) <= new Rational(2, 3));
            Assert.IsFalse(new Rational(1, 2) <= new Rational(1, 3));
        }

        [TestMethod]
        public void RationalReciprocal()
        {
            Assert.AreEqual((Rational)3, Rational.Reciprocal(new Rational(1, 3)));
        }

        [TestMethod]
        public void RationalInfinityArithmetic()
        {
            Assert.AreEqual(Rational.NaN, Rational.PositiveInfinity + Rational.PositiveInfinity);
            Assert.AreEqual(Rational.PositiveInfinity, Rational.PositiveInfinity * Rational.PositiveInfinity);
            Assert.AreEqual(Rational.NaN, Rational.PositiveInfinity - Rational.PositiveInfinity);
            Assert.AreEqual(Rational.NaN, Rational.PositiveInfinity / Rational.PositiveInfinity);

            Assert.AreEqual(Rational.NaN, Rational.NegativeInfinity + Rational.NegativeInfinity);
            Assert.AreEqual(Rational.PositiveInfinity, Rational.NegativeInfinity * Rational.NegativeInfinity);
            Assert.AreEqual(Rational.NaN, Rational.NegativeInfinity - Rational.NegativeInfinity);
            Assert.AreEqual(Rational.NaN, Rational.NegativeInfinity / Rational.NegativeInfinity);

            Assert.AreEqual(Rational.NaN, Rational.PositiveInfinity + Rational.NegativeInfinity);
            Assert.AreEqual(Rational.NegativeInfinity, Rational.PositiveInfinity * Rational.NegativeInfinity);
            Assert.AreEqual(Rational.NaN, Rational.PositiveInfinity - Rational.NegativeInfinity);
            Assert.AreEqual(Rational.NaN, Rational.PositiveInfinity / Rational.NegativeInfinity);

            Assert.AreEqual(Rational.NaN, Rational.NegativeInfinity + Rational.PositiveInfinity);
            Assert.AreEqual(Rational.NegativeInfinity, Rational.NegativeInfinity * Rational.PositiveInfinity);
            Assert.AreEqual(Rational.NaN, Rational.NegativeInfinity - Rational.PositiveInfinity);
            Assert.AreEqual(Rational.NaN, Rational.NegativeInfinity / Rational.PositiveInfinity);

            Assert.AreEqual(Rational.NegativeInfinity, -Rational.PositiveInfinity);
            Assert.AreEqual(Rational.PositiveInfinity, -Rational.NegativeInfinity);

            Assert.AreEqual(Rational.PositiveInfinity, Rational.Zero + Rational.PositiveInfinity);
            Assert.AreEqual(Rational.NaN, Rational.Zero * Rational.PositiveInfinity);
            Assert.AreEqual(Rational.NegativeInfinity, Rational.Zero - Rational.PositiveInfinity);
            Assert.AreEqual(Rational.Zero, Rational.Zero / Rational.PositiveInfinity);

            Assert.AreEqual(Rational.PositiveInfinity, Rational.PositiveInfinity + Rational.Zero);
            Assert.AreEqual(Rational.NaN, Rational.PositiveInfinity * Rational.Zero);
            Assert.AreEqual(Rational.PositiveInfinity, Rational.PositiveInfinity - Rational.Zero);
            Assert.AreEqual(Rational.PositiveInfinity, Rational.PositiveInfinity / Rational.Zero);

            Assert.AreEqual(Rational.PositiveInfinity, Rational.One + Rational.PositiveInfinity);
            Assert.AreEqual(Rational.PositiveInfinity, Rational.One * Rational.PositiveInfinity);
            Assert.AreEqual(Rational.NegativeInfinity, Rational.One - Rational.PositiveInfinity);
            Assert.AreEqual(Rational.Zero, Rational.One / Rational.PositiveInfinity);

            Assert.AreEqual(Rational.PositiveInfinity, Rational.PositiveInfinity + Rational.One);
            Assert.AreEqual(Rational.PositiveInfinity, Rational.PositiveInfinity * Rational.One);
            Assert.AreEqual(Rational.PositiveInfinity, Rational.PositiveInfinity - Rational.One);
            Assert.AreEqual(Rational.PositiveInfinity, Rational.PositiveInfinity / Rational.One);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RationalCompareNaN()
        {
            Rational.Compare(Rational.Zero, Rational.NaN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RationalSignNaN()
        {
            Rational.Sign(Rational.NaN);
        }

        [TestMethod]
        public void RationalSignOfInfinity()
        {
            Assert.AreEqual(1, Rational.Sign(Rational.PositiveInfinity));
            Assert.AreEqual(-1, Rational.Sign(Rational.NegativeInfinity));
        }

        [TestMethod]
        public void RationalMin()
        {
            Assert.AreEqual(new Rational(1, 3), Rational.Min(new Rational(1, 3), new Rational(1, 2)));
            Assert.AreEqual(new Rational(1, 4), Rational.Min(new Rational(1, 3), new Rational(1, 4)));
        }

        [TestMethod]
        public void RationalMax()
        {
            Assert.AreEqual(new Rational(1, 3), Rational.Max(new Rational(1, 3), new Rational(1, 4)));
            Assert.AreEqual(new Rational(1, 2), Rational.Max(new Rational(1, 3), new Rational(1, 2)));
        }

        [TestMethod]
        public void RationalIntPower()
        {
            Assert.AreEqual(new Rational(BigInteger.Pow(2, 20), BigInteger.Pow(3, 20)), Rational.Pow(new Rational(2, 3), 20));
        }

        [TestMethod]
        public void RationalFromDouble()
        {
            Assert.AreEqual(new Rational(2, 3), (Rational)(2d / 3d));
        }

        [TestMethod]
        public void RationalFromDecimal()
        {
            Assert.AreEqual(new Rational(2, 3), (Rational)(2m / 3m));
        }
    }
}