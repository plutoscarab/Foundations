/*
Quantity.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;

namespace Foundations
{
    /// <summary>
    /// The type of physical quantities with units.
    /// </summary>
    public record struct Quantity(double Value, SI Units)
    {
        public static Quantity operator *(Quantity quantity, SI units) =>
            new(quantity.Value, quantity.Units * units);

        public static Quantity operator /(Quantity quantity, SI units) =>
            new(quantity.Value, quantity.Units / units);

        public static Quantity operator *(Quantity a, Quantity b) =>
            new(a.Value * b.Value, a.Units * b.Units);

        public static Quantity operator /(Quantity a, Quantity b) =>
            new(a.Value / b.Value, a.Units / b.Units);

        public static Quantity operator +(Quantity a, Quantity b) =>
            new(a.Value + b.Value, a.Units * (a.Units == b.Units ? b.Units : throw new ArgumentException("Unit mismatch.")));

        public static implicit operator Quantity(double value) =>
            new(value, SI.Dimensionless);

        public override readonly string ToString()
        {
            if (Units == SI.Dimensionless)
                return Value.ToString();

            return $"{Value} {Units}";
        }
    }
}