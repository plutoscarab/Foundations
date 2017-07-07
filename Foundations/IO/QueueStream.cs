
/*
QueueStream.cs

Copyright © 2017 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Foundations.Async;

namespace Foundations.IO
{
    /// <summary>
    /// A <see cref="Stream"/> that supports writing on one thread and reading
    /// on a separate thread. The bytes written by the writer are held in 
    /// memory, up to the specified capacity, until read by the reader.
    /// </summary>
    public sealed class QueueStream : Stream
    {
        long capacity;
        ConcurrentQueue<byte[]> queue;
        long currentSize;
        long length;
        long position;
        byte[] partialItem;
        ManualResetEventAsync readAwaitable = new ManualResetEventAsync();
        ManualResetEventAsync writeAwaitable = new ManualResetEventAsync();

        /// <summary>
        /// <see cref="QueueStream"/> constructor.
        /// </summary>
        /// <param name="capacity">The count of bytes that can be held in the stream after writing and before reading.</param>
        public QueueStream(long capacity)
        {
            Contract.Requires(capacity > 0, nameof(capacity));
            this.capacity = capacity;
            queue = new ConcurrentQueue<byte[]>();
        }

        /// <summary>
        /// Returns true, indicating that the stream can be read.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Returns false, indicating that seek operations are not supported.
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        /// Returns true, indicating that the stream can be written to.
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// Gets the number of bytes that have been written to the stream.
        /// </summary>
        public override long Length => Interlocked.Read(ref length);

        /// <summary>
        /// Gets the number of bytes that have been read from the stream.
        /// </summary>
        public override long Position
        {
            get
            {
                return Interlocked.Read(ref position);
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public override void Flush()
        {
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
        /// Reads a sequence of bytes from the current stream and advances the 
        /// position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(offset >= 0);
            Contract.Requires(offset <= buffer.Length);
            Contract.Requires(count >= 0);
            Contract.Requires(count <= capacity);
            Contract.Requires(count <= buffer.Length - offset);

            try
            {
                if (count > 0)
                {
                    byte[] item = partialItem;
                    partialItem = null;

                    if (item == null)
                    {
                        writeAwaitable.Reset();

                        while (!queue.TryDequeue(out item))
                        {
                            await writeAwaitable;
                            writeAwaitable.Reset();
                        }
                    }

                    if (item.Length <= count)
                    {
                        Buffer.BlockCopy(item, 0, buffer, offset, item.Length);
                        Interlocked.Add(ref position, item.Length);
                        Interlocked.Add(ref currentSize, -item.Length);
                        return item.Length;
                    }
                    else
                    {
                        Buffer.BlockCopy(item, 0, buffer, offset, count);
                        partialItem = new byte[item.Length - count];
                        Buffer.BlockCopy(item, count, partialItem, 0, partialItem.Length);
                        Interlocked.Add(ref position, count);
                        Interlocked.Add(ref currentSize, -count);
                        return count;
                    }
                }
                else
                {
                    return 0;
                }
            }
            finally
            {
                readAwaitable.Set();
            }
        }

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
            return ReadAsync(buffer, offset, count).Result;
        }

        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        /// <param name="cancellationToken"></param>
        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(offset >= 0);
            Contract.Requires(offset <= buffer.Length);
            Contract.Requires(count >= 0);
            Contract.Requires(count <= capacity);
            Contract.Requires(count <= buffer.Length - offset);

            readAwaitable.Reset();

            while (Interlocked.Read(ref currentSize) + count > capacity)
            {
                await readAwaitable;
                readAwaitable.Reset();
            }

            if (count > 0)
            {
                var copy = new byte[count];
                Buffer.BlockCopy(buffer, offset, copy, 0, count);
                queue.Enqueue(copy);
                Interlocked.Add(ref length, count);
                Interlocked.Add(ref currentSize, count);
                writeAwaitable.Set();
            }
        }

        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            WriteAsync(buffer, offset, count).Wait();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
