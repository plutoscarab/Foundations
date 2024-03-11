
/*
SynchronizedRandomSource.cs


*/

using Foundations.RandomNumbers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundations.UnitTests.Objects
{
    [TestClass]
    public class SynchronizedRandomSourceTests
    {
        [TestMethod]
        public void SynchronizedTest()
        {
            var source = new SynchronizedRandomSource(Generator.DefaultSourceFactory());
            var random = new Generator(source, "SynchronizedTest".ToCharArray());
            var random2 = random.Clone();
            var values = new HashSet<uint>();

            for (int i = 0; i < 10000; i++)
            {
                var r = random.UInt32();
                values.Add(r);
            }

            var values1 = new HashSet<uint>();
            var values2 = new HashSet<uint>();

            Parallel.Invoke(
                () =>
                {
                    for (int i = 0; i < 5000; i++)
                    {
                        var r = random2.UInt32();
                        values1.Add(r);
                    }
                },

                () =>
                {
                    for (int i = 0; i < 5000; i++)
                    {
                        var r = random2.UInt32();
                        values2.Add(r);
                    }
                }
            );

            values1.UnionWith(values2);
            values.SymmetricExceptWith(values1);
            Assert.AreEqual(0, values.Count);
        }

        [TestMethod]
        public void CloneTest()
        {
            var source = new SynchronizedRandomSource(new XorShiftRandomSource());
            SHA256RandomSourceTests.CloneTest(source);
        }
    }
}
