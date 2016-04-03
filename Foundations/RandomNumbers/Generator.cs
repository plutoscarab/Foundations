
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
        /// from raw seed data for any <see cref="IRandomSource"/> the returns a non-null value
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
        /// Create a pseudo-random number generator initialized using the current value of the high-precision timer.
        /// </summary>
        public Generator()
            : this(new[] { Stopwatch.GetTimestamp() })
        {
        }
     
        /// <summary>
        /// Create a pseudo-random number generator initialized using the current value of the high-precision timer.
        /// </summary>
        public Generator(IRandomSource source)
            : this(source, new[] { Stopwatch.GetTimestamp() })
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params byte[] seed)
            : this(DefaultSourceFactory(), seed)
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params byte[] seed)
        {
            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            this.source = source;
            var state = source.AllocateState();

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
        /// Get the next value in the pseudo-random sequence as a <see cref="UInt64"/>.
        /// </summary>
        private ulong Mix()
        {
            return sample.UInt64_0 = source.Next();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="UInt64"/>.
        /// </summary>
        public UInt64 NextUInt64()
        {
            return (UInt64)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 1)
            {
                Mix();
                array[offset++] = sample.UInt64_0;
                count -= 1;
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
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Int64"/>.
        /// </summary>
        public Int64 NextInt64()
        {
            return (Int64)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 1)
            {
                Mix();
                array[offset++] = sample.Int64_0;
                count -= 1;
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
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="UInt32"/>.
        /// </summary>
        public UInt32 NextUInt32()
        {
            return (UInt32)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
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

        private static void CreateState(IRandomSource source, byte[] seed, UInt32[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Int32"/>.
        /// </summary>
        public Int32 NextInt32()
        {
            return (Int32)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
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

        private static void CreateState(IRandomSource source, byte[] seed, Int32[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="UInt16"/>.
        /// </summary>
        public UInt16 NextUInt16()
        {
            return (UInt16)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
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

        private static void CreateState(IRandomSource source, byte[] seed, UInt16[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Int16"/>.
        /// </summary>
        public Int16 NextInt16()
        {
            return (Int16)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
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

        private static void CreateState(IRandomSource source, byte[] seed, Int16[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Byte"/>.
        /// </summary>
        public Byte NextByte()
        {
            return (Byte)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Byte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
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

        private static void CreateState(IRandomSource source, byte[] seed, Byte[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="SByte"/>.
        /// </summary>
        public SByte NextSByte()
        {
            return (SByte)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(SByte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
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

        private static void CreateState(IRandomSource source, byte[] seed, SByte[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Double"/>.
        /// </summary>
        public Double NextDouble()
        {
            return (Double)(BitConverter.Int64BitsToDouble((long)(Mix() & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Double[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
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

        private static void CreateState(IRandomSource source, byte[] seed, Double[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Single"/>.
        /// </summary>
        public Single NextSingle()
        {
            return (Single)(BitConverter.Int64BitsToDouble((long)(Mix() & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Single[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
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

        private static void CreateState(IRandomSource source, byte[] seed, Single[] state)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var rand = new Generator(source, seed);
            rand.GetNext(state);
            var dispose = source as IDisposable;
            if (dispose != null) dispose.Dispose();
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

                default:
                    throw new NotSupportedException("Unsupported array type.");
            }
        }
    }
}
