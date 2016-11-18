﻿
/*
GCD.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

THIS IS AN AUTO-GENERATED SOURCE FILE. DO NOT EDIT THIS FILE DIRECTLY.
INSTEAD, EDIT THE .tt FILE WITH THE SAME NAME AND RE-RUN THE TEXT TEMPLATING
FILE GENERATOR. IF YOU SAVE THE FILE IN VISUAL STUDIO IT WILL DO THIS FOR YOU.
*/

using System;
using System.Numerics;

namespace Foundations.Functions
{
    /// <summary>
    /// Number-theoretic functions.
    /// </summary>
	public static partial class NumberTheoretic
    {
        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static int GCD(int p, int q)
        {
            if (p == 0 || q == 0) return 0;
            p = p < 0 ? -p : p;
            q = q < 0 ? -q : q;

            while (true)
            {
                var m = p % q;
                if (m == 0) return q;
                p = q;
                q = m;
            }
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static uint GCD(uint p, uint q)
        {
            if (p == 0 || q == 0) return 0;

            while (true)
            {
                var m = p % q;
                if (m == 0) return q;
                p = q;
                q = m;
            }
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static long GCD(long p, long q)
        {
            if (p == 0 || q == 0) return 0;
            p = p < 0 ? -p : p;
            q = q < 0 ? -q : q;

            while (true)
            {
                var m = p % q;
                if (m == 0) return q;
                p = q;
                q = m;
            }
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static ulong GCD(ulong p, ulong q)
        {
            if (p == 0 || q == 0) return 0;

            while (true)
            {
                var m = p % q;
                if (m == 0) return q;
                p = q;
                q = m;
            }
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static BigInteger GCD(BigInteger p, BigInteger q)
        {
            if (p == 0 || q == 0) return 0;
            p = p < 0 ? -p : p;
            q = q < 0 ? -q : q;

            while (true)
            {
                var m = p % q;
                if (m == 0) return q;
                p = q;
                q = m;
            }
        }

    }
}
