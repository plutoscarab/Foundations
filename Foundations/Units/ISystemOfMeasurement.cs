
/*
ISystemOfMeasurement.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System.Collections.Generic;

namespace Foundations.Units
{
    /// <summary>
    /// Defines the units of measurement and metric prefixes for a system of measurement.
    /// </summary>
    public interface ISystemOfMeasurement
    {
        /// <summary>
        /// Metric prefixes for this system of measurement.
        /// </summary>
        IEnumerable<MetricPrefix> MetricPrefixes { get; }

        /// <summary>
        /// Unit symbols for this system of measurement.
        /// </summary>
        IEnumerable<UnitSymbol> UnitSymbols { get; }

        /// <summary>
        /// Base unit for the specified dimension of measurement.
        /// </summary>
        Unit BaseUnit(Dimension dimension);
    }
}