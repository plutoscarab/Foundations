
/*
Sobol.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using Foundations.Types;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Sobol : IUniformSource
    {
        readonly ulong[] v;
        ulong value;
        ulong skip;
        readonly IEnumerator<int> ruler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polynomialExponents"></param>
        /// <param name="initialValues"></param>
        public Sobol(int[] polynomialExponents, params int[] initialValues)
        {
            v = GetDirectionVectors(64, polynomialExponents, initialValues);
            ruler = Sequences.Ruler().GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polynomialCode"></param>
        /// <param name="initialValues"></param>
        public Sobol(int polynomialCode, params int[] initialValues)
            : this(PolyGF2.FromCode(2UL * (ulong)polynomialCode ^ 1UL ^ (1UL << initialValues.Length))
                  .Exponents.ToArray(), initialValues)
        {
        }

        private Sobol(Sobol other)
        {
            v = other.v;
            value = other.value;
            skip = other.skip;
            ruler = Sequences.Ruler(skip).GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IUniformSource Clone()
        {
            return new Sobol(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ulong Next()
        {
            skip++;
            ruler.MoveNext();
            return value ^= v[ruler.Current];
        }

        internal static ulong[] GetDirectionVectors(int d, int[] polynomialExponents, int[] initialValues)
        {
            if (initialValues == null)
                throw new ArgumentNullException(nameof(initialValues));

            int n = initialValues.Length;
            if (n != polynomialExponents.Max())
                throw new ArgumentException("The number of initial values must be the same as the degree of the primitive polynomial.");

            // Validate initial values.
            var v = new ulong[d];
            long max = 1;
            var m = initialValues;

            for (int i = 0; i < n; i++)
            {
                max <<= 1;

                if (m[i] < 0)
                    throw new ArgumentException($"Initial values cannot be negative. {m[i]} is definitely negative.");

                if (m[i] >= max)
                    throw new ArgumentException($"The i-th initial value (starting with i=0) must be less than 2^i. ${m[i]} should be less than {max}.");

                if ((m[i] & 1) == 0)
                    throw new ArgumentException($"Initial values must be odd. {m[i]} is even.");

                v[i] = (ulong)m[i] << (63 - i);
            }

            // Extrapolate remaining direction vectors.
            for (int i = n; i < d; i++)
            {
                v[i] = v[i - n] >> n;

                for (int j = 1; j <= n; j++)
                {
                    if (polynomialExponents.Contains(n - j))
                    {
                        v[i] ^= v[i - j];
                    }
                }
            }

            return v;
        }
    }
}