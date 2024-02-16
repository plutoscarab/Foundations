
/*
SynchronizedRandomSource.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.RandomNumbers;
using Foundations.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundations.UnitTests.Objects
{
    [TestClass]
    public class SynchronizedRandomSourceTests
    {
        [TestMethod]
        public void SynchronizedTest()
        {
            var source = new SynchronizedRandomSource(Generator.DefaultSourceFactory());
            var random = new Generator(source, "SynchronizedTest".ToCharArray());
            var values = new HashSet<uint>();

            for (int i = 0; i < 10000; i++)
            {
                values.Add(random.UInt32());
            }

            random = new Generator(source, "SynchronizedTest".ToCharArray());
            var values1 = new HashSet<uint>();
            var values2 = new HashSet<uint>();

            Parallel.Invoke(
                delegate
                {
                    for (int i = 0; i < 5000; i++)
                    {
                        values1.Add(random.UInt32());
                    }
                },

                delegate
                {
                    for (int i = 0; i < 5000; i++)
                    {
                        values2.Add(random.UInt32());
                    }
                }
            );

            values1.UnionWith(values2);
            values.ExceptWith(values1);
            Assert.AreEqual(0, values.Count);
        }

        [TestMethod]
        public void CloneTest()
        {
            var source = new SynchronizedRandomSource(new XorShiftRandomSource());
            SHA256RandomSourceTests.CloneTest(source);
        }
    }
}
