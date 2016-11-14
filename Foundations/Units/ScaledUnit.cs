
/*
ScaledUnit.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Text;

namespace Foundations.Units
{
    /// <summary>
    /// A <see cref="Unit"/> of measurement scaled by a factor of a power of 10,
    /// e.g. <see cref="SI.Metre"/> scaled by -6 represents micrometres.
    /// </summary>
    public sealed class ScaledUnit : IEquatable<ScaledUnit>
    {
        /// <summary>
        /// Dimensionless value of 1.
        /// </summary>
        public static ScaledUnit One = new ScaledUnit(Unit.Scalar, 0);

        /// <summary>
        /// The unit of measurement.
        /// </summary>
        public readonly Unit Unit;

        /// <summary>
        /// The power-of-10 scaling factor.
        /// </summary>
        public readonly int Scale;

        /// <summary>
        /// Create a <see cref="ScaledUnit"/>.
        /// </summary>
        public ScaledUnit(Unit units, int scale)
        {
            Unit = units;
            Scale = scale;
        }

        /// <summary>
        /// Gets a string representation of this <see cref="ScaledUnit"/>
        /// e.g. capacitance unit SI.Farad with -6 scale (microfarads) returns "μF".
        /// </summary>
        public override string ToString()
        {
            var s = new StringBuilder();

            if (Scale != 0)
            {
                foreach (var ss in Unit.System.MetricPrefixes)
                {
                    if (Scale == ss.Power)
                    {
                        s.Append(ss.Symbol);
                        break;
                    }
                }

                if (s.Length == 0)
                {
                    s.Append("*10^");
                    s.Append(Scale);
                }
            }

            s.Append(Unit);
            return s.ToString();
        }

        /// <summary>
        /// Parse a scaled unit of measurement, e.g "μF" in SI system results
        /// in SI.Farad with -6 scale (microfarads).
        /// </summary>
        public static bool TryParse(string s, ISystemOfMeasurement system, out ScaledUnit us)
        {
            string unit = string.Empty;
            Unit units = Unit.Scalar;

            foreach (var u in system.UnitSymbols)
            {
                if (u.Symbol.Length == 0)
                    continue;

                if (s.EndsWith(u.Symbol))
                {
                    unit = u.Symbol;
                    units = u.Units;
                    break;
                }
            }

            string scale = string.Empty;
            int power = 0;

            foreach (var sc in system.MetricPrefixes)
            {
                if (sc.Symbol.Length == 0)
                    continue;

                if (s == sc.Symbol + unit)
                {
                    scale = sc.Symbol;
                    power = sc.Power;
                    break;
                }
            }

            if (s == scale + unit)
            {
                us = new ScaledUnit(units, power);
                return true;
            }

            us = One;
            return false;
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static ScaledUnit operator *(ScaledUnit a, ScaledUnit b)
        {
            return new ScaledUnit(a.Unit * b.Unit, a.Scale + b.Scale);
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static ScaledUnit operator /(ScaledUnit a, ScaledUnit b)
        {
            return new ScaledUnit(a.Unit / b.Unit, a.Scale - b.Scale);
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public bool Equals(ScaledUnit other)
        {
            if (other == null) return false;
            return Scale == other.Scale && Unit == other.Unit;
        }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        {
            var us = obj as ScaledUnit;
            if (us == null) return false;
            return this.Equals(us);
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(ScaledUnit a, ScaledUnit b)
        {
            if (ReferenceEquals(a, null))
                return ReferenceEquals(b, null);

            return a.Equals(b);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(ScaledUnit a, ScaledUnit b)
        {
            if (ReferenceEquals(a, null))
                return !ReferenceEquals(b, null);

            return !a.Equals(b);
        }

        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return Unit.GetHashCode() ^ Scale.GetHashCode();
        }
    }
}