﻿
/*
Class1.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Foundations.Types
{
    /// <summary>
    /// A ratio of two integers.
    /// </summary>
    public struct Rational : IEquatable<Rational>, IComparable<Rational>
    {
        /// <summary>
        /// The constant 0.
        /// </summary>
        public static readonly Rational Zero = CreateRaw(0, 1);

        /// <summary>
        /// The constant 1.
        /// </summary>
        public static readonly Rational One = CreateRaw(1, 1);

        /// <summary>
        /// The constant 1/2.
        /// </summary>
        public static readonly Rational OneHalf = CreateRaw(1, 2);

        /// <summary>
        /// Not a rational number.
        /// </summary>
        public static readonly Rational NaN = new Rational();

        /// <summary>
        /// Positive infinity.
        /// </summary>
        public static readonly Rational PositiveInfinity = CreateRaw(1, 0);

        /// <summary>
        /// Negative infinity.
        /// </summary>
        public static readonly Rational NegativeInfinity = CreateRaw(-1, 0);

        /// <summary>
        /// Negative zero.
        /// </summary>
        public static readonly Rational NegativeZero = CreateRaw(0, -1);

        private static readonly Rational decimalMax = (BigInteger)decimal.MaxValue;
        private static readonly Rational decimalMin = (BigInteger)decimal.MinValue;

        BigInteger p;
        BigInteger q;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="p">Numerator.</param>
        /// <param name="q">Denominator.</param>
        public Rational(BigInteger p, BigInteger q)
        {
            var g = BigInteger.GreatestCommonDivisor(p, q);

            if (!g.IsOne)
            {
                p /= g;
                q /= g;
            }

            this.p = p;
            this.q = q;
        }

        private static Rational CreateRaw(BigInteger p, BigInteger q)
        {
            var r = new Rational();
            r.p = p;
            r.q = q;
            return r;
        }

        /// <summary>
        /// Numerator.
        /// </summary>
        public BigInteger P { get { return p; } }

        /// <summary>
        /// Denominator.
        /// </summary>
        public BigInteger Q { get { return q; } }

        /// <summary>
        /// Indicates whether this <see cref="Rational"/> is zero.
        /// </summary>
        public bool IsZero { get { return p.IsZero && !q.IsZero; } }

        /// <summary>
        /// Indicates whether this <see cref="Rational"/> is NaN.
        /// </summary>
        public bool IsNaN { get { return p.IsZero && q.IsZero; } }

        /// <summary>
        /// Indicates whether this <see cref="Rational"/> is positive or negative infinity.
        /// </summary>
        public bool IsInfinity { get { return !p.IsZero && q.IsZero; } }

        /// <summary>
        /// Indicates whether this <see cref="Rational"/> is positive infinity.
        /// </summary>
        public bool IsPositiveInfinity { get { return p.Sign > 0 && q.IsZero; } }

        /// <summary>
        /// Indicates whether this <see cref="Rational"/> is negative infinity.
        /// </summary>
        public bool IsNegativeInfinity { get { return p.Sign < 0 && q.IsZero; } }

        /// <summary>
        /// Indicates whether this <see cref="Rational"/> is negative zero.
        /// </summary>
        public bool IsNegativeZero { get { return p.IsZero && q.Sign < 0; } }

        /// <summary>
        /// Gets a string representation of this <see cref="Rational"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}/{1}", P, Q);
        }

        /// <summary>
        /// Determine whether this <see cref="Rational"/> is equal to another value.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is Rational) return Equals((Rational)obj);
            return false;
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int h = P.GetHashCode();
            h = (h << 11) ^ (h >> 22);
            return h + Q.GetHashCode();
        }

        /// <summary>
        /// Indicates whether this <see cref="Rational"/> is equal to another.
        /// </summary>
        public bool Equals(Rational r)
        {
            return this == r;
        }

        /// <summary>
        /// Implicitly casts a <see cref="Int64"/> to a <see cref="Rational"/>.
        /// </summary>
        public static implicit operator Rational(long n)
        {
            return CreateRaw(n, BigInteger.One);
        }

        /// <summary>
        /// Implicitly casts a <see cref="BigInteger"/> to a <see cref="Rational"/>.
        /// </summary>
        /// <param name="p"></param>
        public static implicit operator Rational(BigInteger p)
        {
            return CreateRaw(p, BigInteger.One);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static Rational operator +(Rational a, Rational b)
        {
            return new Rational(a.p * b.q + a.q * b.p, a.q * b.q);
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static Rational operator -(Rational a, Rational b)
        {
            return new Rational(a.p * b.q - a.q * b.p, a.q * b.q);
        }

        /// <summary>
        /// Negation operator.
        /// </summary>
        public static Rational operator -(Rational r)
        {
            return new Rational(-r.p, r.q);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static Rational operator *(Rational a, Rational b)
        {
            return new Rational(a.p * b.p, a.q * b.q);
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static Rational operator /(Rational a, Rational b)
        {
            return new Rational(a.p * b.q, a.q * b.p);
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(Rational a, Rational b)
        {
            return a.p.Equals(b.p) && a.q.Equals(b.q);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(Rational a, Rational b)
        {
            return !a.p.Equals(b.p) || !a.q.Equals(b.q);
        }

        /// <summary>
        /// Less-than operator.
        /// </summary>
        public static bool operator <(Rational a, Rational b)
        {
            return Rational.Compare(a, b) < 0;
        }

        /// <summary>
        /// Less-than-or-equal-to operator.
        /// </summary>
        public static bool operator <=(Rational a, Rational b)
        {
            return Rational.Compare(a, b) <= 0;
        }

        /// <summary>
        /// Greater-than operator.
        /// </summary>
        public static bool operator >(Rational a, Rational b)
        {
            return Rational.Compare(a, b) > 0;
        }

        /// <summary>
        /// Greater-than-or-equal-to operator.
        /// </summary>
        public static bool operator >=(Rational a, Rational b)
        {
            return Rational.Compare(a, b) >= 0;
        }

        /// <summary>
        /// 1 / r.
        /// </summary>
        public static Rational Reciprocal(Rational r)
        {
            return CreateRaw(r.q, r.p);
        }

        /// <summary>
        /// |r|
        /// </summary>
        public static Rational Abs(Rational r)
        {
            return CreateRaw(BigInteger.Abs(r.p), BigInteger.Abs(r.q));
        }

        /// <summary>
        /// Gets the sign of a - b.
        /// </summary>
        public static int Compare(Rational a, Rational b)
        {
            Rational diff = a - b;
            if (diff.p.IsZero || diff.q.IsZero) return 0;
            if (diff.p.Sign == diff.q.Sign) return 1;
            return -1;
        }

        /// <summary>
        /// Gets the lesser of a and b.
        /// </summary>
        public static Rational Min(Rational a, Rational b)
        {
            return a < b ? a : b;
        }

        /// <summary>
        /// Gets the greater of a and b.
        /// </summary>
        public static Rational Max(Rational a, Rational b)
        {
            return a > b ? a : b;
        }

        /// <summary>
        /// Gets a <see cref="Rational"/> raised to an integer power.
        /// </summary>
        public static Rational Pow(Rational r, int n)
        {
            return CreateRaw(BigInteger.Pow(r.p, n), BigInteger.Pow(r.q, n));
        }

        /// <summary>
        /// Implicitly casts a <see cref="System.Double"/> to a <see cref="Rational"/>.
        /// The result is a Rational identical to the original Double when the Rational 
        /// is converted to a Double, but is not necessarily exactly equal to the Double as-is.
        /// </summary>
        public static implicit operator Rational(double d)
        {
            Rational r = Zero;

            foreach (var c in ContinuedFraction.Convergents(d))
            {
                r = c;
                if (d == (double)c) break;
            }

            return r;
        }

        /// <summary>
        /// Implicitly cast a <see cref="System.Decimal"/> to a <see cref="Rational"/>.
        /// </summary>
        public static implicit operator Rational(decimal d)
        {
            Rational r = Zero;

            foreach (var c in ContinuedFraction.Convergents(d))
            {
                r = c;
                if (d == (decimal)c) break;
            }

            return r;
        }

        /// <summary>
        /// Gets the greatest integer less than or equal to a <see cref="Rational"/>.
        /// </summary>
        public static BigInteger Floor(Rational r)
        {
            if (r.q < 0) r = CreateRaw(-r.p, -r.q);
            if (r.q == 1) return r.p;
            if (r.p == 0) return 0;
            BigInteger rem;
            BigInteger div = BigInteger.DivRem(BigInteger.Abs(r.p), r.q, out rem);
            if (r.p > 0) return div;
            return div - 1;
        }

        /// <summary>
        /// Gets the least integer greater than or equal to a <see cref="Rational"/>.
        /// </summary>
        public static BigInteger Ceiling(Rational r)
        {
            if (r.q < 0) r = CreateRaw(-r.p, -r.q);
            if (r.q == 1) return r.p;
            if (r.p == 0) return 0;
            BigInteger rem;
            BigInteger div = BigInteger.DivRem(BigInteger.Abs(r.p), r.q, out rem);
            if (r.p < 0) return div;
            return div + 1;
        }

        /// <summary>
        /// Round toward zero.
        /// </summary>
        public static BigInteger Truncate(Rational r)
        {
            if (r < 0) return Ceiling(r);
            return Floor(r);
        }

        /// <summary>
        /// Gets the fractional portion of a <see cref="Rational"/>, or the value modulo 1.
        /// </summary>
        public static Rational Frac(Rational r)
        {
            return r - Rational.Floor(r);
        }

        /// <summary>
        /// Round a <see cref="Rational"/> to the nearest integer.
        /// </summary>
        public static BigInteger Round(Rational r)
        {
            return Rational.Floor(r + OneHalf);
        }

        /// <summary>
        /// Gets the binary digit representation of the fractional portion of a <see cref="Rational"/>.
        /// </summary>
        public static IEnumerable<int> FractionalDigits(Rational r)
        {
            r = Rational.Frac(r);

            while(true)
            {
                if (r < OneHalf)
                {
                    yield return 0;
                    r *=2;
                }
                else 
                {
                    yield return 1;
                    r = r * 2 - 1;
                }

                if (r == 0) break;
            }
        }

        /// <summary>
        /// Compares this <see cref="Rational"/> to another.
        /// </summary>
        public int CompareTo(Rational other)
        {
            return Rational.Compare(this, other);
        }

        /// <summary>
        /// Gets the base b digit representation of this <see cref="Rational"/>.
        /// </summary>
        public IEnumerable<int> BaseExpansion(int b)
        {
            if (this < Zero || this >= One)
                throw new InvalidOperationException("BaseExpansion can only be used for values in range [0, 1).");

            Rational r = this;

            while (true)
            {
                r *= b;
                BigInteger f = Rational.Floor(r);
                r -= f;
                if (r == 0) break;
                yield return (int)f;
            }
        }

        /// <summary>
        /// Explicitly casts a <see cref="Rational"/> to a <see cref="System.Double"/>.
        /// </summary>
        public static explicit operator double(Rational r)
        {
            return (double)r.p / (double)r.q;
        }

        /// <summary>
        /// Explicitly casts a <see cref="Rational"/> to a <see cref="System.Decimal"/>.
        /// </summary>
        /// <param name="r"></param>
        public static explicit operator decimal (Rational r)
        {
            if (r > decimalMax || r < decimalMin)
                throw new ArgumentOutOfRangeException();

            var t = Truncate(r);
            r = Abs(r - t);

            decimal d = (decimal)t;
            decimal f = t >= 0 ? 1m : -1m;

            foreach (var digit in r.BaseExpansion(10))
            {
                var g = f / 10m;

                if (g == 0m)
                {
                    if (digit < 5)
                        break;

                    d += f;
                    break;
                }

                f = g;
                d += f * digit;
            }

            return d;
        }

        /// <summary>
        /// Increment operator.
        /// </summary>
        public static Rational operator ++(Rational r)
        {
            return new Rational(r.p + r.q, r.q);
        }

        /// <summary>
        /// Decrement operator.
        /// </summary>
        public static Rational operator --(Rational r)
        {
            return new Rational(r.p - r.q, r.q);
        }
    }
}