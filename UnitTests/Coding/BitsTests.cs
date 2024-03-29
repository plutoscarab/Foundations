﻿
/*
BitsTests.cs
*/

using System.Collections.Generic;
using Foundations.Coding;
using Foundations.RandomNumbers;

namespace Foundations
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class BitsTests
    {
        /// <summary />
        [TestMethod]
	    public void CountByteTest()
        {
            var g = new Generator("CountByteTest");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Byte();
                var m = n;
                var k = 0;

                for (int j = 0; j < 8; j++)
                {
                    if ((m & 1) != 0) k++;
                    m >>= 1;
                }

                Assert.AreEqual(k, Bits.Count(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void ParityByteTest()
        {
            var g = new Generator("ParityByteTest");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Byte();
                var k = Bits.Count(n) & 1;
                Assert.AreEqual(k, Bits.Parity(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2ByteTest()
        {
            var g = new Generator("FloorLog2ByteTest");

            for (int i = 0; i < 200; i++)
            {
                var n = g.Byte();
                n >>= g.Int32(8);
                if (n <= 0) continue;
                var m = n;
                var k = -1;

                do
                {
                    m >>= 1;
                    k++;
                }
                while (m != 0 && m != unchecked((Byte)(-1)));

                Assert.AreEqual(k, Bits.FloorLog2(n));
            }
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FloorLog2ByteZeroTest()
        {
            Bits.FloorLog2((Byte)0);
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2ByteExceptionTest()
        {
            var g = new Generator("FloorLog2ByteExceptionTest");

            for (int i = 0; i < 20; i++)
            {
                var n = g.Byte();
                if (n > 0) continue;

                try
                {
                    Bits.FloorLog2(n);
                    Assert.Fail();
                }
                catch
                {
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2ByteTest()
        {
            var g = new Generator("IsPowerOf2ByteTest");
            var powers = new HashSet<Byte>();

            for (int i = 0; i < 8; i++)
            {
                var n = (Byte)((Byte)1 << i);

                if (n > 0)
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                    powers.Add(n);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var n = g.Byte();
                
                if (powers.Contains(n))
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                }
                else if (n > 0)
                {
                    Assert.IsFalse(Bits.IsPowerOf2(n));
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2ByteZeroTest()
        {
            Assert.IsFalse(Bits.IsPowerOf2((Byte)0));
        }

        /// <summary />
        [TestMethod]
        public void ChooseByteTest()
        {
            var g = new Generator("ChooseByteTest");

            for (int i = 0; i < 100; i++)
            {
                var a = (ulong)g.Byte();
                var b = (ulong)g.Byte();
                var s = (ulong)g.Byte();
                var c = (ulong)Bits.Choose((Byte)a, (Byte)b, (Byte)s);
                ulong m = 0;

                for (int j = 0; j < 8; j++)
                {
                    var mask = 1ul << j;

                    if ((s & mask) == 0)
                        m |= (a & mask);
                    else
                        m |= (b & mask);
                }

                Assert.AreEqual((Byte)c, (Byte)m);
            }
        }

        /// <summary />
        [TestMethod]
        public void CountOfLeadingZerosByteTest()
        {
            var g = new Generator("CountOfLeadingZerosByteTest");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Byte();
                n >>= g.Int32(8);
                var c = Bits.CountOfLeadingZeros(n);
                int k = 0;

                for (int j = 0; j < 8; j++)
                {
                    if ((n & ((Byte)1 << (7 - j))) != 0)
                        break;

                    k++;
                }

                Assert.AreEqual(k, c);
            }
        }

        /// <summary />
        [TestMethod]
	    public void ReverseByteTest()
        {
            var g = new Generator("ReverseByteTest");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Byte();
                var m = n;
                Byte r = 0;

                for (int j = 0; j < 8; j++)
                {
                    r <<= 1;
                    if ((m & 1) != 0) r |= 1;
                    m >>= 1;
                }

                Assert.AreEqual(r, Bits.Reverse(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void CountSByteTest()
        {
            var g = new Generator("CountSByteTest");

            for (int i = 0; i < 100; i++)
            {
                var n = g.SByte();
                var m = n;
                var k = 0;

                for (int j = 0; j < 8; j++)
                {
                    if ((m & 1) != 0) k++;
                    m >>= 1;
                }

                Assert.AreEqual(k, Bits.Count(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void ParitySByteTest()
        {
            var g = new Generator("ParitySByteTest");

            for (int i = 0; i < 100; i++)
            {
                var n = g.SByte();
                var k = Bits.Count(n) & 1;
                Assert.AreEqual(k, Bits.Parity(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2SByteTest()
        {
            var g = new Generator("FloorLog2SByteTest");

            for (int i = 0; i < 200; i++)
            {
                var n = g.SByte();
                n >>= g.Int32(8);
                if (n <= 0) continue;
                var m = n;
                var k = -1;

                do
                {
                    m >>= 1;
                    k++;
                }
                while (m != 0 && m != unchecked((SByte)(-1)));

                Assert.AreEqual(k, Bits.FloorLog2(n));
            }
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FloorLog2SByteZeroTest()
        {
            Bits.FloorLog2((SByte)0);
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2SByteExceptionTest()
        {
            var g = new Generator("FloorLog2SByteExceptionTest");

            for (int i = 0; i < 20; i++)
            {
                var n = g.SByte();
                if (n > 0) continue;

                try
                {
                    Bits.FloorLog2(n);
                    Assert.Fail();
                }
                catch
                {
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2SByteTest()
        {
            var g = new Generator("IsPowerOf2SByteTest");
            var powers = new HashSet<SByte>();

            for (int i = 0; i < 8; i++)
            {
                var n = (SByte)((SByte)1 << i);

                if (n > 0)
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                    powers.Add(n);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var n = g.SByte();
                
                if (powers.Contains(n))
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                }
                else if (n > 0)
                {
                    Assert.IsFalse(Bits.IsPowerOf2(n));
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2SByteZeroTest()
        {
            Assert.IsFalse(Bits.IsPowerOf2((SByte)0));
        }

        /// <summary />
        [TestMethod]
        public void ChooseSByteTest()
        {
            var g = new Generator("ChooseSByteTest");

            for (int i = 0; i < 100; i++)
            {
                var a = (ulong)g.SByte();
                var b = (ulong)g.SByte();
                var s = (ulong)g.SByte();
                var c = (ulong)Bits.Choose((SByte)a, (SByte)b, (SByte)s);
                ulong m = 0;

                for (int j = 0; j < 8; j++)
                {
                    var mask = 1ul << j;

                    if ((s & mask) == 0)
                        m |= (a & mask);
                    else
                        m |= (b & mask);
                }

                Assert.AreEqual((SByte)c, (SByte)m);
            }
        }

        /// <summary />
        [TestMethod]
        public void CountOfLeadingZerosSByteTest()
        {
            var g = new Generator("CountOfLeadingZerosSByteTest");

            for (int i = 0; i < 100; i++)
            {
                var n = g.SByte();
                n >>= g.Int32(8);
                var c = Bits.CountOfLeadingZeros(n);
                int k = 0;

                for (int j = 0; j < 8; j++)
                {
                    if ((n & ((SByte)1 << (7 - j))) != 0)
                        break;

                    k++;
                }

                Assert.AreEqual(k, c);
            }
        }

        /// <summary />
        [TestMethod]
	    public void CountUInt16Test()
        {
            var g = new Generator("CountUInt16Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt16();
                var m = n;
                var k = 0;

                for (int j = 0; j < 16; j++)
                {
                    if ((m & 1) != 0) k++;
                    m >>= 1;
                }

                Assert.AreEqual(k, Bits.Count(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void ParityUInt16Test()
        {
            var g = new Generator("ParityUInt16Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt16();
                var k = Bits.Count(n) & 1;
                Assert.AreEqual(k, Bits.Parity(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2UInt16Test()
        {
            var g = new Generator("FloorLog2UInt16Test");

            for (int i = 0; i < 200; i++)
            {
                var n = g.UInt16();
                n >>= g.Int32(16);
                if (n <= 0) continue;
                var m = n;
                var k = -1;

                do
                {
                    m >>= 1;
                    k++;
                }
                while (m != 0 && m != unchecked((UInt16)(-1)));

                Assert.AreEqual(k, Bits.FloorLog2(n));
            }
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FloorLog2UInt16ZeroTest()
        {
            Bits.FloorLog2((UInt16)0);
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2UInt16ExceptionTest()
        {
            var g = new Generator("FloorLog2UInt16ExceptionTest");

            for (int i = 0; i < 20; i++)
            {
                var n = g.UInt16();
                if (n > 0) continue;

                try
                {
                    Bits.FloorLog2(n);
                    Assert.Fail();
                }
                catch
                {
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2UInt16Test()
        {
            var g = new Generator("IsPowerOf2UInt16Test");
            var powers = new HashSet<UInt16>();

            for (int i = 0; i < 16; i++)
            {
                var n = (UInt16)((UInt16)1 << i);

                if (n > 0)
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                    powers.Add(n);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt16();
                
                if (powers.Contains(n))
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                }
                else if (n > 0)
                {
                    Assert.IsFalse(Bits.IsPowerOf2(n));
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2UInt16ZeroTest()
        {
            Assert.IsFalse(Bits.IsPowerOf2((UInt16)0));
        }

        /// <summary />
        [TestMethod]
        public void ChooseUInt16Test()
        {
            var g = new Generator("ChooseUInt16Test");

            for (int i = 0; i < 100; i++)
            {
                var a = (ulong)g.UInt16();
                var b = (ulong)g.UInt16();
                var s = (ulong)g.UInt16();
                var c = (ulong)Bits.Choose((UInt16)a, (UInt16)b, (UInt16)s);
                ulong m = 0;

                for (int j = 0; j < 16; j++)
                {
                    var mask = 1ul << j;

                    if ((s & mask) == 0)
                        m |= (a & mask);
                    else
                        m |= (b & mask);
                }

                Assert.AreEqual((UInt16)c, (UInt16)m);
            }
        }

        /// <summary />
        [TestMethod]
        public void CountOfLeadingZerosUInt16Test()
        {
            var g = new Generator("CountOfLeadingZerosUInt16Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt16();
                n >>= g.Int32(16);
                var c = Bits.CountOfLeadingZeros(n);
                int k = 0;

                for (int j = 0; j < 16; j++)
                {
                    if ((n & ((UInt16)1 << (15 - j))) != 0)
                        break;

                    k++;
                }

                Assert.AreEqual(k, c);
            }
        }

        /// <summary />
        [TestMethod]
	    public void ReverseUInt16Test()
        {
            var g = new Generator("ReverseUInt16Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt16();
                var m = n;
                UInt16 r = 0;

                for (int j = 0; j < 16; j++)
                {
                    r <<= 1;
                    if ((m & 1) != 0) r |= 1;
                    m >>= 1;
                }

                Assert.AreEqual(r, Bits.Reverse(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void CountInt16Test()
        {
            var g = new Generator("CountInt16Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int16();
                var m = n;
                var k = 0;

                for (int j = 0; j < 16; j++)
                {
                    if ((m & 1) != 0) k++;
                    m >>= 1;
                }

                Assert.AreEqual(k, Bits.Count(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void ParityInt16Test()
        {
            var g = new Generator("ParityInt16Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int16();
                var k = Bits.Count(n) & 1;
                Assert.AreEqual(k, Bits.Parity(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2Int16Test()
        {
            var g = new Generator("FloorLog2Int16Test");

            for (int i = 0; i < 200; i++)
            {
                var n = g.Int16();
                n >>= g.Int32(16);
                if (n <= 0) continue;
                var m = n;
                var k = -1;

                do
                {
                    m >>= 1;
                    k++;
                }
                while (m != 0 && m != unchecked((Int16)(-1)));

                Assert.AreEqual(k, Bits.FloorLog2(n));
            }
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FloorLog2Int16ZeroTest()
        {
            Bits.FloorLog2((Int16)0);
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2Int16ExceptionTest()
        {
            var g = new Generator("FloorLog2Int16ExceptionTest");

            for (int i = 0; i < 20; i++)
            {
                var n = g.Int16();
                if (n > 0) continue;

                try
                {
                    Bits.FloorLog2(n);
                    Assert.Fail();
                }
                catch
                {
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2Int16Test()
        {
            var g = new Generator("IsPowerOf2Int16Test");
            var powers = new HashSet<Int16>();

            for (int i = 0; i < 16; i++)
            {
                var n = (Int16)((Int16)1 << i);

                if (n > 0)
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                    powers.Add(n);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int16();
                
                if (powers.Contains(n))
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                }
                else if (n > 0)
                {
                    Assert.IsFalse(Bits.IsPowerOf2(n));
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2Int16ZeroTest()
        {
            Assert.IsFalse(Bits.IsPowerOf2((Int16)0));
        }

        /// <summary />
        [TestMethod]
        public void ChooseInt16Test()
        {
            var g = new Generator("ChooseInt16Test");

            for (int i = 0; i < 100; i++)
            {
                var a = (ulong)g.Int16();
                var b = (ulong)g.Int16();
                var s = (ulong)g.Int16();
                var c = (ulong)Bits.Choose((Int16)a, (Int16)b, (Int16)s);
                ulong m = 0;

                for (int j = 0; j < 16; j++)
                {
                    var mask = 1ul << j;

                    if ((s & mask) == 0)
                        m |= (a & mask);
                    else
                        m |= (b & mask);
                }

                Assert.AreEqual((Int16)c, (Int16)m);
            }
        }

        /// <summary />
        [TestMethod]
        public void CountOfLeadingZerosInt16Test()
        {
            var g = new Generator("CountOfLeadingZerosInt16Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int16();
                n >>= g.Int32(16);
                var c = Bits.CountOfLeadingZeros(n);
                int k = 0;

                for (int j = 0; j < 16; j++)
                {
                    if ((n & ((Int16)1 << (15 - j))) != 0)
                        break;

                    k++;
                }

                Assert.AreEqual(k, c);
            }
        }

        /// <summary />
        [TestMethod]
	    public void CountUInt32Test()
        {
            var g = new Generator("CountUInt32Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt32();
                var m = n;
                var k = 0;

                for (int j = 0; j < 32; j++)
                {
                    if ((m & 1) != 0) k++;
                    m >>= 1;
                }

                Assert.AreEqual(k, Bits.Count(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void ParityUInt32Test()
        {
            var g = new Generator("ParityUInt32Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt32();
                var k = Bits.Count(n) & 1;
                Assert.AreEqual(k, Bits.Parity(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2UInt32Test()
        {
            var g = new Generator("FloorLog2UInt32Test");

            for (int i = 0; i < 200; i++)
            {
                var n = g.UInt32();
                n >>= g.Int32(32);
                if (n <= 0) continue;
                var m = n;
                var k = -1;

                do
                {
                    m >>= 1;
                    k++;
                }
                while (m != 0 && m != unchecked((UInt32)(-1)));

                Assert.AreEqual(k, Bits.FloorLog2(n));
            }
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FloorLog2UInt32ZeroTest()
        {
            Bits.FloorLog2((UInt32)0);
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2UInt32ExceptionTest()
        {
            var g = new Generator("FloorLog2UInt32ExceptionTest");

            for (int i = 0; i < 20; i++)
            {
                var n = g.UInt32();
                if (n > 0) continue;

                try
                {
                    Bits.FloorLog2(n);
                    Assert.Fail();
                }
                catch
                {
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2UInt32Test()
        {
            var g = new Generator("IsPowerOf2UInt32Test");
            var powers = new HashSet<UInt32>();

            for (int i = 0; i < 32; i++)
            {
                var n = (UInt32)((UInt32)1 << i);

                if (n > 0)
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                    powers.Add(n);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt32();
                
                if (powers.Contains(n))
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                }
                else if (n > 0)
                {
                    Assert.IsFalse(Bits.IsPowerOf2(n));
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2UInt32ZeroTest()
        {
            Assert.IsFalse(Bits.IsPowerOf2((UInt32)0));
        }

        /// <summary />
        [TestMethod]
        public void ChooseUInt32Test()
        {
            var g = new Generator("ChooseUInt32Test");

            for (int i = 0; i < 100; i++)
            {
                var a = (ulong)g.UInt32();
                var b = (ulong)g.UInt32();
                var s = (ulong)g.UInt32();
                var c = (ulong)Bits.Choose((UInt32)a, (UInt32)b, (UInt32)s);
                ulong m = 0;

                for (int j = 0; j < 32; j++)
                {
                    var mask = 1ul << j;

                    if ((s & mask) == 0)
                        m |= (a & mask);
                    else
                        m |= (b & mask);
                }

                Assert.AreEqual((UInt32)c, (UInt32)m);
            }
        }

        /// <summary />
        [TestMethod]
        public void CountOfLeadingZerosUInt32Test()
        {
            var g = new Generator("CountOfLeadingZerosUInt32Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt32();
                n >>= g.Int32(32);
                var c = Bits.CountOfLeadingZeros(n);
                int k = 0;

                for (int j = 0; j < 32; j++)
                {
                    if ((n & ((UInt32)1 << (31 - j))) != 0)
                        break;

                    k++;
                }

                Assert.AreEqual(k, c);
            }
        }

        /// <summary />
        [TestMethod]
	    public void ReverseUInt32Test()
        {
            var g = new Generator("ReverseUInt32Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt32();
                var m = n;
                UInt32 r = 0;

                for (int j = 0; j < 32; j++)
                {
                    r <<= 1;
                    if ((m & 1) != 0) r |= 1;
                    m >>= 1;
                }

                Assert.AreEqual(r, Bits.Reverse(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void RotateRightUInt32Test()
        {
            var g = new Generator("RotateRightUInt32Test");

            for (int i = 0; i < 100; i++)
            {
                for (int s = 1; s < 32; s++)
                {
                    var n = g.UInt32();
                    var m = n;

                    for (int j = 0; j < 32; j++)
                    {
                        m = Bits.RotateRight(m, s);
                    }

                    Assert.AreEqual(n, m);
                }
            }
        }

        /// <summary />
        [TestMethod]
	    public void RotateLeftUInt32Test()
        {
            var g = new Generator("RotateLeftUInt32Test");

            for (int i = 0; i < 100; i++)
            {
                for (int s = 1; s < 32; s++)
                {
                    var n = g.UInt32();
                    var m = n;

                    for (int j = 0; j < 32; j++)
                    {
                        m = Bits.RotateLeft(m, s);
                    }

                    Assert.AreEqual(n, m);
                }
            }
        }

        /// <summary />
        [TestMethod]
	    public void CountInt32Test()
        {
            var g = new Generator("CountInt32Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int32();
                var m = n;
                var k = 0;

                for (int j = 0; j < 32; j++)
                {
                    if ((m & 1) != 0) k++;
                    m >>= 1;
                }

                Assert.AreEqual(k, Bits.Count(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void ParityInt32Test()
        {
            var g = new Generator("ParityInt32Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int32();
                var k = Bits.Count(n) & 1;
                Assert.AreEqual(k, Bits.Parity(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2Int32Test()
        {
            var g = new Generator("FloorLog2Int32Test");

            for (int i = 0; i < 200; i++)
            {
                var n = g.Int32();
                n >>= g.Int32(32);
                if (n <= 0) continue;
                var m = n;
                var k = -1;

                do
                {
                    m >>= 1;
                    k++;
                }
                while (m != 0 && m != unchecked((Int32)(-1)));

                Assert.AreEqual(k, Bits.FloorLog2(n));
            }
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FloorLog2Int32ZeroTest()
        {
            Bits.FloorLog2((Int32)0);
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2Int32ExceptionTest()
        {
            var g = new Generator("FloorLog2Int32ExceptionTest");

            for (int i = 0; i < 20; i++)
            {
                var n = g.Int32();
                if (n > 0) continue;

                try
                {
                    Bits.FloorLog2(n);
                    Assert.Fail();
                }
                catch
                {
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2Int32Test()
        {
            var g = new Generator("IsPowerOf2Int32Test");
            var powers = new HashSet<Int32>();

            for (int i = 0; i < 32; i++)
            {
                var n = (Int32)((Int32)1 << i);

                if (n > 0)
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                    powers.Add(n);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int32();
                
                if (powers.Contains(n))
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                }
                else if (n > 0)
                {
                    Assert.IsFalse(Bits.IsPowerOf2(n));
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2Int32ZeroTest()
        {
            Assert.IsFalse(Bits.IsPowerOf2((Int32)0));
        }

        /// <summary />
        [TestMethod]
        public void ChooseInt32Test()
        {
            var g = new Generator("ChooseInt32Test");

            for (int i = 0; i < 100; i++)
            {
                var a = (ulong)g.Int32();
                var b = (ulong)g.Int32();
                var s = (ulong)g.Int32();
                var c = (ulong)Bits.Choose((Int32)a, (Int32)b, (Int32)s);
                ulong m = 0;

                for (int j = 0; j < 32; j++)
                {
                    var mask = 1ul << j;

                    if ((s & mask) == 0)
                        m |= (a & mask);
                    else
                        m |= (b & mask);
                }

                Assert.AreEqual((Int32)c, (Int32)m);
            }
        }

        /// <summary />
        [TestMethod]
        public void CountOfLeadingZerosInt32Test()
        {
            var g = new Generator("CountOfLeadingZerosInt32Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int32();
                n >>= g.Int32(32);
                var c = Bits.CountOfLeadingZeros(n);
                int k = 0;

                for (int j = 0; j < 32; j++)
                {
                    if ((n & ((Int32)1 << (31 - j))) != 0)
                        break;

                    k++;
                }

                Assert.AreEqual(k, c);
            }
        }

        /// <summary />
        [TestMethod]
	    public void CountUInt64Test()
        {
            var g = new Generator("CountUInt64Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt64();
                var m = n;
                var k = 0;

                for (int j = 0; j < 64; j++)
                {
                    if ((m & 1) != 0) k++;
                    m >>= 1;
                }

                Assert.AreEqual(k, Bits.Count(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void ParityUInt64Test()
        {
            var g = new Generator("ParityUInt64Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt64();
                var k = Bits.Count(n) & 1;
                Assert.AreEqual(k, Bits.Parity(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2UInt64Test()
        {
            var g = new Generator("FloorLog2UInt64Test");

            for (int i = 0; i < 200; i++)
            {
                var n = g.UInt64();
                n >>= g.Int32(64);
                if (n <= 0) continue;
                var m = n;
                var k = -1;

                do
                {
                    m >>= 1;
                    k++;
                }
                while (m != 0 && m != unchecked((UInt64)(-1)));

                Assert.AreEqual(k, Bits.FloorLog2(n));
            }
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FloorLog2UInt64ZeroTest()
        {
            Bits.FloorLog2((UInt64)0);
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2UInt64ExceptionTest()
        {
            var g = new Generator("FloorLog2UInt64ExceptionTest");

            for (int i = 0; i < 20; i++)
            {
                var n = g.UInt64();
                if (n > 0) continue;

                try
                {
                    Bits.FloorLog2(n);
                    Assert.Fail();
                }
                catch
                {
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2UInt64Test()
        {
            var g = new Generator("IsPowerOf2UInt64Test");
            var powers = new HashSet<UInt64>();

            for (int i = 0; i < 64; i++)
            {
                var n = (UInt64)((UInt64)1 << i);

                if (n > 0)
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                    powers.Add(n);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt64();
                
                if (powers.Contains(n))
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                }
                else if (n > 0)
                {
                    Assert.IsFalse(Bits.IsPowerOf2(n));
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2UInt64ZeroTest()
        {
            Assert.IsFalse(Bits.IsPowerOf2((UInt64)0));
        }

        /// <summary />
        [TestMethod]
        public void ChooseUInt64Test()
        {
            var g = new Generator("ChooseUInt64Test");

            for (int i = 0; i < 100; i++)
            {
                var a = (ulong)g.UInt64();
                var b = (ulong)g.UInt64();
                var s = (ulong)g.UInt64();
                var c = (ulong)Bits.Choose((UInt64)a, (UInt64)b, (UInt64)s);
                ulong m = 0;

                for (int j = 0; j < 64; j++)
                {
                    var mask = 1ul << j;

                    if ((s & mask) == 0)
                        m |= (a & mask);
                    else
                        m |= (b & mask);
                }

                Assert.AreEqual((UInt64)c, (UInt64)m);
            }
        }

        /// <summary />
        [TestMethod]
        public void CountOfLeadingZerosUInt64Test()
        {
            var g = new Generator("CountOfLeadingZerosUInt64Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt64();
                n >>= g.Int32(64);
                var c = Bits.CountOfLeadingZeros(n);
                int k = 0;

                for (int j = 0; j < 64; j++)
                {
                    if ((n & ((UInt64)1 << (63 - j))) != 0)
                        break;

                    k++;
                }

                Assert.AreEqual(k, c);
            }
        }

        /// <summary />
        [TestMethod]
	    public void ReverseUInt64Test()
        {
            var g = new Generator("ReverseUInt64Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.UInt64();
                var m = n;
                UInt64 r = 0;

                for (int j = 0; j < 64; j++)
                {
                    r <<= 1;
                    if ((m & 1) != 0) r |= 1;
                    m >>= 1;
                }

                Assert.AreEqual(r, Bits.Reverse(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void RotateRightUInt64Test()
        {
            var g = new Generator("RotateRightUInt64Test");

            for (int i = 0; i < 100; i++)
            {
                for (int s = 1; s < 64; s++)
                {
                    var n = g.UInt64();
                    var m = n;

                    for (int j = 0; j < 64; j++)
                    {
                        m = Bits.RotateRight(m, s);
                    }

                    Assert.AreEqual(n, m);
                }
            }
        }

        /// <summary />
        [TestMethod]
	    public void RotateLeftUInt64Test()
        {
            var g = new Generator("RotateLeftUInt64Test");

            for (int i = 0; i < 100; i++)
            {
                for (int s = 1; s < 64; s++)
                {
                    var n = g.UInt64();
                    var m = n;

                    for (int j = 0; j < 64; j++)
                    {
                        m = Bits.RotateLeft(m, s);
                    }

                    Assert.AreEqual(n, m);
                }
            }
        }

        /// <summary />
        [TestMethod]
	    public void CountInt64Test()
        {
            var g = new Generator("CountInt64Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int64();
                var m = n;
                var k = 0;

                for (int j = 0; j < 64; j++)
                {
                    if ((m & 1) != 0) k++;
                    m >>= 1;
                }

                Assert.AreEqual(k, Bits.Count(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void ParityInt64Test()
        {
            var g = new Generator("ParityInt64Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int64();
                var k = Bits.Count(n) & 1;
                Assert.AreEqual(k, Bits.Parity(n));
            }
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2Int64Test()
        {
            var g = new Generator("FloorLog2Int64Test");

            for (int i = 0; i < 200; i++)
            {
                var n = g.Int64();
                n >>= g.Int32(64);
                if (n <= 0) continue;
                var m = n;
                var k = -1;

                do
                {
                    m >>= 1;
                    k++;
                }
                while (m != 0 && m != unchecked((Int64)(-1)));

                Assert.AreEqual(k, Bits.FloorLog2(n));
            }
        }

        /// <summary />
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FloorLog2Int64ZeroTest()
        {
            Bits.FloorLog2((Int64)0);
        }

        /// <summary />
        [TestMethod]
	    public void FloorLog2Int64ExceptionTest()
        {
            var g = new Generator("FloorLog2Int64ExceptionTest");

            for (int i = 0; i < 20; i++)
            {
                var n = g.Int64();
                if (n > 0) continue;

                try
                {
                    Bits.FloorLog2(n);
                    Assert.Fail();
                }
                catch
                {
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2Int64Test()
        {
            var g = new Generator("IsPowerOf2Int64Test");
            var powers = new HashSet<Int64>();

            for (int i = 0; i < 64; i++)
            {
                var n = (Int64)((Int64)1 << i);

                if (n > 0)
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                    powers.Add(n);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int64();
                
                if (powers.Contains(n))
                {
                    Assert.IsTrue(Bits.IsPowerOf2(n));
                }
                else if (n > 0)
                {
                    Assert.IsFalse(Bits.IsPowerOf2(n));
                }
            }
        }

        /// <summary />
        [TestMethod]
        public void IsPowerOf2Int64ZeroTest()
        {
            Assert.IsFalse(Bits.IsPowerOf2((Int64)0));
        }

        /// <summary />
        [TestMethod]
        public void ChooseInt64Test()
        {
            var g = new Generator("ChooseInt64Test");

            for (int i = 0; i < 100; i++)
            {
                var a = (ulong)g.Int64();
                var b = (ulong)g.Int64();
                var s = (ulong)g.Int64();
                var c = (ulong)Bits.Choose((Int64)a, (Int64)b, (Int64)s);
                ulong m = 0;

                for (int j = 0; j < 64; j++)
                {
                    var mask = 1ul << j;

                    if ((s & mask) == 0)
                        m |= (a & mask);
                    else
                        m |= (b & mask);
                }

                Assert.AreEqual((Int64)c, (Int64)m);
            }
        }

        /// <summary />
        [TestMethod]
        public void CountOfLeadingZerosInt64Test()
        {
            var g = new Generator("CountOfLeadingZerosInt64Test");

            for (int i = 0; i < 100; i++)
            {
                var n = g.Int64();
                n >>= g.Int32(64);
                var c = Bits.CountOfLeadingZeros(n);
                int k = 0;

                for (int j = 0; j < 64; j++)
                {
                    if ((n & ((Int64)1 << (63 - j))) != 0)
                        break;

                    k++;
                }

                Assert.AreEqual(k, c);
            }
        }

    }
}
