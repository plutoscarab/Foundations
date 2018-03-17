
/*
Golomb.cs

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
        /// Golomb code. Remainder after division is encoded using TruncatedBinary,
        /// preceded by unary-coded quotient.
        /// </summary>
        public static IBitEncoding Golomb(int divisor) => new Golomb(divisor);
    }

    /// <summary>
    /// Golomb code. Remainder after division is encoded using TruncatedBinary,
    /// preceded by unary-coded quotient.
    /// </summary>
    public sealed partial class Golomb : IBitEncoding
    {
        private readonly int divisor;
        private readonly IBitEncoding remainderCode;
        private readonly int maxEncodable;

        internal Golomb(int divisor)
        {
            if (divisor < 2)
                throw new ArgumentOutOfRangeException();

            this.divisor = divisor;
            remainderCode = Codes.TruncatedBinary(divisor);
            var r = remainderCode.MaxEncodable;
            maxEncodable = divisor * (63 - remainderCode.GetCode(r).Length) + r;
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
                return maxEncodable;
            }
        }

        /// <summary>
        /// Gets the code corresponding to a value.
        /// </summary>
        public Code GetCode(int value)
        {
            if (value < MinEncodable || value > MaxEncodable)
                throw new ArgumentOutOfRangeException();

            return new Code(Codes.UnaryOnes.GetCode(value / divisor + 1), remainderCode.GetCode(value % divisor));
        }

        /// <summary>
        /// Reads an encoded value from a <see cref="BitReader"/>.
        /// </summary>
        public int Read(BitReader reader)
        {
            return (Codes.UnaryOnes.Read(reader) - 1) * divisor
                + remainderCode.Read(reader);
        }
    }
}