
/*
PrimesTests.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            //var n = Sequences.Primes().Skip(999999).First();
            //Assert.AreEqual(15485863L, n);
        }
    }
}