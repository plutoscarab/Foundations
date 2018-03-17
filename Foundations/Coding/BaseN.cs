
/*
BaseN.cs

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
        /// BaseN code. Encodes the value is a base-N value and writes
        /// each base-N digit using TruncatedBinary code, or FixedWidth
        /// code if the base is a power of 2.
        /// </summary>
        public static IBitEncoding BaseN(int n) => new BaseN(n);

        /// <summary>
        /// BaseN code with power-of-2 base. Encodes fixed-width groups
        /// of bits, with an additional bit for each group to indicate
        /// whether another group is available.
        /// </summary>
        public static IBitEncoding PowerOf2Base(int powerOf2) => new BaseN(1 << powerOf2);
    }

    /// <summary>
    /// BaseN code. Encodes the value is a base-N value and writes
    /// each base-N digit using TruncatedBinary code, or FixedWidth
    /// code if the base is a power of 2.
    /// </summary>
    public sealed partial class BaseN : IBitEncoding
    {
        readonly int n;
        readonly IBitEncoding encoding;

        internal BaseN(int n)
        {
            this.n = n;

            if (Bits.IsPowerOf2(n))
                encoding = Codes.FixedWidth(Bits.FloorLog2(n) + 1);
            else
                encoding = Codes.TruncatedBinary(n);
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
        /// Gets the code corresponding to a value.
        /// </summary>
        public Code GetCode(int value)
        {
            if (value < MinEncodable || value > MaxEncodable)
                throw new ArgumentOutOfRangeException();

            var code = Code.Empty;

            while (true)
            {
                code = code.Append(value < n ? Code.Zero : Code.One);
                code = code.Append(encoding.GetCode(value % n));
                value /= n;
                if (value == 0) return code;
            }
        }

        /// <summary>
        /// Gets the value corresponding to a code.
        /// </summary>
        public int Read(BitReader reader)
        {
            int value = 0;
            int power = 1;

            while (true)
            {
                bool done = reader.Read(1) == 0;
                value += power * encoding.Read(reader);
                if (done) return value;
                power *= n;
            }
        }
    }
}