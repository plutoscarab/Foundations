
/*
ContinuedFraction.cs

*/

using System.Globalization;
using Foundations.RandomNumbers;

using FiniteTerms = System.Collections.Generic.IList<System.Numerics.BigInteger>;
using Terms = System.Collections.Generic.IEnumerable<System.Numerics.BigInteger>;

namespace Foundations.Types
{
	/// <summary>
	/// Continued fraction representation of a rational number. Uses eager evaluation, so irrationals must be truncated after
	/// a certain number of terms, otherwise certain operations are not computable such as equality of values and the
	/// terms resulting from operations on irrationals that produce a rational.
	/// </summary>
	public class ContinuedFraction : Terms, IComparable<ContinuedFraction>, IEquatable<ContinuedFraction>
	{
		/// <summary>
		/// The constant value 0.
		/// </summary>
		public static readonly ContinuedFraction Zero = new(0);

		/// <summary>
		/// The constant value 1.
		/// </summary>
		public static readonly ContinuedFraction One = new(1);

		/// <summary>
		/// The constant value -1.
		/// </summary>
		public static readonly ContinuedFraction MinusOne = new(-1);

		/// <summary>
		/// The constant value 2.
		/// </summary>
		public static readonly ContinuedFraction Two = new(2);

		/// <summary>
		/// The constant value 1/2.
		/// </summary>
		public static readonly ContinuedFraction OneHalf = new([0, 2]);

		/// <summary>
		/// The constant value infinity.
		/// </summary>
		public static readonly ContinuedFraction Infinity = new(Array.Empty<BigInteger>());

		/// <summary>
		/// The irrational constant e.
		/// </summary>
		/// <param name="terms">The number of terms to include in the approximation.</param>
		/// <returns>The approximate value of e.</returns>
		public static ContinuedFraction E(int terms)
		{
			return new ContinuedFraction(YieldE(), terms);
		}

		/// <summary>
		/// The irrational constant pi.
		/// </summary>
		/// <param name="terms">The number of terms to include in the approximation.</param>
		/// <returns>The approximate value of pi.</returns>
		public static ContinuedFraction Pi(int terms)
		{
			return new ContinuedFraction(
				YieldRegularize(
					Term(0).Concat(Odds()),
					Term(4).Concat(Squares())),
				terms);
		}

		/// <summary>
		/// The natural logarithm of <paramref name="n"/>.
		/// </summary>
		public static ContinuedFraction Log(BigInteger n, int terms)
		{
			return new ContinuedFraction(YieldLog(n), terms);
		}

		private readonly FiniteTerms terms;


		/// <summary>
		/// Returns a System.String that represents the current ContinuedFraction.
		/// </summary>
		public override string ToString()
		{
			var s = new StringBuilder("[");
			int n = 0;

			foreach (var term in this.terms)
			{
				switch (n)
				{
					case 0:
						break;

					case 1:
						s.Append("; ");
						break;

					default:
						s.Append(", ");
						break;

					case 100:
						s.Append(", ...");
						goto bail;
				}

				s.Append(term);
				n++;
			}

		bail:
			s.Append(']');
			return s.ToString();
		}

		/// <summary>
		/// Construct a ContinuedFraction from an IList of BigInteger terms.
		/// </summary>
		public ContinuedFraction(FiniteTerms b)
		{
			terms = b;
		}

		/// <summary>
		/// Construct a ContinuedFraction from an IEnumerable of BigInteger terms, truncated after a maximum number of terms.
		/// If term after the final term would have been "1", the final term is incremented.
		/// </summary>
		/// <param name="b"></param>
		/// <param name="maxTerms"></param>
		public ContinuedFraction(Terms b, int maxTerms)
			: this(YieldApproximate(b, maxTerms))
		{
		}

        /// <summary>
        /// Construct a <see cref="ContinuedFraction"/> from IEnumerables representing the numerators and denominators
        /// of a generalized continued fraction.
        /// </summary>
		public ContinuedFraction(Terms a, Terms b, int maxTerms)
			: this(YieldRegularize(a, b), maxTerms)
		{
		}

		/// <summary>
		/// Construct a degenerate continued fraction from a BigInteger.
		/// </summary>
		public ContinuedFraction(BigInteger b)
			: this(Term(b))
		{
		}

		/// <summary>
		/// Convert a BigInteger to a degenerate ContinuedFraction.
		/// </summary>
		public static implicit operator ContinuedFraction(BigInteger b)
		{
			return new ContinuedFraction(b);
		}

		/// <summary>
		/// Construct the continued fraction representation of a ratio of two BigInteger values.
		/// The result is exact.
		/// </summary>
		public ContinuedFraction(BigInteger numerator, BigInteger denominator)
			: this(YieldRatio(numerator, denominator).ToList())
		{
		}

        /// <summary>
        /// Construct the continued fraction representation of a ratio of two BigInteger values.
        /// The result is exact.
        /// </summary>
        public ContinuedFraction(Rational r)
            : this(r.P, r.Q)
        {
        }

        /// <summary>
        /// Convert a Rational to a ContinuedFraction. The result is exact.
        /// </summary>
        public static implicit operator ContinuedFraction(Rational r)
        {
            return new ContinuedFraction(r);
        }

