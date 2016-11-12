
/*
EliasGamma.cs

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
        public static readonly IEncoding EliasGamma = new EliasGamma();
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class EliasGamma : IEncoding
    {
        internal EliasGamma()
        {
        }

        /// <summary>
        /// Gets the category of the Elias Gamma code.
        /// </summary>
        public CodeCategory Category
        {
            get
            {
                return CodeCategory.Universal | CodeCategory.LexicographicallyOrdered;
            }
        }

        /// <summary>
        /// Gets the minimum number that can be encoded.
        /// </summary>
        public int MaxValue
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the minimum number that can be encoded.
        /// </summary>
        public int MinValue
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
        /// Gets the value corresponding to an Elias Gamma code.
        /// </summary>
        public int GetValue(Code code)
        {
            int f = Bits.FloorLog2(code.Bits);

            if (code.Length != 2 * f + 1)
                throw new ArgumentException("Code is not a valid Elias Gamma code.");

            return (int)(code.Bits & ((2ul << f) - 1));
        }
    }
}