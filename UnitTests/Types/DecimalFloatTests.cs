
/*
DecimalFloatTests.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.Types
{
    /// <summary />
    [TestClass]
    public class DecimalFloatTests
    {
        /// <summary />
        [TestMethod]
        public void DecimalFloatHash()
        {
            var d = new DecimalFloat(123, -5);
            var h = d.GetHashCode();
        }

        /// <summary />
        [TestMethod]
        public void DecimalFloatEquatable()
        {
            var d = new DecimalFloat(123, -5);
            var e = new DecimalFloat(123, -5);
            Assert.IsTrue(d.Equals(e));
            Assert.IsTrue(e.Equals(d));
            var f = new DecimalFloat(124, -5);
            Assert.IsFalse(d.Equals(f));
            Assert.IsFalse(f.Equals(d));
            var g = new DecimalFloat(123, -4);
            Assert.IsFalse(d.Equals(g));
            Assert.IsFalse(g.Equals(d));
        }
    }
}