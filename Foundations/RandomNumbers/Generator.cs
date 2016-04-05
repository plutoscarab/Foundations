
/*
Generator.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Foundations.Types;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// Pseudo-random number generator.
    /// </summary>
    public sealed partial class Generator
    {
        /// <summary>
        /// A delegate that creates a <see cref="IRandomSource"/> with a <see cref="IRandomSource.AllocateState"/>
        /// method implementation that returns null. This source is used to generate initial state data
        /// from raw seed data for any <see cref="IRandomSource"/> that returns a non-null value
        /// from <see cref="IRandomSource.AllocateState"/>.
        /// </summary>
        public static Func<IRandomSource> DefaultStateInitializationFactory = delegate
        {
            return new SHA256RandomSource();
        };

        /// <summary>
        /// A delegate that creates a <see cref="IRandomSource"/>. This is the source that
        /// will be used when a <see cref="Generator"/> is created using a constructor that
        /// does not specify a <see cref="IRandomSource"/>.
        /// </summary>
        public static Func<IRandomSource> DefaultSourceFactory = delegate
        {
            return new XorShiftRandomSource();
        };

        /// <summary>
        /// Implicitly cast a <see cref="System.Random"/> to a <see cref="Generator"/>
        /// to enable using System.Random values as arguments to Foundations library
        /// functions that require Generator.
        /// </summary>
        public static implicit operator Generator(System.Random random)
        {
            return new Generator(new SystemRandomSource(random));
        }
        
        private IRandomSource source;
     
        /// <summary>
        /// Create a pseudo-random number generator initialized using system entropy
        /// and default random source.
        /// </summary>
        public Generator()
            : this(null, (byte[])null)
        {
        }
     
        /// <summary>
        /// Create a pseudo-random number generator initialized using system entropy
        /// and specified random source.
        /// </summary>
        public Generator(IRandomSource source)
            : this(source, (byte[])null)
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided seed data
        /// and default random source.
        /// </summary>
        public Generator(params byte[] seed)
            : this(null, seed)
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided seed data
        /// and specified random source.
        /// </summary>
        public Generator(IRandomSource source, params byte[] seed)
        {
            if (source == null)
            {
                source = DefaultSourceFactory();
            }

            this.source = source;
            var state = source.AllocateState();

            if (seed == null)
            {
                if (state == null)
                {
                    throw new ArgumentException("The specified IRandomSource cannot be used without a seed.", nameof(source));
                }

                seed = new byte[Buffer.ByteLength(state)];

                using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
                {
                    rng.GetBytes(seed);
                }
            }

            if (state == null)
            {
                state = seed;
            }
            else
            {
                var stateSource = DefaultStateInitializationFactory();
                CreateState(stateSource, seed, state);
                var dispose = stateSource as IDisposable;
                if (dispose != null) dispose.Dispose();
            }

            source.Initialize(state);
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params ulong[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params ulong[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params long[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params long[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params uint[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params uint[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params int[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params int[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params ushort[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params ushort[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params short[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params short[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params sbyte[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params sbyte[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params char[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params char[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params float[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params float[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params double[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params double[] seed)
            : this(null, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator seeded with string contents.
        /// </summary>
        public Generator(IRandomSource source, string seed)
            : this(source, seed.ToCharArray().GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator seeded with string contents.
        /// </summary>
        public Generator(string seed)
            : this(null, seed.ToCharArray().GetBytes())
        {
        }

        /// <summary>
        /// Get a random <see cref="System.UInt64"/> value.
        /// </summary>
        public UInt64 UInt64()
        {
            return (UInt64)source.Next();
        }

        /// <summary>
        /// Get a random <see cref="System.UInt64"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public UInt64 UInt64(UInt64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            var mask = (UInt64)(range - 1);
            mask |= (UInt64)(mask >> 1);
            mask |= (UInt64)(mask >> 2);
            mask |= (UInt64)(mask >> 4);
            mask |= (UInt64)(mask >> 8);
            mask |= (UInt64)(mask >> 16);
            mask |= (UInt64)(mask >> 32);
            UInt64 sample;

            do
            {
                sample = (UInt64)(UInt64() & (UInt64)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Get a random <see cref="System.UInt64"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public UInt64 UInt64(UInt64 minimum, UInt64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.UInt64.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (UInt64)(minimum + UInt64(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.UInt64"/> values.
        /// </summary>
        public void Fill(UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.UInt64"/> values.
        /// </summary>
        public void Fill(UInt64 range, UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.UInt64"/> values.
        /// </summary>
        public void Fill(UInt64 minimum, UInt64 range, UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.UInt64"/> values.
        /// </summary>
        unsafe public void Fill(UInt64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (UInt64* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 1)
                {
                    *p++ = source.Next();
                    count -= 1;
                }
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.UInt64"/> values.
        /// </summary>
        public void Fill(UInt64 minimum, UInt64 range, UInt64[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.UInt64.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = UInt64(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of UInt64 values.
        /// </summary>
        public IEnumerable<UInt64> UInt64s()
        {
            while(true)
            {
                yield return UInt64();
            }
        }

        /// <summary>
        /// Gets a sequence of UInt64 values in [0, range).
        /// </summary>
        public IEnumerable<UInt64> UInt64s(UInt64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return UInt64(range);
            }
        }

        /// <summary>
        /// Gets a sequence of UInt64 values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<UInt64> UInt64s(UInt64 minimum, UInt64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.UInt64.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return UInt64(minimum, range);
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, UInt64[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.Int64"/> value.
        /// </summary>
        public Int64 Int64()
        {
            return (Int64)source.Next();
        }

        /// <summary>
        /// Get a random non-negative <see cref="System.Int64"/> value.
        /// </summary>
        public Int64 Int64NonNegative()
        {
            return (Int64)(Int64() & 0x7FFFFFFFFFFFFFFF);
        }

        /// <summary>
        /// Get a random <see cref="System.Int64"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Int64 Int64(Int64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            var mask = (UInt64)(range - 1);
            mask |= (UInt64)(mask >> 1);
            mask |= (UInt64)(mask >> 2);
            mask |= (UInt64)(mask >> 4);
            mask |= (UInt64)(mask >> 8);
            mask |= (UInt64)(mask >> 16);
            mask |= (UInt64)(mask >> 32);
            Int64 sample;

            do
            {
                sample = (Int64)(Int64() & (Int64)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Get a random <see cref="System.Int64"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public Int64 Int64(Int64 minimum, Int64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Int64.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Int64)(minimum + Int64(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Int64"/> values.
        /// </summary>
        public void Fill(Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Int64"/> values.
        /// </summary>
        public void Fill(Int64 range, Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Int64"/> values.
        /// </summary>
        public void Fill(Int64 minimum, Int64 range, Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Int64"/> values.
        /// </summary>
        unsafe public void Fill(Int64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (Int64* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 1)
                {
                    *p++ = source.Next();
                    count -= 1;
                }
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Int64"/> values.
        /// </summary>
        public void Fill(Int64 minimum, Int64 range, Int64[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Int64.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = Int64(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of Int64 values.
        /// </summary>
        public IEnumerable<Int64> Int64s()
        {
            while(true)
            {
                yield return Int64();
            }
        }

        /// <summary>
        /// Gets a sequence of Int64 values in [0, range).
        /// </summary>
        public IEnumerable<Int64> Int64s(Int64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Int64(range);
            }
        }

        /// <summary>
        /// Gets a sequence of Int64 values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<Int64> Int64s(Int64 minimum, Int64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Int64.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Int64(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of non-negative Int64 values.
        /// </summary>
        public IEnumerable<Int64> Int64sNonNegative()
        {
            while(true)
            {
                yield return Int64NonNegative();
            }
        }

        /// <summary>
        /// Fill a provided array with non-negative random <see cref="System.Int64"/> values.
        /// </summary>
        public void FillNonNegative(Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            FillNonNegative(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with non-negative random <see cref="System.Int64"/> values.
        /// </summary>
        public void FillNonNegative(Int64[] array, int offset, int count)
        {
            Fill(array, offset, count);

            while (count-- > 0)
            {
                array[offset++] &= 0x7FFFFFFFFFFFFFFF;
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, Int64[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.UInt32"/> value.
        /// </summary>
        public UInt32 UInt32()
        {
            return (UInt32)source.Next();
        }

        /// <summary>
        /// Get a random <see cref="System.UInt32"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public UInt32 UInt32(UInt32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            var mask = (UInt32)(range - 1);
            mask |= (UInt32)(mask >> 1);
            mask |= (UInt32)(mask >> 2);
            mask |= (UInt32)(mask >> 4);
            mask |= (UInt32)(mask >> 8);
            mask |= (UInt32)(mask >> 16);
            UInt32 sample;

            do
            {
                sample = (UInt32)(UInt32() & (UInt32)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Get a random <see cref="System.UInt32"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public UInt32 UInt32(UInt32 minimum, UInt32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.UInt32.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (UInt32)(minimum + UInt32(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.UInt32"/> values.
        /// </summary>
        public void Fill(UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.UInt32"/> values.
        /// </summary>
        public void Fill(UInt32 range, UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.UInt32"/> values.
        /// </summary>
        public void Fill(UInt32 minimum, UInt32 range, UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.UInt32"/> values.
        /// </summary>
        unsafe public void Fill(UInt32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (UInt32* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 2)
                {
                    *p++ = source.Next();
                    count -= 2;
                }

                if (count == 0)
                    return;

                ulong sample = source.Next();
                var p1 = (UInt32*)p;
                var p2 = (UInt32*)&sample;
                *p1 = *p2;
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.UInt32"/> values.
        /// </summary>
        public void Fill(UInt32 minimum, UInt32 range, UInt32[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.UInt32.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = UInt32(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of UInt32 values.
        /// </summary>
        public IEnumerable<UInt32> UInt32s()
        {
            while(true)
            {
                yield return UInt32();
            }
        }

        /// <summary>
        /// Gets a sequence of UInt32 values in [0, range).
        /// </summary>
        public IEnumerable<UInt32> UInt32s(UInt32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return UInt32(range);
            }
        }

        /// <summary>
        /// Gets a sequence of UInt32 values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<UInt32> UInt32s(UInt32 minimum, UInt32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.UInt32.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return UInt32(minimum, range);
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, UInt32[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.Int32"/> value.
        /// </summary>
        public Int32 Int32()
        {
            return (Int32)source.Next();
        }

        /// <summary>
        /// Get a random non-negative <see cref="System.Int32"/> value.
        /// </summary>
        public Int32 Int32NonNegative()
        {
            return (Int32)(Int32() & 0x7FFFFFFF);
        }

        /// <summary>
        /// Get a random <see cref="System.Int32"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Int32 Int32(Int32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            var mask = (UInt32)(range - 1);
            mask |= (UInt32)(mask >> 1);
            mask |= (UInt32)(mask >> 2);
            mask |= (UInt32)(mask >> 4);
            mask |= (UInt32)(mask >> 8);
            mask |= (UInt32)(mask >> 16);
            Int32 sample;

            do
            {
                sample = (Int32)(Int32() & (Int32)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Get a random <see cref="System.Int32"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public Int32 Int32(Int32 minimum, Int32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Int32.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Int32)(minimum + Int32(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Int32"/> values.
        /// </summary>
        public void Fill(Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Int32"/> values.
        /// </summary>
        public void Fill(Int32 range, Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Int32"/> values.
        /// </summary>
        public void Fill(Int32 minimum, Int32 range, Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Int32"/> values.
        /// </summary>
        unsafe public void Fill(Int32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (Int32* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 2)
                {
                    *p++ = source.Next();
                    count -= 2;
                }

                if (count == 0)
                    return;

                ulong sample = source.Next();
                var p1 = (Int32*)p;
                var p2 = (Int32*)&sample;
                *p1 = *p2;
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Int32"/> values.
        /// </summary>
        public void Fill(Int32 minimum, Int32 range, Int32[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Int32.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = Int32(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of Int32 values.
        /// </summary>
        public IEnumerable<Int32> Int32s()
        {
            while(true)
            {
                yield return Int32();
            }
        }

        /// <summary>
        /// Gets a sequence of Int32 values in [0, range).
        /// </summary>
        public IEnumerable<Int32> Int32s(Int32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Int32(range);
            }
        }

        /// <summary>
        /// Gets a sequence of Int32 values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<Int32> Int32s(Int32 minimum, Int32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Int32.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Int32(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of non-negative Int32 values.
        /// </summary>
        public IEnumerable<Int32> Int32sNonNegative()
        {
            while(true)
            {
                yield return Int32NonNegative();
            }
        }

        /// <summary>
        /// Fill a provided array with non-negative random <see cref="System.Int32"/> values.
        /// </summary>
        public void FillNonNegative(Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            FillNonNegative(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with non-negative random <see cref="System.Int32"/> values.
        /// </summary>
        public void FillNonNegative(Int32[] array, int offset, int count)
        {
            Fill(array, offset, count);

            while (count-- > 0)
            {
                array[offset++] &= 0x7FFFFFFF;
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, Int32[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.UInt16"/> value.
        /// </summary>
        public UInt16 UInt16()
        {
            return (UInt16)source.Next();
        }

        /// <summary>
        /// Get a random <see cref="System.UInt16"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public UInt16 UInt16(UInt16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            var mask = (UInt16)(range - 1);
            mask |= (UInt16)(mask >> 1);
            mask |= (UInt16)(mask >> 2);
            mask |= (UInt16)(mask >> 4);
            mask |= (UInt16)(mask >> 8);
            UInt16 sample;

            do
            {
                sample = (UInt16)(UInt16() & (UInt16)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Get a random <see cref="System.UInt16"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public UInt16 UInt16(UInt16 minimum, UInt16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.UInt16.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (UInt16)(minimum + UInt16(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.UInt16"/> values.
        /// </summary>
        public void Fill(UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.UInt16"/> values.
        /// </summary>
        public void Fill(UInt16 range, UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.UInt16"/> values.
        /// </summary>
        public void Fill(UInt16 minimum, UInt16 range, UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.UInt16"/> values.
        /// </summary>
        unsafe public void Fill(UInt16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (UInt16* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 4)
                {
                    *p++ = source.Next();
                    count -= 4;
                }

                if (count == 0)
                    return;

                ulong sample = source.Next();
                var p1 = (UInt16*)p;
                var p2 = (UInt16*)&sample;

                while (count-- > 0)
                {
                    *p1++ = *p2++;
                }
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.UInt16"/> values.
        /// </summary>
        public void Fill(UInt16 minimum, UInt16 range, UInt16[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.UInt16.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = UInt16(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of UInt16 values.
        /// </summary>
        public IEnumerable<UInt16> UInt16s()
        {
            while(true)
            {
                yield return UInt16();
            }
        }

        /// <summary>
        /// Gets a sequence of UInt16 values in [0, range).
        /// </summary>
        public IEnumerable<UInt16> UInt16s(UInt16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return UInt16(range);
            }
        }

        /// <summary>
        /// Gets a sequence of UInt16 values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<UInt16> UInt16s(UInt16 minimum, UInt16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.UInt16.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return UInt16(minimum, range);
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, UInt16[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.Int16"/> value.
        /// </summary>
        public Int16 Int16()
        {
            return (Int16)source.Next();
        }

        /// <summary>
        /// Get a random non-negative <see cref="System.Int16"/> value.
        /// </summary>
        public Int16 Int16NonNegative()
        {
            return (Int16)(Int16() & 0x7FFF);
        }

        /// <summary>
        /// Get a random <see cref="System.Int16"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Int16 Int16(Int16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            var mask = (UInt16)(range - 1);
            mask |= (UInt16)(mask >> 1);
            mask |= (UInt16)(mask >> 2);
            mask |= (UInt16)(mask >> 4);
            mask |= (UInt16)(mask >> 8);
            Int16 sample;

            do
            {
                sample = (Int16)(Int16() & (Int16)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Get a random <see cref="System.Int16"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public Int16 Int16(Int16 minimum, Int16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Int16.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Int16)(minimum + Int16(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Int16"/> values.
        /// </summary>
        public void Fill(Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Int16"/> values.
        /// </summary>
        public void Fill(Int16 range, Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Int16"/> values.
        /// </summary>
        public void Fill(Int16 minimum, Int16 range, Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Int16"/> values.
        /// </summary>
        unsafe public void Fill(Int16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (Int16* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 4)
                {
                    *p++ = source.Next();
                    count -= 4;
                }

                if (count == 0)
                    return;

                ulong sample = source.Next();
                var p1 = (Int16*)p;
                var p2 = (Int16*)&sample;

                while (count-- > 0)
                {
                    *p1++ = *p2++;
                }
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Int16"/> values.
        /// </summary>
        public void Fill(Int16 minimum, Int16 range, Int16[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Int16.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = Int16(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of Int16 values.
        /// </summary>
        public IEnumerable<Int16> Int16s()
        {
            while(true)
            {
                yield return Int16();
            }
        }

        /// <summary>
        /// Gets a sequence of Int16 values in [0, range).
        /// </summary>
        public IEnumerable<Int16> Int16s(Int16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Int16(range);
            }
        }

        /// <summary>
        /// Gets a sequence of Int16 values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<Int16> Int16s(Int16 minimum, Int16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Int16.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Int16(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of non-negative Int16 values.
        /// </summary>
        public IEnumerable<Int16> Int16sNonNegative()
        {
            while(true)
            {
                yield return Int16NonNegative();
            }
        }

        /// <summary>
        /// Fill a provided array with non-negative random <see cref="System.Int16"/> values.
        /// </summary>
        public void FillNonNegative(Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            FillNonNegative(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with non-negative random <see cref="System.Int16"/> values.
        /// </summary>
        public void FillNonNegative(Int16[] array, int offset, int count)
        {
            Fill(array, offset, count);

            while (count-- > 0)
            {
                array[offset++] &= 0x7FFF;
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, Int16[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.Byte"/> value.
        /// </summary>
        public Byte Byte()
        {
            return (Byte)source.Next();
        }

        /// <summary>
        /// Get a random <see cref="System.Byte"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Byte Byte(Byte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            var mask = (Byte)(range - 1);
            mask |= (Byte)(mask >> 1);
            mask |= (Byte)(mask >> 2);
            mask |= (Byte)(mask >> 4);
            Byte sample;

            do
            {
                sample = (Byte)(Byte() & (Byte)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Get a random <see cref="System.Byte"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public Byte Byte(Byte minimum, Byte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Byte.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Byte)(minimum + Byte(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Byte"/> values.
        /// </summary>
        public void Fill(Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Byte"/> values.
        /// </summary>
        public void Fill(Byte range, Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Byte"/> values.
        /// </summary>
        public void Fill(Byte minimum, Byte range, Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Byte"/> values.
        /// </summary>
        unsafe public void Fill(Byte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (Byte* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 8)
                {
                    *p++ = source.Next();
                    count -= 8;
                }

                if (count == 0)
                    return;

                ulong sample = source.Next();
                var p1 = (Byte*)p;
                var p2 = (Byte*)&sample;

                while (count-- > 0)
                {
                    *p1++ = *p2++;
                }
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Byte"/> values.
        /// </summary>
        public void Fill(Byte minimum, Byte range, Byte[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Byte.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = Byte(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of Byte values.
        /// </summary>
        public IEnumerable<Byte> Bytes()
        {
            while(true)
            {
                yield return Byte();
            }
        }

        /// <summary>
        /// Gets a sequence of Byte values in [0, range).
        /// </summary>
        public IEnumerable<Byte> Bytes(Byte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Byte(range);
            }
        }

        /// <summary>
        /// Gets a sequence of Byte values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<Byte> Bytes(Byte minimum, Byte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Byte.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Byte(minimum, range);
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, Byte[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.SByte"/> value.
        /// </summary>
        public SByte SByte()
        {
            return (SByte)source.Next();
        }

        /// <summary>
        /// Get a random non-negative <see cref="System.SByte"/> value.
        /// </summary>
        public SByte SByteNonNegative()
        {
            return (SByte)(SByte() & 0x7F);
        }

        /// <summary>
        /// Get a random <see cref="System.SByte"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public SByte SByte(SByte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            var mask = (Byte)(range - 1);
            mask |= (Byte)(mask >> 1);
            mask |= (Byte)(mask >> 2);
            mask |= (Byte)(mask >> 4);
            SByte sample;

            do
            {
                sample = (SByte)(SByte() & (SByte)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Get a random <see cref="System.SByte"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public SByte SByte(SByte minimum, SByte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.SByte.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (SByte)(minimum + SByte(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.SByte"/> values.
        /// </summary>
        public void Fill(SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.SByte"/> values.
        /// </summary>
        public void Fill(SByte range, SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.SByte"/> values.
        /// </summary>
        public void Fill(SByte minimum, SByte range, SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.SByte"/> values.
        /// </summary>
        unsafe public void Fill(SByte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (SByte* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 8)
                {
                    *p++ = source.Next();
                    count -= 8;
                }

                if (count == 0)
                    return;

                ulong sample = source.Next();
                var p1 = (SByte*)p;
                var p2 = (SByte*)&sample;

                while (count-- > 0)
                {
                    *p1++ = *p2++;
                }
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.SByte"/> values.
        /// </summary>
        public void Fill(SByte minimum, SByte range, SByte[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.SByte.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = SByte(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of SByte values.
        /// </summary>
        public IEnumerable<SByte> SBytes()
        {
            while(true)
            {
                yield return SByte();
            }
        }

        /// <summary>
        /// Gets a sequence of SByte values in [0, range).
        /// </summary>
        public IEnumerable<SByte> SBytes(SByte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return SByte(range);
            }
        }

        /// <summary>
        /// Gets a sequence of SByte values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<SByte> SBytes(SByte minimum, SByte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.SByte.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return SByte(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of non-negative SByte values.
        /// </summary>
        public IEnumerable<SByte> SBytesNonNegative()
        {
            while(true)
            {
                yield return SByteNonNegative();
            }
        }

        /// <summary>
        /// Fill a provided array with non-negative random <see cref="System.SByte"/> values.
        /// </summary>
        public void FillNonNegative(SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            FillNonNegative(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with non-negative random <see cref="System.SByte"/> values.
        /// </summary>
        public void FillNonNegative(SByte[] array, int offset, int count)
        {
            Fill(array, offset, count);

            while (count-- > 0)
            {
                array[offset++] &= 0x7F;
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, SByte[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.Double"/> value.
        /// </summary>
        public Double Double()
        {
            return (Double)(BitConverter.Int64BitsToDouble((long)(source.Next() & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
        }

        /// <summary>
        /// Get a random <see cref="System.Double"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Double Double(Double range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return range * Double();
        }

        /// <summary>
        /// Get a random <see cref="System.Double"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public Double Double(Double minimum, Double range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Double.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Double)(minimum + Double(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Double"/> values.
        /// </summary>
        public void Fill(Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Double"/> values.
        /// </summary>
        public void Fill(Double range, Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Double"/> values.
        /// </summary>
        public void Fill(Double minimum, Double range, Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Double"/> values.
        /// </summary>
        unsafe public void Fill(Double[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (Double* ptr = &array[offset])
            {
                var p = (ulong*)ptr;
                var f = ptr;

                while (count >= 1)
                {
                    *p++ = (source.Next() & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000;
                    *f++ -= 1d;
                    count -= 1;
                }
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Double"/> values.
        /// </summary>
        public void Fill(Double minimum, Double range, Double[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Double.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = Double(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of Double values.
        /// </summary>
        public IEnumerable<Double> Doubles()
        {
            while(true)
            {
                yield return Double();
            }
        }

        /// <summary>
        /// Gets a sequence of Double values in [0, range).
        /// </summary>
        public IEnumerable<Double> Doubles(Double range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Double(range);
            }
        }

        /// <summary>
        /// Gets a sequence of Double values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<Double> Doubles(Double minimum, Double range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Double.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Double(minimum, range);
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, Double[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.Single"/> value.
        /// </summary>
        public Single Single()
        {
            return (Single)(BitConverter.Int64BitsToDouble((long)(source.Next() & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
        }

        /// <summary>
        /// Get a random <see cref="System.Single"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Single Single(Single range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return range * Single();
        }

        /// <summary>
        /// Get a random <see cref="System.Single"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public Single Single(Single minimum, Single range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Single.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Single)(minimum + Single(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Single"/> values.
        /// </summary>
        public void Fill(Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Single"/> values.
        /// </summary>
        public void Fill(Single range, Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Single"/> values.
        /// </summary>
        public void Fill(Single minimum, Single range, Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Single"/> values.
        /// </summary>
        unsafe public void Fill(Single[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (Single* ptr = &array[offset])
            {
                var p = (ulong*)ptr;
                var f = ptr;

                while (count >= 2)
                {
                    *p++ = (source.Next() & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
                    *f++ -= 1f;
                    *f++ -= 1f;
                    count -= 2;
                }

                if (count == 0)
                    return;

                ulong sample = (source.Next() & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
                var p1 = (Single*)p;
                var p2 = (Single*)&sample;
                *p1 = *p2 - 1f;
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Single"/> values.
        /// </summary>
        public void Fill(Single minimum, Single range, Single[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Single.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = Single(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of Single values.
        /// </summary>
        public IEnumerable<Single> Singles()
        {
            while(true)
            {
                yield return Single();
            }
        }

        /// <summary>
        /// Gets a sequence of Single values in [0, range).
        /// </summary>
        public IEnumerable<Single> Singles(Single range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Single(range);
            }
        }

        /// <summary>
        /// Gets a sequence of Single values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<Single> Singles(Single minimum, Single range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Single.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Single(minimum, range);
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, Single[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        /// <summary>
        /// Get a random <see cref="System.Decimal"/> value.
        /// </summary>
        public Decimal Decimal()
        {
            while(true)
            {
                ulong u = source.Next();
                uint i = (uint)source.Next();
                var d = new decimal((int)(i >> 2), (int)u, (int)(u >> 32), false, 28);
                if (d < 1m) return d;
            }
        }

        /// <summary>
        /// Get a random <see cref="System.Decimal"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Decimal Decimal(Decimal range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return range * Decimal();
        }

        /// <summary>
        /// Get a random <see cref="System.Decimal"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public Decimal Decimal(Decimal minimum, Decimal range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Decimal.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Decimal)(minimum + Decimal(range));
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Decimal"/> values.
        /// </summary>
        public void Fill(Decimal[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Decimal"/> values.
        /// </summary>
        public void Fill(Decimal range, Decimal[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Decimal"/> values.
        /// </summary>
        public void Fill(Decimal minimum, Decimal range, Decimal[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Decimal"/> values.
        /// </summary>
        unsafe public void Fill(Decimal[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = Decimal();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Decimal"/> values.
        /// </summary>
        public void Fill(Decimal minimum, Decimal range, Decimal[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Decimal.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = Decimal(minimum, range);
            }
        }

        /// <summary>
        /// Gets a sequence of Decimal values.
        /// </summary>
        public IEnumerable<Decimal> Decimals()
        {
            while(true)
            {
                yield return Decimal();
            }
        }

        /// <summary>
        /// Gets a sequence of Decimal values in [0, range).
        /// </summary>
        public IEnumerable<Decimal> Decimals(Decimal range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Decimal(range);
            }
        }

        /// <summary>
        /// Gets a sequence of Decimal values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<Decimal> Decimals(Decimal minimum, Decimal range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (System.Decimal.MaxValue - range < minimum)
                throw new ArgumentOutOfRangeException(nameof(range));

            while(true)
            {
                yield return Decimal(minimum, range);
            }
        }

        private static void CreateState(IRandomSource source, byte[] seed, Decimal[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.Fill(state);
        }

        private static void CreateState(IRandomSource source, byte[] seed, Array state)
        {
            switch (Type.GetTypeCode(state.GetType().GetElementType()))
            {
                case TypeCode.UInt64:
                    CreateState(source, seed, state as UInt64[]);
                    break;

                case TypeCode.Int64:
                    CreateState(source, seed, state as Int64[]);
                    break;

                case TypeCode.UInt32:
                    CreateState(source, seed, state as UInt32[]);
                    break;

                case TypeCode.Int32:
                    CreateState(source, seed, state as Int32[]);
                    break;

                case TypeCode.UInt16:
                    CreateState(source, seed, state as UInt16[]);
                    break;

                case TypeCode.Int16:
                    CreateState(source, seed, state as Int16[]);
                    break;

                case TypeCode.Byte:
                    CreateState(source, seed, state as Byte[]);
                    break;

                case TypeCode.SByte:
                    CreateState(source, seed, state as SByte[]);
                    break;

                case TypeCode.Double:
                    CreateState(source, seed, state as Double[]);
                    break;

                case TypeCode.Single:
                    CreateState(source, seed, state as Single[]);
                    break;

                case TypeCode.Decimal:
                    CreateState(source, seed, state as Decimal[]);
                    break;

                default:
                    throw new NotSupportedException("Unsupported array type.");
            }
        }
    }
}
