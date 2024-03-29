﻿
/*
MathExtensions.cs
*/

using Foundations.RandomNumbers;

namespace Foundations
{
    /// <summary/>
    [TestClass]
    public sealed class MathExtensionsTests
    {
        /// <summary/>
        [TestMethod]
        public void MathAbsSByteTest()
        {
            var g = new Generator("MathAbsSByteTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.SByte();
                var expected = System.Math.Abs(value);
                var actual = value.Abs();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAbsInt16Test()
        {
            var g = new Generator("MathAbsInt16Test");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Int16();
                var expected = System.Math.Abs(value);
                var actual = value.Abs();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAbsInt32Test()
        {
            var g = new Generator("MathAbsInt32Test");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Int32();
                var expected = System.Math.Abs(value);
                var actual = value.Abs();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAbsInt64Test()
        {
            var g = new Generator("MathAbsInt64Test");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Int64();
                var expected = System.Math.Abs(value);
                var actual = value.Abs();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAbsDecimalTest()
        {
            var g = new Generator("MathAbsDecimalTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Decimal();
                var expected = System.Math.Abs(value);
                var actual = value.Abs();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAbsSingleTest()
        {
            var g = new Generator("MathAbsSingleTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Single();
                var expected = System.Math.Abs(value);
                var actual = value.Abs();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAbsDoubleTest()
        {
            var g = new Generator("MathAbsDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Double();
                var expected = System.Math.Abs(value);
                var actual = value.Abs();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAcosDoubleTest()
        {
            var g = new Generator("MathAcosDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Acos(d);
                var actual = d.Acos();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAsinDoubleTest()
        {
            var g = new Generator("MathAsinDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Asin(d);
                var actual = d.Asin();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAtanDoubleTest()
        {
            var g = new Generator("MathAtanDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Atan(d);
                var actual = d.Atan();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathAtan2DoubleDoubleTest()
        {
            var g = new Generator("MathAtan2DoubleDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var y = g.Double();
                var x = g.Double();
                var expected = System.Math.Atan2(y, x);
                var actual = y.Atan2(x);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathBigMulInt32Int32Test()
        {
            var g = new Generator("MathBigMulInt32Int32Test");

            for (int test = 0; test < 100; test++)
            {
                var a = g.Int32();
                var b = g.Int32();
                var expected = System.Math.BigMul(a, b);
                var actual = a.BigMul(b);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathCeilingExtDecimalTest()
        {
            var g = new Generator("MathCeilingExtDecimalTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Decimal();
                var expected = System.Math.Ceiling(d);
                var actual = d.CeilingExt();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathCeilingDoubleTest()
        {
            var g = new Generator("MathCeilingDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var a = g.Double();
                var expected = System.Math.Ceiling(a);
                var actual = a.Ceiling();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathCosDoubleTest()
        {
            var g = new Generator("MathCosDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Cos(d);
                var actual = d.Cos();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathCoshDoubleTest()
        {
            var g = new Generator("MathCoshDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Double();
                var expected = System.Math.Cosh(value);
                var actual = value.Cosh();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathDivRemInt32Int32Int32Test()
        {
            var g = new Generator("MathDivRemInt32Int32Int32Test");

            for (int test = 0; test < 100; test++)
            {
                var a = g.Int32();
                var b = g.Int32();
                System.Int32 result, actual_result;
                var expected = System.Math.DivRem(a, b, out result);
                var actual = a.DivRem(b, out actual_result);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathDivRemInt64Int64Int64Test()
        {
            var g = new Generator("MathDivRemInt64Int64Int64Test");

            for (int test = 0; test < 100; test++)
            {
                var a = g.Int64();
                var b = g.Int64();
                System.Int64 result, actual_result;
                var expected = System.Math.DivRem(a, b, out result);
                var actual = a.DivRem(b, out actual_result);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathExpDoubleTest()
        {
            var g = new Generator("MathExpDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Exp(d);
                var actual = d.Exp();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathFloorExtDecimalTest()
        {
            var g = new Generator("MathFloorExtDecimalTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Decimal();
                var expected = System.Math.Floor(d);
                var actual = d.FloorExt();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathFloorDoubleTest()
        {
            var g = new Generator("MathFloorDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Floor(d);
                var actual = d.Floor();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathIEEERemainderDoubleDoubleTest()
        {
            var g = new Generator("MathIEEERemainderDoubleDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var x = g.Double();
                var y = g.Double();
                var expected = System.Math.IEEERemainder(x, y);
                var actual = x.IEEERemainder(y);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathLogDoubleDoubleTest()
        {
            var g = new Generator("MathLogDoubleDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var a = g.Double();
                var newBase = g.Double();
                var expected = System.Math.Log(a, newBase);
                var actual = a.Log(newBase);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathLogDoubleTest()
        {
            var g = new Generator("MathLogDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Log(d);
                var actual = d.Log();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathLog10DoubleTest()
        {
            var g = new Generator("MathLog10DoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Log10(d);
                var actual = d.Log10();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxSByteSByteTest()
        {
            var g = new Generator("MathMaxSByteSByteTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.SByte();
                var val2 = g.SByte();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxByteByteTest()
        {
            var g = new Generator("MathMaxByteByteTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Byte();
                var val2 = g.Byte();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxInt16Int16Test()
        {
            var g = new Generator("MathMaxInt16Int16Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Int16();
                var val2 = g.Int16();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxUInt16UInt16Test()
        {
            var g = new Generator("MathMaxUInt16UInt16Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.UInt16();
                var val2 = g.UInt16();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxInt32Int32Test()
        {
            var g = new Generator("MathMaxInt32Int32Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Int32();
                var val2 = g.Int32();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxUInt32UInt32Test()
        {
            var g = new Generator("MathMaxUInt32UInt32Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.UInt32();
                var val2 = g.UInt32();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxInt64Int64Test()
        {
            var g = new Generator("MathMaxInt64Int64Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Int64();
                var val2 = g.Int64();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxUInt64UInt64Test()
        {
            var g = new Generator("MathMaxUInt64UInt64Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.UInt64();
                var val2 = g.UInt64();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxSingleSingleTest()
        {
            var g = new Generator("MathMaxSingleSingleTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Single();
                var val2 = g.Single();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxDoubleDoubleTest()
        {
            var g = new Generator("MathMaxDoubleDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Double();
                var val2 = g.Double();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMaxDecimalDecimalTest()
        {
            var g = new Generator("MathMaxDecimalDecimalTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Decimal();
                var val2 = g.Decimal();
                var expected = System.Math.Max(val1, val2);
                var actual = val1.Max(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinSByteSByteTest()
        {
            var g = new Generator("MathMinSByteSByteTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.SByte();
                var val2 = g.SByte();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinByteByteTest()
        {
            var g = new Generator("MathMinByteByteTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Byte();
                var val2 = g.Byte();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinInt16Int16Test()
        {
            var g = new Generator("MathMinInt16Int16Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Int16();
                var val2 = g.Int16();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinUInt16UInt16Test()
        {
            var g = new Generator("MathMinUInt16UInt16Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.UInt16();
                var val2 = g.UInt16();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinInt32Int32Test()
        {
            var g = new Generator("MathMinInt32Int32Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Int32();
                var val2 = g.Int32();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinUInt32UInt32Test()
        {
            var g = new Generator("MathMinUInt32UInt32Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.UInt32();
                var val2 = g.UInt32();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinInt64Int64Test()
        {
            var g = new Generator("MathMinInt64Int64Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Int64();
                var val2 = g.Int64();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinUInt64UInt64Test()
        {
            var g = new Generator("MathMinUInt64UInt64Test");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.UInt64();
                var val2 = g.UInt64();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinSingleSingleTest()
        {
            var g = new Generator("MathMinSingleSingleTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Single();
                var val2 = g.Single();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinDoubleDoubleTest()
        {
            var g = new Generator("MathMinDoubleDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Double();
                var val2 = g.Double();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathMinDecimalDecimalTest()
        {
            var g = new Generator("MathMinDecimalDecimalTest");

            for (int test = 0; test < 100; test++)
            {
                var val1 = g.Decimal();
                var val2 = g.Decimal();
                var expected = System.Math.Min(val1, val2);
                var actual = val1.Min(val2);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathPowDoubleDoubleTest()
        {
            var g = new Generator("MathPowDoubleDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var x = g.Double();
                var y = g.Double();
                var expected = System.Math.Pow(x, y);
                var actual = x.Pow(y);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathRoundDoubleInt32Test()
        {
            var g = new Generator("MathRoundDoubleInt32Test");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Double();
                var digits = g.Int32(16);
                var expected = System.Math.Round(value, digits);
                var actual = value.Round(digits);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathRoundDoubleMidpointRoundingTest()
        {
            var g = new Generator("MathRoundDoubleMidpointRoundingTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Double();
                var mode = new [] {
                    System.MidpointRounding.ToEven,
                    System.MidpointRounding.AwayFromZero,
                }[g.Int32(2)];
                var expected = System.Math.Round(value, mode);
                var actual = value.Round(mode);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathRoundDoubleInt32MidpointRoundingTest()
        {
            var g = new Generator("MathRoundDoubleInt32MidpointRoundingTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Double();
                var digits = g.Int32(16);
                var mode = new [] {
                    System.MidpointRounding.ToEven,
                    System.MidpointRounding.AwayFromZero,
                }[g.Int32(2)];
                var expected = System.Math.Round(value, digits, mode);
                var actual = value.Round(digits, mode);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathRoundExtDecimalTest()
        {
            var g = new Generator("MathRoundExtDecimalTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Decimal();
                var expected = System.Math.Round(d);
                var actual = d.RoundExt();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathRoundExtDecimalInt32Test()
        {
            var g = new Generator("MathRoundExtDecimalInt32Test");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Decimal();
                var decimals = g.Int32(29);
                var expected = System.Math.Round(d, decimals);
                var actual = d.RoundExt(decimals);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathRoundExtDecimalMidpointRoundingTest()
        {
            var g = new Generator("MathRoundExtDecimalMidpointRoundingTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Decimal();
                var mode = new [] {
                    System.MidpointRounding.ToEven,
                    System.MidpointRounding.AwayFromZero,
                }[g.Int32(2)];
                var expected = System.Math.Round(d, mode);
                var actual = d.RoundExt(mode);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathRoundExtDecimalInt32MidpointRoundingTest()
        {
            var g = new Generator("MathRoundExtDecimalInt32MidpointRoundingTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Decimal();
                var decimals = g.Int32(29);
                var mode = new [] {
                    System.MidpointRounding.ToEven,
                    System.MidpointRounding.AwayFromZero,
                }[g.Int32(2)];
                var expected = System.Math.Round(d, decimals, mode);
                var actual = d.RoundExt(decimals, mode);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathRoundDoubleTest()
        {
            var g = new Generator("MathRoundDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var a = g.Double();
                var expected = System.Math.Round(a);
                var actual = a.Round();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSignSByteTest()
        {
            var g = new Generator("MathSignSByteTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.SByte();
                var expected = System.Math.Sign(value);
                var actual = value.Sign();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSignInt16Test()
        {
            var g = new Generator("MathSignInt16Test");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Int16();
                var expected = System.Math.Sign(value);
                var actual = value.Sign();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSignInt32Test()
        {
            var g = new Generator("MathSignInt32Test");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Int32();
                var expected = System.Math.Sign(value);
                var actual = value.Sign();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSignInt64Test()
        {
            var g = new Generator("MathSignInt64Test");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Int64();
                var expected = System.Math.Sign(value);
                var actual = value.Sign();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSignSingleTest()
        {
            var g = new Generator("MathSignSingleTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Single();
                var expected = System.Math.Sign(value);
                var actual = value.Sign();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSignDoubleTest()
        {
            var g = new Generator("MathSignDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Double();
                var expected = System.Math.Sign(value);
                var actual = value.Sign();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSignDecimalTest()
        {
            var g = new Generator("MathSignDecimalTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Decimal();
                var expected = System.Math.Sign(value);
                var actual = value.Sign();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSinDoubleTest()
        {
            var g = new Generator("MathSinDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var a = g.Double();
                var expected = System.Math.Sin(a);
                var actual = a.Sin();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSinhDoubleTest()
        {
            var g = new Generator("MathSinhDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Double();
                var expected = System.Math.Sinh(value);
                var actual = value.Sinh();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathSqrtDoubleTest()
        {
            var g = new Generator("MathSqrtDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Sqrt(d);
                var actual = d.Sqrt();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathTanDoubleTest()
        {
            var g = new Generator("MathTanDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var a = g.Double();
                var expected = System.Math.Tan(a);
                var actual = a.Tan();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathTanhDoubleTest()
        {
            var g = new Generator("MathTanhDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var value = g.Double();
                var expected = System.Math.Tanh(value);
                var actual = value.Tanh();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathTruncateExtDecimalTest()
        {
            var g = new Generator("MathTruncateExtDecimalTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Decimal();
                var expected = System.Math.Truncate(d);
                var actual = d.TruncateExt();
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary/>
        [TestMethod]
        public void MathTruncateDoubleTest()
        {
            var g = new Generator("MathTruncateDoubleTest");

            for (int test = 0; test < 100; test++)
            {
                var d = g.Double();
                var expected = System.Math.Truncate(d);
                var actual = d.Truncate();
                Assert.AreEqual(expected, actual);
            }
        }

    }
}