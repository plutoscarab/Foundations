
/*
Sobol.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
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
    /// Sobol sequence of quasirandom values in [0, 2^64),
    /// and implementation of <see cref="IUniformSource"/>.
    /// </summary>
    public sealed class Sobol : IUniformSource
    {
        readonly ulong[] v;
        ulong value;
        ulong skip;
        IEnumerator<int> ruler;

        /// <summary>
        /// Creates a <see cref="Sobol"/> with specified initial
        /// values and primitive polynomial coefficients for extending
        /// the initial values to a full set of direction vectors.
        /// </summary>
        public Sobol(int[] polynomialExponents, params int[] initialValues)
            : this(GetDirectionVectors(64, polynomialExponents, initialValues))
        {
        }

        /// <summary>
        /// Creates a <see cref="Sobol"/> with specified initial
        /// values and primitive polynomial coefficients packed into
        /// bits, with leading and trailing coefficient omitted.
        /// </summary>
        /// <param name="polynomialCode">Bit 0 corresponds to x^1.</param>
        /// <param name="initialValues">Initial values.</param>
        public Sobol(int polynomialCode, params int[] initialValues)
            : this(PolyGF2.FromCode(2UL * (ulong)polynomialCode ^ 1UL ^ (1UL << initialValues.Length))
                  .Exponents.ToArray(), initialValues)
        {
        }

        /// <summary>
        /// Creates a <see cref="Sobol"/> using specified 
        /// direction vectors.
        /// </summary>
        public Sobol(ulong[] directionVectors)
        {
            v = directionVectors;
            if (v == null) throw new ArgumentNullException();
            if (v.Length < 64) throw new ArgumentException("If you want to supply fewer than 64 direction vectors, use one of the other constructors.");
            if (v.Length > 64) throw new ArgumentException("Do not provide more than 64 direction vectors.");

            for (int i = 0; i < 64; i++)
            {
                if (i < 63 && (v[i] << (i + 1)) != 0) throw new ArgumentException($"{63 - i} least-significant bits of element {i} must be 0.");
                if ((v[i] << i) == 0) throw new ArgumentException($"Bit {63 - i} of element {i} must be 1.");
            }

            ruler = Sequences.Ruler().GetEnumerator();
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
            var result = value;
            value ^= v[ruler.Current];
            return result;
        }

        /// <summary>
        /// Skip past a number of sequence values. This is
        /// a fast O(1) operation, independent of the count of values 
        /// skipped.
        /// </summary>
        public void Skip(ulong count)
        {
            skip += count;
            value = 0;
            ulong g = skip ^ (skip >> 1);
            
            for (int i = 0; i < 64; i++)
            {
                if ((g & 1) != 0) value ^= v[i];
                g >>= 1;
            }

            ruler.Dispose();
            ruler = Sequences.Ruler(skip).GetEnumerator();
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