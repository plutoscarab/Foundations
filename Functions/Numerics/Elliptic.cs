
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Foundations.Functions.Numerics
{
	/// <summary>
	/// Elliptic integrals.
	/// </summary>
	/// <remarks>
	/// Evaluated using Carlson symmetric form.
	/// </remarks>
	public partial class Elliptic
	{
		const double π =  Math.PI;

		/// <summary>
		/// Complete elliptic integral of the first kind.
		/// </summary>
        public static double K(double m)
        {
			if (m == 1) return double.PositiveInfinity;
            return CarlsonSymmetric.RF(0, 1 - m, 1);
        }

		/// <summary>
		/// Compute the nome for parameter m.
		/// </summary>
		public static double Nome(double m)
		{
            return Math.Exp(-π * Elliptic.K(1 - m) / Elliptic.K(m));
		}

		/// <summary>
		/// Compute the nome for parameter m.
		/// </summary>
		public static double Nome(double m, out double K)
		{
			K = Elliptic.K(m);
            return Math.Exp(-π * Elliptic.K(1 - m) / K);
		}

		/// <summary>
		/// Compute the nome for parameter m, as well as <see cref="K"/>(m) and K'(m) = <see cref="K"/>(1 - m).
		/// </summary>
		public static double Nome(double m, out double K, out double Kprime)
		{
			K = Elliptic.K(m);
			Kprime = Elliptic.K(1 - m);
            return Math.Exp(-π * Kprime / K);
		}

		/// <summary>
        /// Incomplete elliptic integral of the first kind F(φ | m).
		/// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static Complex F(Complex φ, double m)
        {
            if (Math.Abs(φ.Real) > π / 2)
            {
                int n = (int)Math.Round(φ.Real / π);
                return F(φ - n * π, m) + n * 2 * K(m);
            }

            Complex
                sinφ = Complex.Sin(φ),
                cosφ = Complex.Cos(φ);

            return sinφ * CarlsonSymmetric.RF(cosφ * cosφ, 1 - m * sinφ * sinφ, 1);
        }

		/// <summary>
        /// Get a function F(φ) that computes the incomplete elliptic integral of the first kind F(φ | m)
		/// for a constant parameter m. Use this instead of F(φ, m) if you use the same value of m many times.
		/// </summary>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
		public static Func<Complex, Complex> FComplex(double m)
		{
			var k = K(m);

			return φ => 
			{
				Complex offset = 0;

				if (Math.Abs(φ.Real) > π / 2)
				{
					int n = (int)Math.Round(φ.Real / π);
					φ -= n * π;
					offset = n * 2 * k;
				}

				Complex
					sinφ = Complex.Sin(φ),
					cosφ = Complex.Cos(φ);

				return sinφ * CarlsonSymmetric.RF(cosφ * cosφ, 1 - m * sinφ * sinφ, 1) + offset;
			};
		}

		/// <summary>
        /// Incomplete elliptic integral of the first kind F(φ | m).
		/// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static Double F(Double φ, double m)
        {
            if (Math.Abs(φ) > π / 2)
            {
                int n = (int)Math.Round(φ / π);
                return F(φ - n * π, m) + n * 2 * K(m);
            }

            Double
                sinφ = Math.Sin(φ),
                cosφ = Math.Cos(φ);

            return sinφ * CarlsonSymmetric.RF(cosφ * cosφ, 1 - m * sinφ * sinφ, 1);
        }

		/// <summary>
        /// Get a function F(φ) that computes the incomplete elliptic integral of the first kind F(φ | m)
		/// for a constant parameter m. Use this instead of F(φ, m) if you use the same value of m many times.
		/// </summary>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
		public static Func<Double, Double> FDouble(double m)
		{
			var k = K(m);

			return φ => 
			{
				Double offset = 0;

				if (Math.Abs(φ) > π / 2)
				{
					int n = (int)Math.Round(φ / π);
					φ -= n * π;
					offset = n * 2 * k;
				}

				Double
					sinφ = Math.Sin(φ),
					cosφ = Math.Cos(φ);

				return sinφ * CarlsonSymmetric.RF(cosφ * cosφ, 1 - m * sinφ * sinφ, 1) + offset;
			};
		}
	}
}
