
/*
EliasFibonacci.cs

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
        /// EliasFibonacci code. Value is precided by Fibonacci-coded bit length.
        /// </summary>
        public static readonly IBitEncoding EliasFibonacci = new EliasFibonacci();
    }

    /// <summary>
    /// EliasFibonacci code. Value is precided by Fibonacci-coded bit length.
    /// </summary>
    public sealed partial class EliasFibonacci : IBitEncoding
    {
        internal EliasFibonacci()
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
        /// Gets the code corresponding to a value.
        /// </summary>
        public Code GetCode(int value)
        {
            if (value < MinEncodable || value > MaxEncodable)
                throw new ArgumentOutOfRangeException();

            int b = Bits.FloorLog2(value);
            var f = Codes.Fibonacci.GetCode(b + 1);
            ulong bits = (uint)value | (f.Bits << b);
            int length = b + f.Length;
            return new Code(bits, length);
        }

        /// <summary>
        /// Reads an encoded value from a <see cref="BitReader"/>.
        /// </summary>
        public int Read(BitReader reader)
        {
            int f = Codes.Fibonacci.Read(reader) - 1;
            int value = (int)reader.Read(f);
            return value | (1 << f);
        }
    }
}