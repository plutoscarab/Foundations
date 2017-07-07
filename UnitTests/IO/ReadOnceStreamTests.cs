
/*
ReadOnceStreamTests.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.IO
{
    [TestClass()]
    public class ReadOnceStreamTests
    {
        [TestMethod()]
        public void FromStreamTest()
        {
            var stream = ReadOnceStream.FromStream(new MemoryStream());
            Assert.IsTrue(stream.CanRead);
            Assert.IsFalse(stream.CanWrite);
            Assert.IsFalse(stream.CanSeek);
            Assert.AreEqual(0L, stream.Position);
            Assert.AreEqual(0L, stream.Length);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromStreamNullTest()
        {
            var stream = ReadOnceStream.FromStream(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FromStreamWriteOnlyTest()
        {
            try
            {
                using (var file = File.Open("test.txt", FileMode.Create, FileAccess.Write))
                {
                    var stream = ReadOnceStream.FromStream(file);
                }
            }
            finally
            {
                File.Delete("test.txt");
            }
        }

        [TestMethod()]
        public void FlushTest()
        {
            var stream = ReadOnceStream.FromStream(new MemoryStream());
            stream.Flush();
        }

        [TestMethod()]
        public void ReadTest()
        {
            var data = new byte[] { 1, 2, 3, 4, 5 };
            var buffer = new byte[data.Length];
            var stream = ReadOnceStream.FromStream(new MemoryStream(data));
            int n = stream.Read(buffer, 0, buffer.Length);
            Assert.AreEqual(data.Length, n);
            for (int i = 0; i < n; i++) Assert.AreEqual(data[i], buffer[i]);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void SeekTest()
        {
            var stream = ReadOnceStream.FromStream(new MemoryStream());
            stream.Seek(0, SeekOrigin.Begin);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void PositionTest()
        {
            var stream = ReadOnceStream.FromStream(new MemoryStream());
            stream.Position = 0;
        }

        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void SetLengthTest()
        {
            var stream = ReadOnceStream.FromStream(new MemoryStream());
            stream.SetLength(0);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void WriteTest()
        {
            var stream = ReadOnceStream.FromStream(new MemoryStream());
            var data = new byte[] { 1, 2, 3, 4, 5 };
            stream.Write(data, 0, data.Length);
        }
    }
}