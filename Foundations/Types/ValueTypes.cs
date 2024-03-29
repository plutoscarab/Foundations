﻿
namespace Foundations.Types
{
    /// <summary>
    /// An integer power of a factor.
    /// </summary>
	public readonly partial struct Factor<T>(T value, int exponent) : IEquatable<Factor<T>>
	{
		/// <summary>The factor.</summary>
		public readonly T Value = value;

		/// <summary>Exponent of the factor.</summary>
		public readonly int Exponent = exponent;


		/// <summary>Implementation of <see	cref="object.GetHashCode"/>.</summary>
		public override int GetHashCode()
		{
            return HashCode.Combine(Value, Exponent);
		}

		/// <summary>Implementation of <see cref="object.Equals(object)"/>.</summary>
		public override bool Equals(object obj)
		{
			return obj is Factor<T> other && Equals(other);
		}

		/// <summary>Implementation of <see cref="IEquatable{U}.Equals"/>.</summary>
		public bool Equals(Factor<T> other)
		{
			if (!Value.Equals(other.Value)) return false;
			if (!Exponent.Equals(other.Exponent)) return false;
			return true;
		}

		/// <summary>Equality operator.</summary>
		public static bool operator ==(Factor<T> a, Factor<T> b)
		{
			return a.Equals(b);
		}

		/// <summary>Inequality operator.</summary>
		public static bool operator !=(Factor<T> a, Factor<T> b)
		{
			return !a.Equals(b);
		}
	}


	public readonly partial struct SmallDegreePolyGF2(ulong coefficients) : IEquatable<SmallDegreePolyGF2>
	{
		/// <summary>The coefficients of the polynomial.</summary>
		public readonly ulong Coefficients = coefficients;


		/// <summary>Implementation of <see	cref="object.GetHashCode"/>.</summary>
		public override int GetHashCode()
		{
            return HashCode.Combine(Coefficients);
		}

		/// <summary>Implementation of <see cref="object.Equals(object)"/>.</summary>
		public override bool Equals(object obj)
		{
			return obj is SmallDegreePolyGF2 other && Equals(other);
		}

		/// <summary>Implementation of <see cref="IEquatable{U}.Equals"/>.</summary>
		public bool Equals(SmallDegreePolyGF2 other)
		{
			if (!Coefficients.Equals(other.Coefficients)) return false;
			return true;
		}

		/// <summary>Equality operator.</summary>
		public static bool operator ==(SmallDegreePolyGF2 a, SmallDegreePolyGF2 b)
		{
			return a.Equals(b);
		}

		/// <summary>Inequality operator.</summary>
		public static bool operator !=(SmallDegreePolyGF2 a, SmallDegreePolyGF2 b)
		{
			return !a.Equals(b);
		}
	}

    /// <summary>
    /// Helper functions for creating fast, high-quality GetHashCode implementations.
    /// </summary>
	public static class HashHelper
	{
        /// <summary>
        /// Mix a field's hash code into the containing object's hash code
        /// using h = HashHelper.Mixer(h + field.GetHashCode()) for each
        /// field sequentially.
        /// </summary>
		public static int Mixer(int h)
        {
            return (int)(((uint)h ^ ((uint)h >> 17)) * 0xC98F358D);

            // One pass through Mixer followed by one pass through Finisher
            // results in essentially 100% 1-bit avalanche as far as I can tell
            // by statistical testing. There are other values of shift and
            // multiply that do this. Some other choices:
            //
            //                                Longest   Fixed
            // Shift  Multiplier   Weight       Cycle  Points
            //    17  0xC98F358D  17 bits  0xFFFE8861       3  long cycle (this)
            //    16  0xB94C9657  17 bits  0xFF9701A2       1  no addl fixed points
            //    14  0x08112B29  10 bits  0x9145F4FE       1  low weight, no addl fixed
            //    14  0xF5F6ED4D  22 bits  0xFC34BD59       2  high weight, long cycle
            //    16  0xFADDD2EB  22 bits  0x84B68C7B       1  high weight, no addl fixed
            //    16  0x0115A68D  12 bits  0xFB5C8D7D       2  small multiplier
            //    18  0xAAAAAAA5  16 bits  0xC368EA65       1  nice! and inverse is 2D2D2D2D
            //    16  333539595   14 bits  0x6A4B397C       1  inverse is 533535395
            //    17  0x33D326E5  17 bits  0x2AF1D209       3  inverse 0x33D326ED (one bit off!)
            //    16  3333333733  17 bits  0x69416DC8       1  nice decimal pattern
        }

        /// <summary>
        /// Mix a non-null IEnumerable field's hash code into the containing 
        /// object's hash code using h = HashHelper.Mixer(h, field), using
        /// Mixer for each field sequentially.
        /// </summary>
        public static int Mixer<T>(int h, IEnumerable<T> items)
        {
            foreach (var item in items) h = Mixer(h + item.GetHashCode());
            return h;
        }

        /// <summary>
        /// Mix a non-null array field's hash code into the containing 
        /// object's hash code using h = HashHelper.Mixer(h, array), using
        /// Mixer for each field sequentially. Can also be used to
        /// mix in several fields of the same type, e.g. 
        /// h = HashHelper.Mixer(h, field1, field2, field3).
        /// </summary>
        public static int Mixer<T>(int h, params T[] items)
        {
            foreach (var item in items) h = Mixer(h + item.GetHashCode());
            return h;
        }

        /// <summary>
        /// After mixing in each field, use the Finisher to do a final mix
        /// to produce the final result.
        /// </summary>
        public static int Finisher(int h)
        {
            h = Mixer(h);
            return (int)((uint)h ^ ((uint)h >> 17));
        }
	}
}
