
/*
EliasDelta.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
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
        /// Elias Delta code. Value is preceded by bit length, and bit length
        /// is preceded by unary-coded length of bit length.
        /// </summary>
        public static readonly IBitEncoding EliasDelta = new EliasDelta();
    }

    /// <summary>
    /// Elias Delta code. Value is preceded by bit length, and bit length
    /// is preceded by unary-coded length of bit length.
    /// </summary>
    public sealed partial class EliasDelta : IBitEncoding
    {
        internal EliasDelta()
        {
        }

        /// <summary>
        /// Gets the minimum number that can be encoded.
        /// </summary>
        public int MinEncodable
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the minimum number that can be encoded.
        /// </summary>
        public int MaxEncodable
        {
            get
            {
                return int.MaxValue;
            }
        }

        /// <summary>
        /// Gets the Elias Delta code corresponding to a value.
        /// </summary>
        public Code GetCode(int value)
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException();

            // count the number of bits in the original number
            int bits = Bits.FloorLog2(value);

            // count the number of bits in *that* number
            int bits2 = Bits.FloorLog2(bits + 1);

            // Remove the leading '1' bit from the original number and prefix
            // the bits by a number representing the number of bits in the original
            long code = value ^ (1L << bits) ^ ((bits + 1L) << bits);

            // Prefix all of that with a number of 0's equal to one less than bits2
            return new Code(code, bits + 2 * bits2 + 1);
        }

        /// <summary>
        /// Reads an encoded value from a <see cref="BitReader"/>.
        /// </summary>
        public int Read(BitReader reader)
        {
            var u = Codes.UnaryZeros.Read(reader) - 1;
            var len = (int)((reader.Read(u) | (1ul << u)) - 1);
            return (int)(reader.Read(len) | (1ul << len));
        }
    }
}