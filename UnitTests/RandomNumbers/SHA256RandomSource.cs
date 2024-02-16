
/*
SHA256RandomSourceTests.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.RandomNumbers;
using Foundations.Types;
using Foundations.UnitTesting;
using System;

namespace Foundations.UnitTests.Objects
{
    [TestClass]
    public class SHA256RandomSourceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UseAfterDispose()
        {
            var source = new SHA256RandomSource();
            source.Initialize(new byte[] { 1, 2, 3 });
            source.Dispose();
            var value = new ValueUnion();
            source.Next(ref value);
            source.Next(ref value);
            source.Next(ref value);
            source.Next(ref value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RequiresSeed()
        {
            var source = new SHA256RandomSource();
            var random = new Generator(source);
        }

        public static void CloneTest(IRandomSource source)
        {
            new Generator(source, "CloneTest").UInt64();
            var clone = source.Clone();
            Assert.IsNotNull(source);
            var sourceNext = new ValueUnion();
            var cloneNext = new ValueUnion();

            for (int i = 0; i < 100; i++)
            {
                source.Next(ref sourceNext);
                clone.Next(ref cloneNext);
                Assert.AreEqual(sourceNext.UInt64_0, cloneNext.UInt64_0);
            }
        }

        [TestMethod]
        public void CloneTest()
        {
            var source = new SHA256RandomSource();
            CloneTest(source);
        }
    }
}
