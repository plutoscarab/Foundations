
/*
SystemRandomSource.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// IRandomSource wrapper for <see cref="System.Random"/>.
    /// </summary>
    public sealed class SystemRandomSource : IRandomSource
    {
        Random rand;
        byte[] ulongBytes = new byte[sizeof(ulong)];

        /// <summary>
        /// Constructor.
        /// </summary>
        public SystemRandomSource(Random rand)
        {
            this.rand = rand;
        }

        /// <summary>
        /// Allocate the array to be filled with seed data.
        /// </summary>
        public Array AllocateState()
        {
            return new byte[0];
        }

        /// <summary>
        /// Initialize the source using the provided seed data.
        /// </summary>
        public void Initialize(Array state)
        {
        }

        /// <summary>
        /// Gets the next 64 random bits.
        /// </summary>
        public ulong Next()
        {
            rand.NextBytes(ulongBytes);
            return BitConverter.ToUInt64(ulongBytes, 0);
        }

        /// <summary>
        /// Gets null.
        /// </summary>
        public IRandomSource Clone()
        {
            return null;    // can't clone an arbitrary System.Random instance
        }
    }
}
