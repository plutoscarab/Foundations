
namespace Foundations
{
    /// <summary />
    [TestClass]
    public class PrimesTests
    {
        /// <summary />
        [TestMethod]
        public void PrimesTest()
        {
            var n = Sequences.Primes().Skip(99999).First();
            Assert.AreEqual(1299709L, n);
            n = Sequences.Primes().Skip(999999).First();
            Assert.AreEqual(15485863L, n);
        }
    }
}