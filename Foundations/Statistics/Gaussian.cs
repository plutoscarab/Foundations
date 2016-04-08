
/*
Gaussian.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using Foundations.Functions;
using System;

using static Foundations.Constants;

namespace Foundations.Statistics
{
    /// <summary>
    /// Probability distributions.
    /// </summary>
    public static partial class Distributions
    {
        /// <summary>
        /// Gaussian (Normal) distribution.
        /// </summary>
        public static IContinuousProbabilityDistribution Gaussian = new Gaussian();
    }

    /// <summary>
    /// Gaussian distribution.
    /// </summary>
	internal class Gaussian : IContinuousProbabilityDistribution
	{
        /// <summary>
        /// Probability density function.
        /// </summary>
        public double PDF(double x)
        {
            return Math.Exp(-x * x / 2) / Sqrt2π;
        }

		/// <summary>
		/// Cumulative distribution function.
		/// </summary>
		public double CDF(double x)
		{
			return (1 + Special.Erf(x / Sqrt2)) / 2;
		}

        /// <summary>
        /// Inverse cumulative distribution function.
        /// </summary>
        public double InvCDF(double x)
        {
            throw new NotImplementedException();
        }
    }
}
