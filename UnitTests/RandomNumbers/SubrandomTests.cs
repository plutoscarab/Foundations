﻿
/*
SubrandomTests.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

THIS IS AN AUTO-GENERATED SOURCE FILE. DO NOT EDIT THIS FILE DIRECTLY.
INSTEAD, EDIT THE .tt FILE WITH THE SAME NAME AND RE-RUN THE TEXT TEMPLATING
FILE GENERATOR. IF YOU SAVE THE FILE IN VISUAL STUDIO IT WILL DO THIS FOR YOU.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundations.Types;

namespace Foundations.RandomNumbers
{
    /// <summary />
    [TestClass]
    public class SubrandomTests
    {
        private Double StarDiscrepency(List<Double> s)
        {
            s.Sort();
            var d = 0D;

            for (int i = 1; i < s.Count; i++)
            {
                d = Math.Max(d, Math.Abs(i / (Double)s.Count - s[i]));
            }

            return d;
        }

        private void Test(Generator g, Func<IEnumerable<Double>> sequence)
        {
            int wins = 0;

            for (int test = 0; test < 10; test++)
            {
                var s = sequence().Take(1000).ToList();
                var d = StarDiscrepency(s);
                s = g.Doubles().Take(1000).ToList();
                var d2 = StarDiscrepency(s);
                if (d < d2) wins++;
            }

            Assert.IsTrue(wins >= 7);
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceDoubleTest()
        {
            var g = new Generator("AdditiveRecurrenceDoubleTest");
            Test(g, () => Subrandom.AdditiveRecurrenceD());
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceGeneratorDoubleTest()
        {
            var g = new Generator("AdditiveRecurrenceGeneratorDoubleTest");
            Test(g, () => Subrandom.AdditiveRecurrenceD(g));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void AdditiveRecurrenceNullGeneratorDoubleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrenceD((Generator)null));
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceAlphaDoubleTest()
        {
            var g = new Generator("AdditiveRecurrenceAlphaDoubleTest");
            Test(g, () => Subrandom.AdditiveRecurrence(g.Double()));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceLowAlphaDoubleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(0D));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceHighAlphaDoubleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(1D));
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceSZeroAlphaDoubleTest()
        {
            var g = new Generator("AdditiveRecurrenceSZeroAlphaDoubleTest");
            Test(g, () => Subrandom.AdditiveRecurrence(g.Double(), g.Double()));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceSZeroLowAlphaDoubleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(-1D, 0.5D));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceSZeroHighAlphaDoubleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(1D, 0.5D));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceLowSZeroDoubleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(0.5D, 0D));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceHighSZeroDoubleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(0.5D, 1D));
        }

        /// <summary />
        [TestMethod]
        public void VanDerCorputDoubleBase2Test()
        {
            var g = new Generator("VanDerCorputDoubleBase2Test");
            Test(g, () => Subrandom.VanDerCorputD(2));
        }

        /// <summary />
        [TestMethod]
        public void VanDerCorputDoubleBase3Test()
        {
            var g = new Generator("VanDerCorputDoubleBase3Test");
            Test(g, () => Subrandom.VanDerCorputD(3));
        }

        /// <summary />
        [TestMethod]
        public void HaltonDoubleTest()
        {
            var g = new Generator("HaltonDoubleTest");
            
            foreach (var item in Subrandom.HaltonD(new[] { 2, 3 }).Take(100))
            {
                System.Diagnostics.Trace.WriteLine($"{item[0]}, {item[1]}");
            }
        }

        /// <summary />
        [TestMethod]
        public void HaltonDoublePrimesTest()
        {
            var g = new Generator("HaltonDoublePrimesTest");
            
            foreach (var item in Subrandom.HaltonD(3).Take(100))
            {
                System.Diagnostics.Trace.WriteLine($"{item[0]}, {item[1]}, {item[2]}");
            }
        }

        /// <summary />
        [TestMethod]
        public void HammersleyDoublePrimesTest()
        {
            var g = new Generator("HammersleyDoublePrimesTest");
            
            foreach (var item in Subrandom.HammersleyD(2, 256))
            {
                System.Diagnostics.Trace.WriteLine($"{item[0]}, {item[1]}");
            }
        }

        /// <summary />
        [TestMethod]
        public void SobolDoubleTest()
        {
            var p = new PolyGF2(3, 1, 0);
            var m = new[] { 1, 3, 7 };

            foreach (var item in Subrandom.SobolD(p, m).Take(1000))
            {
                System.Diagnostics.Trace.WriteLine(item);
            }
        }

        [TestMethod]
        public void SobolDoubleCodeTest()
        {
            var s = Subrandom.SobolD(13, 1, 1, 1, 3, 11);

            foreach (var item in s.Take(1000))
            {
                System.Diagnostics.Trace.WriteLine(item);
            }
        }

        private Single StarDiscrepency(List<Single> s)
        {
            s.Sort();
            var d = 0F;

            for (int i = 1; i < s.Count; i++)
            {
                d = Math.Max(d, Math.Abs(i / (Single)s.Count - s[i]));
            }

            return d;
        }

        private void Test(Generator g, Func<IEnumerable<Single>> sequence)
        {
            int wins = 0;

            for (int test = 0; test < 10; test++)
            {
                var s = sequence().Take(1000).ToList();
                var d = StarDiscrepency(s);
                s = g.Singles().Take(1000).ToList();
                var d2 = StarDiscrepency(s);
                if (d < d2) wins++;
            }

            Assert.IsTrue(wins >= 7);
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceSingleTest()
        {
            var g = new Generator("AdditiveRecurrenceSingleTest");
            Test(g, () => Subrandom.AdditiveRecurrenceF());
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceGeneratorSingleTest()
        {
            var g = new Generator("AdditiveRecurrenceGeneratorSingleTest");
            Test(g, () => Subrandom.AdditiveRecurrenceF(g));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void AdditiveRecurrenceNullGeneratorSingleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrenceF((Generator)null));
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceAlphaSingleTest()
        {
            var g = new Generator("AdditiveRecurrenceAlphaSingleTest");
            Test(g, () => Subrandom.AdditiveRecurrence(g.Single()));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceLowAlphaSingleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(0F));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceHighAlphaSingleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(1F));
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceSZeroAlphaSingleTest()
        {
            var g = new Generator("AdditiveRecurrenceSZeroAlphaSingleTest");
            Test(g, () => Subrandom.AdditiveRecurrence(g.Single(), g.Single()));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceSZeroLowAlphaSingleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(-1F, 0.5F));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceSZeroHighAlphaSingleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(1F, 0.5F));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceLowSZeroSingleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(0.5F, 0F));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceHighSZeroSingleTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(0.5F, 1F));
        }

        /// <summary />
        [TestMethod]
        public void VanDerCorputSingleBase2Test()
        {
            var g = new Generator("VanDerCorputSingleBase2Test");
            Test(g, () => Subrandom.VanDerCorputF(2));
        }

        /// <summary />
        [TestMethod]
        public void VanDerCorputSingleBase3Test()
        {
            var g = new Generator("VanDerCorputSingleBase3Test");
            Test(g, () => Subrandom.VanDerCorputF(3));
        }

        /// <summary />
        [TestMethod]
        public void HaltonSingleTest()
        {
            var g = new Generator("HaltonSingleTest");
            
            foreach (var item in Subrandom.HaltonF(new[] { 2, 3 }).Take(100))
            {
                System.Diagnostics.Trace.WriteLine($"{item[0]}, {item[1]}");
            }
        }

        /// <summary />
        [TestMethod]
        public void HaltonSinglePrimesTest()
        {
            var g = new Generator("HaltonSinglePrimesTest");
            
            foreach (var item in Subrandom.HaltonF(3).Take(100))
            {
                System.Diagnostics.Trace.WriteLine($"{item[0]}, {item[1]}, {item[2]}");
            }
        }

        /// <summary />
        [TestMethod]
        public void HammersleySinglePrimesTest()
        {
            var g = new Generator("HammersleySinglePrimesTest");
            
            foreach (var item in Subrandom.HammersleyF(2, 256))
            {
                System.Diagnostics.Trace.WriteLine($"{item[0]}, {item[1]}");
            }
        }

        /// <summary />
        [TestMethod]
        public void SobolSingleTest()
        {
            var p = new PolyGF2(3, 1, 0);
            var m = new[] { 1, 3, 7 };

            foreach (var item in Subrandom.SobolF(p, m).Take(1000))
            {
                System.Diagnostics.Trace.WriteLine(item);
            }
        }

        [TestMethod]
        public void SobolSingleCodeTest()
        {
            var s = Subrandom.SobolF(13, 1, 1, 1, 3, 11);

            foreach (var item in s.Take(1000))
            {
                System.Diagnostics.Trace.WriteLine(item);
            }
        }

        private Decimal StarDiscrepency(List<Decimal> s)
        {
            s.Sort();
            var d = 0M;

            for (int i = 1; i < s.Count; i++)
            {
                d = Math.Max(d, Math.Abs(i / (Decimal)s.Count - s[i]));
            }

            return d;
        }

        private void Test(Generator g, Func<IEnumerable<Decimal>> sequence)
        {
            int wins = 0;

            for (int test = 0; test < 10; test++)
            {
                var s = sequence().Take(1000).ToList();
                var d = StarDiscrepency(s);
                s = g.Decimals().Take(1000).ToList();
                var d2 = StarDiscrepency(s);
                if (d < d2) wins++;
            }

            Assert.IsTrue(wins >= 7);
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceDecimalTest()
        {
            var g = new Generator("AdditiveRecurrenceDecimalTest");
            Test(g, () => Subrandom.AdditiveRecurrenceM());
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceGeneratorDecimalTest()
        {
            var g = new Generator("AdditiveRecurrenceGeneratorDecimalTest");
            Test(g, () => Subrandom.AdditiveRecurrenceM(g));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void AdditiveRecurrenceNullGeneratorDecimalTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrenceM((Generator)null));
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceAlphaDecimalTest()
        {
            var g = new Generator("AdditiveRecurrenceAlphaDecimalTest");
            Test(g, () => Subrandom.AdditiveRecurrence(g.Decimal()));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceLowAlphaDecimalTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(0M));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceHighAlphaDecimalTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(1M));
        }

        /// <summary />
        [TestMethod]
	    public void AdditiveRecurrenceSZeroAlphaDecimalTest()
        {
            var g = new Generator("AdditiveRecurrenceSZeroAlphaDecimalTest");
            Test(g, () => Subrandom.AdditiveRecurrence(g.Decimal(), g.Decimal()));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceSZeroLowAlphaDecimalTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(-1M, 0.5M));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceSZeroHighAlphaDecimalTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(1M, 0.5M));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceLowSZeroDecimalTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(0.5M, 0M));
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void AdditiveRecurrenceHighSZeroDecimalTest()
        {
            Test(null, () => Subrandom.AdditiveRecurrence(0.5M, 1M));
        }

        /// <summary />
        [TestMethod]
        public void VanDerCorputDecimalBase2Test()
        {
            var g = new Generator("VanDerCorputDecimalBase2Test");
            Test(g, () => Subrandom.VanDerCorputM(2));
        }

        /// <summary />
        [TestMethod]
        public void VanDerCorputDecimalBase3Test()
        {
            var g = new Generator("VanDerCorputDecimalBase3Test");
            Test(g, () => Subrandom.VanDerCorputM(3));
        }

        /// <summary />
        [TestMethod]
        public void HaltonDecimalTest()
        {
            var g = new Generator("HaltonDecimalTest");
            
            foreach (var item in Subrandom.HaltonM(new[] { 2, 3 }).Take(100))
            {
                System.Diagnostics.Trace.WriteLine($"{item[0]}, {item[1]}");
            }
        }

        /// <summary />
        [TestMethod]
        public void HaltonDecimalPrimesTest()
        {
            var g = new Generator("HaltonDecimalPrimesTest");
            
            foreach (var item in Subrandom.HaltonM(3).Take(100))
            {
                System.Diagnostics.Trace.WriteLine($"{item[0]}, {item[1]}, {item[2]}");
            }
        }

        /// <summary />
        [TestMethod]
        public void HammersleyDecimalPrimesTest()
        {
            var g = new Generator("HammersleyDecimalPrimesTest");
            
            foreach (var item in Subrandom.HammersleyM(2, 256))
            {
                System.Diagnostics.Trace.WriteLine($"{item[0]}, {item[1]}");
            }
        }

        /// <summary />
        [TestMethod]
        public void SobolDecimalTest()
        {
            var p = new PolyGF2(3, 1, 0);
            var m = new[] { 1, 3, 7 };

            foreach (var item in Subrandom.SobolM(p, m).Take(1000))
            {
                System.Diagnostics.Trace.WriteLine(item);
            }
        }

        [TestMethod]
        public void SobolDecimalCodeTest()
        {
            var s = Subrandom.SobolM(13, 1, 1, 1, 3, 11);

            foreach (var item in s.Take(1000))
            {
                System.Diagnostics.Trace.WriteLine(item);
            }
        }

    }
}
