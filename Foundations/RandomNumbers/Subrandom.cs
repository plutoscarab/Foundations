﻿
/*
Subrandom.cs

THIS IS AN AUTO-GENERATED SOURCE FILE. DO NOT EDIT THIS FILE DIRECTLY.
INSTEAD, EDIT THE .tt FILE WITH THE SIMILAR NAME AND RE-RUN THE TEXT TEMPLATING
FILE GENERATOR. IF YOU SAVE THE FILE IN VISUAL STUDIO IT WILL DO THIS FOR YOU.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Foundations.Types;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// Low-discrepency sequences, a.k.a. subrandom or quasirandom numbers.
    /// </summary>
    public static class Subrandom
    {
        /// <summary>
        /// Additive recurrence, sₙ = (s₀ + αn) mod 1.
        /// </summary>
        /// <param name="s0">Value from 0 to 1 exclusive.</param>
        /// <param name="α">Value from 0 to 1 exclusive.</param>
	    public static IEnumerable<Double> AdditiveRecurrence(Double s0, Double α)
        {
            if (s0 < 0 || s0 >= 1) throw new ArgumentOutOfRangeException(nameof(s0));
            if (α <= 0 || α >= 1) throw new ArgumentOutOfRangeException(nameof(α));
            var s = s0;

            while (true)
            {
                yield return s;
                s += α;
                if (s >= 1) s -= 1;
            }
        }

        /// <summary>
        /// Additive recurrence, sₙ = (α + αn) mod 1.
        /// </summary>
        /// <param name="α">Value from 0 to 1 exclusive.</param>
	    public static IEnumerable<Double> AdditiveRecurrence(Double α)
        {
            return AdditiveRecurrence(α, α);
        }

        /// <summary>
        /// Additive recurrence sₙ = (s₀ + αn) mod 1 with random s₀ and algebraic α of degree 2
        /// using specified random number generator.
        /// </summary>
	    public static IEnumerable<Double> AdditiveRecurrenceD(Generator generator)
        {
            if (generator == null) throw new ArgumentNullException();

            return AdditiveRecurrence(generator.Double(), (Double)
                generator.UInt64s()
                    .Select(u => Math.Sqrt(u))
                    .Select(f => f - Math.Floor(f))
                    .First(f => f > 0.3D && f < 0.7D));
        }

        /// <summary>
        /// Additive recurrence sₙ = (s₀ + αn) mod 1 with random s₀ and algebraic α of degree 2
        /// using default random number generator.
        /// </summary>
	    public static IEnumerable<Double> AdditiveRecurrenceD()
        {
            return AdditiveRecurrenceD(new Generator());
        }

      /// <summary>
        /// Van der Corput sequence.
        /// </summary>
        public static IEnumerable<Double> VanDerCorputD(int @base)
        {
            foreach (var pair in VanDerCorput(@base))
            {
                yield return pair.Item1 / (Double)pair.Item2;
            }
        }

        /// <summary>
        /// Halton sequence.
        /// </summary>
        public static IEnumerable<Double[]> HaltonD(int[] bases)
        {
            ValidateHalton(bases);
            var s = bases.Select(b => VanDerCorputD(b).GetEnumerator()).ToArray();
            var x = new Double[bases.Length];

            while (true)
            {
                for (int i = 0; i < bases.Length; i++)
                {
                    s[i].MoveNext();
                    x[i] = s[i].Current;
                }

                yield return x;
            }
        }

        /// <summary>
        /// Halton sequence.
        /// </summary>
        public static IEnumerable<Double[]> HaltonD(int dimension)
        {
            if (dimension < 0) throw new ArgumentOutOfRangeException();
            return HaltonD(Sequences.Primes().Select(_ => (int)_).Take(dimension).ToArray());
        }

        /// <summary>
        /// Hammersley set.
        /// </summary>
        public static IEnumerable<Double[]> HammersleyD(int[] bases, int N)
        {
            if (N < 0) throw new ArgumentOutOfRangeException();
            ValidateHalton(bases);
            var s = bases.Select(b => VanDerCorputD(b).GetEnumerator()).ToArray();
            var x = new Double[bases.Length + 1];

            for (int n = 1; n <= N; n++)
            {
                for (int i = 0; i < bases.Length; i++)
                {
                    s[i].MoveNext();
                    x[i] = s[i].Current;
                }

                x[bases.Length] = n / (Double)N;
                yield return x;
            }
        }

        /// <summary>
        /// Hammersley sequence.
        /// </summary>
        public static IEnumerable<Double[]> HammersleyD(int dimension, int N)
        {
            if (dimension < 2) throw new ArgumentOutOfRangeException();
            return HammersleyD(Sequences.Primes().Select(_ => (int)_).Take(dimension - 1).ToArray(), N);
        }

        /// <summary>
        /// Sobol sequence.
        /// </summary>
        public static IEnumerable<Double> SobolD(PolyGF2 primitivePolynomial, params int[] initialValues)
        {
            var v = SobolDirectionVectors(64, primitivePolynomial, initialValues);
            ulong s = 0;
            var d = ((System.Numerics.BigInteger)1) << 64;

            foreach (var r in Sequences.Ruler())
            {
                s ^= v[r];
                yield return s / 18446744073709551616D;
            }
        }

        /// <summary>
        /// Sobol sequence.
        /// </summary>
        public static IEnumerable<Double> SobolD(int polynomialCode, params int[] initialValues)
        {
            var primitivePolynomial = PolyGF2.FromCode(2UL * (ulong)polynomialCode ^ 1UL ^ (1UL << initialValues.Length));
            return SobolD(primitivePolynomial, initialValues);
        }

        /// <summary>
        /// Additive recurrence, sₙ = (s₀ + αn) mod 1.
        /// </summary>
        /// <param name="s0">Value from 0 to 1 exclusive.</param>
        /// <param name="α">Value from 0 to 1 exclusive.</param>
	    public static IEnumerable<Single> AdditiveRecurrence(Single s0, Single α)
        {
            if (s0 < 0 || s0 >= 1) throw new ArgumentOutOfRangeException(nameof(s0));
            if (α <= 0 || α >= 1) throw new ArgumentOutOfRangeException(nameof(α));
            var s = s0;

            while (true)
            {
                yield return s;
                s += α;
                if (s >= 1) s -= 1;
            }
        }

        /// <summary>
        /// Additive recurrence, sₙ = (α + αn) mod 1.
        /// </summary>
        /// <param name="α">Value from 0 to 1 exclusive.</param>
	    public static IEnumerable<Single> AdditiveRecurrence(Single α)
        {
            return AdditiveRecurrence(α, α);
        }

        /// <summary>
        /// Additive recurrence sₙ = (s₀ + αn) mod 1 with random s₀ and algebraic α of degree 2
        /// using specified random number generator.
        /// </summary>
	    public static IEnumerable<Single> AdditiveRecurrenceF(Generator generator)
        {
            if (generator == null) throw new ArgumentNullException();

            return AdditiveRecurrence(generator.Single(), (Single)
                generator.UInt64s()
                    .Select(u => Math.Sqrt(u))
                    .Select(f => f - Math.Floor(f))
                    .First(f => f > 0.3F && f < 0.7F));
        }

        /// <summary>
        /// Additive recurrence sₙ = (s₀ + αn) mod 1 with random s₀ and algebraic α of degree 2
        /// using default random number generator.
        /// </summary>
	    public static IEnumerable<Single> AdditiveRecurrenceF()
        {
            return AdditiveRecurrenceF(new Generator());
        }

      /// <summary>
        /// Van der Corput sequence.
        /// </summary>
        public static IEnumerable<Single> VanDerCorputF(int @base)
        {
            foreach (var pair in VanDerCorput(@base))
            {
                yield return pair.Item1 / (Single)pair.Item2;
            }
        }

        /// <summary>
        /// Halton sequence.
        /// </summary>
        public static IEnumerable<Single[]> HaltonF(int[] bases)
        {
            ValidateHalton(bases);
            var s = bases.Select(b => VanDerCorputF(b).GetEnumerator()).ToArray();
            var x = new Single[bases.Length];

            while (true)
            {
                for (int i = 0; i < bases.Length; i++)
                {
                    s[i].MoveNext();
                    x[i] = s[i].Current;
                }

                yield return x;
            }
        }

        /// <summary>
        /// Halton sequence.
        /// </summary>
        public static IEnumerable<Single[]> HaltonF(int dimension)
        {
            if (dimension < 0) throw new ArgumentOutOfRangeException();
            return HaltonF(Sequences.Primes().Select(_ => (int)_).Take(dimension).ToArray());
        }

        /// <summary>
        /// Hammersley set.
        /// </summary>
        public static IEnumerable<Single[]> HammersleyF(int[] bases, int N)
        {
            if (N < 0) throw new ArgumentOutOfRangeException();
            ValidateHalton(bases);
            var s = bases.Select(b => VanDerCorputF(b).GetEnumerator()).ToArray();
            var x = new Single[bases.Length + 1];

            for (int n = 1; n <= N; n++)
            {
                for (int i = 0; i < bases.Length; i++)
                {
                    s[i].MoveNext();
                    x[i] = s[i].Current;
                }

                x[bases.Length] = n / (Single)N;
                yield return x;
            }
        }

        /// <summary>
        /// Hammersley sequence.
        /// </summary>
        public static IEnumerable<Single[]> HammersleyF(int dimension, int N)
        {
            if (dimension < 2) throw new ArgumentOutOfRangeException();
            return HammersleyF(Sequences.Primes().Select(_ => (int)_).Take(dimension - 1).ToArray(), N);
        }

        /// <summary>
        /// Sobol sequence.
        /// </summary>
        public static IEnumerable<Single> SobolF(PolyGF2 primitivePolynomial, params int[] initialValues)
        {
            var v = SobolDirectionVectors(64, primitivePolynomial, initialValues);
            ulong s = 0;
            var d = ((System.Numerics.BigInteger)1) << 64;

            foreach (var r in Sequences.Ruler())
            {
                s ^= v[r];
                yield return s / 18446744073709551616F;
            }
        }

        /// <summary>
        /// Sobol sequence.
        /// </summary>
        public static IEnumerable<Single> SobolF(int polynomialCode, params int[] initialValues)
        {
            var primitivePolynomial = PolyGF2.FromCode(2UL * (ulong)polynomialCode ^ 1UL ^ (1UL << initialValues.Length));
            return SobolF(primitivePolynomial, initialValues);
        }

        /// <summary>
        /// Additive recurrence, sₙ = (s₀ + αn) mod 1.
        /// </summary>
        /// <param name="s0">Value from 0 to 1 exclusive.</param>
        /// <param name="α">Value from 0 to 1 exclusive.</param>
	    public static IEnumerable<Decimal> AdditiveRecurrence(Decimal s0, Decimal α)
        {
            if (s0 < 0 || s0 >= 1) throw new ArgumentOutOfRangeException(nameof(s0));
            if (α <= 0 || α >= 1) throw new ArgumentOutOfRangeException(nameof(α));
            var s = s0;

            while (true)
            {
                yield return s;
                s += α;
                if (s >= 1) s -= 1;
            }
        }

        /// <summary>
        /// Additive recurrence, sₙ = (α + αn) mod 1.
        /// </summary>
        /// <param name="α">Value from 0 to 1 exclusive.</param>
	    public static IEnumerable<Decimal> AdditiveRecurrence(Decimal α)
        {
            return AdditiveRecurrence(α, α);
        }

        /// <summary>
        /// Additive recurrence sₙ = (s₀ + αn) mod 1 with random s₀ and algebraic α of degree 2
        /// using specified random number generator.
        /// </summary>
	    public static IEnumerable<Decimal> AdditiveRecurrenceM(Generator generator)
        {
            if (generator == null) throw new ArgumentNullException();

            return AdditiveRecurrence(generator.Decimal(), (Decimal)
                generator.UInt64s()
                    .Select(u => MathM.Sqrt(u))
                    .Select(f => f - MathM.Floor(f))
                    .First(f => f > 0.3M && f < 0.7M));
        }

        /// <summary>
        /// Additive recurrence sₙ = (s₀ + αn) mod 1 with random s₀ and algebraic α of degree 2
        /// using default random number generator.
        /// </summary>
	    public static IEnumerable<Decimal> AdditiveRecurrenceM()
        {
            return AdditiveRecurrenceM(new Generator());
        }

      /// <summary>
        /// Van der Corput sequence.
        /// </summary>
        public static IEnumerable<Decimal> VanDerCorputM(int @base)
        {
            foreach (var pair in VanDerCorput(@base))
            {
                yield return pair.Item1 / (Decimal)pair.Item2;
            }
        }

        /// <summary>
        /// Halton sequence.
        /// </summary>
        public static IEnumerable<Decimal[]> HaltonM(int[] bases)
        {
            ValidateHalton(bases);
            var s = bases.Select(b => VanDerCorputM(b).GetEnumerator()).ToArray();
            var x = new Decimal[bases.Length];

            while (true)
            {
                for (int i = 0; i < bases.Length; i++)
                {
                    s[i].MoveNext();
                    x[i] = s[i].Current;
                }

                yield return x;
            }
        }

        /// <summary>
        /// Halton sequence.
        /// </summary>
        public static IEnumerable<Decimal[]> HaltonM(int dimension)
        {
            if (dimension < 0) throw new ArgumentOutOfRangeException();
            return HaltonM(Sequences.Primes().Select(_ => (int)_).Take(dimension).ToArray());
        }

        /// <summary>
        /// Hammersley set.
        /// </summary>
        public static IEnumerable<Decimal[]> HammersleyM(int[] bases, int N)
        {
            if (N < 0) throw new ArgumentOutOfRangeException();
            ValidateHalton(bases);
            var s = bases.Select(b => VanDerCorputM(b).GetEnumerator()).ToArray();
            var x = new Decimal[bases.Length + 1];

            for (int n = 1; n <= N; n++)
            {
                for (int i = 0; i < bases.Length; i++)
                {
                    s[i].MoveNext();
                    x[i] = s[i].Current;
                }

                x[bases.Length] = n / (Decimal)N;
                yield return x;
            }
        }

        /// <summary>
        /// Hammersley sequence.
        /// </summary>
        public static IEnumerable<Decimal[]> HammersleyM(int dimension, int N)
        {
            if (dimension < 2) throw new ArgumentOutOfRangeException();
            return HammersleyM(Sequences.Primes().Select(_ => (int)_).Take(dimension - 1).ToArray(), N);
        }

        /// <summary>
        /// Sobol sequence.
        /// </summary>
        public static IEnumerable<Decimal> SobolM(PolyGF2 primitivePolynomial, params int[] initialValues)
        {
            var v = SobolDirectionVectors(64, primitivePolynomial, initialValues);
            ulong s = 0;
            var d = ((System.Numerics.BigInteger)1) << 64;

            foreach (var r in Sequences.Ruler())
            {
                s ^= v[r];
                yield return s / 18446744073709551616M;
            }
        }

        /// <summary>
        /// Sobol sequence.
        /// </summary>
        public static IEnumerable<Decimal> SobolM(int polynomialCode, params int[] initialValues)
        {
            var primitivePolynomial = PolyGF2.FromCode(2UL * (ulong)polynomialCode ^ 1UL ^ (1UL << initialValues.Length));
            return SobolM(primitivePolynomial, initialValues);
        }

      /// <summary>
        /// Van der Corput sequence.
        /// </summary>
        public static IEnumerable<Rational> VanDerCorputR(int @base)
        {
            foreach (var pair in VanDerCorput(@base))
            {
                yield return pair.Item1 / (Rational)pair.Item2;
            }
        }

        /// <summary>
        /// Halton sequence.
        /// </summary>
        public static IEnumerable<Rational[]> HaltonR(int[] bases)
        {
            ValidateHalton(bases);
            var s = bases.Select(b => VanDerCorputR(b).GetEnumerator()).ToArray();
            var x = new Rational[bases.Length];

            while (true)
            {
                for (int i = 0; i < bases.Length; i++)
                {
                    s[i].MoveNext();
                    x[i] = s[i].Current;
                }

                yield return x;
            }
        }

        /// <summary>
        /// Halton sequence.
        /// </summary>
        public static IEnumerable<Rational[]> HaltonR(int dimension)
        {
            if (dimension < 0) throw new ArgumentOutOfRangeException();
            return HaltonR(Sequences.Primes().Select(_ => (int)_).Take(dimension).ToArray());
        }

        /// <summary>
        /// Hammersley set.
        /// </summary>
        public static IEnumerable<Rational[]> HammersleyR(int[] bases, int N)
        {
            if (N < 0) throw new ArgumentOutOfRangeException();
            ValidateHalton(bases);
            var s = bases.Select(b => VanDerCorputR(b).GetEnumerator()).ToArray();
            var x = new Rational[bases.Length + 1];

            for (int n = 1; n <= N; n++)
            {
                for (int i = 0; i < bases.Length; i++)
                {
                    s[i].MoveNext();
                    x[i] = s[i].Current;
                }

                x[bases.Length] = n / (Rational)N;
                yield return x;
            }
        }

        /// <summary>
        /// Hammersley sequence.
        /// </summary>
        public static IEnumerable<Rational[]> HammersleyR(int dimension, int N)
        {
            if (dimension < 2) throw new ArgumentOutOfRangeException();
            return HammersleyR(Sequences.Primes().Select(_ => (int)_).Take(dimension - 1).ToArray(), N);
        }

        /// <summary>
        /// Sobol sequence.
        /// </summary>
        public static IEnumerable<Rational> SobolR(PolyGF2 primitivePolynomial, params int[] initialValues)
        {
            var v = SobolDirectionVectors(64, primitivePolynomial, initialValues);
            ulong s = 0;
            var d = ((System.Numerics.BigInteger)1) << 64;

            foreach (var r in Sequences.Ruler())
            {
                s ^= v[r];
                yield return new Rational(s, d);
            }
        }

        /// <summary>
        /// Sobol sequence.
        /// </summary>
        public static IEnumerable<Rational> SobolR(int polynomialCode, params int[] initialValues)
        {
            var primitivePolynomial = PolyGF2.FromCode(2UL * (ulong)polynomialCode ^ 1UL ^ (1UL << initialValues.Length));
            return SobolR(primitivePolynomial, initialValues);
        }

        private static IEnumerable<Tuple<long, long>> VanDerCorput(int @base)
        {
            if (@base < 2) throw new ArgumentOutOfRangeException();
            var digits = new int[1];
            long denom = @base;

            while (true)
            {
                int i = 0;
                long num = 0;

                while (true)
                {
                    num = num * @base + ++digits[i];
                    if (digits[i] < @base) break;
                    digits[i] = 0;
                    num -= @base;
                    i++;

                    if (i >= digits.Length)
                    {
                        Array.Resize(ref digits, digits.Length + 1);
                        denom *= @base;
                    }
                }

                while (++i < digits.Length)
                {
                    num = num * @base + digits[i];
                }

                yield return Tuple.Create(num, denom);
            }
        }

        private static void ValidateHalton(int[] bases)
        {
            if (bases == null) throw new ArgumentNullException();
            if (bases.Length == 0) throw new ArgumentException();

            for (int i = 1; i < bases.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int gcd = Numbers.GCD(bases[i], bases[j]);

                    if (gcd != 1)
                    {
                        throw new ArgumentException($"Values must be coprime. {bases[i]} and {bases[j]} share a factor of {gcd}.");
                    }
                }
            }
        }

        private static void ValidateHammersley(int[] bases)
        {
            if (bases == null) throw new ArgumentNullException();
            if (bases.Length == 0) throw new ArgumentException();

            for (int i = 1; i < bases.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int gcd = Numbers.GCD(bases[i], bases[j]);

                    if (gcd != 1)
                    {
                        throw new ArgumentException($"Values must be coprime. {bases[i]} and {bases[j]} share a factor of {gcd}.");
                    }
                }
            }
        }

        public static ulong[] SobolDirectionVectors(int d, PolyGF2 primitivePolynomial, int[] initialValues)
        {
            if (initialValues == null) 
                throw new ArgumentNullException(nameof(initialValues));

            int n = initialValues.Length;
            if (n != primitivePolynomial.Degree) 
                throw new ArgumentException("The number of initial values must be the same as the degree of the primitive polynomial.");

            PolyGF2 factor;
            long order;

            if (!primitivePolynomial.IsPrimitive(out factor, out order)) 
            {
                ulong q = (1UL << primitivePolynomial.Degree) - 1UL;
                var o = new PolyGF2((int)order, 0);

                if (factor != PolyGF2.Zero)
                    throw new ArgumentException($"Polynomial is not primitive because it is divisible by {factor} = ({primitivePolynomial}) / ({primitivePolynomial / factor}).");
                else
                    throw new ArgumentException($"Polynomial is not primitive because it only has order {order} and not {q}. ({o}) / ({primitivePolynomial}) = ({o / primitivePolynomial})");
            }

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

            // Compute remaining direction vectors.
            var exponents = new HashSet<int>(primitivePolynomial.Exponents);

            for (int i = n; i < d; i++)
            {
                ulong sum = v[i - n] >> n;

                for (int j = 1; j <= n; j++)
                {
                    if (exponents.Contains(n - j))
                    {
                        sum ^= v[i - j];
                    }
                }

                v[i] = sum;
            }

            return v;
        }
    }
}
