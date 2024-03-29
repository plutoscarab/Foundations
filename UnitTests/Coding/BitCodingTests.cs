﻿
/*
BitCodingTests.cs
*/

using System.Collections.Generic;
using Foundations.RandomNumbers;

namespace Foundations.Coding
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class BitCodingTests
    {
        [TestMethod]
        public void UnaryZerosTest()
        {
            EncodingTest(Codes.UnaryZeros);
        }

        [TestMethod]
        public void UnaryOnesTest()
        {
            EncodingTest(Codes.UnaryOnes);
        }

        [TestMethod]
        public void EliasGammaTest()
        {
            EncodingTest(Codes.EliasGamma);
        }

        [TestMethod]
        public void EliasDeltaTest()
        {
            EncodingTest(Codes.EliasDelta);
        }

        [TestMethod]
        public void EliasOmegaTest()
        {
            EncodingTest(Codes.EliasOmega);
        }

        [TestMethod]
        public void FibonacciTest()
        {
            EncodingTest(Codes.Fibonacci);
        }

        [TestMethod]
        public void LevenshteinTest()
        {
            EncodingTest(Codes.Levenshtein);
        }

        [TestMethod]
        public void EliasFibonacciTest()
        {
            EncodingTest(Codes.EliasFibonacci);
        }

        [TestMethod]
        public void TruncatedBinaryTest()
        {
            EncodingTest(Codes.TruncatedBinary(6000));
        }

        [TestMethod]
        public void GolombTest()
        {
            EncodingTest(Codes.Golomb(141));
        }

        [TestMethod]
        public void RiceTest()
        {
            EncodingTest(Codes.Rice(8));
        }

        [TestMethod]
        public void BaseNTest()
        {
            EncodingTest(Codes.BaseN(85));
        }

        [TestMethod]
        public void PowerOf2BaseTest()
        {
            EncodingTest(Codes.PowerOf2Base(5));
        }

        [TestMethod]
        public void FixedWidthTest()
        {
            EncodingTest(Codes.FixedWidth(27));
        }

        private void EncodingTest(IBitEncoding encoding)
        {
            var writer = new BitWriter(1000);
            var values = new List<int>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                int v = g.Int32() >> g.Int32(32);
                if (v < encoding.MinEncodable || v > encoding.MaxEncodable) continue;
                values.Add(v);

                if (i < 500)
                    writer.Write(encoding.GetCode(v));
                else
                    encoding.Write(writer, v);
            }

            var buffer = writer.ToArray();
            var reader = new BitReader(buffer);

            for (int i = 0; i < values.Count; i++)
            {
                int v = encoding.Read(reader);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                encoding.GetCode(encoding.MinEncodable - 1);
                Assert.Fail("Expected ArgumentOutOfRangeException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("ArgumentOutOfRangeException", e.GetType().Name);
            }
        }
    }
}
