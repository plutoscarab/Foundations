﻿
/*
PolyGF2.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundations.Types
{
    /// <summary>
    /// Polynomial over GF(2).
    /// </summary>
    public partial struct PolyGF2 : IEquatable<PolyGF2>
    {
        /// <summary>
        /// The constant value 0.
        /// </summary>
        public static readonly PolyGF2 Zero = new PolyGF2();

        /// <summary>
        /// The constant value 1.
        /// </summary>
        public static readonly PolyGF2 One = new PolyGF2(0);

        private static readonly int[] expZero = new int[0];

        private int[] exponentsInternal;
        private int[] exponents => exponentsInternal == null ? expZero : exponentsInternal;

        /// <summary>
        /// Gets the exponents of the terms of the polynomial.
        /// </summary>
        public IEnumerable<int> Exponents
        {
            get { foreach (var e in exponents) yield return e; }
        }

        /// <summary>
        /// Gets the degree of the polynomial.
        /// </summary>
        public int Degree => exponents.Length == 0 ? 0 : exponents[exponents.Length - 1];

        /// <summary>
        /// Gets the number of terms in the polynomial.
        /// </summary>
        public int TermCount => exponents.Length;

        /// <summary>
        /// Indicates whether the polynomial equals 0.
        /// </summary>
        public bool IsZero => exponents.Length == 0;

        /// <summary>
        /// Indicates whether the polynomial equals 1.
        /// </summary>
        public bool IsOne => exponents.Length == 1 && exponents[0] == 0;

        /// <summary>
        /// Creates a <see cref="PolyGF2"/> with terms indicated
        /// by the specified exponents.
        /// </summary>
        public PolyGF2(IEnumerable<int> exponents)
        {
            if (exponents == null) throw new ArgumentNullException();
            exponentsInternal = GetExponents(exponents.ToArray());
        }

        /// <summary>
        /// Creates a <see cref="PolyGF2"/> with terms indicated
        /// by the specified exponents.
        /// </summary>
        public PolyGF2(params int[] exponents)
        {
            if (exponents == null) throw new ArgumentNullException();
            exponentsInternal = GetExponents(exponents);
        }

        private static int[] GetExponents(int[] exponents)
        { 
            if (exponents.Length < 2)
            {
                return (int[])exponents.Clone();
            }

            var set = new HashSet<int>();

            foreach (var e in exponents)
            {
                if (e < 0)
                    throw new ArgumentOutOfRangeException("Negative exponents are not allowed.");

                if (set.Contains(e))
                    set.Remove(e);
                else
                    set.Add(e);
            }

            exponents = set.ToArray();
            Array.Sort(exponents);
            return exponents;
        }

        private PolyGF2(bool safe, int[] exponents)
        {
            exponentsInternal = exponents;
        }

        /// <summary>
        /// Creates a <see cref="PolyGF2"/> with terms corresponding
        /// bits set in the code value.
        /// </summary>
        public static PolyGF2 FromCode(ulong code)
        {
            var list = new List<int>();

            for (int i = 0; i < 64; i++)
            {
                if ((code & 1) != 0) list.Add(i);
                code >>= 1;
            }

            return new PolyGF2(list);
        }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override string ToString()
        {
            if (TermCount == 0) return "0";

            var s = new StringBuilder();

            foreach (var e in exponents)
            {
                if (s.Length > 0) s.Append(" + ");

                if (e == 0) s.Append("1");
                else s.Append("x" + Superscript(e));
            }

            return s.ToString();
        }

        internal static string Superscript(int n)
        {
            if (n == 1) return "";
            return new string(n.ToString().Select(c => "⁰¹²³⁴⁵⁶⁷⁸⁹"[c - '0']).ToArray());
        }

        /// <summary>
        /// Unary plus.
        /// </summary>
        public static PolyGF2 operator +(PolyGF2 p)
        {
            return p;
        }

        /// <summary>
        /// Negation operator.
        /// </summary>
        public static PolyGF2 operator -(PolyGF2 p)
        {
            return p;
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static PolyGF2 operator +(PolyGF2 p, PolyGF2 q)
        {
            if (p.IsZero) return q;
            if (q.IsZero) return p;
            int i = 0, j = 0, k = 0;

            while (i < p.exponents.Length && j < q.exponents.Length)
            {
                switch (p.exponents[i].CompareTo(q.exponents[j]))
                {
                    case -1:
                        k++;
                        i++;
                        break;

                    case +1:
                        k++;
                        j++;
                        break;

                    case 0:
                        i++;
                        j++;
                        break;
                }
            }

            var exp = new int[k + p.exponents.Length - i + q.exponents.Length - j];
            i = j = k = 0;

            while (i < p.exponents.Length && j < q.exponents.Length)
            {
                switch (p.exponents[i].CompareTo(q.exponents[j]))
                {
                    case -1:
                        exp[k++] = p.exponents[i++];
                        break;

                    case +1:
                        exp[k++] = q.exponents[j++];
                        break;

                    case 0:
                        i++;
                        j++;
                        break;
                }
            }

            Array.Copy(p.exponents, i, exp, k, p.exponents.Length - i);
            Array.Copy(q.exponents, j, exp, k, q.exponents.Length - j);
            return new PolyGF2(true, exp);
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static PolyGF2 operator -(PolyGF2 p, PolyGF2 q)
        {
            return p + q;
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static PolyGF2 operator *(PolyGF2 p, PolyGF2 q)
        {
            if (p.IsZero || q.IsZero) return Zero;
            if (p.IsOne) return q;
            if (q.IsOne) return p;
            if (p.TermCount == 1) return new PolyGF2(q.exponents.Select(e => e + p.Degree));
            if (q.TermCount == 1) return new PolyGF2(p.exponents.Select(e => e + q.Degree));
            var product = Zero;

            foreach (var e in p.exponents)
            {
                product += q * new PolyGF2(e);
            }

            return product;
        }

        private static IEnumerable<int> Increment(int[] exponents)
        {
            int min = exponents.Length;

            for (int i = 0; i < exponents.Length; i++)
            {
                if (exponents[i] > i) { min = i; break; }
            }

            yield return min;
            foreach (var e in exponents.Where(_ => _ > min)) yield return e;
        }

        /// <summary>
        /// Gets the next polynomial in code order.
        /// </summary>
        public static PolyGF2 operator ++(PolyGF2 p)
        {
            return new PolyGF2(Increment(p.exponents));
        }

        private static IEnumerable<int> Decrement(int[] exponents)
        {
            if (exponents.Length == 0)
                throw new OverflowException();

            foreach (var e in Enumerable.Range(0, exponents[0]))
                yield return e;

            foreach (var e in exponents.Skip(1))
                yield return e;
        }

        /// <summary>
        /// Gets the previous polynomial in code order.
        /// </summary>
        public static PolyGF2 operator --(PolyGF2 p)
        {
            return new PolyGF2(Decrement(p.exponents));
        }

        /// <summary>
        /// Enum specifying the ordering of polynomials returned
        /// from <see cref="GetAll(Order)"/>.
        /// </summary>
        public enum Order
        {
            /// <summary>
            /// Return the polynomials in code order.
            /// </summary>
            CodeOrder,

            /// <summary>
            /// Return the polynomials in an order where successive
            /// polynomials differ by a single term.
            /// </summary>
            GrayCodeOrder
        };

        /// <summary>
        /// Get all polynomials over GF(2) in code order.
        /// </summary>
        public static IEnumerable<PolyGF2> GetAll()
        {
            var p = Zero;

            while (true)
            {
                yield return p;
                p++;
            }
        }

        /// <summary>
        /// Gets all polynomials over GF(2) in Gray code order,
        /// in which successive polynomials differ by only one term.
        /// </summary>
        private static IEnumerable<PolyGF2> GetAllGrayCodeOrder()
        {
            var p = Zero;

            foreach (var r in Sequences.Ruler())
            {
                yield return p;
                p = new PolyGF2(p.exponents.Concat(new[] { r }));
            }
        }

        /// <summary>
        /// Get all polynomials over GF(2) in the specified order.
        /// </summary>
        public static IEnumerable<PolyGF2> GetAll(Order order)
        {
            switch (order)
            {
                case Order.CodeOrder: return GetAll();
                case Order.GrayCodeOrder: return GetAllGrayCodeOrder();
                default: throw new ArgumentException();
            }
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static PolyGF2 operator /(PolyGF2 p, PolyGF2 q)
        {
            PolyGF2 r;
            return DivRem(p, q, out r);
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static PolyGF2 operator %(PolyGF2 p, PolyGF2 q)
        {
            PolyGF2 r;
            DivRem(p, q, out r);
            return r;
        }

        /// <summary>
        /// Gets the quotient and remainder of a polynomial division.
        /// </summary>
        public static PolyGF2 DivRem(PolyGF2 p, PolyGF2 q, out PolyGF2 rem)
        {
            if (q.IsZero) throw new DivideByZeroException();

            if (q.IsOne)
            {
                rem = Zero;
                return p;
            }

            if (p.Degree < q.Degree)
            {
                rem = p;
                return Zero;
            }

            if (q.TermCount == 1)
            {
                if (p.exponents[0] >= q.exponents[0])
                {
                    rem = Zero;
                    var exp = (int[])p.exponents.Clone();
                    for (int i = 0; i < exp.Length; i++) exp[i] -= q.exponents[0];
                    return new PolyGF2(true, exp);
                }

                rem = new PolyGF2(p.exponents.Where(_ => _ < q.exponents[0]));
                return new PolyGF2(p.exponents.Select(_ => _ - q.exponents[0]).Where(_ => _ >= 0));
            }

            int shift = p.Degree - q.Degree;
            q *= new PolyGF2(shift);
            var quotient = new List<int>(shift);
            
            while (true)
            {
                p -= q;
                quotient.Add(shift);
                shift -= q.Degree - p.Degree;
                if (shift < 0) break;
                var pshift = q.Degree - p.Degree;

                for (int i = 0; i < q.exponents.Length; i++)
                    q.exponents[i] -= pshift;
            }

            rem = p;
            quotient.Reverse();
            return new PolyGF2(quotient);
        }

        /// <summary>
        /// Implementation of <see cref="IEquatable{PolyGF2}"/>.
        /// </summary>
        public bool Equals(PolyGF2 other)
        {
            if (other.TermCount != TermCount) return false;

            for (int i = 0; i < TermCount; i++)
                if (exponents[i] != other.exponents[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is PolyGF2 && Equals((PolyGF2)obj);
        }

        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashHelper.Finisher(HashHelper.Mixer(-721784075, exponents));
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(PolyGF2 p, PolyGF2 q)
        {
            return p.Equals(q);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(PolyGF2 p, PolyGF2 q)
        {
            return !p.Equals(q);
        }

        /// <summary>
        /// Gets all irreducible polynomials over GF(2).
        /// </summary>
        public static IEnumerable<PolyGF2> GetAllIrreducible()
        {
            PolyGF2 p = Zero;

            foreach (var s in SmallIrreducibles())
            {
                yield return s;
                p = s;
            }

            while (true)
            {
                p++;
                p++;
                if (p.IsIrreducible()) yield return p;
            }
        }

        /// <summary>
        /// Determines if this polynomial is irreducible (has no non-constant factors).
        /// </summary>
        public bool IsIrreducible()
        {
            PolyGF2 factor;
            return IsIrreducible(out factor);
        }

        /// <summary>
        /// Determines if this polynomial is irreducible (has no non-constant factors).
        /// </summary>
        /// <param name="factor">One of the factors, if the polynomial is reducible.</param>
        public bool IsIrreducible(out PolyGF2 factor)
        {
            factor = Zero;

            foreach (var d in GetAllIrreducible())
            {
                if (this == d)
                    return true;

                if (this % d == Zero)
                {
                    factor = d;
                    break;
                }

                if (d.Degree * 2 > Degree)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets all primitive polynomials over GF(2).
        /// </summary>
        public static IEnumerable<PolyGF2> GetAllPrimitive()
        {
            int d = -1;
            var cofactors = new List<PolyGF2>();

            foreach (var p in GetAllIrreducible().Skip(1))
            {
                if (p.Degree > d)
                {
                    d = p.Degree;
                    if (d < 1) continue;
                    long m = (1L << d) - 1L;
                    cofactors.Clear();

                    foreach (var factor in Sequences.PrimeFactors(m).Select(_ => _.Value))
                    {
                        if (factor == m) break;
                        var cofactor = m / factor;
                        if (cofactor < d) continue;
                        cofactors.Add(new PolyGF2((int)cofactor, 0));
                    }
                }

                if (!cofactors.Any(cofactor => cofactor % p == Zero))
                    yield return p;
            }
        }

        /// <summary>
        /// Determines if this polynomial is primitive (irreducible, and not a divisor of
        /// xᵐ+1 for any m &lt; 2ᵈ-1 where d is the degree of the polynomial).
        /// </summary>
        public bool IsPrimitive()
        {
            if (!IsIrreducible()) return false;
            return IsPrimitiveInternal();
        }

        /// <summary>
        /// Determines if this polynomial is primitive (irreducible, and not a divisor of
        /// xᵐ+1 for any m &lt; 2ᵈ-1 where d is the degree of the polynomial).
        /// </summary>
        public bool IsPrimitive(out PolyGF2 factor, out long order)
        {
            order = 0;
            if (!IsIrreducible(out factor)) return false;
            return IsPrimitiveInternal(out order);
        }

        private bool IsPrimitiveInternal()
        {
            long order;
            return IsPrimitiveInternal(out order);
        }

        private bool IsPrimitiveInternal(out long order)
        {
            order = 0;
            if (IsZero) return false;
            if (exponents[0] != 0) return false;
            var d = Degree;
            if (d < 1) return false;
            long m = (1L << d) - 1L;

            foreach (var factor in Sequences.PrimeFactors(m).Select(_ => _.Value))
            {
                if (factor == m) return true;
                var cofactor = m / factor;
                if (cofactor < d) continue;
                order = cofactor;
                if (new PolyGF2((int)cofactor, 0) % this == Zero) return false;
            }

            return true;
        }

        /// <summary>
        /// Gets a value in which the set bits indicate which exponents
        /// have terms present in the polynomial.
        /// </summary>
        public ulong GetCode()
        {
            if (Degree > 63) throw new OverflowException();
            var u = 0UL;
            foreach (var e in exponents) u |= 1UL << e;
            return u;
        }

        private static readonly byte[] smallSkips = new byte[]
        {
            1,1,0,2,2,2,2,1,2,3,1,0,2,2,6,1,2,2,2,2,0,6,2,2,0,5,4,1,6,2,0,4,3,0,7,4,0,2,2,14,0,6,0,5,2,6,8,
            1,0,1,3,2,1,5,1,0,8,1,2,3,5,2,5,3,2,4,5,0,1,4,6,2,1,2,5,2,11,6,2,2,1,2,3,2,4,6,1,4,0,4,3,2,8,0,4,12,
            1,3,8,0,4,1,6,4,6,2,5,2,4,0,2,5,2,4,8,9,1,6,1,0,3,1,4,2,8,6,2,5,0,4,2,3,8,5,7,0,4,8,4,6,7,2,10,1,6,
            7,2,2,5,5,0,5,2,1,6,5,2,9,4,2,1,12,4,3,1,2,4,6,3,2,14,8,1,11,0,6,0,5,2,5,5,0,4,14,2,2,1,14,2,3,4,0,
            5,2,5,11,2,8,0,2,2,1,4,4,1,6,2,3,1,4,11,0,1,2,2,2,10,3,9,10,7,3,2,2,2,8,9,0,12,13,0,5,4,8,3,4,4,3,8,
            5,0,7,2,1,4,11,2,8,1,6,1,0,3,1,3,4,11,0,4,1,5,13,0,5,6,8,5,4,3,4,2,5,0,9,15,11,5,5,2,2,5,5,4,1,3,0,
            15,4,1,2,2,0,4,3,2,3,16,5,0,4,6,2,2,4,6,4,1,3,0,12,3,2,5,8,0,4,2,14,0,5,2,8,18,4,4,3,0,7,1,6,1,0,10,
            5,3,2,2,0,6,2,6,5,5,6,4,2,9,2,6,1,4,5,0,16,5,4,8,0,5,6,1,2,2,6,4,2,11,1,2,1,3,2,6,1,4,4,8,4,0,1,8,5,
            1,11,6,2,6,10,2,8,6,11,2,18,6,0,3,1,4,6,2,1,5,2,3,11,2,8,1,8,6,6,4,8,0,4,6,2,10,6,1,0,1,4,6,4,0,20,
            0,12,0,8,3,11,1,6,2,1,9,3,4,4,0,5,4,0,5,12,3,18,2,1,5,0,7,5,2,2,5,8,1,0,2,0,16,2,2,5,26,1,3,4,30,2,
            5,0,1,8,2,2,6,11,5,19,2,1,2,2,5,3,6,2,2,10,0,8,5,14,2,8,2,5,0,4,1,4,4,4,11,2,2,3,11,2,2,2,2,20,6,22,
            0,25,8,8,1,8,4,8,2,6,6,4,2,8,17,0,2,1,4,1,4,0,4,17,8,11,2,2,3,7,2,5,5,5,4,1,17,2,0,4,3,2,10,0,1,7,2,
            3,2,7,2,5,0,14,13,5,2,8,1,6,2,8,6,2,7,5,2,1,6,2,4,11,8,0,16,0,4,4,1,8,8,3,5,4,9,2,4,5,2,4,6,11,2,2,
            5,2,6,4,2,5,16,0,1,4,3,12,2,0,4,1,2,8,7,18,9,5,7,2,1,4,10,0,2,2,2,7,10,8,24,2,0,10,6,1,2,0,4,3,7,12,
            2,0,4,0,2,7,18,8,19,11,2,8,1,4,1,2,3,9,0,2,6,2,13,5,6,2,6,4,0,4,6,1,4,0,1,3,10,5,6,22,11,6,1,10,5,0,
            3,5,5,1,2,5,13,3,1,14,2,6,2,7,6,2,5,6,4,5,8,6,1,3,8,5,5,1,5,5,10,5,0,2,0,1,11,5,8,13,5,6,14,8,4,13,
            0,8,2,4,5,0,2,2,11,8,12,3,9,2,5,3,17,1,6,7,6,2,16,0,2,2,8,1,6,4,14,5,8,2,2,11,0,13,2,4,6,1,11,5,6,
            16,3,1,4,16,1,13,6,1,10,0,5,4,2,3,6,2,3,2,1,18,5,5,8,3,5,2,1,9,9,2,4,5,8,1,11,0,11,4,0,2,1,11,18,6,
            2,1,5,5,12,0,6,6,2,7,1,4,1,2,7,6,3,4,3,9,11,0,5,13,8,5,3,2,7,6,1,5,5,2,11,2,2,2,19,4,14,10,8,9,3,2,
            6,3,2,4,6,7,11,6,1,9,7,0,10,8,2,1,6,4,0,3,16,6,7,18,0,8,6,3,4,1,2,8,6,2,0,4,7,4,4,0,4,4,16,2,1,8,5,
            7,3,2,2,9,13,0,6,5,6,1,8,0,11,13,2,0,4,0,19,1,3,9,8,1,7,3,6,9,6,0,4,3,5,2,17,8,2,8,1,8,2,18,5,11,8,
            8,4,0,8,0,5,17,12,0,3,6,5,6,3,5,2,4,2,1,16,5,7,2,3,1,4,10,5,6,9,2,11,1,4,22,6,5,4,5,0,9,15,2,5,0,1,
            4,17,0,2,2,6,3,2,18,13,0,3,1,3,2,19,0,4,0,5,2,2,1,21,7,4,0,14,6,4,2,3,4,3,6,1,10,12,2,11,8,2,8,7,9,
            4,0,8,2,5,4,10,4,18,8,2,8,1,6,4,19,2,6,6,1,8,2,4,0,2,5,4,2,6,4,8,8,3,2,5,13,8,2,2,4,6,1,3,9,1,5,15,
            6,12,16,5,11,16,1,7,6,3,2,6,6,0,8,1,6,8,5,12,5,8,4,6,1,5,5,2,5,11,5,8,5,8,9,8,2,5,2,6,4,6,7,2,5,14,
            1,11,9,3,1,4,2,2,8,4,9,2,4,5,6,3,4,11,0,13,4,0,7,14,3,0,6,5,10,4,5,2,5,5,3,2,5,14,2,9,4,1,4,16,4,0,
            2,2,1,8,7,0,8,2,4,5,11,6,7,3,24,6,4,6,2,8,8,2,3,2,1,3,14,14,11,2,10,0,8,4,2,2,14,0,2,13,3,2,4,16,2,
            6,9,5,1,2,5,0,9,23,2,6,2,0,4,2,1,8,10,12,0,3,4,9,2,4,0,4,0,2,9,6,5,1,6,1,10,10,5,5,6,2,0,4,1,0,10,9,
            6,7,10,5,6,4,1,6,6,16,8,0,2,9,1,2,5,2,0,5,5,11,1,9,1,9,13,2,5,6,4,5,1,6,5,1,2,5,7,9,0,8,8,1,16,11,
            12,3,8,2,1,11,9,5,2,2,8,2,2,0,1,3,2,4,16,2,3,12,4,22,9,2,17,4,3,2,2,9,5,2,10,5,16,2,5,0,8,3,9,3,2,8,
            0,2,19,3,2,6,7,2,17,4,0,4,1,0,3,15,1,11,7,5,27,1,6,4,8,2,2,1,0,11,1,18,1,0,10,2,2,9,2,8,2,2,11,20,8,
            0,4,6,12,12,9,6,0,8,5,0,26,12,10,10,27,8,0,4,1,6,4,3,2,2,13,6,15,10,3,2,7,5,2,2,8,6,1,3,2,7,5,5,7,6,
            4,6,8,8,4,6,4,8,2,0,6,0,15,4,5,7,9,18,5,8,5,23,4,3,4,5,6,5,8,2,5,1,2,6,10,2,2,13,3,6,4,2,2,11,1,6,
            22,3,13,6,2,2,15,1,5,5,3,8,5,2,1,6,7,1,14,18,0,5,1,2,23,14,17,5,10,2,2,1,3,7,5,6,5,9,7,5,18,11,8,8,
            2,4,0,2,2,5,4,3,4,4,3,2,8,0,3,4,8,4,6,8,0,4,1,9,3,1,3,0,7,11,7,11,11,3,5,2,10,8,10,4,4,3,9,4,0,17,7,
            4,14,2,6,8,14,4,2,9,6,9,5,2,3,12,0,10,11,11,15,8,1,2,22,1,20,4,4,4,2,8,5,8,2,1,6,1,13,9,0,5,6,11,2,
            26,0,33,20,4,1,4,2,2,5,15,2,5,8,9,2,2,7,6,6,6,5,5,2,11,0,4,7,0,3,4,2,4,14,27,5,12,6,4,6,5,2,0,4,0,
            10,1,6,3,11,5,1,6,8,8,5,6,18,0,4,8,2,3,5,8,5,5,2,5,3,14,1,6,8,2,13,8,2,0,4,8,2,1,16,1,4,19,2,10,3,
            23,1,4,1,2,1,0,5,2,10,9,8,5,4,6,7,2,3,6,2,2,0,18,2,30,7,0,2,6,2,2,17,2,1,23,0,8,5,4,1,4,0,11,10,13,
            6,2,0,8,7,5,7,6,7,5,2,0,4,0,29,4,1,4,1,4,5,0,15,5,4,1,6,2,9,7,11,9,23,7,3,2,1,2,1,4,10,9,4,9,7,2,5,
            2,10,0,2,0,1,21,12,11,6,5,8,0,1,14,11,3,2,6,3,2,3,16,2,6,16,4,2,12,0,13,4,6,6,1,6,11,2,8,9,7,5,4,14,
            14,0,4,0,11,15,5,5,10,0,8,19,11,2,3,4,1,2,8,6,2,5,2,16,5,5,8,12,1,11,12,1,11,5,1,22,1,5,6,8,2,5,2,1,
            12,2,0,5,7,4,4,9,6,9,16,8,1,0,5,12,0,3,12,6,8,0,7,2,1,14,6,2,1,4,3,2,12,0,16,8,3,13,5,3,8,14,5,16,2,
            4,5,15,8,3,4,21,3,0,1,6,2,10,5,6,7,8,2,1,3,2,15,1,0,6,0,2,5,17,6,2,2,14,8,3,7,1,3,7,14,23,6,5,6,10,
            9,2,2,3,26,4,4,4,3,26,6,5,4,6,14,1,3,0,1,8,19,6,10,2,5,2,9,0,1,5,4,0,2,4,2,2,10,11,9,6,6,0,2,7,5,3,
            7,1,4,1,15,2,4,19,4,2,8,11,9,4,6,4,16,5,3,12,10,1,4,6,6,4,0,6,10,8,13,3,9,0,2,5,2,1,13,1,26,3,8,10,
            6,12,9,3,0,1,5,4,5,0,1,4,13,2,13,11,2,1,6,4,5,16,4,5,1,0,3,6,9,13,0,4,0,4,0,13,2,5,0,2,9,9,3,2,6,1,
            4,9,9,1,5,3,4,1,0,5,2,6,8,4,5,2,2,8,6,10,6,3,8,4,8,32,1,2,0,2,1,12,5,9,1,2,3,9,19,7,0,1,8,19,6,4,0,
            2,0,11,21,14,2,2,6,5,2,13,5,2,2,6,12,3,5,11,0,13,3,9,9,3,4,9,3,1,2,17,5,2,9,4,8,8,2,4,13,11,6,3,0,
            10,24,1,5,13,6,1,6,2,6,1,24,0,1,4,6,2,2,15,6,14,6,1,2,9,4,5,9,2,8,1,14,2,7,0,4,9,8,2,19,14,2,6,5,2,
            5,2,5,2,5,2,14,1,10,9,7,4,14,2,8,11,5,3,10,5,0,4,9,0,9,18,3,1,7,0,10,19,3,9,2,3,4,4,8,0,12,1,2,9,5,
            4,21,1,2,11,4,12,8,0,12,4,1,2,14,13,6,5,0,4,1,0,1,14,6,4,1,5,5,4,5,12,3,2,8,17,12,3,2,10,2,7,2,14,2,
            3,8,5,0,12,18,7,5,5,8,0,4,8,28,2,2,5,5,0,5,2,4,2,9,5,3,7,5,12,5,10,0,4,2,2,14,5,9,1,6,2,18,11,5,1,4,
            4,5,4,16,4,5,13,6,1,3,14,8,0,10,11,11,4,4,0,5,4,8,5,4,8,4,6,12,7,6,2,6,1,14,2,21,7,9,6,3,0,6,9,5,6,
            5,6,6,2,0,9,16,6,1,20,2,10,0,2,7,5,9,2,3,9,8,0,1,5,5,10,10,13,9,4,4,2,5,0,6,9,3,5,7,0,17,11,14,6,3,
            13,12,0,5,2,1,6,8,0,5,2,4,2,13,6,2,10,3,20,7,3,4,2,5,8,5,5,2,5,6,4,6,4,0,9,7,24,2,1,5,5,5,13,2,8,8,
            0,3,4,14,10,19,4,28,4,2,5,23,6,4,6,18,1,7,3,9,2,8,1,2,3,12,4,1,10,5,0,3,5,17,17,4,5,0,2,8,14,2,5,4,
            0,8,4,9,20,7,3,1,0,15,1,2,5,9,1,9,8,4,2,5,10,6,2,6,1,8,8,2,10,3,5,6,20,1,5,11,1,8,10,2,3,5,11,11,5,
            5,0,3,4,4,3,28,12,1,2,6,6,0,2,5,15,4,3,2,4,2,14,8,1,6,9,1,8,2,4,5,0,16,0,8,8,20,0,1,3,2,7,30,11,11,
            1,10,15,0,4,0,7,8,11,16,13,6,0,5,4,5,1,9,7,0,21,22,0,22,0,2,20,2,14,5,17,4,4,1,5,5,23,2,15,6,1,4,2,
            14,5,7,2,8,6,11,4,10,2,1,12,4,9,6,4,8,8,2,1,8,2,14,5,0,11,1,2,3,9,9,13,10,2,13,0,12,3,1,2,14,0,8,5,
            2,10,9,11,1,6,3,8,0,7,8,2,19,8,2,5,3,7,2,17,6,2,2,8,2,2,9,4,5,3,9,3,4,0,7,26,16,1,2,3,1,0,17,5,20,1,
            14,4,2,3,5,8,8,16,6,4,2,2,5,5,6,2,5,13,5,6,6,9,8,9,11,1,12,14,2,6,1,6,13,5,1,12,1,9,10,9,7,0,4,9,13,
            0,3,10,3,17,2,6,3,2,5,1,8,4,8,1,2,2,6,4,3,6,9,3,5,3,13,4,14,8,8,15,8,1,2,20,0,12,0,2,2,2,12,4,8,14,
            10,9,4,3,5,7,5,4,18,4,1,9,10,24,4,1,0,1,3,20,0,1,8,5,4,11,5,12,38,1,10,5,3,0,8,2,4,5,2,8,6,5,1,2,3,
            5,0,10,8,8,4,3,4,14,2,0,4,4,6,0,31,5,14,3,1,0,1,5,5,2,2,8,2,6,6,6,1,11,11,10,4,8,2,6,8,5,7,3,2,4,1,
            3,17,2,9,12,6,2,10,4,7,5,0,9,5,4,5,0,1,5,28,3,6,7,1,3,2,5,40,1,4,9,4,4,6,2,2,11,34,6,2,4,2,8,6,6,1,
            2,1,2,11,14,1,36,2,11,3,2,4,5,12,2,11,25,8,2,0,11,5,14,9,4,6,0,4,9,11,0,4,1,0,8,4,3,10,6,11,8,4,8,3,
            5,6,1,2,5,0,1,6,8,10,5,2,14,8,9,4,8,22,6,7,3,5,2,6,6,5,20,7,8,0,3,1,6,1,8,12,1,0,5,2,1,2,13,18,3,8,
            4,3,4,8,6,5,3,22,0,1,3,32,2,5,13,2,1,4,1,5,4,0,5,2,11,7,0,9,16,0,1,18,17,7,17,8,6,2,13,3,4,11,8,4,3,
            2,9,7,8,14,4,0,20,20,5,14,7,6,18,8,13,5,11,4,0,8,0,5,4,14,18,3,2,8,0,3,9,2,6,1,8,5,1,2,6,11,6,1,4,
            15,3,2,5,17,21,13,10,2,8,0,19,2,3,10,3,12,0,8,0,4,0,7,11,17,6,0,2,2,12,5,8,8,5,6,11,1,6,13,12,1,4,6,
            16,4,5,2,1,5,5,6,2,2,2,10,6,1,4,10,8,2,5,2,4,3,23,6,4,4,9,3,5,4,2,20,0,16,25,5,12,9,4,1,6,7,9,7,5,2,
            6,8,1,6,2,1,8,3,6,11,13,2,4,6,5,8,5,4,2,5,3,2,14,6,12,6,6,10,8,7,9,2,8,5,8,3,4,10,16,10,2,13,9,0,5,
            7,8,9,1,0,23,12,2,11,12,3,2,1,18,11,2,18,3,0,22,2,2,18,4,4,8,12,5,14,4,2,3,6,8,7,29,11,2,8,8,1,3,2,
            6,8,1,0,13,3,5,5,2,4,12,0,8,10,3,4,19,11,12,10,3,8,9,10,5,3,5,0,9,6,1,12,9,8,3,9,4,2,1,6,5,1,6,1,2,
            2,2,2,0,10,6,16,2,4,6,10,0,15,10,1,13,11,2,15,8,1,7,6,4,6,2,2,1,8,11,24,16,18,4,8,9,7,1,9,1,5,8,9,0,
            18,3,2,9,4,1,13,6,1,5,8,10,16,5,3,2,3,7,5,2,0,7,4,0,12,6,15,7,2,8,2,2,2,17,15,13,5,0,2,6,1,4,12,2,2,
            2,0,4,5,6,13,17,4,5,0,11,1,15,20,2,20,0,1,8,7,0,8,4,12,6,5,17,8,8,4,3,6,16,11,1,2,23,3,8,2,5,5,10,6,
            6,2,6,15,1,2,4,9,1,6,12,20,1,11,18,4,2,5,0,14,8,13,4,11,9,5,11,8,3,4,0,2,5,5,2,17,20,2,5,5,2,2,4,8,
            8,4,6,11,10,7,8,5,5,9,7,3,1,0,10,8,11,2,2,8,5,2,11,1,6,10,15,2,0,9,23,11,14,0,5,11,5,5,5,5,4,6,12,1,
            2,8,7,11,0,6,0,8,3,13,5,6,3,13,8,9,1,9,7,6,10,5,2,8,6,8,2,6,6,2,5,4,12,0,11,8,12,1,7,8,2,8,0,4,5,9,
            4,2,6,24,0,5,3,5,12,4,1,10,3,9,3,2,3,2,2,3,6,2,8,3,2,27,8,2,4,9,16,2,4,12,9,1,12,6,11,12,5,5,8,2,1,
            21,19,5,21,1,2,0,5,3,4,1,2,3,2,1,15,0,1,14,1,5,13,13,1,0,5,37,1,2,9,2,14,19,14,8,12,4,5,0,5,12,1,0,
            6,9,10,9,3,4,5,26,12,7,6,8,6,6,12,5,8,8,8,1,0,10,2,2,2,2,6,11,14,1,0,9,12,3,16,6,5,11,11,1,9,6,4,3,
            4,4,0,5,2,1,6,10,6,1,21,4,0,22,2,10,3,0,1,3,2,13,5,6,5,2,7,22,1,4,3,2,10,5,0,1,4,13,6,9,6,6,1,26,3,
            2,17,2,7,1,0,3,1,4,6,9,4,0,2,4,11,8,12,1,2,6,9,4,8,22,14,2,0,9,1,10,6,4,6,4,1,2,6,15,9,0,10,5,8,9,
            10,27,9,7,12,4,6,2,4,6,1,2,8,2,4,6,11,3,5,4,2,19,9,8,16,1,4,21,2,6,3,5,0,8,8,1,3,14,5,5,1,13,6,1,4,
            12,10,17,5,14,2,5,0,7,16,1,15,9,1,5,6,4,16,6,6,8,4,5,2,16,6,0,5,26,2,8,22,2,0,4,12,14,1,8,2,26,6,10,
            13,2,5,5,1,4,5,2,5,2,5,17,8,11,6,2,2,8,2,5,12,6,2,1,2,3,8,9,13,2,5,8,7,5,0,4,12,17,6,3,2,4,6,8,12,7,
            12,11,2,1,8,4,4,8,16,6,7,18,2,3,4,1,2,2,5,0,9,12,4,1,3,22,0,16,5,7,16,2,1,12,17,7,3,9,8,7,5,0,19,7,
            9,9,7,8,1,5,19,6,5,2,0,13,3,6,0,1,11,5,14,0,2,23,0,8,2,1,8,18,4,2,2,0,25,10,6,8,7,11,5,0,16,0,8,1,9,
            13,0,7,8,4,3,2,6,8,1,0,7,8,0,4,12,5,11,2,0,5,4,1,9,25,15,2,10,11,1,6,4,6,9,4,11,8,5,4,5,0,1,4,0,8,0,
            9,28,0,4,12,1,15,4,0,8,10,2,5,2,4,3,5,11,11,5,15,8,11,8,7,0,2,2,5,5,2,2,1,4,15,0,8,8,5,2,0,8,1,8,2,
            38,9,7,4,27,14,13,11,2,3,5,10,14,25,0,12,2,6,4,1,3,11,7,33,3,6,5,11,1,0,3,10,3,0,7,5,2,4,22,9,5,0,4,
            8,5,7,3,10,11,0,4,0,2,19,0,2,5,8,2,1,4,17,2,1,2,3,13,20,6,4,3,10,13,8,8,14,6,11,5,1,5,22,3,2,6,10,8,
            3,1,6,4,2,8,4,8,2,4,1,3,0,16,1,8,2,12,2,0,6,18,3,7,9,10,9,2,1,9,10,2,10,3,2,5,2,23,3,6,6,9,8,9,1,4,
            21,4,4,3,9,2,5,2,7,0,5,1,4,0,8,4,37,6,2,9,7,5,6,1,6,4,6,2,11,8,5,2,2,9,20,14,4,8,4,18,2,1,3,0,1,6,2,
            8,4,2,10,11,19,1,6,9,9,3,6,4,8,2,12,4,12,5,2,1,3,7,6,13,2,24,13,2,5,0,4,11,2,20,2,3,1,10,5,5,11,2,
            26,0,13,2,0,4,4,12,3,5,32,1,3,2,13,21,2,15,4,3,4,11,12,0,6,0,1,8,7,6,4,6,5,2,5,17,0,1,9,5,5,8,1,10,
            7,5,17,5,4,6,4,0,2,28,9,4,1,11,4,4,6,6,3,7,0,22,0,17,10,5,18,5,8,6,2,10,2,11,1,4,6,6,2,5,2,8,17,1,2,
            2,6,4,2,5,2,5,17,0,2,13,24,10,2,5,2,0,8,6,6,11,0,8,0,16,2,5,10,11,2,6,13,2,6,4,2,5,7,9,8,16,4,1,2,1,
            9,1,2,15,4,6,2,2,15,4,8,2,6,11,3,4,4,3,7,5,10,2,6,8,2,5,2,20,0,8,20,8,5,6,6,20,18,13,15,3,2,6,0,5,6,
            5,14,12,4,1,4,5,0,8,12,1,4,12,19,2,0,5,5,5,2,14,4,5,2,11,5,5,16,9,0,2,5,7,2,6,11,3,2,1,8,5,7,0,16,9,
            8,7,2,6,22,0,5,2,3,5,28,23,12,14,25,2,5,0,8,6,10,0,12,3,4,5,8,3,2,3,9,6,14,1,5,4,21,0,6,0,3,7,5,7,8,
            12,2,8,8,1,13,2,11,1,8,0,13,2,0,10,2,8,1,11,9,6,7,5,8,12,1,22,4,2,2,10,2,3,10,2,3,4,0,29,5,20,2,6,
            13,0,1,14,19,9,5,0,5,6,4,1,35,4,3,5,13,0,4,1,11,5,9,2,21,4,53,0,5,2,1,5,3,43,0,4,5,1,0,3,18,18,11,
            14,2,6,9,10,4,1,2,3,4,30,15,14,2,6,12,4,9,0,13,2,2,2,7,6,17,1,18,1,6,5,1,14,13,4,4,6,1,14,0,8,0,29,
            5,25,2,13,12,2,8,4,3,2,8,6,1,4,10,19,3,4,8,3,6,5,6,15,6,3,12,5,2,17,2,28,5,0,5,7,3,14,7,0,30,16,22,
            5,2,5,2,2,1,2,15,1,24,2,0,1,8,2,35,6,7,1,4,1,11,5,8,5,5,1,15,12,11,2,5,6,16,1,0,8,1,2,2,6,51,4,8,6,
            2,8,7,44,8,0,6,0,2,2,35,2,5,0,6,9,4,4,3,2,5,4,6,3,8,1,2,5,2,5,29,0,9,2,4,4,4,1,17,1,30,11,8,8,11,0,
            10,0,5,2,19,5,13,0,11,3,13,5,6,5,61,0,7,2,1,20,1,2,1,11,17,7,3,2,2,0,1,3,17,6,11,7,20,1,14,2,8,14,2,
            5,0,2,2,2,8,23,1,4,2,22,9,4,26,3,16,6,5,11,2,2,10,3,2,1,9,5,14,2,5,11,0,5,2,17,7,0,3,1,28,11,0,11,
            10,0,4,2,10,6,8,6,2,2,6,3,2,7,8,17,17,0,17,9,4,1,13,1,4,1,5,11,6,0,2,13,5,0,2,9,4,14,3,5,7,2,13,4,3,
            2,1,14,2,11,1,9,10,0,5,7,2,0,18,1,8,8,5,3,2,2,2,1,6,4,2,32,17,8,16,14,15,8,4,14,2,4,9,2,0,4,6,4,1,3,
            15,10,2,5,0,12,7,2,2,0,6,11,0,13,2,0,37,8,2,11,9,16,13,5,7,4,1,6,13,16,1,8,18,8,7,2,8,5,6,9,3,9,4,8,
            2,8,14,7,6,5,8,2,2,4,8,7,3,0,7,4,0,14,6,4,8,13,4,2,1,13,8,10,10,9,7,8,20,2,12,5,14,5,2,5,3,28,5,2,
            11,6,5,10,11,10,6,2,8,13,2,1,8,2,5,10,17,6,11,8,7,6,6,12,9,4,8,3,4,1,11,8,9,19,8,10,12,3,2,15,4,6,
            10,14,15,8,10,2,19,0,7,9,2,20,4,17,1,2,2,10,23,5,4,6,10,5,3,0,1,20,5,1,2,9,7,5,6,6,13,2,22,1,3,4,33,
            2,11,4,18,3,1,4,2,3,9,1,14,5,0,14,10,2,5,0,10,2,10,6,9,4,0,13,6,1,9,13,0,2,5,6,1,4,3,2,7,17,5,9,2,7,
            9,1,6,14,2,3,5,16,0,5,2,4,4,0,8,4,24,2,22,3,4,8,0,8,1,0,8,7,0,13,11,1,13,1,16,6,2,0,2,5,20,2,11,5,
            11,4,6,1,5,6,4,5,3,5,18,8,7,17,6,5,8,1,0,1,8,13,3,5,11,0,7,3,1,6,2,11,1,25,1,8,25,1,16,5,4,11,25,3,
            2,5,6,1,5,6,13,5,6,0,3,10,6,10,0,9,10,7,6,6,23,19,4,13,5,7,4,3,11,4,1,5,14,6,22,4,8,5,11,6,23,3,11,
            1,8,9,1,6,11,5,16,1,6,11,6,13,13,5,0,11,4,23,0,13,5,7,6,5,6,2,10,12,1,6,4,15,5,0,17,1,14,9,6,9,3,6,
            14,5,8,11,8,2,5,0,7,16,8,18,1,0,8,13,10,2,4,9,4,0,5,2,5,2,4,3,4,11,4,9,2,5,16,9,4,8,0,7,2,12,3,6,0,
            1,6,14,4,7,5,0,8,17,2,12,10,8,0,3,10,8,2,7,2,9,22,3,2,4,5,0,9,13,2,15,1,14,2,17,14,20,11,2,2,3,5,7,
            6,5,2,2,14,1,0,10,6,16,0,5,5,5,10,5,7,6,2,8,4,3,0,5,2,16,5,1,2,20,12,5,4,8,3,2,3,5,17,31,4,9,2,1,6,
            6,10,12,17,45,1,5,10,0,7,15,2,7,9,5,2,26,5,3,2,4,23,2,7,2,5,0,17,2,5,5,1,0,5,28,4,13,4,0,1,2,14,1,6,
            1,9,8,5,5,6,1,11,15,2,26,1,17,11,23,3,1,9,27,2,17,2,1,8,3,4,1,11,4,5,7,11,8,9,4,6,5,23,10,2,5,0,11,
            1,21,8,2,18,2,6,6,1,9,10,13,0,7,20,4,4,4,3,5,5,6,4,5,14,9,14,10,0,1,10,3,5,2,16,6,7,3,5,4,6,4,0,2,8,
            19,8,10,12,9,1,12,4,5,4,1,10,0,8,12,4,16,2,39,2,10,11,0,6,5,27,2,3,7,1,3,10,2,3,4,5,9,4,6,1,3,5,8,
            21,10,12,4,6,16,1,6,2,5,0,10,2,2,6,5,2,35,5,7,11,5,8,6,1,21,2,8,14,5,3,5,2,5,4,3,4,7,0,17,15,10,1,3,
            17,25,17,3,4,2,13,5,0,5,14,7,4,4,9,8,5,9,1,2,3,1,2,4,0,16,8,18,2,10,11,0,2,5,8,2,1,22,19,14,2,8,1,6,
            6,9,9,3,14,17,9,7,3,6,2,11,2,5,0,11,2,5,21,9,7,10,11,1,4,14,6,13,2,2,3,6,16,1,3,2,9,4,29,5,3,19,2,
            11,3,5,4,2,5,3,11,2,5,8,18,1,17,1,0,5,2,14,1,8,0,3,4,35,2,14,35,13,0,2,11,5,8,3,12,3,4,4,0,12,5,0,
            24,2,3,4,4,2,10,4,0,17,5,5,13,19,4,5,13,12,6,13,5,2,22,14,11,9,4,7,11,2,11,8,15,2,2,4,2,5,0,22,4,1,
            8,0,10,2,11,5,2,5,1,2,6,11,3,2,6,9,4,2,0,23,4,4,12,0,5,13,5,8,15,1,5,2,2,12,3,18,4,4,3,4,11,24,0,12,
            11,4,13,19,2,11,26,4,3,2,4,0,17,3,5,6,10,2,2,3,4,8,2,1,9,13,9,8,12,13,8,2,8,5,1,4,3,2,5,1,6,15,7,5,
            7,19,0,9,4,4,7,34,9,0,2,6,27,3,6,12,9,5,10,2,7,6,4,4,6,9,3,6,0,8,19,1,6,21,22,6,14,1,0,2,11,2,2,6,4,
            22,3,8,5,0,12,6,1,8,14,4,4,1,2,6,1,4,3,5,7,3,2,6,3,16,1,7,9,20,2,2,0,11,2,8,1,8,2,4,11,6,29,3,8,2,8,
            1,11,3,0,5,5,28,1,7,6,23,6,5,5,0,4,7,4,1,2,3,1,12,8,4,5,2,8,10,10,4,5,5,7,0,8,7,8,8,4,1,7,3,7,19,8,
            13,20,2,6,1,10,0,8,2,2,13,6,19,14,5,6,2,2,8,4,4,9,4,8,6,21,4,7,6,7,5,6,1,3,0,32,1,11,9,6,7,1,3,13,8,
            7,5,4,4,0,2,21,0,10,8,8,0,8,10,2,3,9,6,4,0,11,2,2,2,11,6,5,5,10,2,22,1,2,4,17,6,6,3,0,8,19,5,3,12,
            13,3,22,3,4,8,8,8,8,14,5,16,13,4,3,5,20,7,9,10,2,2,0,2,14,5,19,10,6,9,5,4,11,2,5,2,1,12,10,6,10,6,8,
            0,7,4,13,6,13,7,2,2,3,16,8,8,8,8,18,1,22,1,2,4,0,10,5,6,6,3,2,15,17,2,6,2,7,5,18,2,7,3,1,2,9,13,5,2,
            12,13,5,4,5,0,1,5,24,1,6,10,1,0,5,4,2,2,15,6,1,2,32,9,2,0,13,2,2,2,13,0,4,8,5,2,10,13,8,6,19,6,2,13,
            2,0,5,1,2,2,3,13,2,1,4,8,9,0,9,8,0,1,4,0,17,9,18,9,1,2,2,5,11,24,4,2,5,16,0,2,9,4,2,6,9,9,1,4,13,11,
            8,15,4,0,11,4,0,4,26,4,3,8,1,6,5,24,9,3,17,4,9,5,4,6,16,20,2,2,0,4,0,4,1,0,9,2,2,50,2,2,8,2,2,6,6,
            23,2,0,7,14,27,1,2,14,8,10,1,0,5,20,0,4,0,2,6,11,10,2,20,11,12,9,3,4,0,17,2,1,3,0,8,8,10,3,11,6,16,
            2,13,9,4,0,4,3,23,14,8,9,8,4,11,5,14,1,3,9,8,1,32,4,1,4,5,4,6,5,18,0,2,13,5,3,0,5,2,4,2,2,8,5,0,16,
            0,9,11,10,0,21,8,9,5,7,0,10,11,0,25,9,7,11,13,2,2,15,20,5,0,8,2,6,16,16,6,11,6,0,8,17,2,11,11,4,0,4,
            6,5,9,7,8,9,3,5,13,3,2,3,10,5,0,4,3,6,9,8,13,2,1,4,6,5,1,16,3,2,2,0,9,4,8,2,12,4,11,12,5,7,2,3,13,
            13,0,2,11,0,5,13,9,2,7,0,1,6,6,4,8,5,5,2,2,11,20,2,5,2,4,8,7,6,5,8,2,12,21,19,0,9,11,11,2,2,8,6,1,8,
            6,8,25,19,0,9,11,10,13,17,12,8,1,6,10,14,5,9,1,6,4,4,19,13,0,8,2,9,13,2,2,10,5,2,14,6,10,5,8,13,13,
            4,8,2,1,3,0,19,7,2,6,16,0,14,24,1,6,1,6,4,9,4,5,8,11,12,4,12,11,10,6,38,8,2,2,1,11,8,11,5,5,4,1,8,5,
            6,5,19,8,0,11,6,0,8,2,5,2,11,14,1,8,2,14,3,13,2,6,7,13,2,3,4,0,8,29,6,2,7,16,2,1,2,8,10,8,14,8,3,2,
            5,4,6,2,5,5,5,15,1,4,13,13,11,4,3,15,7,6,10,9,1,11,3,5,0,2,2,6,6,16,2,4,6,12,8,1,3,19,5,5,8,6,13,3,
            1,4,2,3,10,11,15,14,3,4,3,7,6,10,0,3,18,17,4,3,9,4,5,0,6,3,5,0,26,1,0,5,6,13,10,3,10,1,0,9,9,12,10,
            1,6,1,7,6,2,3,11,2,5,4,5,1,0,13,12,6,16,8,19,3,20,4,6,2,1,26,17,5,1,6,6,2,2,0,4,11,20,14,11,4,2,1,6,
            13,6,2,9,6,11,5,0,8,6,17,12,20,6,16,8,9,6,4,1,0,9,2,8,17,6,0,13,9,2,2,3,10,11,7,6,11,28,8,5,3,6,9,1,
            13,23,6,13,2,17,0,7,5,2,1,11,2,5,14,9,7,6,5,6,9,9,9,8,1,17,10,6,9,13,4,3,0,5,11,7,2,6,3,1,26,7,6,13,
            8,11,5,0,2,2,4,35,3,1,2,5,2,2,4,6,10,6,1,6,1,9,7,0,4,4,5,2,4,21,2,2,5,4,3,1,11,2,5,8,2,2,2,21,13,7,
            5,2,6,11,7,2,5,12,22,5,5,12,1,6,2,2,5,2,1,3,2,6,31,8,9,22,4,12,30,28,6,2,3,5,4,2,8,0,2,26,14,2,4,3,
            9,5,5,2,1,6,1,7,0,17,14,6,1,11,12,2,23,2,0,10,6,1,5,23,4,0,4,0,11,1,6,2,3,8,7,1,4,11,3,1,11,7,8,7,6,
            2,1,4,4,6,39,5,2,5,4,6,0,4,0,5,13,5,8,6,13,5,2,0,20,1,6,18,2,4,5,17,13,2,2,6,10,2,11,5,3,13,4,9,3,4,
            8,25,22,4,7,6,2,9,1,11,0,10,0,2,27,6,10,0,11,14,2,3,9,2,5,0,1,21,2,17,1,8,5,2,14,2,6,2,2,11,10,3,2,
            2,14,9,2,14,6,5,11,2,3,4,9,0,1,10,22,4,6,8,11,4,11,5,3,6,6,14,3,29,13,0,14,5,7,4,1,4,6,10,2,9,5,8,2,
            10,12,6,12,8,6,1,8,0,12,3,1,17,9,5,2,1,3,4,10,17,1,3,17,22,2,10,6,3,5,2,4,14,9,10,8,3,6,4,17,1,11,0,
            6,0,5,6,8,4,5,12,16,0,15,2,5,1,6,2,9,6,10,5,9,6,1,2,14,1,3,4,24,5,5,8,1,5,20,2,8,3,4,7,0,18,7,16,1,
            2,6,16,7,3,2,10,9,13,2,3,14,5,0,11,1,13,12,8,11,17,5,20,12,17,4,23,21,10,2,2,2,3,6,6,0,1,14,1,6,5,5,
            5,8,20,2,8,4,12,4,9,8,4,3,5,5,0,10,18,3,9,2,6,10,0,6,0,1,22,1,2,5,5,16,2,4,0,15,2,5,4,16,0,13,22,2,
            4,8,3,5,2,5,4,2,12,8,8,3,6,6,17,1,6,20,2,7,6,13,9,8,2,18,0,8,4,8,2,9,10,12,3,10,5,2,8,5,0,16,8,17,5,
            5,8,27,1,6,7,13,13,2,14,1,8,7,10,2,20,2,8,9,16,9,7,0,11,6,0,2,10,2,6,12,13,13,3,4,8,0,1,18,1,2,2,8,
            9,7,17,16,3,2,2,12,4,0,4,0,2,5,5,0,10,1,6,2,2,16,2,9,14,2,11,12,4,2,6,11,13,3,4,0,4,11,8,4,6,2,5,0,
            10,9,1,4,1,14,2,9,17,1,2,1,0,22,12,2,2,2,4,14,11,1,6,14,0,5,4,14,10,0,17,9,4,1,0,17,27,8,14,5,1,14,
            2,8,5,2,6,4,5,9,8,8,1,13,11,6,6,2,8,10,22,6,2,19,10,2,2,6,5,2,2,14,2,6,6,4,6,2,12,13,3,9,8,8,2,2,21,
            10,7,4,12,22,1,8,6,5,13,8,3,11,5,2,3,12,5,8,3,4,10,7,11,4,5,0,1,5,8,23,1,6,4,2,6,19,6,8,7,3,2,9,5,
            10,9,1,4,4,1,2,2,14,2,0,9,3,4,13,1,19,6,12,10,2,5,2,5,18,3,8,1,2,3,2,17,2,5,16,1,2,1,22,5,6,12,1,16,
            9,9,10,8,21,12,4,12,4,5,5,8,4,0,8,18,7,3,4,6,11,8,11,3,16,17,20,1,4,4,4,2,0,4,11,19,0,5,6,10,8,1,2,
            9,4,2,3,4,9,13,0,5,3,1,8,2,4,3,5,6,10,0,43,17,18,2,5,10,33,10,2,23,6,3,7,1,8,3,5,6,6,0,5,8,3,14,7,8,
            5,18,8,13,14,7,2,18,3,6,5,14,3,4,13,9,8,4,0,9,4,17,6,14,0,16,4,8,2,14,0,4,3,2,7,2,22,6,9,0,8,0,4,8,
            19,11,4,0,2,12,4,5,0,1,12,5,7,12,6,7,11,1,2,3,12,0,3,4,5,11,0,4,0,2,4,8,0,6,0,10,16,6,18,12,3,0,11,
            5,17,12,7,2,3,17,2,13,0,4,20,2,8,3,4,7,17,11,8,12,20,13,14,5,0,5,17,4,3,1,0,8,8,8,0,4,11,6,30,15,17,
            1,11,0,1,11,2,5,
        };

        internal static IEnumerable<PolyGF2> SmallIrreducibles()
        {
            yield return new PolyGF2(1);
            yield return new PolyGF2(0, 1);
            ulong code = 3;

            foreach (var skip in smallSkips)
            {
                code += (skip + 1UL) * 2;
                yield return PolyGF2.FromCode(code);
            }
        }
    }
}