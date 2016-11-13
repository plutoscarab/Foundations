
/*
TruncatedBinary.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations.Coding
{
    /// <summary>
    /// Encoding implementations.
    /// </summary>
    public static partial class Codes
    {
        /// <summary>
        /// Truncated Binary code.
        /// </summary>
        public static IEncoding<int, Code> TruncatedBinary(int range) => new TruncatedBinary(range);
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TruncatedBinary : IEncoding<int, Code>
    {
        private int range;
        private int bits;
        private int extra;

        internal TruncatedBinary(int range)
        {
            if (range < 1)
                throw new ArgumentOutOfRangeException();

            this.range = range;
            bits = Bits.FloorLog2(range - 1) + 1;
            extra = (int)((1L << bits) - range);
        }

        /// <summary>
        /// Gets the minimum number that can be encoded.
        /// </summary>
        public int MinEncodable
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the minimum number that can be encoded.
        /// </summary>
        public int MaxEncodable
        {
            get
            {
                return range - 1;
            }
        }

        /// <summary>
        /// Gets the code corresponding to a value.
        /// </summary>
        public Code GetCode(int value)
        {
            if (value < MinEncodable || value > MaxEncodable)
                throw new ArgumentOutOfRangeException();

            if (value < extra)
                return new Code(value, bits - 1);
            else
                return new Code(extra + value, bits);
        }

        /// <summary>
        /// Reads an encoded value from a <see cref="BitReader"/>.
        /// </summary>
        public int Read(BitReader reader)
        {
            int value = (int)reader.Read(bits - 1);
            if (value < extra) return value;
            return value * 2 + (int)reader.Read(1) - extra;
        }
    }
}