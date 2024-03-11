
/*
VanDerCorputTests.cs
*/

namespace Foundations.RandomNumbers
{
    [TestClass]
    public class VanDerCorputTests
    {
        [TestMethod]
        public void VanDerCorputBase2Source()
        {
            IUniformSource source = new VanDerCorput();
            Assert.AreEqual(0x8000000000000000UL, source.Next());
            Assert.AreEqual(0x4000000000000000UL, source.Next());
            Assert.AreEqual(0xC000000000000000UL, source.Next());
            Assert.AreEqual(0x2000000000000000UL, source.Next());
            Assert.AreEqual(0xA000000000000000UL, source.Next());
            Assert.AreEqual(0x6000000000000000UL, source.Next());
            Assert.AreEqual(0xE000000000000000UL, source.Next());
        }

        [TestMethod]
        public void VanDerCorputBase3Source()
        {
            IUniformSource source = new VanDerCorput(3);
            Assert.AreEqual(0x5555555555555555UL, source.Next());
            Assert.AreEqual(0xAAAAAAAAAAAAAAABUL, source.Next());
            Assert.AreEqual(0x1C71C71C71C71C72UL, source.Next());
            Assert.AreEqual(0x71C71C71C71C71C7UL, source.Next());
        }
    }
}