
/*
Gaussian.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;

using static Foundations.Constants;

namespace Foundations.Functions
{
    /// <summary>
    /// Special functions.
    /// </summary>
	public static class Gaussian
	{
        /// <summary>
        /// Gaussian probability density.
        /// </summary>
        public static double ProbabilityDensity(double x)
        {
            return Math.Exp(-x * x / 2) / Sqrt2π;
        }

		/// <summary>
		/// Gaussian cumulative distribution function.
		/// </summary>
		public static double CDF(double x)
		{
			return (1 + Special.Erf(x / Sqrt2)) / 2;
		}
    }
}
