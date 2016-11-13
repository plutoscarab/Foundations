
/*
IEnumerableExtensions.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundations
{
    /// <summary>
    /// Extensions for IEnumerable{T}.
    /// </summary>
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Copy items to an array.
        /// </summary>
        public static void CopyTo<T>(this IEnumerable<T> items, T[] array)
        {
            items.CopyTo(array, 0, array.Length);
        }

        /// <summary>
        /// Copy items to an array.
        /// </summary>
        public static void CopyTo<T>(this IEnumerable<T> items, T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex, array.Length);
        }

        /// <summary>
        /// Copy items to an array.
        /// </summary>
        public static void CopyTo<T>(this IEnumerable<T> items, T[] array, int arrayIndex, int count)
        {
            foreach (var item in items.Take(count))
            {
                array[arrayIndex++] = item;
            }
        }
    }
}