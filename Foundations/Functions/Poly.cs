
/*
Poly.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations
{
    /// <summary>
    /// Polynomial evaluation.
    /// </summary>
    public static class Poly
    {
        /// <summary>
        /// Evaluate a polynomial using Horner's method.
        /// </summary>
        /// <param name="x">The function argument.</param>
        /// <param name="coefficients">First element is the constant term, second is x term, third is x² term, etc.</param>
        public static double Eval(double x, params double[] coefficients)
        {
            if (coefficients == null)
                throw new ArgumentNullException(nameof(coefficients));

            int n = coefficients.Length;
            double sum = 0;

            while (n > 0)
            {
                sum = sum * x + coefficients[--n];
            }

            return sum;
        }

        /// <summary>
        /// Evaluate a polynomial using Horner's method.
        /// </summary>
        /// <param name="x">The function argument.</param>
        /// <param name="coefficients">First element is the constant term, second is x term, third is x² term, etc.</param>
        public static float Eval(float x, params float[] coefficients)
        {
            if (coefficients == null)
                throw new ArgumentNullException(nameof(coefficients));

            int n = coefficients.Length;
            float sum = 0;

            while (n > 0)
            {
                sum = sum * x + coefficients[--n];
            }

            return sum;
        }
    }
}