
/*
Quantities.cs

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
    /// Physical quantities.
    /// </summary>
    public static class Quantities
    {
        /// <summary>Speed of light in vacuum.</summary>
        public static Quantity c = 299792458 * SI.Metre.PerSecond;

        /// <summary>Avagadro constant.</summary>
        public static Quantity N = 6.022140857e23 / SI.Mole;

        /// <summary>Boltzmann constant.</summary>
        public static Quantity k = 1.38064852e-23 * SI.Joule.PerKelvin;

        /// <summary>Stefan-Boltzmann constant.</summary>
        public static Quantity σ = 5.670367e-8 * SI.Watt.PerSquareMetre / (SI.Kelvin ^ 4);

        /// <summary>Electric constant.</summary>
        public static Quantity ε0 = 8.854187817e-12 * SI.Farad.PerMetre;

        /// <summary>Magnetic constant.</summary>
        public static Quantity μ0 = 4e-7 * Constants.π * SI.Newton.PerSquareAmpere;

        /// <summary>Electron volt.</summary>
        public static Quantity eV = 1.6021766208e-19 * SI.Joule;

        /// <summary>Elementary charge.</summary>
        public static Quantity ec = 1.6021766208e-19 * SI.Coulomb;

        /// <summary>Fine structure constant.</summary>
        public static Quantity α = 7.2973525664e-3;

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
        public static Quantity R = 8.3144598 * SI.Joule.PerMole.PerKelvin;

        /// <summary>Newtonian constant of gravitation.</summary>
        public static Quantity G = 6.67408e-11 * SI.CubicMetre.PerKilogram.PerSquareSecond;

        /// <summary>Standard gravity.</summary>
        public static Quantity g0 = 9.80665 * SI.Metre.PerSquareSecond;

        /// <summary>Standard atmosphere.</summary>
        public static Quantity atm = 101325 * SI.Pascal;

        /// <summary>Planck constant.</summary>
        public static Quantity h = 6.626070040e-34 * SI.Joule.Second;

        /// <summary>Reduced Planck constant (h / 2π).</summary>
        public static Quantity ħ = 1.054571800e-34 * SI.Joule.Second;

        /// <summary>Rydberg constant.</summary>
        public static Quantity Rinf = 10973731.568508 / SI.Metre;
    }
}
