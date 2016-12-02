
/*
PriorityQueue.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;

namespace Foundations.Collections
{
    /// <summary>
    /// Abstract base class for priority queues.
    /// </summary>
    /// <typeparam name="TValue">The type of the values contained in the queue.</typeparam>
    /// <typeparam name="TPriority">The type of the priority of each value in the queue.</typeparam>
    public abstract class PriorityQueue<TValue, TPriority>
        where TPriority : IComparable<TPriority>
        where TValue : IEquatable<TValue>
    {
        /// <summary>
        /// Sorted tree.
        /// </summary>
        protected SortedTree<TPriority, TValue> tree;

        /// <summary>
        /// Adds a value to the priority queue.
        /// </summary>
        public void Push(TValue value, TPriority key)
        {
            tree.Add(key, value);
        }

        /// <summary>
        /// Gets a key/value pair from the priority queue and removes it.
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<TPriority, TValue> Pop()
        {
            return tree.Pop();
        }

        /// <summary>
        /// Determines if the priority queue is empty.
        /// </summary>
		public bool IsEmpty
		{
			get
			{
				return tree.IsEmpty;
			}
		}

        /// <summary>
        /// Updates the priority of an item already in the queue.
        /// </summary>
        public void UpdatePriority(TValue value, TPriority key)
        {
            tree.Replace(key, value);
        }

        /// <summary>
        /// Removes an item from the priority queue.
        /// </summary>
        public void Remove(TValue value)
        {
            tree.Remove(value);
        }

        /// <summary>
        /// Gets the count of items in the priority queue.
        /// </summary>
        public int Count
        { 
            get { return tree.Count; } 
        }

        /// <summary>
        /// Determines if the priority queue contains the specified item.
        /// </summary>
        public bool Contains(TValue value)
        {
            return tree.Contains(value);
        }
    }

    /// <summary>
    /// Priority queue with maximum value given priority.
    /// </summary>
    public class MaxPriorityQueue<TValue, TPriority> : PriorityQueue<TValue, TPriority>
        where TPriority : IComparable<TPriority>
        where TValue : IEquatable<TValue>
    {
        /// <summary>
        /// Creates a <see cref="MaxPriorityQueue{TValue, TPriority}"/>.
        /// </summary>
        public MaxPriorityQueue()
        {
            tree = new MaxTree<TPriority, TValue>();
        }
    }

    /// <summary>
    /// Priority queue with minimum value given priority.
    /// </summary>
    public class MinPriorityQueue<TValue, TPriority> : PriorityQueue<TValue, TPriority>
        where TPriority : IComparable<TPriority>
        where TValue : IEquatable<TValue>
    {
        /// <summary>
        /// Creates a <see cref="MinPriorityQueue{TValue, TPriority}"/>.
        /// </summary>
        public MinPriorityQueue()
        {
            tree = new MinTree<TPriority, TValue>();
        }
    }

    /// <summary>
    /// Priority queue with maximum <see cref="System.Double"/> given priority.
    /// </summary>
    public class MaxPriorityQueue<T> : MaxPriorityQueue<T, double>
        where T : IEquatable<T>
    {
    }

    /// <summary>
    /// Priority queue with minimum <see cref="System.Double"/> given priority.
    /// </summary>
    public class MinPriorityQueue<T> : MinPriorityQueue<T, double>
        where T : IEquatable<T>
    {
    }
}
