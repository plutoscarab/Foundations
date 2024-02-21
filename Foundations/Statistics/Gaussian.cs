
/*
Gaussian.cs


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
        public static readonly IContinuousProbabilityDistribution Gaussian = new Gaussian();
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
            return Math.Exp(-x * x / 2) * OverSqrt2π;
        }

		/// <summary>
		/// Cumulative distribution function.
		/// </summary>
		public double CDF(double x)
		{
			return (1 + Special.Erf(x * OverSqrt2)) / 2;
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
