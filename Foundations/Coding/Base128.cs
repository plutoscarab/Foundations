
/*
Base128.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.IO;

namespace Foundations.Coding
{
    /// <summary>
    /// Encoding implementations.
    /// </summary>
    public static partial class Codes
    {
        /// <summary>
        /// Base128 code. Encodes the value 7 bits at a time with the 8th 
        /// bit indicating whether another byte is available.
        /// </summary>
        public static readonly IByteEncoding Base128 = new Base128();
    }

    /// <summary>
    /// Base128 code. Encodes the value 7 bits at a time with the 8th 
    /// bit indicating whether another byte is available.
    /// </summary>
    public sealed partial class Base128 : IByteEncoding
    {
        internal Base128()
        {
        }

        /// <summary>
        /// Write a <see cref="System.UInt64"/> to a <see cref="Stream"/>.
        /// </summary>
        public void Write(Stream stream, ulong value)
        {
            while (value >= 0x80)
            {
                stream.WriteByte((byte)((byte)value | 0x80));
                value >>= 7;
            }

            stream.WriteByte((byte)value);
        }

        /// <summary>
        /// Read a <see cref="System.Int64"/> from a <see cref="Stream"/>.
        /// </summary>
        public ulong Read(Stream stream)
        {
            ulong value = 0;
            int shift = 0;
            int b;

            while (shift <= 56)
            {
                b = stream.ReadByte();
                if (b == -1) throw new EndOfStreamException();
                value |= (ulong)(b & 0x7F) << shift;
                if (b < 0x80) return value;
                shift += 7;
            }

            // Shift is 63, so we can only allow one more bit.
            // It must be a 1, otherwise the value would have
            // already finished.
            b = stream.ReadByte();
            if (b == -1) throw new EndOfStreamException();
            if (b != 1) throw new InvalidDataException();
            return value | (1UL << 63);
        }
    }
}