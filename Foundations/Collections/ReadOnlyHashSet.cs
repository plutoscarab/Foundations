
/*
ReadOnlyHashSet.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundations.Collections
{
    /// <summary>
    /// Represents a read-only set with O(1) lookup.
    /// </summary>
    public sealed partial class ReadOnlyHashSet<T> : IEnumerable<T>
    {
        HashSet<T> items;

        /// <summary>
        /// Creates a read-only set.
        /// </summary>
        public ReadOnlyHashSet(IEnumerable<T> items)
        {
            this.items = new HashSet<T>(items);
        }

        /// <summary>
        /// Creates a read-only set.
        /// </summary>
        public ReadOnlyHashSet(IEnumerable<T> items, IEqualityComparer<T> comparer)
        {
            this.items = new HashSet<T>(items, comparer);
        }

        /// <summary>
        /// Determines whether the set contains the specified item.
        /// </summary>
        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the items in the set.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Determines whether the specified object is equal to this object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return items.Equals(obj);
        }

        /// <summary>
        /// Get the hash code for the set.
        /// </summary>
        public override int GetHashCode()
        {
            return items.GetHashCode();
        }

        /// <summary>
        /// Gets a string representation of the set.
        /// </summary>
        public override string ToString()
        {
            var plural = items.Count == 1 ? "" : "s";
            return $"ReadOnlyHashSet of {items.Count} {typeof(T).Name} value{plural}.";
        }

        /// <summary>
        /// Gets the number of items in the set.
        /// </summary>
        public int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// Determines whether the set is a proper subset of the specified collection.
        /// </summary>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return items.IsProperSubsetOf(other);
        }

        /// <summary>
        /// Determines whether the set is a proper superset of the specified collection.
        /// </summary>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return items.IsProperSupersetOf(other);
        }

        /// <summary>
        /// Determines whether the set is a subset of the specified collection.
        /// </summary>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return items.IsSubsetOf(other);
        }

        /// <summary>
        /// Determines whether the set is a superset of the specified collection.
        /// </summary>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return items.IsSupersetOf(other);
        }

        /// <summary>
        /// Determines whether the set shares any elements with the specified collection.
        /// </summary>
        public bool Overlaps(IEnumerable<T> other)
        {
            return items.Overlaps(other);
        }

        /// <summary>
        /// Determines whether the set contains the same elements as the specified collection.
        /// </summary>
        public bool SetEquals(IEnumerable<T> other)
        {
            return items.SetEquals(other);
        }
    }
}