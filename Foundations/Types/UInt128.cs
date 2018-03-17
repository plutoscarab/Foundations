
/*
UInt128.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Text;
using Foundations.Types;

namespace Foundations
{
    /// <summary>
    /// 128-bit unsigned integer.
    /// </summary>
	public struct UInt128
    {
        const ulong msb = 0x8000000000000000;

        ulong hi;
        ulong lo;
        bool overflow;

        /// <summary>
        /// Constant value 0.
        /// </summary>
        public static UInt128 Zero = new UInt128(0);

        /// <summary>
        /// Constant value 1.
        /// </summary>
        public static UInt128 One = new UInt128(1);

        /// <summary>
        /// Minimum value (0).
        /// </summary>
        public static UInt128 MinValue = Zero;

        /// <summary>
        /// Maximum value, about 3.4×10³⁸.
        /// </summary>
        public static UInt128 MaxValue = new UInt128(ulong.MaxValue, ulong.MaxValue);
        private static UInt128 msb128 = new UInt128(msb, 0);

        /// <summary>
        /// Create a <see cref="UInt128"/> from high- and low-order <see cref="UInt64"/> halves.
        /// </summary>
        public UInt128(ulong hi, ulong lo)
        {
            this.hi = hi;
            this.lo = lo;
            overflow = false;
        }

        /// <summary>
        /// Create a <see cref="UInt128"/> from a <see cref="UInt64"/>.
        /// </summary>
        public UInt128(ulong u)
            : this(0, u)
        {
        }

        /// <summary>
        /// Create a <see cref="UInt128"/> from a <see cref="UInt64"/>.
        /// </summary>
        public static implicit operator UInt128(ulong u)
        {
            return new UInt128(u);
        }

        /// <summary>
        /// Gets the high-order <see cref="UInt64"/> half.
        /// </summary>
        public ulong Hi
        {
            get { return hi; }
            set { hi = value; }
        }

        /// <summary>
        /// Gets the low-order <see cref="UInt64"/> half.
        /// </summary>
        public ulong Lo
        {
            get { return lo; }
            set { lo = value; }
        }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is UInt128))
                return false;
            UInt128 o = (UInt128)obj;
            return o.hi == hi && o.lo == lo;
        }

        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashHelper.Finisher(HashHelper.Mixer(-1428872695, hi, lo));
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt128 a, UInt128 b)
        {
            return a.lo == b.lo && a.hi == b.hi;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt128 a, UInt128 b)
        {
            return a.lo != b.lo || a.hi != b.hi;
        }

        /// <summary>
        /// Greater-than operator.
        /// </summary>
        public static bool operator >(UInt128 a, UInt128 b)
        {
            return a.hi > b.hi || (a.hi == b.hi && a.lo > b.lo);
        }

        /// <summary>
        /// Less-than operator.
        /// </summary>
        public static bool operator <(UInt128 a, UInt128 b)
        {
            return a.hi < b.hi || (a.hi == b.hi && a.lo < b.lo);
        }

        /// <summary>
        /// Not-less-than operator.
        /// </summary>
        public static bool operator >=(UInt128 a, UInt128 b)
        {
            return a.hi > b.hi || (a.hi == b.hi && a.lo >= b.lo);
        }

        /// <summary>
        /// Not-greater-than operator.
        /// </summary>
        public static bool operator <=(UInt128 a, UInt128 b)
        {
            return a.hi < b.hi || (a.hi == b.hi && a.lo <= b.lo);
        }

        /// <summary>
        /// Indicates whether this value is an incorrect result due to
        /// overflow or underflow of an arithmetic operation on <see cref="UInt128"/> values.
        /// </summary>
        public bool Overflow
        {
            get { return overflow; }
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt128 operator +(UInt128 a, UInt128 b)
        {
            UInt128 c;
            c.lo = a.lo + b.lo;
            bool carry = c.lo < a.lo;
            c.hi = a.hi + b.hi;
            c.overflow = c.hi < a.hi;
            if (carry)
            {
                c.hi++;
                if (c.hi == 0) c.overflow = true;
            }
            return c;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt128 operator -(UInt128 a, UInt128 b)
        {
            UInt128 c;
            bool borrow = a.lo < b.lo;
            c.lo = a.lo - b.lo;
            c.overflow = a.hi < b.hi;
            c.hi = a.hi - b.hi;
            if (borrow)
            {
                if (c.hi == 0)
                    c.overflow = true;
                c.hi--;
            }
            return c;
        }

        /// <summary>
        /// Multiply two <see cref="UInt64"/> values and get the product as a <see cref="UInt128"/> value.
        /// </summary>
        public static UInt128 Multiply(ulong x, ulong y)
        {
            UInt128 z;
            z.overflow = false;

            if (x == 0 || y == 0)
            {
                z.hi = z.lo = 0;
                return z;
            }

            uint a = (uint)(x >> 32);
            uint b = (uint)x;
            uint c = (uint)(y >> 32);
            uint d = (uint)y;
            ulong l = (ulong)b * d;
            ulong m1 = (ulong)a * d;
            ulong m2 = (ulong)b * c;
            ulong m = m1 + m2;
            ulong h = (ulong)a * c;
            z.lo = l + (m << 32);
            z.hi = h + (m >> 32);
            if (z.lo < l) z.hi++;
            if (m < m1 || m < m2) z.hi += 0x100000000;
            return z;
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt128 operator *(UInt128 x, UInt128 y)
        {
            bool overflow = false;

            if (x.hi != 0 && y.hi != 0)
                overflow = true;

            UInt128 ac = Multiply(x.lo, y.lo);
            UInt128 temp = Multiply(x.lo + x.hi, y.lo + y.hi) - ac - Multiply(x.hi, y.hi);

            if (temp.hi != 0)
                overflow = true;

            temp.ShiftLeft(64);
            temp += ac;

            if (overflow)
                temp.overflow = true;

            return temp;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt128 operator /(UInt128 x, UInt128 y)
        {
            UInt128 r;
            return DivRem(x, y, out r);
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt128 operator %(UInt128 x, UInt128 y)
        {
            UInt128 r;
            DivRem(x, y, out r);
            return r;
        }

        /// <summary>
        /// Division and remainder ("/" and "%" together).
        /// </summary>
        public static UInt128 DivRem(UInt128 x, UInt128 y, out UInt128 remainder)
        {
            if (y == 0)
                throw new DivideByZeroException();

            if (x.lo == 0 && y.lo == 0)
            {
                remainder = UInt128.Zero;
                return UInt128.Zero;
            }

            if (y > x)
            {
                remainder = x;
                return UInt128.Zero;
            }

            int nx = x.FindHightBit();
            int ny = y.FindHightBit();
            int sh = ny - nx;
            y.ShiftLeft(sh);
            UInt128 mask = UInt128.One << sh;
            UInt128 q = UInt128.Zero;

            while (sh-- >= 0)
            {
                if (x >= y)
                {
                    x -= y;
                    q |= mask;
                }

                mask.ShiftRight();
                y.ShiftRight();
            }

            remainder = x;
            return q;
        }

        /// <summary>
        /// In-place right-shift operation.
        /// </summary>
        public static void ShiftRight(ref UInt128 a, int b)
        {
            if (b < 0)
            {
                ShiftLeft(ref a, -b);
                return;
            }

            if (b == 0)
                return;

            if (b < 64)
            {
                a.lo = (a.lo >> b) | (a.hi << (64 - b));
                a.hi >>= b;
                return;
            }

            if (b == 64)
            {
                a.lo = a.hi;
                a.hi = 0;
                return;
            }

            if (b < 128)
            {
                a.lo = a.hi >> b - 64;
                a.hi = 0;
                return;
            }

            a.hi = a.lo = 0;
        }

        /// <summary>
        /// Right-shift operator operator.
        /// </summary>
        public static UInt128 operator >>(UInt128 a, int b)
        {
            ShiftRight(ref a, b);
            return a;
        }

        /// <summary>
        /// In-place left-shift operation.
        /// </summary>
        public static void ShiftLeft(ref UInt128 a, int b)
        {
            if (b < 0)
            {
                ShiftRight(ref a, -b);
                return;
            }

            if (b == 0)
                return;

            if (b < 64)
            {
                a.hi = (a.hi << b) | (a.lo >> (64 - b));
                a.lo <<= b;
                return;
            }

            if (b == 64)
            {
                a.hi = a.lo;
                a.lo = 0;
                return;
            }

            if (b < 128)
            {
                a.hi = a.lo << b - 64;
                a.lo = 0;
                return;
            }

            a.hi = a.lo = 0;
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt128 operator <<(UInt128 a, int b)
        {
            ShiftLeft(ref a, b);
            return a;
        }

        /// <summary>
        /// In-place left shift by 1 bit.
        /// </summary>
        public void ShiftLeft()
        {
            hi = (hi << 1) | (lo >> 63);
            lo <<= 1;
        }

        /// <summary>
        /// In-place left-shift.
        /// </summary>
        public void ShiftLeft(int count)
        {
            ShiftLeft(ref this, count);
        }

        /// <summary>
        /// In-place right-shift by 1 bit.
        /// </summary>
        public void ShiftRight()
        {
            lo = (lo >> 1) | (hi << 63);
            hi >>= 1;
        }

        /// <summary>
        /// In-place right-shift.
        /// </summary>
        public void ShiftRight(int count)
        {
            ShiftRight(ref this, count);
        }

        /// <summary>
        /// In-place right shift with 1 shifted in from left.
        /// </summary>
        public void ShiftRightWithMsb()
        {
            lo = (lo >> 1) | (hi << 63);
            hi = (hi >> 1) | msb;
        }

        /// <summary>
        /// Indicates whether the most-significant bit is set.
        /// </summary>
        public bool IsMsbSet()
        {
            return (hi & msb) != 0;
        }

        /// <summary>
        /// Indicates whether the value is 0.
        /// </summary>
        public bool IsZero()
        {
            return hi == 0 && lo == 0;
        }

        private static int FindHighBit(ulong u)
        {
            if (u == 0)
                throw new ArithmeticException();

            int count = 0;
            while ((u & msb) == 0)
            {
                count++;
                u <<= 1;
            }
            return count;
        }

        private int FindHightBit()
        {
            if (hi == 0 && lo == 0)
                throw new ArithmeticException();

            if (hi != 0)
                return FindHighBit(hi);

            return 64 + FindHighBit(lo);
        }

        /// <summary>
        /// Shifts the value left if and until the MSB is not 0.
        /// </summary>
        public int Normalize()
        {
            if (hi == 0 && lo == 0)
                throw new ArithmeticException();

            int count = FindHightBit();
            ShiftLeft(ref this, count);
            return count;
        }

        /// <summary>
        /// Bitwise AND operator.
        /// </summary>
        public static UInt128 operator &(UInt128 a, UInt128 b)
        {
            a.hi &= b.hi;
            a.lo &= b.lo;
            return a;
        }

        /// <summary>
        /// Bitwise OR operator.
        /// </summary>
        public static UInt128 operator |(UInt128 a, UInt128 b)
        {
            a.hi |= b.hi;
            a.lo |= b.lo;
            return a;
        }

        /// <summary>
        /// Bitwise XOR operator.
        /// </summary>
        public static UInt128 operator ^(UInt128 a, UInt128 b)
        {
            a.hi ^= b.hi;
            a.lo ^= b.lo;
            return a;
        }

        /// <summary>
        /// Bitwise NOT operator.
        /// </summary>
        public static UInt128 operator ~(UInt128 a)
        {
            a.hi = ~a.hi;
            a.lo = ~a.lo;
            return a;
        }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override string ToString()
        {
            if (lo == 0 && hi == 0)
                return "0";

            var s = new StringBuilder();
            UInt128 b = 10;
            UInt128 d = this;

            while (d.lo != 0 || d.hi != 0)
            {
                UInt128 r;
                d = DivRem(d, b, out r);
                s.Insert(0, (char)((int)'0' + (int)r.lo));
            }

            return s.ToString();
        }
    }
}