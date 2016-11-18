
/*
RandomSourceExtensions.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// Extension methods for <see cref="IRandomSource"/>.
    /// </summary>
    public static class RandomSourceExtensions
    {
        /// <summary>
        /// Creates a thread-safe wrapper for an <see cref="IRandomSource"/>.
        /// </summary>
        /// <param name="source"></param>
       public static IRandomSource Synchronized(this IRandomSource source)
        {
            return new SynchronizedRandomSource(source);
        }
    }
}