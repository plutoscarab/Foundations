
/*
SystemRandomSource.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
using Foundations.RandomNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.UnitTests.Objects
{
    [TestClass]
    public class SystemRandomSourceTests
    {
        [TestMethod]
        public void AssignFromSystemRandom()
        {
            var rand = new System.Random();
            Generator generator = rand;
            var data = new int[9999];
            generator.Fill(10, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] <= 9);
            }
        }
    }
}
