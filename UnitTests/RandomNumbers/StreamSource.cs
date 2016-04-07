
/*
StreamSource.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
using Foundations.RandomNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Foundations.UnitTests.Objects
{
    [TestClass]
    public class StreamSourceTests
    {
        [TestMethod]
        public void MemoryStream()
        {
            const int N = 1000000;
            var gen1 = new Generator();
            byte[] data;

            using (var mem = new MemoryStream())
            {
                for (int i = 0; i < N; i++)
                    mem.WriteByte(gen1.Byte());

                mem.Position = 0;
                var gen2 = new Generator(new StreamSource(mem));
                data = new byte[N];
                gen2.Fill(data);
                Assert.AreEqual(N, mem.Position);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 250);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InsufficientStream()
        {
            using (var mem = new MemoryStream())
            {
                var gen = new Generator(new StreamSource(mem));
                gen.Byte();
            }
        }

        [TestMethod]
        public void CloneStreamSource()
        {
            var s = new StreamSource(Stream.Null);
            var c = s.Clone();
            Assert.IsNull(c);
        }
    }
}
