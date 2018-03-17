
/*
ArrayExtensionsTests.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

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