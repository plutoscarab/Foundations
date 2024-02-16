
/*
AdditiveRecurrenceTests.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Diagnostics;
using Foundations.UnitTesting;

namespace Foundations.RandomNumbers
{
    [TestClass]
    public class AdditiveRecurrenceTests
    {
        [TestMethod]
        public void AdditiveRecurrenceGolden()
        {
            IUniformSource source = new AdditiveRecurrence(1, 1, 5, 2);
            Assert.AreEqual(11400714819323198485ul, source.Next());
            Assert.AreEqual(4354685564936845354ul, source.Next());
            Assert.AreEqual(15755400384260043839ul, source.Next());
        }

        [TestMethod]
        public void AdditiveRecurrenceDouble()
        {
            IUniformSource source = new AdditiveRecurrence(1, 1, 5, 2);
            var g = new UniformGenerator(source);
            Assert.AreEqual(0.61803398874989468, g.Double());
            Assert.AreEqual(0.23606797749978958, g.Double());
            Assert.AreEqual(0.85410196624968449, g.Double());
        }
    }
}