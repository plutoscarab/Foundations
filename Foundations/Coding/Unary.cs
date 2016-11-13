
/*
Unary.cs

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
        /// Unary code using 1's as tally bits and 0 as terminator.
        /// </summary>
        public static readonly IEncoding<int, Code> UnaryOnes = new Unary(1);

        /// <summary>
        /// Unary code using 0's as tally bits and 1 as terminator.
        /// </summary>
        public static readonly IEncoding<int, Code> UnaryZeros = new Unary(0);
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Unary : IEncoding<int, Code>
    {
        int tally;

        internal Unary(int tally)
        {
            this.tally = tally;
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
                return 63;
            }
        }

        /// <summary>
        /// Gets the code corresponding to a value.
        /// </summary>
        public Code GetCode(int value)
        {
            if (value < MinEncodable || value > MaxEncodable)
                throw new ArgumentOutOfRangeException();

            return new Code(1 - 3 * tally, value);
        }

        /// <summary>
        /// Reads an encoded value from a <see cref="BitReader"/>.
        /// </summary>
        public int Read(BitReader reader)
        {
            int value = 1;

            while ((int)reader.Read(1) == tally)
                value++;

            return value;
        }
    }
}