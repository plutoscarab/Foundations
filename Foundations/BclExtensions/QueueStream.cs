
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

namespace Foundations.BclExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class QueueStream : Stream
    {
        long capacity;
        ConcurrentQueue<byte[]> queue;
        long currentSize;
        long length;
        long position;
        byte[] partialItem;
        MultiAwaitable readAwaitable = new MultiAwaitable();
        MultiAwaitable writeAwaitable = new MultiAwaitable();

        /// <summary>
        /// 
        /// </summary>
        public QueueStream(long capacity)
        {
            Contract.Requires(capacity > 0, nameof(capacity));
            this.capacity = capacity;
            queue = new ConcurrentQueue<byte[]>();
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// 
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return ReadAsync(buffer, offset, count).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            WriteAsync(buffer, offset, count).Wait();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
