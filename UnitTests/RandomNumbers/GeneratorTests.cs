﻿
/*
Generator.cs
*/

using Foundations.RandomNumbers;
using Foundations.Types;
using System.Collections.Generic;

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
        public void AddFillByteArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new Byte[9999];
            var data2 = new Byte[9999];
            var data3 = new Byte[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Byte)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (Byte)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillByteArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new Byte[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillByteArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new Byte[9999];
            random.AddFill(3 * (Byte.MaxValue / 4), Byte.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullByteArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (Byte[])null);
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

        [TestMethod]
        public void CreateBytes()
        {
            var random = new Generator("CreateBytes");
            var data = random.CreateBytes(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBytesLowCount()
        {
            var random = new Generator("CreateBytes");
            var data = random.CreateBytes(-1);
        }

        [TestMethod]
        public void CreateBytesZeroCount()
        {
            var random = new Generator("CreateBytes");
            var data = random.CreateBytes(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateBytesWithRange()
        {
            var random = new Generator("CreateBytes");
            var data = random.CreateBytes(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBytesLowCountWithRange()
        {
            var random = new Generator("CreateBytes");
            var data = random.CreateBytes(-1, 50);
        }

        [TestMethod]
        public void CreateBytesWithMinAndRange()
        {
            var random = new Generator("CreateBytes");
            var data = random.CreateBytes(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBytesLowCountWithMinAndRange()
        {
            var random = new Generator("CreateBytes");
            var data = random.CreateBytes(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillByteArray()
        {
            var random = new Generator("AddFillByteArray");
            var data1 = random.CreateBytes(99);
            var data2 = (Byte[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateBytes(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((Byte)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullByteArray()
        {
            var random = new Generator("AddFillNullByteArray");
            random.AddFill((Byte[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeByte()
        {
            var random = new Generator("AddFillWithNonPow2RangeByte");
            var data = new Byte[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullByteArray()
        {
            var random = new Generator("AddFillWithRangeAndNullByteArray");
            random.AddFill(64, (Byte[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndByteArray()
        {
            var random = new Generator("AddFillWithRangeAndNullByteArray");
            var data = new Byte[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountByteArray()
        {
            var random = new Generator("AddFillWithOffsetAndCountByteArray");
            var data = new Byte[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullByteArray()
        {
            var random = new Generator("AddFillWithOffsetAndNullByteArray");
            random.AddFill((Byte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetByte()
        {
            var random = new Generator("AddFillWithLowOffsetByte");
            var data = new Byte[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetByte()
        {
            var random = new Generator("AddFillWithLowOffsetByte");
            var data = new Byte[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountByte()
        {
            var random = new Generator("AddFillWithZeroCountByte");
            var data = Enumerable.Range(0, 99).Select(t => (Byte)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Byte)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountByte()
        {
            var random = new Generator("AddFillWithLowCountByte");
            var data = new Byte[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountByte()
        {
            var random = new Generator("AddFillWithHighCountByte");
            var data = new Byte[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleByte()
        {
            var random = new Generator("AddFillExactMultipleByte");
            var data = new Byte[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetByteArray()
        {
            var random = new Generator("AddFillNonPow2WithOffsetByteArray");
            var data = new Byte[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullByteArray()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullByteArray");
            random.AddFill(64, (Byte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetByte()
        {
            var random = new Generator("AddFillRangedWithLowOffsetByte");
            var data = new Byte[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetByte()
        {
            var random = new Generator("AddFillRangedWithHighOffsetByte");
            var data = new Byte[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountByte()
        {
            var random = new Generator("AddFillRangedWithZeroCountByte");
            var data = Enumerable.Range(0, 99).Select(t => (Byte)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Byte)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountByte()
        {
            var random = new Generator("AddFillRangedWithLowCountByte");
            var data = new Byte[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeByte()
        {
            var random = new Generator("AddFillRangedWithLowCountByte");
            var data = new Byte[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountByte()
        {
            var random = new Generator("AddFillRangedWithHighCountByte");
            var data = new Byte[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleByte()
        {
            var random = new Generator("AddFillRangedExactMultipleByte");
            var data = new Byte[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void XorFillByteArray()
        {
            var random = new Generator("XorFillByteArray");
            var data1 = random.CreateBytes(99);
            var data2 = (Byte[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateBytes(99);
            random = new Generator(1);
            random.XorFill(data1);
            LooksRandom(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual(data2[i] ^ data3[i], data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillNullByteArray()
        {
            var random = new Generator("XorFillNullByteArray");
            random.XorFill((Byte[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillWithNonPow2RangeByte()
        {
            var random = new Generator("XorFillWithNonPow2RangeByte");
            var data = new Byte[99];
            random.XorFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithRangeAndNullByteArray()
        {
            var random = new Generator("XorFillWithRangeAndNullByteArray");
            random.XorFill(64, (Byte[])null);
        }

        [TestMethod]
        public void XorFillWithRangeAndByteArray()
        {
            var random = new Generator("XorFillWithRangeAndNullByteArray");
            var data = new Byte[9999];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithOffsetAndNullByteArray()
        {
            var random = new Generator("XorFillWithOffsetAndNullByteArray");
            random.XorFill((Byte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowOffsetByte()
        {
            var random = new Generator("XorFillWithLowOffsetByte");
            var data = new Byte[99];
            random.XorFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighOffsetByte()
        {
            var random = new Generator("XorFillWithLowOffsetByte");
            var data = new Byte[99];
            random.XorFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillWithZeroCountByte()
        {
            var random = new Generator("XorFillWithZeroCountByte");
            var data = Enumerable.Range(0, 99).Select(t => (Byte)t).ToArray();
            random.XorFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Byte)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowCountByte()
        {
            var random = new Generator("XorFillWithLowCountByte");
            var data = new Byte[99];
            random.XorFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighCountByte()
        {
            var random = new Generator("XorFillWithHighCountByte");
            var data = new Byte[99];
            random.XorFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillExactMultipleByte()
        {
            var random = new Generator("XorFillExactMultipleByte");
            var data = new Byte[99 * 8];
            random.XorFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillNonPow2WithOffsetByteArray()
        {
            var random = new Generator("XorFillNonPow2WithOffsetByteArray");
            random.XorFill(57, (Byte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillRangedWithOffsetAndNullByteArray()
        {
            var random = new Generator("XorFillRangedWithOffsetAndNullByteArray");
            random.XorFill(64, (Byte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowOffsetByte()
        {
            var random = new Generator("XorFillRangedWithLowOffsetByte");
            var data = new Byte[99];
            random.XorFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighOffsetByte()
        {
            var random = new Generator("XorFillRangedWithHighOffsetByte");
            var data = new Byte[99];
            random.XorFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillRangedWithZeroCountByte()
        {
            var random = new Generator("XorFillRangedWithZeroCountByte");
            var data = Enumerable.Range(0, 99).Select(t => (Byte)t).ToArray();
            random.XorFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Byte)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowCountByte()
        {
            var random = new Generator("XorFillRangedWithLowCountByte");
            var data = new Byte[99];
            random.XorFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighCountByte()
        {
            var random = new Generator("XorFillRangedWithHighCountByte");
            var data = new Byte[99];
            random.XorFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillRangedExactMultipleByte()
        {
            var random = new Generator("XorFillRangedExactMultipleByte");
            var data = new Byte[99 * 8];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillSByteArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new SByte[9999];
            var data2 = new SByte[9999];
            var data3 = new SByte[9999];

            for (int p = 0; p < 10 * (8 / sizeof(SByte)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (SByte)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillSByteArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new SByte[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillSByteArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new SByte[9999];
            random.AddFill(3 * (SByte.MaxValue / 4), SByte.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullSByteArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (SByte[])null);
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

        [TestMethod]
        public void CreateSBytes()
        {
            var random = new Generator("CreateSBytes");
            var data = random.CreateSBytes(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateSBytesLowCount()
        {
            var random = new Generator("CreateSBytes");
            var data = random.CreateSBytes(-1);
        }

        [TestMethod]
        public void CreateSBytesZeroCount()
        {
            var random = new Generator("CreateSBytes");
            var data = random.CreateSBytes(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateSBytesWithRange()
        {
            var random = new Generator("CreateSBytes");
            var data = random.CreateSBytes(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateSBytesLowCountWithRange()
        {
            var random = new Generator("CreateSBytes");
            var data = random.CreateSBytes(-1, 50);
        }

        [TestMethod]
        public void CreateSBytesWithMinAndRange()
        {
            var random = new Generator("CreateSBytes");
            var data = random.CreateSBytes(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateSBytesLowCountWithMinAndRange()
        {
            var random = new Generator("CreateSBytes");
            var data = random.CreateSBytes(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillSByteArray()
        {
            var random = new Generator("AddFillSByteArray");
            var data1 = random.CreateSBytes(99);
            var data2 = (SByte[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateSBytes(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((SByte)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullSByteArray()
        {
            var random = new Generator("AddFillNullSByteArray");
            random.AddFill((SByte[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeSByte()
        {
            var random = new Generator("AddFillWithNonPow2RangeSByte");
            var data = new SByte[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullSByteArray()
        {
            var random = new Generator("AddFillWithRangeAndNullSByteArray");
            random.AddFill(64, (SByte[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndSByteArray()
        {
            var random = new Generator("AddFillWithRangeAndNullSByteArray");
            var data = new SByte[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountSByteArray()
        {
            var random = new Generator("AddFillWithOffsetAndCountSByteArray");
            var data = new SByte[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullSByteArray()
        {
            var random = new Generator("AddFillWithOffsetAndNullSByteArray");
            random.AddFill((SByte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetSByte()
        {
            var random = new Generator("AddFillWithLowOffsetSByte");
            var data = new SByte[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetSByte()
        {
            var random = new Generator("AddFillWithLowOffsetSByte");
            var data = new SByte[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountSByte()
        {
            var random = new Generator("AddFillWithZeroCountSByte");
            var data = Enumerable.Range(0, 99).Select(t => (SByte)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((SByte)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountSByte()
        {
            var random = new Generator("AddFillWithLowCountSByte");
            var data = new SByte[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountSByte()
        {
            var random = new Generator("AddFillWithHighCountSByte");
            var data = new SByte[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleSByte()
        {
            var random = new Generator("AddFillExactMultipleSByte");
            var data = new SByte[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetSByteArray()
        {
            var random = new Generator("AddFillNonPow2WithOffsetSByteArray");
            var data = new SByte[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullSByteArray()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullSByteArray");
            random.AddFill(64, (SByte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetSByte()
        {
            var random = new Generator("AddFillRangedWithLowOffsetSByte");
            var data = new SByte[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetSByte()
        {
            var random = new Generator("AddFillRangedWithHighOffsetSByte");
            var data = new SByte[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountSByte()
        {
            var random = new Generator("AddFillRangedWithZeroCountSByte");
            var data = Enumerable.Range(0, 99).Select(t => (SByte)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((SByte)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountSByte()
        {
            var random = new Generator("AddFillRangedWithLowCountSByte");
            var data = new SByte[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeSByte()
        {
            var random = new Generator("AddFillRangedWithLowCountSByte");
            var data = new SByte[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountSByte()
        {
            var random = new Generator("AddFillRangedWithHighCountSByte");
            var data = new SByte[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleSByte()
        {
            var random = new Generator("AddFillRangedExactMultipleSByte");
            var data = new SByte[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void XorFillSByteArray()
        {
            var random = new Generator("XorFillSByteArray");
            var data1 = random.CreateSBytes(99);
            var data2 = (SByte[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateSBytes(99);
            random = new Generator(1);
            random.XorFill(data1);
            LooksRandom(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual(data2[i] ^ data3[i], data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillNullSByteArray()
        {
            var random = new Generator("XorFillNullSByteArray");
            random.XorFill((SByte[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillWithNonPow2RangeSByte()
        {
            var random = new Generator("XorFillWithNonPow2RangeSByte");
            var data = new SByte[99];
            random.XorFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithRangeAndNullSByteArray()
        {
            var random = new Generator("XorFillWithRangeAndNullSByteArray");
            random.XorFill(64, (SByte[])null);
        }

        [TestMethod]
        public void XorFillWithRangeAndSByteArray()
        {
            var random = new Generator("XorFillWithRangeAndNullSByteArray");
            var data = new SByte[9999];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithOffsetAndNullSByteArray()
        {
            var random = new Generator("XorFillWithOffsetAndNullSByteArray");
            random.XorFill((SByte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowOffsetSByte()
        {
            var random = new Generator("XorFillWithLowOffsetSByte");
            var data = new SByte[99];
            random.XorFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighOffsetSByte()
        {
            var random = new Generator("XorFillWithLowOffsetSByte");
            var data = new SByte[99];
            random.XorFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillWithZeroCountSByte()
        {
            var random = new Generator("XorFillWithZeroCountSByte");
            var data = Enumerable.Range(0, 99).Select(t => (SByte)t).ToArray();
            random.XorFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((SByte)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowCountSByte()
        {
            var random = new Generator("XorFillWithLowCountSByte");
            var data = new SByte[99];
            random.XorFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighCountSByte()
        {
            var random = new Generator("XorFillWithHighCountSByte");
            var data = new SByte[99];
            random.XorFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillExactMultipleSByte()
        {
            var random = new Generator("XorFillExactMultipleSByte");
            var data = new SByte[99 * 8];
            random.XorFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillNonPow2WithOffsetSByteArray()
        {
            var random = new Generator("XorFillNonPow2WithOffsetSByteArray");
            random.XorFill(57, (SByte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillRangedWithOffsetAndNullSByteArray()
        {
            var random = new Generator("XorFillRangedWithOffsetAndNullSByteArray");
            random.XorFill(64, (SByte[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowOffsetSByte()
        {
            var random = new Generator("XorFillRangedWithLowOffsetSByte");
            var data = new SByte[99];
            random.XorFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighOffsetSByte()
        {
            var random = new Generator("XorFillRangedWithHighOffsetSByte");
            var data = new SByte[99];
            random.XorFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillRangedWithZeroCountSByte()
        {
            var random = new Generator("XorFillRangedWithZeroCountSByte");
            var data = Enumerable.Range(0, 99).Select(t => (SByte)t).ToArray();
            random.XorFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((SByte)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowCountSByte()
        {
            var random = new Generator("XorFillRangedWithLowCountSByte");
            var data = new SByte[99];
            random.XorFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighCountSByte()
        {
            var random = new Generator("XorFillRangedWithHighCountSByte");
            var data = new SByte[99];
            random.XorFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillRangedExactMultipleSByte()
        {
            var random = new Generator("XorFillRangedExactMultipleSByte");
            var data = new SByte[99 * 8];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillUInt16ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new UInt16[9999];
            var data2 = new UInt16[9999];
            var data3 = new UInt16[9999];

            for (int p = 0; p < 10 * (8 / sizeof(UInt16)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (UInt16)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillUInt16ArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new UInt16[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillUInt16ArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new UInt16[9999];
            random.AddFill(3 * (UInt16.MaxValue / 4), UInt16.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullUInt16ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (UInt16[])null);
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

        [TestMethod]
        public void CreateUInt16s()
        {
            var random = new Generator("CreateUInt16s");
            var data = random.CreateUInt16s(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateUInt16sLowCount()
        {
            var random = new Generator("CreateUInt16s");
            var data = random.CreateUInt16s(-1);
        }

        [TestMethod]
        public void CreateUInt16sZeroCount()
        {
            var random = new Generator("CreateUInt16s");
            var data = random.CreateUInt16s(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateUInt16sWithRange()
        {
            var random = new Generator("CreateUInt16s");
            var data = random.CreateUInt16s(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateUInt16sLowCountWithRange()
        {
            var random = new Generator("CreateUInt16s");
            var data = random.CreateUInt16s(-1, 50);
        }

        [TestMethod]
        public void CreateUInt16sWithMinAndRange()
        {
            var random = new Generator("CreateUInt16s");
            var data = random.CreateUInt16s(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateUInt16sLowCountWithMinAndRange()
        {
            var random = new Generator("CreateUInt16s");
            var data = random.CreateUInt16s(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillUInt16Array()
        {
            var random = new Generator("AddFillUInt16Array");
            var data1 = random.CreateUInt16s(99);
            var data2 = (UInt16[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateUInt16s(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((UInt16)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullUInt16Array()
        {
            var random = new Generator("AddFillNullUInt16Array");
            random.AddFill((UInt16[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeUInt16()
        {
            var random = new Generator("AddFillWithNonPow2RangeUInt16");
            var data = new UInt16[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullUInt16Array()
        {
            var random = new Generator("AddFillWithRangeAndNullUInt16Array");
            random.AddFill(64, (UInt16[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndUInt16Array()
        {
            var random = new Generator("AddFillWithRangeAndNullUInt16Array");
            var data = new UInt16[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountUInt16Array()
        {
            var random = new Generator("AddFillWithOffsetAndCountUInt16Array");
            var data = new UInt16[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullUInt16Array()
        {
            var random = new Generator("AddFillWithOffsetAndNullUInt16Array");
            random.AddFill((UInt16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetUInt16()
        {
            var random = new Generator("AddFillWithLowOffsetUInt16");
            var data = new UInt16[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetUInt16()
        {
            var random = new Generator("AddFillWithLowOffsetUInt16");
            var data = new UInt16[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountUInt16()
        {
            var random = new Generator("AddFillWithZeroCountUInt16");
            var data = Enumerable.Range(0, 99).Select(t => (UInt16)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt16)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountUInt16()
        {
            var random = new Generator("AddFillWithLowCountUInt16");
            var data = new UInt16[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountUInt16()
        {
            var random = new Generator("AddFillWithHighCountUInt16");
            var data = new UInt16[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleUInt16()
        {
            var random = new Generator("AddFillExactMultipleUInt16");
            var data = new UInt16[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetUInt16Array()
        {
            var random = new Generator("AddFillNonPow2WithOffsetUInt16Array");
            var data = new UInt16[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullUInt16Array()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullUInt16Array");
            random.AddFill(64, (UInt16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetUInt16()
        {
            var random = new Generator("AddFillRangedWithLowOffsetUInt16");
            var data = new UInt16[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetUInt16()
        {
            var random = new Generator("AddFillRangedWithHighOffsetUInt16");
            var data = new UInt16[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountUInt16()
        {
            var random = new Generator("AddFillRangedWithZeroCountUInt16");
            var data = Enumerable.Range(0, 99).Select(t => (UInt16)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt16)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountUInt16()
        {
            var random = new Generator("AddFillRangedWithLowCountUInt16");
            var data = new UInt16[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeUInt16()
        {
            var random = new Generator("AddFillRangedWithLowCountUInt16");
            var data = new UInt16[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountUInt16()
        {
            var random = new Generator("AddFillRangedWithHighCountUInt16");
            var data = new UInt16[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleUInt16()
        {
            var random = new Generator("AddFillRangedExactMultipleUInt16");
            var data = new UInt16[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void XorFillUInt16Array()
        {
            var random = new Generator("XorFillUInt16Array");
            var data1 = random.CreateUInt16s(99);
            var data2 = (UInt16[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateUInt16s(99);
            random = new Generator(1);
            random.XorFill(data1);
            LooksRandom(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual(data2[i] ^ data3[i], data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillNullUInt16Array()
        {
            var random = new Generator("XorFillNullUInt16Array");
            random.XorFill((UInt16[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillWithNonPow2RangeUInt16()
        {
            var random = new Generator("XorFillWithNonPow2RangeUInt16");
            var data = new UInt16[99];
            random.XorFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithRangeAndNullUInt16Array()
        {
            var random = new Generator("XorFillWithRangeAndNullUInt16Array");
            random.XorFill(64, (UInt16[])null);
        }

        [TestMethod]
        public void XorFillWithRangeAndUInt16Array()
        {
            var random = new Generator("XorFillWithRangeAndNullUInt16Array");
            var data = new UInt16[9999];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithOffsetAndNullUInt16Array()
        {
            var random = new Generator("XorFillWithOffsetAndNullUInt16Array");
            random.XorFill((UInt16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowOffsetUInt16()
        {
            var random = new Generator("XorFillWithLowOffsetUInt16");
            var data = new UInt16[99];
            random.XorFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighOffsetUInt16()
        {
            var random = new Generator("XorFillWithLowOffsetUInt16");
            var data = new UInt16[99];
            random.XorFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillWithZeroCountUInt16()
        {
            var random = new Generator("XorFillWithZeroCountUInt16");
            var data = Enumerable.Range(0, 99).Select(t => (UInt16)t).ToArray();
            random.XorFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt16)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowCountUInt16()
        {
            var random = new Generator("XorFillWithLowCountUInt16");
            var data = new UInt16[99];
            random.XorFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighCountUInt16()
        {
            var random = new Generator("XorFillWithHighCountUInt16");
            var data = new UInt16[99];
            random.XorFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillExactMultipleUInt16()
        {
            var random = new Generator("XorFillExactMultipleUInt16");
            var data = new UInt16[99 * 8];
            random.XorFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillNonPow2WithOffsetUInt16Array()
        {
            var random = new Generator("XorFillNonPow2WithOffsetUInt16Array");
            random.XorFill(57, (UInt16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillRangedWithOffsetAndNullUInt16Array()
        {
            var random = new Generator("XorFillRangedWithOffsetAndNullUInt16Array");
            random.XorFill(64, (UInt16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowOffsetUInt16()
        {
            var random = new Generator("XorFillRangedWithLowOffsetUInt16");
            var data = new UInt16[99];
            random.XorFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighOffsetUInt16()
        {
            var random = new Generator("XorFillRangedWithHighOffsetUInt16");
            var data = new UInt16[99];
            random.XorFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillRangedWithZeroCountUInt16()
        {
            var random = new Generator("XorFillRangedWithZeroCountUInt16");
            var data = Enumerable.Range(0, 99).Select(t => (UInt16)t).ToArray();
            random.XorFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt16)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowCountUInt16()
        {
            var random = new Generator("XorFillRangedWithLowCountUInt16");
            var data = new UInt16[99];
            random.XorFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighCountUInt16()
        {
            var random = new Generator("XorFillRangedWithHighCountUInt16");
            var data = new UInt16[99];
            random.XorFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillRangedExactMultipleUInt16()
        {
            var random = new Generator("XorFillRangedExactMultipleUInt16");
            var data = new UInt16[99 * 8];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillInt16ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new Int16[9999];
            var data2 = new Int16[9999];
            var data3 = new Int16[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Int16)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (Int16)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillInt16ArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new Int16[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillInt16ArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new Int16[9999];
            random.AddFill(3 * (Int16.MaxValue / 4), Int16.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullInt16ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (Int16[])null);
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

        [TestMethod]
        public void CreateInt16s()
        {
            var random = new Generator("CreateInt16s");
            var data = random.CreateInt16s(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInt16sLowCount()
        {
            var random = new Generator("CreateInt16s");
            var data = random.CreateInt16s(-1);
        }

        [TestMethod]
        public void CreateInt16sZeroCount()
        {
            var random = new Generator("CreateInt16s");
            var data = random.CreateInt16s(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateInt16sWithRange()
        {
            var random = new Generator("CreateInt16s");
            var data = random.CreateInt16s(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInt16sLowCountWithRange()
        {
            var random = new Generator("CreateInt16s");
            var data = random.CreateInt16s(-1, 50);
        }

        [TestMethod]
        public void CreateInt16sWithMinAndRange()
        {
            var random = new Generator("CreateInt16s");
            var data = random.CreateInt16s(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInt16sLowCountWithMinAndRange()
        {
            var random = new Generator("CreateInt16s");
            var data = random.CreateInt16s(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillInt16Array()
        {
            var random = new Generator("AddFillInt16Array");
            var data1 = random.CreateInt16s(99);
            var data2 = (Int16[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateInt16s(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((Int16)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullInt16Array()
        {
            var random = new Generator("AddFillNullInt16Array");
            random.AddFill((Int16[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeInt16()
        {
            var random = new Generator("AddFillWithNonPow2RangeInt16");
            var data = new Int16[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullInt16Array()
        {
            var random = new Generator("AddFillWithRangeAndNullInt16Array");
            random.AddFill(64, (Int16[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndInt16Array()
        {
            var random = new Generator("AddFillWithRangeAndNullInt16Array");
            var data = new Int16[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountInt16Array()
        {
            var random = new Generator("AddFillWithOffsetAndCountInt16Array");
            var data = new Int16[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullInt16Array()
        {
            var random = new Generator("AddFillWithOffsetAndNullInt16Array");
            random.AddFill((Int16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetInt16()
        {
            var random = new Generator("AddFillWithLowOffsetInt16");
            var data = new Int16[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetInt16()
        {
            var random = new Generator("AddFillWithLowOffsetInt16");
            var data = new Int16[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountInt16()
        {
            var random = new Generator("AddFillWithZeroCountInt16");
            var data = Enumerable.Range(0, 99).Select(t => (Int16)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int16)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountInt16()
        {
            var random = new Generator("AddFillWithLowCountInt16");
            var data = new Int16[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountInt16()
        {
            var random = new Generator("AddFillWithHighCountInt16");
            var data = new Int16[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleInt16()
        {
            var random = new Generator("AddFillExactMultipleInt16");
            var data = new Int16[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetInt16Array()
        {
            var random = new Generator("AddFillNonPow2WithOffsetInt16Array");
            var data = new Int16[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullInt16Array()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullInt16Array");
            random.AddFill(64, (Int16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetInt16()
        {
            var random = new Generator("AddFillRangedWithLowOffsetInt16");
            var data = new Int16[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetInt16()
        {
            var random = new Generator("AddFillRangedWithHighOffsetInt16");
            var data = new Int16[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountInt16()
        {
            var random = new Generator("AddFillRangedWithZeroCountInt16");
            var data = Enumerable.Range(0, 99).Select(t => (Int16)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int16)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountInt16()
        {
            var random = new Generator("AddFillRangedWithLowCountInt16");
            var data = new Int16[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeInt16()
        {
            var random = new Generator("AddFillRangedWithLowCountInt16");
            var data = new Int16[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountInt16()
        {
            var random = new Generator("AddFillRangedWithHighCountInt16");
            var data = new Int16[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleInt16()
        {
            var random = new Generator("AddFillRangedExactMultipleInt16");
            var data = new Int16[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void XorFillInt16Array()
        {
            var random = new Generator("XorFillInt16Array");
            var data1 = random.CreateInt16s(99);
            var data2 = (Int16[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateInt16s(99);
            random = new Generator(1);
            random.XorFill(data1);
            LooksRandom(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual(data2[i] ^ data3[i], data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillNullInt16Array()
        {
            var random = new Generator("XorFillNullInt16Array");
            random.XorFill((Int16[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillWithNonPow2RangeInt16()
        {
            var random = new Generator("XorFillWithNonPow2RangeInt16");
            var data = new Int16[99];
            random.XorFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithRangeAndNullInt16Array()
        {
            var random = new Generator("XorFillWithRangeAndNullInt16Array");
            random.XorFill(64, (Int16[])null);
        }

        [TestMethod]
        public void XorFillWithRangeAndInt16Array()
        {
            var random = new Generator("XorFillWithRangeAndNullInt16Array");
            var data = new Int16[9999];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithOffsetAndNullInt16Array()
        {
            var random = new Generator("XorFillWithOffsetAndNullInt16Array");
            random.XorFill((Int16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowOffsetInt16()
        {
            var random = new Generator("XorFillWithLowOffsetInt16");
            var data = new Int16[99];
            random.XorFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighOffsetInt16()
        {
            var random = new Generator("XorFillWithLowOffsetInt16");
            var data = new Int16[99];
            random.XorFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillWithZeroCountInt16()
        {
            var random = new Generator("XorFillWithZeroCountInt16");
            var data = Enumerable.Range(0, 99).Select(t => (Int16)t).ToArray();
            random.XorFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int16)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowCountInt16()
        {
            var random = new Generator("XorFillWithLowCountInt16");
            var data = new Int16[99];
            random.XorFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighCountInt16()
        {
            var random = new Generator("XorFillWithHighCountInt16");
            var data = new Int16[99];
            random.XorFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillExactMultipleInt16()
        {
            var random = new Generator("XorFillExactMultipleInt16");
            var data = new Int16[99 * 8];
            random.XorFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillNonPow2WithOffsetInt16Array()
        {
            var random = new Generator("XorFillNonPow2WithOffsetInt16Array");
            random.XorFill(57, (Int16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillRangedWithOffsetAndNullInt16Array()
        {
            var random = new Generator("XorFillRangedWithOffsetAndNullInt16Array");
            random.XorFill(64, (Int16[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowOffsetInt16()
        {
            var random = new Generator("XorFillRangedWithLowOffsetInt16");
            var data = new Int16[99];
            random.XorFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighOffsetInt16()
        {
            var random = new Generator("XorFillRangedWithHighOffsetInt16");
            var data = new Int16[99];
            random.XorFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillRangedWithZeroCountInt16()
        {
            var random = new Generator("XorFillRangedWithZeroCountInt16");
            var data = Enumerable.Range(0, 99).Select(t => (Int16)t).ToArray();
            random.XorFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int16)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowCountInt16()
        {
            var random = new Generator("XorFillRangedWithLowCountInt16");
            var data = new Int16[99];
            random.XorFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighCountInt16()
        {
            var random = new Generator("XorFillRangedWithHighCountInt16");
            var data = new Int16[99];
            random.XorFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillRangedExactMultipleInt16()
        {
            var random = new Generator("XorFillRangedExactMultipleInt16");
            var data = new Int16[99 * 8];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillUInt32ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new UInt32[9999];
            var data2 = new UInt32[9999];
            var data3 = new UInt32[9999];

            for (int p = 0; p < 10 * (8 / sizeof(UInt32)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (UInt32)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillUInt32ArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new UInt32[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillUInt32ArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new UInt32[9999];
            random.AddFill(3 * (UInt32.MaxValue / 4), UInt32.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullUInt32ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (UInt32[])null);
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

        [TestMethod]
        public void CreateUInt32s()
        {
            var random = new Generator("CreateUInt32s");
            var data = random.CreateUInt32s(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateUInt32sLowCount()
        {
            var random = new Generator("CreateUInt32s");
            var data = random.CreateUInt32s(-1);
        }

        [TestMethod]
        public void CreateUInt32sZeroCount()
        {
            var random = new Generator("CreateUInt32s");
            var data = random.CreateUInt32s(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateUInt32sWithRange()
        {
            var random = new Generator("CreateUInt32s");
            var data = random.CreateUInt32s(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateUInt32sLowCountWithRange()
        {
            var random = new Generator("CreateUInt32s");
            var data = random.CreateUInt32s(-1, 50);
        }

        [TestMethod]
        public void CreateUInt32sWithMinAndRange()
        {
            var random = new Generator("CreateUInt32s");
            var data = random.CreateUInt32s(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateUInt32sLowCountWithMinAndRange()
        {
            var random = new Generator("CreateUInt32s");
            var data = random.CreateUInt32s(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillUInt32Array()
        {
            var random = new Generator("AddFillUInt32Array");
            var data1 = random.CreateUInt32s(99);
            var data2 = (UInt32[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateUInt32s(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((UInt32)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullUInt32Array()
        {
            var random = new Generator("AddFillNullUInt32Array");
            random.AddFill((UInt32[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeUInt32()
        {
            var random = new Generator("AddFillWithNonPow2RangeUInt32");
            var data = new UInt32[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullUInt32Array()
        {
            var random = new Generator("AddFillWithRangeAndNullUInt32Array");
            random.AddFill(64, (UInt32[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndUInt32Array()
        {
            var random = new Generator("AddFillWithRangeAndNullUInt32Array");
            var data = new UInt32[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountUInt32Array()
        {
            var random = new Generator("AddFillWithOffsetAndCountUInt32Array");
            var data = new UInt32[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullUInt32Array()
        {
            var random = new Generator("AddFillWithOffsetAndNullUInt32Array");
            random.AddFill((UInt32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetUInt32()
        {
            var random = new Generator("AddFillWithLowOffsetUInt32");
            var data = new UInt32[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetUInt32()
        {
            var random = new Generator("AddFillWithLowOffsetUInt32");
            var data = new UInt32[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountUInt32()
        {
            var random = new Generator("AddFillWithZeroCountUInt32");
            var data = Enumerable.Range(0, 99).Select(t => (UInt32)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt32)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountUInt32()
        {
            var random = new Generator("AddFillWithLowCountUInt32");
            var data = new UInt32[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountUInt32()
        {
            var random = new Generator("AddFillWithHighCountUInt32");
            var data = new UInt32[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleUInt32()
        {
            var random = new Generator("AddFillExactMultipleUInt32");
            var data = new UInt32[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetUInt32Array()
        {
            var random = new Generator("AddFillNonPow2WithOffsetUInt32Array");
            var data = new UInt32[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullUInt32Array()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullUInt32Array");
            random.AddFill(64, (UInt32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetUInt32()
        {
            var random = new Generator("AddFillRangedWithLowOffsetUInt32");
            var data = new UInt32[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetUInt32()
        {
            var random = new Generator("AddFillRangedWithHighOffsetUInt32");
            var data = new UInt32[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountUInt32()
        {
            var random = new Generator("AddFillRangedWithZeroCountUInt32");
            var data = Enumerable.Range(0, 99).Select(t => (UInt32)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt32)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountUInt32()
        {
            var random = new Generator("AddFillRangedWithLowCountUInt32");
            var data = new UInt32[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeUInt32()
        {
            var random = new Generator("AddFillRangedWithLowCountUInt32");
            var data = new UInt32[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountUInt32()
        {
            var random = new Generator("AddFillRangedWithHighCountUInt32");
            var data = new UInt32[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleUInt32()
        {
            var random = new Generator("AddFillRangedExactMultipleUInt32");
            var data = new UInt32[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void XorFillUInt32Array()
        {
            var random = new Generator("XorFillUInt32Array");
            var data1 = random.CreateUInt32s(99);
            var data2 = (UInt32[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateUInt32s(99);
            random = new Generator(1);
            random.XorFill(data1);
            LooksRandom(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual(data2[i] ^ data3[i], data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillNullUInt32Array()
        {
            var random = new Generator("XorFillNullUInt32Array");
            random.XorFill((UInt32[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillWithNonPow2RangeUInt32()
        {
            var random = new Generator("XorFillWithNonPow2RangeUInt32");
            var data = new UInt32[99];
            random.XorFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithRangeAndNullUInt32Array()
        {
            var random = new Generator("XorFillWithRangeAndNullUInt32Array");
            random.XorFill(64, (UInt32[])null);
        }

        [TestMethod]
        public void XorFillWithRangeAndUInt32Array()
        {
            var random = new Generator("XorFillWithRangeAndNullUInt32Array");
            var data = new UInt32[9999];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithOffsetAndNullUInt32Array()
        {
            var random = new Generator("XorFillWithOffsetAndNullUInt32Array");
            random.XorFill((UInt32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowOffsetUInt32()
        {
            var random = new Generator("XorFillWithLowOffsetUInt32");
            var data = new UInt32[99];
            random.XorFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighOffsetUInt32()
        {
            var random = new Generator("XorFillWithLowOffsetUInt32");
            var data = new UInt32[99];
            random.XorFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillWithZeroCountUInt32()
        {
            var random = new Generator("XorFillWithZeroCountUInt32");
            var data = Enumerable.Range(0, 99).Select(t => (UInt32)t).ToArray();
            random.XorFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt32)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowCountUInt32()
        {
            var random = new Generator("XorFillWithLowCountUInt32");
            var data = new UInt32[99];
            random.XorFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighCountUInt32()
        {
            var random = new Generator("XorFillWithHighCountUInt32");
            var data = new UInt32[99];
            random.XorFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillExactMultipleUInt32()
        {
            var random = new Generator("XorFillExactMultipleUInt32");
            var data = new UInt32[99 * 8];
            random.XorFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillNonPow2WithOffsetUInt32Array()
        {
            var random = new Generator("XorFillNonPow2WithOffsetUInt32Array");
            random.XorFill(57, (UInt32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillRangedWithOffsetAndNullUInt32Array()
        {
            var random = new Generator("XorFillRangedWithOffsetAndNullUInt32Array");
            random.XorFill(64, (UInt32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowOffsetUInt32()
        {
            var random = new Generator("XorFillRangedWithLowOffsetUInt32");
            var data = new UInt32[99];
            random.XorFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighOffsetUInt32()
        {
            var random = new Generator("XorFillRangedWithHighOffsetUInt32");
            var data = new UInt32[99];
            random.XorFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillRangedWithZeroCountUInt32()
        {
            var random = new Generator("XorFillRangedWithZeroCountUInt32");
            var data = Enumerable.Range(0, 99).Select(t => (UInt32)t).ToArray();
            random.XorFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt32)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowCountUInt32()
        {
            var random = new Generator("XorFillRangedWithLowCountUInt32");
            var data = new UInt32[99];
            random.XorFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighCountUInt32()
        {
            var random = new Generator("XorFillRangedWithHighCountUInt32");
            var data = new UInt32[99];
            random.XorFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillRangedExactMultipleUInt32()
        {
            var random = new Generator("XorFillRangedExactMultipleUInt32");
            var data = new UInt32[99 * 8];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillInt32ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new Int32[9999];
            var data2 = new Int32[9999];
            var data3 = new Int32[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Int32)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (Int32)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillInt32ArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new Int32[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillInt32ArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new Int32[9999];
            random.AddFill(3 * (Int32.MaxValue / 4), Int32.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullInt32ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (Int32[])null);
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

        [TestMethod]
        public void CreateInt32s()
        {
            var random = new Generator("CreateInt32s");
            var data = random.CreateInt32s(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInt32sLowCount()
        {
            var random = new Generator("CreateInt32s");
            var data = random.CreateInt32s(-1);
        }

        [TestMethod]
        public void CreateInt32sZeroCount()
        {
            var random = new Generator("CreateInt32s");
            var data = random.CreateInt32s(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateInt32sWithRange()
        {
            var random = new Generator("CreateInt32s");
            var data = random.CreateInt32s(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInt32sLowCountWithRange()
        {
            var random = new Generator("CreateInt32s");
            var data = random.CreateInt32s(-1, 50);
        }

        [TestMethod]
        public void CreateInt32sWithMinAndRange()
        {
            var random = new Generator("CreateInt32s");
            var data = random.CreateInt32s(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInt32sLowCountWithMinAndRange()
        {
            var random = new Generator("CreateInt32s");
            var data = random.CreateInt32s(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillInt32Array()
        {
            var random = new Generator("AddFillInt32Array");
            var data1 = random.CreateInt32s(99);
            var data2 = (Int32[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateInt32s(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((Int32)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullInt32Array()
        {
            var random = new Generator("AddFillNullInt32Array");
            random.AddFill((Int32[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeInt32()
        {
            var random = new Generator("AddFillWithNonPow2RangeInt32");
            var data = new Int32[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullInt32Array()
        {
            var random = new Generator("AddFillWithRangeAndNullInt32Array");
            random.AddFill(64, (Int32[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndInt32Array()
        {
            var random = new Generator("AddFillWithRangeAndNullInt32Array");
            var data = new Int32[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountInt32Array()
        {
            var random = new Generator("AddFillWithOffsetAndCountInt32Array");
            var data = new Int32[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullInt32Array()
        {
            var random = new Generator("AddFillWithOffsetAndNullInt32Array");
            random.AddFill((Int32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetInt32()
        {
            var random = new Generator("AddFillWithLowOffsetInt32");
            var data = new Int32[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetInt32()
        {
            var random = new Generator("AddFillWithLowOffsetInt32");
            var data = new Int32[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountInt32()
        {
            var random = new Generator("AddFillWithZeroCountInt32");
            var data = Enumerable.Range(0, 99).Select(t => (Int32)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int32)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountInt32()
        {
            var random = new Generator("AddFillWithLowCountInt32");
            var data = new Int32[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountInt32()
        {
            var random = new Generator("AddFillWithHighCountInt32");
            var data = new Int32[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleInt32()
        {
            var random = new Generator("AddFillExactMultipleInt32");
            var data = new Int32[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetInt32Array()
        {
            var random = new Generator("AddFillNonPow2WithOffsetInt32Array");
            var data = new Int32[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullInt32Array()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullInt32Array");
            random.AddFill(64, (Int32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetInt32()
        {
            var random = new Generator("AddFillRangedWithLowOffsetInt32");
            var data = new Int32[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetInt32()
        {
            var random = new Generator("AddFillRangedWithHighOffsetInt32");
            var data = new Int32[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountInt32()
        {
            var random = new Generator("AddFillRangedWithZeroCountInt32");
            var data = Enumerable.Range(0, 99).Select(t => (Int32)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int32)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountInt32()
        {
            var random = new Generator("AddFillRangedWithLowCountInt32");
            var data = new Int32[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeInt32()
        {
            var random = new Generator("AddFillRangedWithLowCountInt32");
            var data = new Int32[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountInt32()
        {
            var random = new Generator("AddFillRangedWithHighCountInt32");
            var data = new Int32[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleInt32()
        {
            var random = new Generator("AddFillRangedExactMultipleInt32");
            var data = new Int32[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void XorFillInt32Array()
        {
            var random = new Generator("XorFillInt32Array");
            var data1 = random.CreateInt32s(99);
            var data2 = (Int32[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateInt32s(99);
            random = new Generator(1);
            random.XorFill(data1);
            LooksRandom(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual(data2[i] ^ data3[i], data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillNullInt32Array()
        {
            var random = new Generator("XorFillNullInt32Array");
            random.XorFill((Int32[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillWithNonPow2RangeInt32()
        {
            var random = new Generator("XorFillWithNonPow2RangeInt32");
            var data = new Int32[99];
            random.XorFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithRangeAndNullInt32Array()
        {
            var random = new Generator("XorFillWithRangeAndNullInt32Array");
            random.XorFill(64, (Int32[])null);
        }

        [TestMethod]
        public void XorFillWithRangeAndInt32Array()
        {
            var random = new Generator("XorFillWithRangeAndNullInt32Array");
            var data = new Int32[9999];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithOffsetAndNullInt32Array()
        {
            var random = new Generator("XorFillWithOffsetAndNullInt32Array");
            random.XorFill((Int32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowOffsetInt32()
        {
            var random = new Generator("XorFillWithLowOffsetInt32");
            var data = new Int32[99];
            random.XorFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighOffsetInt32()
        {
            var random = new Generator("XorFillWithLowOffsetInt32");
            var data = new Int32[99];
            random.XorFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillWithZeroCountInt32()
        {
            var random = new Generator("XorFillWithZeroCountInt32");
            var data = Enumerable.Range(0, 99).Select(t => (Int32)t).ToArray();
            random.XorFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int32)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowCountInt32()
        {
            var random = new Generator("XorFillWithLowCountInt32");
            var data = new Int32[99];
            random.XorFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighCountInt32()
        {
            var random = new Generator("XorFillWithHighCountInt32");
            var data = new Int32[99];
            random.XorFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillExactMultipleInt32()
        {
            var random = new Generator("XorFillExactMultipleInt32");
            var data = new Int32[99 * 8];
            random.XorFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillNonPow2WithOffsetInt32Array()
        {
            var random = new Generator("XorFillNonPow2WithOffsetInt32Array");
            random.XorFill(57, (Int32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillRangedWithOffsetAndNullInt32Array()
        {
            var random = new Generator("XorFillRangedWithOffsetAndNullInt32Array");
            random.XorFill(64, (Int32[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowOffsetInt32()
        {
            var random = new Generator("XorFillRangedWithLowOffsetInt32");
            var data = new Int32[99];
            random.XorFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighOffsetInt32()
        {
            var random = new Generator("XorFillRangedWithHighOffsetInt32");
            var data = new Int32[99];
            random.XorFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillRangedWithZeroCountInt32()
        {
            var random = new Generator("XorFillRangedWithZeroCountInt32");
            var data = Enumerable.Range(0, 99).Select(t => (Int32)t).ToArray();
            random.XorFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int32)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowCountInt32()
        {
            var random = new Generator("XorFillRangedWithLowCountInt32");
            var data = new Int32[99];
            random.XorFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighCountInt32()
        {
            var random = new Generator("XorFillRangedWithHighCountInt32");
            var data = new Int32[99];
            random.XorFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillRangedExactMultipleInt32()
        {
            var random = new Generator("XorFillRangedExactMultipleInt32");
            var data = new Int32[99 * 8];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillUInt64ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new UInt64[9999];
            var data2 = new UInt64[9999];
            var data3 = new UInt64[9999];

            for (int p = 0; p < 10 * (8 / sizeof(UInt64)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (UInt64)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillUInt64ArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new UInt64[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillUInt64ArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new UInt64[9999];
            random.AddFill(3 * (UInt64.MaxValue / 4), UInt64.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullUInt64ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (UInt64[])null);
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

        [TestMethod]
        public void CreateUInt64s()
        {
            var random = new Generator("CreateUInt64s");
            var data = random.CreateUInt64s(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateUInt64sLowCount()
        {
            var random = new Generator("CreateUInt64s");
            var data = random.CreateUInt64s(-1);
        }

        [TestMethod]
        public void CreateUInt64sZeroCount()
        {
            var random = new Generator("CreateUInt64s");
            var data = random.CreateUInt64s(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateUInt64sWithRange()
        {
            var random = new Generator("CreateUInt64s");
            var data = random.CreateUInt64s(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateUInt64sLowCountWithRange()
        {
            var random = new Generator("CreateUInt64s");
            var data = random.CreateUInt64s(-1, 50);
        }

        [TestMethod]
        public void CreateUInt64sWithMinAndRange()
        {
            var random = new Generator("CreateUInt64s");
            var data = random.CreateUInt64s(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateUInt64sLowCountWithMinAndRange()
        {
            var random = new Generator("CreateUInt64s");
            var data = random.CreateUInt64s(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillUInt64Array()
        {
            var random = new Generator("AddFillUInt64Array");
            var data1 = random.CreateUInt64s(99);
            var data2 = (UInt64[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateUInt64s(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((UInt64)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullUInt64Array()
        {
            var random = new Generator("AddFillNullUInt64Array");
            random.AddFill((UInt64[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeUInt64()
        {
            var random = new Generator("AddFillWithNonPow2RangeUInt64");
            var data = new UInt64[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullUInt64Array()
        {
            var random = new Generator("AddFillWithRangeAndNullUInt64Array");
            random.AddFill(64, (UInt64[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndUInt64Array()
        {
            var random = new Generator("AddFillWithRangeAndNullUInt64Array");
            var data = new UInt64[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountUInt64Array()
        {
            var random = new Generator("AddFillWithOffsetAndCountUInt64Array");
            var data = new UInt64[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullUInt64Array()
        {
            var random = new Generator("AddFillWithOffsetAndNullUInt64Array");
            random.AddFill((UInt64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetUInt64()
        {
            var random = new Generator("AddFillWithLowOffsetUInt64");
            var data = new UInt64[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetUInt64()
        {
            var random = new Generator("AddFillWithLowOffsetUInt64");
            var data = new UInt64[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountUInt64()
        {
            var random = new Generator("AddFillWithZeroCountUInt64");
            var data = Enumerable.Range(0, 99).Select(t => (UInt64)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt64)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountUInt64()
        {
            var random = new Generator("AddFillWithLowCountUInt64");
            var data = new UInt64[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountUInt64()
        {
            var random = new Generator("AddFillWithHighCountUInt64");
            var data = new UInt64[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleUInt64()
        {
            var random = new Generator("AddFillExactMultipleUInt64");
            var data = new UInt64[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetUInt64Array()
        {
            var random = new Generator("AddFillNonPow2WithOffsetUInt64Array");
            var data = new UInt64[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullUInt64Array()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullUInt64Array");
            random.AddFill(64, (UInt64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetUInt64()
        {
            var random = new Generator("AddFillRangedWithLowOffsetUInt64");
            var data = new UInt64[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetUInt64()
        {
            var random = new Generator("AddFillRangedWithHighOffsetUInt64");
            var data = new UInt64[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountUInt64()
        {
            var random = new Generator("AddFillRangedWithZeroCountUInt64");
            var data = Enumerable.Range(0, 99).Select(t => (UInt64)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt64)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountUInt64()
        {
            var random = new Generator("AddFillRangedWithLowCountUInt64");
            var data = new UInt64[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeUInt64()
        {
            var random = new Generator("AddFillRangedWithLowCountUInt64");
            var data = new UInt64[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountUInt64()
        {
            var random = new Generator("AddFillRangedWithHighCountUInt64");
            var data = new UInt64[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleUInt64()
        {
            var random = new Generator("AddFillRangedExactMultipleUInt64");
            var data = new UInt64[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void XorFillUInt64Array()
        {
            var random = new Generator("XorFillUInt64Array");
            var data1 = random.CreateUInt64s(99);
            var data2 = (UInt64[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateUInt64s(99);
            random = new Generator(1);
            random.XorFill(data1);
            LooksRandom(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual(data2[i] ^ data3[i], data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillNullUInt64Array()
        {
            var random = new Generator("XorFillNullUInt64Array");
            random.XorFill((UInt64[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillWithNonPow2RangeUInt64()
        {
            var random = new Generator("XorFillWithNonPow2RangeUInt64");
            var data = new UInt64[99];
            random.XorFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithRangeAndNullUInt64Array()
        {
            var random = new Generator("XorFillWithRangeAndNullUInt64Array");
            random.XorFill(64, (UInt64[])null);
        }

        [TestMethod]
        public void XorFillWithRangeAndUInt64Array()
        {
            var random = new Generator("XorFillWithRangeAndNullUInt64Array");
            var data = new UInt64[9999];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithOffsetAndNullUInt64Array()
        {
            var random = new Generator("XorFillWithOffsetAndNullUInt64Array");
            random.XorFill((UInt64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowOffsetUInt64()
        {
            var random = new Generator("XorFillWithLowOffsetUInt64");
            var data = new UInt64[99];
            random.XorFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighOffsetUInt64()
        {
            var random = new Generator("XorFillWithLowOffsetUInt64");
            var data = new UInt64[99];
            random.XorFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillWithZeroCountUInt64()
        {
            var random = new Generator("XorFillWithZeroCountUInt64");
            var data = Enumerable.Range(0, 99).Select(t => (UInt64)t).ToArray();
            random.XorFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt64)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowCountUInt64()
        {
            var random = new Generator("XorFillWithLowCountUInt64");
            var data = new UInt64[99];
            random.XorFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighCountUInt64()
        {
            var random = new Generator("XorFillWithHighCountUInt64");
            var data = new UInt64[99];
            random.XorFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillExactMultipleUInt64()
        {
            var random = new Generator("XorFillExactMultipleUInt64");
            var data = new UInt64[99 * 8];
            random.XorFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillNonPow2WithOffsetUInt64Array()
        {
            var random = new Generator("XorFillNonPow2WithOffsetUInt64Array");
            random.XorFill(57, (UInt64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillRangedWithOffsetAndNullUInt64Array()
        {
            var random = new Generator("XorFillRangedWithOffsetAndNullUInt64Array");
            random.XorFill(64, (UInt64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowOffsetUInt64()
        {
            var random = new Generator("XorFillRangedWithLowOffsetUInt64");
            var data = new UInt64[99];
            random.XorFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighOffsetUInt64()
        {
            var random = new Generator("XorFillRangedWithHighOffsetUInt64");
            var data = new UInt64[99];
            random.XorFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillRangedWithZeroCountUInt64()
        {
            var random = new Generator("XorFillRangedWithZeroCountUInt64");
            var data = Enumerable.Range(0, 99).Select(t => (UInt64)t).ToArray();
            random.XorFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((UInt64)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowCountUInt64()
        {
            var random = new Generator("XorFillRangedWithLowCountUInt64");
            var data = new UInt64[99];
            random.XorFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighCountUInt64()
        {
            var random = new Generator("XorFillRangedWithHighCountUInt64");
            var data = new UInt64[99];
            random.XorFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillRangedExactMultipleUInt64()
        {
            var random = new Generator("XorFillRangedExactMultipleUInt64");
            var data = new UInt64[99 * 8];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillInt64ArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new Int64[9999];
            var data2 = new Int64[9999];
            var data3 = new Int64[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Int64)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (Int64)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillInt64ArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new Int64[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillInt64ArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new Int64[9999];
            random.AddFill(3 * (Int64.MaxValue / 4), Int64.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullInt64ArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (Int64[])null);
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

        [TestMethod]
        public void CreateInt64s()
        {
            var random = new Generator("CreateInt64s");
            var data = random.CreateInt64s(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInt64sLowCount()
        {
            var random = new Generator("CreateInt64s");
            var data = random.CreateInt64s(-1);
        }

        [TestMethod]
        public void CreateInt64sZeroCount()
        {
            var random = new Generator("CreateInt64s");
            var data = random.CreateInt64s(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateInt64sWithRange()
        {
            var random = new Generator("CreateInt64s");
            var data = random.CreateInt64s(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInt64sLowCountWithRange()
        {
            var random = new Generator("CreateInt64s");
            var data = random.CreateInt64s(-1, 50);
        }

        [TestMethod]
        public void CreateInt64sWithMinAndRange()
        {
            var random = new Generator("CreateInt64s");
            var data = random.CreateInt64s(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInt64sLowCountWithMinAndRange()
        {
            var random = new Generator("CreateInt64s");
            var data = random.CreateInt64s(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillInt64Array()
        {
            var random = new Generator("AddFillInt64Array");
            var data1 = random.CreateInt64s(99);
            var data2 = (Int64[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateInt64s(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((Int64)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullInt64Array()
        {
            var random = new Generator("AddFillNullInt64Array");
            random.AddFill((Int64[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeInt64()
        {
            var random = new Generator("AddFillWithNonPow2RangeInt64");
            var data = new Int64[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullInt64Array()
        {
            var random = new Generator("AddFillWithRangeAndNullInt64Array");
            random.AddFill(64, (Int64[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndInt64Array()
        {
            var random = new Generator("AddFillWithRangeAndNullInt64Array");
            var data = new Int64[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountInt64Array()
        {
            var random = new Generator("AddFillWithOffsetAndCountInt64Array");
            var data = new Int64[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullInt64Array()
        {
            var random = new Generator("AddFillWithOffsetAndNullInt64Array");
            random.AddFill((Int64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetInt64()
        {
            var random = new Generator("AddFillWithLowOffsetInt64");
            var data = new Int64[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetInt64()
        {
            var random = new Generator("AddFillWithLowOffsetInt64");
            var data = new Int64[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountInt64()
        {
            var random = new Generator("AddFillWithZeroCountInt64");
            var data = Enumerable.Range(0, 99).Select(t => (Int64)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int64)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountInt64()
        {
            var random = new Generator("AddFillWithLowCountInt64");
            var data = new Int64[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountInt64()
        {
            var random = new Generator("AddFillWithHighCountInt64");
            var data = new Int64[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleInt64()
        {
            var random = new Generator("AddFillExactMultipleInt64");
            var data = new Int64[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetInt64Array()
        {
            var random = new Generator("AddFillNonPow2WithOffsetInt64Array");
            var data = new Int64[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullInt64Array()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullInt64Array");
            random.AddFill(64, (Int64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetInt64()
        {
            var random = new Generator("AddFillRangedWithLowOffsetInt64");
            var data = new Int64[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetInt64()
        {
            var random = new Generator("AddFillRangedWithHighOffsetInt64");
            var data = new Int64[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountInt64()
        {
            var random = new Generator("AddFillRangedWithZeroCountInt64");
            var data = Enumerable.Range(0, 99).Select(t => (Int64)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int64)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountInt64()
        {
            var random = new Generator("AddFillRangedWithLowCountInt64");
            var data = new Int64[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeInt64()
        {
            var random = new Generator("AddFillRangedWithLowCountInt64");
            var data = new Int64[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountInt64()
        {
            var random = new Generator("AddFillRangedWithHighCountInt64");
            var data = new Int64[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleInt64()
        {
            var random = new Generator("AddFillRangedExactMultipleInt64");
            var data = new Int64[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void XorFillInt64Array()
        {
            var random = new Generator("XorFillInt64Array");
            var data1 = random.CreateInt64s(99);
            var data2 = (Int64[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateInt64s(99);
            random = new Generator(1);
            random.XorFill(data1);
            LooksRandom(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual(data2[i] ^ data3[i], data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillNullInt64Array()
        {
            var random = new Generator("XorFillNullInt64Array");
            random.XorFill((Int64[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillWithNonPow2RangeInt64()
        {
            var random = new Generator("XorFillWithNonPow2RangeInt64");
            var data = new Int64[99];
            random.XorFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithRangeAndNullInt64Array()
        {
            var random = new Generator("XorFillWithRangeAndNullInt64Array");
            random.XorFill(64, (Int64[])null);
        }

        [TestMethod]
        public void XorFillWithRangeAndInt64Array()
        {
            var random = new Generator("XorFillWithRangeAndNullInt64Array");
            var data = new Int64[9999];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillWithOffsetAndNullInt64Array()
        {
            var random = new Generator("XorFillWithOffsetAndNullInt64Array");
            random.XorFill((Int64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowOffsetInt64()
        {
            var random = new Generator("XorFillWithLowOffsetInt64");
            var data = new Int64[99];
            random.XorFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighOffsetInt64()
        {
            var random = new Generator("XorFillWithLowOffsetInt64");
            var data = new Int64[99];
            random.XorFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillWithZeroCountInt64()
        {
            var random = new Generator("XorFillWithZeroCountInt64");
            var data = Enumerable.Range(0, 99).Select(t => (Int64)t).ToArray();
            random.XorFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int64)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithLowCountInt64()
        {
            var random = new Generator("XorFillWithLowCountInt64");
            var data = new Int64[99];
            random.XorFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillWithHighCountInt64()
        {
            var random = new Generator("XorFillWithHighCountInt64");
            var data = new Int64[99];
            random.XorFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillExactMultipleInt64()
        {
            var random = new Generator("XorFillExactMultipleInt64");
            var data = new Int64[99 * 8];
            random.XorFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFillNonPow2WithOffsetInt64Array()
        {
            var random = new Generator("XorFillNonPow2WithOffsetInt64Array");
            random.XorFill(57, (Int64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XorFillRangedWithOffsetAndNullInt64Array()
        {
            var random = new Generator("XorFillRangedWithOffsetAndNullInt64Array");
            random.XorFill(64, (Int64[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowOffsetInt64()
        {
            var random = new Generator("XorFillRangedWithLowOffsetInt64");
            var data = new Int64[99];
            random.XorFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighOffsetInt64()
        {
            var random = new Generator("XorFillRangedWithHighOffsetInt64");
            var data = new Int64[99];
            random.XorFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void XorFillRangedWithZeroCountInt64()
        {
            var random = new Generator("XorFillRangedWithZeroCountInt64");
            var data = Enumerable.Range(0, 99).Select(t => (Int64)t).ToArray();
            random.XorFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Int64)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithLowCountInt64()
        {
            var random = new Generator("XorFillRangedWithLowCountInt64");
            var data = new Int64[99];
            random.XorFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void XorFillRangedWithHighCountInt64()
        {
            var random = new Generator("XorFillRangedWithHighCountInt64");
            var data = new Int64[99];
            random.XorFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void XorFillRangedExactMultipleInt64()
        {
            var random = new Generator("XorFillRangedExactMultipleInt64");
            var data = new Int64[99 * 8];
            random.XorFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillSingleArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new Single[9999];
            var data2 = new Single[9999];
            var data3 = new Single[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Single)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (Single)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillSingleArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new Single[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillSingleArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new Single[9999];
            random.AddFill(3 * (Single.MaxValue / 4), Single.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullSingleArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (Single[])null);
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

        [TestMethod]
        public void CreateSingles()
        {
            var random = new Generator("CreateSingles");
            var data = random.CreateSingles(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateSinglesLowCount()
        {
            var random = new Generator("CreateSingles");
            var data = random.CreateSingles(-1);
        }

        [TestMethod]
        public void CreateSinglesZeroCount()
        {
            var random = new Generator("CreateSingles");
            var data = random.CreateSingles(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateSinglesWithRange()
        {
            var random = new Generator("CreateSingles");
            var data = random.CreateSingles(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateSinglesLowCountWithRange()
        {
            var random = new Generator("CreateSingles");
            var data = random.CreateSingles(-1, 50);
        }

        [TestMethod]
        public void CreateSinglesWithMinAndRange()
        {
            var random = new Generator("CreateSingles");
            var data = random.CreateSingles(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateSinglesLowCountWithMinAndRange()
        {
            var random = new Generator("CreateSingles");
            var data = random.CreateSingles(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillSingleArray()
        {
            var random = new Generator("AddFillSingleArray");
            var data1 = random.CreateSingles(99);
            var data2 = (Single[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateSingles(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((Single)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullSingleArray()
        {
            var random = new Generator("AddFillNullSingleArray");
            random.AddFill((Single[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeSingle()
        {
            var random = new Generator("AddFillWithNonPow2RangeSingle");
            var data = new Single[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullSingleArray()
        {
            var random = new Generator("AddFillWithRangeAndNullSingleArray");
            random.AddFill(64, (Single[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndSingleArray()
        {
            var random = new Generator("AddFillWithRangeAndNullSingleArray");
            var data = new Single[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountSingleArray()
        {
            var random = new Generator("AddFillWithOffsetAndCountSingleArray");
            var data = new Single[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullSingleArray()
        {
            var random = new Generator("AddFillWithOffsetAndNullSingleArray");
            random.AddFill((Single[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetSingle()
        {
            var random = new Generator("AddFillWithLowOffsetSingle");
            var data = new Single[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetSingle()
        {
            var random = new Generator("AddFillWithLowOffsetSingle");
            var data = new Single[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountSingle()
        {
            var random = new Generator("AddFillWithZeroCountSingle");
            var data = Enumerable.Range(0, 99).Select(t => (Single)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Single)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountSingle()
        {
            var random = new Generator("AddFillWithLowCountSingle");
            var data = new Single[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountSingle()
        {
            var random = new Generator("AddFillWithHighCountSingle");
            var data = new Single[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleSingle()
        {
            var random = new Generator("AddFillExactMultipleSingle");
            var data = new Single[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetSingleArray()
        {
            var random = new Generator("AddFillNonPow2WithOffsetSingleArray");
            var data = new Single[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullSingleArray()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullSingleArray");
            random.AddFill(64, (Single[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetSingle()
        {
            var random = new Generator("AddFillRangedWithLowOffsetSingle");
            var data = new Single[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetSingle()
        {
            var random = new Generator("AddFillRangedWithHighOffsetSingle");
            var data = new Single[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountSingle()
        {
            var random = new Generator("AddFillRangedWithZeroCountSingle");
            var data = Enumerable.Range(0, 99).Select(t => (Single)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Single)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountSingle()
        {
            var random = new Generator("AddFillRangedWithLowCountSingle");
            var data = new Single[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeSingle()
        {
            var random = new Generator("AddFillRangedWithLowCountSingle");
            var data = new Single[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountSingle()
        {
            var random = new Generator("AddFillRangedWithHighCountSingle");
            var data = new Single[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleSingle()
        {
            var random = new Generator("AddFillRangedExactMultipleSingle");
            var data = new Single[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillDoubleArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new Double[9999];
            var data2 = new Double[9999];
            var data3 = new Double[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Double)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (Double)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillDoubleArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new Double[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillDoubleArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new Double[9999];
            random.AddFill(3 * (Double.MaxValue / 4), Double.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullDoubleArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (Double[])null);
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

        [TestMethod]
        public void CreateDoubles()
        {
            var random = new Generator("CreateDoubles");
            var data = random.CreateDoubles(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateDoublesLowCount()
        {
            var random = new Generator("CreateDoubles");
            var data = random.CreateDoubles(-1);
        }

        [TestMethod]
        public void CreateDoublesZeroCount()
        {
            var random = new Generator("CreateDoubles");
            var data = random.CreateDoubles(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateDoublesWithRange()
        {
            var random = new Generator("CreateDoubles");
            var data = random.CreateDoubles(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateDoublesLowCountWithRange()
        {
            var random = new Generator("CreateDoubles");
            var data = random.CreateDoubles(-1, 50);
        }

        [TestMethod]
        public void CreateDoublesWithMinAndRange()
        {
            var random = new Generator("CreateDoubles");
            var data = random.CreateDoubles(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateDoublesLowCountWithMinAndRange()
        {
            var random = new Generator("CreateDoubles");
            var data = random.CreateDoubles(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillDoubleArray()
        {
            var random = new Generator("AddFillDoubleArray");
            var data1 = random.CreateDoubles(99);
            var data2 = (Double[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateDoubles(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((Double)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullDoubleArray()
        {
            var random = new Generator("AddFillNullDoubleArray");
            random.AddFill((Double[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeDouble()
        {
            var random = new Generator("AddFillWithNonPow2RangeDouble");
            var data = new Double[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullDoubleArray()
        {
            var random = new Generator("AddFillWithRangeAndNullDoubleArray");
            random.AddFill(64, (Double[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndDoubleArray()
        {
            var random = new Generator("AddFillWithRangeAndNullDoubleArray");
            var data = new Double[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountDoubleArray()
        {
            var random = new Generator("AddFillWithOffsetAndCountDoubleArray");
            var data = new Double[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullDoubleArray()
        {
            var random = new Generator("AddFillWithOffsetAndNullDoubleArray");
            random.AddFill((Double[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetDouble()
        {
            var random = new Generator("AddFillWithLowOffsetDouble");
            var data = new Double[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetDouble()
        {
            var random = new Generator("AddFillWithLowOffsetDouble");
            var data = new Double[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountDouble()
        {
            var random = new Generator("AddFillWithZeroCountDouble");
            var data = Enumerable.Range(0, 99).Select(t => (Double)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Double)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountDouble()
        {
            var random = new Generator("AddFillWithLowCountDouble");
            var data = new Double[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountDouble()
        {
            var random = new Generator("AddFillWithHighCountDouble");
            var data = new Double[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleDouble()
        {
            var random = new Generator("AddFillExactMultipleDouble");
            var data = new Double[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetDoubleArray()
        {
            var random = new Generator("AddFillNonPow2WithOffsetDoubleArray");
            var data = new Double[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullDoubleArray()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullDoubleArray");
            random.AddFill(64, (Double[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetDouble()
        {
            var random = new Generator("AddFillRangedWithLowOffsetDouble");
            var data = new Double[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetDouble()
        {
            var random = new Generator("AddFillRangedWithHighOffsetDouble");
            var data = new Double[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountDouble()
        {
            var random = new Generator("AddFillRangedWithZeroCountDouble");
            var data = Enumerable.Range(0, 99).Select(t => (Double)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Double)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountDouble()
        {
            var random = new Generator("AddFillRangedWithLowCountDouble");
            var data = new Double[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeDouble()
        {
            var random = new Generator("AddFillRangedWithLowCountDouble");
            var data = new Double[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountDouble()
        {
            var random = new Generator("AddFillRangedWithHighCountDouble");
            var data = new Double[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleDouble()
        {
            var random = new Generator("AddFillRangedExactMultipleDouble");
            var data = new Double[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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
        public void AddFillDecimalArrayWithMinAndRange()
        {
            var random = new Generator();
            var data1 = new Decimal[9999];
            var data2 = new Decimal[9999];
            var data3 = new Decimal[9999];

            for (int p = 0; p < 10 * (8 / sizeof(Decimal)); p++)
            {
                random.Fill(25, 50, data1);
                Array.Copy(data1, data3, data1.Length);
                var g = random.Clone();
                random.AddFill(25, 50, data1);
                g.Fill(25, 50, data2);

                for (int i = 0; i < data1.Length; i++)
                {
                    Assert.AreEqual(data1[i], (Decimal)(data3[i] + data2[i]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillDecimalArrayWithMinAndLowRange()
        {
            var random = new Generator();
            var data = new Decimal[9999];
            random.AddFill(25, 0, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillDecimalArrayWithMinAndHighRange()
        {
            var random = new Generator();
            var data = new Decimal[9999];
            random.AddFill(3 * (Decimal.MaxValue / 4), Decimal.MaxValue / 2, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullDecimalArrayWithMinAndRange()
        {
            var random = new Generator();
            random.AddFill(25, 50, (Decimal[])null);
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

        [TestMethod]
        public void CreateDecimals()
        {
            var random = new Generator("CreateDecimals");
            var data = random.CreateDecimals(99);
            Assert.IsNotNull(data);
            Assert.AreEqual(99, data.Length);
            LooksRandom(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateDecimalsLowCount()
        {
            var random = new Generator("CreateDecimals");
            var data = random.CreateDecimals(-1);
        }

        [TestMethod]
        public void CreateDecimalsZeroCount()
        {
            var random = new Generator("CreateDecimals");
            var data = random.CreateDecimals(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void CreateDecimalsWithRange()
        {
            var random = new Generator("CreateDecimals");
            var data = random.CreateDecimals(9999, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 45);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateDecimalsLowCountWithRange()
        {
            var random = new Generator("CreateDecimals");
            var data = random.CreateDecimals(-1, 50);
        }

        [TestMethod]
        public void CreateDecimalsWithMinAndRange()
        {
            var random = new Generator("CreateDecimals");
            var data = random.CreateDecimals(9999, 25, 50);
            Assert.IsNotNull(data);
            Assert.AreEqual(9999, data.Length);
            Assert.IsTrue(data.Min() < 30);
            Assert.IsTrue(data.Max() >= 70);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateDecimalsLowCountWithMinAndRange()
        {
            var random = new Generator("CreateDecimals");
            var data = random.CreateDecimals(-1, 25, 50);
        }

        [TestMethod]
        public void AddFillDecimalArray()
        {
            var random = new Generator("AddFillDecimalArray");
            var data1 = random.CreateDecimals(99);
            var data2 = (Decimal[])data1.Clone();
            random = new Generator(1);
            var data3 = random.CreateDecimals(99);
            random = new Generator(1);
            random.AddFill(data1);

            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual((Decimal)(data2[i] + data3[i]), data1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillNullDecimalArray()
        {
            var random = new Generator("AddFillNullDecimalArray");
            random.AddFill((Decimal[])null);
        }

        [TestMethod]
        public void AddFillWithNonPow2RangeDecimal()
        {
            var random = new Generator("AddFillWithNonPow2RangeDecimal");
            var data = new Decimal[99];
            random.AddFill(57, data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithRangeAndNullDecimalArray()
        {
            var random = new Generator("AddFillWithRangeAndNullDecimalArray");
            random.AddFill(64, (Decimal[])null);
        }

        [TestMethod]
        public void AddFillWithRangeAndDecimalArray()
        {
            var random = new Generator("AddFillWithRangeAndNullDecimalArray");
            var data = new Decimal[9999];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
        }

        [TestMethod]
        public void AddFillWithOffsetAndCountDecimalArray()
        {
            var random = new Generator("AddFillWithOffsetAndCountDecimalArray");
            var data = new Decimal[9999];

            for (int i = 0; i < 8; i++)
            {
                random.AddFill(data, 0, data.Length - i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillWithOffsetAndNullDecimalArray()
        {
            var random = new Generator("AddFillWithOffsetAndNullDecimalArray");
            random.AddFill((Decimal[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowOffsetDecimal()
        {
            var random = new Generator("AddFillWithLowOffsetDecimal");
            var data = new Decimal[99];
            random.AddFill(data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighOffsetDecimal()
        {
            var random = new Generator("AddFillWithLowOffsetDecimal");
            var data = new Decimal[99];
            random.AddFill(data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillWithZeroCountDecimal()
        {
            var random = new Generator("AddFillWithZeroCountDecimal");
            var data = Enumerable.Range(0, 99).Select(t => (Decimal)t).ToArray();
            random.AddFill(data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Decimal)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithLowCountDecimal()
        {
            var random = new Generator("AddFillWithLowCountDecimal");
            var data = new Decimal[99];
            random.AddFill(data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillWithHighCountDecimal()
        {
            var random = new Generator("AddFillWithHighCountDecimal");
            var data = new Decimal[99];
            random.AddFill(data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillExactMultipleDecimal()
        {
            var random = new Generator("AddFillExactMultipleDecimal");
            var data = new Decimal[99 * 8];
            random.AddFill(data);
            LooksRandom(data);
        }

        [TestMethod]
        public void AddFillNonPow2WithOffsetDecimalArray()
        {
            var random = new Generator("AddFillNonPow2WithOffsetDecimalArray");
            var data = new Decimal[99 * 8];
            random.AddFill(57, data, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddFillRangedWithOffsetAndNullDecimalArray()
        {
            var random = new Generator("AddFillRangedWithOffsetAndNullDecimalArray");
            random.AddFill(64, (Decimal[])null, 0, 99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowOffsetDecimal()
        {
            var random = new Generator("AddFillRangedWithLowOffsetDecimal");
            var data = new Decimal[99];
            random.AddFill(64, data, -1, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighOffsetDecimal()
        {
            var random = new Generator("AddFillRangedWithHighOffsetDecimal");
            var data = new Decimal[99];
            random.AddFill(64, data, data.Length, data.Length);
        }

        [TestMethod]
        public void AddFillRangedWithZeroCountDecimal()
        {
            var random = new Generator("AddFillRangedWithZeroCountDecimal");
            var data = Enumerable.Range(0, 99).Select(t => (Decimal)t).ToArray();
            random.AddFill(64, data, 0, 0);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual((Decimal)i, data[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowCountDecimal()
        {
            var random = new Generator("AddFillRangedWithLowCountDecimal");
            var data = new Decimal[99];
            random.AddFill(64, data, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithLowRangeDecimal()
        {
            var random = new Generator("AddFillRangedWithLowCountDecimal");
            var data = new Decimal[99];
            random.AddFill(64, 0, data, 0, data.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddFillRangedWithHighCountDecimal()
        {
            var random = new Generator("AddFillRangedWithHighCountDecimal");
            var data = new Decimal[99];
            random.AddFill(64, data, 0, data.Length + 1);
        }

        [TestMethod]
        public void AddFillRangedExactMultipleDecimal()
        {
            var random = new Generator("AddFillRangedExactMultipleDecimal");
            var data = new Decimal[99 * 8];
            random.AddFill(64, data);
            Assert.IsTrue(data.Min() < 5);
            Assert.IsTrue(data.Max() >= 59);
            Assert.IsTrue(data.Max() < 64);
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

        [TestMethod]
        public void CreateValueUnionsArray()
        {
            var random = new Generator("CreateValueUnionsArray");
            var data = random.CreateValueUnions(99);
            Assert.AreEqual(99, data.Length);
            var ulongs = data.Select(t => t.UInt64_0).ToArray();
            LooksRandom(ulongs);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateValueUnionsLowCount()
        {
            var random = new Generator("CreateValueUnionsLowCount");
            var data = random.CreateValueUnions(-1);
        }
    }
}
