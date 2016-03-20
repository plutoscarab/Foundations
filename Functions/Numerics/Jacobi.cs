
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Foundations.Functions.Numerics
{
	/// <summary>
	/// Jacobi elliptic functions.
	/// </summary>
	public static partial class Jacobi
	{
		const double π = Math.PI;


		/// <summary>
		/// Jacobi elliptic cosine.
		/// </summary>
		/// <param name="φ">Amplitude.</param>
		/// <param name="m">Parameter.</param>
		public static Complex cn(Complex φ, double m)
		{
            double K, q = Elliptic.Nome(m, out K);
            Complex ζ = π * φ / (2 * K);
			return (Theta.θ4(0, q) / Theta.θ2(0, q)) * (Theta.θ2(ζ, q) / Theta.θ4(ζ, q));
		}

		/// <summary>
		/// Gets a function that computes the Jacobi elliptic cosine for parameter m.
		/// Use this instead of <see cref="cn(Complex, double)"/> if the same value of m is used many times.
		/// </summary>
		/// <param name="m">Parameter.</param>
		public static Func<Complex, Complex> cnComplex(double m)
		{
			Func<Complex, Complex> t2, t4;
			Complex f;
			double kf;

			{
				double K, q = Elliptic.Nome(m, out K);
				kf = π / (2 * K);
				t2 = Theta.θ2ComplexForNome(q);
				t4 = Theta.θ4ComplexForNome(q);
				f = t4(0) / t2(0);
			}

			return φ => 
			{
				Complex ζ = kf * φ;
				return f * (t2(ζ) / t4(ζ));
			};
		}

		/// <summary>
		/// Jacobi elliptic sine.
		/// </summary>
		/// <param name="φ">Amplitude.</param>
		/// <param name="m">Parameter.</param>
		public static Complex sn(Complex φ, double m)
		{
            double K, q = Elliptic.Nome(m, out K);
            Complex ζ = π * φ / (2 * K);
			return (Theta.θ3(0, q) / Theta.θ2(0, q)) * (Theta.θ1(ζ, q) / Theta.θ4(ζ, q));
		}

		/// <summary>
		/// Jacobi elliptic tangent.
		/// </summary>
		/// <param name="φ">Amplitude.</param>
		/// <param name="m">Parameter.</param>
		public static Complex dn(Complex φ, double m)
		{
            double K, q = Elliptic.Nome(m, out K);
            Complex ζ = π * φ / (2 * K);
			return (Theta.θ4(0, q) / Theta.θ3(0, q)) * (Theta.θ3(ζ, q) / Theta.θ4(ζ, q));
		}

		/// <summary>
		/// Jacobi elliptic sine, cosine, and tangent.
		/// </summary>
		public struct JacobiComplex
		{
			/// <summary>Jacobi elliptic sine.</summary>
			public readonly Complex sn;

			/// <summary>Jacobi elliptic cosine.</summary>
			public readonly Complex cn;

			/// <summary>Jacobi elliptic tangent.</summary>
			public readonly Complex dn;

			/// <summary>Constructor.</summary>
			public JacobiComplex(Complex sn, Complex cn, Complex dn)
			{
				this.sn = sn; this.cn = cn; this.dn = dn;
			}
		}

		/// <summary>
		/// Compute the Jacobi elliptic cn, sn, and dn functions.
		/// This is faster than calling each function individually if you need two or all three of the results.
		/// </summary>
		/// <param name="φ">Amplitude.</param>
		/// <param name="m">Parameter.</param>
		public static JacobiComplex Multi(Complex φ, double m)
		{
            double K, q = Elliptic.Nome(m, out K);

            Complex 
				ζ = π * φ / (2 * K),
				z2 = Theta.θ2(0, q),
				z3 = Theta.θ3(0, q),
				z4 = Theta.θ4(0, q),
				t4 = Theta.θ4(ζ, q);

			return new JacobiComplex(
				(z3 / z2) * (Theta.θ1(ζ, q) / t4),
				(z4 / z2) * (Theta.θ2(ζ, q) / t4),
				(z4 / z3) * (Theta.θ3(ζ, q) / t4));
		}

		/// <summary>
		/// Jacobi elliptic amplitude.
		/// </summary>
		public static Complex am(Complex u, double m)
		{
			return Complex.Asin(sn(u, m));
		}

		/// <summary>
		/// Jacobi elliptic arccosine.
		/// </summary>
		public static Complex arccn(Complex z, double m)
		{
			return Elliptic.F(Complex.Acos(z), m);
		}

		/// <summary>
		/// Gets a function that computes the Jacobi elliptic arccosine for parameter m.
		/// Use this instead of <see cref="arccn(Complex, double)"/> if you use the same value of m many times.
		/// </summary>
		public static Func<Complex, Complex> arccnComplex(double m)
		{
			var ef = Elliptic.FComplex(m);
			return z => ef(Complex.Acos(z));
		}

		/// <summary>
		/// Jacobi elliptic arcsine.
		/// </summary>
		public static Complex arcsn(Complex z, double m)
		{
			return Elliptic.F(Complex.Asin(z), m);
		}

		/// <summary>
		/// Jacobi elliptic cosine.
		/// </summary>
		/// <param name="φ">Amplitude.</param>
		/// <param name="m">Parameter.</param>
		public static Double cn(Double φ, double m)
		{
            double K, q = Elliptic.Nome(m, out K);
            Double ζ = π * φ / (2 * K);
			return (Theta.θ4(0, q) / Theta.θ2(0, q)) * (Theta.θ2(ζ, q) / Theta.θ4(ζ, q));
		}

		/// <summary>
		/// Gets a function that computes the Jacobi elliptic cosine for parameter m.
		/// Use this instead of <see cref="cn(Double, double)"/> if the same value of m is used many times.
		/// </summary>
		/// <param name="m">Parameter.</param>
		public static Func<Double, Double> cnDouble(double m)
		{
			Func<Double, Double> t2, t4;
			Double f;
			double kf;

			{
				double K, q = Elliptic.Nome(m, out K);
				kf = π / (2 * K);
				t2 = Theta.θ2DoubleForNome(q);
				t4 = Theta.θ4DoubleForNome(q);
				f = t4(0) / t2(0);
			}

			return φ => 
			{
				Double ζ = kf * φ;
				return f * (t2(ζ) / t4(ζ));
			};
		}

		/// <summary>
		/// Jacobi elliptic sine.
		/// </summary>
		/// <param name="φ">Amplitude.</param>
		/// <param name="m">Parameter.</param>
		public static Double sn(Double φ, double m)
		{
            double K, q = Elliptic.Nome(m, out K);
            Double ζ = π * φ / (2 * K);
			return (Theta.θ3(0, q) / Theta.θ2(0, q)) * (Theta.θ1(ζ, q) / Theta.θ4(ζ, q));
		}

		/// <summary>
		/// Jacobi elliptic tangent.
		/// </summary>
		/// <param name="φ">Amplitude.</param>
		/// <param name="m">Parameter.</param>
		public static Double dn(Double φ, double m)
		{
            double K, q = Elliptic.Nome(m, out K);
            Double ζ = π * φ / (2 * K);
			return (Theta.θ4(0, q) / Theta.θ3(0, q)) * (Theta.θ3(ζ, q) / Theta.θ4(ζ, q));
		}

		/// <summary>
		/// Jacobi elliptic sine, cosine, and tangent.
		/// </summary>
		public struct JacobiDouble
		{
			/// <summary>Jacobi elliptic sine.</summary>
			public readonly Double sn;

			/// <summary>Jacobi elliptic cosine.</summary>
			public readonly Double cn;

			/// <summary>Jacobi elliptic tangent.</summary>
			public readonly Double dn;

			/// <summary>Constructor.</summary>
			public JacobiDouble(Double sn, Double cn, Double dn)
			{
				this.sn = sn; this.cn = cn; this.dn = dn;
			}
		}

		/// <summary>
		/// Compute the Jacobi elliptic cn, sn, and dn functions.
		/// This is faster than calling each function individually if you need two or all three of the results.
		/// </summary>
		/// <param name="φ">Amplitude.</param>
		/// <param name="m">Parameter.</param>
		public static JacobiDouble Multi(Double φ, double m)
		{
            double K, q = Elliptic.Nome(m, out K);

            Double 
				ζ = π * φ / (2 * K),
				z2 = Theta.θ2(0, q),
				z3 = Theta.θ3(0, q),
				z4 = Theta.θ4(0, q),
				t4 = Theta.θ4(ζ, q);

			return new JacobiDouble(
				(z3 / z2) * (Theta.θ1(ζ, q) / t4),
				(z4 / z2) * (Theta.θ2(ζ, q) / t4),
				(z4 / z3) * (Theta.θ3(ζ, q) / t4));
		}

		/// <summary>
		/// Jacobi elliptic amplitude.
		/// </summary>
		public static Double am(Double u, double m)
		{
			return Math.Asin(sn(u, m));
		}

		/// <summary>
		/// Jacobi elliptic arccosine.
		/// </summary>
		public static Double arccn(Double z, double m)
		{
			return Elliptic.F(Math.Acos(z), m);
		}

		/// <summary>
		/// Gets a function that computes the Jacobi elliptic arccosine for parameter m.
		/// Use this instead of <see cref="arccn(Double, double)"/> if you use the same value of m many times.
		/// </summary>
		public static Func<Double, Double> arccnDouble(double m)
		{
			var ef = Elliptic.FDouble(m);
			return z => ef(Math.Acos(z));
		}

		/// <summary>
		/// Jacobi elliptic arcsine.
		/// </summary>
		public static Double arcsn(Double z, double m)
		{
			return Elliptic.F(Math.Asin(z), m);
		}
	}
}
