
/*
SystemRandomSource.cs


*/

using Foundations.Types;
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
        public void Next(ref ValueUnion value)
        {
            rand.NextBytes(ulongBytes);
            value.UInt64_0 = BitConverter.ToUInt64(ulongBytes, 0);
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
