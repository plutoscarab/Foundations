
using static Foundations.Constants;
using static Foundations.Functions.Elliptic;
using static Foundations.Functions.Theta;

namespace Foundations.Functions
{
    /// <summary>
    /// Jacobi elliptic functions.
    /// </summary>
    public static partial class Jacobi
    {

        /// <summary>
        /// Jacobi elliptic cn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Complex cn(Complex φ, double m)
        {
            double K, q = Nome(m, out K);
            Complex ζ = φ * (π / (2 * K));
            return (θ4((double)0, q) / θ2((double)0, q)) * (θ2(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic cn for parameter m.
        /// Use this instead of <see cref="cn(Complex, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<Complex, Complex> cnComplex(double m)
        {
            double kf, f;
            Func<Complex, Complex> t2, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ4((double)0, q) / θ2((double)0, q);
                t2 = θ2ComplexForNome(q);
                t4 = θ4ComplexForNome(q);
            }

            return φ => 
            {
                Complex ζ = kf * φ;
                return f * (t2(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic sn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Complex sn(Complex φ, double m)
        {
            double K, q = Nome(m, out K);
            Complex ζ = φ * (π / (2 * K));
            return (θ3((double)0, q) / θ2((double)0, q)) * (θ1(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic sn for parameter m.
        /// Use this instead of <see cref="sn(Complex, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<Complex, Complex> snComplex(double m)
        {
            double kf, f;
            Func<Complex, Complex> t1, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ3((double)0, q) / θ2((double)0, q);
                t1 = θ1ComplexForNome(q);
                t4 = θ4ComplexForNome(q);
            }

            return φ =>
            {
                Complex ζ = kf * φ;
                return f * (t1(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic dn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Complex dn(Complex φ, double m)
        {
            double K, q = Nome(m, out K);
            Complex ζ = φ * (π / (2 * K));
            return (θ4((double)0, q) / θ3((double)0, q)) * (θ3(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic dn for parameter m.
        /// Use this instead of <see cref="dn(Complex, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<Complex, Complex> dnComplex(double m)
        {
            double kf, f;
            Func<Complex, Complex> t3, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ4((double)0, q) / θ3((double)0, q);
                t3 = θ3ComplexForNome(q);
                t4 = θ4ComplexForNome(q);
            }

            return φ =>
            {
                Complex ζ = kf * φ;
                return f * (t3(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic sn, cn, dn.
        /// </summary>
        public struct JacobiComplex
        {
            /// <summary>Jacobi elliptic sn.</summary>
            public readonly Complex sn;

            /// <summary>Jacobi elliptic cn.</summary>
            public readonly Complex cn;

            /// <summary>Jacobi elliptic dn.</summary>
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
            double K, q = Nome(m, out K);

            Complex 
                ζ = φ * (π / (2 * K)),
                z2 = θ2((double)0, q),
                z3 = θ3((double)0, q),
                z4 = θ4((double)0, q),
                t4 = θ4(ζ, q);

            return new JacobiComplex(
                (z3 / z2) * (θ1(ζ, q) / t4),
                (z4 / z2) * (θ2(ζ, q) / t4),
                (z4 / z3) * (θ3(ζ, q) / t4));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic cn, sn, and dn.
        /// This is faster than calling each function individually if you need two or all three of the results.
        /// Use this instead of <see cref="Multi(Complex, double)"/> if the same value of m is used many times.
        /// </summary>
        public static Func<Complex, JacobiComplex> MultiComplex(double m)
        {
            double kf, z32, z42, z43;
            Func<Complex, Complex> t1, t2, t3, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                t1 = θ1ComplexForNome(q);
                t2 = θ2ComplexForNome(q);
                t3 = θ3ComplexForNome(q);
                t4 = θ4ComplexForNome(q);

                double
                    z2 = θ2((double)0, q),
                    z3 = θ3((double)0, q),
                    z4 = θ4((double)0, q);

                z32 = z3 / z2;
                z42 = z4 / z2;
                z43 = z4 / z3;
            }

            return φ => 
            {
                Complex 
                    ζ = kf * φ,
                    d4 = t4(ζ);

                return new JacobiComplex(
                    z32 * (t1(ζ) / d4),
                    z42 * (t2(ζ) / d4),
                    z43 * (t3(ζ) / d4)
                );
            };
        }

        /// <summary>
        /// Jacobi elliptic amplitude.
        /// </summary>
        public static Complex am(Complex u, double m)
        {
            return Complex.Asin(sn(u, m));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic amplitude function for parameter m.
        /// Use this instead of <see cref="am(Complex, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<Complex, Complex> amComplex(double m)
        {
            var fsn = snComplex(m);
            return u => Complex.Asin(fsn(u));
        }

        /// <summary>
        /// Jacobi elliptic inverse cn.
        /// </summary>
        public static Complex arccn(Complex z, double m)
        {
            return F(Complex.Acos(z), m);
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic inverse cn for parameter m.
        /// Use this instead of <see cref="arccn(Complex, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<Complex, Complex> arccnComplex(double m)
        {
            var ef = FComplex(m);
            return z => ef(Complex.Acos(z));
        }

        /// <summary>
        /// Jacobi elliptic arcsine.
        /// </summary>
        public static Complex arcsn(Complex z, double m)
        {
            return F(Complex.Asin(z), m);
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic inverse sn for parameter m.
        /// Use this instead of <see cref="arcsn(Complex, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<Complex, Complex> arcsnComplex(double m)
        {
            var ef = FComplex(m);
            return z => ef(Complex.Asin(z));
        }

        /// <summary>
        /// Jacobi elliptic cn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Double cn(Double φ, double m)
        {
            double K, q = Nome(m, out K);
            Double ζ = φ * (π / (2 * K));
            return (θ4((double)0, q) / θ2((double)0, q)) * (θ2(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic cn for parameter m.
        /// Use this instead of <see cref="cn(Double, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<Double, Double> cnDouble(double m)
        {
            double kf, f;
            Func<Double, Double> t2, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ4((double)0, q) / θ2((double)0, q);
                t2 = θ2DoubleForNome(q);
                t4 = θ4DoubleForNome(q);
            }

            return φ => 
            {
                Double ζ = kf * φ;
                return f * (t2(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic sn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Double sn(Double φ, double m)
        {
            double K, q = Nome(m, out K);
            Double ζ = φ * (π / (2 * K));
            return (θ3((double)0, q) / θ2((double)0, q)) * (θ1(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic sn for parameter m.
        /// Use this instead of <see cref="sn(Double, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<Double, Double> snDouble(double m)
        {
            double kf, f;
            Func<Double, Double> t1, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ3((double)0, q) / θ2((double)0, q);
                t1 = θ1DoubleForNome(q);
                t4 = θ4DoubleForNome(q);
            }

            return φ =>
            {
                Double ζ = kf * φ;
                return f * (t1(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic dn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Double dn(Double φ, double m)
        {
            double K, q = Nome(m, out K);
            Double ζ = φ * (π / (2 * K));
            return (θ4((double)0, q) / θ3((double)0, q)) * (θ3(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic dn for parameter m.
        /// Use this instead of <see cref="dn(Double, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<Double, Double> dnDouble(double m)
        {
            double kf, f;
            Func<Double, Double> t3, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ4((double)0, q) / θ3((double)0, q);
                t3 = θ3DoubleForNome(q);
                t4 = θ4DoubleForNome(q);
            }

            return φ =>
            {
                Double ζ = kf * φ;
                return f * (t3(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic sn, cn, dn.
        /// </summary>
        public struct JacobiDouble
        {
            /// <summary>Jacobi elliptic sn.</summary>
            public readonly Double sn;

            /// <summary>Jacobi elliptic cn.</summary>
            public readonly Double cn;

            /// <summary>Jacobi elliptic dn.</summary>
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
            double K, q = Nome(m, out K);

            Double 
                ζ = φ * (π / (2 * K)),
                z2 = θ2((double)0, q),
                z3 = θ3((double)0, q),
                z4 = θ4((double)0, q),
                t4 = θ4(ζ, q);

            return new JacobiDouble(
                (z3 / z2) * (θ1(ζ, q) / t4),
                (z4 / z2) * (θ2(ζ, q) / t4),
                (z4 / z3) * (θ3(ζ, q) / t4));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic cn, sn, and dn.
        /// This is faster than calling each function individually if you need two or all three of the results.
        /// Use this instead of <see cref="Multi(Double, double)"/> if the same value of m is used many times.
        /// </summary>
        public static Func<Double, JacobiDouble> MultiDouble(double m)
        {
            double kf, z32, z42, z43;
            Func<Double, Double> t1, t2, t3, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                t1 = θ1DoubleForNome(q);
                t2 = θ2DoubleForNome(q);
                t3 = θ3DoubleForNome(q);
                t4 = θ4DoubleForNome(q);

                double
                    z2 = θ2((double)0, q),
                    z3 = θ3((double)0, q),
                    z4 = θ4((double)0, q);

                z32 = z3 / z2;
                z42 = z4 / z2;
                z43 = z4 / z3;
            }

            return φ => 
            {
                Double 
                    ζ = kf * φ,
                    d4 = t4(ζ);

                return new JacobiDouble(
                    z32 * (t1(ζ) / d4),
                    z42 * (t2(ζ) / d4),
                    z43 * (t3(ζ) / d4)
                );
            };
        }

        /// <summary>
        /// Jacobi elliptic amplitude.
        /// </summary>
        public static Double am(Double u, double m)
        {
            return Math.Asin(sn(u, m));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic amplitude function for parameter m.
        /// Use this instead of <see cref="am(Double, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<Double, Double> amDouble(double m)
        {
            var fsn = snDouble(m);
            return u => Math.Asin(fsn(u));
        }

        /// <summary>
        /// Jacobi elliptic inverse cn.
        /// </summary>
        public static Double arccn(Double z, double m)
        {
            return F(Math.Acos(z), m);
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic inverse cn for parameter m.
        /// Use this instead of <see cref="arccn(Double, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<Double, Double> arccnDouble(double m)
        {
            var ef = FDouble(m);
            return z => ef(Math.Acos(z));
        }

        /// <summary>
        /// Jacobi elliptic arcsine.
        /// </summary>
        public static Double arcsn(Double z, double m)
        {
            return F(Math.Asin(z), m);
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic inverse sn for parameter m.
        /// Use this instead of <see cref="arcsn(Double, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<Double, Double> arcsnDouble(double m)
        {
            var ef = FDouble(m);
            return z => ef(Math.Asin(z));
        }

        /// <summary>
        /// Jacobi elliptic cn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static ComplexQuad cn(ComplexQuad φ, double m)
        {
            double K, q = Nome(m, out K);
            ComplexQuad ζ = φ * (π / (2 * K));
            return (θ4((Quad)0, q) / θ2((Quad)0, q)) * (θ2(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic cn for parameter m.
        /// Use this instead of <see cref="cn(ComplexQuad, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<ComplexQuad, ComplexQuad> cnComplexQuad(double m)
        {
            double kf, f;
            Func<ComplexQuad, ComplexQuad> t2, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ4((Quad)0, q) / θ2((Quad)0, q);
                t2 = θ2ComplexQuadForNome(q);
                t4 = θ4ComplexQuadForNome(q);
            }

            return φ => 
            {
                ComplexQuad ζ = kf * φ;
                return f * (t2(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic sn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static ComplexQuad sn(ComplexQuad φ, double m)
        {
            double K, q = Nome(m, out K);
            ComplexQuad ζ = φ * (π / (2 * K));
            return (θ3((Quad)0, q) / θ2((Quad)0, q)) * (θ1(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic sn for parameter m.
        /// Use this instead of <see cref="sn(ComplexQuad, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<ComplexQuad, ComplexQuad> snComplexQuad(double m)
        {
            double kf, f;
            Func<ComplexQuad, ComplexQuad> t1, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ3((Quad)0, q) / θ2((Quad)0, q);
                t1 = θ1ComplexQuadForNome(q);
                t4 = θ4ComplexQuadForNome(q);
            }

            return φ =>
            {
                ComplexQuad ζ = kf * φ;
                return f * (t1(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic dn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static ComplexQuad dn(ComplexQuad φ, double m)
        {
            double K, q = Nome(m, out K);
            ComplexQuad ζ = φ * (π / (2 * K));
            return (θ4((Quad)0, q) / θ3((Quad)0, q)) * (θ3(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic dn for parameter m.
        /// Use this instead of <see cref="dn(ComplexQuad, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<ComplexQuad, ComplexQuad> dnComplexQuad(double m)
        {
            double kf, f;
            Func<ComplexQuad, ComplexQuad> t3, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ4((Quad)0, q) / θ3((Quad)0, q);
                t3 = θ3ComplexQuadForNome(q);
                t4 = θ4ComplexQuadForNome(q);
            }

            return φ =>
            {
                ComplexQuad ζ = kf * φ;
                return f * (t3(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic sn, cn, dn.
        /// </summary>
        public struct JacobiComplexQuad
        {
            /// <summary>Jacobi elliptic sn.</summary>
            public readonly ComplexQuad sn;

            /// <summary>Jacobi elliptic cn.</summary>
            public readonly ComplexQuad cn;

            /// <summary>Jacobi elliptic dn.</summary>
            public readonly ComplexQuad dn;

            /// <summary>Constructor.</summary>
            public JacobiComplexQuad(ComplexQuad sn, ComplexQuad cn, ComplexQuad dn)
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
        public static JacobiComplexQuad Multi(ComplexQuad φ, double m)
        {
            double K, q = Nome(m, out K);

            ComplexQuad 
                ζ = φ * (π / (2 * K)),
                z2 = θ2((Quad)0, q),
                z3 = θ3((Quad)0, q),
                z4 = θ4((Quad)0, q),
                t4 = θ4(ζ, q);

            return new JacobiComplexQuad(
                (z3 / z2) * (θ1(ζ, q) / t4),
                (z4 / z2) * (θ2(ζ, q) / t4),
                (z4 / z3) * (θ3(ζ, q) / t4));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic cn, sn, and dn.
        /// This is faster than calling each function individually if you need two or all three of the results.
        /// Use this instead of <see cref="Multi(ComplexQuad, double)"/> if the same value of m is used many times.
        /// </summary>
        public static Func<ComplexQuad, JacobiComplexQuad> MultiComplexQuad(double m)
        {
            double kf, z32, z42, z43;
            Func<ComplexQuad, ComplexQuad> t1, t2, t3, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                t1 = θ1ComplexQuadForNome(q);
                t2 = θ2ComplexQuadForNome(q);
                t3 = θ3ComplexQuadForNome(q);
                t4 = θ4ComplexQuadForNome(q);

                double
                    z2 = θ2((Quad)0, q),
                    z3 = θ3((Quad)0, q),
                    z4 = θ4((Quad)0, q);

                z32 = z3 / z2;
                z42 = z4 / z2;
                z43 = z4 / z3;
            }

            return φ => 
            {
                ComplexQuad 
                    ζ = kf * φ,
                    d4 = t4(ζ);

                return new JacobiComplexQuad(
                    z32 * (t1(ζ) / d4),
                    z42 * (t2(ζ) / d4),
                    z43 * (t3(ζ) / d4)
                );
            };
        }

        /// <summary>
        /// Jacobi elliptic amplitude.
        /// </summary>
        public static ComplexQuad am(ComplexQuad u, double m)
        {
            return ComplexQuad.Asin(sn(u, m));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic amplitude function for parameter m.
        /// Use this instead of <see cref="am(ComplexQuad, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<ComplexQuad, ComplexQuad> amComplexQuad(double m)
        {
            var fsn = snComplexQuad(m);
            return u => ComplexQuad.Asin(fsn(u));
        }

        /// <summary>
        /// Jacobi elliptic inverse cn.
        /// </summary>
        public static ComplexQuad arccn(ComplexQuad z, double m)
        {
            return F(ComplexQuad.Acos(z), m);
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic inverse cn for parameter m.
        /// Use this instead of <see cref="arccn(ComplexQuad, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<ComplexQuad, ComplexQuad> arccnComplexQuad(double m)
        {
            var ef = FComplexQuad(m);
            return z => ef(ComplexQuad.Acos(z));
        }

        /// <summary>
        /// Jacobi elliptic arcsine.
        /// </summary>
        public static ComplexQuad arcsn(ComplexQuad z, double m)
        {
            return F(ComplexQuad.Asin(z), m);
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic inverse sn for parameter m.
        /// Use this instead of <see cref="arcsn(ComplexQuad, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<ComplexQuad, ComplexQuad> arcsnComplexQuad(double m)
        {
            var ef = FComplexQuad(m);
            return z => ef(ComplexQuad.Asin(z));
        }

        /// <summary>
        /// Jacobi elliptic cn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Quad cn(Quad φ, double m)
        {
            double K, q = Nome(m, out K);
            Quad ζ = φ * (π / (2 * K));
            return (θ4((Quad)0, q) / θ2((Quad)0, q)) * (θ2(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic cn for parameter m.
        /// Use this instead of <see cref="cn(Quad, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<Quad, Quad> cnQuad(double m)
        {
            double kf, f;
            Func<Quad, Quad> t2, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ4((Quad)0, q) / θ2((Quad)0, q);
                t2 = θ2QuadForNome(q);
                t4 = θ4QuadForNome(q);
            }

            return φ => 
            {
                Quad ζ = kf * φ;
                return f * (t2(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic sn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Quad sn(Quad φ, double m)
        {
            double K, q = Nome(m, out K);
            Quad ζ = φ * (π / (2 * K));
            return (θ3((Quad)0, q) / θ2((Quad)0, q)) * (θ1(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic sn for parameter m.
        /// Use this instead of <see cref="sn(Quad, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<Quad, Quad> snQuad(double m)
        {
            double kf, f;
            Func<Quad, Quad> t1, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ3((Quad)0, q) / θ2((Quad)0, q);
                t1 = θ1QuadForNome(q);
                t4 = θ4QuadForNome(q);
            }

            return φ =>
            {
                Quad ζ = kf * φ;
                return f * (t1(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic dn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Quad dn(Quad φ, double m)
        {
            double K, q = Nome(m, out K);
            Quad ζ = φ * (π / (2 * K));
            return (θ4((Quad)0, q) / θ3((Quad)0, q)) * (θ3(ζ, q) / θ4(ζ, q));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic dn for parameter m.
        /// Use this instead of <see cref="dn(Quad, double)"/> if the same value of m is used many times.
        /// </summary>
        /// <param name="m">Parameter.</param>
        public static Func<Quad, Quad> dnQuad(double m)
        {
            double kf, f;
            Func<Quad, Quad> t3, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                f = θ4((Quad)0, q) / θ3((Quad)0, q);
                t3 = θ3QuadForNome(q);
                t4 = θ4QuadForNome(q);
            }

            return φ =>
            {
                Quad ζ = kf * φ;
                return f * (t3(ζ) / t4(ζ));
            };
        }

        /// <summary>
        /// Jacobi elliptic sn, cn, dn.
        /// </summary>
        public struct JacobiQuad
        {
            /// <summary>Jacobi elliptic sn.</summary>
            public readonly Quad sn;

            /// <summary>Jacobi elliptic cn.</summary>
            public readonly Quad cn;

            /// <summary>Jacobi elliptic dn.</summary>
            public readonly Quad dn;

            /// <summary>Constructor.</summary>
            public JacobiQuad(Quad sn, Quad cn, Quad dn)
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
        public static JacobiQuad Multi(Quad φ, double m)
        {
            double K, q = Nome(m, out K);

            Quad 
                ζ = φ * (π / (2 * K)),
                z2 = θ2((Quad)0, q),
                z3 = θ3((Quad)0, q),
                z4 = θ4((Quad)0, q),
                t4 = θ4(ζ, q);

            return new JacobiQuad(
                (z3 / z2) * (θ1(ζ, q) / t4),
                (z4 / z2) * (θ2(ζ, q) / t4),
                (z4 / z3) * (θ3(ζ, q) / t4));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic cn, sn, and dn.
        /// This is faster than calling each function individually if you need two or all three of the results.
        /// Use this instead of <see cref="Multi(Quad, double)"/> if the same value of m is used many times.
        /// </summary>
        public static Func<Quad, JacobiQuad> MultiQuad(double m)
        {
            double kf, z32, z42, z43;
            Func<Quad, Quad> t1, t2, t3, t4;

            {
                double K, q = Nome(m, out K);
                kf = π / (2 * K);
                t1 = θ1QuadForNome(q);
                t2 = θ2QuadForNome(q);
                t3 = θ3QuadForNome(q);
                t4 = θ4QuadForNome(q);

                double
                    z2 = θ2((Quad)0, q),
                    z3 = θ3((Quad)0, q),
                    z4 = θ4((Quad)0, q);

                z32 = z3 / z2;
                z42 = z4 / z2;
                z43 = z4 / z3;
            }

            return φ => 
            {
                Quad 
                    ζ = kf * φ,
                    d4 = t4(ζ);

                return new JacobiQuad(
                    z32 * (t1(ζ) / d4),
                    z42 * (t2(ζ) / d4),
                    z43 * (t3(ζ) / d4)
                );
            };
        }

        /// <summary>
        /// Jacobi elliptic amplitude.
        /// </summary>
        public static Quad am(Quad u, double m)
        {
            return Quad.Asin(sn(u, m));
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic amplitude function for parameter m.
        /// Use this instead of <see cref="am(Quad, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<Quad, Quad> amQuad(double m)
        {
            var fsn = snQuad(m);
            return u => Quad.Asin(fsn(u));
        }

        /// <summary>
        /// Jacobi elliptic inverse cn.
        /// </summary>
        public static Quad arccn(Quad z, double m)
        {
            return F(Quad.Acos(z), m);
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic inverse cn for parameter m.
        /// Use this instead of <see cref="arccn(Quad, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<Quad, Quad> arccnQuad(double m)
        {
            var ef = FQuad(m);
            return z => ef(Quad.Acos(z));
        }

        /// <summary>
        /// Jacobi elliptic arcsine.
        /// </summary>
        public static Quad arcsn(Quad z, double m)
        {
            return F(Quad.Asin(z), m);
        }

        /// <summary>
        /// Gets a function that computes the Jacobi elliptic inverse sn for parameter m.
        /// Use this instead of <see cref="arcsn(Quad, double)"/> if you use the same value of m many times.
        /// </summary>
        public static Func<Quad, Quad> arcsnQuad(double m)
        {
            var ef = FQuad(m);
            return z => ef(Quad.Asin(z));
        }
    }
}
