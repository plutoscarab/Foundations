﻿
/*
UniformRandom.cs

*/

using System;
using System.Collections.Generic;
using Foundations.Types;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// Implementation of <see cref="IUniformSource"/>
    /// that uses random 64-bit values.
    /// </summary>
    public sealed class UniformRandom : IUniformSource
    {
        private Generator generator;

        /// <summary>
        /// Creates an instance of <see cref="UniformRandom"/>
        /// from a source of random bits.
        /// </summary>
        public UniformRandom(Generator generator)
        {
            if (generator == null) throw new ArgumentNullException();
            this.generator = generator;
        }

        /// <summary>
        /// Implementation of <see cref="IUniformSource"/>.
        /// </summary>
        public IUniformSource Clone()
        {
            var clone = generator.Clone();
            if (clone == null) return null;
            return new UniformRandom(clone);
        }

        /// <summary>
        /// Implementation of <see cref="IUniformSource"/>.
        /// </summary>
        public ulong Next()
        {
            return generator.UInt64();
        }
    }
}