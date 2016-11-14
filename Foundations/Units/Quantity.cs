
/*
Quantity.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations.Units
{
    /// <summary>
    /// A value with dimensional units.
    /// </summary>
    public class Quantity : IEquatable<Quantity>, IComparable<Quantity>
    {
        /// <summary>
        /// The value.
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// The units of measurement.
        /// </summary>
        public Unit Unit { get; private set; }

        /// <summary>
        /// Create a <see cref="Quantity"/>.
        /// </summary>
        public Quantity(double value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override string ToString()
        {
            return $"{Value}{Unit}";
        }

        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Unit.GetHashCode();
        }

        /// <summary>
        /// Determines if two <see cref="Quantity"/> values are equal.
        /// </summary>
        public bool Equals(Quantity other)
        {
            if (other == null) return false;
            return Value == other.Value && Unit == other.Unit;
        }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as Quantity);
        }

        private static void EnsureUnits(Quantity a, Quantity b)
        {
            if (a.Unit != b.Unit) throw new ArgumentException("Quantities must have the same units.");
        }

        /// <summary>
        /// Compare this <see cref="Quantity"/> to another.
        /// </summary>
        public int CompareTo(Quantity other)
        {
            if (other == null)
                throw new ArgumentNullException();

            EnsureUnits(this, other);
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Add two quantities. They must have the same units.
        /// </summary>
        public static Quantity operator +(Quantity a, Quantity b)
        {
            EnsureUnits(a, b);
            return new Quantity(a.Value + b.Value, a.Unit);
        }

        /// <summary>
        /// Subtract two quantities. They must have the same units.
        /// </summary>
        public static Quantity operator -(Quantity a, Quantity b)
        {
            EnsureUnits(a, b);
            return new Quantity(a.Value - b.Value, a.Unit);
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static Quantity operator -(Quantity q)
        {
            return new Quantity(-q.Value, q.Unit);
        }

        /// <summary>
        /// Multiply two quantities.
        /// </summary>
        public static Quantity operator *(Quantity a, Quantity b)
        {
            return new Quantity(a.Value * b.Value, a.Unit * b.Unit);
        }

        /// <summary>
        /// Divide two quantities.
        /// </summary>
        public static Quantity operator /(Quantity a, Quantity b)
        {
            return new Quantity(a.Value / b.Value, a.Unit / b.Unit);
        }

        /// <summary>
        /// Convert a value to a scalar quantity.
        /// </summary>
        public static implicit operator Quantity(double d)
        {
            return new Quantity(d, Unit.Scalar);
        }

        /// <summary>
        /// Convert a <see cref="Unit"/> to a quantity of 1 of that unit.
        /// </summary>
        public static implicit operator Quantity(Unit u)
        {
            return new Quantity(1.0, u);
        }
    }
}