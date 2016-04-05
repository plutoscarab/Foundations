
/*
IRandomSource.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// A source of random bits.
    /// </summary>
    public interface IRandomSource
    {
        /// <summary>
        /// Allocates an array for state information. This array will be filled with random values
        /// derived from seed data and then passed to Initialize().
        /// If null is returned, the raw seed data will be passed as a byte array instead.
        /// </summary>
        Array AllocateState();

        /// <summary>
        /// Initializes the source using the provided seed data.
        /// </summary>
        void Initialize(Array state);

        /// <summary>
        /// Gets the next 64 random bits.
        /// </summary>
        ulong Next();

        /// <summary>
        /// Gets a copy of this <see cref="IRandomSource"/>.
        /// </summary>
        /// <returns>
        /// Returns a copy of this <see cref="IRandomSource"/> with the
        /// same state. Returns null if the future state is not completely
        /// determined from the current state or if the source is
        /// otherwise unclonable.
        /// </returns>
        IRandomSource Clone();
    }
}
