
/*
StreamSource.cs
*/

using Foundations.RandomNumbers;
using System.IO;

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
