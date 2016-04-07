
/*
StreamSource.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.Types;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// StreamSource gets its "random" bits from a <see cref="System.IO.Stream"/>.
    /// The randomness of this data depends on the Stream.
    /// </summary>
    public sealed class StreamSource : IRandomSource
    {
        Stream stream;
        byte[] buffer;

        /// <summary>
        /// Constructor.
        /// </summary>
        public StreamSource(Stream stream)
        {
            this.stream = stream;
            buffer = new byte[8];
        }

        /// <summary>
        /// Allocate state. We return an array because we don't want to require a seed,
        /// but an empty array because we're not going to use the initialization data.
        /// </summary>
        public Array AllocateState()
        {
            return new byte[0];
        }

        /// <summary>
        /// Not used.
        /// </summary>
        public void Initialize(Array state)
        {
        }

        /// <summary>
        /// Gets the next 64 bits.
        /// </summary>
        public void Next(ref ValueUnion value)
        {
            int n = stream.Read(buffer, 0, sizeof(ulong));
            if (n < sizeof(ulong)) throw new InvalidOperationException("Stream is not a suitable IRandomSource.");
            value.UInt64_0 = BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Not cloneable.
        /// </summary>
        public IRandomSource Clone()
        {
            return null;
        }
    }
}
