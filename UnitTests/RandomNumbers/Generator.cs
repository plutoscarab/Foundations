
/*
Generator.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundations.RandomNumbers;
using Foundations.Types;

namespace Foundations.UnitTests.RandomNumbers
{
    [TestClass]
    public sealed class GeneratorTests
    {
        [TestMethod]
        public void CloneTest()
        {
            var random = new Generator(new XorShiftRandomSource());
            var clone = random.Clone();

            for (int i = 0; i < 100; i++)
            {
                var expected = random.Byte();
                var actual = clone.Byte();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void CloneConstructor()
        {
            var random = new Generator(new XorShiftRandomSource());
            var clone = new Generator(random);

            for (int i = 0; i < 100; i++)
            {
                var expected = random.Byte();
                var actual = clone.Byte();
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void UnclonableTest()
        {
            var random = new Generator(new SystemRandomSource(new Random()));
            Assert.IsNull(random.Clone());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UnclonableException()
        {
            var random = new Generator(new SystemRandomSource(new Random()));
            new Generator(random);
        }
        [TestMethod]
        public void IndividualByteValues()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualByteValues".ToCharArray());
            var data = new Byte[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.Byte();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualByteValuesUpToRange()
        {
            var random = new Generator();
            var data = new Byte[99];
            var hash = new HashSet<Byte>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Byte(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualByteInvalidRange()
        {
            var random = new Generator();
            random.Byte(0);
        }

        [TestMethod]
        public void IndividualByteValuesInRange()
        {
            var random = new Generator();
            var data = new Byte[99];
            var hash = new HashSet<Byte>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Byte(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualByteValuesInRangeTooLow()
        {
            var random = new Generator();
            random.Byte(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualByteValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.Byte(3 * (Byte.MaxValue / 4), Byte.MaxValue / 2);
        }

        [TestMethod]
        public void RandomByteArrays()
        {
            var random = new Generator("RandomBytes".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new Byte[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomByteArrayWithRange()
        {
            var random = new Generator();
            var data = new Byte[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomByteArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new Byte[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Byte)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomByteArrayWithLowRange()
        {
            var random = new Generator();
            var data = new Byte[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomByteArrayWithHighRange()
        {
            var random = new Generator();
            var data = new Byte[10000];
            random.Fill(3 * (Byte.MaxValue / 4), Byte.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullByteArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (Byte[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullByteArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Byte[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullByteSubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Byte[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomByteSubArrayWithLowOffset()
        {
            var random = new Generator("RandomByteArrayWithRange".ToCharArray());
            var data = new Byte[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomByteSubArrayWithHighOffset()
        {
            var random = new Generator("RandomByteArrayWithRange".ToCharArray());
            var data = new Byte[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomByteSubArrayWithLowCount()
        {
            var random = new Generator("RandomByteArrayWithRange".ToCharArray());
            var data = new Byte[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomByteSubArrayWithHighCount()
        {
            var random = new Generator("RandomByteArrayWithRange".ToCharArray());
            var data = new Byte[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullByteArray()
        {
            var random = new Generator("RandomBytes".ToCharArray());
            Byte[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullByteArrayWithOffsetCount()
        {
            var random = new Generator("RandomBytes".ToCharArray());
            Byte[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowByteArrayOffset()
        {
            var random = new Generator("RandomBytes".ToCharArray());
            var data = new Byte[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighByteArrayOffset()
        {
            var random = new Generator("RandomBytes".ToCharArray());
            var data = new Byte[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowByteArrayCount()
        {
            var random = new Generator("RandomBytes".ToCharArray());
            var data = new Byte[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighByteArrayCount()
        {
            var random = new Generator("RandomBytes".ToCharArray());
            var data = new Byte[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateByteState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Byte[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new Byte[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateByteStateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new Byte[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as Byte[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateByteStateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Byte[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new Byte[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateByteStateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Byte[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new Byte[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateByteStateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Byte[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            Byte[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithBytes()
        {
            var random = new Generator("SeedWithBytes".ToCharArray());
            var data = new Byte[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithBytesAndSource()
        {
            var random = new Generator("SeedWithBytes".ToCharArray());
            var data = new Byte[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomByteSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Byte[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomByteFromEntropy()
        {
            var random = new Generator();
            var data = new Byte[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomByteFromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new Byte[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthByteArray()
        {
            var random = new Generator("FillZeroLengthByteArray");
            var data = new Byte[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((Byte)0, data[0]);
            Assert.AreEqual((Byte)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfByte()
        {
            var random = new Generator("EnumeratorOfByte");
            var data = random.Bytes().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfByteWithRange()
        {
            var random = new Generator("EnumeratorOfByteWithRange");
            var data = random.Bytes(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfByteWithRangeLow()
        {
            var random = new Generator();
            var data = random.Bytes(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfByteWithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfByteWithOffsetAndRange");
            var data = random.Bytes(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfByteWithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.Bytes(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfByteWithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.Bytes(3 * (System.Byte.MaxValue / 4), System.Byte.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(Byte[] array)
        {
            var minValue = (double)Byte.MinValue;
            var maxValue = (double)Byte.MaxValue;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualSByteValues()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualSByteValues".ToCharArray());
            var data = new SByte[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.SByte();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualNonNegativeSByteValues()
        {
            var random = new Generator();

            for (int i = 0; i < 1000; i++)
            {
                var value = random.SByteNonNegative();
                Assert.IsTrue(value >= 0);
            }
        }

        [TestMethod]
        public void FillNonNegativeSByte()
        {
            var random = new Generator();
            var data = new SByte[10000];
            random.FillNonNegative(data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FillNullNonNegativeSByte()
        {
            var random = new Generator();
            random.FillNonNegative((SByte[])null);
        }

        [TestMethod]
        public void EnumeratorOfNonNegativeSByte()
        {
            var random = new Generator("EnumeratorOfNonNegativeSByte");
            var data = random.SBytesNonNegative().Take(1000).ToArray();
            
            for (int i = 0; i < data.Length; i++)
                Assert.IsTrue(data[i] >= 0);
        }

        [TestMethod]
        public void IndividualSByteValuesUpToRange()
        {
            var random = new Generator();
            var data = new SByte[99];
            var hash = new HashSet<SByte>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.SByte(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualSByteInvalidRange()
        {
            var random = new Generator();
            random.SByte(0);
        }

        [TestMethod]
        public void IndividualSByteValuesInRange()
        {
            var random = new Generator();
            var data = new SByte[99];
            var hash = new HashSet<SByte>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.SByte(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualSByteValuesInRangeTooLow()
        {
            var random = new Generator();
            random.SByte(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualSByteValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.SByte(3 * (SByte.MaxValue / 4), SByte.MaxValue / 2);
        }

        [TestMethod]
        public void RandomSByteArrays()
        {
            var random = new Generator("RandomSBytes".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new SByte[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomSByteArrayWithRange()
        {
            var random = new Generator();
            var data = new SByte[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomSByteArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new SByte[9999];

            for (int p = 0; p < 10 * (8 / sizeof(SByte)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSByteArrayWithLowRange()
        {
            var random = new Generator();
            var data = new SByte[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSByteArrayWithHighRange()
        {
            var random = new Generator();
            var data = new SByte[10000];
            random.Fill(3 * (SByte.MaxValue / 4), SByte.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullSByteArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (SByte[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullSByteArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (SByte[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullSByteSubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (SByte[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSByteSubArrayWithLowOffset()
        {
            var random = new Generator("RandomSByteArrayWithRange".ToCharArray());
            var data = new SByte[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSByteSubArrayWithHighOffset()
        {
            var random = new Generator("RandomSByteArrayWithRange".ToCharArray());
            var data = new SByte[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSByteSubArrayWithLowCount()
        {
            var random = new Generator("RandomSByteArrayWithRange".ToCharArray());
            var data = new SByte[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSByteSubArrayWithHighCount()
        {
            var random = new Generator("RandomSByteArrayWithRange".ToCharArray());
            var data = new SByte[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSByteArray()
        {
            var random = new Generator("RandomSBytes".ToCharArray());
            SByte[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSByteArrayWithOffsetCount()
        {
            var random = new Generator("RandomSBytes".ToCharArray());
            SByte[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowSByteArrayOffset()
        {
            var random = new Generator("RandomSBytes".ToCharArray());
            var data = new SByte[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighSByteArrayOffset()
        {
            var random = new Generator("RandomSBytes".ToCharArray());
            var data = new SByte[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowSByteArrayCount()
        {
            var random = new Generator("RandomSBytes".ToCharArray());
            var data = new SByte[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighSByteArrayCount()
        {
            var random = new Generator("RandomSBytes".ToCharArray());
            var data = new SByte[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateSByteState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(SByte[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new SByte[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateSByteStateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new SByte[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as SByte[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateSByteStateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(SByte[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new SByte[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateSByteStateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(SByte[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new SByte[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateSByteStateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(SByte[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            SByte[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithSBytes()
        {
            var random = new Generator("SeedWithSBytes".ToCharArray());
            var data = new SByte[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithSBytesAndSource()
        {
            var random = new Generator("SeedWithSBytes".ToCharArray());
            var data = new SByte[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomSByteSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new SByte[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomSByteFromEntropy()
        {
            var random = new Generator();
            var data = new SByte[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomSByteFromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new SByte[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthSByteArray()
        {
            var random = new Generator("FillZeroLengthSByteArray");
            var data = new SByte[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((SByte)0, data[0]);
            Assert.AreEqual((SByte)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfSByte()
        {
            var random = new Generator("EnumeratorOfSByte");
            var data = random.SBytes().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfSByteWithRange()
        {
            var random = new Generator("EnumeratorOfSByteWithRange");
            var data = random.SBytes(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfSByteWithRangeLow()
        {
            var random = new Generator();
            var data = random.SBytes(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfSByteWithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfSByteWithOffsetAndRange");
            var data = random.SBytes(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfSByteWithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.SBytes(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfSByteWithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.SBytes(3 * (System.SByte.MaxValue / 4), System.SByte.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(SByte[] array)
        {
            var minValue = (double)SByte.MinValue;
            var maxValue = (double)SByte.MaxValue;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualUInt16Values()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualUInt16Values".ToCharArray());
            var data = new UInt16[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.UInt16();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualUInt16ValuesUpToRange()
        {
            var random = new Generator();
            var data = new UInt16[99];
            var hash = new HashSet<UInt16>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.UInt16(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualUInt16InvalidRange()
        {
            var random = new Generator();
            random.UInt16(0);
        }

        [TestMethod]
        public void IndividualUInt16ValuesInRange()
        {
            var random = new Generator();
            var data = new UInt16[99];
            var hash = new HashSet<UInt16>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.UInt16(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualUInt16ValuesInRangeTooLow()
        {
            var random = new Generator();
            random.UInt16(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualUInt16ValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.UInt16(3 * (UInt16.MaxValue / 4), UInt16.MaxValue / 2);
        }

        [TestMethod]
        public void RandomUInt16Arrays()
        {
            var random = new Generator("RandomUInt16s".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new UInt16[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomUInt16ArrayWithRange()
        {
            var random = new Generator();
            var data = new UInt16[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomUInt16ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new UInt16[9999];

            for (int p = 0; p < 10 * (8 / sizeof(UInt16)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt16ArrayWithLowRange()
        {
            var random = new Generator();
            var data = new UInt16[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt16ArrayWithHighRange()
        {
            var random = new Generator();
            var data = new UInt16[10000];
            random.Fill(3 * (UInt16.MaxValue / 4), UInt16.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullUInt16ArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (UInt16[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullUInt16ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (UInt16[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullUInt16SubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (UInt16[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt16SubArrayWithLowOffset()
        {
            var random = new Generator("RandomUInt16ArrayWithRange".ToCharArray());
            var data = new UInt16[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt16SubArrayWithHighOffset()
        {
            var random = new Generator("RandomUInt16ArrayWithRange".ToCharArray());
            var data = new UInt16[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt16SubArrayWithLowCount()
        {
            var random = new Generator("RandomUInt16ArrayWithRange".ToCharArray());
            var data = new UInt16[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt16SubArrayWithHighCount()
        {
            var random = new Generator("RandomUInt16ArrayWithRange".ToCharArray());
            var data = new UInt16[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullUInt16Array()
        {
            var random = new Generator("RandomUInt16s".ToCharArray());
            UInt16[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullUInt16ArrayWithOffsetCount()
        {
            var random = new Generator("RandomUInt16s".ToCharArray());
            UInt16[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowUInt16ArrayOffset()
        {
            var random = new Generator("RandomUInt16s".ToCharArray());
            var data = new UInt16[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighUInt16ArrayOffset()
        {
            var random = new Generator("RandomUInt16s".ToCharArray());
            var data = new UInt16[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowUInt16ArrayCount()
        {
            var random = new Generator("RandomUInt16s".ToCharArray());
            var data = new UInt16[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighUInt16ArrayCount()
        {
            var random = new Generator("RandomUInt16s".ToCharArray());
            var data = new UInt16[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateUInt16State()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt16[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new UInt16[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateUInt16StateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new UInt16[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as UInt16[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateUInt16StateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt16[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new UInt16[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateUInt16StateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt16[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new UInt16[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateUInt16StateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt16[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            UInt16[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithUInt16s()
        {
            var random = new Generator("SeedWithUInt16s".ToCharArray());
            var data = new UInt16[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithUInt16sAndSource()
        {
            var random = new Generator("SeedWithUInt16s".ToCharArray());
            var data = new UInt16[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomUInt16SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new UInt16[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomUInt16FromEntropy()
        {
            var random = new Generator();
            var data = new UInt16[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomUInt16FromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new UInt16[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthUInt16Array()
        {
            var random = new Generator("FillZeroLengthUInt16Array");
            var data = new UInt16[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((UInt16)0, data[0]);
            Assert.AreEqual((UInt16)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfUInt16()
        {
            var random = new Generator("EnumeratorOfUInt16");
            var data = random.UInt16s().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfUInt16WithRange()
        {
            var random = new Generator("EnumeratorOfUInt16WithRange");
            var data = random.UInt16s(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfUInt16WithRangeLow()
        {
            var random = new Generator();
            var data = random.UInt16s(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfUInt16WithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfUInt16WithOffsetAndRange");
            var data = random.UInt16s(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfUInt16WithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.UInt16s(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfUInt16WithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.UInt16s(3 * (System.UInt16.MaxValue / 4), System.UInt16.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(UInt16[] array)
        {
            var minValue = (double)UInt16.MinValue;
            var maxValue = (double)UInt16.MaxValue;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualInt16Values()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualInt16Values".ToCharArray());
            var data = new Int16[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.Int16();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualNonNegativeInt16Values()
        {
            var random = new Generator();

            for (int i = 0; i < 1000; i++)
            {
                var value = random.Int16NonNegative();
                Assert.IsTrue(value >= 0);
            }
        }

        [TestMethod]
        public void FillNonNegativeInt16()
        {
            var random = new Generator();
            var data = new Int16[10000];
            random.FillNonNegative(data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FillNullNonNegativeInt16()
        {
            var random = new Generator();
            random.FillNonNegative((Int16[])null);
        }

        [TestMethod]
        public void EnumeratorOfNonNegativeInt16()
        {
            var random = new Generator("EnumeratorOfNonNegativeInt16");
            var data = random.Int16sNonNegative().Take(1000).ToArray();
            
            for (int i = 0; i < data.Length; i++)
                Assert.IsTrue(data[i] >= 0);
        }

        [TestMethod]
        public void IndividualInt16ValuesUpToRange()
        {
            var random = new Generator();
            var data = new Int16[99];
            var hash = new HashSet<Int16>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Int16(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualInt16InvalidRange()
        {
            var random = new Generator();
            random.Int16(0);
        }

        [TestMethod]
        public void IndividualInt16ValuesInRange()
        {
            var random = new Generator();
            var data = new Int16[99];
            var hash = new HashSet<Int16>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Int16(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualInt16ValuesInRangeTooLow()
        {
            var random = new Generator();
            random.Int16(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualInt16ValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.Int16(3 * (Int16.MaxValue / 4), Int16.MaxValue / 2);
        }

        [TestMethod]
        public void RandomInt16Arrays()
        {
            var random = new Generator("RandomInt16s".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new Int16[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomInt16ArrayWithRange()
        {
            var random = new Generator();
            var data = new Int16[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomInt16ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new Int16[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Int16)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt16ArrayWithLowRange()
        {
            var random = new Generator();
            var data = new Int16[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt16ArrayWithHighRange()
        {
            var random = new Generator();
            var data = new Int16[10000];
            random.Fill(3 * (Int16.MaxValue / 4), Int16.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullInt16ArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (Int16[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullInt16ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Int16[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullInt16SubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Int16[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt16SubArrayWithLowOffset()
        {
            var random = new Generator("RandomInt16ArrayWithRange".ToCharArray());
            var data = new Int16[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt16SubArrayWithHighOffset()
        {
            var random = new Generator("RandomInt16ArrayWithRange".ToCharArray());
            var data = new Int16[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt16SubArrayWithLowCount()
        {
            var random = new Generator("RandomInt16ArrayWithRange".ToCharArray());
            var data = new Int16[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt16SubArrayWithHighCount()
        {
            var random = new Generator("RandomInt16ArrayWithRange".ToCharArray());
            var data = new Int16[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInt16Array()
        {
            var random = new Generator("RandomInt16s".ToCharArray());
            Int16[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInt16ArrayWithOffsetCount()
        {
            var random = new Generator("RandomInt16s".ToCharArray());
            Int16[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowInt16ArrayOffset()
        {
            var random = new Generator("RandomInt16s".ToCharArray());
            var data = new Int16[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighInt16ArrayOffset()
        {
            var random = new Generator("RandomInt16s".ToCharArray());
            var data = new Int16[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowInt16ArrayCount()
        {
            var random = new Generator("RandomInt16s".ToCharArray());
            var data = new Int16[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighInt16ArrayCount()
        {
            var random = new Generator("RandomInt16s".ToCharArray());
            var data = new Int16[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateInt16State()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int16[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new Int16[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateInt16StateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new Int16[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as Int16[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateInt16StateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int16[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new Int16[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateInt16StateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int16[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new Int16[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateInt16StateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int16[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            Int16[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithInt16s()
        {
            var random = new Generator("SeedWithInt16s".ToCharArray());
            var data = new Int16[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithInt16sAndSource()
        {
            var random = new Generator("SeedWithInt16s".ToCharArray());
            var data = new Int16[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomInt16SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Int16[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomInt16FromEntropy()
        {
            var random = new Generator();
            var data = new Int16[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomInt16FromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new Int16[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthInt16Array()
        {
            var random = new Generator("FillZeroLengthInt16Array");
            var data = new Int16[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((Int16)0, data[0]);
            Assert.AreEqual((Int16)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfInt16()
        {
            var random = new Generator("EnumeratorOfInt16");
            var data = random.Int16s().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfInt16WithRange()
        {
            var random = new Generator("EnumeratorOfInt16WithRange");
            var data = random.Int16s(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfInt16WithRangeLow()
        {
            var random = new Generator();
            var data = random.Int16s(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfInt16WithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfInt16WithOffsetAndRange");
            var data = random.Int16s(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfInt16WithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.Int16s(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfInt16WithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.Int16s(3 * (System.Int16.MaxValue / 4), System.Int16.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(Int16[] array)
        {
            var minValue = (double)Int16.MinValue;
            var maxValue = (double)Int16.MaxValue;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualUInt32Values()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualUInt32Values".ToCharArray());
            var data = new UInt32[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.UInt32();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualUInt32ValuesUpToRange()
        {
            var random = new Generator();
            var data = new UInt32[99];
            var hash = new HashSet<UInt32>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.UInt32(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualUInt32InvalidRange()
        {
            var random = new Generator();
            random.UInt32(0);
        }

        [TestMethod]
        public void IndividualUInt32ValuesInRange()
        {
            var random = new Generator();
            var data = new UInt32[99];
            var hash = new HashSet<UInt32>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.UInt32(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualUInt32ValuesInRangeTooLow()
        {
            var random = new Generator();
            random.UInt32(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualUInt32ValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.UInt32(3 * (UInt32.MaxValue / 4), UInt32.MaxValue / 2);
        }

        [TestMethod]
        public void RandomUInt32Arrays()
        {
            var random = new Generator("RandomUInt32s".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new UInt32[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomUInt32ArrayWithRange()
        {
            var random = new Generator();
            var data = new UInt32[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomUInt32ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new UInt32[9999];

            for (int p = 0; p < 10 * (8 / sizeof(UInt32)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt32ArrayWithLowRange()
        {
            var random = new Generator();
            var data = new UInt32[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt32ArrayWithHighRange()
        {
            var random = new Generator();
            var data = new UInt32[10000];
            random.Fill(3 * (UInt32.MaxValue / 4), UInt32.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullUInt32ArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (UInt32[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullUInt32ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (UInt32[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullUInt32SubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (UInt32[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt32SubArrayWithLowOffset()
        {
            var random = new Generator("RandomUInt32ArrayWithRange".ToCharArray());
            var data = new UInt32[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt32SubArrayWithHighOffset()
        {
            var random = new Generator("RandomUInt32ArrayWithRange".ToCharArray());
            var data = new UInt32[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt32SubArrayWithLowCount()
        {
            var random = new Generator("RandomUInt32ArrayWithRange".ToCharArray());
            var data = new UInt32[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt32SubArrayWithHighCount()
        {
            var random = new Generator("RandomUInt32ArrayWithRange".ToCharArray());
            var data = new UInt32[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullUInt32Array()
        {
            var random = new Generator("RandomUInt32s".ToCharArray());
            UInt32[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullUInt32ArrayWithOffsetCount()
        {
            var random = new Generator("RandomUInt32s".ToCharArray());
            UInt32[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowUInt32ArrayOffset()
        {
            var random = new Generator("RandomUInt32s".ToCharArray());
            var data = new UInt32[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighUInt32ArrayOffset()
        {
            var random = new Generator("RandomUInt32s".ToCharArray());
            var data = new UInt32[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowUInt32ArrayCount()
        {
            var random = new Generator("RandomUInt32s".ToCharArray());
            var data = new UInt32[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighUInt32ArrayCount()
        {
            var random = new Generator("RandomUInt32s".ToCharArray());
            var data = new UInt32[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateUInt32State()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt32[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new UInt32[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateUInt32StateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new UInt32[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as UInt32[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateUInt32StateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt32[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new UInt32[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateUInt32StateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt32[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new UInt32[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateUInt32StateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt32[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            UInt32[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithUInt32s()
        {
            var random = new Generator("SeedWithUInt32s".ToCharArray());
            var data = new UInt32[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithUInt32sAndSource()
        {
            var random = new Generator("SeedWithUInt32s".ToCharArray());
            var data = new UInt32[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomUInt32SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new UInt32[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomUInt32FromEntropy()
        {
            var random = new Generator();
            var data = new UInt32[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomUInt32FromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new UInt32[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthUInt32Array()
        {
            var random = new Generator("FillZeroLengthUInt32Array");
            var data = new UInt32[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((UInt32)0, data[0]);
            Assert.AreEqual((UInt32)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfUInt32()
        {
            var random = new Generator("EnumeratorOfUInt32");
            var data = random.UInt32s().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfUInt32WithRange()
        {
            var random = new Generator("EnumeratorOfUInt32WithRange");
            var data = random.UInt32s(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfUInt32WithRangeLow()
        {
            var random = new Generator();
            var data = random.UInt32s(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfUInt32WithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfUInt32WithOffsetAndRange");
            var data = random.UInt32s(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfUInt32WithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.UInt32s(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfUInt32WithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.UInt32s(3 * (System.UInt32.MaxValue / 4), System.UInt32.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(UInt32[] array)
        {
            var minValue = (double)UInt32.MinValue;
            var maxValue = (double)UInt32.MaxValue;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualInt32Values()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualInt32Values".ToCharArray());
            var data = new Int32[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.Int32();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualNonNegativeInt32Values()
        {
            var random = new Generator();

            for (int i = 0; i < 1000; i++)
            {
                var value = random.Int32NonNegative();
                Assert.IsTrue(value >= 0);
            }
        }

        [TestMethod]
        public void FillNonNegativeInt32()
        {
            var random = new Generator();
            var data = new Int32[10000];
            random.FillNonNegative(data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FillNullNonNegativeInt32()
        {
            var random = new Generator();
            random.FillNonNegative((Int32[])null);
        }

        [TestMethod]
        public void EnumeratorOfNonNegativeInt32()
        {
            var random = new Generator("EnumeratorOfNonNegativeInt32");
            var data = random.Int32sNonNegative().Take(1000).ToArray();
            
            for (int i = 0; i < data.Length; i++)
                Assert.IsTrue(data[i] >= 0);
        }

        [TestMethod]
        public void IndividualInt32ValuesUpToRange()
        {
            var random = new Generator();
            var data = new Int32[99];
            var hash = new HashSet<Int32>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Int32(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualInt32InvalidRange()
        {
            var random = new Generator();
            random.Int32(0);
        }

        [TestMethod]
        public void IndividualInt32ValuesInRange()
        {
            var random = new Generator();
            var data = new Int32[99];
            var hash = new HashSet<Int32>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Int32(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualInt32ValuesInRangeTooLow()
        {
            var random = new Generator();
            random.Int32(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualInt32ValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.Int32(3 * (Int32.MaxValue / 4), Int32.MaxValue / 2);
        }

        [TestMethod]
        public void RandomInt32Arrays()
        {
            var random = new Generator("RandomInt32s".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new Int32[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomInt32ArrayWithRange()
        {
            var random = new Generator();
            var data = new Int32[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomInt32ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new Int32[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Int32)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt32ArrayWithLowRange()
        {
            var random = new Generator();
            var data = new Int32[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt32ArrayWithHighRange()
        {
            var random = new Generator();
            var data = new Int32[10000];
            random.Fill(3 * (Int32.MaxValue / 4), Int32.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullInt32ArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (Int32[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullInt32ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Int32[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullInt32SubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Int32[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt32SubArrayWithLowOffset()
        {
            var random = new Generator("RandomInt32ArrayWithRange".ToCharArray());
            var data = new Int32[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt32SubArrayWithHighOffset()
        {
            var random = new Generator("RandomInt32ArrayWithRange".ToCharArray());
            var data = new Int32[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt32SubArrayWithLowCount()
        {
            var random = new Generator("RandomInt32ArrayWithRange".ToCharArray());
            var data = new Int32[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt32SubArrayWithHighCount()
        {
            var random = new Generator("RandomInt32ArrayWithRange".ToCharArray());
            var data = new Int32[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInt32Array()
        {
            var random = new Generator("RandomInt32s".ToCharArray());
            Int32[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInt32ArrayWithOffsetCount()
        {
            var random = new Generator("RandomInt32s".ToCharArray());
            Int32[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowInt32ArrayOffset()
        {
            var random = new Generator("RandomInt32s".ToCharArray());
            var data = new Int32[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighInt32ArrayOffset()
        {
            var random = new Generator("RandomInt32s".ToCharArray());
            var data = new Int32[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowInt32ArrayCount()
        {
            var random = new Generator("RandomInt32s".ToCharArray());
            var data = new Int32[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighInt32ArrayCount()
        {
            var random = new Generator("RandomInt32s".ToCharArray());
            var data = new Int32[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateInt32State()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int32[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new Int32[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateInt32StateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new Int32[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as Int32[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateInt32StateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int32[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new Int32[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateInt32StateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int32[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new Int32[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateInt32StateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int32[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            Int32[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithInt32s()
        {
            var random = new Generator("SeedWithInt32s".ToCharArray());
            var data = new Int32[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithInt32sAndSource()
        {
            var random = new Generator("SeedWithInt32s".ToCharArray());
            var data = new Int32[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomInt32SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Int32[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomInt32FromEntropy()
        {
            var random = new Generator();
            var data = new Int32[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomInt32FromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new Int32[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthInt32Array()
        {
            var random = new Generator("FillZeroLengthInt32Array");
            var data = new Int32[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((Int32)0, data[0]);
            Assert.AreEqual((Int32)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfInt32()
        {
            var random = new Generator("EnumeratorOfInt32");
            var data = random.Int32s().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfInt32WithRange()
        {
            var random = new Generator("EnumeratorOfInt32WithRange");
            var data = random.Int32s(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfInt32WithRangeLow()
        {
            var random = new Generator();
            var data = random.Int32s(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfInt32WithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfInt32WithOffsetAndRange");
            var data = random.Int32s(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfInt32WithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.Int32s(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfInt32WithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.Int32s(3 * (System.Int32.MaxValue / 4), System.Int32.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(Int32[] array)
        {
            var minValue = (double)Int32.MinValue;
            var maxValue = (double)Int32.MaxValue;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualUInt64Values()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualUInt64Values".ToCharArray());
            var data = new UInt64[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.UInt64();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualUInt64ValuesUpToRange()
        {
            var random = new Generator();
            var data = new UInt64[99];
            var hash = new HashSet<UInt64>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.UInt64(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualUInt64InvalidRange()
        {
            var random = new Generator();
            random.UInt64(0);
        }

        [TestMethod]
        public void IndividualUInt64ValuesInRange()
        {
            var random = new Generator();
            var data = new UInt64[99];
            var hash = new HashSet<UInt64>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.UInt64(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualUInt64ValuesInRangeTooLow()
        {
            var random = new Generator();
            random.UInt64(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualUInt64ValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.UInt64(3 * (UInt64.MaxValue / 4), UInt64.MaxValue / 2);
        }

        [TestMethod]
        public void RandomUInt64Arrays()
        {
            var random = new Generator("RandomUInt64s".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new UInt64[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomUInt64ArrayWithRange()
        {
            var random = new Generator();
            var data = new UInt64[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomUInt64ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new UInt64[9999];

            for (int p = 0; p < 10 * (8 / sizeof(UInt64)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt64ArrayWithLowRange()
        {
            var random = new Generator();
            var data = new UInt64[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt64ArrayWithHighRange()
        {
            var random = new Generator();
            var data = new UInt64[10000];
            random.Fill(3 * (UInt64.MaxValue / 4), UInt64.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullUInt64ArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (UInt64[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullUInt64ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (UInt64[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullUInt64SubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (UInt64[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt64SubArrayWithLowOffset()
        {
            var random = new Generator("RandomUInt64ArrayWithRange".ToCharArray());
            var data = new UInt64[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt64SubArrayWithHighOffset()
        {
            var random = new Generator("RandomUInt64ArrayWithRange".ToCharArray());
            var data = new UInt64[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt64SubArrayWithLowCount()
        {
            var random = new Generator("RandomUInt64ArrayWithRange".ToCharArray());
            var data = new UInt64[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomUInt64SubArrayWithHighCount()
        {
            var random = new Generator("RandomUInt64ArrayWithRange".ToCharArray());
            var data = new UInt64[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullUInt64Array()
        {
            var random = new Generator("RandomUInt64s".ToCharArray());
            UInt64[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullUInt64ArrayWithOffsetCount()
        {
            var random = new Generator("RandomUInt64s".ToCharArray());
            UInt64[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowUInt64ArrayOffset()
        {
            var random = new Generator("RandomUInt64s".ToCharArray());
            var data = new UInt64[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighUInt64ArrayOffset()
        {
            var random = new Generator("RandomUInt64s".ToCharArray());
            var data = new UInt64[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowUInt64ArrayCount()
        {
            var random = new Generator("RandomUInt64s".ToCharArray());
            var data = new UInt64[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighUInt64ArrayCount()
        {
            var random = new Generator("RandomUInt64s".ToCharArray());
            var data = new UInt64[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateUInt64State()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt64[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new UInt64[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateUInt64StateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new UInt64[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as UInt64[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateUInt64StateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt64[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new UInt64[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateUInt64StateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt64[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new UInt64[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateUInt64StateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(UInt64[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            UInt64[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithUInt64s()
        {
            var random = new Generator("SeedWithUInt64s".ToCharArray());
            var data = new UInt64[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithUInt64sAndSource()
        {
            var random = new Generator("SeedWithUInt64s".ToCharArray());
            var data = new UInt64[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomUInt64SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new UInt64[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomUInt64FromEntropy()
        {
            var random = new Generator();
            var data = new UInt64[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomUInt64FromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new UInt64[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthUInt64Array()
        {
            var random = new Generator("FillZeroLengthUInt64Array");
            var data = new UInt64[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((UInt64)0, data[0]);
            Assert.AreEqual((UInt64)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfUInt64()
        {
            var random = new Generator("EnumeratorOfUInt64");
            var data = random.UInt64s().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfUInt64WithRange()
        {
            var random = new Generator("EnumeratorOfUInt64WithRange");
            var data = random.UInt64s(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfUInt64WithRangeLow()
        {
            var random = new Generator();
            var data = random.UInt64s(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfUInt64WithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfUInt64WithOffsetAndRange");
            var data = random.UInt64s(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfUInt64WithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.UInt64s(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfUInt64WithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.UInt64s(3 * (System.UInt64.MaxValue / 4), System.UInt64.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(UInt64[] array)
        {
            var minValue = (double)UInt64.MinValue;
            var maxValue = (double)UInt64.MaxValue;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualInt64Values()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualInt64Values".ToCharArray());
            var data = new Int64[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.Int64();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualNonNegativeInt64Values()
        {
            var random = new Generator();

            for (int i = 0; i < 1000; i++)
            {
                var value = random.Int64NonNegative();
                Assert.IsTrue(value >= 0);
            }
        }

        [TestMethod]
        public void FillNonNegativeInt64()
        {
            var random = new Generator();
            var data = new Int64[10000];
            random.FillNonNegative(data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FillNullNonNegativeInt64()
        {
            var random = new Generator();
            random.FillNonNegative((Int64[])null);
        }

        [TestMethod]
        public void EnumeratorOfNonNegativeInt64()
        {
            var random = new Generator("EnumeratorOfNonNegativeInt64");
            var data = random.Int64sNonNegative().Take(1000).ToArray();
            
            for (int i = 0; i < data.Length; i++)
                Assert.IsTrue(data[i] >= 0);
        }

        [TestMethod]
        public void IndividualInt64ValuesUpToRange()
        {
            var random = new Generator();
            var data = new Int64[99];
            var hash = new HashSet<Int64>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Int64(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualInt64InvalidRange()
        {
            var random = new Generator();
            random.Int64(0);
        }

        [TestMethod]
        public void IndividualInt64ValuesInRange()
        {
            var random = new Generator();
            var data = new Int64[99];
            var hash = new HashSet<Int64>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Int64(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualInt64ValuesInRangeTooLow()
        {
            var random = new Generator();
            random.Int64(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualInt64ValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.Int64(3 * (Int64.MaxValue / 4), Int64.MaxValue / 2);
        }

        [TestMethod]
        public void RandomInt64Arrays()
        {
            var random = new Generator("RandomInt64s".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new Int64[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomInt64ArrayWithRange()
        {
            var random = new Generator();
            var data = new Int64[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomInt64ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new Int64[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Int64)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt64ArrayWithLowRange()
        {
            var random = new Generator();
            var data = new Int64[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt64ArrayWithHighRange()
        {
            var random = new Generator();
            var data = new Int64[10000];
            random.Fill(3 * (Int64.MaxValue / 4), Int64.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullInt64ArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (Int64[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullInt64ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Int64[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullInt64SubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Int64[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt64SubArrayWithLowOffset()
        {
            var random = new Generator("RandomInt64ArrayWithRange".ToCharArray());
            var data = new Int64[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt64SubArrayWithHighOffset()
        {
            var random = new Generator("RandomInt64ArrayWithRange".ToCharArray());
            var data = new Int64[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt64SubArrayWithLowCount()
        {
            var random = new Generator("RandomInt64ArrayWithRange".ToCharArray());
            var data = new Int64[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomInt64SubArrayWithHighCount()
        {
            var random = new Generator("RandomInt64ArrayWithRange".ToCharArray());
            var data = new Int64[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInt64Array()
        {
            var random = new Generator("RandomInt64s".ToCharArray());
            Int64[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInt64ArrayWithOffsetCount()
        {
            var random = new Generator("RandomInt64s".ToCharArray());
            Int64[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowInt64ArrayOffset()
        {
            var random = new Generator("RandomInt64s".ToCharArray());
            var data = new Int64[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighInt64ArrayOffset()
        {
            var random = new Generator("RandomInt64s".ToCharArray());
            var data = new Int64[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowInt64ArrayCount()
        {
            var random = new Generator("RandomInt64s".ToCharArray());
            var data = new Int64[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighInt64ArrayCount()
        {
            var random = new Generator("RandomInt64s".ToCharArray());
            var data = new Int64[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateInt64State()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int64[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new Int64[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateInt64StateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new Int64[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as Int64[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateInt64StateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int64[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new Int64[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateInt64StateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int64[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new Int64[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateInt64StateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Int64[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            Int64[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithInt64s()
        {
            var random = new Generator("SeedWithInt64s".ToCharArray());
            var data = new Int64[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithInt64sAndSource()
        {
            var random = new Generator("SeedWithInt64s".ToCharArray());
            var data = new Int64[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomInt64SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Int64[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomInt64FromEntropy()
        {
            var random = new Generator();
            var data = new Int64[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomInt64FromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new Int64[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthInt64Array()
        {
            var random = new Generator("FillZeroLengthInt64Array");
            var data = new Int64[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((Int64)0, data[0]);
            Assert.AreEqual((Int64)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfInt64()
        {
            var random = new Generator("EnumeratorOfInt64");
            var data = random.Int64s().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfInt64WithRange()
        {
            var random = new Generator("EnumeratorOfInt64WithRange");
            var data = random.Int64s(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfInt64WithRangeLow()
        {
            var random = new Generator();
            var data = random.Int64s(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfInt64WithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfInt64WithOffsetAndRange");
            var data = random.Int64s(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfInt64WithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.Int64s(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfInt64WithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.Int64s(3 * (System.Int64.MaxValue / 4), System.Int64.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(Int64[] array)
        {
            var minValue = (double)Int64.MinValue;
            var maxValue = (double)Int64.MaxValue;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualSingleValues()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualSingleValues".ToCharArray());
            var data = new Single[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.Single();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualSingleValuesUpToRange()
        {
            var random = new Generator();
            var data = new Single[99];
            var hash = new HashSet<Single>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Single(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualSingleInvalidRange()
        {
            var random = new Generator();
            random.Single(0);
        }

        [TestMethod]
        public void IndividualSingleValuesInRange()
        {
            var random = new Generator();
            var data = new Single[99];
            var hash = new HashSet<Single>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Single(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualSingleValuesInRangeTooLow()
        {
            var random = new Generator();
            random.Single(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualSingleValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.Single(3 * (Single.MaxValue / 4), Single.MaxValue / 2);
        }

        [TestMethod]
        public void RandomSingleArrays()
        {
            var random = new Generator("RandomSingles".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new Single[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomSingleArrayWithRange()
        {
            var random = new Generator();
            var data = new Single[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomSingleArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new Single[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Single)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSingleArrayWithLowRange()
        {
            var random = new Generator();
            var data = new Single[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSingleArrayWithHighRange()
        {
            var random = new Generator();
            var data = new Single[10000];
            random.Fill(3 * (Single.MaxValue / 4), Single.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullSingleArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (Single[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullSingleArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Single[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullSingleSubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Single[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSingleSubArrayWithLowOffset()
        {
            var random = new Generator("RandomSingleArrayWithRange".ToCharArray());
            var data = new Single[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSingleSubArrayWithHighOffset()
        {
            var random = new Generator("RandomSingleArrayWithRange".ToCharArray());
            var data = new Single[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSingleSubArrayWithLowCount()
        {
            var random = new Generator("RandomSingleArrayWithRange".ToCharArray());
            var data = new Single[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomSingleSubArrayWithHighCount()
        {
            var random = new Generator("RandomSingleArrayWithRange".ToCharArray());
            var data = new Single[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSingleArray()
        {
            var random = new Generator("RandomSingles".ToCharArray());
            Single[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSingleArrayWithOffsetCount()
        {
            var random = new Generator("RandomSingles".ToCharArray());
            Single[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowSingleArrayOffset()
        {
            var random = new Generator("RandomSingles".ToCharArray());
            var data = new Single[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighSingleArrayOffset()
        {
            var random = new Generator("RandomSingles".ToCharArray());
            var data = new Single[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowSingleArrayCount()
        {
            var random = new Generator("RandomSingles".ToCharArray());
            var data = new Single[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighSingleArrayCount()
        {
            var random = new Generator("RandomSingles".ToCharArray());
            var data = new Single[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateSingleState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Single[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new Single[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateSingleStateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new Single[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as Single[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateSingleStateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Single[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new Single[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateSingleStateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Single[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new Single[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateSingleStateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Single[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            Single[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithSingles()
        {
            var random = new Generator("SeedWithSingles".ToCharArray());
            var data = new Single[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithSinglesAndSource()
        {
            var random = new Generator("SeedWithSingles".ToCharArray());
            var data = new Single[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomSingleSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Single[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomSingleFromEntropy()
        {
            var random = new Generator();
            var data = new Single[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomSingleFromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new Single[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthSingleArray()
        {
            var random = new Generator("FillZeroLengthSingleArray");
            var data = new Single[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((Single)0, data[0]);
            Assert.AreEqual((Single)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfSingle()
        {
            var random = new Generator("EnumeratorOfSingle");
            var data = random.Singles().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfSingleWithRange()
        {
            var random = new Generator("EnumeratorOfSingleWithRange");
            var data = random.Singles(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfSingleWithRangeLow()
        {
            var random = new Generator();
            var data = random.Singles(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfSingleWithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfSingleWithOffsetAndRange");
            var data = random.Singles(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfSingleWithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.Singles(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfSingleWithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.Singles(3 * (System.Single.MaxValue / 4), System.Single.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(Single[] array)
        {
            var minValue = 0.0;
            var maxValue = 1.0;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualDoubleValues()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualDoubleValues".ToCharArray());
            var data = new Double[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.Double();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualDoubleValuesUpToRange()
        {
            var random = new Generator();
            var data = new Double[99];
            var hash = new HashSet<Double>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Double(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualDoubleInvalidRange()
        {
            var random = new Generator();
            random.Double(0);
        }

        [TestMethod]
        public void IndividualDoubleValuesInRange()
        {
            var random = new Generator();
            var data = new Double[99];
            var hash = new HashSet<Double>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Double(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualDoubleValuesInRangeTooLow()
        {
            var random = new Generator();
            random.Double(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualDoubleValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.Double(3 * (Double.MaxValue / 4), Double.MaxValue / 2);
        }

        [TestMethod]
        public void RandomDoubleArrays()
        {
            var random = new Generator("RandomDoubles".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new Double[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomDoubleArrayWithRange()
        {
            var random = new Generator();
            var data = new Double[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomDoubleArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new Double[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Double)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDoubleArrayWithLowRange()
        {
            var random = new Generator();
            var data = new Double[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDoubleArrayWithHighRange()
        {
            var random = new Generator();
            var data = new Double[10000];
            random.Fill(3 * (Double.MaxValue / 4), Double.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullDoubleArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (Double[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullDoubleArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Double[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullDoubleSubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Double[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDoubleSubArrayWithLowOffset()
        {
            var random = new Generator("RandomDoubleArrayWithRange".ToCharArray());
            var data = new Double[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDoubleSubArrayWithHighOffset()
        {
            var random = new Generator("RandomDoubleArrayWithRange".ToCharArray());
            var data = new Double[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDoubleSubArrayWithLowCount()
        {
            var random = new Generator("RandomDoubleArrayWithRange".ToCharArray());
            var data = new Double[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDoubleSubArrayWithHighCount()
        {
            var random = new Generator("RandomDoubleArrayWithRange".ToCharArray());
            var data = new Double[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDoubleArray()
        {
            var random = new Generator("RandomDoubles".ToCharArray());
            Double[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDoubleArrayWithOffsetCount()
        {
            var random = new Generator("RandomDoubles".ToCharArray());
            Double[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowDoubleArrayOffset()
        {
            var random = new Generator("RandomDoubles".ToCharArray());
            var data = new Double[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighDoubleArrayOffset()
        {
            var random = new Generator("RandomDoubles".ToCharArray());
            var data = new Double[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowDoubleArrayCount()
        {
            var random = new Generator("RandomDoubles".ToCharArray());
            var data = new Double[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighDoubleArrayCount()
        {
            var random = new Generator("RandomDoubles".ToCharArray());
            var data = new Double[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateDoubleState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Double[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new Double[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateDoubleStateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new Double[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as Double[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateDoubleStateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Double[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new Double[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateDoubleStateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Double[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new Double[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateDoubleStateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Double[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            Double[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void SeedWithDoubles()
        {
            var random = new Generator("SeedWithDoubles".ToCharArray());
            var data = new Double[99];
            random.Fill(data);
            random = new Generator(data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void SeedWithDoublesAndSource()
        {
            var random = new Generator("SeedWithDoubles".ToCharArray());
            var data = new Double[99];
            random.Fill(data);
            var source = Generator.DefaultSourceFactory();
            random = new Generator(source, data);
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomDoubleSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Double[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomDoubleFromEntropy()
        {
            var random = new Generator();
            var data = new Double[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomDoubleFromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new Double[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthDoubleArray()
        {
            var random = new Generator("FillZeroLengthDoubleArray");
            var data = new Double[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((Double)0, data[0]);
            Assert.AreEqual((Double)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfDouble()
        {
            var random = new Generator("EnumeratorOfDouble");
            var data = random.Doubles().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfDoubleWithRange()
        {
            var random = new Generator("EnumeratorOfDoubleWithRange");
            var data = random.Doubles(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfDoubleWithRangeLow()
        {
            var random = new Generator();
            var data = random.Doubles(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfDoubleWithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfDoubleWithOffsetAndRange");
            var data = random.Doubles(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfDoubleWithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.Doubles(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfDoubleWithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.Doubles(3 * (System.Double.MaxValue / 4), System.Double.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(Double[] array)
        {
            var minValue = 0.0;
            var maxValue = 1.0;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }

        [TestMethod]
        public void IndividualDecimalValues()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualDecimalValues".ToCharArray());
            var data = new Decimal[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.Decimal();

            LooksRandom(data);
        }

        [TestMethod]
        public void IndividualDecimalValuesUpToRange()
        {
            var random = new Generator();
            var data = new Decimal[99];
            var hash = new HashSet<Decimal>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Decimal(50);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 50);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualDecimalInvalidRange()
        {
            var random = new Generator();
            random.Decimal(0);
        }

        [TestMethod]
        public void IndividualDecimalValuesInRange()
        {
            var random = new Generator();
            var data = new Decimal[99];
            var hash = new HashSet<Decimal>();

            for (int i = 0; i < 10000; i++)
            {
                var x = random.Decimal(25, 50);
                Assert.IsTrue(x >= 25);
                Assert.IsTrue(x < 75);
                hash.Add(x);
                if (hash.Count == 50) break;
            }

            Assert.AreEqual(50, hash.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualDecimalValuesInRangeTooLow()
        {
            var random = new Generator();
            random.Decimal(25, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndividualDecimalValuesInRangeTooHigh()
        {
            var random = new Generator();
            random.Decimal(3 * (Decimal.MaxValue / 4), Decimal.MaxValue / 2);
        }

        [TestMethod]
        public void RandomDecimalArrays()
        {
            var random = new Generator("RandomDecimals".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new Decimal[99 + i];
                random.Fill(data);
                LooksRandom(data);
            }
        }

        [TestMethod]
        public void RandomDecimalArrayWithRange()
        {
            var random = new Generator();
            var data = new Decimal[10000];
            random.Fill(50, data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }

            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        public void RandomDecimalArrayWithMinAndRange()
        {
            var random = new Generator();
            var data = new Decimal[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Decimal)); p++)
            {            
                random.Fill(25, 50, data);

                for (int i = 0; i < data.Length; i++)
                {
                    Assert.IsTrue(data[i] >= 25);
                    Assert.IsTrue(data[i] < 75);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDecimalArrayWithLowRange()
        {
            var random = new Generator();
            var data = new Decimal[10000];
            random.Fill(0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDecimalArrayWithHighRange()
        {
            var random = new Generator();
            var data = new Decimal[10000];
            random.Fill(3 * (Decimal.MaxValue / 4), Decimal.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullDecimalArrayWithRange()
        {
            var random = new Generator();
            random.Fill(50, (Decimal[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullDecimalArrayWithMinAndRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Decimal[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RandomNullDecimalSubArrayWithRange()
        {
            var random = new Generator();
            random.Fill(0, 50, (Decimal[])null, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDecimalSubArrayWithLowOffset()
        {
            var random = new Generator("RandomDecimalArrayWithRange".ToCharArray());
            var data = new Decimal[99];
            random.Fill(0, 50, data, -1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDecimalSubArrayWithHighOffset()
        {
            var random = new Generator("RandomDecimalArrayWithRange".ToCharArray());
            var data = new Decimal[99];
            random.Fill(0, 50, data, 99, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDecimalSubArrayWithLowCount()
        {
            var random = new Generator("RandomDecimalArrayWithRange".ToCharArray());
            var data = new Decimal[99];
            random.Fill(0, 50, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RandomDecimalSubArrayWithHighCount()
        {
            var random = new Generator("RandomDecimalArrayWithRange".ToCharArray());
            var data = new Decimal[99];
            random.Fill(0, 50, data, 0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDecimalArray()
        {
            var random = new Generator("RandomDecimals".ToCharArray());
            Decimal[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDecimalArrayWithOffsetCount()
        {
            var random = new Generator("RandomDecimals".ToCharArray());
            Decimal[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowDecimalArrayOffset()
        {
            var random = new Generator("RandomDecimals".ToCharArray());
            var data = new Decimal[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighDecimalArrayOffset()
        {
            var random = new Generator("RandomDecimals".ToCharArray());
            var data = new Decimal[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowDecimalArrayCount()
        {
            var random = new Generator("RandomDecimals".ToCharArray());
            var data = new Decimal[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighDecimalArrayCount()
        {
            var random = new Generator("RandomDecimals".ToCharArray());
            var data = new Decimal[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateDecimalState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Decimal[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new Decimal[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state);
        }

        [TestMethod]
        public void CreateDecimalStateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new Decimal[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom(state as Decimal[]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateDecimalStateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Decimal[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new Decimal[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateDecimalStateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Decimal[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new Decimal[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateDecimalStateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Decimal[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            Decimal[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void RandomDecimalSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Decimal[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomDecimalFromEntropy()
        {
            var random = new Generator();
            var data = new Decimal[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void RandomDecimalFromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new Decimal[99];
            random.Fill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void FillZeroLengthDecimalArray()
        {
            var random = new Generator("FillZeroLengthDecimalArray");
            var data = new Decimal[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual((Decimal)0, data[0]);
            Assert.AreEqual((Decimal)0, data[98]);
        }

        [TestMethod]
        public void EnumeratorOfDecimal()
        {
            var random = new Generator("EnumeratorOfDecimal");
            var data = random.Decimals().Take(99).ToArray();
            LooksRandom(data);
        }

        [TestMethod]
        public void EnumeratorOfDecimalWithRange()
        {
            var random = new Generator("EnumeratorOfDecimalWithRange");
            var data = random.Decimals(50).Take(1000).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] >= 0);
                Assert.IsTrue(data[i] < 50);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfDecimalWithRangeLow()
        {
            var random = new Generator();
            var data = random.Decimals(0).Take(1000).ToArray();
        }

        [TestMethod]
        public void EnumeratorOfDecimalWithOffsetAndRange()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "EnumeratorOfDecimalWithOffsetAndRange");
            var data = random.Decimals(25, 50).Take(10000).ToArray();
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfDecimalWithOffsetAndRangeLow()
        {
            var random = new Generator();
            var data = random.Decimals(25, 0).Take(1000).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnumeratorOfDecimalWithOffsetAndRangeHigh()
        {
            var random = new Generator();
            var data = random.Decimals(3 * (System.Decimal.MaxValue / 4), System.Decimal.MaxValue / 2).Take(1000).ToArray();
        }

        private void LooksRandom(Decimal[] array)
        {
            var minValue = 0.0;
            var maxValue = 1.0;
            var range = (maxValue - minValue) / 8;
            var min = array.Min(t => (double)t);
            Assert.IsTrue(min < minValue + range);
            var max = array.Max(t => (double)t);
            Assert.IsTrue(max > maxValue - range);
            var avg = array.Average(t => (double)t);
            var mid = (minValue + maxValue) / 2.0;
            Assert.IsTrue(avg > mid - range);
            Assert.IsTrue(avg < mid + range);
        }


        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateStateUnsupportedArray()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new string[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateStateUnsupportedObject()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new object[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void IndividualValueUnionValues()
        {
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualValueUnionValues".ToCharArray());
            var data = new ValueUnion[99];

            for (int i = 0; i < data.Length; i++)
                data[i] = random.ValueUnion();

            LooksRandom(data.Select(t => t.UInt64_0).ToArray());
        }

        [TestMethod]
        public void RandomValueUnionArrays()
        {
            var random = new Generator("RandomValueUnions".ToCharArray());

            for (int i = 0; i < 8; i++)
            {
                var data = new ValueUnion[99 + i];
                random.Fill(data);
                LooksRandom(data.Select(t => t.UInt64_0).ToArray());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullValueUnionArray()
        {
            var random = new Generator("RandomValueUnions".ToCharArray());
            ValueUnion[] data = null;
            random.Fill(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullValueUnionArrayWithOffsetCount()
        {
            var random = new Generator("RandomValueUnions".ToCharArray());
            ValueUnion[] data = null;
            random.Fill(data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowValueUnionArrayOffset()
        {
            var random = new Generator("RandomValueUnions".ToCharArray());
            var data = new ValueUnion[99];
            random.Fill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighValueUnionArrayOffset()
        {
            var random = new Generator("RandomValueUnions".ToCharArray());
            var data = new ValueUnion[99];
            random.Fill(data, data.Length + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LowValueUnionArrayCount()
        {
            var random = new Generator("RandomValueUnions".ToCharArray());
            var data = new ValueUnion[99];
            random.Fill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HighValueUnionArrayCount()
        {
            var random = new Generator("RandomValueUnions".ToCharArray());
            var data = new ValueUnion[99];
            random.Fill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void CreateValueUnionState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(ValueUnion[]) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            var state = new ValueUnion[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom((state as ValueUnion[]).Select(t => t.UInt64_0).ToArray());
        }

        [TestMethod]
        public void CreateValueUnionStateWeaklyTyped()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new ValueUnion[99];
            method.Invoke(null, new object[] { source, seed, state });
            LooksRandom((state as ValueUnion[]).Select(t => t.UInt64_0).ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateValueUnionStateNullSource()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(ValueUnion[]) }, null);
            IRandomSource source = null;
            var seed = new byte[] { 1, 2, 3 };
            var state = new ValueUnion[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateValueUnionStateNullSeed()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(ValueUnion[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = null;
            var state = new ValueUnion[99];
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateValueUnionStateNullState()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(ValueUnion[]) }, null);
            var source = new SHA256RandomSource();
            byte[] seed = new byte[] { 1, 2, 3 };
            ValueUnion[] state = null;
            method.Invoke(null, new object[] { source, seed, state });
        }

        [TestMethod]
        public void RandomValueUnionSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new ValueUnion[99];
            random.Fill(data);
            LooksRandom(data.Select(t => t.UInt64_0).ToArray());
        }

        [TestMethod]
        public void RandomValueUnionFromEntropy()
        {
            var random = new Generator();
            var data = new ValueUnion[99];
            random.Fill(data);
            LooksRandom(data.Select(t => t.UInt64_0).ToArray());
        }

        [TestMethod]
        public void RandomValueUnionFromEntropyAndSource()
        {
            var source = Generator.DefaultSourceFactory();
            var random = new Generator(source);
            var data = new ValueUnion[99];
            random.Fill(data);
            LooksRandom(data.Select(t => t.UInt64_0).ToArray());
        }

        [TestMethod]
        public void FillZeroLengthValueUnionArray()
        {
            var random = new Generator("FillZeroLengthValueUnionArray");
            var data = new ValueUnion[99];
            random.Fill(data, 0, 0);
            Assert.AreEqual(0, data[0].Int32_0);
            Assert.AreEqual(0, data[98].Int32_0);
        }

        [TestMethod]
        public void EnumeratorOfValueUnion()
        {
            var random = new Generator("EnumeratorOfValueUnion");
            var data = random.ValueUnions().Take(99).ToArray();
            LooksRandom(data.Select(t => t.UInt64_0).ToArray());
        }
    }
}
