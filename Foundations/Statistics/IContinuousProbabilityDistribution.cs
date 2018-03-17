
/*
IContinuousProbabilityDistribution.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

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