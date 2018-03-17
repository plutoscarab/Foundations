
/*
EliasGamma.cs

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
        /// Elias Gamma code. Value is preceded by unary-coded bit length.
        /// </summary>
        public static readonly IBitEncoding EliasGamma = new EliasGamma();
    }

    /// <summary>
    /// Elias Gamma code. Value is preceded by unary-coded bit length.
    /// </summary>
    public sealed partial class EliasGamma : IBitEncoding
    {
        internal EliasGamma()
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
        /// Gets the Elias Gamma code corresponding to a value.
        /// </summary>
        public Code GetCode(int value)
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException();

            // count the number of bits in the number
            int bits = Bits.FloorLog2(value);

            // create a code using the original bits but prefixing it with a number
            // of 0's equal to one less than the number of bits in the original
            return new Code(value, 2 * bits + 1);
        }

        /// <summary>
        /// Reads an encoded value from a <see cref="BitReader"/>.
        /// </summary>
        public int Read(BitReader reader)
        {
            var u = Codes.UnaryZeros.Read(reader) - 1;
            return (int)reader.Read(u) | (1 << u);
        }
    }
}