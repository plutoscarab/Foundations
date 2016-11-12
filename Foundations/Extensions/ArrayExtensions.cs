
/*
ArrayExtensions.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;

namespace Foundations
{
    /// <summary>
    /// Extension methods for <see cref="Array"/> types.
    /// </summary>
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Gets the contents of this array as an array of <see cref="Byte"/>s.
        /// </summary>
        public static byte[] GetBytes<T>(this T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int n = Buffer.ByteLength(array);
            var bytes = new byte[n];
            Buffer.BlockCopy(array, 0, bytes, 0, n);
            return bytes;
        }

        /// <summary>
        /// Gets the contents of this array as an array of <see cref="Byte"/>s.
        /// </summary>
        public static byte[] GetBytes<T>(this Array array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int n = Buffer.ByteLength(array);
            var bytes = new byte[n];
            Buffer.BlockCopy(array, 0, bytes, 0, n);
            return bytes;
        }
    }
}