
/*
BitWriter.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundations.Coding
{
    /// <summary>
    /// Writes groups of bits to a byte array.
    /// </summary>
    public sealed partial class BitWriter
    {
        static byte[] mask = Enumerable.Range(0, 9).Select(i => (byte)(0xFF >> (8 - i))).ToArray();

        byte[] buffer;
        int position;
        int shift;

        /// <summary>
        /// Create a <see cref="BitWriter"/>.
        /// </summary>
        public BitWriter()
            : this(1024)
        {
        }

        /// <summary>
        /// Create a <see cref="BitWriter"/>.
        /// </summary>
        public BitWriter(int initialCapacityInBytes)
        {
            buffer = new byte[initialCapacityInBytes];
        }

        /// <summary>
        /// Write a <see cref="Code"/>. The code bits are packed into bytes
        /// in big-endian order, i.e. the first bit is written to the MSB of
        /// the first byte.
        /// </summary>
        public void Write(Code code)
        {
            Write(code.Bits, code.Length);
        }

        /// <summary>
        /// Write a <see cref="System.Int32"/> using the specified <see cref="IBitEncoding"/>.
        /// </summary>
        public void Write(IBitEncoding encoding, int value)
        {
            Write(encoding.GetCode(value));
        }

        /// <summary>
        /// Write a set of bits. The bits are packed into bytes
        /// in big-endian order, i.e. the first bit is written to the MSB of
        /// the first byte.
        /// </summary>
        /// <param name="bits">The bits. The LSB is the last bit written. Extra bits in MSBs are ignored.</param>
        /// <param name="count">The number of bits to write.</param>
        public void Write(ulong bits, int count)
        {
            if (count < 0 || count > 64)
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count > 0)
            {
                int toWrite = Math.Min(count, 8 - shift);

                if (position == buffer.Length)
                {
                    Array.Resize(ref buffer, buffer.Length + (buffer.Length >> 3));
                }

                buffer[position] |= (byte)(((bits >> (count - toWrite)) & mask[toWrite]) << (8 - shift - toWrite));
                shift += toWrite;

                if (shift == 8)
                {
                    position++;
                    shift = 0;
                }

                count -= toWrite;
            }
        }

        /// <summary>
        /// Copy the bits to a <see cref="Stream"/>. Final byte is padded with 0 bits if necessary.
        /// </summary>
        /// <returns>Returns the number of bits copied.</returns>
        public long CopyTo(Stream stream)
        {
            stream.Write(buffer, 0, position + (shift + 7) / 8);
            return Length;
        }

        /// <summary>
        /// Copy the bits to a <see cref="Stream"/> asynchronously. Final byte is padded with 0 bits if necessary.
        /// </summary>
        /// <returns>Returns the number of bits copied.</returns>
        public Task<long> CopyToAsync(Stream stream)
        {
            var source = new TaskCompletionSource<long>();

            stream.BeginWrite(buffer, 0, position + (shift + 7) / 8, iar => 
            {
                try
                {
                    stream.EndWrite(iar);
                    source.SetResult(Length);
                }
                catch(Exception e)
                {
                    source.SetException(e);
                }
            }, null);

            return source.Task;
        }

        /// <summary>
        /// Copy the bits to an array. Final byte is padded with 0 bits if necessary.
        /// </summary>
        /// <returns>Returns the number of bits copied.</returns>
        public long CopyTo(byte[] array)
        {
            int length = position + (shift + 7) / 8;
            Array.Copy(buffer, 0, array, 0, length);
            return Length;
        }

        /// <summary>
        /// Copy the bits to an array. Final byte is padded with 0 bits if necessary.
        /// </summary>
        /// <returns>Returns the number of bits copied.</returns>
        public long CopyTo(byte[] array, int offset)
        {
            int length = position + (shift + 7) / 8;
            Array.Copy(buffer, 0, array, offset, length);
            return Length;
        }

        /// <summary>
        /// Creates an array containing the bits. Final byte is padded with 0 bits if necessary.
        /// </summary>
        public byte[] ToArray()
        {
            byte[] array = new byte[position + (shift + 7) / 8];
            Buffer.BlockCopy(buffer, 0, array, 0, array.Length);
            return array;
        }

        /// <summary>
        /// Gets the number of bits written.
        /// </summary>
        public long Length
        {
            get { return 8L * position + (8 - shift) & 7; }
        }
    }
}