		/// <summary>
		/// Construct a continued fraction from a System.Decimal. The result is exact.
		/// </summary>
		public ContinuedFraction(decimal d)
		{
			terms = YieldDecimal(d).ToList();
		}

		/// <summary>
		/// Convert a System.Decimal to a ContinuedFraction. The result is exact.
		/// </summary>
		public static implicit operator ContinuedFraction(decimal d)
		{
			return new ContinuedFraction(d);
		}

		/// <summary>
		/// Convert a ContinuedFraction to a System.Decimal. The result is not guaranteed
		/// to be exact or within valid range.
		/// </summary>
		public static explicit operator decimal(ContinuedFraction cf)
		{
			return decimal.Parse(cf.ToDigits(30));
		}

		/// <summary>
		/// Construct a continued fraction from a System.Double. The result is exact.
		/// </summary>
		public ContinuedFraction(double d)
		{
			if (double.IsNaN(d))
				throw new ArgumentOutOfRangeException(nameof(d));

			if (double.IsInfinity(d))
			{
				terms = Infinity.terms;
				return;
			}

			if (d == 0.0)
			{
				terms = Zero.terms;
				return;
			}

			terms = new ContinuedFraction(Rational.FromFloatingPoint(d)).terms;
		}

		/// <summary>
		/// Convert a System.Double to a ContinuedFraction. The result is exact.
		/// </summary>
		public static implicit operator ContinuedFraction(double d)
		{
			return new ContinuedFraction(d);
		}

		/// <summary>
		/// Construct a continued fraction from a System.Single. The result is exact.
		/// </summary>
		public ContinuedFraction(float d)
		{
			if (float.IsNaN(d))
				throw new ArgumentOutOfRangeException(nameof(d));

			if (float.IsInfinity(d))
			{
				terms = Infinity.terms;
				return;
			}

			if (d == 0.0f)
			{
				terms = Zero.terms;
				return;
			}

			terms = new ContinuedFraction(Rational.FromFloatingPoint(d)).terms;
		}

		private static readonly BigInteger MaxDouble = (BigInteger)double.MaxValue;

		private static readonly BigInteger MinDouble = (BigInteger)double.MinValue;

		/// <summary>
		/// Convert a ContinuedFraction to a System.Double. The result is not guaranteed
		/// to be exact or within valid range.
		/// </summary>
		public static explicit operator double(ContinuedFraction cf)
		{
			if (cf.IsInteger)
				return (double)cf.Floor();

			bool neg = cf.IsNegative;
			cf = Abs(cf);
			BigInteger p, q;
			cf.ToRatio(out p, out q);
			int exp = 0;

			while (p.IsEven)
			{
				exp++;
				p >>= 1;
			}

			while (q.IsEven)
			{
				exp--;
				q >>= 1;
			}

			while (p < 2 * q)
			{
				exp--;
				p <<= 1;
			}

			while (q * 2 < p)
			{
				exp++;
				q <<= 1;
			}

			long bits = 0;

			for (int i = 0; i < 53; i++)
			{
				bits <<= 1;

				if (p >= q)
				{
					bits |= 1;
					p %= q;
				}

				p <<= 1;
			}

			if (p > q)
			{
				bits++;	// round up
			}

			if ((bits & (1L << 52)) == 0)	// overflow from rounding up
			{
				bits = 0;
				exp++;
			}
			else
			{
				bits ^= 1L << 52;
			}

			exp += 1023;
			bits |= (long)exp << 52;

			if (neg)
				bits ^= long.MinValue;

			return BitConverter.Int64BitsToDouble(bits);
		}

		/// <summary>
		/// Convert a System.Single to a ContinuedFraction. The result is exact.
		/// </summary>
		public static implicit operator ContinuedFraction(float f)
		{
			return new ContinuedFraction(f);
		}

		/// <summary>
		/// Convert a ContinuedFraction to a System.Single. The result is not guaranteed
		/// to be exact or within valid range.
		/// </summary>
		public static explicit operator float(ContinuedFraction cf)
		{
			return (float)(double)cf;
		}

		/// <summary>
		/// Construct a degenerate continued fraction from a System.Int32.
		/// </summary>
		public ContinuedFraction(int i)
			: this((BigInteger)i)
		{
		}

		/// <summary>
		/// Convert a System.Int32 to a degenerate ContinuedFraction.
		/// </summary>
		public static implicit operator ContinuedFraction(int i)
		{
			return new ContinuedFraction(i);
		}

		/// <summary>
		/// Construct a degenerate continued fraction from a System.UInt32.
		/// </summary>
		public ContinuedFraction(uint i)
			: this((BigInteger)i)
		{
		}

		/// <summary>
		/// Convert a System.UInt32 to a degenerate ContinuedFraction.
		/// </summary>
		public static implicit operator ContinuedFraction(uint i)
		{
			return new ContinuedFraction(i);
		}

		/// <summary>
		/// Construct a degenerate continued fraction from a System.Int64.
		/// </summary>
		public ContinuedFraction(long i)
			: this((BigInteger)i)
		{
		}

		/// <summary>
		/// Convert a System.Int64 to a degenerate ContinuedFraction.
		/// </summary>
		public static implicit operator ContinuedFraction(long i)
		{
			return new ContinuedFraction(i);
		}

