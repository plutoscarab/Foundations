
/*
FactorialsTests.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Foundations.Types;
using Foundations.UnitTesting;

namespace Foundations
{
    /// <summary />
    [TestClass]
    public class FactorialsTests
    {
        [TestMethod]
        public void FactorialsInt64Test()
        {
            var f = Sequences.Factorials().ToList();

            for (int i = 2; i < f.Count; i++)
                Assert.IsTrue(f[i] > f[i - 1]);

            Show(f, _ => $"{_}, ");
        }

        [TestMethod]
        public void FactorialsDoubleTest()
        {
            var f = Sequences.FactorialsD().ToList();

            for (int i = 2; i < f.Count; i++)
                Assert.IsTrue(f[i] > f[i - 1]);

            Show(f, _ => $"{_:r}, ");
        }

        [TestMethod]
        public void OverFactorialsTest()
        {
            var f = Sequences.OverFactorials().ToList();
     
            for (int i = 2; i < f.Count; i++)
                Assert.IsTrue(f[i] < f[i - 1]);

            Show(f, _ => $"{_:r}, ");
        }

        [TestMethod]
        public void FactorialsBigTest()
        {
            var g = Sequences.OverFactorials().ToList();
            var f = Sequences.FactorialsB().Take(g.Count).ToList();

            for (var i = 0; i < g.Count; i++)
            {
                var x = (double)f[i] * g[i];
                Assert.IsTrue(Math.Abs(x - 1) < 1e-15);
            }
        }

        private void Show<T>(IEnumerable<T> list, Func<T, string> formatter)
        {
            int len = 0;

            foreach (var item in list)
            {
                var s = formatter(item);
                if (len + s.Length > 80)
                {
                    Trace.WriteLine("");
                    len = 0;
                }
                Trace.Write(s);
                len += s.Length;
            }
        }
    }
}