
/*
GCD.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;

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
            p = Math.Abs(p);
            q = Math.Abs(q);

            while (true)
            {
                int m = p % q;
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
                uint m = p % q;
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
            p = Math.Abs(p);
            q = Math.Abs(q);

            while (true)
            {
                long m = p % q;
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
                ulong m = p % q;
                if (m == 0) return q;
                p = q;
                q = m;
            }
        }

    }
}
