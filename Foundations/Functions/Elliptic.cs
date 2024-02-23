
using static Foundations.Constants;

namespace Foundations.Functions
{
    /// <summary>
    /// Elliptic integrals.
    /// </summary>
    /// <remarks>
    /// Evaluated using Carlson symmetric form.
    /// </remarks>
    public static partial class Elliptic
    {
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
            return Math.Exp(-π * K(1 - m) / K(m));
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

        /// <summary>
        /// Incomplete elliptic integral of the first kind F(φ | m).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static ComplexQuad F(ComplexQuad φ, double m)
        {
            if (Quad.Abs(φ.Re) > π / 2)
            {
                int n = (int)Quad.Round(φ.Re / π);
                return F(φ - n * π, m) + n * 2 * K(m);
            }

            ComplexQuad
                sinφ = ComplexQuad.Sin(φ),
                cosφ = ComplexQuad.Cos(φ);

            return sinφ * CarlsonSymmetric.RF(cosφ * cosφ, 1 - m * sinφ * sinφ, 1);
        }

        /// <summary>
        /// Get a function F(φ) that computes the incomplete elliptic integral of the first kind F(φ | m)
        /// for a constant parameter m. Use this instead of F(φ, m) if you use the same value of m many times.
        /// </summary>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static Func<ComplexQuad, ComplexQuad> FComplexQuad(double m)
        {
            var k = K(m);

            return φ => 
            {
                ComplexQuad offset = 0;

                if (Quad.Abs(φ.Re) > π / 2)
                {
                    int n = (int)Quad.Round(φ.Re / π);
                    φ -= n * π;
                    offset = n * 2 * k;
                }

                ComplexQuad
                    sinφ = ComplexQuad.Sin(φ),
                    cosφ = ComplexQuad.Cos(φ);

                return sinφ * CarlsonSymmetric.RF(cosφ * cosφ, 1 - m * sinφ * sinφ, 1) + offset;
            };
        }

        /// <summary>
        /// Incomplete elliptic integral of the first kind F(φ | m).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static Quad F(Quad φ, double m)
        {
            if (Quad.Abs(φ) > π / 2)
            {
                int n = (int)Quad.Round(φ / π);
                return F(φ - n * π, m) + n * 2 * K(m);
            }

            Quad
                sinφ = Quad.Sin(φ),
                cosφ = Quad.Cos(φ);

            return sinφ * CarlsonSymmetric.RF(cosφ * cosφ, 1 - m * sinφ * sinφ, 1);
        }

        /// <summary>
        /// Get a function F(φ) that computes the incomplete elliptic integral of the first kind F(φ | m)
        /// for a constant parameter m. Use this instead of F(φ, m) if you use the same value of m many times.
        /// </summary>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static Func<Quad, Quad> FQuad(double m)
        {
            var k = K(m);

            return φ => 
            {
                Quad offset = 0;

                if (Quad.Abs(φ) > π / 2)
                {
                    int n = (int)Quad.Round(φ / π);
                    φ -= n * π;
                    offset = n * 2 * k;
                }

                Quad
                    sinφ = Quad.Sin(φ),
                    cosφ = Quad.Cos(φ);

                return sinφ * CarlsonSymmetric.RF(cosφ * cosφ, 1 - m * sinφ * sinφ, 1) + offset;
            };
        }

        /// <summary>
        /// Incomplete elliptic integral of the second kind E(φ | m).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static Complex E(Complex φ, double m)
        {
			if (φ == 0) return 0;

            Complex
                sinφ = Complex.Sin(φ),
                c = 1 / (sinφ * sinφ);

            return CarlsonSymmetric.RF(c - 1, c - m, c) - (m / 3) * CarlsonSymmetric.RD(c - 1, c - m, c);
        }

        /// <summary>
        /// Incomplete elliptic integral of the second kind E(φ | m).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static Double E(Double φ, double m)
        {
			if (φ == 0) return 0;

            Double
                sinφ = Math.Sin(φ),
                c = 1 / (sinφ * sinφ);

            return CarlsonSymmetric.RF(c - 1, c - m, c) - (m / 3) * CarlsonSymmetric.RD(c - 1, c - m, c);
        }

        /// <summary>
        /// Incomplete elliptic integral of the second kind E(φ | m).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static ComplexQuad E(ComplexQuad φ, double m)
        {
			if (φ == 0) return 0;

            ComplexQuad
                sinφ = ComplexQuad.Sin(φ),
                c = 1 / (sinφ * sinφ);

            return CarlsonSymmetric.RF(c - 1, c - m, c) - (m / 3) * CarlsonSymmetric.RD(c - 1, c - m, c);
        }

        /// <summary>
        /// Incomplete elliptic integral of the second kind E(φ | m).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
        public static Quad E(Quad φ, double m)
        {
			if (φ == 0) return 0;

            Quad
                sinφ = Quad.Sin(φ),
                c = 1 / (sinφ * sinφ);

            return CarlsonSymmetric.RF(c - 1, c - m, c) - (m / 3) * CarlsonSymmetric.RD(c - 1, c - m, c);
        }

        /// <summary>
        /// Incomplete elliptic integral of the third kind Π(φ | m, n).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
		/// <param name="n" />
        public static Complex Π(Complex φ, double m, Complex n)
        {
            Complex
                sinφ = Complex.Sin(φ),
                c = 1 / (sinφ * sinφ);

            return CarlsonSymmetric.RF(c - 1, c - m, c) - (n / 3) * CarlsonSymmetric.RJ(c - 1, c - m, c, c + n);
        }

        /// <summary>
        /// Incomplete elliptic integral of the third kind Π(φ | m, n).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
		/// <param name="n" />
        public static Double Π(Double φ, double m, Double n)
        {
            Double
                sinφ = Math.Sin(φ),
                c = 1 / (sinφ * sinφ);

            return CarlsonSymmetric.RF(c - 1, c - m, c) - (n / 3) * CarlsonSymmetric.RJ(c - 1, c - m, c, c + n);
        }

        /// <summary>
        /// Incomplete elliptic integral of the third kind Π(φ | m, n).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
		/// <param name="n" />
        public static ComplexQuad Π(ComplexQuad φ, double m, ComplexQuad n)
        {
            ComplexQuad
                sinφ = ComplexQuad.Sin(φ),
                c = 1 / (sinφ * sinφ);

            return CarlsonSymmetric.RF(c - 1, c - m, c) - (n / 3) * CarlsonSymmetric.RJ(c - 1, c - m, c, c + n);
        }

        /// <summary>
        /// Incomplete elliptic integral of the third kind Π(φ | m, n).
        /// </summary>
        /// <param name="φ">Argument.</param>
        /// <param name="m">Parameter, equal to k², the square of the modulus.</param>
		/// <param name="n" />
        public static Quad Π(Quad φ, double m, Quad n)
        {
            Quad
                sinφ = Quad.Sin(φ),
                c = 1 / (sinφ * sinφ);

            return CarlsonSymmetric.RF(c - 1, c - m, c) - (n / 3) * CarlsonSymmetric.RJ(c - 1, c - m, c, c + n);
        }
    }
}