		/// <summary>
		/// Construct a degenerate continued fraction from a System.UInt64.
		/// </summary>
		public ContinuedFraction(ulong i)
			: this((BigInteger)i)
		{
		}

		/// <summary>
		/// Convert a System.UInt64 to a degenerate ContinuedFraction.
		/// </summary>
		public static implicit operator ContinuedFraction(ulong i)
		{
			return new ContinuedFraction(i);
		}

		/// <summary>
		/// Enumerate the terms of the continued fraction.
		/// </summary>
		public IEnumerator<BigInteger> GetEnumerator()
		{
			return terms.GetEnumerator();
		}

		/// <summary>
		/// Enumerate the terms of the continued fraction.
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return terms.GetEnumerator();
		}

		/// <summary>
		/// Generate the decimal representation of the continued fraction with a maximum of 17 digits of precision,
		/// using the current culture.
		/// </summary>
		public string ToDigits()
		{
			return ToDigits(CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Generate the decimal representation of the continued fraction with a specified precision, 
		/// using the current culture.
		/// </summary>
		public string ToDigits(int places)
		{
			return ToDigits(places, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Generate the decimal representation of the continued fraction with a maximum of 17 digits of precision,
		/// using the specified format provider.
		/// </summary>
		public string ToDigits(IFormatProvider format)
		{
			return ToDigits(17, format);
		}

		/// <summary>
		/// Generate the decimal representation of the continued fraction with a specified precision
		/// and format provider.
		/// </summary>
		public string ToDigits(int places, IFormatProvider format)
		{
			return ToDigits(places, 10, format);
		}

		/// <summary>
		/// Generate the numeric representation of the continued fraction with a specified precision
		/// and number base, using the current culture.
		/// </summary>
		public string ToDigits(int places, int numberBase)
		{
			return ToDigits(places, numberBase, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Generate the numeric representation of the continued fraction with a specified precision,
		/// number base, and format provider.
		/// </summary>
		public string ToDigits(int places, int numberBase, IFormatProvider format)
		{
			if (numberBase < 2 || numberBase > 36)
				throw new ArgumentOutOfRangeException(nameof(numberBase));

			NumberFormatInfo nfi = (NumberFormatInfo)format.GetFormat(typeof(NumberFormatInfo));

			if (IsInfinity)
				return nfi.PositiveInfinitySymbol;

			var result = new StringBuilder();
			var x = this;
			var n = x.First();
			var digits = x.Frac().YieldDigits(places + 1, numberBase).Skip(1).ToList();

			if (digits.Count > places)	// we have extra digit for rounding
			{
				var final = digits[^1];
				digits.RemoveAt(digits.Count - 1);
				int half = (numberBase + 1) / 2;

				if (final >= half)
				{
					while (digits.Count > 0 && ++digits[^1] == numberBase)	// round up
					{
						digits.RemoveAt(digits.Count - 1);	// overflow to previous digit
					}

					if (digits.Count == 0)	// they were all 9's
					{
						n++;				// so we need to increment the whole part
					}
				}
			}

			// remove trailing zeros
			while (digits.Count != 0 && digits[^1] == 0)
			{
				digits.RemoveAt(digits.Count - 1);
			}

			if (n > BigInteger.Zero)
			{
				var whole = new StringBuilder();

				while (n != BigInteger.Zero)
				{
                    n = BigInteger.DivRem(n, numberBase, out BigInteger digit);
                    whole.Insert(0, Digit((int)digit, nfi));
				}

				result.Append(whole);
			}
			else
			{
				result.Append(Digit(0, nfi));
			}

			if (digits.Count != 0)
			{
				result.Append(nfi.NumberDecimalSeparator);

				foreach (int digit in digits)
				{
					result.Append(Digit(digit, nfi));
				}
			}

			if (IsNegative)
			{
				switch (nfi.NumberNegativePattern)
				{
					case 0:
						return $"({result})";

					case 1:
						return $"{nfi.NegativeSign}{result}";

					case 2:
						return $"{nfi.NegativeSign} {result}";

					case 3:
						return $"{result}{nfi.NegativeSign}";

					case 4:
						return $"{result} {nfi.NegativeSign}";
				}
			}

			return result.ToString();
		}

		/// <summary>
		/// Indicates whether the value of the current ContinuedFraction object is ContinuedFraction.One.
		/// </summary>
		public bool IsOne
		{
			get { return terms.Any() && terms.First().IsOne && !terms.Skip(1).Any(); }
		}

		/// <summary>
		/// Indicates whether the value of the current ContinuedFraction object is ContinuedFraction.Zero.
		/// </summary>
		public bool IsZero
		{
			get { return terms.Any() && terms.First().IsZero && !terms.Skip(1).Any(); }
		}

		/// <summary>
		/// Indicates whether the value of the current ContinuedFraction object is ContinuedFraction.Infinity.
		/// </summary>
		public bool IsInfinity
		{
			get { return !terms.Any(); }
		}

		/// <summary>
		/// Returns a value indicating the sign of the continued fraction.
		/// </summary>
		public int Sign
		{
			get
			{
				if (IsInfinity)
					throw new InvalidOperationException();

				var s = terms.First().Sign;

				if (s != 0)
					return s;

				if (!terms.Skip(1).Any())
					return 0;

				return terms.Skip(1).First().Sign;
			}
		}

		/// <summary>
		/// Indicates whether the current continued fraction is an integer, without a fractional part.
		/// </summary>
		public bool IsInteger
		{
			get { return !IsInfinity && !terms.Skip(1).Any(); }
		}

		/// <summary>
		/// Indicates whether the current continued fraction is negative.
		/// </summary>
		public bool IsNegative
		{
			get { return this.Sign < 0; }
		}

		/// <summary>
		/// Returns the largest integer less than or equal to the current value.
		/// </summary>
		public BigInteger Floor()
		{
			return Floor(this);
		}

		/// <summary>
		/// Returns the largest integer less than or equal to the specified value.
		/// </summary>
		public static BigInteger Floor(ContinuedFraction c)
		{
			if (c.IsInfinity)
				throw new InvalidOperationException();

			return c.First();
		}

		/// <summary>
		/// Returns the smallest integer greater than or equal to the current value.
		/// </summary>
		public BigInteger Ceiling()
		{
			return Ceiling(this);
		}

		/// <summary>
		/// Returns the smallest integer greater than or equal to the specified value.
		/// </summary>
		public static BigInteger Ceiling(ContinuedFraction c)
		{
			if (c.IsInfinity)
				throw new InvalidOperationException();

			if (c.IsInteger)
				return c.First();

			return c.First() + BigInteger.One;
		}

		/// <summary>
		/// Removes the fractional portion of the continued fraction and returns only the integer portion.
		/// </summary>
		public BigInteger Truncate()
		{
			return Truncate(this);
		}

		/// <summary>
		/// Removes the fractional portion of the specified continued fraction and returns only the integer portion.
		/// </summary>
		public static BigInteger Truncate(ContinuedFraction c)
		{
			if (c.IsNegative)
				return Ceiling(c);

			return Floor(c);
		}

		/// <summary>
		/// Returns the integer nearest to the current continued fraction.
		/// </summary>
		public BigInteger Round()
		{
			return Round(this);
		}

		/// <summary>
		/// Returns the integer nearest to the specified continued fraction.
		/// If two values are equally distant, it returns the even value.
		/// </summary>
		public static BigInteger Round(ContinuedFraction c)
		{
			BigInteger n = c.First();

			if (c.IsInteger)
				return n;

			var f = Frac(c);

			switch (Abs(f).CompareTo(OneHalf))
			{
				case -1:
					return n;

				case +1:
					return f.Sign > 0 ? n + BigInteger.One : n - BigInteger.One;
			}

			if (n.IsEven)
				return n;

			return f.Sign > 0 ? n + BigInteger.One : n - BigInteger.One;
		}

		/// <summary>
		/// Removes the integer portion of the continued fraction and returns only the fractional portion.
		/// </summary>
		public ContinuedFraction Frac()
		{
			return Frac(this);
		}

		/// <summary>
		/// Removes the integer portion of the specified continued fraction and returns only the fractional portion.
		/// </summary>
		public static ContinuedFraction Frac(ContinuedFraction c)
		{
			if (c.IsInteger)
				return Zero;

			return new(PrependZero(c.Skip(1)).ToList());
		}

		/// <summary>
		/// Returns the reciprocal of the current continued fraction.
		/// </summary>
		public ContinuedFraction Reciprocal()
		{
			return Reciprocal(this);
		}

		/// <summary>
		/// Returns the reciprocal of the specified continued fraction.
		/// </summary>
		public static ContinuedFraction Reciprocal(ContinuedFraction c)
		{
			if (c.IsInfinity)
				return ContinuedFraction.Zero;

			if (c.First().IsZero)
				return new ContinuedFraction(c.Skip(1).ToList());

			return new ContinuedFraction(PrependZero(c).ToList());
		}

		/// <summary>
		/// Returns the current continued fraction negated (changes the sign).
		/// </summary>
		public ContinuedFraction Negate()
		{
			return Negate(this);
		}

		/// <summary>
		/// Returns the specified continued fraction negated (changes the sign).
		/// </summary>
		public static ContinuedFraction Negate(ContinuedFraction c)
		{
			return new ContinuedFraction(YieldNegate(c).ToList());
		}

		/// <summary>
		/// Applies a linear fractional transformation of two continued fractions.
		/// </summary>
		/// <returns>(a + bx + cy + dxy) / (e + fx + gy + hxy)</returns>
		public static ContinuedFraction Transform(ContinuedFraction x, ContinuedFraction y, BigInteger a, BigInteger b, BigInteger c, BigInteger d, BigInteger e, BigInteger f, BigInteger g, BigInteger h)
		{
			return new ContinuedFraction(YieldTransform(x, y, a, b, c, d, e, f, g, h).ToList());
		}

		/// <summary>
		/// Applies a linear fractional transformation of the current continued fraction.
		/// </summary>
		/// <returns>(a + bx) / (c + dx)</returns>
		public ContinuedFraction Transform(BigInteger a, BigInteger b, BigInteger c, BigInteger d)
		{
			return new ContinuedFraction(YieldTransform(this, a, b, c, d).ToList());
		}

		/// <summary>
		/// Returns the absolute value of the current continued fraction.
		/// </summary>
		public ContinuedFraction Abs()
		{
			return Abs(this);
		}

		/// <summary>
		/// Returns the absolute value of the specified continued fraction.
		/// </summary>
		public static ContinuedFraction Abs(ContinuedFraction c)
		{
			if (c.Sign < 0)
				return Negate(c);

			return c;
		}

		/// <summary>
		/// Compares two continued fractions and returns the sign of their difference.
		/// </summary>
		public static int Compare(ContinuedFraction a, ContinuedFraction b)
		{
			if (a.IsInfinity || b.IsInfinity)
				throw new InvalidOperationException();

			using var ea = a.GetEnumerator();
			var ha = ea.MoveNext();
			using var eb = b.GetEnumerator();
			var hb = eb.MoveNext();
			int s = 1;

			while (ha && hb)
			{
				int compare = ea.Current.CompareTo(eb.Current);

				if (compare != 0)
					return compare * s;

				s = -s;
				ha = ea.MoveNext();
				hb = eb.MoveNext();
			}

			if (!ha && !hb)
				return 0;

			if (hb)
				return s;

			return -s;
		}

		/// <summary>
		/// Compares the current continued fraction with another one and returns the sign of their difference.
		/// </summary>
		public int CompareTo(ContinuedFraction cf)
		{
			return Compare(this, cf);
		}

		/// <summary>
		/// Indicates whether the current continued fraction is equal to another one.
		/// </summary>
		public bool Equals(ContinuedFraction other)
		{
			return Compare(this, other) == 0;
		}

		/// <summary>
		/// Indicates whether the current continued fraction is equal to another one.
		/// </summary>
		public override bool Equals(object obj)
		{
			var cf = obj as ContinuedFraction;

			if (cf == null)
				return false;

			return Compare(this, cf) == 0;
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		public override int GetHashCode()
		{
            return HashHelper.Finisher(HashHelper.Mixer(-949609549, this));
		}

		/// <summary>
		/// Returns the larger of two continued fractions.
		/// </summary>
		public static ContinuedFraction Max(ContinuedFraction a, ContinuedFraction b)
		{
			if (Compare(a, b) > 0)
				return a;

			return b;
		}

		/// <summary>
		/// Returns the smaller of two continued fractions.
		/// </summary>
		public static ContinuedFraction Min(ContinuedFraction a, ContinuedFraction b)
		{
			if (Compare(a, b) < 0)
				return a;

			return b;
		}

		/// <summary>
		/// Calculates the convergents of the current continued fraction.
		/// </summary>
		public IEnumerable<Rational> Convergents()
		{
			return Convergents(this);
		}

		/// <summary>
		/// Calculates the convergents of the specified continued fraction.
		/// </summary>
		public static IEnumerable<Rational> Convergents(ContinuedFraction cf)
		{
			var a = BigInteger.One;
			var b = BigInteger.Zero;
			var c = BigInteger.Zero;
			var d = BigInteger.One;

			foreach (var term in cf)
			{
				(a, b) = (b, b * term + a);
				(c, d) = (d, d * term + c);

				yield return new Rational(d, b);
			}
		}

		/// <summary>
		/// Calculates the convergent of the current continued fraction.
		/// </summary>
		public void ToRatio(out BigInteger numerator, out BigInteger denominator)
		{
			ToRatio(this, out numerator, out denominator);
		}

        /// <summary>
        /// Calculates the convergent of the current continued fraction.
        /// </summary>
        public void ToRatio(out Rational r)
        {
            ToRatio(this, out r);
        }

		/// <summary>
		/// Calculates the convergent of the specified continued fraction.
		/// </summary>
		public static void ToRatio(ContinuedFraction cf, out BigInteger numerator, out BigInteger denominator)
		{
			var convergent = Convergents(cf).Last();
			numerator = convergent.P;
			denominator = convergent.Q;
		}

        /// <summary>
        /// Calculates the convergent of the specified continued fraction.
        /// </summary>
        public static void ToRatio(ContinuedFraction cf, out Rational r)
        {
            r = Convergents(cf).Last();
        }

		/// <summary>
		/// Calculates the continued fraction representation of the square root of a non-negative integer.
		/// </summary>
		public static ContinuedFraction Sqrt(BigInteger b, int maxTerms)
		{
			if (b.Sign < 0)
				throw new ArgumentOutOfRangeException(nameof(b));

			return new ContinuedFraction(YieldSqrt(b), maxTerms);
		}

		/// <summary>
		/// Unary negation operator.
		/// </summary>
		public static ContinuedFraction operator -(ContinuedFraction c)
		{
			return new ContinuedFraction(NormalizeSign(c.Transform(0, -1, 1, 0)).ToList());
		}

		/// <summary>
		/// Addition operator.
		/// </summary>
		public static ContinuedFraction operator +(ContinuedFraction x, ContinuedFraction y)
		{
			return new ContinuedFraction(NormalizeSign(Transform(x, y, 0, 1, 1, 0, 1, 0, 0, 0)).ToList());
		}

		/// <summary>
		/// Subtraction operator.
		/// </summary>
		public static ContinuedFraction operator -(ContinuedFraction x, ContinuedFraction y)
		{
			return new ContinuedFraction(NormalizeSign(Transform(x, y, 0, 1, -1, 0, 1, 0, 0, 0)).ToList());
		}

		/// <summary>
		/// Multiplication operator.
		/// </summary>
		public static ContinuedFraction operator *(ContinuedFraction x, ContinuedFraction y)
		{
			return new ContinuedFraction(NormalizeSign(Transform(x, y, 0, 0, 0, 1, 1, 0, 0, 0)).ToList());
		}

		/// <summary>
		/// Division operator.
		/// </summary>
		public static ContinuedFraction operator /(ContinuedFraction x, ContinuedFraction y)
		{
			return new ContinuedFraction(NormalizeSign(Transform(x, y, 0, 1, 0, 0, 0, 0, 1, 0)).ToList());
		}

		/// <summary>
		/// Modulus operator.
		/// </summary>
		public static ContinuedFraction operator %(ContinuedFraction x, ContinuedFraction y)
		{
			return x - Round(x / y) * y;
		}

		/// <summary>
		/// Equality operator.
		/// </summary>
		public static bool operator ==(ContinuedFraction x, ContinuedFraction y)
		{
			return Compare(x, y) == 0;
		}

		/// <summary>
		/// Inequality operator.
		/// </summary>
		public static bool operator !=(ContinuedFraction x, ContinuedFraction y)
		{
			return Compare(x, y) != 0;
		}

		/// <summary>
		/// Greater-than operator.
		/// </summary>
		public static bool operator >(ContinuedFraction x, ContinuedFraction y)
		{
			return Compare(x, y) > 0;
		}

		/// <summary>
		/// Less-than operator.
		/// </summary>
		public static bool operator <(ContinuedFraction x, ContinuedFraction y)
		{
			return Compare(x, y) < 0;
		}

		/// <summary>
		/// Greater-or-equal operator.
		/// </summary>
		public static bool operator >=(ContinuedFraction x, ContinuedFraction y)
		{
			return Compare(x, y) >= 0;
		}

		/// <summary>
		/// Less-or-equal operator.
		/// </summary>
		public static bool operator <=(ContinuedFraction x, ContinuedFraction y)
		{
			return Compare(x, y) <= 0;
		}

		/// <summary>
		/// Raise the current continued fraction to an integer power.
		/// </summary>
		public ContinuedFraction Pow(int exponent)
		{
			return Pow(this, exponent);
		}

		/// <summary>
		/// Raise the specified continued fraction to an integer power.
		/// </summary>
		public static ContinuedFraction Pow(ContinuedFraction cf, int exponent)
		{
			if (exponent == 0)
				return One;

			if (exponent < 0)
				return Reciprocal(Pow(cf, -exponent));

			if ((exponent & 1) == 0)
			{
				var p = Pow(cf, exponent / 2);
				return p * p;
			}

			return cf * Pow(cf, exponent - 1);
		}

        /// <summary>
        /// Generate a uniformly-distributed random number between 0 and 1.
        /// </summary>
        public static ContinuedFraction UniformRandom(Random rand, int terms)
        {
            return new ContinuedFraction(YieldUniformRandom(rand, terms), terms);
        }

        /// <summary>
        /// Generate a uniformly-distributed random number between 0 and 1.
        /// </summary>
        public static ContinuedFraction UniformRandom(Generator rand, int terms)
        {
            return new ContinuedFraction(YieldUniformRandom(rand, terms), terms);
        }

        /// <summary>
        /// Returns the next rational number in the Moshe Newman enumeration of the Calkin Wilf tree.
        /// (Eventually lists all positive rational numbers if you start with zero.)
        /// </summary>
        public ContinuedFraction Next()
		{
			return Next(this);
		}

        /// <summary>
        /// Returns the next rational number in the Calkin Wilf tree.
        /// (Eventually lists all positive rational numbers if you start with zero.)
        /// </summary>
        /// <remarks>Uses the Moshe Newman recurrence relation.</remarks>
        public static ContinuedFraction Next(ContinuedFraction x)
		{
			return Reciprocal(Floor(x) * Two + One - x);
		}


		private static Terms YieldRegularize(Terms x, Terms y)
		{
			BigInteger a = 0;
			BigInteger b = 1;
			BigInteger c = 1;
			BigInteger d = 0;
			using var ey = y.GetEnumerator();
			var hy = ey.MoveNext();

			foreach (var term in x)
			{
				BigInteger b0;

				if (hy)
				{
					b0 = ey.Current;
					hy = ey.MoveNext();
				}
				else
				{
					b0 = BigInteger.One;
				}

				(a, b) = (b * b0, b * term + a);
				(c, d) = (d * b0, d * term + c);

				if (d.IsZero && c.IsZero)
					break;

				while (!d.IsZero && !c.IsZero)
				{
                    var q = BigInteger.DivRem(b, d, out BigInteger r);
                    var s = BigInteger.DivRem(a, c, out BigInteger t);

					if (q != s)
						break;

					yield return q;
					(b, d) = (d, r);
					(a, c) = (c, t);
				}
			}

			if (d.IsZero)
				yield break;

			foreach (var term in YieldRatio(b, d))
				yield return term;
		}

		private static FiniteTerms Term(BigInteger b)
		{
			return [b];
		}

		private static Terms Odds()
		{
			var i = BigInteger.One;
			BigInteger two = 2;

			while (true)
			{
				yield return i;
				i += two;
			}
		}

		private static Terms Squares()
		{
			var square = BigInteger.Zero;

			foreach (var odd in Odds())
			{
				square += odd;
				yield return square;
			}
		}

		private static FiniteTerms YieldApproximate(Terms b, int maxTerms)
		{
			var list = b.Take(maxTerms + 1).ToList();	// get one extra term, for rounding

			if (list.Count > maxTerms)	// we got the extra term
			{
				var last = list[maxTerms];
				list.RemoveAt(maxTerms);

				if (last == BigInteger.One)
				{
					list[maxTerms - 1]++;	// round up
				}
			}

			// normalize
			while (list.Count > 1 && list[list.Count - 1] == BigInteger.One)
			{
				list.RemoveAt(list.Count - 1);
				list[^1]++;
			}

			return list;
		}

		private static Terms YieldDecimal(decimal d)
		{
			int[] bits = decimal.GetBits(d);
			byte[] bytes = new byte[12];
			Buffer.BlockCopy(bits, 0, bytes, 0, 12);
			var numerator = new BigInteger(bytes);
			int exponent = (byte)(bits[3] >> 16);
			var denominator = BigInteger.Pow(10, exponent);
			if (bits[3] < 0) numerator = -numerator;
			return YieldRatio(numerator, denominator);
		}

		private static Terms YieldRatio(BigInteger p, BigInteger q)
		{
			if (q == BigInteger.Zero)
				yield break;

			if (BigInteger.Abs(q) == BigInteger.One)
			{
				yield return p * q.Sign;
				yield break;
			}

			if (p.Sign * q.Sign == -1)
			{
				var floor = p / q - BigInteger.One;
				yield return floor;
				var temp = p - floor * q;
				p = q;
				q = temp;
			}

			while (true)
			{
				if (q == BigInteger.One)
				{
					yield return p;
					yield break;
				}

                BigInteger d = BigInteger.DivRem(p, q, out BigInteger m);
                yield return d;

				if (m == BigInteger.Zero)
					yield break;

				p = q;
				q = m;
			}
		}

		private static string Digit(int d, NumberFormatInfo nfi)
		{
			if (d < 10)
				return nfi.NativeDigits[d];
			else
				return ((char)((int)'A' + (d - 10))).ToString();
		}

		private IEnumerable<int> YieldDigits(int places, int numberBase)
		{
			BigInteger a = 0, b = 1, c = 1, d = 0;
			BigInteger q, s;

            foreach (var term in this)
			{
				(a, b) = (b, b * term + a);
				(c, d) = (d, d * term + c);

				while (d != 0 && c != 0)
				{
					q = BigInteger.DivRem(b, d, out var r);
					s = BigInteger.DivRem(a, c, out var t);

					if (q != s)
						break;

					yield return (int)q;
					places--;

					if (places < 0)
						yield break;

					b = numberBase * r;
					a = numberBase * t;
				}
			}

			while (d != 0)
			{
				q = BigInteger.DivRem(b, d, out var r);
				yield return (int)q;
				places--;

				if (places < 0)
					yield break;

				b = numberBase * r;
			}
		}

		public static Terms YieldE()
		{
			BigInteger two = 2;
			yield return two;
			BigInteger i = BigInteger.Zero;

			while (true)
			{
				i += two;
				yield return BigInteger.One;
				yield return i;
				yield return BigInteger.One;
			}
		}

		private static IEnumerable<T> Dup<T>(IEnumerable<T> terms)
		{
			foreach (var term in terms)
			{
				yield return term;
				yield return term;
			}
		}

		public static Terms YieldLog(BigInteger n)
		{
			n--;
			var a = Term(n).Concat(Dup(Squares()).Select(s => s * n));
			var b = NonnegativeIntegers();
			return YieldRegularize(b, a);
		}

		private static Terms PrependZero(Terms c)
		{
			yield return BigInteger.Zero;

			foreach (var term in c)
				yield return term;
		}

		private static Terms YieldNegate(Terms c)
		{
			if (!c.Any())
				yield break;

			if (!c.Skip(1).Any())
			{
				yield return -c.First();
				yield break;
			}

            using var e = c.GetEnumerator();
            e.MoveNext();
            var first = e.Current;
            e.MoveNext();
            var second = e.Current;

            if (e.MoveNext())
            {
                var third = e.Current;
                yield return -first - BigInteger.One;
                yield return third + BigInteger.One;

                while (e.MoveNext())
                    yield return e.Current;
            }
            else if (second == 2)
            {
                yield return -first - BigInteger.One;
                yield return second;
            }
            else
            {
                yield return -first - BigInteger.One;
                yield return BigInteger.One;
                yield return second - BigInteger.One;
            }
        }

		private static Terms YieldTransform(
			Terms x, Terms y, 
			BigInteger a, BigInteger b, BigInteger c, BigInteger d, 
			BigInteger e, BigInteger f, BigInteger g, BigInteger h)
		{
			using var ea = x.GetEnumerator();
			var ha = ea.MoveNext();
			using var eb = y.GetEnumerator();
			var hb = eb.MoveNext();

			while (ha && hb)
			{
				var p = ea.Current;
				(a, b) = (b, a + b * p);
				(c, d) = (d, c + d * p);
				(e, f) = (f, e + f * p);
				(g, h) = (h, g + h * p);
				ha = ea.MoveNext();

				var q = eb.Current;
				(a, c) = (c, a + c * q);
				(b, d) = (d, b + d * q);
				(e, g) = (g, e + g * q);
				(f, h) = (h, f + h * q);
				hb = eb.MoveNext();

				if (e.IsZero && f.IsZero && g.IsZero && h.IsZero)
					yield break;

				while (!e.IsZero && !f.IsZero && !g.IsZero && !h.IsZero)
				{
                    var q0 = BigInteger.DivRem(a, e, out BigInteger r0);
                    var q1 = BigInteger.DivRem(b, f, out BigInteger r1);
					var q2 = BigInteger.DivRem(c, g, out BigInteger r2);
					var q3 = BigInteger.DivRem(d, h, out BigInteger r3);

					if (q0 != q1 || q0 != q2 || q0 != q3)
						break;

					yield return q0;
					(a, e) = (e, r0);
					(b, f) = (f, r1);
					(c, g) = (g, r2);
					(d, h) = (h, r3);
				}
			}

			while (ha)	// the rest of y is infinite, a, b, e, f don't matter, calculate (c + dx) / (g + hx)
			{
				var p = ea.Current;
				(c, d) = (d, c + d * p);
				(g, h) = (h, g + h * p);
				ha = ea.MoveNext();

				if (g.IsZero && h.IsZero)
					yield break;

				while (!g.IsZero && !h.IsZero)
				{
                    var q2 = BigInteger.DivRem(c, g, out BigInteger r2);
                    var q3 = BigInteger.DivRem(d, h, out BigInteger r3);

					if (q2 != q3)
						break;

					yield return q2;
					(c, g) = (g, r2);
					(d, h) = (h, r3);
				}
			}

			while (hb)	// rest of x is infinite, a, c, e, g don't matter, calculate (b + dy) / (f + hy)
			{
				var q = eb.Current;
				(b, d) = (d, b + d * q);
				(f, h) = (h, f + h * q);
				hb = eb.MoveNext();

				if (f.IsZero && h.IsZero)
					yield break;

				while (!f.IsZero && !h.IsZero)
				{
                    var q1 = BigInteger.DivRem(b, f, out BigInteger r1);
                    var q3 = BigInteger.DivRem(d, h, out BigInteger r3);

					if (q1 != q3)
						break;

					yield return q1;
					(b, f) = (f, r1);
					(d, h) = (h, r3);
				}
			}

			if (h.IsZero)
				yield break;

			foreach (var term in YieldRatio(d, h))
				yield return term;
		}

		private static Terms YieldTransform(Terms cf, BigInteger a, BigInteger b, BigInteger c, BigInteger d)
		{
			foreach (var term in cf)
			{
				(a, b) = (b, b * term + a);
				(c, d) = (d, d * term + c);

				if (d.IsZero && c.IsZero)
					yield break;

				while (!d.IsZero && !c.IsZero)
				{
                    var q = BigInteger.DivRem(b, d, out BigInteger r);
                    var s = BigInteger.DivRem(a, c, out BigInteger t);

					if (q != s)
						break;

					yield return q;
					(b, d) = (d, r);
					(a, c) = (c, t);
				}
			}

			if (d.IsZero)
				yield break;

			foreach (var term in YieldRatio(b, d))
				yield return term;
		}

		private static BigInteger Sqrt_(BigInteger n)
		{
			BigInteger guess = BigInteger.One;
			BigInteger two = 2;

			while (true)
			{
				BigInteger newGuess = (n / guess + guess) / two;

				if (guess == newGuess)
					return guess;

				if (BigInteger.Abs(guess - newGuess) < two)
				{
					int sign1 = (guess * guess - n).Sign;
					int sign2 = (newGuess * newGuess - n).Sign;

					if (sign1 * sign2 == -1)
						return BigInteger.Min(guess, newGuess);
				}

				guess = newGuess;
			}
		}

		internal static Terms YieldSqrt(BigInteger S)
		{
            ArgumentOutOfRangeException.ThrowIfLessThan(S, BigInteger.Zero, nameof(S));

            BigInteger a0 = Sqrt_(S);
			yield return a0;

			if (a0 * a0 == S)
				yield break;

			BigInteger m = 0;
			BigInteger d = 1;
			BigInteger a = a0;

			while (true)
			{
				m = d * a - m;
				d = (S - m * m) / d;
				a = (a0 + m) / d;
				yield return a;
			}
		}

		private static Terms NormalizeSign(ContinuedFraction c)
		{
			if (c.Skip(1).Any() && c.Skip(1).First().Sign < 0)
			{
				return YieldNegate(c.Select(t => -t));
			}

			return c;
		}

		private static Terms NonnegativeIntegers()
		{
			var i = BigInteger.Zero;

			while (true)
			{
				yield return i;
				i++;
			}
		}

        private static Terms YieldUniformRandom(Random rand, int maxTerms)
        {
            return YieldUniformRandom((Generator)rand, maxTerms);
        }

        private static Terms YieldUniformRandom(Generator rand, int maxTerms)
        {
            var q = 1UL << 32;
			var p = (ulong)rand.UInt32();
			if (p == 0) p = 1;
			yield return 0;

			while (maxTerms-- > 0)
			{
				var n = q / p;
				yield return n;
				(p, q) = (q - n * p, p);
				var bits = 31 - Math.ILogB(q);
				(p, q) = (p << bits, q << bits);
				p |= rand.UInt32() & ((1UL << bits) - 1);
			}
        }
    }
}
