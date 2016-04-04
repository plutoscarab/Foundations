
/*
Generator.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
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
            : this(DefaultSourceFactory(), seed.GetBytes())
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
            : this(DefaultSourceFactory(), seed.GetBytes())
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
            : this(DefaultSourceFactory(), seed.GetBytes())
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
            : this(DefaultSourceFactory(), seed.GetBytes())
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
            : this(DefaultSourceFactory(), seed.GetBytes())
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
            : this(DefaultSourceFactory(), seed.GetBytes())
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
            : this(DefaultSourceFactory(), seed.GetBytes())
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
            : this(DefaultSourceFactory(), seed.GetBytes())
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
            : this(DefaultSourceFactory(), seed.GetBytes())
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
            : this(DefaultSourceFactory(), seed.GetBytes())
        {
        }

#pragma warning disable 0169
        private ValueUnion sample;
#pragma warning restore 0169

        /// <summary>
        /// Generate the next 64 bits.
        /// </summary>
        private ulong Mix()
        {
            return sample.UInt64_0 = source.Next();
        }

        /// <summary>
        /// Get a random <see cref="System.UInt64"/> value.
        /// </summary>
        public UInt64 UInt64()
        {
            return (UInt64)Mix();
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
        public void Fill(UInt64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 1)
            {
                Mix();
                array[offset++] = sample.UInt64_0;
                count -= 1;
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
            return (Int64)Mix();
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
        public void Fill(Int64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 1)
            {
                Mix();
                array[offset++] = sample.Int64_0;
                count -= 1;
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
            return (UInt32)Mix();
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
        public void Fill(UInt32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 2)
            {
                Mix();
                array[offset++] = sample.UInt32_1;
                array[offset++] = sample.UInt32_0;
                count -= 2;
            }

            if (count == 0)
                return;

            Mix();
             
            array[offset++] = sample.UInt32_1;
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
            return (Int32)Mix();
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
        public void Fill(Int32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 2)
            {
                Mix();
                array[offset++] = sample.Int32_1;
                array[offset++] = sample.Int32_0;
                count -= 2;
            }

            if (count == 0)
                return;

            Mix();
             
            array[offset++] = sample.Int32_1;
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
            return (UInt16)Mix();
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
        public void Fill(UInt16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 4)
            {
                Mix();
                array[offset++] = sample.UInt16_3;
                array[offset++] = sample.UInt16_2;
                array[offset++] = sample.UInt16_1;
                array[offset++] = sample.UInt16_0;
                count -= 4;
            }

            if (count == 0)
                return;

            Mix();

            switch (count)
            {
                case 3: array[offset++] = sample.UInt16_3; goto case 2;
                case 2: array[offset++] = sample.UInt16_2; goto case 1;
                case 1: array[offset++] = sample.UInt16_1; break;
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
            return (Int16)Mix();
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
        public void Fill(Int16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 4)
            {
                Mix();
                array[offset++] = sample.Int16_3;
                array[offset++] = sample.Int16_2;
                array[offset++] = sample.Int16_1;
                array[offset++] = sample.Int16_0;
                count -= 4;
            }

            if (count == 0)
                return;

            Mix();

            switch (count)
            {
                case 3: array[offset++] = sample.Int16_3; goto case 2;
                case 2: array[offset++] = sample.Int16_2; goto case 1;
                case 1: array[offset++] = sample.Int16_1; break;
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
            return (Byte)Mix();
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
        public void Fill(Byte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 8)
            {
                Mix();
                array[offset++] = sample.Byte_7;
                array[offset++] = sample.Byte_6;
                array[offset++] = sample.Byte_5;
                array[offset++] = sample.Byte_4;
                array[offset++] = sample.Byte_3;
                array[offset++] = sample.Byte_2;
                array[offset++] = sample.Byte_1;
                array[offset++] = sample.Byte_0;
                count -= 8;
            }

            if (count == 0)
                return;

            Mix();

            switch (count)
            {
                case 7: array[offset++] = sample.Byte_7; goto case 6;
                case 6: array[offset++] = sample.Byte_6; goto case 5;
                case 5: array[offset++] = sample.Byte_5; goto case 4;
                case 4: array[offset++] = sample.Byte_4; goto case 3;
                case 3: array[offset++] = sample.Byte_3; goto case 2;
                case 2: array[offset++] = sample.Byte_2; goto case 1;
                case 1: array[offset++] = sample.Byte_1; break;
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
            return (SByte)Mix();
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
        public void Fill(SByte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 8)
            {
                Mix();
                array[offset++] = sample.SByte_7;
                array[offset++] = sample.SByte_6;
                array[offset++] = sample.SByte_5;
                array[offset++] = sample.SByte_4;
                array[offset++] = sample.SByte_3;
                array[offset++] = sample.SByte_2;
                array[offset++] = sample.SByte_1;
                array[offset++] = sample.SByte_0;
                count -= 8;
            }

            if (count == 0)
                return;

            Mix();

            switch (count)
            {
                case 7: array[offset++] = sample.SByte_7; goto case 6;
                case 6: array[offset++] = sample.SByte_6; goto case 5;
                case 5: array[offset++] = sample.SByte_5; goto case 4;
                case 4: array[offset++] = sample.SByte_4; goto case 3;
                case 3: array[offset++] = sample.SByte_3; goto case 2;
                case 2: array[offset++] = sample.SByte_2; goto case 1;
                case 1: array[offset++] = sample.SByte_1; break;
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
            return (Double)(BitConverter.Int64BitsToDouble((long)(Mix() & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
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
        public void Fill(Double[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 1)
            {
                Mix();
                sample.UInt64_0 = (sample.UInt64_0 & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000;
                sample.Double_0 -= 1d;
                array[offset++] = sample.Double_0;
                count -= 1;
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
            return (Single)(BitConverter.Int64BitsToDouble((long)(Mix() & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
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
        public void Fill(Single[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 2)
            {
                Mix();
                sample.UInt32_1 = (sample.UInt32_1 & 0x007FFFFF) | 0x3F800000;
                sample.Single_1 -= 1f;
                array[offset++] = sample.Single_1;
                sample.UInt32_0 = (sample.UInt32_0 & 0x007FFFFF) | 0x3F800000;
                sample.Single_0 -= 1f;
                array[offset++] = sample.Single_0;
                count -= 2;
            }

            if (count == 0)
                return;

            Mix();
                            sample.UInt32_1 = (sample.UInt32_1 & 0x007FFFFF) | 0x3F800000;
                sample.Single_1 -= 1f;
 
            array[offset++] = sample.Single_1;
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
            return MakeDecimal();
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
        public void Fill(Decimal[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = MakeDecimal();
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

        private decimal MakeDecimal()
        {
            while(true)
            {
                ulong u = Mix();
                uint i = (uint)Mix();
                var d = new decimal((int)(i >> 2), (int)u, (int)(u >> 32), false, 28);
                if (d < 1m) return d;
            }
        }
    }
}
