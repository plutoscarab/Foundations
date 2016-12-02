
/*
MinTree.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations.Collections
{
    /// <summary>
    /// Sorted tree with minimum key at the top.
    /// </summary>
    public class MinTree<TKey, TValue> : SortedTree<TKey, TValue> 
        where TValue : IEquatable<TValue>
        where TKey : IComparable<TKey>
    {
        private static TKey Min(TKey x, TKey y)
        {
            if (x.CompareTo(y) < 0)
                return x;

            return y;
        }

        /// <summary>
        /// Creates a <see cref="Min(TKey, TKey)"/>.
        /// </summary>
        public MinTree()
            : base((x, y) => Min(x, y))
        {
        }
    }
}
