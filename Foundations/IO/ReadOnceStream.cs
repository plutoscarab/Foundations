
/*
ReadOnceStream.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.IO;

namespace Foundations.IO
{
    /// <summary>
    /// A read-only, non-seekable <see cref="System.IO.Stream"/>.
    /// This is an abstract class intended to make it easier to
    /// implement streams of this type by exposing only the
    /// methods required for implementation. Writing- and seeking-
    /// related methods are already implemented appropriately.
    /// </summary>
    public abstract class ReadOnceStream : Stream
    {
        private class ReadOnceFromStream : ReadOnceStream
        {
            Stream stream;

            public ReadOnceFromStream(Stream stream)
            {
                this.stream = stream;
            }

            protected override long GetLength()
            {
                return stream.Length;
            }

            protected override long GetPosition()
            {
                return stream.Position;
            }

            protected override int ReadBytes(byte[] buffer, int offset, int count)
            {
                return stream.Read(buffer, offset, count);
            }
        }

        /// <summary>
        /// Gets a <see cref="ReadOnceStream"/> wrapper around a <see cref="System.IO.Stream"/>.
        /// </summary>
        public static ReadOnceStream FromStream(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException();
            if (!stream.CanRead) throw new ArgumentException("Stream is not readable.");
            return new ReadOnceFromStream(stream);
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// Always returns true.
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports seeking.
        /// Always returns false.
        /// </summary>
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports writing.
        /// Always returns false.
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the length in bytes of the stream.
        /// </summary>
        protected abstract long GetLength();

        /// <summary>
        /// Gets the length in bytes of the stream.
        /// </summary>
        public override long Length
        {
            get { return GetLength(); }
        }

        /// <summary>
        /// Gets the position within the current stream.
        /// </summary>
        protected abstract long GetPosition();

        /// <summary>
        /// Gets the position within the current stream.
        /// </summary>
        public override long Position
        {
            get { return GetPosition(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the 
        /// position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        protected abstract int ReadBytes(byte[] buffer, int offset, int count);

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the 
        /// position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return ReadBytes(buffer, offset, count);
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
