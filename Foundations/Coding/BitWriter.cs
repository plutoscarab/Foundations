
/*
BitWriter.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
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
    /// 
    /// </summary>
    public sealed partial class BitWriter
    {
        static byte[] mask = Enumerable.Range(0, 9).Select(i => (byte)(0xFF >> (8 - i))).ToArray();

        byte[] buffer;
        int position;
        int shift;

        /// <summary>
        /// 
        /// </summary>
        public BitWriter(int initialCapacityInBytes)
        {
            buffer = new byte[initialCapacityInBytes];
        }

        /// <summary>
        /// 
        /// </summary>
        public void Write(Coding.Code code)
        {
            Write(code.Bits, code.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Write(ulong bits, int count)
        {
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
        /// 
        /// </summary>
        public void CopyTo(Stream stream)
        {
            stream.Write(buffer, 0, position + (shift + 7) / 8);
        }

        /// <summary>
        /// 
        /// </summary>
        public Task CopyToAsync(Stream stream)
        {
            var source = new TaskCompletionSource<bool>();

            stream.BeginWrite(buffer, 0, position + (shift + 7) / 8, iar => 
            {
                try
                {
                    stream.EndWrite(iar);
                    source.SetResult(true);
                }
                catch(Exception e)
                {
                    source.SetException(e);
                }
            }, null);

            return source.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public int CopyTo(byte[] array)
        {
            int length = position + (shift + 7) / 8;
            Array.Copy(buffer, 0, array, 0, length);
            return length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int CopyTo(byte[] array, int offset)
        {
            int length = position + (shift + 7) / 8;
            Array.Copy(buffer, 0, array, offset, length);
            return length;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] ToArray()
        {
            byte[] array = new byte[position + (shift + 7) / 8];
            Buffer.BlockCopy(buffer, 0, array, 0, array.Length);
            return array;
        }
    }
}