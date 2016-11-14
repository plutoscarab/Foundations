
/*
Constants.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
using Foundations.Units;

namespace Foundations
{
    /// <summary>
    /// Constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>PI</summary>
        public const double π = Math.PI;

        /// <summary>Base of natural logarithm.</summary>
        public const double e = 2.7182818284590451;

        /// <summary>Square root of 2π.</summary>
        public const double Sqrt2π = 2.5066282746310002;

        /// <summary>Square root of 5.</summary>
        public const double Sqrt5 = 2.23606797749979;

        /// <summary>Square root of π.</summary>
        public const double Sqrtπ = 1.772453850905516;

        /// <summary>Square root of 3.</summary>
        public const double Sqrt3 = 1.7320508075688772;

        /// <summary>Golden ratio.</summary>
        public const double φ = 1.6180339887498949;

        /// <summary>Square root of 2.</summary>
        public const double Sqrt2 = 1.4142135623730951;

        /// <summary>Square root of π/2.</summary>
        public const double SqrtHalfπ = 1.2533141373155003;

        /// <summary>Euler–Mascheroni constant.</summary>
        public const double γ = 0.57721566490153287;

        /// <summary>Speed of light in vacuum.</summary>
        public static Quantity c = 299792458 * SI.Metre / SI.Second;

        /// <summary>Avagadro constant.</summary>
        public static Quantity N = 6.022140857e23 / SI.Mole;

        /// <summary>Boltzmann constant.</summary>
        public static Quantity k = 1.38064852e-23 * SI.Joule / SI.Kelvin;

        /// <summary>Stefan-Boltzmann constant.</summary>
        public static Quantity σ = 5.670367e-8 * SI.Watt / (SI.Metre ^ 2) / (SI.Kelvin ^ 4);

        /// <summary>Electric constant.</summary>
        public static Quantity ε0 = 8.854187817e-12 * SI.Farad / SI.Metre;

        /// <summary>Magnetic constant.</summary>
        public static Quantity μ0 = 4e-7 * π * SI.Newton / (SI.Ampere ^ 2);

        /// <summary>Electron volt.</summary>
        public static Quantity eV = 1.6021766208e-19 * SI.Joule;

        /// <summary>Elementary charge.</summary>
        public static Quantity ec = 1.6021766208e-19 * SI.Coulomb;

        /// <summary>Fine structure constant.</summary>
        public const double α = 7.2973525664e-3;

        /// <summary>Magnetic flux quantum.</summary>
        public static Quantity ϕ0 = 2.067833831e-15 * SI.Weber;

        /// <summary>Conductance quantum.</summary>
        public static Quantity G0 = 7.7480917310e-5 * SI.Siemens;

        /// <summary>Atomic mass constant.</summary>
        public static Quantity mu = 1.660539040e-27 * SI.Kilogram;

        /// <summary>Proton mass.</summary>
        public static Quantity mp = 1.672621898e-27 * SI.Kilogram;

        /// <summary>Electron mass.</summary>
        public static Quantity me = 9.10938356e-31 * SI.Kilogram;

        /// <summary>Molar gas constant.</summary>
        public static Quantity R = 8.3144598 * SI.Joule / SI.Mole / SI.Kelvin;

        /// <summary>Newtonian constant of gravitation.</summary>
        public static Quantity G = 6.67408e-11 * (SI.Metre ^ 3) / SI.Kilogram / (SI.Second ^ 2);

        /// <summary>Standard gravity.</summary>
        public static Quantity g0 = 9.80665 * SI.Metre / (SI.Second ^ 2);

        /// <summary>Standard atmosphere.</summary>
        public static Quantity atm = 101325 * SI.Pascal;

        /// <summary>Planck constant.</summary>
        public static Quantity h = 6.626070040e-34 * SI.Joule * SI.Second;

        /// <summary>Reduced Planck constant (h / 2π).</summary>
        public static Quantity ħ = 1.054571800e-34 * SI.Joule * SI.Second;

        /// <summary>Rydberg constant.</summary>
        public static Quantity Rinf = 10973731.568508 / SI.Metre;
    }
}
