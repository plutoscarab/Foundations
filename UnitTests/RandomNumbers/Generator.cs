
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

namespace Foundations.UnitTests.RandomNumbers
{
    [TestClass]
    public sealed class GeneratorTests
    {
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
            random.Fill(data, data.Length, 0);
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
        public void RandomByteSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Byte[99];
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
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualNonNegativeSByteValues".ToCharArray());

            for (int i = 0; i < 1000; i++)
            {
                var value = random.SByteNonNegative();
                Assert.IsTrue(value >= 0);
            }
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
            random.Fill(data, data.Length, 0);
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
        public void RandomSByteSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new SByte[99];
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
            random.Fill(data, data.Length, 0);
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
        public void RandomUInt16SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new UInt16[99];
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
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualNonNegativeInt16Values".ToCharArray());

            for (int i = 0; i < 1000; i++)
            {
                var value = random.Int16NonNegative();
                Assert.IsTrue(value >= 0);
            }
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
            random.Fill(data, data.Length, 0);
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
        public void RandomInt16SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Int16[99];
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
            random.Fill(data, data.Length, 0);
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
        public void RandomUInt32SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new UInt32[99];
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
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualNonNegativeInt32Values".ToCharArray());

            for (int i = 0; i < 1000; i++)
            {
                var value = random.Int32NonNegative();
                Assert.IsTrue(value >= 0);
            }
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
            random.Fill(data, data.Length, 0);
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
        public void RandomInt32SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Int32[99];
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
            random.Fill(data, data.Length, 0);
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
        public void RandomUInt64SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new UInt64[99];
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
            var random = new Generator(Generator.DefaultSourceFactory(), "IndividualNonNegativeInt64Values".ToCharArray());

            for (int i = 0; i < 1000; i++)
            {
                var value = random.Int64NonNegative();
                Assert.IsTrue(value >= 0);
            }
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
            random.Fill(data, data.Length, 0);
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
        public void RandomInt64SeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Int64[99];
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
            random.Fill(data, data.Length, 0);
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
        public void RandomSingleSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Single[99];
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
            random.Fill(data, data.Length, 0);
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
        public void RandomDoubleSeededWithNull()
        {
            var source = new XorShiftRandomSource();
            var random = new Generator(source, (byte[])null);
            var data = new Double[99];
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
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void CreateStateUnsupportedArray()
        {
            var method = typeof(Generator).GetMethod("CreateState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new[] { typeof(IRandomSource), typeof(byte[]), typeof(Array) }, null);
            var source = new SHA256RandomSource();
            var seed = new byte[] { 1, 2, 3 };
            Array state = new string[99];
            method.Invoke(null, new object[] { source, seed, state });
        }
    }
}
