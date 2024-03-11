
/*
BitStreamTests.cs
*/

using System.Collections.Generic;
using Foundations.RandomNumbers;

namespace Foundations.Coding
{
    /// <summary />
    [TestClass]
    public class BitStreamTests
    {
        /// <summary />
        [TestMethod]
        public void BitStreamTest()
        {
            var g = new Generator("BitStreamTest");
            var codes = new List<Code>();

            for (int i = 0; i < 10000; i++)
            {
                var bits = g.UInt64();
                var length = g.Int32(65);
                bits >>= 64 - length;
                var code = new Code(bits, length);
                codes.Add(code);
            }

            var writer = new BitWriter(1000);

            foreach (var code in codes)
            {
                writer.Write(code);
            }

            var buffer = writer.ToArray();
            var reader = new BitReader(buffer);

            foreach (var code in codes)
            {
                ulong bits = reader.Read(code.Length);
                Assert.AreEqual(code.Bits, bits);
            }
        }
    }
}