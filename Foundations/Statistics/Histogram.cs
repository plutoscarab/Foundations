﻿
/*
Histogram.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

THIS IS AN AUTO-GENERATED SOURCE FILE. DO NOT EDIT THIS FILE DIRECTLY.
INSTEAD, EDIT THE .tt FILE WITH THE SAME NAME AND RE-RUN THE TEXT TEMPLATING
FILE GENERATOR. IF YOU SAVE THE FILE IN VISUAL STUDIO IT WILL DO THIS FOR YOU.
*/

using System;
using System.Collections.Generic;

namespace Foundations.Statistics
{
    /// <summary>
    /// Counts the number of occurrances of each value.
    /// </summary>
    public static partial class Histogram
    {
        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<sbyte> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<sbyte> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<sbyte> list, int binCount, out sbyte min, out sbyte max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = sbyte.MaxValue;
            max = sbyte.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(((_ - minClosure) * (sbyte)binCount + range - 1) / range));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<sbyte> items, int binCount, sbyte max)
        {
            if (max == sbyte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32((_ * (sbyte)binCount + max) / (max + 1)));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<byte> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<byte> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<byte> list, int binCount, out byte min, out byte max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = byte.MaxValue;
            max = byte.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(((_ - minClosure) * (byte)binCount + range - 1) / range));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<byte> items, int binCount, byte max)
        {
            if (max == byte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32((_ * (byte)binCount + max) / (max + 1)));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<short> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<short> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<short> list, int binCount, out short min, out short max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = short.MaxValue;
            max = short.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(((_ - minClosure) * (short)binCount + range - 1) / range));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<short> items, int binCount, short max)
        {
            if (max == short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32((_ * (short)binCount + max) / (max + 1)));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<ushort> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<ushort> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<ushort> list, int binCount, out ushort min, out ushort max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = ushort.MaxValue;
            max = ushort.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(((_ - minClosure) * (ushort)binCount + range - 1) / range));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<ushort> items, int binCount, ushort max)
        {
            if (max == ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32((_ * (ushort)binCount + max) / (max + 1)));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<int> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<int> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<int> list, int binCount, out int min, out int max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = int.MaxValue;
            max = int.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(((_ - minClosure) * (int)binCount + range - 1) / range));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<int> items, int binCount, int max)
        {
            if (max == int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32((_ * (int)binCount + max) / (max + 1)));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<uint> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<uint> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<uint> list, int binCount, out uint min, out uint max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = uint.MaxValue;
            max = uint.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(((_ - minClosure) * (uint)binCount + range - 1) / range));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<uint> items, int binCount, uint max)
        {
            if (max == uint.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32((_ * (uint)binCount + max) / (max + 1)));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<long> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<long> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<long> list, int binCount, out long min, out long max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = long.MaxValue;
            max = long.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(((_ - minClosure) * (long)binCount + range - 1) / range));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<long> items, int binCount, long max)
        {
            if (max == long.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32((_ * (long)binCount + max) / (max + 1)));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<ulong> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<ulong> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<ulong> list, int binCount, out ulong min, out ulong max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = ulong.MaxValue;
            max = ulong.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(((_ - minClosure) * (ulong)binCount + range - 1) / range));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<ulong> items, int binCount, ulong max)
        {
            if (max == ulong.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32((_ * (ulong)binCount + max) / (max + 1)));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<float> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<float> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<float> list, int binCount, out float min, out float max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = float.MaxValue;
            max = float.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(Math.Floor(((_ - minClosure) * (float)binCount + range - 1) / range)));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<float> items, int binCount, float max)
        {
            if (max == float.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32(Math.Floor((_ * (float)binCount + max) / (max + 1))));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<double> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<double> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<double> list, int binCount, out double min, out double max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = double.MaxValue;
            max = double.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(Math.Floor(((_ - minClosure) * (double)binCount + range - 1) / range)));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<double> items, int binCount, double max)
        {
            if (max == double.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32(Math.Floor((_ * (double)binCount + max) / (max + 1))));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<decimal> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<decimal> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<decimal> list, int binCount, out decimal min, out decimal max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = decimal.MaxValue;
            max = decimal.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(Math.Floor(((_ - minClosure) * (decimal)binCount + range - 1) / range)));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<decimal> items, int binCount, decimal max)
        {
            if (max == decimal.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32(Math.Floor((_ * (decimal)binCount + max) / (max + 1))));
        }

        /// <summary>
        /// Count the number of occurrances of each non-negative value.
        /// </summary>
        public static int[] From(IEnumerable<char> items)
        {
            return From(items, null, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances of each value in the range [0, binCount).
        /// </summary>
        public static int[] From(IEnumerable<char> items, int binCount)
        {
            return From(items, binCount, Convert.ToInt32);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. The bins are as equally-sized as possible
        /// according to the range of values found in the list.
        /// </summary>
        public static int[] From(IList<char> list, int binCount, out char min, out char max)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            min = char.MaxValue;
            max = char.MinValue;

            foreach (var item in list)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            var range = max - min + (int)1;
            var minClosure = min;
            return From(list, binCount, _ => Convert.ToInt32(((_ - minClosure) * (char)binCount + range - 1) / range));
        }

        /// <summary>
        /// Count the number of occurrances in each bin.
        /// </summary>
        public static int[] From(IEnumerable<char> items, int binCount, char max)
        {
            if (max == char.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(max));

            return From(items, binCount, _ => Convert.ToInt32((_ * (char)binCount + max) / (max + 1)));
        }


        /// <summary>
        /// Count the number of occurrances in each bin. Each value is assigned to a bin using
        /// the binning function.
        /// </summary>
        public static int[] From<T>(IEnumerable<T> items, Func<T, int> binningFunction)
        {
            return From(items, null, binningFunction);
        }

        /// <summary>
        /// Count the number of occurrances in each bin. Each value is assigned to a bin using
        /// the binning function.
        /// </summary>
        public static int[] From<T>(IEnumerable<T> items, int? binCount, Func<T, int> binningFunction)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (binningFunction == null)
                throw new ArgumentNullException(nameof(binningFunction));

            var hist = new int[binCount == null ? 0 : binCount.Value];

            foreach (var item in items)
            {
                int value = binningFunction(item);

                if (value < 0)
                    throw new NotSupportedException("Result of binning function must be non-negative.");

                if (hist.Length <= value)
                {
                    if (binCount == null)
                    {
                        Array.Resize(ref hist, value + 1);
                    }
                    else
                    {
                        throw new NotSupportedException("Result of binning function must be less than bin count.");
                    }
                }

                hist[value]++;
            }

            return hist;
        }
    }
}
