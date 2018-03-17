
/*
WriteOnceStream.cs

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
    /// A write-only, non-seekable <see cref="System.IO.Stream"/>.
    /// This is an abstract class intended to make it easier to
    /// implement streams of this type by exposing only the
    /// methods required for implementation. Reading- and seeking-
    /// related methods are already implemented appropriately.
    /// </summary>
    public abstract class WriteOnceStream : Stream
    {
        private class WriteOnceToStream : WriteOnceStream
        {
            Stream stream;

            public WriteOnceToStream(Stream stream)
            {
                this.stream = stream;
            }

            protected override void DoFlush()
            {
                stream.Flush();
            }

            protected override long GetPosition()
            {
                return stream.Position;
            }

            protected override void WriteBytes(byte[] buffer, int offset, int count)
            {
                stream.Write(buffer, offset, count);
            }
        }

        /// <summary>
        /// Gets a <see cref="WriteOnceStream"/> wrapper around a <see cref="System.IO.Stream"/>.
        /// </summary>
        public static WriteOnceStream ToStream(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException();
            if (!stream.CanWrite) throw new ArgumentException("Stream is not writeable.");
            return new WriteOnceToStream(stream);
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// Always returns false.
        /// </summary>
        public override bool CanRead
        {
            get { return false; }
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
        /// Always returns true.
        /// </summary>
        public override bool CanWrite
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the length in bytes of the stream.
        /// Not supported.
        /// </summary>
        public override long Length
        {
            get { throw new NotSupportedException(); }
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
        /// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        protected abstract void DoFlush();

        /// <summary>
        /// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
            DoFlush();
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
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
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        protected abstract void WriteBytes(byte[] buffer, int offset, int count);

        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            WriteBytes(buffer, offset, count);
        }
    }
}
