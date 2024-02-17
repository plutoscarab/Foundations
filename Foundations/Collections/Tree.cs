
/*
Tree.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundations.Collections
{
    /// <summary>
    /// Tree.
    /// </summary>
    public class Tree<TKey, TValue> where TValue : IEquatable<TValue>
    {
        Func<TKey, TKey, TKey> aggregator;

        /// <summary>
        /// Keys.
        /// </summary>
        protected List<TKey> keys;

        /// <summary>
        /// Values.
        /// </summary>
        protected Map<TValue, int> values;

        /// <summary>
        /// Creates an instance of <see cref="Tree{TKey, TValue}"/>.
        /// </summary>
        public Tree(Func<TKey, TKey, TKey> aggregator)
        {
            this.aggregator = aggregator;
            keys = new List<TKey>();
            values = new Map<TValue, int>();
        }

        /// <summary>
        /// Gets the count of items.
        /// </summary>
        public int Count
        {
            get { return (keys.Count + 1) / 2; }
        }

        /// <summary>
        /// Determines if the collection contains the specified value.
        /// </summary>
        public bool Contains(TValue value)
        {
            return values.Contains(value);
        }

        /// <summary>
        /// Add an item to the collection.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            if (keys.Count == 0)
            {
                keys.Add(key);
                values[0] = value;
                return;
            }

            int count = keys.Count;
            int parent = (count - 1) / 2;
            values[count] = values[parent];
            values[count + 1] = value;
            keys.Add(keys[parent]);
            keys.Add(key);

            while (true)
            {
                keys[parent] = aggregator(keys[parent * 2 + 1], keys[parent * 2 + 2]);
                if (parent == 0) break;
                parent = (parent - 1) / 2;
            }
        }

        private void AggregateKeys(int index)
        {
            while (index > 0)
            {
                index = (index - 1) / 2;
                keys[index] = aggregator(keys[index * 2 + 1], keys[index * 2 + 2]);
            }
        }

        /// <summary>
        /// Replaces an item in the collection.
        /// </summary>
        public void Replace(TKey key, TValue value)
        {
            int index = values[value];
            keys[index] = key;
            AggregateKeys(index);
        }

        private TValue ExtractLast(out TKey key)
        {
            int count = keys.Count - 1;
            key = keys[count];
            TValue value = values[count];
            values.Remove(count);

            if (count == 0)
            {
                keys.RemoveAt(0);
                return value;
            }

            int index = (count - 1) / 2;
            values[index] = values[count - 1];
            keys[index] = keys[count - 1];
            keys.RemoveAt(count);
            keys.RemoveAt(count - 1);
            AggregateKeys(index);
            return value;
        }

        /// <summary>
        /// Remove an item from the collection.
        /// </summary>
        public void Remove(TValue value)
        {
            TValue last = ExtractLast(out TKey key);
            if (value.Equals(last)) return;
            int index = values[value];
            values[index] = last;
            keys[index] = key;
            AggregateKeys(index);
        }

        /// <summary>
        /// Determines of the collection is empty.
        /// </summary>
		public bool IsEmpty
		{
			get
			{
				return keys.Count == 0;
			}
		}

        public TKey RootKey
        {
            get
            {
                return keys[0];
            }
        }
	}
}