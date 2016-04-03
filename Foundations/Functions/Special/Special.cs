﻿
/*
Special.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
using System.Numerics;

using static Foundations.Constants;

namespace Foundations.Functions
{
    /// <summary>
    /// Miscellaneous special functions.
    /// </summary>
	public static class Special
	{

		static Double ErfSmallZ(Double z)
		{
			Double
                sum = z,
			    term = z,
			    zz = -z * z;

			int n = 0;

			while (true)
			{
				term *= zz / ++n;
				Double old = sum;
    			sum += term / (2 * n + 1);

				if (sum == old)
					break;
			}

			return sum * 2 / Sqrtπ;
		}

		static Double ErfLargeZ(Double z)
		{
			Double
                zz = z * z,
                t = 0;

			for (int k = 60; k >= 1; k--)
			{
				t = (k - 0.5) / (1 + k / (zz + t));
			}

			return (1 - Math.Exp(Math.Log(z) - zz) / (t + zz) / Sqrtπ) * Math.Sign(z);
		}

		/// <summary>
		/// Error function.
		/// </summary>
		public static Double Erf(Double z)
		{
			if (Math.Abs(z) < 6.5)
				return ErfSmallZ(z);

			return ErfLargeZ(z);
		}

        /// <summary>
        /// Complementary error function.
        /// </summary>
        public static Double Erfc(Double z)
        {
            return 1 - Erf(z);
        }

        /// <summary>
        /// Imaginary error function.
        /// </summary>
        public static Double Erfi(Double z)
        {
            return (-Complex.ImaginaryOne * Erf(Complex.ImaginaryOne * z)).Real;
        }

		static Complex ErfSmallZ(Complex z)
		{
			Complex
                sum = z,
			    term = z,
			    zz = -z * z;

			int n = 0;

			while (true)
			{
				term *= zz / ++n;
				Complex old = sum;
    			sum += term / (2 * n + 1);

				if (sum == old)
					break;
			}

			return sum * 2 / Sqrtπ;
		}

		static Complex ErfLargeZ(Complex z)
		{
			Complex
                zz = z * z,
                t = 0;

			for (int k = 60; k >= 1; k--)
			{
				t = (k - 0.5) / (1 + k / (zz + t));
			}

			return (1 - Complex.Exp(Complex.Log(z) - zz) / (t + zz) / Sqrtπ) * Math.Sign(z.Real);
		}

		/// <summary>
		/// Error function.
		/// </summary>
		public static Complex Erf(Complex z)
		{
            if (z.Imaginary == 0) 
                return Erf(z.Real);

			if (Complex.Abs(z) < 6.5)
				return ErfSmallZ(z);

			return ErfLargeZ(z);
		}

        /// <summary>
        /// Complementary error function.
        /// </summary>
        public static Complex Erfc(Complex z)
        {
            if (z.Imaginary == 0) 
                return Erfc(z.Real);

            return 1 - Erf(z);
        }

        /// <summary>
        /// Imaginary error function.
        /// </summary>
        public static Complex Erfi(Complex z)
        {
            if (z.Imaginary == 0) 
                return Erfi(z.Real);

            return (-Complex.ImaginaryOne * Erf(Complex.ImaginaryOne * z));
        }
    }
}
