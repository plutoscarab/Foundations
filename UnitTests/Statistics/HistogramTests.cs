﻿
/*
HistogramTests.cs
*/
using System.Collections.Generic;

namespace Foundations.Statistics
{
    [TestClass]
    public class HistogramTests
    {

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromsbyte()
        {
            var arr = new [] { (sbyte)1, (sbyte)2, (sbyte)2, (sbyte)3, (sbyte)3, (sbyte)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativesbyte()
        {
            var arr = new [] { (sbyte)1, (sbyte)2, (sbyte)2, (sbyte)3, (sbyte)3, (sbyte)(-3) };
            Histogram.From(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNullsbyte()
        {
            Histogram.From((IEnumerable<sbyte>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromsbyteWithBinCount()
        {
            var arr = new [] { (sbyte)1, (sbyte)2, (sbyte)2, (sbyte)3, (sbyte)3, (sbyte)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromsbyteOverBinCount()
        {
            var arr = new [] { (sbyte)1, (sbyte)2, (sbyte)2, (sbyte)3, (sbyte)3, (sbyte)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromsbyteWithFunction()
        {
            var arr = new [] { (sbyte)1, (sbyte)2, (sbyte)2, (sbyte)3, (sbyte)3, (sbyte)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromsbyteWithNullFunction()
        {
            var arr = new [] { (sbyte)1, (sbyte)2, (sbyte)2, (sbyte)3, (sbyte)3, (sbyte)3 };
            Histogram.From(arr, (Func<sbyte, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromsbyteGetMinMax()
        {
            var arr = new [] { (sbyte)1, (sbyte)2, (sbyte)2, (sbyte)3, (sbyte)3, (sbyte)3 };
            sbyte min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((sbyte)1, min);
            Assert.AreEqual((sbyte)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromsbyteGetMinMaxOfNull()
        {
            sbyte min, max;
            var hist = Histogram.From((IList<sbyte>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromsbyteWithMax()
        {
            var arr = new [] { (sbyte)1, (sbyte)2, (sbyte)2, (sbyte)3, (sbyte)3, (sbyte)3 };
            var hist = Histogram.From(arr, 5, (sbyte)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromsbyteWithMaxValue()
        {
            var arr = new [] { (sbyte)1, (sbyte)2, (sbyte)2, (sbyte)3, (sbyte)3, (sbyte)3 };
            var hist = Histogram.From(arr, 5, sbyte.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Frombyte()
        {
            var arr = new [] { (byte)1, (byte)2, (byte)2, (byte)3, (byte)3, (byte)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativebyte()
        {
            var arr = new [] { (byte)1, (byte)2, (byte)2, (byte)3, (byte)3, (byte)3 };
            Histogram.From(arr, _ => Convert.ToInt32(_) - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNullbyte()
        {
            Histogram.From((IEnumerable<byte>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FrombyteWithBinCount()
        {
            var arr = new [] { (byte)1, (byte)2, (byte)2, (byte)3, (byte)3, (byte)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FrombyteOverBinCount()
        {
            var arr = new [] { (byte)1, (byte)2, (byte)2, (byte)3, (byte)3, (byte)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FrombyteWithFunction()
        {
            var arr = new [] { (byte)1, (byte)2, (byte)2, (byte)3, (byte)3, (byte)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FrombyteWithNullFunction()
        {
            var arr = new [] { (byte)1, (byte)2, (byte)2, (byte)3, (byte)3, (byte)3 };
            Histogram.From(arr, (Func<byte, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FrombyteGetMinMax()
        {
            var arr = new [] { (byte)1, (byte)2, (byte)2, (byte)3, (byte)3, (byte)3 };
            byte min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((byte)1, min);
            Assert.AreEqual((byte)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FrombyteGetMinMaxOfNull()
        {
            byte min, max;
            var hist = Histogram.From((IList<byte>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FrombyteWithMax()
        {
            var arr = new [] { (byte)1, (byte)2, (byte)2, (byte)3, (byte)3, (byte)3 };
            var hist = Histogram.From(arr, 5, (byte)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FrombyteWithMaxValue()
        {
            var arr = new [] { (byte)1, (byte)2, (byte)2, (byte)3, (byte)3, (byte)3 };
            var hist = Histogram.From(arr, 5, byte.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromshort()
        {
            var arr = new [] { (short)1, (short)2, (short)2, (short)3, (short)3, (short)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativeshort()
        {
            var arr = new [] { (short)1, (short)2, (short)2, (short)3, (short)3, (short)(-3) };
            Histogram.From(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNullshort()
        {
            Histogram.From((IEnumerable<short>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromshortWithBinCount()
        {
            var arr = new [] { (short)1, (short)2, (short)2, (short)3, (short)3, (short)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromshortOverBinCount()
        {
            var arr = new [] { (short)1, (short)2, (short)2, (short)3, (short)3, (short)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromshortWithFunction()
        {
            var arr = new [] { (short)1, (short)2, (short)2, (short)3, (short)3, (short)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromshortWithNullFunction()
        {
            var arr = new [] { (short)1, (short)2, (short)2, (short)3, (short)3, (short)3 };
            Histogram.From(arr, (Func<short, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromshortGetMinMax()
        {
            var arr = new [] { (short)1, (short)2, (short)2, (short)3, (short)3, (short)3 };
            short min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((short)1, min);
            Assert.AreEqual((short)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromshortGetMinMaxOfNull()
        {
            short min, max;
            var hist = Histogram.From((IList<short>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromshortWithMax()
        {
            var arr = new [] { (short)1, (short)2, (short)2, (short)3, (short)3, (short)3 };
            var hist = Histogram.From(arr, 5, (short)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromshortWithMaxValue()
        {
            var arr = new [] { (short)1, (short)2, (short)2, (short)3, (short)3, (short)3 };
            var hist = Histogram.From(arr, 5, short.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromushort()
        {
            var arr = new [] { (ushort)1, (ushort)2, (ushort)2, (ushort)3, (ushort)3, (ushort)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativeushort()
        {
            var arr = new [] { (ushort)1, (ushort)2, (ushort)2, (ushort)3, (ushort)3, (ushort)3 };
            Histogram.From(arr, _ => Convert.ToInt32(_) - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNullushort()
        {
            Histogram.From((IEnumerable<ushort>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromushortWithBinCount()
        {
            var arr = new [] { (ushort)1, (ushort)2, (ushort)2, (ushort)3, (ushort)3, (ushort)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromushortOverBinCount()
        {
            var arr = new [] { (ushort)1, (ushort)2, (ushort)2, (ushort)3, (ushort)3, (ushort)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromushortWithFunction()
        {
            var arr = new [] { (ushort)1, (ushort)2, (ushort)2, (ushort)3, (ushort)3, (ushort)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromushortWithNullFunction()
        {
            var arr = new [] { (ushort)1, (ushort)2, (ushort)2, (ushort)3, (ushort)3, (ushort)3 };
            Histogram.From(arr, (Func<ushort, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromushortGetMinMax()
        {
            var arr = new [] { (ushort)1, (ushort)2, (ushort)2, (ushort)3, (ushort)3, (ushort)3 };
            ushort min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((ushort)1, min);
            Assert.AreEqual((ushort)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromushortGetMinMaxOfNull()
        {
            ushort min, max;
            var hist = Histogram.From((IList<ushort>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromushortWithMax()
        {
            var arr = new [] { (ushort)1, (ushort)2, (ushort)2, (ushort)3, (ushort)3, (ushort)3 };
            var hist = Histogram.From(arr, 5, (ushort)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromushortWithMaxValue()
        {
            var arr = new [] { (ushort)1, (ushort)2, (ushort)2, (ushort)3, (ushort)3, (ushort)3 };
            var hist = Histogram.From(arr, 5, ushort.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromint()
        {
            var arr = new [] { (int)1, (int)2, (int)2, (int)3, (int)3, (int)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativeint()
        {
            var arr = new [] { (int)1, (int)2, (int)2, (int)3, (int)3, (int)(-3) };
            Histogram.From(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNullint()
        {
            Histogram.From((IEnumerable<int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromintWithBinCount()
        {
            var arr = new [] { (int)1, (int)2, (int)2, (int)3, (int)3, (int)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromintOverBinCount()
        {
            var arr = new [] { (int)1, (int)2, (int)2, (int)3, (int)3, (int)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromintWithFunction()
        {
            var arr = new [] { (int)1, (int)2, (int)2, (int)3, (int)3, (int)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromintWithNullFunction()
        {
            var arr = new [] { (int)1, (int)2, (int)2, (int)3, (int)3, (int)3 };
            Histogram.From(arr, (Func<int, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromintGetMinMax()
        {
            var arr = new [] { (int)1, (int)2, (int)2, (int)3, (int)3, (int)3 };
            int min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((int)1, min);
            Assert.AreEqual((int)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromintGetMinMaxOfNull()
        {
            int min, max;
            var hist = Histogram.From((IList<int>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromintWithMax()
        {
            var arr = new [] { (int)1, (int)2, (int)2, (int)3, (int)3, (int)3 };
            var hist = Histogram.From(arr, 5, (int)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromintWithMaxValue()
        {
            var arr = new [] { (int)1, (int)2, (int)2, (int)3, (int)3, (int)3 };
            var hist = Histogram.From(arr, 5, int.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromuint()
        {
            var arr = new [] { (uint)1, (uint)2, (uint)2, (uint)3, (uint)3, (uint)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativeuint()
        {
            var arr = new [] { (uint)1, (uint)2, (uint)2, (uint)3, (uint)3, (uint)3 };
            Histogram.From(arr, _ => Convert.ToInt32(_) - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNulluint()
        {
            Histogram.From((IEnumerable<uint>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromuintWithBinCount()
        {
            var arr = new [] { (uint)1, (uint)2, (uint)2, (uint)3, (uint)3, (uint)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromuintOverBinCount()
        {
            var arr = new [] { (uint)1, (uint)2, (uint)2, (uint)3, (uint)3, (uint)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromuintWithFunction()
        {
            var arr = new [] { (uint)1, (uint)2, (uint)2, (uint)3, (uint)3, (uint)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromuintWithNullFunction()
        {
            var arr = new [] { (uint)1, (uint)2, (uint)2, (uint)3, (uint)3, (uint)3 };
            Histogram.From(arr, (Func<uint, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromuintGetMinMax()
        {
            var arr = new [] { (uint)1, (uint)2, (uint)2, (uint)3, (uint)3, (uint)3 };
            uint min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((uint)1, min);
            Assert.AreEqual((uint)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromuintGetMinMaxOfNull()
        {
            uint min, max;
            var hist = Histogram.From((IList<uint>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromuintWithMax()
        {
            var arr = new [] { (uint)1, (uint)2, (uint)2, (uint)3, (uint)3, (uint)3 };
            var hist = Histogram.From(arr, 5, (uint)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromuintWithMaxValue()
        {
            var arr = new [] { (uint)1, (uint)2, (uint)2, (uint)3, (uint)3, (uint)3 };
            var hist = Histogram.From(arr, 5, uint.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromlong()
        {
            var arr = new [] { (long)1, (long)2, (long)2, (long)3, (long)3, (long)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativelong()
        {
            var arr = new [] { (long)1, (long)2, (long)2, (long)3, (long)3, (long)(-3) };
            Histogram.From(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNulllong()
        {
            Histogram.From((IEnumerable<long>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromlongWithBinCount()
        {
            var arr = new [] { (long)1, (long)2, (long)2, (long)3, (long)3, (long)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromlongOverBinCount()
        {
            var arr = new [] { (long)1, (long)2, (long)2, (long)3, (long)3, (long)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromlongWithFunction()
        {
            var arr = new [] { (long)1, (long)2, (long)2, (long)3, (long)3, (long)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromlongWithNullFunction()
        {
            var arr = new [] { (long)1, (long)2, (long)2, (long)3, (long)3, (long)3 };
            Histogram.From(arr, (Func<long, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromlongGetMinMax()
        {
            var arr = new [] { (long)1, (long)2, (long)2, (long)3, (long)3, (long)3 };
            long min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((long)1, min);
            Assert.AreEqual((long)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromlongGetMinMaxOfNull()
        {
            long min, max;
            var hist = Histogram.From((IList<long>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromlongWithMax()
        {
            var arr = new [] { (long)1, (long)2, (long)2, (long)3, (long)3, (long)3 };
            var hist = Histogram.From(arr, 5, (long)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromlongWithMaxValue()
        {
            var arr = new [] { (long)1, (long)2, (long)2, (long)3, (long)3, (long)3 };
            var hist = Histogram.From(arr, 5, long.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromulong()
        {
            var arr = new [] { (ulong)1, (ulong)2, (ulong)2, (ulong)3, (ulong)3, (ulong)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativeulong()
        {
            var arr = new [] { (ulong)1, (ulong)2, (ulong)2, (ulong)3, (ulong)3, (ulong)3 };
            Histogram.From(arr, _ => Convert.ToInt32(_) - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNullulong()
        {
            Histogram.From((IEnumerable<ulong>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromulongWithBinCount()
        {
            var arr = new [] { (ulong)1, (ulong)2, (ulong)2, (ulong)3, (ulong)3, (ulong)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromulongOverBinCount()
        {
            var arr = new [] { (ulong)1, (ulong)2, (ulong)2, (ulong)3, (ulong)3, (ulong)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromulongWithFunction()
        {
            var arr = new [] { (ulong)1, (ulong)2, (ulong)2, (ulong)3, (ulong)3, (ulong)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromulongWithNullFunction()
        {
            var arr = new [] { (ulong)1, (ulong)2, (ulong)2, (ulong)3, (ulong)3, (ulong)3 };
            Histogram.From(arr, (Func<ulong, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromulongGetMinMax()
        {
            var arr = new [] { (ulong)1, (ulong)2, (ulong)2, (ulong)3, (ulong)3, (ulong)3 };
            ulong min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((ulong)1, min);
            Assert.AreEqual((ulong)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromulongGetMinMaxOfNull()
        {
            ulong min, max;
            var hist = Histogram.From((IList<ulong>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromulongWithMax()
        {
            var arr = new [] { (ulong)1, (ulong)2, (ulong)2, (ulong)3, (ulong)3, (ulong)3 };
            var hist = Histogram.From(arr, 5, (ulong)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromulongWithMaxValue()
        {
            var arr = new [] { (ulong)1, (ulong)2, (ulong)2, (ulong)3, (ulong)3, (ulong)3 };
            var hist = Histogram.From(arr, 5, ulong.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromfloat()
        {
            var arr = new [] { (float)1, (float)2, (float)2, (float)3, (float)3, (float)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativefloat()
        {
            var arr = new [] { (float)1, (float)2, (float)2, (float)3, (float)3, (float)(-3) };
            Histogram.From(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNullfloat()
        {
            Histogram.From((IEnumerable<float>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromfloatWithBinCount()
        {
            var arr = new [] { (float)1, (float)2, (float)2, (float)3, (float)3, (float)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromfloatOverBinCount()
        {
            var arr = new [] { (float)1, (float)2, (float)2, (float)3, (float)3, (float)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromfloatWithFunction()
        {
            var arr = new [] { (float)1, (float)2, (float)2, (float)3, (float)3, (float)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromfloatWithNullFunction()
        {
            var arr = new [] { (float)1, (float)2, (float)2, (float)3, (float)3, (float)3 };
            Histogram.From(arr, (Func<float, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromfloatGetMinMax()
        {
            var arr = new [] { (float)1, (float)2, (float)2, (float)3, (float)3, (float)3 };
            float min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((float)1, min);
            Assert.AreEqual((float)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromfloatGetMinMaxOfNull()
        {
            float min, max;
            var hist = Histogram.From((IList<float>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromfloatWithMax()
        {
            var arr = new [] { (float)1, (float)2, (float)2, (float)3, (float)3, (float)3 };
            var hist = Histogram.From(arr, 5, (float)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromfloatWithMaxValue()
        {
            var arr = new [] { (float)1, (float)2, (float)2, (float)3, (float)3, (float)3 };
            var hist = Histogram.From(arr, 5, float.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromdouble()
        {
            var arr = new [] { (double)1, (double)2, (double)2, (double)3, (double)3, (double)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativedouble()
        {
            var arr = new [] { (double)1, (double)2, (double)2, (double)3, (double)3, (double)(-3) };
            Histogram.From(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNulldouble()
        {
            Histogram.From((IEnumerable<double>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromdoubleWithBinCount()
        {
            var arr = new [] { (double)1, (double)2, (double)2, (double)3, (double)3, (double)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromdoubleOverBinCount()
        {
            var arr = new [] { (double)1, (double)2, (double)2, (double)3, (double)3, (double)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromdoubleWithFunction()
        {
            var arr = new [] { (double)1, (double)2, (double)2, (double)3, (double)3, (double)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromdoubleWithNullFunction()
        {
            var arr = new [] { (double)1, (double)2, (double)2, (double)3, (double)3, (double)3 };
            Histogram.From(arr, (Func<double, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromdoubleGetMinMax()
        {
            var arr = new [] { (double)1, (double)2, (double)2, (double)3, (double)3, (double)3 };
            double min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((double)1, min);
            Assert.AreEqual((double)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromdoubleGetMinMaxOfNull()
        {
            double min, max;
            var hist = Histogram.From((IList<double>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromdoubleWithMax()
        {
            var arr = new [] { (double)1, (double)2, (double)2, (double)3, (double)3, (double)3 };
            var hist = Histogram.From(arr, 5, (double)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromdoubleWithMaxValue()
        {
            var arr = new [] { (double)1, (double)2, (double)2, (double)3, (double)3, (double)3 };
            var hist = Histogram.From(arr, 5, double.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromdecimal()
        {
            var arr = new [] { (decimal)1, (decimal)2, (decimal)2, (decimal)3, (decimal)3, (decimal)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativedecimal()
        {
            var arr = new [] { (decimal)1, (decimal)2, (decimal)2, (decimal)3, (decimal)3, (decimal)(-3) };
            Histogram.From(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNulldecimal()
        {
            Histogram.From((IEnumerable<decimal>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromdecimalWithBinCount()
        {
            var arr = new [] { (decimal)1, (decimal)2, (decimal)2, (decimal)3, (decimal)3, (decimal)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromdecimalOverBinCount()
        {
            var arr = new [] { (decimal)1, (decimal)2, (decimal)2, (decimal)3, (decimal)3, (decimal)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromdecimalWithFunction()
        {
            var arr = new [] { (decimal)1, (decimal)2, (decimal)2, (decimal)3, (decimal)3, (decimal)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromdecimalWithNullFunction()
        {
            var arr = new [] { (decimal)1, (decimal)2, (decimal)2, (decimal)3, (decimal)3, (decimal)3 };
            Histogram.From(arr, (Func<decimal, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromdecimalGetMinMax()
        {
            var arr = new [] { (decimal)1, (decimal)2, (decimal)2, (decimal)3, (decimal)3, (decimal)3 };
            decimal min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((decimal)1, min);
            Assert.AreEqual((decimal)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromdecimalGetMinMaxOfNull()
        {
            decimal min, max;
            var hist = Histogram.From((IList<decimal>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromdecimalWithMax()
        {
            var arr = new [] { (decimal)1, (decimal)2, (decimal)2, (decimal)3, (decimal)3, (decimal)3 };
            var hist = Histogram.From(arr, 5, (decimal)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromdecimalWithMaxValue()
        {
            var arr = new [] { (decimal)1, (decimal)2, (decimal)2, (decimal)3, (decimal)3, (decimal)3 };
            var hist = Histogram.From(arr, 5, decimal.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void Fromchar()
        {
            var arr = new [] { (char)1, (char)2, (char)2, (char)3, (char)3, (char)3 };
            var hist = Histogram.From(arr);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromNegativechar()
        {
            var arr = new [] { (char)1, (char)2, (char)2, (char)3, (char)3, (char)3 };
            Histogram.From(arr, _ => Convert.ToInt32(_) - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromNullchar()
        {
            Histogram.From((IEnumerable<char>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromcharWithBinCount()
        {
            var arr = new [] { (char)1, (char)2, (char)2, (char)3, (char)3, (char)3 };
            var hist = Histogram.From(arr, 5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
	    public void FromcharOverBinCount()
        {
            var arr = new [] { (char)1, (char)2, (char)2, (char)3, (char)3, (char)3 };
            Histogram.From(arr, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromcharWithFunction()
        {
            var arr = new [] { (char)1, (char)2, (char)2, (char)3, (char)3, (char)3 };
            var hist = Histogram.From(arr, _ => 3);
            Assert.AreEqual(4, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(0, hist[2]);
            Assert.AreEqual(6, hist[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromcharWithNullFunction()
        {
            var arr = new [] { (char)1, (char)2, (char)2, (char)3, (char)3, (char)3 };
            Histogram.From(arr, (Func<char, int>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromcharGetMinMax()
        {
            var arr = new [] { (char)1, (char)2, (char)2, (char)3, (char)3, (char)3 };
            char min, max;
            var hist = Histogram.From(arr, 5, out min, out max);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual((char)1, min);
            Assert.AreEqual((char)3, max);
            Assert.AreEqual(1, hist[0]);
            Assert.AreEqual(0, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(0, hist[3]);
            Assert.AreEqual(3, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
	    public void FromcharGetMinMaxOfNull()
        {
            char min, max;
            var hist = Histogram.From((IList<char>)null, 5, out min, out max);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
	    public void FromcharWithMax()
        {
            var arr = new [] { (char)1, (char)2, (char)2, (char)3, (char)3, (char)3 };
            var hist = Histogram.From(arr, 5, (char)5);
            Assert.AreEqual(5, hist.Length);
            Assert.AreEqual(0, hist[0]);
            Assert.AreEqual(1, hist[1]);
            Assert.AreEqual(2, hist[2]);
            Assert.AreEqual(3, hist[3]);
            Assert.AreEqual(0, hist[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
	    public void FromcharWithMaxValue()
        {
            var arr = new [] { (char)1, (char)2, (char)2, (char)3, (char)3, (char)3 };
            var hist = Histogram.From(arr, 5, char.MaxValue);
        }
    }
}
