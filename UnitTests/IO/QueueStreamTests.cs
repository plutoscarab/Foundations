
/*
QueueStreamTests.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Threading;
using System.Threading.Tasks;
using Foundations.RandomNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.IO
{
    [TestClass]
    public class QueueStreamTests
    {
        [TestMethod]
        public void QueueStreamTest()
        {
            var stream = new QueueStream(1000);
            int numWrites = 0;
            int numReads = 0;

            var writer = Task.Run(async () =>
            {
                var g = new Generator();
                var b = new byte[1000];
                byte k = 0;

                while (true)
                {
                    int n = g.Int32(1, 999);
                    for (int i = 0; i < n; i++) b[i] = k++;
                    await stream.WriteAsync(b, 0, n);
                    numWrites++;
                }
            });

            var reader = Task.Run(async () =>
            {
                var g = new Generator();
                var b = new byte[1000];
                byte k = 0;

                while (true)
                {
                    int n = g.Int32(1, 499);
                    n = await stream.ReadAsync(b, 0, n);
                    numReads++;

                    for (int i = 0; i < n; i++)
                    {
                        Assert.AreEqual(k++, b[i]);
                    }
                }
            });

            Task.WaitAny(writer, reader, Task.Delay(100));
            Assert.IsFalse(writer.IsFaulted, "writer");
            Assert.IsFalse(reader.IsFaulted, "reader");
            int w = numWrites;
            int r = numReads;
            Thread.Sleep(100);
            Assert.IsTrue(numWrites > w, "writes");
            Assert.IsTrue(numReads > r, "reads");
            Console.WriteLine($"Writes: {numWrites:N0}");
            Console.WriteLine($"Reads: {numReads:N0}");
            Console.WriteLine($"Bytes: {stream.Position:N0}");
        }
    }
}
