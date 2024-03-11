
/*
RulerTests.cs

*/

namespace Foundations
{
    /// <summary />
    [TestClass]
    public class RulerTests
    {
        /// <summary />
        [TestMethod]
        public void RulerTest()
        {
            var n = Sequences.Ruler().Take(20).ToArray();
            var expected = new[] { 1, 2, 1, 3, 1, 2, 1, 4, 1, 2, 1, 3, 1, 2, 1, 5, 1, 2, 1, 3, };
            
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], n[i] + 1, "Position " + i);
            }
        }

        /// <summary />
        [TestMethod]
        public void RulerPartialSumTest()
        {
            var n = Sequences.Ruler().Take(100000).Sum();
            Assert.AreEqual(99994, n);
        }

        [TestMethod]
        public void RulerSkipTest()
        {
            var r1 = Sequences.Ruler().Skip(1000000).Take(100).ToList();
            var r2 = Sequences.Ruler(1000000).Take(100).ToList();
            for (int i = 0; i < 100; i++)
                Assert.AreEqual(r1[i], r2[i]);
        }
    }
}