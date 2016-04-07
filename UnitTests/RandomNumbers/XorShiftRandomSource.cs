
/*
XorShiftRandomSourceTests.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.RandomNumbers;
using Foundations.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Foundations.UnitTests.Objects
{
    [TestClass]
    public class XorShiftRandomSourceTests
    {
        [TestMethod]
        public void CloneTest()
        {
            var source = new XorShiftRandomSource();
            SHA256RandomSourceTests.CloneTest(source);
        }

        [TestMethod]
        public void Speed()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source);
            random.UInt64();
            var value = new ValueUnion();
            const int N = 10000000;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < N; i++) source.Next(ref value);
            Trace.WriteLine(sw.Elapsed.TotalSeconds * 1e9 / N + "ns/64b, " + N * sizeof(ulong) / sw.Elapsed.TotalSeconds / 1e9 + "GB/s");
        }

        [TestMethod]
        public void GeneratorOverhead()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source);
            random.UInt64();
            const int N = 10000000;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < N; i++) random.UInt64();
            Trace.WriteLine(sw.Elapsed.TotalSeconds * 1e9 / N + "ns/64b, " + N * sizeof(ulong) / sw.Elapsed.TotalSeconds / 1e9 + "GB/s");
        }
    }
}
