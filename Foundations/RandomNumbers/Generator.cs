﻿
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

        private Generator(IRandomSource source, bool throwIfNonClonable)
        {
            this.source = source.Clone();

            if (this.source ==  null && throwIfNonClonable)
                throw new ArgumentException("Source generator is using a source that cannot be cloned.");
        }

        /// <summary>
        /// Create a pseudo-random number generator with the same state as an existing one.
        /// </summary>
        public Generator(Generator generator)
            : this(generator.source, true)
        {
        }

        /// <summary>
        /// Gets a copy of this <see cref="Generator"/> with the same state.
        /// </summary>
        public Generator Clone()
        {
            var source = this.source.Clone();
            if (source == null) return null;
            return new Generator(source, false);
        }

        /// <summary>
        /// Gets a synchronized ("thread-safe") copy of this <see cref="Generator"/>.
        /// </summary>
        public Generator Synchronized()
        {
            return new Generator(new SynchronizedRandomSource(source));
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
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(IRandomSource source, params bool[] seed)
            : this(source, seed.GetBytes())
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public Generator(params bool[] seed)
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
        /// Gets a random <see cref="System.UInt64"/> value.
        /// </summary>
        public UInt64 UInt64()
        {
            ValueUnion value = new();
            Next(ref value);
            return value.UInt64_0;
        }

        private UInt64 GetRangeMask(UInt64 range)
        {
            var mask = (UInt64)(range - 1);
            mask |= (UInt64)(mask >> 1);
            mask |= (UInt64)(mask >> 2);
            mask |= (UInt64)(mask >> 4);
            mask |= (UInt64)(mask >> 8);
            mask |= (UInt64)(mask >> 16);
            mask |= (UInt64)(mask >> 32);
            return mask;
        }

        /// <summary>
        /// Gets a random <see cref="System.UInt64"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public UInt64 UInt64(UInt64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            UInt64 mask = GetRangeMask(range);
            UInt64 sample;

            do
            {
                sample = (UInt64)(UInt64() & (UInt64)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Gets a random <see cref="System.UInt64"/> value.
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
                ValueUnion value = new();

                while (count >= 1)
                {
                    Next(ref value);
                    *p++ = value.UInt64_0;
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

            ulong mask = GetRangeMask(range);

            while (count-- > 0)
            {
                array[offset++] = UInt64(minimum, range);
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt64"/> values into an array using exclusive-OR operation.
        /// </summary>
        public void XorFill(UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt64"/> values into an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        public void XorFill(UInt64 range, UInt64[] array)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(range, array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt64"/> values into an array using exclusive-OR operation.
        /// </summary>
        unsafe public void XorFill(UInt64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (UInt64* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 1)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0;
                    count -= 1;
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt64"/> values into part of an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        /// <param name="offset">The index of the first element in the array to be affected.</param>
        /// <param name="count">The number of elements in the array to be affected.</param>
        unsafe public void XorFill(UInt64 range, UInt64[] array, int offset, int count)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();
            ulong mask = GetRangeMask(range);

            fixed (UInt64* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 1)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0 & mask;
                    count -= 1;
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.UInt64"/> values to values in an array.
        /// </summary>
        public void AddFill(UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.UInt64"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(UInt64 range, UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.UInt64"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(UInt64 minimum, UInt64 range, UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.UInt64"/> values to part of an array.
        /// </summary>
        public void AddFill(UInt64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] += UInt64();
            }
        }

        /// <summary>
        /// Adds random <see cref="System.UInt64"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(UInt64 minimum, UInt64 range, UInt64[] array, int offset, int count)
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
                array[offset++] += UInt64(minimum, range);
            }
        }

        /// <summary>
        /// Adds random <see cref="System.UInt64"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(UInt64 range, UInt64[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of UInt64 values.
        /// </summary>
        public IEnumerable<UInt64> UInt64s()
        {            
            while (true)
            {
                yield return UInt64();
            }
        }

        /// <summary>
        /// Gets a sequence of UInt64 values in [0, range).
        /// </summary>
        public IEnumerable<UInt64> UInt64s(UInt64 range)
        {
            return UInt64s(0, range);
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

            ulong mask = GetRangeMask(range);

            ValueUnion value = new();

            while(true)
            {
                Next(ref value);
                value.UInt64_0 &= mask;
                if (value.UInt64_0 < range) yield return (UInt64)(minimum + value.UInt64_0);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt64"/>s and fill it with random values.
        /// </summary>
        public UInt64[] CreateUInt64s(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt64[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt64"/>s and fill it with random values in [0, range).
        /// </summary>
        public UInt64[] CreateUInt64s(int count, UInt64 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt64[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt64"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public UInt64[] CreateUInt64s(int count, UInt64 minimum, UInt64 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt64[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.Int64"/> value.
        /// </summary>
        public Int64 Int64()
        {
            ValueUnion value = new();
            Next(ref value);
            return value.Int64_0;
        }

        /// <summary>
        /// Gets a random non-negative <see cref="System.Int64"/> value.
        /// </summary>
        public Int64 Int64NonNegative()
        {
            return (Int64)(Int64() & 0x7FFFFFFFFFFFFFFF);
        }

        private UInt64 GetRangeMask(Int64 range)
        {
            var mask = (UInt64)(range - 1);
            mask |= (UInt64)(mask >> 1);
            mask |= (UInt64)(mask >> 2);
            mask |= (UInt64)(mask >> 4);
            mask |= (UInt64)(mask >> 8);
            mask |= (UInt64)(mask >> 16);
            mask |= (UInt64)(mask >> 32);
            return mask;
        }

        /// <summary>
        /// Gets a random <see cref="System.Int64"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Int64 Int64(Int64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            UInt64 mask = GetRangeMask(range);
            Int64 sample;

            do
            {
                sample = (Int64)(Int64() & (Int64)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Gets a random <see cref="System.Int64"/> value.
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
                ValueUnion value = new();

                while (count >= 1)
                {
                    Next(ref value);
                    *p++ = value.UInt64_0;
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

            ulong mask = GetRangeMask(range);

            while (count-- > 0)
            {
                array[offset++] = Int64(minimum, range);
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.Int64"/> values into an array using exclusive-OR operation.
        /// </summary>
        public void XorFill(Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.Int64"/> values into an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        public void XorFill(Int64 range, Int64[] array)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(range, array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.Int64"/> values into an array using exclusive-OR operation.
        /// </summary>
        unsafe public void XorFill(Int64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (Int64* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 1)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0;
                    count -= 1;
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.Int64"/> values into part of an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        /// <param name="offset">The index of the first element in the array to be affected.</param>
        /// <param name="count">The number of elements in the array to be affected.</param>
        unsafe public void XorFill(Int64 range, Int64[] array, int offset, int count)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();
            ulong mask = GetRangeMask(range);

            fixed (Int64* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 1)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0 & mask;
                    count -= 1;
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Int64"/> values to values in an array.
        /// </summary>
        public void AddFill(Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Int64"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Int64 range, Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Int64"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Int64 minimum, Int64 range, Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Int64"/> values to part of an array.
        /// </summary>
        public void AddFill(Int64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] += Int64();
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Int64"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(Int64 minimum, Int64 range, Int64[] array, int offset, int count)
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
                array[offset++] += Int64(minimum, range);
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Int64"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(Int64 range, Int64[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of Int64 values.
        /// </summary>
        public IEnumerable<Int64> Int64s()
        {            
            while (true)
            {
                yield return Int64();
            }
        }

        /// <summary>
        /// Gets a sequence of Int64 values in [0, range).
        /// </summary>
        public IEnumerable<Int64> Int64s(Int64 range)
        {
            return Int64s(0, range);
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

            ulong mask = GetRangeMask(range);

            ValueUnion value = new();

            while(true)
            {
                Next(ref value);
                value.UInt64_0 &= mask;
                if (value.Int64_0 < range) yield return (Int64)(minimum + value.Int64_0);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int64"/>s and fill it with random values.
        /// </summary>
        public Int64[] CreateInt64s(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Int64[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int64"/>s and fill it with random values in [0, range).
        /// </summary>
        public Int64[] CreateInt64s(int count, Int64 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Int64[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int64"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public Int64[] CreateInt64s(int count, Int64 minimum, Int64 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Int64[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.UInt32"/> value.
        /// </summary>
        public UInt32 UInt32()
        {
            ValueUnion value = new();
            Next(ref value);
            return value.UInt32_0;
        }

        private UInt32 GetRangeMask(UInt32 range)
        {
            var mask = (UInt32)(range - 1);
            mask |= (UInt32)(mask >> 1);
            mask |= (UInt32)(mask >> 2);
            mask |= (UInt32)(mask >> 4);
            mask |= (UInt32)(mask >> 8);
            mask |= (UInt32)(mask >> 16);
            return mask;
        }

        /// <summary>
        /// Gets a random <see cref="System.UInt32"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public UInt32 UInt32(UInt32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            UInt32 mask = GetRangeMask(range);
            UInt32 sample;

            do
            {
                sample = (UInt32)(UInt32() & (UInt32)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Gets a random <see cref="System.UInt32"/> value.
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
                ValueUnion value = new();

                while (count >= 2)
                {
                    Next(ref value);
                    *p++ = value.UInt64_0;
                    count -= 2;
                }

                if (count == 0)
                    return;

                Next(ref value);
                ulong sample = value.UInt64_0;
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.UInt32_0 < range) 
                { 
                    array[offset++] = (UInt32)(minimum + value.UInt32_0); 
                    if (--count == 0) break; 
                }

                if (value.UInt32_1 < range) 
                { 
                    array[offset++] = (UInt32)(minimum + value.UInt32_1); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt32"/> values into an array using exclusive-OR operation.
        /// </summary>
        public void XorFill(UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt32"/> values into an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        public void XorFill(UInt32 range, UInt32[] array)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(range, array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt32"/> values into an array using exclusive-OR operation.
        /// </summary>
        unsafe public void XorFill(UInt32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (UInt32* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 2)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0;
                    count -= 2;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64();
                var p1 = (UInt32*)p;
                var p2 = (UInt32*)&sample;
                *p1 ^= *p2;
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt32"/> values into part of an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        /// <param name="offset">The index of the first element in the array to be affected.</param>
        /// <param name="count">The number of elements in the array to be affected.</param>
        unsafe public void XorFill(UInt32 range, UInt32[] array, int offset, int count)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();
            ulong mask = GetRangeMask(range);
            mask |= mask << 32;

            fixed (UInt32* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 2)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0 & mask;
                    count -= 2;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64() & mask;
                var p1 = (UInt32*)p;
                var p2 = (UInt32*)&sample;
                *p1 ^= *p2;
            }
        }

        /// <summary>
        /// Adds random <see cref="System.UInt32"/> values to values in an array.
        /// </summary>
        public void AddFill(UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.UInt32"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(UInt32 range, UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.UInt32"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(UInt32 minimum, UInt32 range, UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.UInt32"/> values to part of an array.
        /// </summary>
        public void AddFill(UInt32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            while (count >= 2)
            {
                Next(ref value);
                array[offset++] += value.UInt32_0;
                array[offset++] += value.UInt32_1;
                count -= 2;
            }

            if (count == 0)
                return;

            Next(ref value);

            array[offset++] += value.UInt32_0; 
            if (--count == 0) return;
        }

        /// <summary>
        /// Adds random <see cref="System.UInt32"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(UInt32 minimum, UInt32 range, UInt32[] array, int offset, int count)
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.UInt32_0 < range) 
                { 
                    array[offset++] += (UInt32)(minimum + value.UInt32_0); 
                    if (--count == 0) break; 
                }

                if (value.UInt32_1 < range) 
                { 
                    array[offset++] += (UInt32)(minimum + value.UInt32_1); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.UInt32"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(UInt32 range, UInt32[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of UInt32 values.
        /// </summary>
        public IEnumerable<UInt32> UInt32s()
        {            
            ValueUnion value = new();

            
            while (true)
            {
                Next(ref value);
                yield return value.UInt32_0;
                yield return value.UInt32_1;
            }
        }

        /// <summary>
        /// Gets a sequence of UInt32 values in [0, range).
        /// </summary>
        public IEnumerable<UInt32> UInt32s(UInt32 range)
        {
            return UInt32s(0, range);
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 32;

            ValueUnion value = new();

            while(true)
            {
                Next(ref value);
                value.UInt64_0 &= mask;
                if (value.UInt32_0 < range) yield return (UInt32)(minimum + value.UInt32_0);
                if (value.UInt32_1 < range) yield return (UInt32)(minimum + value.UInt32_1);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt32"/>s and fill it with random values.
        /// </summary>
        public UInt32[] CreateUInt32s(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt32[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt32"/>s and fill it with random values in [0, range).
        /// </summary>
        public UInt32[] CreateUInt32s(int count, UInt32 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt32[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt32"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public UInt32[] CreateUInt32s(int count, UInt32 minimum, UInt32 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt32[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.Int32"/> value.
        /// </summary>
        public Int32 Int32()
        {
            ValueUnion value = new();
            Next(ref value);
            return value.Int32_0;
        }

        /// <summary>
        /// Gets a random non-negative <see cref="System.Int32"/> value.
        /// </summary>
        public Int32 Int32NonNegative()
        {
            return (Int32)(Int32() & 0x7FFFFFFF);
        }

        private UInt32 GetRangeMask(Int32 range)
        {
            var mask = (UInt32)(range - 1);
            mask |= (UInt32)(mask >> 1);
            mask |= (UInt32)(mask >> 2);
            mask |= (UInt32)(mask >> 4);
            mask |= (UInt32)(mask >> 8);
            mask |= (UInt32)(mask >> 16);
            return mask;
        }

        /// <summary>
        /// Gets a random <see cref="System.Int32"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Int32 Int32(Int32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            UInt32 mask = GetRangeMask(range);
            Int32 sample;

            do
            {
                sample = (Int32)(Int32() & (Int32)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Gets a random <see cref="System.Int32"/> value.
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
                ValueUnion value = new();

                while (count >= 2)
                {
                    Next(ref value);
                    *p++ = value.UInt64_0;
                    count -= 2;
                }

                if (count == 0)
                    return;

                Next(ref value);
                ulong sample = value.UInt64_0;
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.Int32_0 < range) 
                { 
                    array[offset++] = (Int32)(minimum + value.Int32_0); 
                    if (--count == 0) break; 
                }

                if (value.Int32_1 < range) 
                { 
                    array[offset++] = (Int32)(minimum + value.Int32_1); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.Int32"/> values into an array using exclusive-OR operation.
        /// </summary>
        public void XorFill(Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.Int32"/> values into an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        public void XorFill(Int32 range, Int32[] array)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(range, array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.Int32"/> values into an array using exclusive-OR operation.
        /// </summary>
        unsafe public void XorFill(Int32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (Int32* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 2)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0;
                    count -= 2;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64();
                var p1 = (Int32*)p;
                var p2 = (Int32*)&sample;
                *p1 ^= *p2;
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.Int32"/> values into part of an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        /// <param name="offset">The index of the first element in the array to be affected.</param>
        /// <param name="count">The number of elements in the array to be affected.</param>
        unsafe public void XorFill(Int32 range, Int32[] array, int offset, int count)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();
            ulong mask = GetRangeMask(range);
            mask |= mask << 32;

            fixed (Int32* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 2)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0 & mask;
                    count -= 2;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64() & mask;
                var p1 = (Int32*)p;
                var p2 = (Int32*)&sample;
                *p1 ^= *p2;
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Int32"/> values to values in an array.
        /// </summary>
        public void AddFill(Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Int32"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Int32 range, Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Int32"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Int32 minimum, Int32 range, Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Int32"/> values to part of an array.
        /// </summary>
        public void AddFill(Int32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            while (count >= 2)
            {
                Next(ref value);
                array[offset++] += value.Int32_0;
                array[offset++] += value.Int32_1;
                count -= 2;
            }

            if (count == 0)
                return;

            Next(ref value);

            array[offset++] += value.Int32_0; 
            if (--count == 0) return;
        }

        /// <summary>
        /// Adds random <see cref="System.Int32"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(Int32 minimum, Int32 range, Int32[] array, int offset, int count)
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.Int32_0 < range) 
                { 
                    array[offset++] += (Int32)(minimum + value.Int32_0); 
                    if (--count == 0) break; 
                }

                if (value.Int32_1 < range) 
                { 
                    array[offset++] += (Int32)(minimum + value.Int32_1); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Int32"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(Int32 range, Int32[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of Int32 values.
        /// </summary>
        public IEnumerable<Int32> Int32s()
        {            
            ValueUnion value = new();

            
            while (true)
            {
                Next(ref value);
                yield return value.Int32_0;
                yield return value.Int32_1;
            }
        }

        /// <summary>
        /// Gets a sequence of Int32 values in [0, range).
        /// </summary>
        public IEnumerable<Int32> Int32s(Int32 range)
        {
            return Int32s(0, range);
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 32;

            ValueUnion value = new();

            while(true)
            {
                Next(ref value);
                value.UInt64_0 &= mask;
                if (value.Int32_0 < range) yield return (Int32)(minimum + value.Int32_0);
                if (value.Int32_1 < range) yield return (Int32)(minimum + value.Int32_1);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int32"/>s and fill it with random values.
        /// </summary>
        public Int32[] CreateInt32s(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Int32[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int32"/>s and fill it with random values in [0, range).
        /// </summary>
        public Int32[] CreateInt32s(int count, Int32 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Int32[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int32"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public Int32[] CreateInt32s(int count, Int32 minimum, Int32 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Int32[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.UInt16"/> value.
        /// </summary>
        public UInt16 UInt16()
        {
            ValueUnion value = new();
            Next(ref value);
            return value.UInt16_0;
        }

        private UInt16 GetRangeMask(UInt16 range)
        {
            var mask = (UInt16)(range - 1);
            mask |= (UInt16)(mask >> 1);
            mask |= (UInt16)(mask >> 2);
            mask |= (UInt16)(mask >> 4);
            mask |= (UInt16)(mask >> 8);
            return mask;
        }

        /// <summary>
        /// Gets a random <see cref="System.UInt16"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public UInt16 UInt16(UInt16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            UInt16 mask = GetRangeMask(range);
            UInt16 sample;

            do
            {
                sample = (UInt16)(UInt16() & (UInt16)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Gets a random <see cref="System.UInt16"/> value.
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
                ValueUnion value = new();

                while (count >= 4)
                {
                    Next(ref value);
                    *p++ = value.UInt64_0;
                    count -= 4;
                }

                if (count == 0)
                    return;

                Next(ref value);
                ulong sample = value.UInt64_0;
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.UInt16_0 < range) 
                { 
                    array[offset++] = (UInt16)(minimum + value.UInt16_0); 
                    if (--count == 0) break; 
                }

                if (value.UInt16_1 < range) 
                { 
                    array[offset++] = (UInt16)(minimum + value.UInt16_1); 
                    if (--count == 0) break; 
                }

                if (value.UInt16_2 < range) 
                { 
                    array[offset++] = (UInt16)(minimum + value.UInt16_2); 
                    if (--count == 0) break; 
                }

                if (value.UInt16_3 < range) 
                { 
                    array[offset++] = (UInt16)(minimum + value.UInt16_3); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt16"/> values into an array using exclusive-OR operation.
        /// </summary>
        public void XorFill(UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt16"/> values into an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        public void XorFill(UInt16 range, UInt16[] array)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(range, array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt16"/> values into an array using exclusive-OR operation.
        /// </summary>
        unsafe public void XorFill(UInt16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (UInt16* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 4)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0;
                    count -= 4;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64();
                var p1 = (UInt16*)p;
                var p2 = (UInt16*)&sample;

                while (count-- > 0)
                {
                    *p1++ ^= *p2++;
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.UInt16"/> values into part of an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        /// <param name="offset">The index of the first element in the array to be affected.</param>
        /// <param name="count">The number of elements in the array to be affected.</param>
        unsafe public void XorFill(UInt16 range, UInt16[] array, int offset, int count)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();
            ulong mask = GetRangeMask(range);
            mask |= mask << 16;
            mask |= mask << 32;

            fixed (UInt16* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 4)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0 & mask;
                    count -= 4;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64() & mask;
                var p1 = (UInt16*)p;
                var p2 = (UInt16*)&sample;

                while (count-- > 0)
                {
                    *p1++ ^= *p2++;
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.UInt16"/> values to values in an array.
        /// </summary>
        public void AddFill(UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.UInt16"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(UInt16 range, UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.UInt16"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(UInt16 minimum, UInt16 range, UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.UInt16"/> values to part of an array.
        /// </summary>
        public void AddFill(UInt16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            while (count >= 4)
            {
                Next(ref value);
                array[offset++] += value.UInt16_0;
                array[offset++] += value.UInt16_1;
                array[offset++] += value.UInt16_2;
                array[offset++] += value.UInt16_3;
                count -= 4;
            }

            if (count == 0)
                return;

            Next(ref value);

            array[offset++] += value.UInt16_0; 
            if (--count == 0) return;

            array[offset++] += value.UInt16_1; 
            if (--count == 0) return;

            array[offset++] += value.UInt16_2; 
            if (--count == 0) return;
        }

        /// <summary>
        /// Adds random <see cref="System.UInt16"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(UInt16 minimum, UInt16 range, UInt16[] array, int offset, int count)
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.UInt16_0 < range) 
                { 
                    array[offset++] += (UInt16)(minimum + value.UInt16_0); 
                    if (--count == 0) break; 
                }

                if (value.UInt16_1 < range) 
                { 
                    array[offset++] += (UInt16)(minimum + value.UInt16_1); 
                    if (--count == 0) break; 
                }

                if (value.UInt16_2 < range) 
                { 
                    array[offset++] += (UInt16)(minimum + value.UInt16_2); 
                    if (--count == 0) break; 
                }

                if (value.UInt16_3 < range) 
                { 
                    array[offset++] += (UInt16)(minimum + value.UInt16_3); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.UInt16"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(UInt16 range, UInt16[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of UInt16 values.
        /// </summary>
        public IEnumerable<UInt16> UInt16s()
        {            
            ValueUnion value = new();

            
            while (true)
            {
                Next(ref value);
                yield return value.UInt16_0;
                yield return value.UInt16_1;
                yield return value.UInt16_2;
                yield return value.UInt16_3;
            }
        }

        /// <summary>
        /// Gets a sequence of UInt16 values in [0, range).
        /// </summary>
        public IEnumerable<UInt16> UInt16s(UInt16 range)
        {
            return UInt16s(0, range);
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while(true)
            {
                Next(ref value);
                value.UInt64_0 &= mask;
                if (value.UInt16_0 < range) yield return (UInt16)(minimum + value.UInt16_0);
                if (value.UInt16_1 < range) yield return (UInt16)(minimum + value.UInt16_1);
                if (value.UInt16_2 < range) yield return (UInt16)(minimum + value.UInt16_2);
                if (value.UInt16_3 < range) yield return (UInt16)(minimum + value.UInt16_3);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt16"/>s and fill it with random values.
        /// </summary>
        public UInt16[] CreateUInt16s(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt16[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt16"/>s and fill it with random values in [0, range).
        /// </summary>
        public UInt16[] CreateUInt16s(int count, UInt16 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt16[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt16"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public UInt16[] CreateUInt16s(int count, UInt16 minimum, UInt16 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt16[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.Int16"/> value.
        /// </summary>
        public Int16 Int16()
        {
            ValueUnion value = new();
            Next(ref value);
            return value.Int16_0;
        }

        /// <summary>
        /// Gets a random non-negative <see cref="System.Int16"/> value.
        /// </summary>
        public Int16 Int16NonNegative()
        {
            return (Int16)(Int16() & 0x7FFF);
        }

        private UInt16 GetRangeMask(Int16 range)
        {
            var mask = (UInt16)(range - 1);
            mask |= (UInt16)(mask >> 1);
            mask |= (UInt16)(mask >> 2);
            mask |= (UInt16)(mask >> 4);
            mask |= (UInt16)(mask >> 8);
            return mask;
        }

        /// <summary>
        /// Gets a random <see cref="System.Int16"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Int16 Int16(Int16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            UInt16 mask = GetRangeMask(range);
            Int16 sample;

            do
            {
                sample = (Int16)(Int16() & (Int16)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Gets a random <see cref="System.Int16"/> value.
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
                ValueUnion value = new();

                while (count >= 4)
                {
                    Next(ref value);
                    *p++ = value.UInt64_0;
                    count -= 4;
                }

                if (count == 0)
                    return;

                Next(ref value);
                ulong sample = value.UInt64_0;
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.Int16_0 < range) 
                { 
                    array[offset++] = (Int16)(minimum + value.Int16_0); 
                    if (--count == 0) break; 
                }

                if (value.Int16_1 < range) 
                { 
                    array[offset++] = (Int16)(minimum + value.Int16_1); 
                    if (--count == 0) break; 
                }

                if (value.Int16_2 < range) 
                { 
                    array[offset++] = (Int16)(minimum + value.Int16_2); 
                    if (--count == 0) break; 
                }

                if (value.Int16_3 < range) 
                { 
                    array[offset++] = (Int16)(minimum + value.Int16_3); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.Int16"/> values into an array using exclusive-OR operation.
        /// </summary>
        public void XorFill(Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.Int16"/> values into an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        public void XorFill(Int16 range, Int16[] array)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(range, array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.Int16"/> values into an array using exclusive-OR operation.
        /// </summary>
        unsafe public void XorFill(Int16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (Int16* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 4)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0;
                    count -= 4;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64();
                var p1 = (Int16*)p;
                var p2 = (Int16*)&sample;

                while (count-- > 0)
                {
                    *p1++ ^= *p2++;
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.Int16"/> values into part of an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        /// <param name="offset">The index of the first element in the array to be affected.</param>
        /// <param name="count">The number of elements in the array to be affected.</param>
        unsafe public void XorFill(Int16 range, Int16[] array, int offset, int count)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();
            ulong mask = GetRangeMask(range);
            mask |= mask << 16;
            mask |= mask << 32;

            fixed (Int16* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 4)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0 & mask;
                    count -= 4;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64() & mask;
                var p1 = (Int16*)p;
                var p2 = (Int16*)&sample;

                while (count-- > 0)
                {
                    *p1++ ^= *p2++;
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Int16"/> values to values in an array.
        /// </summary>
        public void AddFill(Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Int16"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Int16 range, Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Int16"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Int16 minimum, Int16 range, Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Int16"/> values to part of an array.
        /// </summary>
        public void AddFill(Int16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            while (count >= 4)
            {
                Next(ref value);
                array[offset++] += value.Int16_0;
                array[offset++] += value.Int16_1;
                array[offset++] += value.Int16_2;
                array[offset++] += value.Int16_3;
                count -= 4;
            }

            if (count == 0)
                return;

            Next(ref value);

            array[offset++] += value.Int16_0; 
            if (--count == 0) return;

            array[offset++] += value.Int16_1; 
            if (--count == 0) return;

            array[offset++] += value.Int16_2; 
            if (--count == 0) return;
        }

        /// <summary>
        /// Adds random <see cref="System.Int16"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(Int16 minimum, Int16 range, Int16[] array, int offset, int count)
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.Int16_0 < range) 
                { 
                    array[offset++] += (Int16)(minimum + value.Int16_0); 
                    if (--count == 0) break; 
                }

                if (value.Int16_1 < range) 
                { 
                    array[offset++] += (Int16)(minimum + value.Int16_1); 
                    if (--count == 0) break; 
                }

                if (value.Int16_2 < range) 
                { 
                    array[offset++] += (Int16)(minimum + value.Int16_2); 
                    if (--count == 0) break; 
                }

                if (value.Int16_3 < range) 
                { 
                    array[offset++] += (Int16)(minimum + value.Int16_3); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Int16"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(Int16 range, Int16[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of Int16 values.
        /// </summary>
        public IEnumerable<Int16> Int16s()
        {            
            ValueUnion value = new();

            
            while (true)
            {
                Next(ref value);
                yield return value.Int16_0;
                yield return value.Int16_1;
                yield return value.Int16_2;
                yield return value.Int16_3;
            }
        }

        /// <summary>
        /// Gets a sequence of Int16 values in [0, range).
        /// </summary>
        public IEnumerable<Int16> Int16s(Int16 range)
        {
            return Int16s(0, range);
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while(true)
            {
                Next(ref value);
                value.UInt64_0 &= mask;
                if (value.Int16_0 < range) yield return (Int16)(minimum + value.Int16_0);
                if (value.Int16_1 < range) yield return (Int16)(minimum + value.Int16_1);
                if (value.Int16_2 < range) yield return (Int16)(minimum + value.Int16_2);
                if (value.Int16_3 < range) yield return (Int16)(minimum + value.Int16_3);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int16"/>s and fill it with random values.
        /// </summary>
        public Int16[] CreateInt16s(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Int16[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int16"/>s and fill it with random values in [0, range).
        /// </summary>
        public Int16[] CreateInt16s(int count, Int16 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Int16[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int16"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public Int16[] CreateInt16s(int count, Int16 minimum, Int16 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Int16[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.Byte"/> value.
        /// </summary>
        public Byte Byte()
        {
            ValueUnion value = new();
            Next(ref value);
            return value.Byte_0;
        }

        private Byte GetRangeMask(Byte range)
        {
            var mask = (Byte)(range - 1);
            mask |= (Byte)(mask >> 1);
            mask |= (Byte)(mask >> 2);
            mask |= (Byte)(mask >> 4);
            return mask;
        }

        /// <summary>
        /// Gets a random <see cref="System.Byte"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Byte Byte(Byte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            Byte mask = GetRangeMask(range);
            Byte sample;

            do
            {
                sample = (Byte)(Byte() & (Byte)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Gets a random <see cref="System.Byte"/> value.
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
                ValueUnion value = new();

                while (count >= 8)
                {
                    Next(ref value);
                    *p++ = value.UInt64_0;
                    count -= 8;
                }

                if (count == 0)
                    return;

                Next(ref value);
                ulong sample = value.UInt64_0;
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 8;
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.Byte_0 < range) 
                { 
                    array[offset++] = (Byte)(minimum + value.Byte_0); 
                    if (--count == 0) break; 
                }

                if (value.Byte_1 < range) 
                { 
                    array[offset++] = (Byte)(minimum + value.Byte_1); 
                    if (--count == 0) break; 
                }

                if (value.Byte_2 < range) 
                { 
                    array[offset++] = (Byte)(minimum + value.Byte_2); 
                    if (--count == 0) break; 
                }

                if (value.Byte_3 < range) 
                { 
                    array[offset++] = (Byte)(minimum + value.Byte_3); 
                    if (--count == 0) break; 
                }

                if (value.Byte_4 < range) 
                { 
                    array[offset++] = (Byte)(minimum + value.Byte_4); 
                    if (--count == 0) break; 
                }

                if (value.Byte_5 < range) 
                { 
                    array[offset++] = (Byte)(minimum + value.Byte_5); 
                    if (--count == 0) break; 
                }

                if (value.Byte_6 < range) 
                { 
                    array[offset++] = (Byte)(minimum + value.Byte_6); 
                    if (--count == 0) break; 
                }

                if (value.Byte_7 < range) 
                { 
                    array[offset++] = (Byte)(minimum + value.Byte_7); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.Byte"/> values into an array using exclusive-OR operation.
        /// </summary>
        public void XorFill(Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.Byte"/> values into an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        public void XorFill(Byte range, Byte[] array)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(range, array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.Byte"/> values into an array using exclusive-OR operation.
        /// </summary>
        unsafe public void XorFill(Byte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (Byte* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 8)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0;
                    count -= 8;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64();
                var p1 = (Byte*)p;
                var p2 = (Byte*)&sample;

                while (count-- > 0)
                {
                    *p1++ ^= *p2++;
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.Byte"/> values into part of an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        /// <param name="offset">The index of the first element in the array to be affected.</param>
        /// <param name="count">The number of elements in the array to be affected.</param>
        unsafe public void XorFill(Byte range, Byte[] array, int offset, int count)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();
            ulong mask = GetRangeMask(range);
            mask |= mask << 8;
            mask |= mask << 16;
            mask |= mask << 32;

            fixed (Byte* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 8)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0 & mask;
                    count -= 8;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64() & mask;
                var p1 = (Byte*)p;
                var p2 = (Byte*)&sample;

                while (count-- > 0)
                {
                    *p1++ ^= *p2++;
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Byte"/> values to values in an array.
        /// </summary>
        public void AddFill(Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Byte"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Byte range, Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Byte"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Byte minimum, Byte range, Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Byte"/> values to part of an array.
        /// </summary>
        public void AddFill(Byte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            while (count >= 8)
            {
                Next(ref value);
                array[offset++] += value.Byte_0;
                array[offset++] += value.Byte_1;
                array[offset++] += value.Byte_2;
                array[offset++] += value.Byte_3;
                array[offset++] += value.Byte_4;
                array[offset++] += value.Byte_5;
                array[offset++] += value.Byte_6;
                array[offset++] += value.Byte_7;
                count -= 8;
            }

            if (count == 0)
                return;

            Next(ref value);

            array[offset++] += value.Byte_0; 
            if (--count == 0) return;

            array[offset++] += value.Byte_1; 
            if (--count == 0) return;

            array[offset++] += value.Byte_2; 
            if (--count == 0) return;

            array[offset++] += value.Byte_3; 
            if (--count == 0) return;

            array[offset++] += value.Byte_4; 
            if (--count == 0) return;

            array[offset++] += value.Byte_5; 
            if (--count == 0) return;

            array[offset++] += value.Byte_6; 
            if (--count == 0) return;
        }

        /// <summary>
        /// Adds random <see cref="System.Byte"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(Byte minimum, Byte range, Byte[] array, int offset, int count)
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 8;
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.Byte_0 < range) 
                { 
                    array[offset++] += (Byte)(minimum + value.Byte_0); 
                    if (--count == 0) break; 
                }

                if (value.Byte_1 < range) 
                { 
                    array[offset++] += (Byte)(minimum + value.Byte_1); 
                    if (--count == 0) break; 
                }

                if (value.Byte_2 < range) 
                { 
                    array[offset++] += (Byte)(minimum + value.Byte_2); 
                    if (--count == 0) break; 
                }

                if (value.Byte_3 < range) 
                { 
                    array[offset++] += (Byte)(minimum + value.Byte_3); 
                    if (--count == 0) break; 
                }

                if (value.Byte_4 < range) 
                { 
                    array[offset++] += (Byte)(minimum + value.Byte_4); 
                    if (--count == 0) break; 
                }

                if (value.Byte_5 < range) 
                { 
                    array[offset++] += (Byte)(minimum + value.Byte_5); 
                    if (--count == 0) break; 
                }

                if (value.Byte_6 < range) 
                { 
                    array[offset++] += (Byte)(minimum + value.Byte_6); 
                    if (--count == 0) break; 
                }

                if (value.Byte_7 < range) 
                { 
                    array[offset++] += (Byte)(minimum + value.Byte_7); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Byte"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(Byte range, Byte[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of Byte values.
        /// </summary>
        public IEnumerable<Byte> Bytes()
        {            
            ValueUnion value = new();

            
            while (true)
            {
                Next(ref value);
                yield return value.Byte_0;
                yield return value.Byte_1;
                yield return value.Byte_2;
                yield return value.Byte_3;
                yield return value.Byte_4;
                yield return value.Byte_5;
                yield return value.Byte_6;
                yield return value.Byte_7;
            }
        }

        /// <summary>
        /// Gets a sequence of Byte values in [0, range).
        /// </summary>
        public IEnumerable<Byte> Bytes(Byte range)
        {
            return Bytes(0, range);
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 8;
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while(true)
            {
                Next(ref value);
                value.UInt64_0 &= mask;
                if (value.Byte_0 < range) yield return (Byte)(minimum + value.Byte_0);
                if (value.Byte_1 < range) yield return (Byte)(minimum + value.Byte_1);
                if (value.Byte_2 < range) yield return (Byte)(minimum + value.Byte_2);
                if (value.Byte_3 < range) yield return (Byte)(minimum + value.Byte_3);
                if (value.Byte_4 < range) yield return (Byte)(minimum + value.Byte_4);
                if (value.Byte_5 < range) yield return (Byte)(minimum + value.Byte_5);
                if (value.Byte_6 < range) yield return (Byte)(minimum + value.Byte_6);
                if (value.Byte_7 < range) yield return (Byte)(minimum + value.Byte_7);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Byte"/>s and fill it with random values.
        /// </summary>
        public Byte[] CreateBytes(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Byte[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Byte"/>s and fill it with random values in [0, range).
        /// </summary>
        public Byte[] CreateBytes(int count, Byte range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Byte[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Byte"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public Byte[] CreateBytes(int count, Byte minimum, Byte range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Byte[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.SByte"/> value.
        /// </summary>
        public SByte SByte()
        {
            ValueUnion value = new();
            Next(ref value);
            return value.SByte_0;
        }

        /// <summary>
        /// Gets a random non-negative <see cref="System.SByte"/> value.
        /// </summary>
        public SByte SByteNonNegative()
        {
            return (SByte)(SByte() & 0x7F);
        }

        private Byte GetRangeMask(SByte range)
        {
            var mask = (Byte)(range - 1);
            mask |= (Byte)(mask >> 1);
            mask |= (Byte)(mask >> 2);
            mask |= (Byte)(mask >> 4);
            return mask;
        }

        /// <summary>
        /// Gets a random <see cref="System.SByte"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public SByte SByte(SByte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            Byte mask = GetRangeMask(range);
            SByte sample;

            do
            {
                sample = (SByte)(SByte() & (SByte)mask);
            }
            while (sample >= range);

            return sample;
        }

        /// <summary>
        /// Gets a random <see cref="System.SByte"/> value.
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
                ValueUnion value = new();

                while (count >= 8)
                {
                    Next(ref value);
                    *p++ = value.UInt64_0;
                    count -= 8;
                }

                if (count == 0)
                    return;

                Next(ref value);
                ulong sample = value.UInt64_0;
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 8;
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.SByte_0 < range) 
                { 
                    array[offset++] = (SByte)(minimum + value.SByte_0); 
                    if (--count == 0) break; 
                }

                if (value.SByte_1 < range) 
                { 
                    array[offset++] = (SByte)(minimum + value.SByte_1); 
                    if (--count == 0) break; 
                }

                if (value.SByte_2 < range) 
                { 
                    array[offset++] = (SByte)(minimum + value.SByte_2); 
                    if (--count == 0) break; 
                }

                if (value.SByte_3 < range) 
                { 
                    array[offset++] = (SByte)(minimum + value.SByte_3); 
                    if (--count == 0) break; 
                }

                if (value.SByte_4 < range) 
                { 
                    array[offset++] = (SByte)(minimum + value.SByte_4); 
                    if (--count == 0) break; 
                }

                if (value.SByte_5 < range) 
                { 
                    array[offset++] = (SByte)(minimum + value.SByte_5); 
                    if (--count == 0) break; 
                }

                if (value.SByte_6 < range) 
                { 
                    array[offset++] = (SByte)(minimum + value.SByte_6); 
                    if (--count == 0) break; 
                }

                if (value.SByte_7 < range) 
                { 
                    array[offset++] = (SByte)(minimum + value.SByte_7); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.SByte"/> values into an array using exclusive-OR operation.
        /// </summary>
        public void XorFill(SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.SByte"/> values into an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        public void XorFill(SByte range, SByte[] array)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(range, array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.SByte"/> values into an array using exclusive-OR operation.
        /// </summary>
        unsafe public void XorFill(SByte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (SByte* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 8)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0;
                    count -= 8;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64();
                var p1 = (SByte*)p;
                var p2 = (SByte*)&sample;

                while (count-- > 0)
                {
                    *p1++ ^= *p2++;
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.SByte"/> values into part of an array using exclusive-OR operation.
        /// </summary>
        /// <param name="range">Maximum value, exclusive. Must be a power of 2.</param>
        /// <param name="array">The array into which the random values will be mixed.</param>
        /// <param name="offset">The index of the first element in the array to be affected.</param>
        /// <param name="count">The number of elements in the array to be affected.</param>
        unsafe public void XorFill(SByte range, SByte[] array, int offset, int count)
        {
            if ((range & (range - 1)) != 0)
                throw new ArgumentException("Parameter must be a power of 2.", nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();
            ulong mask = GetRangeMask(range);
            mask |= mask << 8;
            mask |= mask << 16;
            mask |= mask << 32;

            fixed (SByte* ptr = &array[offset])
            {
                var p = (ulong*)ptr;

                while (count >= 8)
                {
                    Next(ref value);
                    *p++ ^= value.UInt64_0 & mask;
                    count -= 8;
                }

                if (count == 0)
                    return;

                ulong sample = UInt64() & mask;
                var p1 = (SByte*)p;
                var p2 = (SByte*)&sample;

                while (count-- > 0)
                {
                    *p1++ ^= *p2++;
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.SByte"/> values to values in an array.
        /// </summary>
        public void AddFill(SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.SByte"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(SByte range, SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.SByte"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(SByte minimum, SByte range, SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.SByte"/> values to part of an array.
        /// </summary>
        public void AddFill(SByte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            while (count >= 8)
            {
                Next(ref value);
                array[offset++] += value.SByte_0;
                array[offset++] += value.SByte_1;
                array[offset++] += value.SByte_2;
                array[offset++] += value.SByte_3;
                array[offset++] += value.SByte_4;
                array[offset++] += value.SByte_5;
                array[offset++] += value.SByte_6;
                array[offset++] += value.SByte_7;
                count -= 8;
            }

            if (count == 0)
                return;

            Next(ref value);

            array[offset++] += value.SByte_0; 
            if (--count == 0) return;

            array[offset++] += value.SByte_1; 
            if (--count == 0) return;

            array[offset++] += value.SByte_2; 
            if (--count == 0) return;

            array[offset++] += value.SByte_3; 
            if (--count == 0) return;

            array[offset++] += value.SByte_4; 
            if (--count == 0) return;

            array[offset++] += value.SByte_5; 
            if (--count == 0) return;

            array[offset++] += value.SByte_6; 
            if (--count == 0) return;
        }

        /// <summary>
        /// Adds random <see cref="System.SByte"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(SByte minimum, SByte range, SByte[] array, int offset, int count)
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 8;
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while (count > 0)
            {
                Next(ref value);
                value.UInt64_0 &= mask;

                if (value.SByte_0 < range) 
                { 
                    array[offset++] += (SByte)(minimum + value.SByte_0); 
                    if (--count == 0) break; 
                }

                if (value.SByte_1 < range) 
                { 
                    array[offset++] += (SByte)(minimum + value.SByte_1); 
                    if (--count == 0) break; 
                }

                if (value.SByte_2 < range) 
                { 
                    array[offset++] += (SByte)(minimum + value.SByte_2); 
                    if (--count == 0) break; 
                }

                if (value.SByte_3 < range) 
                { 
                    array[offset++] += (SByte)(minimum + value.SByte_3); 
                    if (--count == 0) break; 
                }

                if (value.SByte_4 < range) 
                { 
                    array[offset++] += (SByte)(minimum + value.SByte_4); 
                    if (--count == 0) break; 
                }

                if (value.SByte_5 < range) 
                { 
                    array[offset++] += (SByte)(minimum + value.SByte_5); 
                    if (--count == 0) break; 
                }

                if (value.SByte_6 < range) 
                { 
                    array[offset++] += (SByte)(minimum + value.SByte_6); 
                    if (--count == 0) break; 
                }

                if (value.SByte_7 < range) 
                { 
                    array[offset++] += (SByte)(minimum + value.SByte_7); 
                    if (--count == 0) break; 
                }
            }
        }

        /// <summary>
        /// Adds random <see cref="System.SByte"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(SByte range, SByte[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of SByte values.
        /// </summary>
        public IEnumerable<SByte> SBytes()
        {            
            ValueUnion value = new();

            
            while (true)
            {
                Next(ref value);
                yield return value.SByte_0;
                yield return value.SByte_1;
                yield return value.SByte_2;
                yield return value.SByte_3;
                yield return value.SByte_4;
                yield return value.SByte_5;
                yield return value.SByte_6;
                yield return value.SByte_7;
            }
        }

        /// <summary>
        /// Gets a sequence of SByte values in [0, range).
        /// </summary>
        public IEnumerable<SByte> SBytes(SByte range)
        {
            return SBytes(0, range);
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

            ulong mask = GetRangeMask(range);
            mask |= mask << 8;
            mask |= mask << 16;
            mask |= mask << 32;

            ValueUnion value = new();

            while(true)
            {
                Next(ref value);
                value.UInt64_0 &= mask;
                if (value.SByte_0 < range) yield return (SByte)(minimum + value.SByte_0);
                if (value.SByte_1 < range) yield return (SByte)(minimum + value.SByte_1);
                if (value.SByte_2 < range) yield return (SByte)(minimum + value.SByte_2);
                if (value.SByte_3 < range) yield return (SByte)(minimum + value.SByte_3);
                if (value.SByte_4 < range) yield return (SByte)(minimum + value.SByte_4);
                if (value.SByte_5 < range) yield return (SByte)(minimum + value.SByte_5);
                if (value.SByte_6 < range) yield return (SByte)(minimum + value.SByte_6);
                if (value.SByte_7 < range) yield return (SByte)(minimum + value.SByte_7);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.SByte"/>s and fill it with random values.
        /// </summary>
        public SByte[] CreateSBytes(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new SByte[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.SByte"/>s and fill it with random values in [0, range).
        /// </summary>
        public SByte[] CreateSBytes(int count, SByte range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new SByte[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.SByte"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public SByte[] CreateSBytes(int count, SByte minimum, SByte range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new SByte[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.Double"/> value.
        /// </summary>
        public Double Double()
        {
            ValueUnion value = new();
            Next(ref value);
            return (Double)(BitConverter.Int64BitsToDouble((value.Int64_0 & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
        }

        /// <summary>
        /// Gets a random <see cref="System.Double"/> value.
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
        /// Gets a random <see cref="System.Double"/> value.
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
                ValueUnion value = new();

                while (count >= 1)
                {
                    Next(ref value);
                    *p++ = (value.UInt64_0 & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000;
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
        /// Adds random <see cref="System.Double"/> values to values in an array.
        /// </summary>
        public void AddFill(Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Double"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Double range, Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Double"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Double minimum, Double range, Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Double"/> values to part of an array.
        /// </summary>
        public void AddFill(Double[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] += Double();
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Double"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(Double minimum, Double range, Double[] array, int offset, int count)
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
                array[offset++] += Double(minimum, range);
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Double"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(Double range, Double[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of Double values.
        /// </summary>
        public IEnumerable<Double> Doubles()
        {            
            while (true)
            {
                yield return Double();
            }
        }

        /// <summary>
        /// Gets a sequence of Double values in [0, range).
        /// </summary>
        public IEnumerable<Double> Doubles(Double range)
        {
            return Doubles(0, range);
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

        /// <summary>
        /// Allocate an array of <see cref="System.Double"/>s and fill it with random values.
        /// </summary>
        public Double[] CreateDoubles(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Double[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Double"/>s and fill it with random values in [0, range).
        /// </summary>
        public Double[] CreateDoubles(int count, Double range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Double[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Double"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public Double[] CreateDoubles(int count, Double minimum, Double range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Double[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.Single"/> value.
        /// </summary>
        public Single Single()
        {
            ValueUnion value = new();
            Next(ref value);
            return (Single)(BitConverter.Int64BitsToDouble((value.Int64_0 & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
        }

        /// <summary>
        /// Gets a random <see cref="System.Single"/> value.
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
        /// Gets a random <see cref="System.Single"/> value.
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
                ValueUnion value = new();

                while (count >= 2)
                {
                    Next(ref value);
                    *p++ = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
                    *f++ -= 1f;
                    *f++ -= 1f;
                    count -= 2;
                }

                if (count == 0)
                    return;

                Next(ref value);
                ulong sample = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
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

            ValueUnion value = new();

            while (count >= 2)
            {
                Next(ref value);
                value.UInt64_0 = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
                array[offset++] = minimum + (value.Single_0 - 1f) * range;
                array[offset++] = minimum + (value.Single_1 - 1f) * range;
                count -= 2;
            }

            if (count >= 1)
            {
                Next(ref value);
                value.UInt64_0 = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
                array[offset++] = minimum + (value.Single_0 - 1f) * range;
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Single"/> values to values in an array.
        /// </summary>
        public void AddFill(Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Single"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Single range, Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Single"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Single minimum, Single range, Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Single"/> values to part of an array.
        /// </summary>
        public void AddFill(Single[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            while (count >= 2)
            {
                Next(ref value);
                value.UInt64_0 = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
				var sample = value.Single_0 - 1f;
                array[offset++] += sample;
				sample = value.Single_1 - 1f;
                array[offset++] += sample;
                count -= 2;
            }

            if (count >= 1)
            {
                Next(ref value);
                value.UInt64_0 = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
				var sample = value.Single_0 - 1f;
                array[offset] += sample;
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Single"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(Single minimum, Single range, Single[] array, int offset, int count)
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

            ValueUnion value = new();

            while (count >= 2)
            {
                Next(ref value);
                value.UInt64_0 = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
				var sample = minimum + (value.Single_0 - 1f) * range;
                array[offset++] += sample;
				sample = minimum + (value.Single_1 - 1f) * range;
                array[offset++] += sample;
                count -= 2;
            }

            if (count >= 1)
            {
                Next(ref value);
                value.UInt64_0 = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
				var sample = minimum + (value.Single_0 - 1f) * range;
                array[offset] += sample;
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Single"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(Single range, Single[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of Single values.
        /// </summary>
        public IEnumerable<Single> Singles()
        {            
            ValueUnion value = new();

            
            while (true)
            {
                Next(ref value);
                value.UInt64_0 = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
                yield return value.Single_0 - 1f;
                yield return value.Single_1 - 1f;
            }
        }

        /// <summary>
        /// Gets a sequence of Single values in [0, range).
        /// </summary>
        public IEnumerable<Single> Singles(Single range)
        {
            return Singles(0, range);
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

            ValueUnion value = new();

            while(true)
            {
                Next(ref value);
                value.UInt64_0 = (value.UInt64_0 & 0x007FFFFF007FFFFF) | 0x3F8000003F800000;
                yield return minimum + (value.Single_0 - 1f) * range;
                yield return minimum + (value.Single_1 - 1f) * range;
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Single"/>s and fill it with random values.
        /// </summary>
        public Single[] CreateSingles(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Single[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Single"/>s and fill it with random values in [0, range).
        /// </summary>
        public Single[] CreateSingles(int count, Single range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Single[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Single"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public Single[] CreateSingles(int count, Single minimum, Single range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Single[count];
            Fill(minimum, range, data);
            return data;
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
        /// Gets a random <see cref="System.Decimal"/> value.
        /// </summary>
        public Decimal Decimal()
        {
            while(true)
            {
                ulong u = UInt64();
                uint i = UInt32();
                var d = new decimal((int)(i >> 2), (int)u, (int)(u >> 32), false, 28);
                if (d < 1m) return d;
            }
        }

        /// <summary>
        /// Gets a random <see cref="System.Decimal"/> value.
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
        /// Gets a random <see cref="System.Decimal"/> value.
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
        /// Adds random <see cref="System.Decimal"/> values to values in an array.
        /// </summary>
        public void AddFill(Decimal[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Decimal"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Decimal range, Decimal[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Decimal"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Decimal minimum, Decimal range, Decimal[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds random <see cref="System.Decimal"/> values to part of an array.
        /// </summary>
        public void AddFill(Decimal[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] += Decimal();
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Decimal"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(Decimal minimum, Decimal range, Decimal[] array, int offset, int count)
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
                array[offset++] += Decimal(minimum, range);
            }
        }

        /// <summary>
        /// Adds random <see cref="System.Decimal"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(Decimal range, Decimal[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of Decimal values.
        /// </summary>
        public IEnumerable<Decimal> Decimals()
        {            
            while (true)
            {
                yield return Decimal();
            }
        }

        /// <summary>
        /// Gets a sequence of Decimal values in [0, range).
        /// </summary>
        public IEnumerable<Decimal> Decimals(Decimal range)
        {
            return Decimals(0, range);
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

        /// <summary>
        /// Allocate an array of <see cref="System.Decimal"/>s and fill it with random values.
        /// </summary>
        public Decimal[] CreateDecimals(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Decimal[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Decimal"/>s and fill it with random values in [0, range).
        /// </summary>
        public Decimal[] CreateDecimals(int count, Decimal range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Decimal[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Decimal"/>s and fill it with random values in [minimum, minimum + range).
        /// </summary>
        public Decimal[] CreateDecimals(int count, Decimal minimum, Decimal range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Decimal[count];
            Fill(minimum, range, data);
            return data;
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
        /// <summary>
        /// Gets a random <see cref="System.Boolean"/> value.
        /// </summary>
        public Boolean Boolean()
        {
            ValueUnion value = new();
            Next(ref value);
            return (value.UInt64_0 & 1UL) != 0;
        }

        /// <summary>
        /// Fill a provided array with random <see cref="System.Boolean"/> values.
        /// </summary>
        public void Fill(Boolean[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="System.Boolean"/> values.
        /// </summary>
        unsafe public void Fill(Boolean[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (Boolean* ptr = &array[offset])
            {
                var p = ptr;

                while (count >= 64)
                {
                    Next(ref value);
                    *p++ = (value.UInt32_0 & 0x1) != 0;
                    *p++ = (value.UInt32_0 & 0x2) != 0;
                    *p++ = (value.UInt32_0 & 0x4) != 0;
                    *p++ = (value.UInt32_0 & 0x8) != 0;
                    *p++ = (value.UInt32_0 & 0x10) != 0;
                    *p++ = (value.UInt32_0 & 0x20) != 0;
                    *p++ = (value.UInt32_0 & 0x40) != 0;
                    *p++ = (value.UInt32_0 & 0x80) != 0;
                    *p++ = (value.UInt32_0 & 0x100) != 0;
                    *p++ = (value.UInt32_0 & 0x200) != 0;
                    *p++ = (value.UInt32_0 & 0x400) != 0;
                    *p++ = (value.UInt32_0 & 0x800) != 0;
                    *p++ = (value.UInt32_0 & 0x1000) != 0;
                    *p++ = (value.UInt32_0 & 0x2000) != 0;
                    *p++ = (value.UInt32_0 & 0x4000) != 0;
                    *p++ = (value.UInt32_0 & 0x8000) != 0;
                    *p++ = (value.UInt32_0 & 0x10000) != 0;
                    *p++ = (value.UInt32_0 & 0x20000) != 0;
                    *p++ = (value.UInt32_0 & 0x40000) != 0;
                    *p++ = (value.UInt32_0 & 0x80000) != 0;
                    *p++ = (value.UInt32_0 & 0x100000) != 0;
                    *p++ = (value.UInt32_0 & 0x200000) != 0;
                    *p++ = (value.UInt32_0 & 0x400000) != 0;
                    *p++ = (value.UInt32_0 & 0x800000) != 0;
                    *p++ = (value.UInt32_0 & 0x1000000) != 0;
                    *p++ = (value.UInt32_0 & 0x2000000) != 0;
                    *p++ = (value.UInt32_0 & 0x4000000) != 0;
                    *p++ = (value.UInt32_0 & 0x8000000) != 0;
                    *p++ = (value.UInt32_0 & 0x10000000) != 0;
                    *p++ = (value.UInt32_0 & 0x20000000) != 0;
                    *p++ = (value.UInt32_0 & 0x40000000) != 0;
                    *p++ = (value.UInt32_0 & 0x80000000) != 0;
                    *p++ = (value.UInt32_1 & 0x1) != 0;
                    *p++ = (value.UInt32_1 & 0x2) != 0;
                    *p++ = (value.UInt32_1 & 0x4) != 0;
                    *p++ = (value.UInt32_1 & 0x8) != 0;
                    *p++ = (value.UInt32_1 & 0x10) != 0;
                    *p++ = (value.UInt32_1 & 0x20) != 0;
                    *p++ = (value.UInt32_1 & 0x40) != 0;
                    *p++ = (value.UInt32_1 & 0x80) != 0;
                    *p++ = (value.UInt32_1 & 0x100) != 0;
                    *p++ = (value.UInt32_1 & 0x200) != 0;
                    *p++ = (value.UInt32_1 & 0x400) != 0;
                    *p++ = (value.UInt32_1 & 0x800) != 0;
                    *p++ = (value.UInt32_1 & 0x1000) != 0;
                    *p++ = (value.UInt32_1 & 0x2000) != 0;
                    *p++ = (value.UInt32_1 & 0x4000) != 0;
                    *p++ = (value.UInt32_1 & 0x8000) != 0;
                    *p++ = (value.UInt32_1 & 0x10000) != 0;
                    *p++ = (value.UInt32_1 & 0x20000) != 0;
                    *p++ = (value.UInt32_1 & 0x40000) != 0;
                    *p++ = (value.UInt32_1 & 0x80000) != 0;
                    *p++ = (value.UInt32_1 & 0x100000) != 0;
                    *p++ = (value.UInt32_1 & 0x200000) != 0;
                    *p++ = (value.UInt32_1 & 0x400000) != 0;
                    *p++ = (value.UInt32_1 & 0x800000) != 0;
                    *p++ = (value.UInt32_1 & 0x1000000) != 0;
                    *p++ = (value.UInt32_1 & 0x2000000) != 0;
                    *p++ = (value.UInt32_1 & 0x4000000) != 0;
                    *p++ = (value.UInt32_1 & 0x8000000) != 0;
                    *p++ = (value.UInt32_1 & 0x10000000) != 0;
                    *p++ = (value.UInt32_1 & 0x20000000) != 0;
                    *p++ = (value.UInt32_1 & 0x40000000) != 0;
                    *p++ = (value.UInt32_1 & 0x80000000) != 0;
                    count -= 64;
                }

                if (count == 0)
                    return;

                Next(ref value);

                switch (count)
                {
                    case 63:
                        *p++ = (value.UInt32_0 & 0x1) != 0;
                        goto case 62;
                    case 62:
                        *p++ = (value.UInt32_0 & 0x2) != 0;
                        goto case 61;
                    case 61:
                        *p++ = (value.UInt32_0 & 0x4) != 0;
                        goto case 60;
                    case 60:
                        *p++ = (value.UInt32_0 & 0x8) != 0;
                        goto case 59;
                    case 59:
                        *p++ = (value.UInt32_0 & 0x10) != 0;
                        goto case 58;
                    case 58:
                        *p++ = (value.UInt32_0 & 0x20) != 0;
                        goto case 57;
                    case 57:
                        *p++ = (value.UInt32_0 & 0x40) != 0;
                        goto case 56;
                    case 56:
                        *p++ = (value.UInt32_0 & 0x80) != 0;
                        goto case 55;
                    case 55:
                        *p++ = (value.UInt32_0 & 0x100) != 0;
                        goto case 54;
                    case 54:
                        *p++ = (value.UInt32_0 & 0x200) != 0;
                        goto case 53;
                    case 53:
                        *p++ = (value.UInt32_0 & 0x400) != 0;
                        goto case 52;
                    case 52:
                        *p++ = (value.UInt32_0 & 0x800) != 0;
                        goto case 51;
                    case 51:
                        *p++ = (value.UInt32_0 & 0x1000) != 0;
                        goto case 50;
                    case 50:
                        *p++ = (value.UInt32_0 & 0x2000) != 0;
                        goto case 49;
                    case 49:
                        *p++ = (value.UInt32_0 & 0x4000) != 0;
                        goto case 48;
                    case 48:
                        *p++ = (value.UInt32_0 & 0x8000) != 0;
                        goto case 47;
                    case 47:
                        *p++ = (value.UInt32_0 & 0x10000) != 0;
                        goto case 46;
                    case 46:
                        *p++ = (value.UInt32_0 & 0x20000) != 0;
                        goto case 45;
                    case 45:
                        *p++ = (value.UInt32_0 & 0x40000) != 0;
                        goto case 44;
                    case 44:
                        *p++ = (value.UInt32_0 & 0x80000) != 0;
                        goto case 43;
                    case 43:
                        *p++ = (value.UInt32_0 & 0x100000) != 0;
                        goto case 42;
                    case 42:
                        *p++ = (value.UInt32_0 & 0x200000) != 0;
                        goto case 41;
                    case 41:
                        *p++ = (value.UInt32_0 & 0x400000) != 0;
                        goto case 40;
                    case 40:
                        *p++ = (value.UInt32_0 & 0x800000) != 0;
                        goto case 39;
                    case 39:
                        *p++ = (value.UInt32_0 & 0x1000000) != 0;
                        goto case 38;
                    case 38:
                        *p++ = (value.UInt32_0 & 0x2000000) != 0;
                        goto case 37;
                    case 37:
                        *p++ = (value.UInt32_0 & 0x4000000) != 0;
                        goto case 36;
                    case 36:
                        *p++ = (value.UInt32_0 & 0x8000000) != 0;
                        goto case 35;
                    case 35:
                        *p++ = (value.UInt32_0 & 0x10000000) != 0;
                        goto case 34;
                    case 34:
                        *p++ = (value.UInt32_0 & 0x20000000) != 0;
                        goto case 33;
                    case 33:
                        *p++ = (value.UInt32_0 & 0x40000000) != 0;
                        goto case 32;
                    case 32:
                        *p++ = (value.UInt32_0 & 0x80000000) != 0;
                        goto case 31;
                    case 31:
                        *p++ = (value.UInt32_1 & 0x1) != 0;
                        goto case 30;
                    case 30:
                        *p++ = (value.UInt32_1 & 0x2) != 0;
                        goto case 29;
                    case 29:
                        *p++ = (value.UInt32_1 & 0x4) != 0;
                        goto case 28;
                    case 28:
                        *p++ = (value.UInt32_1 & 0x8) != 0;
                        goto case 27;
                    case 27:
                        *p++ = (value.UInt32_1 & 0x10) != 0;
                        goto case 26;
                    case 26:
                        *p++ = (value.UInt32_1 & 0x20) != 0;
                        goto case 25;
                    case 25:
                        *p++ = (value.UInt32_1 & 0x40) != 0;
                        goto case 24;
                    case 24:
                        *p++ = (value.UInt32_1 & 0x80) != 0;
                        goto case 23;
                    case 23:
                        *p++ = (value.UInt32_1 & 0x100) != 0;
                        goto case 22;
                    case 22:
                        *p++ = (value.UInt32_1 & 0x200) != 0;
                        goto case 21;
                    case 21:
                        *p++ = (value.UInt32_1 & 0x400) != 0;
                        goto case 20;
                    case 20:
                        *p++ = (value.UInt32_1 & 0x800) != 0;
                        goto case 19;
                    case 19:
                        *p++ = (value.UInt32_1 & 0x1000) != 0;
                        goto case 18;
                    case 18:
                        *p++ = (value.UInt32_1 & 0x2000) != 0;
                        goto case 17;
                    case 17:
                        *p++ = (value.UInt32_1 & 0x4000) != 0;
                        goto case 16;
                    case 16:
                        *p++ = (value.UInt32_1 & 0x8000) != 0;
                        goto case 15;
                    case 15:
                        *p++ = (value.UInt32_1 & 0x10000) != 0;
                        goto case 14;
                    case 14:
                        *p++ = (value.UInt32_1 & 0x20000) != 0;
                        goto case 13;
                    case 13:
                        *p++ = (value.UInt32_1 & 0x40000) != 0;
                        goto case 12;
                    case 12:
                        *p++ = (value.UInt32_1 & 0x80000) != 0;
                        goto case 11;
                    case 11:
                        *p++ = (value.UInt32_1 & 0x100000) != 0;
                        goto case 10;
                    case 10:
                        *p++ = (value.UInt32_1 & 0x200000) != 0;
                        goto case 9;
                    case 9:
                        *p++ = (value.UInt32_1 & 0x400000) != 0;
                        goto case 8;
                    case 8:
                        *p++ = (value.UInt32_1 & 0x800000) != 0;
                        goto case 7;
                    case 7:
                        *p++ = (value.UInt32_1 & 0x1000000) != 0;
                        goto case 6;
                    case 6:
                        *p++ = (value.UInt32_1 & 0x2000000) != 0;
                        goto case 5;
                    case 5:
                        *p++ = (value.UInt32_1 & 0x4000000) != 0;
                        goto case 4;
                    case 4:
                        *p++ = (value.UInt32_1 & 0x8000000) != 0;
                        goto case 3;
                    case 3:
                        *p++ = (value.UInt32_1 & 0x10000000) != 0;
                        goto case 2;
                    case 2:
                        *p++ = (value.UInt32_1 & 0x20000000) != 0;
                        goto case 1;
                    case 1:
                        *p++ = (value.UInt32_1 & 0x40000000) != 0;
                        break;
                }
            }
        }

        /// <summary>
        /// Mixes random <see cref="System.Boolean"/> values into an array using exclusive-OR operation.
        /// </summary>
        public void XorFill(Boolean[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            XorFill(array, 0, array.Length);
        }

        /// <summary>
        /// Mixes random <see cref="System.Boolean"/> values into an array using exclusive-OR operation.
        /// </summary>
        unsafe public void XorFill(Boolean[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            ValueUnion value = new();

            fixed (Boolean* ptr = &array[offset])
            {
                var p = ptr;

                while (count >= 64)
                {
                    Next(ref value);
                    *p++ ^= (value.UInt32_0 & 0x1) != 0;
                    *p++ ^= (value.UInt32_0 & 0x2) != 0;
                    *p++ ^= (value.UInt32_0 & 0x4) != 0;
                    *p++ ^= (value.UInt32_0 & 0x8) != 0;
                    *p++ ^= (value.UInt32_0 & 0x10) != 0;
                    *p++ ^= (value.UInt32_0 & 0x20) != 0;
                    *p++ ^= (value.UInt32_0 & 0x40) != 0;
                    *p++ ^= (value.UInt32_0 & 0x80) != 0;
                    *p++ ^= (value.UInt32_0 & 0x100) != 0;
                    *p++ ^= (value.UInt32_0 & 0x200) != 0;
                    *p++ ^= (value.UInt32_0 & 0x400) != 0;
                    *p++ ^= (value.UInt32_0 & 0x800) != 0;
                    *p++ ^= (value.UInt32_0 & 0x1000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x2000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x4000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x8000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x10000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x20000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x40000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x80000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x100000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x200000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x400000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x800000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x1000000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x2000000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x4000000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x8000000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x10000000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x20000000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x40000000) != 0;
                    *p++ ^= (value.UInt32_0 & 0x80000000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x1) != 0;
                    *p++ ^= (value.UInt32_1 & 0x2) != 0;
                    *p++ ^= (value.UInt32_1 & 0x4) != 0;
                    *p++ ^= (value.UInt32_1 & 0x8) != 0;
                    *p++ ^= (value.UInt32_1 & 0x10) != 0;
                    *p++ ^= (value.UInt32_1 & 0x20) != 0;
                    *p++ ^= (value.UInt32_1 & 0x40) != 0;
                    *p++ ^= (value.UInt32_1 & 0x80) != 0;
                    *p++ ^= (value.UInt32_1 & 0x100) != 0;
                    *p++ ^= (value.UInt32_1 & 0x200) != 0;
                    *p++ ^= (value.UInt32_1 & 0x400) != 0;
                    *p++ ^= (value.UInt32_1 & 0x800) != 0;
                    *p++ ^= (value.UInt32_1 & 0x1000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x2000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x4000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x8000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x10000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x20000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x40000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x80000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x100000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x200000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x400000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x800000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x1000000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x2000000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x4000000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x8000000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x10000000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x20000000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x40000000) != 0;
                    *p++ ^= (value.UInt32_1 & 0x80000000) != 0;
                    count -= 64;
                }

                if (count == 0)
                    return;

                Next(ref value);

                switch (count)
                {
                    case 63:
                        *p++ ^= (value.UInt32_0 & 0x1) != 0;
                        goto case 62;
                    case 62:
                        *p++ ^= (value.UInt32_0 & 0x2) != 0;
                        goto case 61;
                    case 61:
                        *p++ ^= (value.UInt32_0 & 0x4) != 0;
                        goto case 60;
                    case 60:
                        *p++ ^= (value.UInt32_0 & 0x8) != 0;
                        goto case 59;
                    case 59:
                        *p++ ^= (value.UInt32_0 & 0x10) != 0;
                        goto case 58;
                    case 58:
                        *p++ ^= (value.UInt32_0 & 0x20) != 0;
                        goto case 57;
                    case 57:
                        *p++ ^= (value.UInt32_0 & 0x40) != 0;
                        goto case 56;
                    case 56:
                        *p++ ^= (value.UInt32_0 & 0x80) != 0;
                        goto case 55;
                    case 55:
                        *p++ ^= (value.UInt32_0 & 0x100) != 0;
                        goto case 54;
                    case 54:
                        *p++ ^= (value.UInt32_0 & 0x200) != 0;
                        goto case 53;
                    case 53:
                        *p++ ^= (value.UInt32_0 & 0x400) != 0;
                        goto case 52;
                    case 52:
                        *p++ ^= (value.UInt32_0 & 0x800) != 0;
                        goto case 51;
                    case 51:
                        *p++ ^= (value.UInt32_0 & 0x1000) != 0;
                        goto case 50;
                    case 50:
                        *p++ ^= (value.UInt32_0 & 0x2000) != 0;
                        goto case 49;
                    case 49:
                        *p++ ^= (value.UInt32_0 & 0x4000) != 0;
                        goto case 48;
                    case 48:
                        *p++ ^= (value.UInt32_0 & 0x8000) != 0;
                        goto case 47;
                    case 47:
                        *p++ ^= (value.UInt32_0 & 0x10000) != 0;
                        goto case 46;
                    case 46:
                        *p++ ^= (value.UInt32_0 & 0x20000) != 0;
                        goto case 45;
                    case 45:
                        *p++ ^= (value.UInt32_0 & 0x40000) != 0;
                        goto case 44;
                    case 44:
                        *p++ ^= (value.UInt32_0 & 0x80000) != 0;
                        goto case 43;
                    case 43:
                        *p++ ^= (value.UInt32_0 & 0x100000) != 0;
                        goto case 42;
                    case 42:
                        *p++ ^= (value.UInt32_0 & 0x200000) != 0;
                        goto case 41;
                    case 41:
                        *p++ ^= (value.UInt32_0 & 0x400000) != 0;
                        goto case 40;
                    case 40:
                        *p++ ^= (value.UInt32_0 & 0x800000) != 0;
                        goto case 39;
                    case 39:
                        *p++ ^= (value.UInt32_0 & 0x1000000) != 0;
                        goto case 38;
                    case 38:
                        *p++ ^= (value.UInt32_0 & 0x2000000) != 0;
                        goto case 37;
                    case 37:
                        *p++ ^= (value.UInt32_0 & 0x4000000) != 0;
                        goto case 36;
                    case 36:
                        *p++ ^= (value.UInt32_0 & 0x8000000) != 0;
                        goto case 35;
                    case 35:
                        *p++ ^= (value.UInt32_0 & 0x10000000) != 0;
                        goto case 34;
                    case 34:
                        *p++ ^= (value.UInt32_0 & 0x20000000) != 0;
                        goto case 33;
                    case 33:
                        *p++ ^= (value.UInt32_0 & 0x40000000) != 0;
                        goto case 32;
                    case 32:
                        *p++ ^= (value.UInt32_0 & 0x80000000) != 0;
                        goto case 31;
                    case 31:
                        *p++ ^= (value.UInt32_1 & 0x1) != 0;
                        goto case 30;
                    case 30:
                        *p++ ^= (value.UInt32_1 & 0x2) != 0;
                        goto case 29;
                    case 29:
                        *p++ ^= (value.UInt32_1 & 0x4) != 0;
                        goto case 28;
                    case 28:
                        *p++ ^= (value.UInt32_1 & 0x8) != 0;
                        goto case 27;
                    case 27:
                        *p++ ^= (value.UInt32_1 & 0x10) != 0;
                        goto case 26;
                    case 26:
                        *p++ ^= (value.UInt32_1 & 0x20) != 0;
                        goto case 25;
                    case 25:
                        *p++ ^= (value.UInt32_1 & 0x40) != 0;
                        goto case 24;
                    case 24:
                        *p++ ^= (value.UInt32_1 & 0x80) != 0;
                        goto case 23;
                    case 23:
                        *p++ ^= (value.UInt32_1 & 0x100) != 0;
                        goto case 22;
                    case 22:
                        *p++ ^= (value.UInt32_1 & 0x200) != 0;
                        goto case 21;
                    case 21:
                        *p++ ^= (value.UInt32_1 & 0x400) != 0;
                        goto case 20;
                    case 20:
                        *p++ ^= (value.UInt32_1 & 0x800) != 0;
                        goto case 19;
                    case 19:
                        *p++ ^= (value.UInt32_1 & 0x1000) != 0;
                        goto case 18;
                    case 18:
                        *p++ ^= (value.UInt32_1 & 0x2000) != 0;
                        goto case 17;
                    case 17:
                        *p++ ^= (value.UInt32_1 & 0x4000) != 0;
                        goto case 16;
                    case 16:
                        *p++ ^= (value.UInt32_1 & 0x8000) != 0;
                        goto case 15;
                    case 15:
                        *p++ ^= (value.UInt32_1 & 0x10000) != 0;
                        goto case 14;
                    case 14:
                        *p++ ^= (value.UInt32_1 & 0x20000) != 0;
                        goto case 13;
                    case 13:
                        *p++ ^= (value.UInt32_1 & 0x40000) != 0;
                        goto case 12;
                    case 12:
                        *p++ ^= (value.UInt32_1 & 0x80000) != 0;
                        goto case 11;
                    case 11:
                        *p++ ^= (value.UInt32_1 & 0x100000) != 0;
                        goto case 10;
                    case 10:
                        *p++ ^= (value.UInt32_1 & 0x200000) != 0;
                        goto case 9;
                    case 9:
                        *p++ ^= (value.UInt32_1 & 0x400000) != 0;
                        goto case 8;
                    case 8:
                        *p++ ^= (value.UInt32_1 & 0x800000) != 0;
                        goto case 7;
                    case 7:
                        *p++ ^= (value.UInt32_1 & 0x1000000) != 0;
                        goto case 6;
                    case 6:
                        *p++ ^= (value.UInt32_1 & 0x2000000) != 0;
                        goto case 5;
                    case 5:
                        *p++ ^= (value.UInt32_1 & 0x4000000) != 0;
                        goto case 4;
                    case 4:
                        *p++ ^= (value.UInt32_1 & 0x8000000) != 0;
                        goto case 3;
                    case 3:
                        *p++ ^= (value.UInt32_1 & 0x10000000) != 0;
                        goto case 2;
                    case 2:
                        *p++ ^= (value.UInt32_1 & 0x20000000) != 0;
                        goto case 1;
                    case 1:
                        *p++ ^= (value.UInt32_1 & 0x40000000) != 0;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets a sequence of Boolean values.
        /// </summary>
        public IEnumerable<Boolean> Booleans()
        {
            ValueUnion value = new();

            while (true)
            {
                Next(ref value);
                yield return (value.UInt32_0 & 0x1) != 0;
                yield return (value.UInt32_0 & 0x2) != 0;
                yield return (value.UInt32_0 & 0x4) != 0;
                yield return (value.UInt32_0 & 0x8) != 0;
                yield return (value.UInt32_0 & 0x10) != 0;
                yield return (value.UInt32_0 & 0x20) != 0;
                yield return (value.UInt32_0 & 0x40) != 0;
                yield return (value.UInt32_0 & 0x80) != 0;
                yield return (value.UInt32_0 & 0x100) != 0;
                yield return (value.UInt32_0 & 0x200) != 0;
                yield return (value.UInt32_0 & 0x400) != 0;
                yield return (value.UInt32_0 & 0x800) != 0;
                yield return (value.UInt32_0 & 0x1000) != 0;
                yield return (value.UInt32_0 & 0x2000) != 0;
                yield return (value.UInt32_0 & 0x4000) != 0;
                yield return (value.UInt32_0 & 0x8000) != 0;
                yield return (value.UInt32_0 & 0x10000) != 0;
                yield return (value.UInt32_0 & 0x20000) != 0;
                yield return (value.UInt32_0 & 0x40000) != 0;
                yield return (value.UInt32_0 & 0x80000) != 0;
                yield return (value.UInt32_0 & 0x100000) != 0;
                yield return (value.UInt32_0 & 0x200000) != 0;
                yield return (value.UInt32_0 & 0x400000) != 0;
                yield return (value.UInt32_0 & 0x800000) != 0;
                yield return (value.UInt32_0 & 0x1000000) != 0;
                yield return (value.UInt32_0 & 0x2000000) != 0;
                yield return (value.UInt32_0 & 0x4000000) != 0;
                yield return (value.UInt32_0 & 0x8000000) != 0;
                yield return (value.UInt32_0 & 0x10000000) != 0;
                yield return (value.UInt32_0 & 0x20000000) != 0;
                yield return (value.UInt32_0 & 0x40000000) != 0;
                yield return (value.UInt32_0 & 0x80000000) != 0;
                yield return (value.UInt32_1 & 0x1) != 0;
                yield return (value.UInt32_1 & 0x2) != 0;
                yield return (value.UInt32_1 & 0x4) != 0;
                yield return (value.UInt32_1 & 0x8) != 0;
                yield return (value.UInt32_1 & 0x10) != 0;
                yield return (value.UInt32_1 & 0x20) != 0;
                yield return (value.UInt32_1 & 0x40) != 0;
                yield return (value.UInt32_1 & 0x80) != 0;
                yield return (value.UInt32_1 & 0x100) != 0;
                yield return (value.UInt32_1 & 0x200) != 0;
                yield return (value.UInt32_1 & 0x400) != 0;
                yield return (value.UInt32_1 & 0x800) != 0;
                yield return (value.UInt32_1 & 0x1000) != 0;
                yield return (value.UInt32_1 & 0x2000) != 0;
                yield return (value.UInt32_1 & 0x4000) != 0;
                yield return (value.UInt32_1 & 0x8000) != 0;
                yield return (value.UInt32_1 & 0x10000) != 0;
                yield return (value.UInt32_1 & 0x20000) != 0;
                yield return (value.UInt32_1 & 0x40000) != 0;
                yield return (value.UInt32_1 & 0x80000) != 0;
                yield return (value.UInt32_1 & 0x100000) != 0;
                yield return (value.UInt32_1 & 0x200000) != 0;
                yield return (value.UInt32_1 & 0x400000) != 0;
                yield return (value.UInt32_1 & 0x800000) != 0;
                yield return (value.UInt32_1 & 0x1000000) != 0;
                yield return (value.UInt32_1 & 0x2000000) != 0;
                yield return (value.UInt32_1 & 0x4000000) != 0;
                yield return (value.UInt32_1 & 0x8000000) != 0;
                yield return (value.UInt32_1 & 0x10000000) != 0;
                yield return (value.UInt32_1 & 0x20000000) != 0;
                yield return (value.UInt32_1 & 0x40000000) != 0;
                yield return (value.UInt32_1 & 0x80000000) != 0;
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Boolean"/>s and fill it with random values.
        /// </summary>
        public Boolean[] CreateBooleans(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Boolean[count];
            Fill(data);
            return data;
        }

        private void Next(ref ValueUnion value)
        {
            source.Next(ref value);
        }

        /// <summary>
        /// Gets a random <see cref="ValueUnion"/> value.
        /// </summary>
        public ValueUnion ValueUnion()
        {
            ValueUnion value = new();
            Next(ref value);
            return value;
        }

        /// <summary>
        /// Fill a provided array with random <see cref="ValueUnion"/> values.
        /// </summary>
        public void Fill(ValueUnion[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with random <see cref="ValueUnion"/> values.
        /// </summary>
        unsafe public void Fill(ValueUnion[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset > array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count == 0)
                return;

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            fixed (ValueUnion* ptr = &array[offset])
            {
                var p = (ValueUnion*)ptr;

                while (count >= 1)
                {
                    source.Next(ref *p++);
                    count -= 1;
                }
            }
        }

        /// <summary>
        /// Gets a sequence of ValueUnion values.
        /// </summary>
        public IEnumerable<ValueUnion> ValueUnions()
        {
            while(true)
            {
                yield return ValueUnion();
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="ValueUnion"/>s and fill it with random values.
        /// </summary>
        public ValueUnion[] CreateValueUnions(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new ValueUnion[count];
            Fill(data);
            return data;
        }

        private static void CreateState(IRandomSource source, byte[] seed, ValueUnion[] state)
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
                    return;

                case TypeCode.Int64:
                    CreateState(source, seed, state as Int64[]);
                    return;

                case TypeCode.UInt32:
                    CreateState(source, seed, state as UInt32[]);
                    return;

                case TypeCode.Int32:
                    CreateState(source, seed, state as Int32[]);
                    return;

                case TypeCode.UInt16:
                    CreateState(source, seed, state as UInt16[]);
                    return;

                case TypeCode.Int16:
                    CreateState(source, seed, state as Int16[]);
                    return;

                case TypeCode.Byte:
                    CreateState(source, seed, state as Byte[]);
                    return;

                case TypeCode.SByte:
                    CreateState(source, seed, state as SByte[]);
                    return;

                case TypeCode.Double:
                    CreateState(source, seed, state as Double[]);
                    return;

                case TypeCode.Single:
                    CreateState(source, seed, state as Single[]);
                    return;

                case TypeCode.Decimal:
                    CreateState(source, seed, state as Decimal[]);
                    return;

                case TypeCode.Object:
                    if (state is ValueUnion[])
                    {
                        CreateState(source, seed, state as ValueUnion[]);
                        return;
                    }
                    break;
            }

            throw new NotSupportedException("Unsupported array type.");
        }
    }
}
