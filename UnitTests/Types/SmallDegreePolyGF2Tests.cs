
/*
SmallDegreePolyGF2Tests.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Diagnostics;
using System.Linq;
using Foundations.UnitTesting;

namespace Foundations.Types
{
    /// <summary />
    [TestClass]
    public class SmallDegreePolyGF2Tests
    {
        /// <summary />
        [TestMethod]
        public void SmallDegreePolyGF2DegreeTest()
        {
            var p = new SmallDegreePolyGF2(0x17Bul);
            Assert.AreEqual(8, p.Degree);
        }

        [TestMethod]
        public void SmallDegreePolyGF2MultTest()
        {
            var p = new SmallDegreePolyGF2(0, 2, 5, 17);
            var q = new SmallDegreePolyGF2(0, 2, 3);
            var z = p * q;
            Assert.AreEqual(20, z.Degree);
            Assert.AreEqual(0x1A0199ul, z.Coefficients);
        }

        [TestMethod]
        public void SmallDegreePolyGF2DivRemTest()
        {
            var p = new SmallDegreePolyGF2(0x1A0199ul);
            var q = new SmallDegreePolyGF2(0, 2, 3);
            SmallDegreePolyGF2 r;
            var d = SmallDegreePolyGF2.DivRem(p, q, out r);
            Assert.AreEqual(0ul, r.Coefficients);
            Assert.AreEqual(0x20025ul, d.Coefficients);
            d = SmallDegreePolyGF2.DivRem(p, d, out r);
            Assert.AreEqual(0ul, r.Coefficients);
            Assert.AreEqual(0xDul, d.Coefficients);
        }

        //[TestMethod]
        public void SmallDegreePolyGF2PrimesTest()
        {
            var last = 3ul;
            int len = 0;

            foreach (var p in SmallDegreePolyGF2.Primes().Skip(2))
            {
                if (p.Coefficients > ushort.MaxValue)
                    break;

                var s = ((p.Coefficients - last) / 2 - 1).ToString() + ", ";

                if (len + s.Length > 100)
                {
                    Trace.WriteLine("");
                    len = 0;
                }

                Trace.Write(s);
                len += s.Length;
                last = p.Coefficients;
            }
        }

        [TestMethod]
        public void SmallDegreePolyGF2FactorsTest()
        {
            foreach (var p in Enumerable.Range(2, 100).Select(i => new SmallDegreePolyGF2((ulong)i)))
            {
                Trace.Write($"{p} = ");

                foreach (var f in p.GetFactors())
                {
                    if (f.Value.Coefficients == 2)
                        Trace.Write(f.Value);
                    else
                        Trace.Write($"({f.Value})");
                    Trace.Write(SmallDegreePolyGF2.Superscript(f.Exponent));
                }

                Trace.WriteLine("");
            }
        }

        [TestMethod]
        public void SobolCoefficients()
        {
            Trace.WriteLine("s\ta\tP");
            foreach (var p in SmallDegreePolyGF2.Primes().Skip(1).Take(1000))
            {
                var a = p.Coefficients;
                a ^= 1ul << p.Degree;
                a >>= 1;
                Trace.WriteLine($"{p.Degree}\t{a}\t{p}");
            }
        }
    }
}