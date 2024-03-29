﻿
/*
ByteCodingTests.cs
*/

using System.Collections.Generic;
using System.IO;
using Foundations.RandomNumbers;

namespace Foundations.Coding
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ByteCodingTests
    {
        [TestMethod]
        public void Base128ByteTest()
        {
            EncodingByteTest(Codes.Base128);
        }

        [TestMethod]
        public void Base128SByteTest()
        {
            EncodingSByteTest(Codes.Base128);
        }

        [TestMethod]
        public void Base128CharTest()
        {
            EncodingCharTest(Codes.Base128);
        }

        [TestMethod]
        public void Base128Int16Test()
        {
            EncodingInt16Test(Codes.Base128);
        }

        [TestMethod]
        public void Base128UInt16Test()
        {
            EncodingUInt16Test(Codes.Base128);
        }

        [TestMethod]
        public void Base128Int32Test()
        {
            EncodingInt32Test(Codes.Base128);
        }

        [TestMethod]
        public void Base128UInt32Test()
        {
            EncodingUInt32Test(Codes.Base128);
        }

        [TestMethod]
        public void Base128Int64Test()
        {
            EncodingInt64Test(Codes.Base128);
        }

        [TestMethod]
        public void Base128UInt64Test()
        {
            EncodingUInt64Test(Codes.Base128);
        }

        [TestMethod]
        public void Base128DateTimeTest()
        {
            EncodingDateTimeTest(Codes.Base128);
        }

        [TestMethod]
        public void Base128BooleanTest()
        {
            EncodingBooleanTest(Codes.Base128);
        }

        private void EncodingByteTest(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<Byte>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = (Byte)(g.Byte() >> g.Int32(8));
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadByte(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadByte(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingSByteTest(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<SByte>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = (SByte)(g.SByte() >> g.Int32(8));
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadSByte(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadSByte(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingCharTest(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<Char>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = (char)g.UInt16();
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadChar(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadChar(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingInt16Test(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<Int16>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = (Int16)(g.Int16() >> g.Int32(16));
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadInt16(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadInt16(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingUInt16Test(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<UInt16>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = (UInt16)(g.UInt16() >> g.Int32(16));
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadUInt16(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadUInt16(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingInt32Test(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<Int32>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = (Int32)(g.Int32() >> g.Int32(32));
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadInt32(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadInt32(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingUInt32Test(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<UInt32>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = (UInt32)(g.UInt32() >> g.Int32(32));
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadUInt32(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadUInt32(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingInt64Test(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<Int64>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = (Int64)(g.Int64() >> g.Int32(64));
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadInt64(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadInt64(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingUInt64Test(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<UInt64>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = (UInt64)(g.UInt64() >> g.Int32(64));
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadUInt64(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadUInt64(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingDateTimeTest(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<DateTime>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = new DateTime(g.Int64(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks - DateTime.MinValue.Ticks + 1));
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadDateTime(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadDateTime(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

        private void EncodingBooleanTest(IByteEncoding encoding)
        {
            var stream = new MemoryStream();
            var values = new List<Boolean>();
            var g = new Generator(encoding.GetType().Name);

            for (int i = 0; i < 1000; i++)
            {
                var v = g.Boolean();
                values.Add(v);
                encoding.Write(stream, v);
            }

            stream.Position = 0;

            for (int i = 0; i < values.Count; i++)
            {
                var v = encoding.ReadBoolean(stream);
                Assert.AreEqual(values[i], v);
            }

            try
            {
                using (var r = new RandomStream(g))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        encoding.ReadBoolean(r);
                    }
                }

                Assert.Fail("Expected InvalidDataException");
            }
            catch(Exception e)
            {
                Assert.AreEqual("InvalidDataException", e.GetType().Name);
            }
        }

    }
}
