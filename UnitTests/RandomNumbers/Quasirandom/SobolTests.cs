
/*
SobolTests.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Diagnostics;
using Foundations.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.RandomNumbers
{
    [TestClass]
    public class SobolTests
    {
        [TestMethod]
        public void SobolDouble()
        {
            IUniformSource source = new Sobol(new[] { 0, 1, 3 }, 1, 3, 7);
            var g = new UniformGenerator(source);
            Assert.AreEqual(0.5, g.Double());
            Assert.AreEqual(0.25, g.Double());
            Assert.AreEqual(0.75, g.Double());
            Assert.AreEqual(0.125, g.Double());
            Assert.AreEqual(0.625, g.Double());
            Assert.AreEqual(0.375, g.Double());
            Assert.AreEqual(0.875, g.Double());
            Assert.AreEqual(0.6875, g.Double());
            Assert.AreEqual(0.1875, g.Double());
            Assert.AreEqual(0.9375, g.Double());
        }

        [TestMethod]
        public void SobolRational()
        {
            IUniformSource source = new Sobol(new[] { 0, 1, 3 }, 1, 3, 7);
            var g = new UniformGenerator(source);
            Assert.AreEqual((Rational)1 / 2, g.Rational());
            Assert.AreEqual((Rational)1 / 4, g.Rational());
            Assert.AreEqual((Rational)3 / 4, g.Rational());
            Assert.AreEqual((Rational)1 / 8, g.Rational());
            Assert.AreEqual((Rational)5 / 8, g.Rational());
            Assert.AreEqual((Rational)3 / 8, g.Rational());
            Assert.AreEqual((Rational)7 / 8, g.Rational());
            Assert.AreEqual((Rational)11 / 16, g.Rational());
            Assert.AreEqual((Rational)3 / 16, g.Rational());
            Assert.AreEqual((Rational)15 / 16, g.Rational());
        }

        [TestMethod]
        public void SobolSkip()
        {
            IUniformSource s1 = new Sobol(new[] { 0, 1, 3 }, 1, 3, 7);
            var s2 = (Sobol)s1.Clone();
            for (int i = 0; i < 1000; i++) s1.Next();
            s2.Skip(1000);
            for (int i = 0; i < 1000; i++)
                Assert.AreEqual(s1.Next(), s2.Next());
        }
    }
}