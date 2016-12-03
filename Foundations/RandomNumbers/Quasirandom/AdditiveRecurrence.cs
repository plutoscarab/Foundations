﻿
/*
AdditiveRecurrence.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Linq;
using System.Numerics;
using Foundations.Coding;
using Foundations.Types;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AdditiveRecurrence : IUniformSource
    {
        ulong value;
        ulong increment;

        /// <summary>
        /// 
        /// </summary>
        public AdditiveRecurrence(ulong start, ulong increment)
        {
            if ((increment & 1) == 0) throw new ArgumentException("Increment must be odd.", nameof(increment));
            value = start;
            this.increment = increment;
        }

        /// <summary>
        /// 
        /// </summary>
        public AdditiveRecurrence(ulong increment)
            : this(0, increment)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public AdditiveRecurrence(int a, int b, int c, int d)
        {
            var x = (a + b * ContinuedFraction.Sqrt(c, 60)) / d;
            var g = BigInteger.Pow(2, 64);
            var y = Rational.Floor(Rational.Frac(x.Convergents().Last()) * g);
            if (y == g) y--;
            value = 0;
            increment = (ulong)y | 1UL;
        }

        /// <summary>
        /// Implementation of <see cref="IUniformSource.Clone"/>.
        /// </summary>
        public IUniformSource Clone()
        {
            return new AdditiveRecurrence(value, increment);
        }

        /// <summary>
        /// Implementation of <see cref="IUniformSource.Next"/>.
        /// </summary>
        public ulong Next()
        {
            return value += increment;
        }
    }
}