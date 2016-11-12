
/*
Code.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Text;

namespace Foundations.Coding
{
    /// <summary>
    /// Represents a sequence of 0 to 64 bits
    /// </summary>
    public struct Code
    {
        /// <summary>
        /// Zero-length code.
        /// </summary>
        public static Code Empty = new Code(0, 0);

        private ulong bits;
        private int length;

        /// <summary>
        /// The bits. The unused most-significant bits are set to zero.
        /// </summary>
        public ulong Bits
        { get { return bits; } }

        /// <summary>
        /// The number of bits in the code
        /// </summary>
        public int Length
        { get { return length; } }

        /// <summary>
        /// Creates a Code object containing the specified bits and length.
        /// </summary>
        /// <param name="bits">The bits. The unused most-significant bits are set to zero.</param>
        /// <param name="length">The number of bits in the code.</param>
        public Code(ulong bits, int length)
        {
            if (length < 0 || length > 64)
                throw new Exception();

            this.bits = bits;
            this.length = length;
        }

        /// <summary>
        /// Creates a Code object containing the specified bits and length.
        /// </summary>
        /// <param name="bits">The bits. The unused most-significant bits are set to zero.</param>
        /// <param name="length">The number of bits in the code.</param>
        public Code(long bits, int length)
            : this((ulong)bits, length)
        {
        }

        /// <summary>
        /// Creates a code by concatenating existing codes.
        /// </summary>
        public Code(params Code[] codes)
        {
            int length = 0;
            int count = codes.Length;
            ulong bits = 0;

            for (int i = 0; i < count; i++)
            {
                bits <<= codes[i].Length;
                bits |= codes[i].Bits;
                length += codes[i].Length;
            }

            if (length > 64)
                throw new ArgumentOutOfRangeException();

            this.bits = bits;
            this.length = length;
        }

        /// <summary>
        /// Creates a code by appending a code to an existing one.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Code Append(Code code)
        {
            return new Code(this, code);
        }

        /// <summary>
        /// Converts the bits of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>Returns a string of '0' and '1' characters representing the code bits.</returns>
        public override string ToString()
        {
            var b = new StringBuilder();

            for (int i = 0; i < Length; i++)
            {
                if ((Bits & (1UL << (Length - i - 1))) == 0)
                    b.Append('0');
                else
                    b.Append('1');
            }

            return b.ToString();
        }

        /// <summary>
        /// Reverses the order of the bits in the code.
        /// </summary>
        /// <returns>A code of the same length with the bits in reverse order.</returns>
        public Code Reverse()
        {
            return new Code(ReverseBits(Bits, Length), Length);
        }

        /// <summary>
        /// Reverses the order of bits in a number.
        /// </summary>
        /// <param name="bits">The original bits, with zero's in the unused MSB's.</param>
        /// <param name="length">The number of bits to reverse.</param>
        /// <returns></returns>
        public static ulong ReverseBits(ulong bits, int length)
        {
            if (length < 1)
                throw new ArgumentOutOfRangeException();

            ulong temp = 0;

            while (length-- > 0)
            {
                temp = (temp << 1) | (bits & 1);
                bits >>= 1;
            }

            return temp;
        }

        /// <summary>
        /// Returns the Elias Delta code for the specified positive integer.
        /// </summary>
        /// <param name="n">A positive integer.</param>
        /// <returns>The Elias Delta code representing the integer.</returns>
        public static Code EliasDelta(int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException();

            // special case for n=1
            if (n == 1)
                return new Code(1, 1);

            // count the number of bits in the original number
            int bits = Coding.Bits.Count(n);

            // count the number of bits in *that* number
            int bits2 = Coding.Bits.Count(bits);

            // Remove the leading '1' bit from the original number and prefix
            // the bits by a number representing the number of bits in the original
            long code = (long)n ^ ((long)(bits ^ 1) << (bits - 1));

            // Prefix all of that with a number of 0's equal to one less than bits2
            return new Code(n, bits + 2 * bits2 - 2);
        }

        const int fibs = 45;
        static int[] fib = new int[fibs]
            {
                1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765,
                10946, 17711, 28657, 46368, 75025, 121393, 196418, 317811, 514229, 832040, 1346269,
                2178309, 3524578, 5702887, 9227465, 14930352, 24157817, 39088169, 63245986,
                102334155, 165580141, 267914296, 433494437, 701408733, 1134903170, 1836311903
            };

        /// <summary>
        /// Returns the Fibonacci code representing the specified positive integer.
        /// </summary>
        /// <param name="n">An integer between 1 and 1,836,311,902.</param>
        /// <returns>The Fibonacci Code representation of the number.</returns>
        public static Code Fibonacci(int n)
        {
            if (n < 1 || n >= fib[fibs - 1])
                throw new ArgumentOutOfRangeException();

            // find the largest Fibonacci number not greater than n
            int i = 0;
            while (fib[i + 1] <= n)
                i++;

            // Start forming the code. Always starts with '1' bit.
            long code = 1;
            int length = 1;

            // Decompose n into the sum of Fibonacci numbers
            while (i >= 0)
            {
                // make room for the next bit
                code <<= 1;
                length++;

                // add a '1' bit if the current Fibonacci number is in the sum
                if (n >= fib[i])
                {
                    n -= fib[i];
                    code |= 1;
                }

                // move to the next smallest Fibonacci number
                i--;
            }

            return new Code(code, length);
        }

        /// <summary>
        /// Get the truncated binary code. This is not a prefix code.
        /// </summary>
        /// <param name="n">Value.</param>
        /// <param name="m">One more than maximum value. Minimum value is zero.</param>
        /// <returns></returns>
        public static Code TruncatedBinary(int n, int m)
        {
            if (m < 1 || n < 0 || n >= m)
                throw new ArgumentOutOfRangeException();

            int bits = Coding.Bits.Count(m - 1);
            int extra = (1 << bits) - m;

            if (n < extra)
                return new Code(n, bits - 1);
            else
                return new Code(extra + n, bits);
        }

        /// <summary>
        /// Get the unary code. This is a prefix code.
        /// </summary>
        public static Code Unary(int n)
        {
            if (n < 0 || n > 62)
                throw new ArgumentOutOfRangeException();

            long bits = ((1 << n) - 1) << 1;
            return new Code(bits, n + 1);
        }

        /// <summary>
        /// Get the Golomb code. This is a prefix code.
        /// </summary>
        public static Code Golomb(int n, int divisor)
        {
            if (n < 0 || divisor < 2)
                throw new ArgumentOutOfRangeException();

            return new Code(Unary(n / divisor), TruncatedBinary(n % divisor, divisor));
        }

        /// <summary>
        /// Get the Rice code. This is a prefix code.
        /// </summary>
        public static Code Rice(int n, int powerOf2)
        {
            if (n < 0 || powerOf2 < 1)
                throw new ArgumentException();

            long mask = (1 << powerOf2) - 1;
            return new Code(Unary(n >> powerOf2), new Code(n & mask, powerOf2));
        }
    }
}