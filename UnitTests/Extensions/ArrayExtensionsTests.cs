
/*
ArrayExtensionsTests.cs

*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations
{
    /// <summary />
    [TestClass]
    public class ArrayExtensionsTests
    {
        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArrayGetBytes()
        {
            Array array = null;
            byte[] b = array.GetBytes();
        }
    }
}