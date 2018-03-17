
/*
Fibonacci.cs

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
        /// Fibonacci code. Value is encoded as Zeckendorf's representation
        /// and terminated with a final '1' bit.
        /// </summary>
        public static readonly IBitEncoding Fibonacci = new Fibonacci();
    }

    /// <summary>
    /// Fibonacci code. Value is encoded as Zeckendorf's representation
    /// and terminated with a final '1' bit.
    /// </summary>
    public sealed partial class Fibonacci : IBitEncoding
    {
        internal Fibonacci()
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
                return fib[fibs - 1] - 1;
            }
        }

        /// <summary>
        /// Gets the Fibonacci code corresponding to a value.
        /// </summary>
        public Code GetCode(int value)
        {
            if (value < 1 || value >= fib[fibs - 1])
                throw new ArgumentOutOfRangeException();

            // find the largest Fibonacci number not greater than n
            int i = 0;
            while (fib[i + 1] <= value)
                i++;

            // Start forming the code. Always ends with '1' bit.
            long code = 1;
            int length = 1;

            // Decompose n into the sum of Fibonacci numbers
            while (i >= 0)
            {
                // add a '1' bit if the current Fibonacci number is in the sum
                if (value >= fib[i])
                {
                    value -= fib[i];
                    code |= 1L << length;
                }

                // move to the next smallest Fibonacci number
                i--;
                length++;
            }

            return new Code(code, length);
        }

        /// <summary>
        /// Gets the value corresponding to a Fibonacci code.
        /// </summary>
        public int Read(BitReader reader)
        {
            int value = 0;
            int f = 0;
            bool bit = false;

            while (true)
            {
                if (reader.Read(1) == 0)
                {
                    bit = false;
                }
                else if (bit)
                {
                    break;
                }
                else
                {
                    bit = true;
                    value += fib[f];
                }

                f++;
            }

            return value;
        }

        const int fibs = 45;

        static int[] fib = new int[fibs]
        {
            1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181,
            6765, 10946, 17711, 28657, 46368, 75025, 121393, 196418, 317811, 514229,
            832040, 1346269, 2178309, 3524578, 5702887, 9227465, 14930352, 24157817,
            39088169, 63245986, 102334155, 165580141, 267914296, 433494437, 701408733,
            1134903170, 1836311903
        };
    }
}