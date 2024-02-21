
/*
IContinuousProbabilityDistribution.cs

*/

using System;

namespace Foundations.Statistics
{
    /// <summary>
    /// Defines a statistical probability distribution of a continuous variable.
    /// </summary>
    public interface IContinuousProbabilityDistribution
    {
        /// <summary>
        /// Probability density.
        /// </summary>
        double PDF(double x);

        /// <summary>
        /// Cumulative Distribution Function.
        /// </summary>
        double CDF(double x);

        /// <summary>
        /// Inverse Cumulative Distribution Function (CDF).
        /// </summary>
        double InvCDF(double x);
    }
}