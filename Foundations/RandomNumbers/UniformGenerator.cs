﻿
using System;
using System.Collections.Generic;
using System.Numerics;
using Foundations.Types;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// Equidistributed number generator.
    /// </summary>
    public sealed partial class UniformGenerator
    {
        private IUniformSource source;
     
        /// <summary>
        /// Create an equidistributed number generator
        /// using the specified uniform source.
        /// </summary>
        public UniformGenerator(IUniformSource source)
        {
            this.source = source;
        }

        private UniformGenerator(IUniformSource source, bool throwIfNonClonable)
        {
            this.source = source.Clone();

            if (this.source ==  null && throwIfNonClonable)
                throw new ArgumentException("Source generator is using a source that cannot be cloned.");
        }

        /// <summary>
        /// Create an equidistributed number generator with the same state as an existing one.
        /// </summary>
        public UniformGenerator(UniformGenerator generator)
            : this(generator.source, true)
        {
        }

        /// <summary>
        /// Gets a copy of this <see cref="UniformGenerator"/> with the same state.
        /// </summary>
        public UniformGenerator Clone()
        {
            var source = this.source.Clone();
            if (source == null) return null;
            return new UniformGenerator(source, false);
        }

        private static BigInteger TwoTo64 = BigInteger.Pow(2, 64);

        /// <summary>
        /// Gets an equidistributed <see cref="System.UInt64"/> value.
        /// </summary>
        public UInt64 UInt64()
        {
            return (UInt64)source.Next();
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.UInt64"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public UInt64 UInt64(UInt64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (UInt64)((range * (UInt128)UInt64()) >> 64);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.UInt64"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.UInt64"/> values.
        /// </summary>
        public void Fill(UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.UInt64"/> values.
        /// </summary>
        public void Fill(UInt64 range, UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.UInt64"/> values.
        /// </summary>
        public void Fill(UInt64 minimum, UInt64 range, UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.UInt64"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = UInt64();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.UInt64"/> values.
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
        /// Adds equidistributed <see cref="System.UInt64"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(UInt64 range, UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.UInt64"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(UInt64 minimum, UInt64 range, UInt64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.UInt64"/> values in [minimum, minimum + range) to part of an array.
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
        /// Adds equidistributed <see cref="System.UInt64"/> values in [0, range) to part of an array.
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
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while (true)
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

            while (true)
            {
                yield return UInt64(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt64"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.UInt64"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.UInt64"/>s and fill it with equidistributed values in [minimum, minimum + range).
        /// </summary>
        public UInt64[] CreateUInt64s(int count, UInt64 minimum, UInt64 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt64[count];
            Fill(minimum, range, data);
            return data;
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Int64"/> value.
        /// </summary>
        public Int64 Int64()
        {
            return (Int64)(source.Next() >> 1);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Int64"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Int64 Int64(Int64 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Int64)((range * (long)(UInt64() >> 1)) >> 63);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Int64"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.Int64"/> values.
        /// </summary>
        public void Fill(Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Int64"/> values.
        /// </summary>
        public void Fill(Int64 range, Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Int64"/> values.
        /// </summary>
        public void Fill(Int64 minimum, Int64 range, Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Int64"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = Int64();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Int64"/> values.
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
        /// Adds equidistributed <see cref="System.Int64"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Int64 range, Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Int64"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Int64 minimum, Int64 range, Int64[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Int64"/> values in [minimum, minimum + range) to part of an array.
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
        /// Adds equidistributed <see cref="System.Int64"/> values in [0, range) to part of an array.
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
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while (true)
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

            while (true)
            {
                yield return Int64(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int64"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.Int64"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.Int64"/>s and fill it with equidistributed values in [minimum, minimum + range).
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
        /// Gets an equidistributed <see cref="System.UInt32"/> value.
        /// </summary>
        public UInt32 UInt32()
        {
            return (UInt32)(source.Next() >> 32);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.UInt32"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public UInt32 UInt32(UInt32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (UInt32)((range * (ulong)(UInt64() >> 32)) >> 32);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.UInt32"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.UInt32"/> values.
        /// </summary>
        public void Fill(UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.UInt32"/> values.
        /// </summary>
        public void Fill(UInt32 range, UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.UInt32"/> values.
        /// </summary>
        public void Fill(UInt32 minimum, UInt32 range, UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.UInt32"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = UInt32();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.UInt32"/> values.
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
        /// Adds equidistributed <see cref="System.UInt32"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(UInt32 range, UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.UInt32"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(UInt32 minimum, UInt32 range, UInt32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.UInt32"/> values in [minimum, minimum + range) to part of an array.
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

            while (count-- > 0)
            {
                array[offset++] += UInt32(minimum, range);
            }
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.UInt32"/> values in [0, range) to part of an array.
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
            while (true)
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

            while (true)
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

            while (true)
            {
                yield return UInt32(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt32"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.UInt32"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.UInt32"/>s and fill it with equidistributed values in [minimum, minimum + range).
        /// </summary>
        public UInt32[] CreateUInt32s(int count, UInt32 minimum, UInt32 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt32[count];
            Fill(minimum, range, data);
            return data;
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Int32"/> value.
        /// </summary>
        public Int32 Int32()
        {
            return (Int32)(source.Next() >> 33);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Int32"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Int32 Int32(Int32 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Int32)((range * (long)(UInt64() >> 33)) >> 31);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Int32"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.Int32"/> values.
        /// </summary>
        public void Fill(Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Int32"/> values.
        /// </summary>
        public void Fill(Int32 range, Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Int32"/> values.
        /// </summary>
        public void Fill(Int32 minimum, Int32 range, Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Int32"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = Int32();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Int32"/> values.
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
        /// Adds equidistributed <see cref="System.Int32"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Int32 range, Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Int32"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Int32 minimum, Int32 range, Int32[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Int32"/> values in [minimum, minimum + range) to part of an array.
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

            while (count-- > 0)
            {
                array[offset++] += Int32(minimum, range);
            }
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Int32"/> values in [0, range) to part of an array.
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
            while (true)
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

            while (true)
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

            while (true)
            {
                yield return Int32(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int32"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.Int32"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.Int32"/>s and fill it with equidistributed values in [minimum, minimum + range).
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
        /// Gets an equidistributed <see cref="System.UInt16"/> value.
        /// </summary>
        public UInt16 UInt16()
        {
            return (UInt16)(source.Next() >> 48);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.UInt16"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public UInt16 UInt16(UInt16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (UInt16)((range * (ulong)(UInt64() >> 48)) >> 16);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.UInt16"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.UInt16"/> values.
        /// </summary>
        public void Fill(UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.UInt16"/> values.
        /// </summary>
        public void Fill(UInt16 range, UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.UInt16"/> values.
        /// </summary>
        public void Fill(UInt16 minimum, UInt16 range, UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.UInt16"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = UInt16();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.UInt16"/> values.
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
        /// Adds equidistributed <see cref="System.UInt16"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(UInt16 range, UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.UInt16"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(UInt16 minimum, UInt16 range, UInt16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.UInt16"/> values in [minimum, minimum + range) to part of an array.
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

            while (count-- > 0)
            {
                array[offset++] += UInt16(minimum, range);
            }
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.UInt16"/> values in [0, range) to part of an array.
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
            while (true)
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

            while (true)
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

            while (true)
            {
                yield return UInt16(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.UInt16"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.UInt16"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.UInt16"/>s and fill it with equidistributed values in [minimum, minimum + range).
        /// </summary>
        public UInt16[] CreateUInt16s(int count, UInt16 minimum, UInt16 range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new UInt16[count];
            Fill(minimum, range, data);
            return data;
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Int16"/> value.
        /// </summary>
        public Int16 Int16()
        {
            return (Int16)(source.Next() >> 49);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Int16"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Int16 Int16(Int16 range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Int16)((range * (long)(UInt64() >> 49)) >> 15);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Int16"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.Int16"/> values.
        /// </summary>
        public void Fill(Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Int16"/> values.
        /// </summary>
        public void Fill(Int16 range, Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Int16"/> values.
        /// </summary>
        public void Fill(Int16 minimum, Int16 range, Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Int16"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = Int16();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Int16"/> values.
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
        /// Adds equidistributed <see cref="System.Int16"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Int16 range, Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Int16"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Int16 minimum, Int16 range, Int16[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Int16"/> values in [minimum, minimum + range) to part of an array.
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

            while (count-- > 0)
            {
                array[offset++] += Int16(minimum, range);
            }
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Int16"/> values in [0, range) to part of an array.
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
            while (true)
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

            while (true)
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

            while (true)
            {
                yield return Int16(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Int16"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.Int16"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.Int16"/>s and fill it with equidistributed values in [minimum, minimum + range).
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
        /// Gets an equidistributed <see cref="System.Byte"/> value.
        /// </summary>
        public Byte Byte()
        {
            return (Byte)(source.Next() >> 56);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Byte"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Byte Byte(Byte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Byte)((range * (ulong)(UInt64() >> 56)) >> 8);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Byte"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.Byte"/> values.
        /// </summary>
        public void Fill(Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Byte"/> values.
        /// </summary>
        public void Fill(Byte range, Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Byte"/> values.
        /// </summary>
        public void Fill(Byte minimum, Byte range, Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Byte"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = Byte();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Byte"/> values.
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
        /// Adds equidistributed <see cref="System.Byte"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Byte range, Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Byte"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Byte minimum, Byte range, Byte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Byte"/> values in [minimum, minimum + range) to part of an array.
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

            while (count-- > 0)
            {
                array[offset++] += Byte(minimum, range);
            }
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Byte"/> values in [0, range) to part of an array.
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
            while (true)
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

            while (true)
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

            while (true)
            {
                yield return Byte(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Byte"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.Byte"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.Byte"/>s and fill it with equidistributed values in [minimum, minimum + range).
        /// </summary>
        public Byte[] CreateBytes(int count, Byte minimum, Byte range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Byte[count];
            Fill(minimum, range, data);
            return data;
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.SByte"/> value.
        /// </summary>
        public SByte SByte()
        {
            return (SByte)(source.Next() >> 57);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.SByte"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public SByte SByte(SByte range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (SByte)((range * (long)(UInt64() >> 57)) >> 7);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.SByte"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.SByte"/> values.
        /// </summary>
        public void Fill(SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.SByte"/> values.
        /// </summary>
        public void Fill(SByte range, SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.SByte"/> values.
        /// </summary>
        public void Fill(SByte minimum, SByte range, SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.SByte"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = SByte();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.SByte"/> values.
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
        /// Adds equidistributed <see cref="System.SByte"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(SByte range, SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.SByte"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(SByte minimum, SByte range, SByte[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.SByte"/> values in [minimum, minimum + range) to part of an array.
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

            while (count-- > 0)
            {
                array[offset++] += SByte(minimum, range);
            }
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.SByte"/> values in [0, range) to part of an array.
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
            while (true)
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

            while (true)
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

            while (true)
            {
                yield return SByte(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.SByte"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.SByte"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.SByte"/>s and fill it with equidistributed values in [minimum, minimum + range).
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
        /// Gets an equidistributed <see cref="System.Double"/> value.
        /// </summary>
        public Double Double()
        {
            return (Double)(BitConverter.Int64BitsToDouble(((Int64() >> 11) & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Double"/> value.
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
        /// Gets an equidistributed <see cref="System.Double"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.Double"/> values.
        /// </summary>
        public void Fill(Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Double"/> values.
        /// </summary>
        public void Fill(Double range, Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Double"/> values.
        /// </summary>
        public void Fill(Double minimum, Double range, Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Double"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = Double();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Double"/> values.
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
        /// Adds equidistributed <see cref="System.Double"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Double range, Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Double"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Double minimum, Double range, Double[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Double"/> values in [minimum, minimum + range) to part of an array.
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
        /// Adds equidistributed <see cref="System.Double"/> values in [0, range) to part of an array.
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
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while (true)
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

            while (true)
            {
                yield return Double(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Double"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.Double"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.Double"/>s and fill it with equidistributed values in [minimum, minimum + range).
        /// </summary>
        public Double[] CreateDoubles(int count, Double minimum, Double range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Double[count];
            Fill(minimum, range, data);
            return data;
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Single"/> value.
        /// </summary>
        public Single Single()
        {
            return (Single)(BitConverter.Int64BitsToDouble(((Int64() >> 11) & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000) - 1.0);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="System.Single"/> value.
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
        /// Gets an equidistributed <see cref="System.Single"/> value.
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
        /// Fill a provided array with equidistributed <see cref="System.Single"/> values.
        /// </summary>
        public void Fill(Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Single"/> values.
        /// </summary>
        public void Fill(Single range, Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="System.Single"/> values.
        /// </summary>
        public void Fill(Single minimum, Single range, Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Single"/> values.
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

            while (count-- > 0)
            {
                array[offset++] = Single();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="System.Single"/> values.
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
        /// Adds equidistributed <see cref="System.Single"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Single range, Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Single"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Single minimum, Single range, Single[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Single"/> values in [minimum, minimum + range) to part of an array.
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

            while (count-- > 0)
            {
                array[offset++] += Single(minimum, range);
            }
        }

        /// <summary>
        /// Adds equidistributed <see cref="System.Single"/> values in [0, range) to part of an array.
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
            while (true)
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

            while (true)
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

            while (true)
            {
                yield return Single(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="System.Single"/>s and fill it with equidistributed values.
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
        /// Allocate an array of <see cref="System.Single"/>s and fill it with equidistributed values in [0, range).
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
        /// Allocate an array of <see cref="System.Single"/>s and fill it with equidistributed values in [minimum, minimum + range).
        /// </summary>
        public Single[] CreateSingles(int count, Single minimum, Single range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Single[count];
            Fill(minimum, range, data);
            return data;
        }

        /// <summary>
        /// Gets an equidistributed <see cref="Foundations.Types.Rational"/> value.
        /// </summary>
        public Rational Rational()
        {
            return new Rational(source.Next(), TwoTo64);
        }

        /// <summary>
        /// Gets an equidistributed <see cref="Foundations.Types.Rational"/> value.
        /// </summary>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between 0 (inclusive) and range (exclusive).</returns>
        public Rational Rational(Rational range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return range * Rational();
        }

        /// <summary>
        /// Gets an equidistributed <see cref="Foundations.Types.Rational"/> value.
        /// </summary>
        /// <param ref="minimum">The minimum value to return.</param>
        /// <param ref="range">The range of values to return.</param>
        /// <returns>Returns a value between minimum (inclusive) and minimum+range (exclusive).</returns>
        public Rational Rational(Rational minimum, Rational range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            return (Rational)(minimum + Rational(range));
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="Foundations.Types.Rational"/> values.
        /// </summary>
        public void Fill(Rational[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="Foundations.Types.Rational"/> values.
        /// </summary>
        public void Fill(Rational range, Rational[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array with equidistributed <see cref="Foundations.Types.Rational"/> values.
        /// </summary>
        public void Fill(Rational minimum, Rational range, Rational[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            Fill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="Foundations.Types.Rational"/> values.
        /// </summary>
        unsafe public void Fill(Rational[] array, int offset, int count)
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
                array[offset++] = Rational();
            }
        }

        /// <summary>
        /// Fill a specified portion of a provided array with equidistributed <see cref="Foundations.Types.Rational"/> values.
        /// </summary>
        public void Fill(Rational minimum, Rational range, Rational[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] = Rational(minimum, range);
            }
        }

        /// <summary>
        /// Adds equidistributed <see cref="Foundations.Types.Rational"/> values in [0, range) to values in an array.
        /// </summary>
        public void AddFill(Rational range, Rational[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(0, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="Foundations.Types.Rational"/> values in [minimum, minimum + range) to values in an array.
        /// </summary>
        public void AddFill(Rational minimum, Rational range, Rational[] array)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            AddFill(minimum, range, array, 0, array.Length);
        }

        /// <summary>
        /// Adds equidistributed <see cref="Foundations.Types.Rational"/> values in [minimum, minimum + range) to part of an array.
        /// </summary>
        public void AddFill(Rational minimum, Rational range, Rational[] array, int offset, int count)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count-- > 0)
            {
                array[offset++] += Rational(minimum, range);
            }
        }

        /// <summary>
        /// Adds equidistributed <see cref="Foundations.Types.Rational"/> values in [0, range) to part of an array.
        /// </summary>
        public void AddFill(Rational range, Rational[] array, int offset, int count)
        {
            AddFill(0, range, array, offset, count);
        }

        /// <summary>
        /// Gets a sequence of Rational values.
        /// </summary>
        public IEnumerable<Rational> Rationals()
        {
            while (true)
            {
                yield return Rational();
            }
        }

        /// <summary>
        /// Gets a sequence of Rational values in [0, range).
        /// </summary>
        public IEnumerable<Rational> Rationals(Rational range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while (true)
            {
                yield return Rational(range);
            }
        }

        /// <summary>
        /// Gets a sequence of Rational values in [minimum, minimum + range).
        /// </summary>
        public IEnumerable<Rational> Rationals(Rational minimum, Rational range)
        {
            if (range <= 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            while (true)
            {
                yield return Rational(minimum, range);
            }
        }

        /// <summary>
        /// Allocate an array of <see cref="Foundations.Types.Rational"/>s and fill it with equidistributed values.
        /// </summary>
        public Rational[] CreateRationals(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Rational[count];
            Fill(data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="Foundations.Types.Rational"/>s and fill it with equidistributed values in [0, range).
        /// </summary>
        public Rational[] CreateRationals(int count, Rational range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Rational[count];
            Fill(range, data);
            return data;
        }

        /// <summary>
        /// Allocate an array of <see cref="Foundations.Types.Rational"/>s and fill it with equidistributed values in [minimum, minimum + range).
        /// </summary>
        public Rational[] CreateRationals(int count, Rational minimum, Rational range)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var data = new Rational[count];
            Fill(minimum, range, data);
            return data;
        }
    }
}
