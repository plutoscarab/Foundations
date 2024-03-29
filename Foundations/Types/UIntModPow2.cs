﻿
namespace Foundations.Types
{
    /// <summary>
    /// Unsigned 1-bit integer.
    /// </summary>
    public struct UInt1 : IEquatable<UInt1>, IComparable<UInt1>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt1"/>.
        /// </summary>
        public readonly byte Value;

        /// <summary>
        /// Creates a <see cref="UInt1"/>.
        /// </summary>
        public UInt1(byte value)
        {
            Value = (byte)(value & 0b1);
        }

        /// <summary>
        /// Creates a <see cref="UInt1"/>.
        /// </summary>
        public UInt1(int value)
        {
            Value = (byte)(value & 0b1);
        }

        /// <summary>
        /// Creates a <see cref="UInt1"/>.
        /// </summary>
        public UInt1(long value)
        {
            Value = (byte)(value & 0b1);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt1"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator byte(UInt1 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt1"/>.
        /// </summary>
        public static implicit operator UInt1(byte value)
        { return new UInt1(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt1 && Equals((UInt1)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt1}"/>.
        /// </summary>
        public readonly bool Equals(UInt1 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt1 a, UInt1 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt1 a, UInt1 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt1}"/>.
        /// </summary>
        public readonly int CompareTo(UInt1 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt1 operator +(UInt1 a, UInt1 b)
        {
            return new UInt1(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt1 Add(UInt1 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt1 operator -(UInt1 a, UInt1 b)
        {
            return new UInt1(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt1 Subtract(UInt1 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt1 operator -(UInt1 value)
        {
            return new UInt1(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt1 operator *(UInt1 a, UInt1 b)
        {
            return new UInt1(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt1 Multiply(UInt1 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt1 operator /(UInt1 a, UInt1 b)
        {
            return new UInt1(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt1 Divide(UInt1 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt1 operator %(UInt1 a, UInt1 b)
        {
            return new UInt1(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt1 Mod(UInt1 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt1 Pow(UInt1 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1;
                }

                e >>= 1;
                b = (b * b) & 0b1;
            }

            return new UInt1(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt1 operator &(UInt1 a, UInt1 b)
        {
            return new UInt1(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt1 And(UInt1 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt1 operator |(UInt1 a, UInt1 b)
        {
            return new UInt1(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt1 Or(UInt1 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt1 operator ^(UInt1 a, UInt1 b)
        {
            return new UInt1(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt1 Xor(UInt1 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt1 operator ~(UInt1 value)
        {
            return new UInt1(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt1 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt1 operator >>(UInt1 value, int shift)
        {
            return new UInt1(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt1 operator <<(UInt1 value, int shift)
        {
            return new UInt1(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 2-bit integer.
    /// </summary>
    public struct UInt2 : IEquatable<UInt2>, IComparable<UInt2>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt2"/>.
        /// </summary>
        public readonly byte Value;

        /// <summary>
        /// Creates a <see cref="UInt2"/>.
        /// </summary>
        public UInt2(byte value)
        {
            Value = (byte)(value & 0b11);
        }

        /// <summary>
        /// Creates a <see cref="UInt2"/>.
        /// </summary>
        public UInt2(int value)
        {
            Value = (byte)(value & 0b11);
        }

        /// <summary>
        /// Creates a <see cref="UInt2"/>.
        /// </summary>
        public UInt2(long value)
        {
            Value = (byte)(value & 0b11);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt2"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator byte(UInt2 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt2"/>.
        /// </summary>
        public static implicit operator UInt2(byte value)
        { return new UInt2(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt2 && Equals((UInt2)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt2}"/>.
        /// </summary>
        public readonly bool Equals(UInt2 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt2 a, UInt2 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt2 a, UInt2 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt2}"/>.
        /// </summary>
        public readonly int CompareTo(UInt2 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt2 operator +(UInt2 a, UInt2 b)
        {
            return new UInt2(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt2 Add(UInt2 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt2 operator -(UInt2 a, UInt2 b)
        {
            return new UInt2(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt2 Subtract(UInt2 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt2 operator -(UInt2 value)
        {
            return new UInt2(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt2 operator *(UInt2 a, UInt2 b)
        {
            return new UInt2(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt2 Multiply(UInt2 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt2 operator /(UInt2 a, UInt2 b)
        {
            return new UInt2(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt2 Divide(UInt2 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt2 operator %(UInt2 a, UInt2 b)
        {
            return new UInt2(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt2 Mod(UInt2 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt2 Pow(UInt2 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11;
                }

                e >>= 1;
                b = (b * b) & 0b11;
            }

            return new UInt2(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt2 operator &(UInt2 a, UInt2 b)
        {
            return new UInt2(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt2 And(UInt2 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt2 operator |(UInt2 a, UInt2 b)
        {
            return new UInt2(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt2 Or(UInt2 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt2 operator ^(UInt2 a, UInt2 b)
        {
            return new UInt2(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt2 Xor(UInt2 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt2 operator ~(UInt2 value)
        {
            return new UInt2(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt2 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt2 operator >>(UInt2 value, int shift)
        {
            return new UInt2(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt2 operator <<(UInt2 value, int shift)
        {
            return new UInt2(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 3-bit integer.
    /// </summary>
    public struct UInt3 : IEquatable<UInt3>, IComparable<UInt3>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt3"/>.
        /// </summary>
        public readonly byte Value;

        /// <summary>
        /// Creates a <see cref="UInt3"/>.
        /// </summary>
        public UInt3(byte value)
        {
            Value = (byte)(value & 0b111);
        }

        /// <summary>
        /// Creates a <see cref="UInt3"/>.
        /// </summary>
        public UInt3(int value)
        {
            Value = (byte)(value & 0b111);
        }

        /// <summary>
        /// Creates a <see cref="UInt3"/>.
        /// </summary>
        public UInt3(long value)
        {
            Value = (byte)(value & 0b111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt3"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator byte(UInt3 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt3"/>.
        /// </summary>
        public static implicit operator UInt3(byte value)
        { return new UInt3(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt3 && Equals((UInt3)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt3}"/>.
        /// </summary>
        public readonly bool Equals(UInt3 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt3 a, UInt3 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt3 a, UInt3 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt3}"/>.
        /// </summary>
        public readonly int CompareTo(UInt3 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt3 operator +(UInt3 a, UInt3 b)
        {
            return new UInt3(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt3 Add(UInt3 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt3 operator -(UInt3 a, UInt3 b)
        {
            return new UInt3(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt3 Subtract(UInt3 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt3 operator -(UInt3 value)
        {
            return new UInt3(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt3 operator *(UInt3 a, UInt3 b)
        {
            return new UInt3(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt3 Multiply(UInt3 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt3 operator /(UInt3 a, UInt3 b)
        {
            return new UInt3(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt3 Divide(UInt3 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt3 operator %(UInt3 a, UInt3 b)
        {
            return new UInt3(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt3 Mod(UInt3 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt3 Pow(UInt3 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111;
                }

                e >>= 1;
                b = (b * b) & 0b111;
            }

            return new UInt3(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt3 operator &(UInt3 a, UInt3 b)
        {
            return new UInt3(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt3 And(UInt3 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt3 operator |(UInt3 a, UInt3 b)
        {
            return new UInt3(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt3 Or(UInt3 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt3 operator ^(UInt3 a, UInt3 b)
        {
            return new UInt3(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt3 Xor(UInt3 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt3 operator ~(UInt3 value)
        {
            return new UInt3(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt3 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt3 operator >>(UInt3 value, int shift)
        {
            return new UInt3(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt3 operator <<(UInt3 value, int shift)
        {
            return new UInt3(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 4-bit integer.
    /// </summary>
    public struct UInt4 : IEquatable<UInt4>, IComparable<UInt4>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt4"/>.
        /// </summary>
        public readonly byte Value;

        /// <summary>
        /// Creates a <see cref="UInt4"/>.
        /// </summary>
        public UInt4(byte value)
        {
            Value = (byte)(value & 0b1111);
        }

        /// <summary>
        /// Creates a <see cref="UInt4"/>.
        /// </summary>
        public UInt4(int value)
        {
            Value = (byte)(value & 0b1111);
        }

        /// <summary>
        /// Creates a <see cref="UInt4"/>.
        /// </summary>
        public UInt4(long value)
        {
            Value = (byte)(value & 0b1111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt4"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator byte(UInt4 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt4"/>.
        /// </summary>
        public static implicit operator UInt4(byte value)
        { return new UInt4(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt4 && Equals((UInt4)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt4}"/>.
        /// </summary>
        public readonly bool Equals(UInt4 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt4 a, UInt4 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt4 a, UInt4 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt4}"/>.
        /// </summary>
        public readonly int CompareTo(UInt4 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt4 operator +(UInt4 a, UInt4 b)
        {
            return new UInt4(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt4 Add(UInt4 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt4 operator -(UInt4 a, UInt4 b)
        {
            return new UInt4(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt4 Subtract(UInt4 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt4 operator -(UInt4 value)
        {
            return new UInt4(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt4 operator *(UInt4 a, UInt4 b)
        {
            return new UInt4(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt4 Multiply(UInt4 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt4 operator /(UInt4 a, UInt4 b)
        {
            return new UInt4(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt4 Divide(UInt4 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt4 operator %(UInt4 a, UInt4 b)
        {
            return new UInt4(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt4 Mod(UInt4 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt4 Pow(UInt4 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111;
                }

                e >>= 1;
                b = (b * b) & 0b1111;
            }

            return new UInt4(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt4 operator &(UInt4 a, UInt4 b)
        {
            return new UInt4(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt4 And(UInt4 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt4 operator |(UInt4 a, UInt4 b)
        {
            return new UInt4(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt4 Or(UInt4 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt4 operator ^(UInt4 a, UInt4 b)
        {
            return new UInt4(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt4 Xor(UInt4 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt4 operator ~(UInt4 value)
        {
            return new UInt4(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt4 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt4 operator >>(UInt4 value, int shift)
        {
            return new UInt4(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt4 operator <<(UInt4 value, int shift)
        {
            return new UInt4(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 5-bit integer.
    /// </summary>
    public struct UInt5 : IEquatable<UInt5>, IComparable<UInt5>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt5"/>.
        /// </summary>
        public readonly byte Value;

        /// <summary>
        /// Creates a <see cref="UInt5"/>.
        /// </summary>
        public UInt5(byte value)
        {
            Value = (byte)(value & 0b11111);
        }

        /// <summary>
        /// Creates a <see cref="UInt5"/>.
        /// </summary>
        public UInt5(int value)
        {
            Value = (byte)(value & 0b11111);
        }

        /// <summary>
        /// Creates a <see cref="UInt5"/>.
        /// </summary>
        public UInt5(long value)
        {
            Value = (byte)(value & 0b11111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt5"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator byte(UInt5 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt5"/>.
        /// </summary>
        public static implicit operator UInt5(byte value)
        { return new UInt5(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt5 && Equals((UInt5)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt5}"/>.
        /// </summary>
        public readonly bool Equals(UInt5 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt5 a, UInt5 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt5 a, UInt5 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt5}"/>.
        /// </summary>
        public readonly int CompareTo(UInt5 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt5 operator +(UInt5 a, UInt5 b)
        {
            return new UInt5(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt5 Add(UInt5 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt5 operator -(UInt5 a, UInt5 b)
        {
            return new UInt5(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt5 Subtract(UInt5 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt5 operator -(UInt5 value)
        {
            return new UInt5(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt5 operator *(UInt5 a, UInt5 b)
        {
            return new UInt5(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt5 Multiply(UInt5 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt5 operator /(UInt5 a, UInt5 b)
        {
            return new UInt5(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt5 Divide(UInt5 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt5 operator %(UInt5 a, UInt5 b)
        {
            return new UInt5(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt5 Mod(UInt5 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt5 Pow(UInt5 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111;
                }

                e >>= 1;
                b = (b * b) & 0b11111;
            }

            return new UInt5(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt5 operator &(UInt5 a, UInt5 b)
        {
            return new UInt5(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt5 And(UInt5 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt5 operator |(UInt5 a, UInt5 b)
        {
            return new UInt5(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt5 Or(UInt5 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt5 operator ^(UInt5 a, UInt5 b)
        {
            return new UInt5(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt5 Xor(UInt5 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt5 operator ~(UInt5 value)
        {
            return new UInt5(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt5 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt5 operator >>(UInt5 value, int shift)
        {
            return new UInt5(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt5 operator <<(UInt5 value, int shift)
        {
            return new UInt5(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 6-bit integer.
    /// </summary>
    public struct UInt6 : IEquatable<UInt6>, IComparable<UInt6>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt6"/>.
        /// </summary>
        public readonly byte Value;

        /// <summary>
        /// Creates a <see cref="UInt6"/>.
        /// </summary>
        public UInt6(byte value)
        {
            Value = (byte)(value & 0b111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt6"/>.
        /// </summary>
        public UInt6(int value)
        {
            Value = (byte)(value & 0b111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt6"/>.
        /// </summary>
        public UInt6(long value)
        {
            Value = (byte)(value & 0b111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt6"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator byte(UInt6 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt6"/>.
        /// </summary>
        public static implicit operator UInt6(byte value)
        { return new UInt6(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt6 && Equals((UInt6)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt6}"/>.
        /// </summary>
        public readonly bool Equals(UInt6 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt6 a, UInt6 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt6 a, UInt6 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt6}"/>.
        /// </summary>
        public readonly int CompareTo(UInt6 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt6 operator +(UInt6 a, UInt6 b)
        {
            return new UInt6(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt6 Add(UInt6 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt6 operator -(UInt6 a, UInt6 b)
        {
            return new UInt6(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt6 Subtract(UInt6 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt6 operator -(UInt6 value)
        {
            return new UInt6(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt6 operator *(UInt6 a, UInt6 b)
        {
            return new UInt6(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt6 Multiply(UInt6 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt6 operator /(UInt6 a, UInt6 b)
        {
            return new UInt6(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt6 Divide(UInt6 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt6 operator %(UInt6 a, UInt6 b)
        {
            return new UInt6(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt6 Mod(UInt6 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt6 Pow(UInt6 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111111;
                }

                e >>= 1;
                b = (b * b) & 0b111111;
            }

            return new UInt6(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt6 operator &(UInt6 a, UInt6 b)
        {
            return new UInt6(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt6 And(UInt6 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt6 operator |(UInt6 a, UInt6 b)
        {
            return new UInt6(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt6 Or(UInt6 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt6 operator ^(UInt6 a, UInt6 b)
        {
            return new UInt6(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt6 Xor(UInt6 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt6 operator ~(UInt6 value)
        {
            return new UInt6(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt6 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt6 operator >>(UInt6 value, int shift)
        {
            return new UInt6(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt6 operator <<(UInt6 value, int shift)
        {
            return new UInt6(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 7-bit integer.
    /// </summary>
    public struct UInt7 : IEquatable<UInt7>, IComparable<UInt7>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt7"/>.
        /// </summary>
        public readonly byte Value;

        /// <summary>
        /// Creates a <see cref="UInt7"/>.
        /// </summary>
        public UInt7(byte value)
        {
            Value = (byte)(value & 0b1111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt7"/>.
        /// </summary>
        public UInt7(int value)
        {
            Value = (byte)(value & 0b1111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt7"/>.
        /// </summary>
        public UInt7(long value)
        {
            Value = (byte)(value & 0b1111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt7"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator byte(UInt7 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt7"/>.
        /// </summary>
        public static implicit operator UInt7(byte value)
        { return new UInt7(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt7 && Equals((UInt7)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt7}"/>.
        /// </summary>
        public readonly bool Equals(UInt7 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt7 a, UInt7 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt7 a, UInt7 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt7}"/>.
        /// </summary>
        public readonly int CompareTo(UInt7 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt7 operator +(UInt7 a, UInt7 b)
        {
            return new UInt7(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt7 Add(UInt7 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt7 operator -(UInt7 a, UInt7 b)
        {
            return new UInt7(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt7 Subtract(UInt7 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt7 operator -(UInt7 value)
        {
            return new UInt7(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt7 operator *(UInt7 a, UInt7 b)
        {
            return new UInt7(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt7 Multiply(UInt7 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt7 operator /(UInt7 a, UInt7 b)
        {
            return new UInt7(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt7 Divide(UInt7 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt7 operator %(UInt7 a, UInt7 b)
        {
            return new UInt7(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt7 Mod(UInt7 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt7 Pow(UInt7 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111111;
                }

                e >>= 1;
                b = (b * b) & 0b1111111;
            }

            return new UInt7(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt7 operator &(UInt7 a, UInt7 b)
        {
            return new UInt7(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt7 And(UInt7 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt7 operator |(UInt7 a, UInt7 b)
        {
            return new UInt7(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt7 Or(UInt7 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt7 operator ^(UInt7 a, UInt7 b)
        {
            return new UInt7(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt7 Xor(UInt7 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt7 operator ~(UInt7 value)
        {
            return new UInt7(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt7 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt7 operator >>(UInt7 value, int shift)
        {
            return new UInt7(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt7 operator <<(UInt7 value, int shift)
        {
            return new UInt7(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 8-bit integer.
    /// </summary>
    public struct UInt8 : IEquatable<UInt8>, IComparable<UInt8>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt8"/>.
        /// </summary>
        public readonly byte Value;

        /// <summary>
        /// Creates a <see cref="UInt8"/>.
        /// </summary>
        public UInt8(byte value)
        {
            Value = (byte)(value & 0b11111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt8"/>.
        /// </summary>
        public UInt8(int value)
        {
            Value = (byte)(value & 0b11111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt8"/>.
        /// </summary>
        public UInt8(long value)
        {
            Value = (byte)(value & 0b11111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt8"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator byte(UInt8 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt8"/>.
        /// </summary>
        public static implicit operator UInt8(byte value)
        { return new UInt8(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt8 && Equals((UInt8)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt8}"/>.
        /// </summary>
        public readonly bool Equals(UInt8 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt8 a, UInt8 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt8 a, UInt8 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt8}"/>.
        /// </summary>
        public readonly int CompareTo(UInt8 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt8 operator +(UInt8 a, UInt8 b)
        {
            return new UInt8(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt8 Add(UInt8 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt8 operator -(UInt8 a, UInt8 b)
        {
            return new UInt8(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt8 Subtract(UInt8 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt8 operator -(UInt8 value)
        {
            return new UInt8(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt8 operator *(UInt8 a, UInt8 b)
        {
            return new UInt8(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt8 Multiply(UInt8 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt8 operator /(UInt8 a, UInt8 b)
        {
            return new UInt8(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt8 Divide(UInt8 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt8 operator %(UInt8 a, UInt8 b)
        {
            return new UInt8(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt8 Mod(UInt8 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt8 Pow(UInt8 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111111;
                }

                e >>= 1;
                b = (b * b) & 0b11111111;
            }

            return new UInt8(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt8 operator &(UInt8 a, UInt8 b)
        {
            return new UInt8(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt8 And(UInt8 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt8 operator |(UInt8 a, UInt8 b)
        {
            return new UInt8(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt8 Or(UInt8 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt8 operator ^(UInt8 a, UInt8 b)
        {
            return new UInt8(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt8 Xor(UInt8 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt8 operator ~(UInt8 value)
        {
            return new UInt8(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt8 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt8 operator >>(UInt8 value, int shift)
        {
            return new UInt8(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt8 operator <<(UInt8 value, int shift)
        {
            return new UInt8(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 9-bit integer.
    /// </summary>
    public struct UInt9 : IEquatable<UInt9>, IComparable<UInt9>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt9"/>.
        /// </summary>
        public readonly ushort Value;

        /// <summary>
        /// Creates a <see cref="UInt9"/>.
        /// </summary>
        public UInt9(ushort value)
        {
            Value = (ushort)(value & 0b111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt9"/>.
        /// </summary>
        public UInt9(int value)
        {
            Value = (ushort)(value & 0b111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt9"/>.
        /// </summary>
        public UInt9(long value)
        {
            Value = (ushort)(value & 0b111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt9"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ushort(UInt9 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt9"/>.
        /// </summary>
        public static implicit operator UInt9(ushort value)
        { return new UInt9(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt9 && Equals((UInt9)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt9}"/>.
        /// </summary>
        public readonly bool Equals(UInt9 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt9 a, UInt9 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt9 a, UInt9 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt9}"/>.
        /// </summary>
        public readonly int CompareTo(UInt9 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt9 operator +(UInt9 a, UInt9 b)
        {
            return new UInt9(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt9 Add(UInt9 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt9 operator -(UInt9 a, UInt9 b)
        {
            return new UInt9(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt9 Subtract(UInt9 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt9 operator -(UInt9 value)
        {
            return new UInt9(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt9 operator *(UInt9 a, UInt9 b)
        {
            return new UInt9(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt9 Multiply(UInt9 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt9 operator /(UInt9 a, UInt9 b)
        {
            return new UInt9(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt9 Divide(UInt9 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt9 operator %(UInt9 a, UInt9 b)
        {
            return new UInt9(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt9 Mod(UInt9 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt9 Pow(UInt9 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111111111;
                }

                e >>= 1;
                b = (b * b) & 0b111111111;
            }

            return new UInt9(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt9 operator &(UInt9 a, UInt9 b)
        {
            return new UInt9(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt9 And(UInt9 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt9 operator |(UInt9 a, UInt9 b)
        {
            return new UInt9(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt9 Or(UInt9 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt9 operator ^(UInt9 a, UInt9 b)
        {
            return new UInt9(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt9 Xor(UInt9 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt9 operator ~(UInt9 value)
        {
            return new UInt9(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt9 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt9 operator >>(UInt9 value, int shift)
        {
            return new UInt9(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt9 operator <<(UInt9 value, int shift)
        {
            return new UInt9(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 10-bit integer.
    /// </summary>
    public struct UInt10 : IEquatable<UInt10>, IComparable<UInt10>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt10"/>.
        /// </summary>
        public readonly ushort Value;

        /// <summary>
        /// Creates a <see cref="UInt10"/>.
        /// </summary>
        public UInt10(ushort value)
        {
            Value = (ushort)(value & 0b1111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt10"/>.
        /// </summary>
        public UInt10(int value)
        {
            Value = (ushort)(value & 0b1111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt10"/>.
        /// </summary>
        public UInt10(long value)
        {
            Value = (ushort)(value & 0b1111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt10"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ushort(UInt10 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt10"/>.
        /// </summary>
        public static implicit operator UInt10(ushort value)
        { return new UInt10(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt10 && Equals((UInt10)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt10}"/>.
        /// </summary>
        public readonly bool Equals(UInt10 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt10 a, UInt10 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt10 a, UInt10 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt10}"/>.
        /// </summary>
        public readonly int CompareTo(UInt10 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt10 operator +(UInt10 a, UInt10 b)
        {
            return new UInt10(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt10 Add(UInt10 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt10 operator -(UInt10 a, UInt10 b)
        {
            return new UInt10(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt10 Subtract(UInt10 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt10 operator -(UInt10 value)
        {
            return new UInt10(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt10 operator *(UInt10 a, UInt10 b)
        {
            return new UInt10(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt10 Multiply(UInt10 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt10 operator /(UInt10 a, UInt10 b)
        {
            return new UInt10(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt10 Divide(UInt10 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt10 operator %(UInt10 a, UInt10 b)
        {
            return new UInt10(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt10 Mod(UInt10 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt10 Pow(UInt10 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111111111;
                }

                e >>= 1;
                b = (b * b) & 0b1111111111;
            }

            return new UInt10(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt10 operator &(UInt10 a, UInt10 b)
        {
            return new UInt10(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt10 And(UInt10 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt10 operator |(UInt10 a, UInt10 b)
        {
            return new UInt10(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt10 Or(UInt10 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt10 operator ^(UInt10 a, UInt10 b)
        {
            return new UInt10(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt10 Xor(UInt10 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt10 operator ~(UInt10 value)
        {
            return new UInt10(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt10 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt10 operator >>(UInt10 value, int shift)
        {
            return new UInt10(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt10 operator <<(UInt10 value, int shift)
        {
            return new UInt10(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 11-bit integer.
    /// </summary>
    public struct UInt11 : IEquatable<UInt11>, IComparable<UInt11>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt11"/>.
        /// </summary>
        public readonly ushort Value;

        /// <summary>
        /// Creates a <see cref="UInt11"/>.
        /// </summary>
        public UInt11(ushort value)
        {
            Value = (ushort)(value & 0b11111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt11"/>.
        /// </summary>
        public UInt11(int value)
        {
            Value = (ushort)(value & 0b11111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt11"/>.
        /// </summary>
        public UInt11(long value)
        {
            Value = (ushort)(value & 0b11111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt11"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ushort(UInt11 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt11"/>.
        /// </summary>
        public static implicit operator UInt11(ushort value)
        { return new UInt11(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt11 && Equals((UInt11)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt11}"/>.
        /// </summary>
        public readonly bool Equals(UInt11 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt11 a, UInt11 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt11 a, UInt11 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt11}"/>.
        /// </summary>
        public readonly int CompareTo(UInt11 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt11 operator +(UInt11 a, UInt11 b)
        {
            return new UInt11(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt11 Add(UInt11 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt11 operator -(UInt11 a, UInt11 b)
        {
            return new UInt11(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt11 Subtract(UInt11 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt11 operator -(UInt11 value)
        {
            return new UInt11(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt11 operator *(UInt11 a, UInt11 b)
        {
            return new UInt11(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt11 Multiply(UInt11 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt11 operator /(UInt11 a, UInt11 b)
        {
            return new UInt11(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt11 Divide(UInt11 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt11 operator %(UInt11 a, UInt11 b)
        {
            return new UInt11(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt11 Mod(UInt11 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt11 Pow(UInt11 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111111111;
                }

                e >>= 1;
                b = (b * b) & 0b11111111111;
            }

            return new UInt11(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt11 operator &(UInt11 a, UInt11 b)
        {
            return new UInt11(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt11 And(UInt11 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt11 operator |(UInt11 a, UInt11 b)
        {
            return new UInt11(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt11 Or(UInt11 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt11 operator ^(UInt11 a, UInt11 b)
        {
            return new UInt11(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt11 Xor(UInt11 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt11 operator ~(UInt11 value)
        {
            return new UInt11(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt11 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt11 operator >>(UInt11 value, int shift)
        {
            return new UInt11(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt11 operator <<(UInt11 value, int shift)
        {
            return new UInt11(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 12-bit integer.
    /// </summary>
    public struct UInt12 : IEquatable<UInt12>, IComparable<UInt12>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt12"/>.
        /// </summary>
        public readonly ushort Value;

        /// <summary>
        /// Creates a <see cref="UInt12"/>.
        /// </summary>
        public UInt12(ushort value)
        {
            Value = (ushort)(value & 0b111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt12"/>.
        /// </summary>
        public UInt12(int value)
        {
            Value = (ushort)(value & 0b111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt12"/>.
        /// </summary>
        public UInt12(long value)
        {
            Value = (ushort)(value & 0b111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt12"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ushort(UInt12 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt12"/>.
        /// </summary>
        public static implicit operator UInt12(ushort value)
        { return new UInt12(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt12 && Equals((UInt12)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt12}"/>.
        /// </summary>
        public readonly bool Equals(UInt12 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt12 a, UInt12 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt12 a, UInt12 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt12}"/>.
        /// </summary>
        public readonly int CompareTo(UInt12 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt12 operator +(UInt12 a, UInt12 b)
        {
            return new UInt12(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt12 Add(UInt12 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt12 operator -(UInt12 a, UInt12 b)
        {
            return new UInt12(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt12 Subtract(UInt12 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt12 operator -(UInt12 value)
        {
            return new UInt12(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt12 operator *(UInt12 a, UInt12 b)
        {
            return new UInt12(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt12 Multiply(UInt12 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt12 operator /(UInt12 a, UInt12 b)
        {
            return new UInt12(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt12 Divide(UInt12 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt12 operator %(UInt12 a, UInt12 b)
        {
            return new UInt12(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt12 Mod(UInt12 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt12 Pow(UInt12 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b111111111111;
            }

            return new UInt12(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt12 operator &(UInt12 a, UInt12 b)
        {
            return new UInt12(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt12 And(UInt12 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt12 operator |(UInt12 a, UInt12 b)
        {
            return new UInt12(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt12 Or(UInt12 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt12 operator ^(UInt12 a, UInt12 b)
        {
            return new UInt12(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt12 Xor(UInt12 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt12 operator ~(UInt12 value)
        {
            return new UInt12(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt12 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt12 operator >>(UInt12 value, int shift)
        {
            return new UInt12(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt12 operator <<(UInt12 value, int shift)
        {
            return new UInt12(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 13-bit integer.
    /// </summary>
    public struct UInt13 : IEquatable<UInt13>, IComparable<UInt13>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt13"/>.
        /// </summary>
        public readonly ushort Value;

        /// <summary>
        /// Creates a <see cref="UInt13"/>.
        /// </summary>
        public UInt13(ushort value)
        {
            Value = (ushort)(value & 0b1111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt13"/>.
        /// </summary>
        public UInt13(int value)
        {
            Value = (ushort)(value & 0b1111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt13"/>.
        /// </summary>
        public UInt13(long value)
        {
            Value = (ushort)(value & 0b1111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt13"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ushort(UInt13 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt13"/>.
        /// </summary>
        public static implicit operator UInt13(ushort value)
        { return new UInt13(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt13 && Equals((UInt13)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt13}"/>.
        /// </summary>
        public readonly bool Equals(UInt13 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt13 a, UInt13 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt13 a, UInt13 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt13}"/>.
        /// </summary>
        public readonly int CompareTo(UInt13 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt13 operator +(UInt13 a, UInt13 b)
        {
            return new UInt13(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt13 Add(UInt13 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt13 operator -(UInt13 a, UInt13 b)
        {
            return new UInt13(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt13 Subtract(UInt13 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt13 operator -(UInt13 value)
        {
            return new UInt13(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt13 operator *(UInt13 a, UInt13 b)
        {
            return new UInt13(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt13 Multiply(UInt13 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt13 operator /(UInt13 a, UInt13 b)
        {
            return new UInt13(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt13 Divide(UInt13 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt13 operator %(UInt13 a, UInt13 b)
        {
            return new UInt13(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt13 Mod(UInt13 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt13 Pow(UInt13 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b1111111111111;
            }

            return new UInt13(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt13 operator &(UInt13 a, UInt13 b)
        {
            return new UInt13(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt13 And(UInt13 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt13 operator |(UInt13 a, UInt13 b)
        {
            return new UInt13(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt13 Or(UInt13 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt13 operator ^(UInt13 a, UInt13 b)
        {
            return new UInt13(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt13 Xor(UInt13 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt13 operator ~(UInt13 value)
        {
            return new UInt13(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt13 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt13 operator >>(UInt13 value, int shift)
        {
            return new UInt13(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt13 operator <<(UInt13 value, int shift)
        {
            return new UInt13(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 14-bit integer.
    /// </summary>
    public struct UInt14 : IEquatable<UInt14>, IComparable<UInt14>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt14"/>.
        /// </summary>
        public readonly ushort Value;

        /// <summary>
        /// Creates a <see cref="UInt14"/>.
        /// </summary>
        public UInt14(ushort value)
        {
            Value = (ushort)(value & 0b11111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt14"/>.
        /// </summary>
        public UInt14(int value)
        {
            Value = (ushort)(value & 0b11111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt14"/>.
        /// </summary>
        public UInt14(long value)
        {
            Value = (ushort)(value & 0b11111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt14"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ushort(UInt14 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt14"/>.
        /// </summary>
        public static implicit operator UInt14(ushort value)
        { return new UInt14(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt14 && Equals((UInt14)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt14}"/>.
        /// </summary>
        public readonly bool Equals(UInt14 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt14 a, UInt14 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt14 a, UInt14 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt14}"/>.
        /// </summary>
        public readonly int CompareTo(UInt14 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt14 operator +(UInt14 a, UInt14 b)
        {
            return new UInt14(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt14 Add(UInt14 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt14 operator -(UInt14 a, UInt14 b)
        {
            return new UInt14(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt14 Subtract(UInt14 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt14 operator -(UInt14 value)
        {
            return new UInt14(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt14 operator *(UInt14 a, UInt14 b)
        {
            return new UInt14(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt14 Multiply(UInt14 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt14 operator /(UInt14 a, UInt14 b)
        {
            return new UInt14(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt14 Divide(UInt14 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt14 operator %(UInt14 a, UInt14 b)
        {
            return new UInt14(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt14 Mod(UInt14 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt14 Pow(UInt14 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b11111111111111;
            }

            return new UInt14(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt14 operator &(UInt14 a, UInt14 b)
        {
            return new UInt14(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt14 And(UInt14 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt14 operator |(UInt14 a, UInt14 b)
        {
            return new UInt14(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt14 Or(UInt14 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt14 operator ^(UInt14 a, UInt14 b)
        {
            return new UInt14(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt14 Xor(UInt14 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt14 operator ~(UInt14 value)
        {
            return new UInt14(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt14 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt14 operator >>(UInt14 value, int shift)
        {
            return new UInt14(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt14 operator <<(UInt14 value, int shift)
        {
            return new UInt14(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 15-bit integer.
    /// </summary>
    public struct UInt15 : IEquatable<UInt15>, IComparable<UInt15>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt15"/>.
        /// </summary>
        public readonly ushort Value;

        /// <summary>
        /// Creates a <see cref="UInt15"/>.
        /// </summary>
        public UInt15(ushort value)
        {
            Value = (ushort)(value & 0b111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt15"/>.
        /// </summary>
        public UInt15(int value)
        {
            Value = (ushort)(value & 0b111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt15"/>.
        /// </summary>
        public UInt15(long value)
        {
            Value = (ushort)(value & 0b111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt15"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ushort(UInt15 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt15"/>.
        /// </summary>
        public static implicit operator UInt15(ushort value)
        { return new UInt15(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt15 && Equals((UInt15)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt15}"/>.
        /// </summary>
        public readonly bool Equals(UInt15 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt15 a, UInt15 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt15 a, UInt15 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt15}"/>.
        /// </summary>
        public readonly int CompareTo(UInt15 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt15 operator +(UInt15 a, UInt15 b)
        {
            return new UInt15(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt15 Add(UInt15 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt15 operator -(UInt15 a, UInt15 b)
        {
            return new UInt15(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt15 Subtract(UInt15 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt15 operator -(UInt15 value)
        {
            return new UInt15(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt15 operator *(UInt15 a, UInt15 b)
        {
            return new UInt15(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt15 Multiply(UInt15 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt15 operator /(UInt15 a, UInt15 b)
        {
            return new UInt15(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt15 Divide(UInt15 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt15 operator %(UInt15 a, UInt15 b)
        {
            return new UInt15(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt15 Mod(UInt15 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt15 Pow(UInt15 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b111111111111111;
            }

            return new UInt15(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt15 operator &(UInt15 a, UInt15 b)
        {
            return new UInt15(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt15 And(UInt15 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt15 operator |(UInt15 a, UInt15 b)
        {
            return new UInt15(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt15 Or(UInt15 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt15 operator ^(UInt15 a, UInt15 b)
        {
            return new UInt15(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt15 Xor(UInt15 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt15 operator ~(UInt15 value)
        {
            return new UInt15(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt15 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt15 operator >>(UInt15 value, int shift)
        {
            return new UInt15(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt15 operator <<(UInt15 value, int shift)
        {
            return new UInt15(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 16-bit integer.
    /// </summary>
    public struct UInt16Ex : IEquatable<UInt16Ex>, IComparable<UInt16Ex>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt16Ex"/>.
        /// </summary>
        public readonly ushort Value;

        /// <summary>
        /// Creates a <see cref="UInt16Ex"/>.
        /// </summary>
        public UInt16Ex(ushort value)
        {
            Value = (ushort)(value & 0b1111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt16Ex"/>.
        /// </summary>
        public UInt16Ex(int value)
        {
            Value = (ushort)(value & 0b1111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt16Ex"/>.
        /// </summary>
        public UInt16Ex(long value)
        {
            Value = (ushort)(value & 0b1111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt16Ex"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ushort(UInt16Ex value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt16Ex"/>.
        /// </summary>
        public static implicit operator UInt16Ex(ushort value)
        { return new UInt16Ex(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt16Ex && Equals((UInt16Ex)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt16Ex}"/>.
        /// </summary>
        public readonly bool Equals(UInt16Ex other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt16Ex a, UInt16Ex b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt16Ex a, UInt16Ex b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt16Ex}"/>.
        /// </summary>
        public readonly int CompareTo(UInt16Ex other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt16Ex operator +(UInt16Ex a, UInt16Ex b)
        {
            return new UInt16Ex(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt16Ex Add(UInt16Ex other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt16Ex operator -(UInt16Ex a, UInt16Ex b)
        {
            return new UInt16Ex(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt16Ex Subtract(UInt16Ex other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt16Ex operator -(UInt16Ex value)
        {
            return new UInt16Ex(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt16Ex operator *(UInt16Ex a, UInt16Ex b)
        {
            return new UInt16Ex(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt16Ex Multiply(UInt16Ex other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt16Ex operator /(UInt16Ex a, UInt16Ex b)
        {
            return new UInt16Ex(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt16Ex Divide(UInt16Ex other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt16Ex operator %(UInt16Ex a, UInt16Ex b)
        {
            return new UInt16Ex(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt16Ex Mod(UInt16Ex other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt16Ex Pow(UInt16Ex value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            int result = 1;
            int b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b1111111111111111;
            }

            return new UInt16Ex(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt16Ex operator &(UInt16Ex a, UInt16Ex b)
        {
            return new UInt16Ex(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt16Ex And(UInt16Ex other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt16Ex operator |(UInt16Ex a, UInt16Ex b)
        {
            return new UInt16Ex(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt16Ex Or(UInt16Ex other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt16Ex operator ^(UInt16Ex a, UInt16Ex b)
        {
            return new UInt16Ex(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt16Ex Xor(UInt16Ex other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt16Ex operator ~(UInt16Ex value)
        {
            return new UInt16Ex(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt16Ex Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt16Ex operator >>(UInt16Ex value, int shift)
        {
            return new UInt16Ex(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt16Ex operator <<(UInt16Ex value, int shift)
        {
            return new UInt16Ex(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 17-bit integer.
    /// </summary>
    public struct UInt17 : IEquatable<UInt17>, IComparable<UInt17>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt17"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt17"/>.
        /// </summary>
        public UInt17(uint value)
        {
            Value = (uint)(value & 0b11111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt17"/>.
        /// </summary>
        public UInt17(int value)
        {
            Value = (uint)(value & 0b11111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt17"/>.
        /// </summary>
        public UInt17(long value)
        {
            Value = (uint)(value & 0b11111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt17"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt17 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt17"/>.
        /// </summary>
        public static implicit operator UInt17(uint value)
        { return new UInt17(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt17 && Equals((UInt17)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt17}"/>.
        /// </summary>
        public readonly bool Equals(UInt17 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt17 a, UInt17 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt17 a, UInt17 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt17}"/>.
        /// </summary>
        public readonly int CompareTo(UInt17 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt17 operator +(UInt17 a, UInt17 b)
        {
            return new UInt17(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt17 Add(UInt17 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt17 operator -(UInt17 a, UInt17 b)
        {
            return new UInt17(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt17 Subtract(UInt17 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt17 operator -(UInt17 value)
        {
            return new UInt17(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt17 operator *(UInt17 a, UInt17 b)
        {
            return new UInt17(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt17 Multiply(UInt17 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt17 operator /(UInt17 a, UInt17 b)
        {
            return new UInt17(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt17 Divide(UInt17 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt17 operator %(UInt17 a, UInt17 b)
        {
            return new UInt17(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt17 Mod(UInt17 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt17 Pow(UInt17 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b11111111111111111;
            }

            return new UInt17(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt17 operator &(UInt17 a, UInt17 b)
        {
            return new UInt17(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt17 And(UInt17 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt17 operator |(UInt17 a, UInt17 b)
        {
            return new UInt17(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt17 Or(UInt17 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt17 operator ^(UInt17 a, UInt17 b)
        {
            return new UInt17(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt17 Xor(UInt17 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt17 operator ~(UInt17 value)
        {
            return new UInt17(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt17 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt17 operator >>(UInt17 value, int shift)
        {
            return new UInt17(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt17 operator <<(UInt17 value, int shift)
        {
            return new UInt17(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 18-bit integer.
    /// </summary>
    public struct UInt18 : IEquatable<UInt18>, IComparable<UInt18>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt18"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt18"/>.
        /// </summary>
        public UInt18(uint value)
        {
            Value = (uint)(value & 0b111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt18"/>.
        /// </summary>
        public UInt18(int value)
        {
            Value = (uint)(value & 0b111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt18"/>.
        /// </summary>
        public UInt18(long value)
        {
            Value = (uint)(value & 0b111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt18"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt18 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt18"/>.
        /// </summary>
        public static implicit operator UInt18(uint value)
        { return new UInt18(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt18 && Equals((UInt18)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt18}"/>.
        /// </summary>
        public readonly bool Equals(UInt18 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt18 a, UInt18 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt18 a, UInt18 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt18}"/>.
        /// </summary>
        public readonly int CompareTo(UInt18 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt18 operator +(UInt18 a, UInt18 b)
        {
            return new UInt18(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt18 Add(UInt18 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt18 operator -(UInt18 a, UInt18 b)
        {
            return new UInt18(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt18 Subtract(UInt18 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt18 operator -(UInt18 value)
        {
            return new UInt18(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt18 operator *(UInt18 a, UInt18 b)
        {
            return new UInt18(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt18 Multiply(UInt18 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt18 operator /(UInt18 a, UInt18 b)
        {
            return new UInt18(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt18 Divide(UInt18 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt18 operator %(UInt18 a, UInt18 b)
        {
            return new UInt18(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt18 Mod(UInt18 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt18 Pow(UInt18 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b111111111111111111;
            }

            return new UInt18(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt18 operator &(UInt18 a, UInt18 b)
        {
            return new UInt18(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt18 And(UInt18 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt18 operator |(UInt18 a, UInt18 b)
        {
            return new UInt18(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt18 Or(UInt18 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt18 operator ^(UInt18 a, UInt18 b)
        {
            return new UInt18(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt18 Xor(UInt18 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt18 operator ~(UInt18 value)
        {
            return new UInt18(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt18 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt18 operator >>(UInt18 value, int shift)
        {
            return new UInt18(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt18 operator <<(UInt18 value, int shift)
        {
            return new UInt18(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 19-bit integer.
    /// </summary>
    public struct UInt19 : IEquatable<UInt19>, IComparable<UInt19>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt19"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt19"/>.
        /// </summary>
        public UInt19(uint value)
        {
            Value = (uint)(value & 0b1111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt19"/>.
        /// </summary>
        public UInt19(int value)
        {
            Value = (uint)(value & 0b1111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt19"/>.
        /// </summary>
        public UInt19(long value)
        {
            Value = (uint)(value & 0b1111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt19"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt19 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt19"/>.
        /// </summary>
        public static implicit operator UInt19(uint value)
        { return new UInt19(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt19 && Equals((UInt19)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt19}"/>.
        /// </summary>
        public readonly bool Equals(UInt19 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt19 a, UInt19 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt19 a, UInt19 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt19}"/>.
        /// </summary>
        public readonly int CompareTo(UInt19 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt19 operator +(UInt19 a, UInt19 b)
        {
            return new UInt19(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt19 Add(UInt19 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt19 operator -(UInt19 a, UInt19 b)
        {
            return new UInt19(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt19 Subtract(UInt19 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt19 operator -(UInt19 value)
        {
            return new UInt19(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt19 operator *(UInt19 a, UInt19 b)
        {
            return new UInt19(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt19 Multiply(UInt19 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt19 operator /(UInt19 a, UInt19 b)
        {
            return new UInt19(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt19 Divide(UInt19 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt19 operator %(UInt19 a, UInt19 b)
        {
            return new UInt19(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt19 Mod(UInt19 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt19 Pow(UInt19 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b1111111111111111111;
            }

            return new UInt19(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt19 operator &(UInt19 a, UInt19 b)
        {
            return new UInt19(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt19 And(UInt19 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt19 operator |(UInt19 a, UInt19 b)
        {
            return new UInt19(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt19 Or(UInt19 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt19 operator ^(UInt19 a, UInt19 b)
        {
            return new UInt19(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt19 Xor(UInt19 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt19 operator ~(UInt19 value)
        {
            return new UInt19(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt19 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt19 operator >>(UInt19 value, int shift)
        {
            return new UInt19(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt19 operator <<(UInt19 value, int shift)
        {
            return new UInt19(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 20-bit integer.
    /// </summary>
    public struct UInt20 : IEquatable<UInt20>, IComparable<UInt20>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt20"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt20"/>.
        /// </summary>
        public UInt20(uint value)
        {
            Value = (uint)(value & 0b11111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt20"/>.
        /// </summary>
        public UInt20(int value)
        {
            Value = (uint)(value & 0b11111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt20"/>.
        /// </summary>
        public UInt20(long value)
        {
            Value = (uint)(value & 0b11111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt20"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt20 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt20"/>.
        /// </summary>
        public static implicit operator UInt20(uint value)
        { return new UInt20(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt20 && Equals((UInt20)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt20}"/>.
        /// </summary>
        public readonly bool Equals(UInt20 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt20 a, UInt20 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt20 a, UInt20 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt20}"/>.
        /// </summary>
        public readonly int CompareTo(UInt20 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt20 operator +(UInt20 a, UInt20 b)
        {
            return new UInt20(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt20 Add(UInt20 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt20 operator -(UInt20 a, UInt20 b)
        {
            return new UInt20(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt20 Subtract(UInt20 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt20 operator -(UInt20 value)
        {
            return new UInt20(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt20 operator *(UInt20 a, UInt20 b)
        {
            return new UInt20(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt20 Multiply(UInt20 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt20 operator /(UInt20 a, UInt20 b)
        {
            return new UInt20(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt20 Divide(UInt20 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt20 operator %(UInt20 a, UInt20 b)
        {
            return new UInt20(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt20 Mod(UInt20 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt20 Pow(UInt20 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b11111111111111111111;
            }

            return new UInt20(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt20 operator &(UInt20 a, UInt20 b)
        {
            return new UInt20(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt20 And(UInt20 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt20 operator |(UInt20 a, UInt20 b)
        {
            return new UInt20(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt20 Or(UInt20 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt20 operator ^(UInt20 a, UInt20 b)
        {
            return new UInt20(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt20 Xor(UInt20 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt20 operator ~(UInt20 value)
        {
            return new UInt20(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt20 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt20 operator >>(UInt20 value, int shift)
        {
            return new UInt20(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt20 operator <<(UInt20 value, int shift)
        {
            return new UInt20(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 21-bit integer.
    /// </summary>
    public struct UInt21 : IEquatable<UInt21>, IComparable<UInt21>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt21"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt21"/>.
        /// </summary>
        public UInt21(uint value)
        {
            Value = (uint)(value & 0b111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt21"/>.
        /// </summary>
        public UInt21(int value)
        {
            Value = (uint)(value & 0b111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt21"/>.
        /// </summary>
        public UInt21(long value)
        {
            Value = (uint)(value & 0b111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt21"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt21 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt21"/>.
        /// </summary>
        public static implicit operator UInt21(uint value)
        { return new UInt21(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt21 && Equals((UInt21)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt21}"/>.
        /// </summary>
        public readonly bool Equals(UInt21 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt21 a, UInt21 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt21 a, UInt21 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt21}"/>.
        /// </summary>
        public readonly int CompareTo(UInt21 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt21 operator +(UInt21 a, UInt21 b)
        {
            return new UInt21(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt21 Add(UInt21 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt21 operator -(UInt21 a, UInt21 b)
        {
            return new UInt21(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt21 Subtract(UInt21 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt21 operator -(UInt21 value)
        {
            return new UInt21(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt21 operator *(UInt21 a, UInt21 b)
        {
            return new UInt21(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt21 Multiply(UInt21 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt21 operator /(UInt21 a, UInt21 b)
        {
            return new UInt21(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt21 Divide(UInt21 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt21 operator %(UInt21 a, UInt21 b)
        {
            return new UInt21(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt21 Mod(UInt21 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt21 Pow(UInt21 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b111111111111111111111;
            }

            return new UInt21(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt21 operator &(UInt21 a, UInt21 b)
        {
            return new UInt21(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt21 And(UInt21 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt21 operator |(UInt21 a, UInt21 b)
        {
            return new UInt21(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt21 Or(UInt21 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt21 operator ^(UInt21 a, UInt21 b)
        {
            return new UInt21(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt21 Xor(UInt21 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt21 operator ~(UInt21 value)
        {
            return new UInt21(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt21 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt21 operator >>(UInt21 value, int shift)
        {
            return new UInt21(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt21 operator <<(UInt21 value, int shift)
        {
            return new UInt21(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 22-bit integer.
    /// </summary>
    public struct UInt22 : IEquatable<UInt22>, IComparable<UInt22>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt22"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt22"/>.
        /// </summary>
        public UInt22(uint value)
        {
            Value = (uint)(value & 0b1111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt22"/>.
        /// </summary>
        public UInt22(int value)
        {
            Value = (uint)(value & 0b1111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt22"/>.
        /// </summary>
        public UInt22(long value)
        {
            Value = (uint)(value & 0b1111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt22"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt22 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt22"/>.
        /// </summary>
        public static implicit operator UInt22(uint value)
        { return new UInt22(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt22 && Equals((UInt22)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt22}"/>.
        /// </summary>
        public readonly bool Equals(UInt22 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt22 a, UInt22 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt22 a, UInt22 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt22}"/>.
        /// </summary>
        public readonly int CompareTo(UInt22 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt22 operator +(UInt22 a, UInt22 b)
        {
            return new UInt22(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt22 Add(UInt22 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt22 operator -(UInt22 a, UInt22 b)
        {
            return new UInt22(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt22 Subtract(UInt22 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt22 operator -(UInt22 value)
        {
            return new UInt22(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt22 operator *(UInt22 a, UInt22 b)
        {
            return new UInt22(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt22 Multiply(UInt22 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt22 operator /(UInt22 a, UInt22 b)
        {
            return new UInt22(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt22 Divide(UInt22 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt22 operator %(UInt22 a, UInt22 b)
        {
            return new UInt22(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt22 Mod(UInt22 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt22 Pow(UInt22 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b1111111111111111111111;
            }

            return new UInt22(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt22 operator &(UInt22 a, UInt22 b)
        {
            return new UInt22(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt22 And(UInt22 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt22 operator |(UInt22 a, UInt22 b)
        {
            return new UInt22(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt22 Or(UInt22 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt22 operator ^(UInt22 a, UInt22 b)
        {
            return new UInt22(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt22 Xor(UInt22 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt22 operator ~(UInt22 value)
        {
            return new UInt22(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt22 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt22 operator >>(UInt22 value, int shift)
        {
            return new UInt22(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt22 operator <<(UInt22 value, int shift)
        {
            return new UInt22(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 23-bit integer.
    /// </summary>
    public struct UInt23 : IEquatable<UInt23>, IComparable<UInt23>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt23"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt23"/>.
        /// </summary>
        public UInt23(uint value)
        {
            Value = (uint)(value & 0b11111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt23"/>.
        /// </summary>
        public UInt23(int value)
        {
            Value = (uint)(value & 0b11111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt23"/>.
        /// </summary>
        public UInt23(long value)
        {
            Value = (uint)(value & 0b11111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt23"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt23 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt23"/>.
        /// </summary>
        public static implicit operator UInt23(uint value)
        { return new UInt23(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt23 && Equals((UInt23)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt23}"/>.
        /// </summary>
        public readonly bool Equals(UInt23 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt23 a, UInt23 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt23 a, UInt23 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt23}"/>.
        /// </summary>
        public readonly int CompareTo(UInt23 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt23 operator +(UInt23 a, UInt23 b)
        {
            return new UInt23(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt23 Add(UInt23 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt23 operator -(UInt23 a, UInt23 b)
        {
            return new UInt23(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt23 Subtract(UInt23 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt23 operator -(UInt23 value)
        {
            return new UInt23(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt23 operator *(UInt23 a, UInt23 b)
        {
            return new UInt23(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt23 Multiply(UInt23 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt23 operator /(UInt23 a, UInt23 b)
        {
            return new UInt23(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt23 Divide(UInt23 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt23 operator %(UInt23 a, UInt23 b)
        {
            return new UInt23(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt23 Mod(UInt23 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt23 Pow(UInt23 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b11111111111111111111111;
            }

            return new UInt23(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt23 operator &(UInt23 a, UInt23 b)
        {
            return new UInt23(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt23 And(UInt23 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt23 operator |(UInt23 a, UInt23 b)
        {
            return new UInt23(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt23 Or(UInt23 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt23 operator ^(UInt23 a, UInt23 b)
        {
            return new UInt23(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt23 Xor(UInt23 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt23 operator ~(UInt23 value)
        {
            return new UInt23(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt23 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt23 operator >>(UInt23 value, int shift)
        {
            return new UInt23(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt23 operator <<(UInt23 value, int shift)
        {
            return new UInt23(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 24-bit integer.
    /// </summary>
    public struct UInt24 : IEquatable<UInt24>, IComparable<UInt24>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt24"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt24"/>.
        /// </summary>
        public UInt24(uint value)
        {
            Value = (uint)(value & 0b111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt24"/>.
        /// </summary>
        public UInt24(int value)
        {
            Value = (uint)(value & 0b111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt24"/>.
        /// </summary>
        public UInt24(long value)
        {
            Value = (uint)(value & 0b111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt24"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt24 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt24"/>.
        /// </summary>
        public static implicit operator UInt24(uint value)
        { return new UInt24(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt24 && Equals((UInt24)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt24}"/>.
        /// </summary>
        public readonly bool Equals(UInt24 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt24 a, UInt24 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt24 a, UInt24 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt24}"/>.
        /// </summary>
        public readonly int CompareTo(UInt24 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt24 operator +(UInt24 a, UInt24 b)
        {
            return new UInt24(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt24 Add(UInt24 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt24 operator -(UInt24 a, UInt24 b)
        {
            return new UInt24(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt24 Subtract(UInt24 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt24 operator -(UInt24 value)
        {
            return new UInt24(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt24 operator *(UInt24 a, UInt24 b)
        {
            return new UInt24(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt24 Multiply(UInt24 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt24 operator /(UInt24 a, UInt24 b)
        {
            return new UInt24(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt24 Divide(UInt24 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt24 operator %(UInt24 a, UInt24 b)
        {
            return new UInt24(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt24 Mod(UInt24 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt24 Pow(UInt24 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b111111111111111111111111;
            }

            return new UInt24(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt24 operator &(UInt24 a, UInt24 b)
        {
            return new UInt24(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt24 And(UInt24 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt24 operator |(UInt24 a, UInt24 b)
        {
            return new UInt24(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt24 Or(UInt24 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt24 operator ^(UInt24 a, UInt24 b)
        {
            return new UInt24(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt24 Xor(UInt24 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt24 operator ~(UInt24 value)
        {
            return new UInt24(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt24 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt24 operator >>(UInt24 value, int shift)
        {
            return new UInt24(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt24 operator <<(UInt24 value, int shift)
        {
            return new UInt24(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 25-bit integer.
    /// </summary>
    public struct UInt25 : IEquatable<UInt25>, IComparable<UInt25>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt25"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt25"/>.
        /// </summary>
        public UInt25(uint value)
        {
            Value = (uint)(value & 0b1111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt25"/>.
        /// </summary>
        public UInt25(int value)
        {
            Value = (uint)(value & 0b1111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt25"/>.
        /// </summary>
        public UInt25(long value)
        {
            Value = (uint)(value & 0b1111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt25"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt25 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt25"/>.
        /// </summary>
        public static implicit operator UInt25(uint value)
        { return new UInt25(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt25 && Equals((UInt25)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt25}"/>.
        /// </summary>
        public readonly bool Equals(UInt25 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt25 a, UInt25 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt25 a, UInt25 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt25}"/>.
        /// </summary>
        public readonly int CompareTo(UInt25 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt25 operator +(UInt25 a, UInt25 b)
        {
            return new UInt25(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt25 Add(UInt25 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt25 operator -(UInt25 a, UInt25 b)
        {
            return new UInt25(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt25 Subtract(UInt25 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt25 operator -(UInt25 value)
        {
            return new UInt25(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt25 operator *(UInt25 a, UInt25 b)
        {
            return new UInt25(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt25 Multiply(UInt25 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt25 operator /(UInt25 a, UInt25 b)
        {
            return new UInt25(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt25 Divide(UInt25 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt25 operator %(UInt25 a, UInt25 b)
        {
            return new UInt25(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt25 Mod(UInt25 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt25 Pow(UInt25 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b1111111111111111111111111;
            }

            return new UInt25(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt25 operator &(UInt25 a, UInt25 b)
        {
            return new UInt25(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt25 And(UInt25 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt25 operator |(UInt25 a, UInt25 b)
        {
            return new UInt25(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt25 Or(UInt25 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt25 operator ^(UInt25 a, UInt25 b)
        {
            return new UInt25(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt25 Xor(UInt25 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt25 operator ~(UInt25 value)
        {
            return new UInt25(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt25 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt25 operator >>(UInt25 value, int shift)
        {
            return new UInt25(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt25 operator <<(UInt25 value, int shift)
        {
            return new UInt25(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 26-bit integer.
    /// </summary>
    public struct UInt26 : IEquatable<UInt26>, IComparable<UInt26>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt26"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt26"/>.
        /// </summary>
        public UInt26(uint value)
        {
            Value = (uint)(value & 0b11111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt26"/>.
        /// </summary>
        public UInt26(int value)
        {
            Value = (uint)(value & 0b11111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt26"/>.
        /// </summary>
        public UInt26(long value)
        {
            Value = (uint)(value & 0b11111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt26"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt26 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt26"/>.
        /// </summary>
        public static implicit operator UInt26(uint value)
        { return new UInt26(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt26 && Equals((UInt26)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt26}"/>.
        /// </summary>
        public readonly bool Equals(UInt26 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt26 a, UInt26 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt26 a, UInt26 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt26}"/>.
        /// </summary>
        public readonly int CompareTo(UInt26 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt26 operator +(UInt26 a, UInt26 b)
        {
            return new UInt26(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt26 Add(UInt26 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt26 operator -(UInt26 a, UInt26 b)
        {
            return new UInt26(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt26 Subtract(UInt26 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt26 operator -(UInt26 value)
        {
            return new UInt26(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt26 operator *(UInt26 a, UInt26 b)
        {
            return new UInt26(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt26 Multiply(UInt26 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt26 operator /(UInt26 a, UInt26 b)
        {
            return new UInt26(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt26 Divide(UInt26 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt26 operator %(UInt26 a, UInt26 b)
        {
            return new UInt26(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt26 Mod(UInt26 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt26 Pow(UInt26 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b11111111111111111111111111;
            }

            return new UInt26(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt26 operator &(UInt26 a, UInt26 b)
        {
            return new UInt26(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt26 And(UInt26 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt26 operator |(UInt26 a, UInt26 b)
        {
            return new UInt26(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt26 Or(UInt26 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt26 operator ^(UInt26 a, UInt26 b)
        {
            return new UInt26(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt26 Xor(UInt26 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt26 operator ~(UInt26 value)
        {
            return new UInt26(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt26 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt26 operator >>(UInt26 value, int shift)
        {
            return new UInt26(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt26 operator <<(UInt26 value, int shift)
        {
            return new UInt26(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 27-bit integer.
    /// </summary>
    public struct UInt27 : IEquatable<UInt27>, IComparable<UInt27>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt27"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt27"/>.
        /// </summary>
        public UInt27(uint value)
        {
            Value = (uint)(value & 0b111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt27"/>.
        /// </summary>
        public UInt27(int value)
        {
            Value = (uint)(value & 0b111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt27"/>.
        /// </summary>
        public UInt27(long value)
        {
            Value = (uint)(value & 0b111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt27"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt27 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt27"/>.
        /// </summary>
        public static implicit operator UInt27(uint value)
        { return new UInt27(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt27 && Equals((UInt27)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt27}"/>.
        /// </summary>
        public readonly bool Equals(UInt27 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt27 a, UInt27 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt27 a, UInt27 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt27}"/>.
        /// </summary>
        public readonly int CompareTo(UInt27 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt27 operator +(UInt27 a, UInt27 b)
        {
            return new UInt27(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt27 Add(UInt27 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt27 operator -(UInt27 a, UInt27 b)
        {
            return new UInt27(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt27 Subtract(UInt27 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt27 operator -(UInt27 value)
        {
            return new UInt27(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt27 operator *(UInt27 a, UInt27 b)
        {
            return new UInt27(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt27 Multiply(UInt27 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt27 operator /(UInt27 a, UInt27 b)
        {
            return new UInt27(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt27 Divide(UInt27 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt27 operator %(UInt27 a, UInt27 b)
        {
            return new UInt27(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt27 Mod(UInt27 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt27 Pow(UInt27 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111111111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b111111111111111111111111111;
            }

            return new UInt27(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt27 operator &(UInt27 a, UInt27 b)
        {
            return new UInt27(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt27 And(UInt27 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt27 operator |(UInt27 a, UInt27 b)
        {
            return new UInt27(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt27 Or(UInt27 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt27 operator ^(UInt27 a, UInt27 b)
        {
            return new UInt27(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt27 Xor(UInt27 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt27 operator ~(UInt27 value)
        {
            return new UInt27(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt27 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt27 operator >>(UInt27 value, int shift)
        {
            return new UInt27(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt27 operator <<(UInt27 value, int shift)
        {
            return new UInt27(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 28-bit integer.
    /// </summary>
    public struct UInt28 : IEquatable<UInt28>, IComparable<UInt28>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt28"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt28"/>.
        /// </summary>
        public UInt28(uint value)
        {
            Value = (uint)(value & 0b1111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt28"/>.
        /// </summary>
        public UInt28(int value)
        {
            Value = (uint)(value & 0b1111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt28"/>.
        /// </summary>
        public UInt28(long value)
        {
            Value = (uint)(value & 0b1111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt28"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt28 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt28"/>.
        /// </summary>
        public static implicit operator UInt28(uint value)
        { return new UInt28(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt28 && Equals((UInt28)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt28}"/>.
        /// </summary>
        public readonly bool Equals(UInt28 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt28 a, UInt28 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt28 a, UInt28 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt28}"/>.
        /// </summary>
        public readonly int CompareTo(UInt28 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt28 operator +(UInt28 a, UInt28 b)
        {
            return new UInt28(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt28 Add(UInt28 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt28 operator -(UInt28 a, UInt28 b)
        {
            return new UInt28(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt28 Subtract(UInt28 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt28 operator -(UInt28 value)
        {
            return new UInt28(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt28 operator *(UInt28 a, UInt28 b)
        {
            return new UInt28(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt28 Multiply(UInt28 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt28 operator /(UInt28 a, UInt28 b)
        {
            return new UInt28(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt28 Divide(UInt28 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt28 operator %(UInt28 a, UInt28 b)
        {
            return new UInt28(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt28 Mod(UInt28 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt28 Pow(UInt28 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111111111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b1111111111111111111111111111;
            }

            return new UInt28(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt28 operator &(UInt28 a, UInt28 b)
        {
            return new UInt28(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt28 And(UInt28 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt28 operator |(UInt28 a, UInt28 b)
        {
            return new UInt28(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt28 Or(UInt28 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt28 operator ^(UInt28 a, UInt28 b)
        {
            return new UInt28(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt28 Xor(UInt28 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt28 operator ~(UInt28 value)
        {
            return new UInt28(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt28 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt28 operator >>(UInt28 value, int shift)
        {
            return new UInt28(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt28 operator <<(UInt28 value, int shift)
        {
            return new UInt28(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 29-bit integer.
    /// </summary>
    public struct UInt29 : IEquatable<UInt29>, IComparable<UInt29>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt29"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt29"/>.
        /// </summary>
        public UInt29(uint value)
        {
            Value = (uint)(value & 0b11111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt29"/>.
        /// </summary>
        public UInt29(int value)
        {
            Value = (uint)(value & 0b11111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt29"/>.
        /// </summary>
        public UInt29(long value)
        {
            Value = (uint)(value & 0b11111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt29"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt29 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt29"/>.
        /// </summary>
        public static implicit operator UInt29(uint value)
        { return new UInt29(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt29 && Equals((UInt29)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt29}"/>.
        /// </summary>
        public readonly bool Equals(UInt29 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt29 a, UInt29 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt29 a, UInt29 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt29}"/>.
        /// </summary>
        public readonly int CompareTo(UInt29 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt29 operator +(UInt29 a, UInt29 b)
        {
            return new UInt29(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt29 Add(UInt29 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt29 operator -(UInt29 a, UInt29 b)
        {
            return new UInt29(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt29 Subtract(UInt29 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt29 operator -(UInt29 value)
        {
            return new UInt29(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt29 operator *(UInt29 a, UInt29 b)
        {
            return new UInt29(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt29 Multiply(UInt29 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt29 operator /(UInt29 a, UInt29 b)
        {
            return new UInt29(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt29 Divide(UInt29 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt29 operator %(UInt29 a, UInt29 b)
        {
            return new UInt29(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt29 Mod(UInt29 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt29 Pow(UInt29 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111111111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b11111111111111111111111111111;
            }

            return new UInt29(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt29 operator &(UInt29 a, UInt29 b)
        {
            return new UInt29(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt29 And(UInt29 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt29 operator |(UInt29 a, UInt29 b)
        {
            return new UInt29(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt29 Or(UInt29 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt29 operator ^(UInt29 a, UInt29 b)
        {
            return new UInt29(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt29 Xor(UInt29 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt29 operator ~(UInt29 value)
        {
            return new UInt29(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt29 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt29 operator >>(UInt29 value, int shift)
        {
            return new UInt29(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt29 operator <<(UInt29 value, int shift)
        {
            return new UInt29(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 30-bit integer.
    /// </summary>
    public struct UInt30 : IEquatable<UInt30>, IComparable<UInt30>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt30"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt30"/>.
        /// </summary>
        public UInt30(uint value)
        {
            Value = (uint)(value & 0b111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt30"/>.
        /// </summary>
        public UInt30(int value)
        {
            Value = (uint)(value & 0b111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt30"/>.
        /// </summary>
        public UInt30(long value)
        {
            Value = (uint)(value & 0b111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt30"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt30 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt30"/>.
        /// </summary>
        public static implicit operator UInt30(uint value)
        { return new UInt30(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt30 && Equals((UInt30)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt30}"/>.
        /// </summary>
        public readonly bool Equals(UInt30 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt30 a, UInt30 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt30 a, UInt30 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt30}"/>.
        /// </summary>
        public readonly int CompareTo(UInt30 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt30 operator +(UInt30 a, UInt30 b)
        {
            return new UInt30(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt30 Add(UInt30 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt30 operator -(UInt30 a, UInt30 b)
        {
            return new UInt30(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt30 Subtract(UInt30 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt30 operator -(UInt30 value)
        {
            return new UInt30(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt30 operator *(UInt30 a, UInt30 b)
        {
            return new UInt30(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt30 Multiply(UInt30 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt30 operator /(UInt30 a, UInt30 b)
        {
            return new UInt30(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt30 Divide(UInt30 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt30 operator %(UInt30 a, UInt30 b)
        {
            return new UInt30(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt30 Mod(UInt30 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt30 Pow(UInt30 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b111111111111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b111111111111111111111111111111;
            }

            return new UInt30(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt30 operator &(UInt30 a, UInt30 b)
        {
            return new UInt30(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt30 And(UInt30 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt30 operator |(UInt30 a, UInt30 b)
        {
            return new UInt30(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt30 Or(UInt30 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt30 operator ^(UInt30 a, UInt30 b)
        {
            return new UInt30(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt30 Xor(UInt30 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt30 operator ~(UInt30 value)
        {
            return new UInt30(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt30 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt30 operator >>(UInt30 value, int shift)
        {
            return new UInt30(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt30 operator <<(UInt30 value, int shift)
        {
            return new UInt30(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 31-bit integer.
    /// </summary>
    public struct UInt31 : IEquatable<UInt31>, IComparable<UInt31>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt31"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt31"/>.
        /// </summary>
        public UInt31(uint value)
        {
            Value = (uint)(value & 0b1111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt31"/>.
        /// </summary>
        public UInt31(int value)
        {
            Value = (uint)(value & 0b1111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt31"/>.
        /// </summary>
        public UInt31(long value)
        {
            Value = (uint)(value & 0b1111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt31"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt31 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt31"/>.
        /// </summary>
        public static implicit operator UInt31(uint value)
        { return new UInt31(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt31 && Equals((UInt31)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt31}"/>.
        /// </summary>
        public readonly bool Equals(UInt31 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt31 a, UInt31 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt31 a, UInt31 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt31}"/>.
        /// </summary>
        public readonly int CompareTo(UInt31 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt31 operator +(UInt31 a, UInt31 b)
        {
            return new UInt31(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt31 Add(UInt31 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt31 operator -(UInt31 a, UInt31 b)
        {
            return new UInt31(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt31 Subtract(UInt31 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt31 operator -(UInt31 value)
        {
            return new UInt31(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt31 operator *(UInt31 a, UInt31 b)
        {
            return new UInt31(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt31 Multiply(UInt31 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt31 operator /(UInt31 a, UInt31 b)
        {
            return new UInt31(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt31 Divide(UInt31 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt31 operator %(UInt31 a, UInt31 b)
        {
            return new UInt31(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt31 Mod(UInt31 other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt31 Pow(UInt31 value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b1111111111111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b1111111111111111111111111111111;
            }

            return new UInt31(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt31 operator &(UInt31 a, UInt31 b)
        {
            return new UInt31(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt31 And(UInt31 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt31 operator |(UInt31 a, UInt31 b)
        {
            return new UInt31(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt31 Or(UInt31 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt31 operator ^(UInt31 a, UInt31 b)
        {
            return new UInt31(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt31 Xor(UInt31 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt31 operator ~(UInt31 value)
        {
            return new UInt31(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt31 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt31 operator >>(UInt31 value, int shift)
        {
            return new UInt31(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt31 operator <<(UInt31 value, int shift)
        {
            return new UInt31(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 32-bit integer.
    /// </summary>
    public struct UInt32Ex : IEquatable<UInt32Ex>, IComparable<UInt32Ex>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt32Ex"/>.
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Creates a <see cref="UInt32Ex"/>.
        /// </summary>
        public UInt32Ex(uint value)
        {
            Value = (uint)(value & 0b11111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt32Ex"/>.
        /// </summary>
        public UInt32Ex(int value)
        {
            Value = (uint)(value & 0b11111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt32Ex"/>.
        /// </summary>
        public UInt32Ex(long value)
        {
            Value = (uint)(value & 0b11111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt32Ex"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator uint(UInt32Ex value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt32Ex"/>.
        /// </summary>
        public static implicit operator UInt32Ex(uint value)
        { return new UInt32Ex(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt32Ex && Equals((UInt32Ex)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt32Ex}"/>.
        /// </summary>
        public readonly bool Equals(UInt32Ex other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt32Ex a, UInt32Ex b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt32Ex a, UInt32Ex b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt32Ex}"/>.
        /// </summary>
        public readonly int CompareTo(UInt32Ex other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt32Ex operator +(UInt32Ex a, UInt32Ex b)
        {
            return new UInt32Ex(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt32Ex Add(UInt32Ex other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt32Ex operator -(UInt32Ex a, UInt32Ex b)
        {
            return new UInt32Ex(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt32Ex Subtract(UInt32Ex other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt32Ex operator -(UInt32Ex value)
        {
            return new UInt32Ex(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt32Ex operator *(UInt32Ex a, UInt32Ex b)
        {
            return new UInt32Ex(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt32Ex Multiply(UInt32Ex other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt32Ex operator /(UInt32Ex a, UInt32Ex b)
        {
            return new UInt32Ex(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt32Ex Divide(UInt32Ex other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt32Ex operator %(UInt32Ex a, UInt32Ex b)
        {
            return new UInt32Ex(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt32Ex Mod(UInt32Ex other)
        {
            return this % other;
        }

        /// <summary>
        /// Modular exponentiation.
        /// </summary>
        public static UInt32Ex Pow(UInt32Ex value, int exponent)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(exponent, 0, nameof(exponent));
            
            if (exponent == 0) return 1;
            if (value.Value == 0) return 0;
            long result = 1;
            long b = value.Value;
            var e = Math.Abs(exponent);

            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * b) & 0b11111111111111111111111111111111;
                }

                e >>= 1;
                b = (b * b) & 0b11111111111111111111111111111111;
            }

            return new UInt32Ex(result);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt32Ex operator &(UInt32Ex a, UInt32Ex b)
        {
            return new UInt32Ex(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt32Ex And(UInt32Ex other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt32Ex operator |(UInt32Ex a, UInt32Ex b)
        {
            return new UInt32Ex(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt32Ex Or(UInt32Ex other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt32Ex operator ^(UInt32Ex a, UInt32Ex b)
        {
            return new UInt32Ex(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt32Ex Xor(UInt32Ex other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt32Ex operator ~(UInt32Ex value)
        {
            return new UInt32Ex(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt32Ex Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt32Ex operator >>(UInt32Ex value, int shift)
        {
            return new UInt32Ex(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt32Ex operator <<(UInt32Ex value, int shift)
        {
            return new UInt32Ex(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 33-bit integer.
    /// </summary>
    public struct UInt33 : IEquatable<UInt33>, IComparable<UInt33>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt33"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt33"/>.
        /// </summary>
        public UInt33(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt33"/>.
        /// </summary>
        public UInt33(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt33"/>.
        /// </summary>
        public UInt33(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt33"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt33 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt33"/>.
        /// </summary>
        public static implicit operator UInt33(ulong value)
        { return new UInt33(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt33 && Equals((UInt33)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt33}"/>.
        /// </summary>
        public readonly bool Equals(UInt33 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt33 a, UInt33 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt33 a, UInt33 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt33}"/>.
        /// </summary>
        public readonly int CompareTo(UInt33 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt33 operator +(UInt33 a, UInt33 b)
        {
            return new UInt33(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt33 Add(UInt33 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt33 operator -(UInt33 a, UInt33 b)
        {
            return new UInt33(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt33 Subtract(UInt33 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt33 operator -(UInt33 value)
        {
            return new UInt33(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt33 operator *(UInt33 a, UInt33 b)
        {
            return new UInt33(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt33 Multiply(UInt33 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt33 operator /(UInt33 a, UInt33 b)
        {
            return new UInt33(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt33 Divide(UInt33 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt33 operator %(UInt33 a, UInt33 b)
        {
            return new UInt33(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt33 Mod(UInt33 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt33 operator &(UInt33 a, UInt33 b)
        {
            return new UInt33(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt33 And(UInt33 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt33 operator |(UInt33 a, UInt33 b)
        {
            return new UInt33(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt33 Or(UInt33 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt33 operator ^(UInt33 a, UInt33 b)
        {
            return new UInt33(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt33 Xor(UInt33 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt33 operator ~(UInt33 value)
        {
            return new UInt33(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt33 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt33 operator >>(UInt33 value, int shift)
        {
            return new UInt33(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt33 operator <<(UInt33 value, int shift)
        {
            return new UInt33(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 34-bit integer.
    /// </summary>
    public struct UInt34 : IEquatable<UInt34>, IComparable<UInt34>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt34"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt34"/>.
        /// </summary>
        public UInt34(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt34"/>.
        /// </summary>
        public UInt34(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt34"/>.
        /// </summary>
        public UInt34(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt34"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt34 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt34"/>.
        /// </summary>
        public static implicit operator UInt34(ulong value)
        { return new UInt34(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt34 && Equals((UInt34)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt34}"/>.
        /// </summary>
        public readonly bool Equals(UInt34 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt34 a, UInt34 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt34 a, UInt34 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt34}"/>.
        /// </summary>
        public readonly int CompareTo(UInt34 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt34 operator +(UInt34 a, UInt34 b)
        {
            return new UInt34(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt34 Add(UInt34 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt34 operator -(UInt34 a, UInt34 b)
        {
            return new UInt34(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt34 Subtract(UInt34 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt34 operator -(UInt34 value)
        {
            return new UInt34(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt34 operator *(UInt34 a, UInt34 b)
        {
            return new UInt34(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt34 Multiply(UInt34 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt34 operator /(UInt34 a, UInt34 b)
        {
            return new UInt34(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt34 Divide(UInt34 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt34 operator %(UInt34 a, UInt34 b)
        {
            return new UInt34(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt34 Mod(UInt34 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt34 operator &(UInt34 a, UInt34 b)
        {
            return new UInt34(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt34 And(UInt34 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt34 operator |(UInt34 a, UInt34 b)
        {
            return new UInt34(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt34 Or(UInt34 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt34 operator ^(UInt34 a, UInt34 b)
        {
            return new UInt34(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt34 Xor(UInt34 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt34 operator ~(UInt34 value)
        {
            return new UInt34(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt34 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt34 operator >>(UInt34 value, int shift)
        {
            return new UInt34(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt34 operator <<(UInt34 value, int shift)
        {
            return new UInt34(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 35-bit integer.
    /// </summary>
    public struct UInt35 : IEquatable<UInt35>, IComparable<UInt35>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt35"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt35"/>.
        /// </summary>
        public UInt35(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt35"/>.
        /// </summary>
        public UInt35(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt35"/>.
        /// </summary>
        public UInt35(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt35"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt35 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt35"/>.
        /// </summary>
        public static implicit operator UInt35(ulong value)
        { return new UInt35(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt35 && Equals((UInt35)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt35}"/>.
        /// </summary>
        public readonly bool Equals(UInt35 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt35 a, UInt35 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt35 a, UInt35 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt35}"/>.
        /// </summary>
        public readonly int CompareTo(UInt35 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt35 operator +(UInt35 a, UInt35 b)
        {
            return new UInt35(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt35 Add(UInt35 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt35 operator -(UInt35 a, UInt35 b)
        {
            return new UInt35(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt35 Subtract(UInt35 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt35 operator -(UInt35 value)
        {
            return new UInt35(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt35 operator *(UInt35 a, UInt35 b)
        {
            return new UInt35(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt35 Multiply(UInt35 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt35 operator /(UInt35 a, UInt35 b)
        {
            return new UInt35(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt35 Divide(UInt35 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt35 operator %(UInt35 a, UInt35 b)
        {
            return new UInt35(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt35 Mod(UInt35 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt35 operator &(UInt35 a, UInt35 b)
        {
            return new UInt35(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt35 And(UInt35 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt35 operator |(UInt35 a, UInt35 b)
        {
            return new UInt35(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt35 Or(UInt35 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt35 operator ^(UInt35 a, UInt35 b)
        {
            return new UInt35(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt35 Xor(UInt35 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt35 operator ~(UInt35 value)
        {
            return new UInt35(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt35 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt35 operator >>(UInt35 value, int shift)
        {
            return new UInt35(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt35 operator <<(UInt35 value, int shift)
        {
            return new UInt35(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 36-bit integer.
    /// </summary>
    public struct UInt36 : IEquatable<UInt36>, IComparable<UInt36>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt36"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt36"/>.
        /// </summary>
        public UInt36(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt36"/>.
        /// </summary>
        public UInt36(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt36"/>.
        /// </summary>
        public UInt36(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt36"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt36 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt36"/>.
        /// </summary>
        public static implicit operator UInt36(ulong value)
        { return new UInt36(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt36 && Equals((UInt36)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt36}"/>.
        /// </summary>
        public readonly bool Equals(UInt36 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt36 a, UInt36 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt36 a, UInt36 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt36}"/>.
        /// </summary>
        public readonly int CompareTo(UInt36 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt36 operator +(UInt36 a, UInt36 b)
        {
            return new UInt36(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt36 Add(UInt36 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt36 operator -(UInt36 a, UInt36 b)
        {
            return new UInt36(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt36 Subtract(UInt36 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt36 operator -(UInt36 value)
        {
            return new UInt36(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt36 operator *(UInt36 a, UInt36 b)
        {
            return new UInt36(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt36 Multiply(UInt36 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt36 operator /(UInt36 a, UInt36 b)
        {
            return new UInt36(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt36 Divide(UInt36 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt36 operator %(UInt36 a, UInt36 b)
        {
            return new UInt36(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt36 Mod(UInt36 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt36 operator &(UInt36 a, UInt36 b)
        {
            return new UInt36(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt36 And(UInt36 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt36 operator |(UInt36 a, UInt36 b)
        {
            return new UInt36(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt36 Or(UInt36 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt36 operator ^(UInt36 a, UInt36 b)
        {
            return new UInt36(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt36 Xor(UInt36 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt36 operator ~(UInt36 value)
        {
            return new UInt36(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt36 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt36 operator >>(UInt36 value, int shift)
        {
            return new UInt36(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt36 operator <<(UInt36 value, int shift)
        {
            return new UInt36(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 37-bit integer.
    /// </summary>
    public struct UInt37 : IEquatable<UInt37>, IComparable<UInt37>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt37"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt37"/>.
        /// </summary>
        public UInt37(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt37"/>.
        /// </summary>
        public UInt37(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt37"/>.
        /// </summary>
        public UInt37(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt37"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt37 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt37"/>.
        /// </summary>
        public static implicit operator UInt37(ulong value)
        { return new UInt37(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt37 && Equals((UInt37)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt37}"/>.
        /// </summary>
        public readonly bool Equals(UInt37 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt37 a, UInt37 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt37 a, UInt37 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt37}"/>.
        /// </summary>
        public readonly int CompareTo(UInt37 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt37 operator +(UInt37 a, UInt37 b)
        {
            return new UInt37(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt37 Add(UInt37 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt37 operator -(UInt37 a, UInt37 b)
        {
            return new UInt37(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt37 Subtract(UInt37 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt37 operator -(UInt37 value)
        {
            return new UInt37(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt37 operator *(UInt37 a, UInt37 b)
        {
            return new UInt37(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt37 Multiply(UInt37 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt37 operator /(UInt37 a, UInt37 b)
        {
            return new UInt37(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt37 Divide(UInt37 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt37 operator %(UInt37 a, UInt37 b)
        {
            return new UInt37(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt37 Mod(UInt37 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt37 operator &(UInt37 a, UInt37 b)
        {
            return new UInt37(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt37 And(UInt37 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt37 operator |(UInt37 a, UInt37 b)
        {
            return new UInt37(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt37 Or(UInt37 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt37 operator ^(UInt37 a, UInt37 b)
        {
            return new UInt37(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt37 Xor(UInt37 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt37 operator ~(UInt37 value)
        {
            return new UInt37(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt37 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt37 operator >>(UInt37 value, int shift)
        {
            return new UInt37(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt37 operator <<(UInt37 value, int shift)
        {
            return new UInt37(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 38-bit integer.
    /// </summary>
    public struct UInt38 : IEquatable<UInt38>, IComparable<UInt38>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt38"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt38"/>.
        /// </summary>
        public UInt38(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt38"/>.
        /// </summary>
        public UInt38(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt38"/>.
        /// </summary>
        public UInt38(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt38"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt38 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt38"/>.
        /// </summary>
        public static implicit operator UInt38(ulong value)
        { return new UInt38(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt38 && Equals((UInt38)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt38}"/>.
        /// </summary>
        public readonly bool Equals(UInt38 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt38 a, UInt38 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt38 a, UInt38 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt38}"/>.
        /// </summary>
        public readonly int CompareTo(UInt38 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt38 operator +(UInt38 a, UInt38 b)
        {
            return new UInt38(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt38 Add(UInt38 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt38 operator -(UInt38 a, UInt38 b)
        {
            return new UInt38(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt38 Subtract(UInt38 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt38 operator -(UInt38 value)
        {
            return new UInt38(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt38 operator *(UInt38 a, UInt38 b)
        {
            return new UInt38(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt38 Multiply(UInt38 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt38 operator /(UInt38 a, UInt38 b)
        {
            return new UInt38(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt38 Divide(UInt38 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt38 operator %(UInt38 a, UInt38 b)
        {
            return new UInt38(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt38 Mod(UInt38 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt38 operator &(UInt38 a, UInt38 b)
        {
            return new UInt38(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt38 And(UInt38 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt38 operator |(UInt38 a, UInt38 b)
        {
            return new UInt38(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt38 Or(UInt38 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt38 operator ^(UInt38 a, UInt38 b)
        {
            return new UInt38(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt38 Xor(UInt38 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt38 operator ~(UInt38 value)
        {
            return new UInt38(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt38 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt38 operator >>(UInt38 value, int shift)
        {
            return new UInt38(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt38 operator <<(UInt38 value, int shift)
        {
            return new UInt38(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 39-bit integer.
    /// </summary>
    public struct UInt39 : IEquatable<UInt39>, IComparable<UInt39>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt39"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt39"/>.
        /// </summary>
        public UInt39(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt39"/>.
        /// </summary>
        public UInt39(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt39"/>.
        /// </summary>
        public UInt39(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt39"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt39 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt39"/>.
        /// </summary>
        public static implicit operator UInt39(ulong value)
        { return new UInt39(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt39 && Equals((UInt39)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt39}"/>.
        /// </summary>
        public readonly bool Equals(UInt39 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt39 a, UInt39 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt39 a, UInt39 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt39}"/>.
        /// </summary>
        public readonly int CompareTo(UInt39 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt39 operator +(UInt39 a, UInt39 b)
        {
            return new UInt39(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt39 Add(UInt39 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt39 operator -(UInt39 a, UInt39 b)
        {
            return new UInt39(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt39 Subtract(UInt39 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt39 operator -(UInt39 value)
        {
            return new UInt39(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt39 operator *(UInt39 a, UInt39 b)
        {
            return new UInt39(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt39 Multiply(UInt39 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt39 operator /(UInt39 a, UInt39 b)
        {
            return new UInt39(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt39 Divide(UInt39 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt39 operator %(UInt39 a, UInt39 b)
        {
            return new UInt39(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt39 Mod(UInt39 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt39 operator &(UInt39 a, UInt39 b)
        {
            return new UInt39(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt39 And(UInt39 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt39 operator |(UInt39 a, UInt39 b)
        {
            return new UInt39(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt39 Or(UInt39 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt39 operator ^(UInt39 a, UInt39 b)
        {
            return new UInt39(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt39 Xor(UInt39 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt39 operator ~(UInt39 value)
        {
            return new UInt39(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt39 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt39 operator >>(UInt39 value, int shift)
        {
            return new UInt39(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt39 operator <<(UInt39 value, int shift)
        {
            return new UInt39(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 40-bit integer.
    /// </summary>
    public struct UInt40 : IEquatable<UInt40>, IComparable<UInt40>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt40"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt40"/>.
        /// </summary>
        public UInt40(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt40"/>.
        /// </summary>
        public UInt40(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt40"/>.
        /// </summary>
        public UInt40(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt40"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt40 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt40"/>.
        /// </summary>
        public static implicit operator UInt40(ulong value)
        { return new UInt40(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt40 && Equals((UInt40)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt40}"/>.
        /// </summary>
        public readonly bool Equals(UInt40 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt40 a, UInt40 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt40 a, UInt40 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt40}"/>.
        /// </summary>
        public readonly int CompareTo(UInt40 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt40 operator +(UInt40 a, UInt40 b)
        {
            return new UInt40(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt40 Add(UInt40 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt40 operator -(UInt40 a, UInt40 b)
        {
            return new UInt40(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt40 Subtract(UInt40 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt40 operator -(UInt40 value)
        {
            return new UInt40(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt40 operator *(UInt40 a, UInt40 b)
        {
            return new UInt40(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt40 Multiply(UInt40 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt40 operator /(UInt40 a, UInt40 b)
        {
            return new UInt40(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt40 Divide(UInt40 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt40 operator %(UInt40 a, UInt40 b)
        {
            return new UInt40(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt40 Mod(UInt40 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt40 operator &(UInt40 a, UInt40 b)
        {
            return new UInt40(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt40 And(UInt40 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt40 operator |(UInt40 a, UInt40 b)
        {
            return new UInt40(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt40 Or(UInt40 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt40 operator ^(UInt40 a, UInt40 b)
        {
            return new UInt40(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt40 Xor(UInt40 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt40 operator ~(UInt40 value)
        {
            return new UInt40(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt40 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt40 operator >>(UInt40 value, int shift)
        {
            return new UInt40(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt40 operator <<(UInt40 value, int shift)
        {
            return new UInt40(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 41-bit integer.
    /// </summary>
    public struct UInt41 : IEquatable<UInt41>, IComparable<UInt41>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt41"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt41"/>.
        /// </summary>
        public UInt41(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt41"/>.
        /// </summary>
        public UInt41(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt41"/>.
        /// </summary>
        public UInt41(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt41"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt41 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt41"/>.
        /// </summary>
        public static implicit operator UInt41(ulong value)
        { return new UInt41(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt41 && Equals((UInt41)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt41}"/>.
        /// </summary>
        public readonly bool Equals(UInt41 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt41 a, UInt41 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt41 a, UInt41 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt41}"/>.
        /// </summary>
        public readonly int CompareTo(UInt41 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt41 operator +(UInt41 a, UInt41 b)
        {
            return new UInt41(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt41 Add(UInt41 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt41 operator -(UInt41 a, UInt41 b)
        {
            return new UInt41(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt41 Subtract(UInt41 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt41 operator -(UInt41 value)
        {
            return new UInt41(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt41 operator *(UInt41 a, UInt41 b)
        {
            return new UInt41(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt41 Multiply(UInt41 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt41 operator /(UInt41 a, UInt41 b)
        {
            return new UInt41(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt41 Divide(UInt41 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt41 operator %(UInt41 a, UInt41 b)
        {
            return new UInt41(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt41 Mod(UInt41 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt41 operator &(UInt41 a, UInt41 b)
        {
            return new UInt41(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt41 And(UInt41 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt41 operator |(UInt41 a, UInt41 b)
        {
            return new UInt41(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt41 Or(UInt41 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt41 operator ^(UInt41 a, UInt41 b)
        {
            return new UInt41(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt41 Xor(UInt41 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt41 operator ~(UInt41 value)
        {
            return new UInt41(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt41 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt41 operator >>(UInt41 value, int shift)
        {
            return new UInt41(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt41 operator <<(UInt41 value, int shift)
        {
            return new UInt41(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 42-bit integer.
    /// </summary>
    public struct UInt42 : IEquatable<UInt42>, IComparable<UInt42>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt42"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt42"/>.
        /// </summary>
        public UInt42(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt42"/>.
        /// </summary>
        public UInt42(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt42"/>.
        /// </summary>
        public UInt42(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt42"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt42 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt42"/>.
        /// </summary>
        public static implicit operator UInt42(ulong value)
        { return new UInt42(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt42 && Equals((UInt42)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt42}"/>.
        /// </summary>
        public readonly bool Equals(UInt42 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt42 a, UInt42 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt42 a, UInt42 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt42}"/>.
        /// </summary>
        public readonly int CompareTo(UInt42 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt42 operator +(UInt42 a, UInt42 b)
        {
            return new UInt42(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt42 Add(UInt42 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt42 operator -(UInt42 a, UInt42 b)
        {
            return new UInt42(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt42 Subtract(UInt42 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt42 operator -(UInt42 value)
        {
            return new UInt42(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt42 operator *(UInt42 a, UInt42 b)
        {
            return new UInt42(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt42 Multiply(UInt42 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt42 operator /(UInt42 a, UInt42 b)
        {
            return new UInt42(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt42 Divide(UInt42 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt42 operator %(UInt42 a, UInt42 b)
        {
            return new UInt42(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt42 Mod(UInt42 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt42 operator &(UInt42 a, UInt42 b)
        {
            return new UInt42(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt42 And(UInt42 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt42 operator |(UInt42 a, UInt42 b)
        {
            return new UInt42(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt42 Or(UInt42 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt42 operator ^(UInt42 a, UInt42 b)
        {
            return new UInt42(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt42 Xor(UInt42 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt42 operator ~(UInt42 value)
        {
            return new UInt42(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt42 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt42 operator >>(UInt42 value, int shift)
        {
            return new UInt42(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt42 operator <<(UInt42 value, int shift)
        {
            return new UInt42(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 43-bit integer.
    /// </summary>
    public struct UInt43 : IEquatable<UInt43>, IComparable<UInt43>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt43"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt43"/>.
        /// </summary>
        public UInt43(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt43"/>.
        /// </summary>
        public UInt43(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt43"/>.
        /// </summary>
        public UInt43(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt43"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt43 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt43"/>.
        /// </summary>
        public static implicit operator UInt43(ulong value)
        { return new UInt43(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt43 && Equals((UInt43)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt43}"/>.
        /// </summary>
        public readonly bool Equals(UInt43 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt43 a, UInt43 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt43 a, UInt43 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt43}"/>.
        /// </summary>
        public readonly int CompareTo(UInt43 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt43 operator +(UInt43 a, UInt43 b)
        {
            return new UInt43(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt43 Add(UInt43 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt43 operator -(UInt43 a, UInt43 b)
        {
            return new UInt43(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt43 Subtract(UInt43 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt43 operator -(UInt43 value)
        {
            return new UInt43(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt43 operator *(UInt43 a, UInt43 b)
        {
            return new UInt43(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt43 Multiply(UInt43 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt43 operator /(UInt43 a, UInt43 b)
        {
            return new UInt43(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt43 Divide(UInt43 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt43 operator %(UInt43 a, UInt43 b)
        {
            return new UInt43(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt43 Mod(UInt43 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt43 operator &(UInt43 a, UInt43 b)
        {
            return new UInt43(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt43 And(UInt43 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt43 operator |(UInt43 a, UInt43 b)
        {
            return new UInt43(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt43 Or(UInt43 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt43 operator ^(UInt43 a, UInt43 b)
        {
            return new UInt43(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt43 Xor(UInt43 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt43 operator ~(UInt43 value)
        {
            return new UInt43(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt43 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt43 operator >>(UInt43 value, int shift)
        {
            return new UInt43(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt43 operator <<(UInt43 value, int shift)
        {
            return new UInt43(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 44-bit integer.
    /// </summary>
    public struct UInt44 : IEquatable<UInt44>, IComparable<UInt44>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt44"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt44"/>.
        /// </summary>
        public UInt44(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt44"/>.
        /// </summary>
        public UInt44(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt44"/>.
        /// </summary>
        public UInt44(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt44"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt44 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt44"/>.
        /// </summary>
        public static implicit operator UInt44(ulong value)
        { return new UInt44(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt44 && Equals((UInt44)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt44}"/>.
        /// </summary>
        public readonly bool Equals(UInt44 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt44 a, UInt44 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt44 a, UInt44 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt44}"/>.
        /// </summary>
        public readonly int CompareTo(UInt44 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt44 operator +(UInt44 a, UInt44 b)
        {
            return new UInt44(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt44 Add(UInt44 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt44 operator -(UInt44 a, UInt44 b)
        {
            return new UInt44(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt44 Subtract(UInt44 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt44 operator -(UInt44 value)
        {
            return new UInt44(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt44 operator *(UInt44 a, UInt44 b)
        {
            return new UInt44(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt44 Multiply(UInt44 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt44 operator /(UInt44 a, UInt44 b)
        {
            return new UInt44(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt44 Divide(UInt44 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt44 operator %(UInt44 a, UInt44 b)
        {
            return new UInt44(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt44 Mod(UInt44 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt44 operator &(UInt44 a, UInt44 b)
        {
            return new UInt44(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt44 And(UInt44 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt44 operator |(UInt44 a, UInt44 b)
        {
            return new UInt44(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt44 Or(UInt44 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt44 operator ^(UInt44 a, UInt44 b)
        {
            return new UInt44(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt44 Xor(UInt44 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt44 operator ~(UInt44 value)
        {
            return new UInt44(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt44 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt44 operator >>(UInt44 value, int shift)
        {
            return new UInt44(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt44 operator <<(UInt44 value, int shift)
        {
            return new UInt44(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 45-bit integer.
    /// </summary>
    public struct UInt45 : IEquatable<UInt45>, IComparable<UInt45>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt45"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt45"/>.
        /// </summary>
        public UInt45(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt45"/>.
        /// </summary>
        public UInt45(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt45"/>.
        /// </summary>
        public UInt45(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt45"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt45 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt45"/>.
        /// </summary>
        public static implicit operator UInt45(ulong value)
        { return new UInt45(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt45 && Equals((UInt45)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt45}"/>.
        /// </summary>
        public readonly bool Equals(UInt45 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt45 a, UInt45 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt45 a, UInt45 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt45}"/>.
        /// </summary>
        public readonly int CompareTo(UInt45 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt45 operator +(UInt45 a, UInt45 b)
        {
            return new UInt45(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt45 Add(UInt45 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt45 operator -(UInt45 a, UInt45 b)
        {
            return new UInt45(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt45 Subtract(UInt45 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt45 operator -(UInt45 value)
        {
            return new UInt45(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt45 operator *(UInt45 a, UInt45 b)
        {
            return new UInt45(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt45 Multiply(UInt45 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt45 operator /(UInt45 a, UInt45 b)
        {
            return new UInt45(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt45 Divide(UInt45 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt45 operator %(UInt45 a, UInt45 b)
        {
            return new UInt45(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt45 Mod(UInt45 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt45 operator &(UInt45 a, UInt45 b)
        {
            return new UInt45(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt45 And(UInt45 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt45 operator |(UInt45 a, UInt45 b)
        {
            return new UInt45(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt45 Or(UInt45 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt45 operator ^(UInt45 a, UInt45 b)
        {
            return new UInt45(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt45 Xor(UInt45 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt45 operator ~(UInt45 value)
        {
            return new UInt45(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt45 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt45 operator >>(UInt45 value, int shift)
        {
            return new UInt45(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt45 operator <<(UInt45 value, int shift)
        {
            return new UInt45(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 46-bit integer.
    /// </summary>
    public struct UInt46 : IEquatable<UInt46>, IComparable<UInt46>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt46"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt46"/>.
        /// </summary>
        public UInt46(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt46"/>.
        /// </summary>
        public UInt46(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt46"/>.
        /// </summary>
        public UInt46(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt46"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt46 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt46"/>.
        /// </summary>
        public static implicit operator UInt46(ulong value)
        { return new UInt46(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt46 && Equals((UInt46)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt46}"/>.
        /// </summary>
        public readonly bool Equals(UInt46 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt46 a, UInt46 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt46 a, UInt46 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt46}"/>.
        /// </summary>
        public readonly int CompareTo(UInt46 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt46 operator +(UInt46 a, UInt46 b)
        {
            return new UInt46(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt46 Add(UInt46 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt46 operator -(UInt46 a, UInt46 b)
        {
            return new UInt46(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt46 Subtract(UInt46 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt46 operator -(UInt46 value)
        {
            return new UInt46(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt46 operator *(UInt46 a, UInt46 b)
        {
            return new UInt46(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt46 Multiply(UInt46 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt46 operator /(UInt46 a, UInt46 b)
        {
            return new UInt46(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt46 Divide(UInt46 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt46 operator %(UInt46 a, UInt46 b)
        {
            return new UInt46(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt46 Mod(UInt46 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt46 operator &(UInt46 a, UInt46 b)
        {
            return new UInt46(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt46 And(UInt46 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt46 operator |(UInt46 a, UInt46 b)
        {
            return new UInt46(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt46 Or(UInt46 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt46 operator ^(UInt46 a, UInt46 b)
        {
            return new UInt46(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt46 Xor(UInt46 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt46 operator ~(UInt46 value)
        {
            return new UInt46(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt46 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt46 operator >>(UInt46 value, int shift)
        {
            return new UInt46(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt46 operator <<(UInt46 value, int shift)
        {
            return new UInt46(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 47-bit integer.
    /// </summary>
    public struct UInt47 : IEquatable<UInt47>, IComparable<UInt47>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt47"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt47"/>.
        /// </summary>
        public UInt47(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt47"/>.
        /// </summary>
        public UInt47(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt47"/>.
        /// </summary>
        public UInt47(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt47"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt47 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt47"/>.
        /// </summary>
        public static implicit operator UInt47(ulong value)
        { return new UInt47(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt47 && Equals((UInt47)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt47}"/>.
        /// </summary>
        public readonly bool Equals(UInt47 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt47 a, UInt47 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt47 a, UInt47 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt47}"/>.
        /// </summary>
        public readonly int CompareTo(UInt47 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt47 operator +(UInt47 a, UInt47 b)
        {
            return new UInt47(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt47 Add(UInt47 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt47 operator -(UInt47 a, UInt47 b)
        {
            return new UInt47(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt47 Subtract(UInt47 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt47 operator -(UInt47 value)
        {
            return new UInt47(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt47 operator *(UInt47 a, UInt47 b)
        {
            return new UInt47(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt47 Multiply(UInt47 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt47 operator /(UInt47 a, UInt47 b)
        {
            return new UInt47(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt47 Divide(UInt47 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt47 operator %(UInt47 a, UInt47 b)
        {
            return new UInt47(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt47 Mod(UInt47 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt47 operator &(UInt47 a, UInt47 b)
        {
            return new UInt47(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt47 And(UInt47 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt47 operator |(UInt47 a, UInt47 b)
        {
            return new UInt47(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt47 Or(UInt47 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt47 operator ^(UInt47 a, UInt47 b)
        {
            return new UInt47(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt47 Xor(UInt47 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt47 operator ~(UInt47 value)
        {
            return new UInt47(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt47 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt47 operator >>(UInt47 value, int shift)
        {
            return new UInt47(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt47 operator <<(UInt47 value, int shift)
        {
            return new UInt47(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 48-bit integer.
    /// </summary>
    public struct UInt48 : IEquatable<UInt48>, IComparable<UInt48>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt48"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt48"/>.
        /// </summary>
        public UInt48(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt48"/>.
        /// </summary>
        public UInt48(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt48"/>.
        /// </summary>
        public UInt48(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt48"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt48 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt48"/>.
        /// </summary>
        public static implicit operator UInt48(ulong value)
        { return new UInt48(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt48 && Equals((UInt48)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt48}"/>.
        /// </summary>
        public readonly bool Equals(UInt48 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt48 a, UInt48 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt48 a, UInt48 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt48}"/>.
        /// </summary>
        public readonly int CompareTo(UInt48 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt48 operator +(UInt48 a, UInt48 b)
        {
            return new UInt48(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt48 Add(UInt48 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt48 operator -(UInt48 a, UInt48 b)
        {
            return new UInt48(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt48 Subtract(UInt48 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt48 operator -(UInt48 value)
        {
            return new UInt48(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt48 operator *(UInt48 a, UInt48 b)
        {
            return new UInt48(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt48 Multiply(UInt48 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt48 operator /(UInt48 a, UInt48 b)
        {
            return new UInt48(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt48 Divide(UInt48 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt48 operator %(UInt48 a, UInt48 b)
        {
            return new UInt48(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt48 Mod(UInt48 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt48 operator &(UInt48 a, UInt48 b)
        {
            return new UInt48(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt48 And(UInt48 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt48 operator |(UInt48 a, UInt48 b)
        {
            return new UInt48(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt48 Or(UInt48 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt48 operator ^(UInt48 a, UInt48 b)
        {
            return new UInt48(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt48 Xor(UInt48 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt48 operator ~(UInt48 value)
        {
            return new UInt48(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt48 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt48 operator >>(UInt48 value, int shift)
        {
            return new UInt48(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt48 operator <<(UInt48 value, int shift)
        {
            return new UInt48(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 49-bit integer.
    /// </summary>
    public struct UInt49 : IEquatable<UInt49>, IComparable<UInt49>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt49"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt49"/>.
        /// </summary>
        public UInt49(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt49"/>.
        /// </summary>
        public UInt49(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt49"/>.
        /// </summary>
        public UInt49(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt49"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt49 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt49"/>.
        /// </summary>
        public static implicit operator UInt49(ulong value)
        { return new UInt49(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt49 && Equals((UInt49)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt49}"/>.
        /// </summary>
        public readonly bool Equals(UInt49 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt49 a, UInt49 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt49 a, UInt49 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt49}"/>.
        /// </summary>
        public readonly int CompareTo(UInt49 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt49 operator +(UInt49 a, UInt49 b)
        {
            return new UInt49(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt49 Add(UInt49 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt49 operator -(UInt49 a, UInt49 b)
        {
            return new UInt49(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt49 Subtract(UInt49 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt49 operator -(UInt49 value)
        {
            return new UInt49(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt49 operator *(UInt49 a, UInt49 b)
        {
            return new UInt49(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt49 Multiply(UInt49 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt49 operator /(UInt49 a, UInt49 b)
        {
            return new UInt49(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt49 Divide(UInt49 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt49 operator %(UInt49 a, UInt49 b)
        {
            return new UInt49(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt49 Mod(UInt49 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt49 operator &(UInt49 a, UInt49 b)
        {
            return new UInt49(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt49 And(UInt49 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt49 operator |(UInt49 a, UInt49 b)
        {
            return new UInt49(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt49 Or(UInt49 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt49 operator ^(UInt49 a, UInt49 b)
        {
            return new UInt49(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt49 Xor(UInt49 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt49 operator ~(UInt49 value)
        {
            return new UInt49(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt49 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt49 operator >>(UInt49 value, int shift)
        {
            return new UInt49(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt49 operator <<(UInt49 value, int shift)
        {
            return new UInt49(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 50-bit integer.
    /// </summary>
    public struct UInt50 : IEquatable<UInt50>, IComparable<UInt50>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt50"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt50"/>.
        /// </summary>
        public UInt50(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt50"/>.
        /// </summary>
        public UInt50(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt50"/>.
        /// </summary>
        public UInt50(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt50"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt50 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt50"/>.
        /// </summary>
        public static implicit operator UInt50(ulong value)
        { return new UInt50(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt50 && Equals((UInt50)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt50}"/>.
        /// </summary>
        public readonly bool Equals(UInt50 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt50 a, UInt50 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt50 a, UInt50 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt50}"/>.
        /// </summary>
        public readonly int CompareTo(UInt50 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt50 operator +(UInt50 a, UInt50 b)
        {
            return new UInt50(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt50 Add(UInt50 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt50 operator -(UInt50 a, UInt50 b)
        {
            return new UInt50(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt50 Subtract(UInt50 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt50 operator -(UInt50 value)
        {
            return new UInt50(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt50 operator *(UInt50 a, UInt50 b)
        {
            return new UInt50(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt50 Multiply(UInt50 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt50 operator /(UInt50 a, UInt50 b)
        {
            return new UInt50(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt50 Divide(UInt50 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt50 operator %(UInt50 a, UInt50 b)
        {
            return new UInt50(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt50 Mod(UInt50 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt50 operator &(UInt50 a, UInt50 b)
        {
            return new UInt50(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt50 And(UInt50 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt50 operator |(UInt50 a, UInt50 b)
        {
            return new UInt50(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt50 Or(UInt50 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt50 operator ^(UInt50 a, UInt50 b)
        {
            return new UInt50(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt50 Xor(UInt50 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt50 operator ~(UInt50 value)
        {
            return new UInt50(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt50 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt50 operator >>(UInt50 value, int shift)
        {
            return new UInt50(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt50 operator <<(UInt50 value, int shift)
        {
            return new UInt50(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 51-bit integer.
    /// </summary>
    public struct UInt51 : IEquatable<UInt51>, IComparable<UInt51>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt51"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt51"/>.
        /// </summary>
        public UInt51(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt51"/>.
        /// </summary>
        public UInt51(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt51"/>.
        /// </summary>
        public UInt51(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt51"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt51 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt51"/>.
        /// </summary>
        public static implicit operator UInt51(ulong value)
        { return new UInt51(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt51 && Equals((UInt51)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt51}"/>.
        /// </summary>
        public readonly bool Equals(UInt51 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt51 a, UInt51 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt51 a, UInt51 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt51}"/>.
        /// </summary>
        public readonly int CompareTo(UInt51 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt51 operator +(UInt51 a, UInt51 b)
        {
            return new UInt51(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt51 Add(UInt51 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt51 operator -(UInt51 a, UInt51 b)
        {
            return new UInt51(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt51 Subtract(UInt51 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt51 operator -(UInt51 value)
        {
            return new UInt51(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt51 operator *(UInt51 a, UInt51 b)
        {
            return new UInt51(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt51 Multiply(UInt51 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt51 operator /(UInt51 a, UInt51 b)
        {
            return new UInt51(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt51 Divide(UInt51 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt51 operator %(UInt51 a, UInt51 b)
        {
            return new UInt51(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt51 Mod(UInt51 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt51 operator &(UInt51 a, UInt51 b)
        {
            return new UInt51(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt51 And(UInt51 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt51 operator |(UInt51 a, UInt51 b)
        {
            return new UInt51(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt51 Or(UInt51 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt51 operator ^(UInt51 a, UInt51 b)
        {
            return new UInt51(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt51 Xor(UInt51 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt51 operator ~(UInt51 value)
        {
            return new UInt51(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt51 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt51 operator >>(UInt51 value, int shift)
        {
            return new UInt51(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt51 operator <<(UInt51 value, int shift)
        {
            return new UInt51(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 52-bit integer.
    /// </summary>
    public struct UInt52 : IEquatable<UInt52>, IComparable<UInt52>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt52"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt52"/>.
        /// </summary>
        public UInt52(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt52"/>.
        /// </summary>
        public UInt52(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt52"/>.
        /// </summary>
        public UInt52(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt52"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt52 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt52"/>.
        /// </summary>
        public static implicit operator UInt52(ulong value)
        { return new UInt52(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt52 && Equals((UInt52)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt52}"/>.
        /// </summary>
        public readonly bool Equals(UInt52 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt52 a, UInt52 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt52 a, UInt52 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt52}"/>.
        /// </summary>
        public readonly int CompareTo(UInt52 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt52 operator +(UInt52 a, UInt52 b)
        {
            return new UInt52(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt52 Add(UInt52 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt52 operator -(UInt52 a, UInt52 b)
        {
            return new UInt52(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt52 Subtract(UInt52 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt52 operator -(UInt52 value)
        {
            return new UInt52(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt52 operator *(UInt52 a, UInt52 b)
        {
            return new UInt52(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt52 Multiply(UInt52 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt52 operator /(UInt52 a, UInt52 b)
        {
            return new UInt52(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt52 Divide(UInt52 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt52 operator %(UInt52 a, UInt52 b)
        {
            return new UInt52(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt52 Mod(UInt52 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt52 operator &(UInt52 a, UInt52 b)
        {
            return new UInt52(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt52 And(UInt52 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt52 operator |(UInt52 a, UInt52 b)
        {
            return new UInt52(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt52 Or(UInt52 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt52 operator ^(UInt52 a, UInt52 b)
        {
            return new UInt52(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt52 Xor(UInt52 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt52 operator ~(UInt52 value)
        {
            return new UInt52(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt52 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt52 operator >>(UInt52 value, int shift)
        {
            return new UInt52(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt52 operator <<(UInt52 value, int shift)
        {
            return new UInt52(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 53-bit integer.
    /// </summary>
    public struct UInt53 : IEquatable<UInt53>, IComparable<UInt53>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt53"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt53"/>.
        /// </summary>
        public UInt53(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt53"/>.
        /// </summary>
        public UInt53(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt53"/>.
        /// </summary>
        public UInt53(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt53"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt53 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt53"/>.
        /// </summary>
        public static implicit operator UInt53(ulong value)
        { return new UInt53(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt53 && Equals((UInt53)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt53}"/>.
        /// </summary>
        public readonly bool Equals(UInt53 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt53 a, UInt53 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt53 a, UInt53 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt53}"/>.
        /// </summary>
        public readonly int CompareTo(UInt53 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt53 operator +(UInt53 a, UInt53 b)
        {
            return new UInt53(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt53 Add(UInt53 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt53 operator -(UInt53 a, UInt53 b)
        {
            return new UInt53(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt53 Subtract(UInt53 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt53 operator -(UInt53 value)
        {
            return new UInt53(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt53 operator *(UInt53 a, UInt53 b)
        {
            return new UInt53(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt53 Multiply(UInt53 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt53 operator /(UInt53 a, UInt53 b)
        {
            return new UInt53(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt53 Divide(UInt53 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt53 operator %(UInt53 a, UInt53 b)
        {
            return new UInt53(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt53 Mod(UInt53 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt53 operator &(UInt53 a, UInt53 b)
        {
            return new UInt53(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt53 And(UInt53 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt53 operator |(UInt53 a, UInt53 b)
        {
            return new UInt53(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt53 Or(UInt53 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt53 operator ^(UInt53 a, UInt53 b)
        {
            return new UInt53(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt53 Xor(UInt53 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt53 operator ~(UInt53 value)
        {
            return new UInt53(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt53 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt53 operator >>(UInt53 value, int shift)
        {
            return new UInt53(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt53 operator <<(UInt53 value, int shift)
        {
            return new UInt53(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 54-bit integer.
    /// </summary>
    public struct UInt54 : IEquatable<UInt54>, IComparable<UInt54>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt54"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt54"/>.
        /// </summary>
        public UInt54(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt54"/>.
        /// </summary>
        public UInt54(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt54"/>.
        /// </summary>
        public UInt54(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt54"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt54 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt54"/>.
        /// </summary>
        public static implicit operator UInt54(ulong value)
        { return new UInt54(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt54 && Equals((UInt54)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt54}"/>.
        /// </summary>
        public readonly bool Equals(UInt54 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt54 a, UInt54 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt54 a, UInt54 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt54}"/>.
        /// </summary>
        public readonly int CompareTo(UInt54 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt54 operator +(UInt54 a, UInt54 b)
        {
            return new UInt54(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt54 Add(UInt54 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt54 operator -(UInt54 a, UInt54 b)
        {
            return new UInt54(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt54 Subtract(UInt54 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt54 operator -(UInt54 value)
        {
            return new UInt54(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt54 operator *(UInt54 a, UInt54 b)
        {
            return new UInt54(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt54 Multiply(UInt54 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt54 operator /(UInt54 a, UInt54 b)
        {
            return new UInt54(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt54 Divide(UInt54 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt54 operator %(UInt54 a, UInt54 b)
        {
            return new UInt54(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt54 Mod(UInt54 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt54 operator &(UInt54 a, UInt54 b)
        {
            return new UInt54(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt54 And(UInt54 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt54 operator |(UInt54 a, UInt54 b)
        {
            return new UInt54(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt54 Or(UInt54 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt54 operator ^(UInt54 a, UInt54 b)
        {
            return new UInt54(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt54 Xor(UInt54 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt54 operator ~(UInt54 value)
        {
            return new UInt54(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt54 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt54 operator >>(UInt54 value, int shift)
        {
            return new UInt54(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt54 operator <<(UInt54 value, int shift)
        {
            return new UInt54(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 55-bit integer.
    /// </summary>
    public struct UInt55 : IEquatable<UInt55>, IComparable<UInt55>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt55"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt55"/>.
        /// </summary>
        public UInt55(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt55"/>.
        /// </summary>
        public UInt55(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt55"/>.
        /// </summary>
        public UInt55(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt55"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt55 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt55"/>.
        /// </summary>
        public static implicit operator UInt55(ulong value)
        { return new UInt55(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt55 && Equals((UInt55)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt55}"/>.
        /// </summary>
        public readonly bool Equals(UInt55 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt55 a, UInt55 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt55 a, UInt55 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt55}"/>.
        /// </summary>
        public readonly int CompareTo(UInt55 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt55 operator +(UInt55 a, UInt55 b)
        {
            return new UInt55(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt55 Add(UInt55 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt55 operator -(UInt55 a, UInt55 b)
        {
            return new UInt55(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt55 Subtract(UInt55 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt55 operator -(UInt55 value)
        {
            return new UInt55(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt55 operator *(UInt55 a, UInt55 b)
        {
            return new UInt55(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt55 Multiply(UInt55 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt55 operator /(UInt55 a, UInt55 b)
        {
            return new UInt55(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt55 Divide(UInt55 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt55 operator %(UInt55 a, UInt55 b)
        {
            return new UInt55(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt55 Mod(UInt55 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt55 operator &(UInt55 a, UInt55 b)
        {
            return new UInt55(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt55 And(UInt55 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt55 operator |(UInt55 a, UInt55 b)
        {
            return new UInt55(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt55 Or(UInt55 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt55 operator ^(UInt55 a, UInt55 b)
        {
            return new UInt55(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt55 Xor(UInt55 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt55 operator ~(UInt55 value)
        {
            return new UInt55(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt55 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt55 operator >>(UInt55 value, int shift)
        {
            return new UInt55(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt55 operator <<(UInt55 value, int shift)
        {
            return new UInt55(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 56-bit integer.
    /// </summary>
    public struct UInt56 : IEquatable<UInt56>, IComparable<UInt56>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt56"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt56"/>.
        /// </summary>
        public UInt56(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt56"/>.
        /// </summary>
        public UInt56(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt56"/>.
        /// </summary>
        public UInt56(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt56"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt56 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt56"/>.
        /// </summary>
        public static implicit operator UInt56(ulong value)
        { return new UInt56(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt56 && Equals((UInt56)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt56}"/>.
        /// </summary>
        public readonly bool Equals(UInt56 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt56 a, UInt56 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt56 a, UInt56 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt56}"/>.
        /// </summary>
        public readonly int CompareTo(UInt56 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt56 operator +(UInt56 a, UInt56 b)
        {
            return new UInt56(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt56 Add(UInt56 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt56 operator -(UInt56 a, UInt56 b)
        {
            return new UInt56(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt56 Subtract(UInt56 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt56 operator -(UInt56 value)
        {
            return new UInt56(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt56 operator *(UInt56 a, UInt56 b)
        {
            return new UInt56(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt56 Multiply(UInt56 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt56 operator /(UInt56 a, UInt56 b)
        {
            return new UInt56(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt56 Divide(UInt56 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt56 operator %(UInt56 a, UInt56 b)
        {
            return new UInt56(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt56 Mod(UInt56 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt56 operator &(UInt56 a, UInt56 b)
        {
            return new UInt56(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt56 And(UInt56 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt56 operator |(UInt56 a, UInt56 b)
        {
            return new UInt56(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt56 Or(UInt56 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt56 operator ^(UInt56 a, UInt56 b)
        {
            return new UInt56(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt56 Xor(UInt56 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt56 operator ~(UInt56 value)
        {
            return new UInt56(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt56 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt56 operator >>(UInt56 value, int shift)
        {
            return new UInt56(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt56 operator <<(UInt56 value, int shift)
        {
            return new UInt56(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 57-bit integer.
    /// </summary>
    public struct UInt57 : IEquatable<UInt57>, IComparable<UInt57>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt57"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt57"/>.
        /// </summary>
        public UInt57(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt57"/>.
        /// </summary>
        public UInt57(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt57"/>.
        /// </summary>
        public UInt57(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt57"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt57 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt57"/>.
        /// </summary>
        public static implicit operator UInt57(ulong value)
        { return new UInt57(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt57 && Equals((UInt57)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt57}"/>.
        /// </summary>
        public readonly bool Equals(UInt57 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt57 a, UInt57 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt57 a, UInt57 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt57}"/>.
        /// </summary>
        public readonly int CompareTo(UInt57 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt57 operator +(UInt57 a, UInt57 b)
        {
            return new UInt57(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt57 Add(UInt57 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt57 operator -(UInt57 a, UInt57 b)
        {
            return new UInt57(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt57 Subtract(UInt57 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt57 operator -(UInt57 value)
        {
            return new UInt57(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt57 operator *(UInt57 a, UInt57 b)
        {
            return new UInt57(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt57 Multiply(UInt57 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt57 operator /(UInt57 a, UInt57 b)
        {
            return new UInt57(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt57 Divide(UInt57 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt57 operator %(UInt57 a, UInt57 b)
        {
            return new UInt57(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt57 Mod(UInt57 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt57 operator &(UInt57 a, UInt57 b)
        {
            return new UInt57(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt57 And(UInt57 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt57 operator |(UInt57 a, UInt57 b)
        {
            return new UInt57(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt57 Or(UInt57 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt57 operator ^(UInt57 a, UInt57 b)
        {
            return new UInt57(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt57 Xor(UInt57 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt57 operator ~(UInt57 value)
        {
            return new UInt57(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt57 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt57 operator >>(UInt57 value, int shift)
        {
            return new UInt57(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt57 operator <<(UInt57 value, int shift)
        {
            return new UInt57(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 58-bit integer.
    /// </summary>
    public struct UInt58 : IEquatable<UInt58>, IComparable<UInt58>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt58"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt58"/>.
        /// </summary>
        public UInt58(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt58"/>.
        /// </summary>
        public UInt58(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt58"/>.
        /// </summary>
        public UInt58(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt58"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt58 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt58"/>.
        /// </summary>
        public static implicit operator UInt58(ulong value)
        { return new UInt58(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt58 && Equals((UInt58)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt58}"/>.
        /// </summary>
        public readonly bool Equals(UInt58 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt58 a, UInt58 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt58 a, UInt58 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt58}"/>.
        /// </summary>
        public readonly int CompareTo(UInt58 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt58 operator +(UInt58 a, UInt58 b)
        {
            return new UInt58(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt58 Add(UInt58 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt58 operator -(UInt58 a, UInt58 b)
        {
            return new UInt58(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt58 Subtract(UInt58 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt58 operator -(UInt58 value)
        {
            return new UInt58(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt58 operator *(UInt58 a, UInt58 b)
        {
            return new UInt58(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt58 Multiply(UInt58 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt58 operator /(UInt58 a, UInt58 b)
        {
            return new UInt58(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt58 Divide(UInt58 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt58 operator %(UInt58 a, UInt58 b)
        {
            return new UInt58(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt58 Mod(UInt58 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt58 operator &(UInt58 a, UInt58 b)
        {
            return new UInt58(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt58 And(UInt58 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt58 operator |(UInt58 a, UInt58 b)
        {
            return new UInt58(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt58 Or(UInt58 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt58 operator ^(UInt58 a, UInt58 b)
        {
            return new UInt58(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt58 Xor(UInt58 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt58 operator ~(UInt58 value)
        {
            return new UInt58(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt58 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt58 operator >>(UInt58 value, int shift)
        {
            return new UInt58(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt58 operator <<(UInt58 value, int shift)
        {
            return new UInt58(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 59-bit integer.
    /// </summary>
    public struct UInt59 : IEquatable<UInt59>, IComparable<UInt59>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt59"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt59"/>.
        /// </summary>
        public UInt59(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt59"/>.
        /// </summary>
        public UInt59(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt59"/>.
        /// </summary>
        public UInt59(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt59"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt59 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt59"/>.
        /// </summary>
        public static implicit operator UInt59(ulong value)
        { return new UInt59(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt59 && Equals((UInt59)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt59}"/>.
        /// </summary>
        public readonly bool Equals(UInt59 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt59 a, UInt59 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt59 a, UInt59 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt59}"/>.
        /// </summary>
        public readonly int CompareTo(UInt59 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt59 operator +(UInt59 a, UInt59 b)
        {
            return new UInt59(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt59 Add(UInt59 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt59 operator -(UInt59 a, UInt59 b)
        {
            return new UInt59(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt59 Subtract(UInt59 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt59 operator -(UInt59 value)
        {
            return new UInt59(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt59 operator *(UInt59 a, UInt59 b)
        {
            return new UInt59(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt59 Multiply(UInt59 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt59 operator /(UInt59 a, UInt59 b)
        {
            return new UInt59(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt59 Divide(UInt59 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt59 operator %(UInt59 a, UInt59 b)
        {
            return new UInt59(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt59 Mod(UInt59 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt59 operator &(UInt59 a, UInt59 b)
        {
            return new UInt59(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt59 And(UInt59 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt59 operator |(UInt59 a, UInt59 b)
        {
            return new UInt59(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt59 Or(UInt59 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt59 operator ^(UInt59 a, UInt59 b)
        {
            return new UInt59(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt59 Xor(UInt59 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt59 operator ~(UInt59 value)
        {
            return new UInt59(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt59 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt59 operator >>(UInt59 value, int shift)
        {
            return new UInt59(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt59 operator <<(UInt59 value, int shift)
        {
            return new UInt59(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 60-bit integer.
    /// </summary>
    public struct UInt60 : IEquatable<UInt60>, IComparable<UInt60>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt60"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt60"/>.
        /// </summary>
        public UInt60(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt60"/>.
        /// </summary>
        public UInt60(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt60"/>.
        /// </summary>
        public UInt60(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt60"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt60 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt60"/>.
        /// </summary>
        public static implicit operator UInt60(ulong value)
        { return new UInt60(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt60 && Equals((UInt60)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt60}"/>.
        /// </summary>
        public readonly bool Equals(UInt60 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt60 a, UInt60 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt60 a, UInt60 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt60}"/>.
        /// </summary>
        public readonly int CompareTo(UInt60 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt60 operator +(UInt60 a, UInt60 b)
        {
            return new UInt60(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt60 Add(UInt60 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt60 operator -(UInt60 a, UInt60 b)
        {
            return new UInt60(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt60 Subtract(UInt60 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt60 operator -(UInt60 value)
        {
            return new UInt60(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt60 operator *(UInt60 a, UInt60 b)
        {
            return new UInt60(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt60 Multiply(UInt60 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt60 operator /(UInt60 a, UInt60 b)
        {
            return new UInt60(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt60 Divide(UInt60 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt60 operator %(UInt60 a, UInt60 b)
        {
            return new UInt60(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt60 Mod(UInt60 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt60 operator &(UInt60 a, UInt60 b)
        {
            return new UInt60(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt60 And(UInt60 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt60 operator |(UInt60 a, UInt60 b)
        {
            return new UInt60(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt60 Or(UInt60 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt60 operator ^(UInt60 a, UInt60 b)
        {
            return new UInt60(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt60 Xor(UInt60 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt60 operator ~(UInt60 value)
        {
            return new UInt60(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt60 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt60 operator >>(UInt60 value, int shift)
        {
            return new UInt60(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt60 operator <<(UInt60 value, int shift)
        {
            return new UInt60(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 61-bit integer.
    /// </summary>
    public struct UInt61 : IEquatable<UInt61>, IComparable<UInt61>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt61"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt61"/>.
        /// </summary>
        public UInt61(ulong value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt61"/>.
        /// </summary>
        public UInt61(int value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt61"/>.
        /// </summary>
        public UInt61(long value)
        {
            Value = (ulong)(value & 0b1111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt61"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt61 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt61"/>.
        /// </summary>
        public static implicit operator UInt61(ulong value)
        { return new UInt61(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt61 && Equals((UInt61)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt61}"/>.
        /// </summary>
        public readonly bool Equals(UInt61 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt61 a, UInt61 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt61 a, UInt61 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt61}"/>.
        /// </summary>
        public readonly int CompareTo(UInt61 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt61 operator +(UInt61 a, UInt61 b)
        {
            return new UInt61(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt61 Add(UInt61 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt61 operator -(UInt61 a, UInt61 b)
        {
            return new UInt61(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt61 Subtract(UInt61 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt61 operator -(UInt61 value)
        {
            return new UInt61(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt61 operator *(UInt61 a, UInt61 b)
        {
            return new UInt61(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt61 Multiply(UInt61 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt61 operator /(UInt61 a, UInt61 b)
        {
            return new UInt61(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt61 Divide(UInt61 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt61 operator %(UInt61 a, UInt61 b)
        {
            return new UInt61(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt61 Mod(UInt61 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt61 operator &(UInt61 a, UInt61 b)
        {
            return new UInt61(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt61 And(UInt61 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt61 operator |(UInt61 a, UInt61 b)
        {
            return new UInt61(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt61 Or(UInt61 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt61 operator ^(UInt61 a, UInt61 b)
        {
            return new UInt61(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt61 Xor(UInt61 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt61 operator ~(UInt61 value)
        {
            return new UInt61(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt61 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt61 operator >>(UInt61 value, int shift)
        {
            return new UInt61(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt61 operator <<(UInt61 value, int shift)
        {
            return new UInt61(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 62-bit integer.
    /// </summary>
    public struct UInt62 : IEquatable<UInt62>, IComparable<UInt62>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt62"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt62"/>.
        /// </summary>
        public UInt62(ulong value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt62"/>.
        /// </summary>
        public UInt62(int value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt62"/>.
        /// </summary>
        public UInt62(long value)
        {
            Value = (ulong)(value & 0b11111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt62"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt62 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt62"/>.
        /// </summary>
        public static implicit operator UInt62(ulong value)
        { return new UInt62(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt62 && Equals((UInt62)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt62}"/>.
        /// </summary>
        public readonly bool Equals(UInt62 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt62 a, UInt62 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt62 a, UInt62 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt62}"/>.
        /// </summary>
        public readonly int CompareTo(UInt62 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt62 operator +(UInt62 a, UInt62 b)
        {
            return new UInt62(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt62 Add(UInt62 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt62 operator -(UInt62 a, UInt62 b)
        {
            return new UInt62(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt62 Subtract(UInt62 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt62 operator -(UInt62 value)
        {
            return new UInt62(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt62 operator *(UInt62 a, UInt62 b)
        {
            return new UInt62(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt62 Multiply(UInt62 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt62 operator /(UInt62 a, UInt62 b)
        {
            return new UInt62(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt62 Divide(UInt62 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt62 operator %(UInt62 a, UInt62 b)
        {
            return new UInt62(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt62 Mod(UInt62 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt62 operator &(UInt62 a, UInt62 b)
        {
            return new UInt62(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt62 And(UInt62 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt62 operator |(UInt62 a, UInt62 b)
        {
            return new UInt62(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt62 Or(UInt62 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt62 operator ^(UInt62 a, UInt62 b)
        {
            return new UInt62(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt62 Xor(UInt62 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt62 operator ~(UInt62 value)
        {
            return new UInt62(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt62 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt62 operator >>(UInt62 value, int shift)
        {
            return new UInt62(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt62 operator <<(UInt62 value, int shift)
        {
            return new UInt62(value.Value << shift);
        }
    }

    /// <summary>
    /// Unsigned 63-bit integer.
    /// </summary>
    public struct UInt63 : IEquatable<UInt63>, IComparable<UInt63>
    {
        /// <summary>
        /// The numeric value of the <see cref="UInt63"/>.
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// Creates a <see cref="UInt63"/>.
        /// </summary>
        public UInt63(ulong value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt63"/>.
        /// </summary>
        public UInt63(int value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Creates a <see cref="UInt63"/>.
        /// </summary>
        public UInt63(long value)
        {
            Value = (ulong)(value & 0b111111111111111111111111111111111111111111111111111111111111111);
        }

        /// <summary>
        /// Explicitly converts a <see cref="UInt63"/> to a <see cref="System.Int32"/>.
        /// </summary>
        public static explicit operator ulong(UInt63 value)
        { return value.Value; }

        /// <summary>
        /// Implicitly converts a <see cref="System.Int32"/> to a <see cref="UInt63"/>.
        /// </summary>
        public static implicit operator UInt63(ulong value)
        { return new UInt63(value); }

        /// <summary>
        /// Implementation of <see cref="object.ToString"/>.
        /// </summary>
        public override readonly string ToString()
        { return Value.ToString(); }
        
        /// <summary>
        /// Implementation of <see cref="object.GetHashCode"/>.
        /// </summary>
        public override readonly int GetHashCode()
        { return Value.GetHashCode(); }

        /// <summary>
        /// Implementation of <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        { return obj is UInt63 && Equals((UInt63)obj); }

        /// <summary>
        /// Implementation of <see cref="IEquatable{UInt63}"/>.
        /// </summary>
        public readonly bool Equals(UInt63 other)
        { return Value == other.Value; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(UInt63 a, UInt63 b)
        { return a.Equals(b); }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(UInt63 a, UInt63 b)
        { return !a.Equals(b); }

        /// <summary>
        /// Implementation of <see cref="IComparable{UInt63}"/>.
        /// </summary>
        public readonly int CompareTo(UInt63 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        public static UInt63 operator +(UInt63 a, UInt63 b)
        {
            return new UInt63(a.Value + b.Value);
        }

        /// <summary>
        /// Add.
        /// </summary>
        public readonly UInt63 Add(UInt63 other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtraction operator.
        /// </summary>
        public static UInt63 operator -(UInt63 a, UInt63 b)
        {
            return new UInt63(a.Value - b.Value);
        }

        /// <summary>
        /// Subtract.
        /// </summary>
        public readonly UInt63 Subtract(UInt63 other)
        {
            return this - other;
        }

        /// <summary>
        /// Unary negation operator.
        /// </summary>
        public static UInt63 operator -(UInt63 value)
        {
            return new UInt63(0 - value.Value);
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        public static UInt63 operator *(UInt63 a, UInt63 b)
        {
            return new UInt63(a.Value * b.Value);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        public readonly UInt63 Multiply(UInt63 other)
        {
            return this * other;
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        public static UInt63 operator /(UInt63 a, UInt63 b)
        {
            return new UInt63(a.Value / b.Value);
        }

        /// <summary>
        /// Divide.
        /// </summary>
        public readonly UInt63 Divide(UInt63 other)
        {
            return this / other;
        }

        /// <summary>
        /// Modulo operator.
        /// </summary>
        public static UInt63 operator %(UInt63 a, UInt63 b)
        {
            return new UInt63(a.Value % b.Value);
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public readonly UInt63 Mod(UInt63 other)
        {
            return this % other;
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public static UInt63 operator &(UInt63 a, UInt63 b)
        {
            return new UInt63(a.Value & b.Value);
        }

        /// <summary>
        /// Bitwise AND.
        /// </summary>
        public readonly UInt63 And(UInt63 other)
        {
            return this & other;
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public static UInt63 operator |(UInt63 a, UInt63 b)
        {
            return new UInt63(a.Value | b.Value);
        }

        /// <summary>
        /// Bitwise OR.
        /// </summary>
        public readonly UInt63 Or(UInt63 other)
        {
            return this | other;
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public static UInt63 operator ^(UInt63 a, UInt63 b)
        {
            return new UInt63(a.Value ^ b.Value);
        }

        /// <summary>
        /// Bitwise XOR.
        /// </summary>
        public readonly UInt63 Xor(UInt63 other)
        {
            return this ^ other;
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public static UInt63 operator ~(UInt63 value)
        {
            return new UInt63(~value.Value);
        }

        /// <summary>
        /// Bitwise NOT.
        /// </summary>
        public readonly UInt63 Not()
        {
            return ~this;
        }

        /// <summary>
        /// Right-shift operator.
        /// </summary>
        public static UInt63 operator >>(UInt63 value, int shift)
        {
            return new UInt63(value.Value >> shift);
        }

        /// <summary>
        /// Left-shift operator.
        /// </summary>
        public static UInt63 operator <<(UInt63 value, int shift)
        {
            return new UInt63(value.Value << shift);
        }
    }

}
