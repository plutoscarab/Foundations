
/*
BitReader.cs

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
    public sealed partial class BitReader
    {
        static byte[] mask = Enumerable.Range(0, 9).Select(i => (byte)(0xFF >> (8 - i))).ToArray();

        byte[] buffer;
        int position;
        int shift;

        /// <summary>
        /// 
        /// </summary>
        public BitReader(byte[] buffer)
            : this(buffer, 0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public BitReader(byte[] buffer, int offset)
        {
            this.buffer = buffer;
            this.position = offset;
        }

        /// <summary>
        /// 
        /// </summary>
        public ulong Read(int count)
        {
            ulong bits = 0;
            int length = 0;

            while (count > 0)
            {
                int toRead = Math.Min(count, 8 - shift);
                if (position >= buffer.Length) throw new EndOfStreamException();
                bits <<= toRead;
                bits |= ((uint)buffer[position] >> (8 - shift - toRead)) & mask[toRead];
                length += toRead;
                shift += toRead;

                if (shift == 8)
                {
                    position++;
                    shift = 0;
                }

                count -= toRead;
            }

            return bits;
        }
    }
}