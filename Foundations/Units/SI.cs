
/*
SI.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;

namespace Foundations.Units
{
    /// <summary>
    /// Systems of measurement.
    /// </summary>
    public static partial class Systems
    {
        /// <summary>
        /// International System of Units.
        /// </summary>
        public static readonly ISystemOfMeasurement SI = Units.SI.Instance.Value;
    }

    /// <summary>
    /// International System of Units.
    /// </summary>
    public sealed partial class SI : ISystemOfMeasurement
    {
        internal static readonly Lazy<ISystemOfMeasurement> Instance = 
            new Lazy<ISystemOfMeasurement>(() => new SI());

        private SI()
        {
        }

        static MetricPrefix[] scaleSymbols = new MetricPrefix[]
        {
            new MetricPrefix("", 0),
            new MetricPrefix("m", -3),	// milli
			new MetricPrefix("μ", -6),	// micro
			new MetricPrefix("n", -9),	// nano
			new MetricPrefix("p", -12),	// pico
			new MetricPrefix("k", 3),	// kilo
			new MetricPrefix("M", 6),	// mega
			new MetricPrefix("G", 9),	// giga
			new MetricPrefix("T", 12),	// tera
		};

        static UnitSymbol[] unitSymbols = new UnitSymbol[]
        {
            new UnitSymbol("m", Metre),	// although m = milli
            new UnitSymbol("kg", Kilogram),
            new UnitSymbol("s", Second),
            new UnitSymbol("Hz", Hertz),
            new UnitSymbol("A", Ampere),
            new UnitSymbol("K", Kelvin),
            new UnitSymbol("mol", Mole),
            new UnitSymbol("cd", Candela),
            new UnitSymbol("Wb", Weber),
            new UnitSymbol("Pa", Pascal),
            new UnitSymbol("lx", Lux),
            new UnitSymbol("J", Joule),
            new UnitSymbol("W", Watt),
            new UnitSymbol("C", Coulomb),
            new UnitSymbol("V", Volt),
            new UnitSymbol("F", Farad),
            new UnitSymbol("Ω", Ohm),
            new UnitSymbol("ohm", Ohm),
            new UnitSymbol("H", Henry),
            new UnitSymbol("T", Tesla),	// although T = tera
			new UnitSymbol("N", Newton),
        };

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override string ToString()
        {
            return "International System of Units (SI)";
        }

        /// <summary>
        /// Scale symbols.
        /// </summary>
        public IEnumerable<MetricPrefix> MetricPrefixes => scaleSymbols;

        /// <summary>
        /// Unit symbols.
        /// </summary>
        public IEnumerable<UnitSymbol> UnitSymbols => unitSymbols;

        /// <summary>
        /// Base unit for the specified dimension of measurement.
        /// </summary>
        public Unit BaseUnit(Dimension dimension)
        {
            switch (dimension)
            {
                case Dimension.Amount:
                    return Mole;

                case Dimension.Current:
                    return Ampere;

                case Dimension.Intensity:
                    return Candela;

                case Dimension.Length:
                    return Metre;

                case Dimension.Mass:
                    return Kilogram;

                case Dimension.Temperature:
                    return Kelvin;

                case Dimension.Time:
                    return Second;
            }

            throw new NotSupportedException();
        }
    }
}