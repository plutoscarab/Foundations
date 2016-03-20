﻿
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Foundations.Functions.Numerics
{
    internal static class Machine
    {
        public static double ε = Math.Pow(2, -53);
    }

	/// <summary>
	/// Elliptic integrals in Carlson symmetric form.
	/// </summary>
	/// <remarks>
	/// B. C. Carlson (1995) Numerical computation of real or complex elliptic integrals.  Numer. Algorithms 10 (1-2),  pp. 13–26. 
	/// Implements the enhancements described in http://dlmf.nist.gov/19.36
	/// </remarks>
	public static partial class CarlsonSymmetric
	{
		/// <summary>
		/// Symmetric elliptic integral of the first kind.
		/// </summary>
        public static Complex RF(Complex x, Complex y, Complex z)
        {
            switch ((x == 0 ? 1 : 0) + (y == 0 ? 1 : 0) + (z == 0 ? 1 : 0))
            {
                case 2:
                    return double.PositiveInfinity;
            }

            Complex
                A0 = (x + y + z) / 3,
                A = A0;

            double
                r = 10 * Machine.ε,
                Q = Math.Pow(3 * r, -1 / 8d) * Math.Max(Math.Max(Complex.Abs(A - x), Complex.Abs(A - y)), Complex.Abs(A - z));

            while (Q >= Complex.Abs(A))
            {
                Complex
                    sx = Complex.Sqrt(x),
                    sy = Complex.Sqrt(y),
                    sz = Complex.Sqrt(z),
                    λ = sx * sy + sy * sz + sz * sx;

                A = (A + λ) / 4;
                x = (x + λ) / 4;
                y = (y + λ) / 4;
                z = (z + λ) / 4;
                Q /= 4;
            }

            Complex
                X = 1 - x / A,
                Y = 1 - y / A,
                Z = - X - Y,
                E2 = Z * Z - X * Y,
                E3 = X * Y * Z;

            return (1 + E3 * (1 / 14d + 3 * E3 / 104) + E2 * (1 / 10d + 3 * E3 / 44 + E2 * (1 / 24d + E3 / 16 + 5 * E2 / 208))) / Complex.Sqrt(A);
        }

		/// <summary>
		/// Symmetric elliptic integral of the first kind.
		/// </summary>
        public static Double RF(Double x, Double y, Double z)
        {
            switch ((x == 0 ? 1 : 0) + (y == 0 ? 1 : 0) + (z == 0 ? 1 : 0))
            {
                case 2:
                    return double.PositiveInfinity;
            }

            Double
                A0 = (x + y + z) / 3,
                A = A0;

            double
                r = 10 * Machine.ε,
                Q = Math.Pow(3 * r, -1 / 8d) * Math.Max(Math.Max(Math.Abs(A - x), Math.Abs(A - y)), Math.Abs(A - z));

            while (Q >= Math.Abs(A))
            {
                Double
                    sx = Math.Sqrt(x),
                    sy = Math.Sqrt(y),
                    sz = Math.Sqrt(z),
                    λ = sx * sy + sy * sz + sz * sx;

                A = (A + λ) / 4;
                x = (x + λ) / 4;
                y = (y + λ) / 4;
                z = (z + λ) / 4;
                Q /= 4;
            }

            Double
                X = 1 - x / A,
                Y = 1 - y / A,
                Z = - X - Y,
                E2 = Z * Z - X * Y,
                E3 = X * Y * Z;

            return (1 + E3 * (1 / 14d + 3 * E3 / 104) + E2 * (1 / 10d + 3 * E3 / 44 + E2 * (1 / 24d + E3 / 16 + 5 * E2 / 208))) / Math.Sqrt(A);
        }

	}
}

