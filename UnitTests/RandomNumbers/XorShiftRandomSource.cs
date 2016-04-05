
/*
XorShiftRandomSourceTests.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
using Foundations.RandomNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Foundations.UnitTests.Objects
{
    [TestClass]
    public class XorShiftRandomSourceTests
    {
        [TestMethod]
        public void CloneTest()
        {
            var source = new XorShiftRandomSource();
            SHA256RandomSourceTests.CloneTest(source);
        }
    }
}
