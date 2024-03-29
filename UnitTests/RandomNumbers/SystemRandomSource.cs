﻿
/*
SystemRandomSource.cs


*/

using Foundations.RandomNumbers;

namespace Foundations.UnitTests.Objects
{
    [TestClass]
    public class SystemRandomSourceTests
    {
        [TestMethod]
        public void AssignFromSystemRandom()
        {
            var rand = new System.Random();
            Generator generator = rand;
            var data = new int[9999];
            generator.Fill(10, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] <= 9);
            }
        }

#if DEBUG
#else
        [TestMethod]
        public void GeneratorIsFaster()
        {
            Generator generator = new Generator();
            var data = new byte[10000000];
            generator.Fill(data);
            var sw = Stopwatch.StartNew();
            generator.Fill(data);
            var time1 = sw.Elapsed;
            var rand = new Random();
            var bytes = new byte[Buffer.ByteLength(data)];
            rand.NextBytes(bytes);
            sw = Stopwatch.StartNew();
            rand.NextBytes(bytes);
            var time2 = sw.Elapsed;
            Assert.IsTrue(time2 > time1 + time1 + time1 + time1);
            Trace.WriteLine(time2.TotalSeconds / time1.TotalSeconds + "x faster");
        }
#endif

        [TestMethod]
        public void CloneTest()
        {
            var source = new SystemRandomSource(new Random());
            var clone = source.Clone();
            Assert.IsNull(clone);
        }
    }
}
