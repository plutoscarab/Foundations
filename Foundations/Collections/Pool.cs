
/*
Pool.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Concurrent;

namespace Foundations.Collections
{
    /// <summary>
    /// A pool of reusable instances of items that are expensive to create,
    /// expensive to garbage-collect, or prone to cause heap fragmentation.
    /// </summary>
    public sealed class Pool<T>
    {
        private static ConcurrentBag<T> pool = new ConcurrentBag<T>();

        /// <summary>
        /// Borrows an item from the pool. Use <see cref="Return"/> to return the item.
        /// If the pool is empty, use the constructor function to create a new instance.
        /// </summary>
        public T Borrow(Func<T> constructor)
        {
            T item;

            if (!pool.TryTake(out item))
            {
                item = constructor();
            }

            return item;
        }

        /// <summary>
        /// Returns an item to the pool.
        /// </summary>
        public void Return(T item)
        {
            pool.Add(item);
        }
    }
}