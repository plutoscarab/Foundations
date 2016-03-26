﻿
/*
Jacobi.cs

Copyright (c) 2016 Pluto Scarab Software. All Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
using System.Numerics;

using static Foundations.Functions.Numerics.Elliptic;
using static Foundations.Functions.Numerics.Theta;

namespace Foundations.Functions.Numerics
{
    /// <summary>
    /// Jacobi elliptic functions.
    /// </summary>
    public static partial class Jacobi
    {
        const double π = Math.PI;


        /// <summary>
        /// Jacobi elliptic cn.
        /// </summary>
        /// <param name="φ">Amplitude.</param>
        /// <param name="m">Parameter.</param>
        public static Complex cn(Complex φ, double m)
        {
            double K, q = Nome(m, out K);
            Complex ζ = φ * (π / (2 * K));
            return (θ4(0, q) / θ2(0, q)) * (θ2(ζ, q) / θ4(ζ, q));
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
                f = θ4(0, q) / θ2(0, q);
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
            return (θ3(0, q) / θ2(0, q)) * (θ1(ζ, q) / θ4(ζ, q));
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
                f = θ3(0, q) / θ2(0, q);
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
            return (θ4(0, q) / θ3(0, q)) * (θ3(ζ, q) / θ4(ζ, q));
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
                f = θ4(0, q) / θ3(0, q);
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
                z2 = θ2(0, q),
                z3 = θ3(0, q),
                z4 = θ4(0, q),
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
                    z2 = θ2(0, q),
                    z3 = θ3(0, q),
                    z4 = θ4(0, q);

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
            return (θ4(0, q) / θ2(0, q)) * (θ2(ζ, q) / θ4(ζ, q));
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
                f = θ4(0, q) / θ2(0, q);
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
            return (θ3(0, q) / θ2(0, q)) * (θ1(ζ, q) / θ4(ζ, q));
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
                f = θ3(0, q) / θ2(0, q);
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
            return (θ4(0, q) / θ3(0, q)) * (θ3(ζ, q) / θ4(ζ, q));
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
                f = θ4(0, q) / θ3(0, q);
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
                z2 = θ2(0, q),
                z3 = θ3(0, q),
                z4 = θ4(0, q),
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
                    z2 = θ2(0, q),
                    z3 = θ3(0, q),
                    z4 = θ4(0, q);

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
    }
}
