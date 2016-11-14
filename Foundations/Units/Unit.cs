
/*
Units.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Linq;
using System.Text;

namespace Foundations.Units
{
    /// <summary>
    /// A product of powers of base units.
    /// </summary>
    public class Unit
    {
        /// <summary>
        /// Unit of scalar quantities.
        /// </summary>
        public static Unit Scalar = new Unit(null, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// System of measurement for these units.
        /// </summary>
        public readonly ISystemOfMeasurement System;

        /// <summary>
        /// Factor of length.
        /// </summary>
        public readonly int Length;

        /// <summary>
        /// Factor of mass.
        /// </summary>
        public readonly int Mass;

        /// <summary>
        /// Factor of time.
        /// </summary>
        public readonly int Time;

        /// <summary>
        /// Factor of electric current.
        /// </summary>
        public readonly int Current;

        /// <summary>
        /// Factor of thermodynamic temperature.
        /// </summary>
        public readonly int Temperature;

        /// <summary>
        /// Factor of amount of substance.
        /// </summary>
        public readonly int Amount;

        /// <summary>
        /// Factor of luminous intensity.
        /// </summary>
        public readonly int Intensity;

        /// <summary>
        /// Create a <see cref="Unit"/>.
        /// </summary>
        public Unit(ISystemOfMeasurement system, int length, int mass, int time, int current, int temperature, int amount, int intensity)
        {
            System = system;
            Length = length;
            Mass = mass;
            Time = time;
            Current = current;
            Temperature = temperature;
            Amount = amount;
            Intensity = intensity;
        }

        /// <summary>
        /// Gets a string representation of this object.
        /// </summary>
        public override string ToString()
        {
            if (this == Scalar)
                return string.Empty;

            var s = new StringBuilder();

            for (int i = 4; i >= -4; i--)
            {
                foreach (var u in System.UnitSymbols)
                {
                    if (this == (u.Units ^ i))
                    {
                        if (i < 0)
                            s.Append("/");

                        s.Append(u.Symbol);

                        if (Math.Abs(i) != 1)
                        {
                            s.Append("^");
                            s.Append(Math.Abs(i));
                        }

                        return s.ToString();
                    }
                }
            }

            Action<int, Dimension> pattern = (n, dim) =>
            {
                if (n == 0) return;
                if (s.Length > 0) s.Append("·");
                var unit = System.BaseUnit(dim);
                var sym = System.UnitSymbols.First(u => u.Units == unit);
                s.Append(sym);
                if (n == 1) return;
                s.Append("^");
                s.Append(n);
            };

            pattern(Length, Dimension.Length);
            pattern(Mass, Dimension.Mass);
            pattern(Time, Dimension.Time);
            pattern(Current, Dimension.Current);
            pattern(Temperature, Dimension.Temperature);
            pattern(Amount, Dimension.Amount);
            pattern(Intensity, Dimension.Intensity);
            return s.ToString();
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(Unit a, Unit b)
        {
            return a.System == b.System
                && a.Length == b.Length
                && a.Mass == b.Mass
                && a.Time == b.Time
                && a.Current == b.Current
                && a.Temperature == b.Temperature
                && a.Amount == b.Amount
                && a.Intensity == b.Intensity;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(Unit a, Unit b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/> />
        /// </summary>
        public override bool Equals(object obj)
        {
            var u = obj as Unit;

            if (u == null)
                return false;

            return u == this;
        }

        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Length
                ^ Rotate(Mass, 4)
                ^ Rotate(Time, 8)
                ^ Rotate(Current, 12)
                ^ Rotate(Temperature, 16)
                ^ Rotate(Amount, 20)
                ^ Rotate(Intensity, 24);
        }

        private static int Rotate(int x, int n)
        {
            return (x >> n) ^ (x << (32 - n));
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static Unit operator -(Unit u)
        {
            return new Unit(u.System, -u.Length, -u.Mass, -u.Time, -u.Current, -u.Temperature, -u.Amount, -u.Intensity);
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
        public static Quantity operator *(Unit a, double b)
        {
            return (Quantity)a * b;
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
        public static Quantity operator *(double a, Unit b)
        {
            return a * (Quantity)b;
        }

        /// <summary>
        /// Scalar division.
        /// </summary>
        public static Quantity operator /(double a, Unit b)
        {
            return a / (Quantity)b;
        }

        /// <summary>
        /// Scalar division.
        /// </summary>
        public static Quantity operator /(Unit a, double b)
        {
            return (Quantity)a / b;
        }

        /// <summary>
        /// Addition operator, for multiplying quantities.
        /// </summary>
        public static Unit operator *(Unit a, Unit b)
        {
            if (a != Scalar && b != Scalar && a.System != b.System)
                throw new ArgumentException($"Units are not from the same system of measurement ({a.System} vs {b.System}).");

            return new Unit(
                a != Scalar ? a.System : b.System,
                a.Length + b.Length,
                a.Mass + b.Mass,
                a.Time + b.Time,
                a.Current + b.Current,
                a.Temperature + b.Temperature,
                a.Amount + b.Amount,
                a.Intensity + b.Intensity);
        }

        /// <summary>
        /// Subtraction operator, for dividing quantities.
        /// </summary>
        public static Unit operator /(Unit a, Unit b)
        {
            if (a != Scalar && b != Scalar && a.System != b.System)
                throw new ArgumentException($"Units are not from the same system of measurement ({a.System} vs {b.System}).");

            return new Unit(
                a != Scalar ? a.System : b.System,
                a.Length - b.Length,
                a.Mass - b.Mass,
                a.Time - b.Time,
                a.Current - b.Current,
                a.Temperature - b.Temperature,
                a.Amount - b.Amount,
                a.Intensity - b.Intensity);
        }

        /// <summary>
        /// Exponentiation operator.
        /// </summary>
        public static Unit operator ^(Unit a, int b)
        {
            return new Unit(
                a.System,
                a.Length * b,
                a.Mass * b,
                a.Time * b,
                a.Current * b,
                a.Temperature * b,
                a.Amount * b,
                a.Intensity * b);
        }
    }
}