
/*
ComparisonTable.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.Coding
{
    /// <summary />
    [TestClass]
    public class ComparisonTable
    {
        /// <summary />
        [TestMethod]
        public void GenerateComparisonTable()
        {
            var es = new[] { Codes.EliasDelta, Codes.EliasFibonacci, Codes.EliasGamma, Codes.EliasOmega, Codes.Fibonacci, Codes.Levenshtein };

            foreach (var e in es)
            {
                Trace.WriteLine(e.GetType().Name);

                foreach (var k in FindBreaks(e))
                    Trace.WriteLine($"{k.Item1}\t{k.Item2}");

                Trace.WriteLine("");
            }
        }

        private IEnumerable<Tuple<int, int>> FindBreaks(IBitEncoding e)
        {
            Code min = e.GetCode(e.MinEncodable);
            yield return Tuple.Create(e.MinEncodable, min.Length);
            Code max = e.GetCode(e.MaxEncodable);

            foreach (var k in FindBreaks(e, e.MinEncodable, min, e.MaxEncodable, max))
                yield return k;
        }

        private IEnumerable<Tuple<int, int>> FindBreaks(IBitEncoding e, int imin, Code min, int imax, Code max)
        {
            if (imax == imin + 1)
            {
                yield return Tuple.Create(imax, max.Length);
                yield break;
            }

            int imid = imin + (imax - imin) / 2;
            Code mid = e.GetCode(imid);

            if (mid.Length > min.Length)
            {
                foreach (var k in FindBreaks(e, imin, min, imid, mid))
                    yield return k;
            }

            if (max.Length > mid.Length)
            {
                foreach (var k in FindBreaks(e, imid, mid, imax, max))
                    yield return k;
            }
        }
    }
}