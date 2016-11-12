
/*
EliasDelta.cs

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
        /// Elias Gamma code.
        /// </summary>
        public static readonly IEncoding<int, Code> EliasDelta = new EliasDelta();
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class EliasDelta : IEncoding<int, Code>
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
        /// Gets the value corresponding to an Elias Delta code.
        /// </summary>
        public int GetValue(Code code)
        {
            int bits2 = code.Length - Bits.FloorLog2(code.Bits);
            int bits = (int)(code.Bits >> (code.Length - 2 * bits2 + 1)) - 1;

            if (code.Length != bits + 2 * bits2 - 1)
                throw new ArgumentException("Code is not a valid Elias Delta code.");

            var mask = 1UL << bits;
            return (int)(mask | (code.Bits & (mask - 1)));
        }
    }
}