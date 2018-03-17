
/*
VanDerCorput.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using Foundations.Coding;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class VanDerCorput : IUniformSource
    {
        private uint b;
        private ulong index;
        private UInt128 scale;

        /// <summary>
        /// Binary Van der Corput sequence.
        /// </summary>
        public VanDerCorput()
        {
            b = 2;
        }

        /// <summary>
        /// Van der Corput sequence for specified base.
        /// </summary>
        public VanDerCorput(int @base)
        {
            b = (uint)@base;
            if (b < 2) throw new ArgumentOutOfRangeException();
            scale = (UInt128.One << 124) / b;
        }

        private VanDerCorput(VanDerCorput other)
        {
            b = other.b;
            index = other.index;
            scale = other.scale;
        }

        /// <summary>
        /// Implementation of <see cref="IUniformSource.Clone"/>.
        /// </summary>
        public IUniformSource Clone()
        {
            return new VanDerCorput(this);
        }

        /// <summary>
        /// Implementation of <see cref="IUniformSource.Next"/>.
        /// </summary>
        public ulong Next()
        {
            index++;

            if (b == 2)
                return Bits.Reverse(index);

            UInt128 u = 0;
            ulong i = index;
            UInt128 s = scale;

            while (i > 0)
            {
                u += (i % b) * s;
                i /= b;
                s /= b;
            }

            if ((u.Lo << 4) >= 0x8000000000000000)
                u += 0x1000000000000000;

            return (u >> 60).Lo;
        }
    }
}