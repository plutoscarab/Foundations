
/*
SHA256RandomSource.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.Types;
using System;
using System.Security.Cryptography;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// SHA256RandomSource
    /// </summary>
    public sealed class SHA256RandomSource : IRandomSource, IDisposable
    {
        byte[] state;
        int index;
        SHA256 sha;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SHA256RandomSource()
        {
            sha = SHA256.Create();
        }

        /// <summary>
        /// Allocate state. We return null because raw byte array data of arbitrary size is good enough.
        /// </summary>
        public Array AllocateState()
        {
            return null;
        }

        /// <summary>
        /// Initialize the source using the provided seed data.
        /// </summary>
        public void Initialize(Array state)
        {
            this.state = state as byte[];
            index = 4;
        }

        /// <summary>
        /// Gets the next 64 random bits.
        /// </summary>
        public void Next(ref ValueUnion value)
        {
            if (index == 4)
            {
                index = 0;
                if (sha == null) throw new ObjectDisposedException(GetType().Name);
                state = sha.ComputeHash(state);
            }

            value.UInt64_0 = BitConverter.ToUInt64(state, index++ * 8);
        }

        /// <summary>
        /// Gets a copy of this <see cref="SHA256RandomSource"/> with the same state.
        /// </summary>
        public IRandomSource Clone()
        {
            var result = new SHA256RandomSource();
            result.state = (byte[])state.Clone();
            result.index = index;
            return result;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (sha != null)
            {
                sha.Dispose();
                sha = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
