
/*
SynchronizedRandomSource.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.Types;
using System;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// Thread-safe IRandomSource.
    /// </summary>
    public sealed class SynchronizedRandomSource : IRandomSource
    {
        IRandomSource source;
        object syncRoot = new object();

        /// <summary>
        /// Constructor.
        /// </summary>
        public SynchronizedRandomSource(IRandomSource source)
        {
            this.source = source;
        }

        /// <summary>
        /// Allocate the array to be filled with seed data.
        /// </summary>
        public Array AllocateState()
        {
            return source.AllocateState();
        }

        /// <summary>
        /// Initialize the source using the provided seed data.
        /// </summary>
        public void Initialize(Array state)
        {
            source.Initialize(state);
        }

        /// <summary>
        /// Gets the next 64 random bits.
        /// </summary>
        public void Next(ref ValueUnion value)
        {
            lock(syncRoot)
            {
                source.Next(ref value);
            }
        }

        /// <summary>
        /// Gets a copy of this <see cref="SynchronizedRandomSource"/> with the same state.
        /// </summary>
        public IRandomSource Clone()
        {
            return source.Clone();
        }
    }
}
