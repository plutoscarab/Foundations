
/*
Quantities.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;

namespace Foundations
{
    /// <summary>
    /// Physical quantities.
    /// </summary>
    public static class Quantities
    {
        /// <summary>Speed of light in vacuum.</summary>
        public static readonly Quantity c = 299792458 * SI.Metre.PerSecond;

        /// <summary>Avagadro constant.</summary>
        public static readonly Quantity N = 6.022140857e23 / SI.Mole;

        /// <summary>Boltzmann constant.</summary>
        public static readonly Quantity k = 1.38064852e-23 * SI.Joule / SI.Kelvin;

        /// <summary>Stefan-Boltzmann constant.</summary>
        public static readonly Quantity σ = 5.670367e-8 * SI.Watt / SI.SquareMetre / (SI.Kelvin ^ 4);

        /// <summary>Electric constant.</summary>
        public static readonly Quantity ε0 = 8.854187817e-12 * SI.Farad / SI.Metre;

        /// <summary>Magnetic constant.</summary>
        public static readonly Quantity μ0 = 4e-7 * Constants.π * SI.Newton / (SI.Ampere ^ 2);

        /// <summary>Electron volt.</summary>
        public static readonly Quantity eV = 1.6021766208e-19 * SI.Joule;

        /// <summary>Elementary charge.</summary>
        public static readonly Quantity ec = 1.6021766208e-19 * SI.Coulomb;

        /// <summary>Fine structure constant.</summary>
        public static readonly Quantity α = 7.2973525664e-3;

        /// <summary>Magnetic flux quantum.</summary>
        public static readonly Quantity ϕ0 = 2.067833831e-15 * SI.Weber;

        /// <summary>Conductance quantum.</summary>
        public static readonly Quantity G0 = 7.7480917310e-5 * SI.Siemens;

        /// <summary>Atomic mass constant.</summary>
        public static readonly Quantity mu = 1.660539040e-27 * SI.Kilogram;

        /// <summary>Proton mass.</summary>
        public static readonly Quantity mp = 1.672621898e-27 * SI.Kilogram;

        /// <summary>Electron mass.</summary>
        public static readonly Quantity me = 9.10938356e-31 * SI.Kilogram;

        /// <summary>Molar gas constant.</summary>
        public static readonly Quantity R = 8.3144598 * SI.Joule / SI.Mole / SI.Kelvin;

        /// <summary>Newtonian constant of gravitation.</summary>
        public static readonly Quantity G = 6.67408e-11 * SI.CubicMetre / SI.Kilogram / (SI.Second ^ 2);

        /// <summary>Standard gravity.</summary>
        public static readonly Quantity g0 = 9.80665 * SI.Metre / (SI.Second ^ 2);

        /// <summary>Standard atmosphere.</summary>
        public static readonly Quantity atm = 101325 * SI.Pascal;

        /// <summary>Planck constant.</summary>
        public static readonly Quantity h = 6.626070040e-34 * SI.Joule * SI.Second;

        /// <summary>Reduced Planck constant (h / 2π).</summary>
        public static readonly Quantity ħ = 1.054571800e-34 * SI.Joule * SI.Second;

        /// <summary>Rydberg constant.</summary>
        public static readonly Quantity Rinf = 10973731.568508 / SI.Metre;
    }
}
