
/*
PolyGF2Tests.cs

*/

using System.Diagnostics;
using Foundations.RandomNumbers;

namespace Foundations.Types
{
    /// <summary />
    [TestClass]
    public class PolyGF2Tests
    {
        /// <summary />
        [TestMethod]
        public void PolyGF2GetAll()
        {
            foreach (var p in PolyGF2.GetAll().Take(100))
            {
                Trace.WriteLine(p);
            }
        }

        [TestMethod]
        public void PolyGF2Multiply()
        {
            var ps = PolyGF2.GetAll().Take(1000).ToArray();
            var g = new Generator("PolyGF2Multiply");

            for (int i = 0; i < 100; i++)
            {
                var p = ps[g.Int32(1000)];
                var q = ps[g.Int32(1000)];
                var z = p * q;
                Assert.AreEqual(p.Degree + q.Degree, z.Degree, $"{z} = ({p})({q})");
            }
        }

        [TestMethod]
        public void PolyGF2DivNoRem()
        {
            var ps = PolyGF2.GetAll().Take(1000).ToArray();
            var g = new Generator("PolyGF2DivNoRem");

            for (int i = 0; i < 100; i++)
            {
                var p = ps[g.Int32(1000)];
                var q = ps[g.Int32(1000)];
                var z = p * q;
                PolyGF2 r;
                var d = PolyGF2.DivRem(z, p, out r);
                Assert.IsTrue(r.IsZero);
                Assert.AreEqual(q, d);
                d = PolyGF2.DivRem(z, q, out r);
                Assert.IsTrue(r.IsZero);
                Assert.AreEqual(p, d);
            }
        }

        [TestMethod]
        public void PolyGF2DivRem()
        {
            var ps = PolyGF2.GetAll().Take(1000).ToArray();
            var g = new Generator("PolyGF2DivRem");

            for (int i = 0; i < 100; i++)
            {
                var p = ps[g.Int32(1000)];
                var q = ps[g.Int32(1000)];
                PolyGF2 r;
                var d = PolyGF2.DivRem(p, q, out r);
                Assert.AreEqual(p, d * q + r);
            }
        }

        [TestMethod]
        public void PolyGF2Irreducible()
        {
            foreach (var p in PolyGF2.GetAllIrreducible().Take(1000))
            {
                Trace.WriteLine(p);
            }
        }

        [TestMethod]
        public void PolyGF2Primitive()
        {
            foreach (var p in PolyGF2.GetAllPrimitive().Take(100))
            {
                Trace.WriteLine(p);
            }
        }

        [TestMethod]
        public void PolyGF2SmallIrreducibles()
        {
            var last = 0UL;
            int len = 0;

            foreach (var p in PolyGF2.GetAllIrreducible().Take(8192))
            {
                var c = p.GetCode();
                var s = ((int)(c - last) / 2 - 1).ToString() + ",";
                if (len + s.Length > 100) { Trace.WriteLine(""); len = 0; }
                Trace.Write(s);
                len += s.Length;
                last = c;
            }
        }

        [TestMethod]
        public void PolyGF2SmallIrreduciblesCheck()
        {
            var p1 = PolyGF2.GetAllIrreducible().Take(1000).GetEnumerator();
            var p2 = PolyGF2.SmallIrreducibles().Take(1000).GetEnumerator();

            while (p1.MoveNext() && p2.MoveNext())
            {
                Assert.AreEqual(p1.Current, p2.Current);
            }
        }

        [TestMethod]
        public void SobolCoefficients()
        {
            Trace.WriteLine("s\ta\tP");

            foreach (var p in PolyGF2.GetAllPrimitive().Take(100))
            {
                var a = p.GetCode();
                a ^= 1ul << p.Degree;
                a >>= 1;
                Trace.WriteLine($"{p.Degree}\t{a}\t{p}");
            }
        }
    }
}