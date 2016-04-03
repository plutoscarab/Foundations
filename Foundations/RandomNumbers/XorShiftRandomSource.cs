
/*
XorShift1024Star.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

Based on public domain source at http://xorshift.di.unimi.it/xorshift1024star.c
by Sebastiano Vigna (vigna@acm.org).
*/

using System;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// XorShift1024* random source.
    /// </summary>
    public sealed class XorShiftRandomSource : IRandomSource
    {
        ulong[] state;
        int p;

        /// <summary>
        /// Allocate the array to be filled with seed data.
        /// </summary>
        public Array AllocateState()
        {
            return new ulong[16];
        }

        /// <summary>
        /// Initialize the source using the provided seed data.
        /// </summary>
        public void Initialize(Array state)
        {
            this.state = state as ulong[];
        }

        /// <summary>
        /// Gets the next 64 random bits.
        /// </summary>
        public ulong Next()
        {
            ulong
                s0 = state[p],
                s1 = state[p = (p + 1) & 15];

            s1 ^= s1 << 31;
            state[p] = s1 ^ s0 ^ (s1 >> 11) ^ (s0 >> 30);
            return state[p] * 1181783497276652981;
        }
    }
}
