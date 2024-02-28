
namespace Foundations
{
    public static partial class MathM
    {
        public static (decimal Scaled, int Log2) Log2Normalize(decimal x)
        {
            var log = (int)Math.Round(Math.Log2((double)x));
            x /= (decimal)Math.Pow(2.0, log);
            return (x, log);
        }
    }
    /// <summary>
    /// Implementations of <see cref="System.Math"/> methods for <see cref="System.Decimal"/> arguments.
    /// </summary>
    public static partial class MathM
    {
        /// <summary>
        /// Square root.
        /// </summary>
        public static Decimal Sqrt(Decimal x)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(x, 0M, nameof(x));
			var s = (Decimal)Math.Sqrt((double)x);
            s = (x / s + s) / 2M;
			return (x / s + s) / 2M;
        }

        public static Decimal Log(Decimal x)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(x, 0M, nameof(x));

            if (x == 1M)
                return 0M;

            (x, var e) = MathM.Log2Normalize(x);
            var y = (x - 1M) / (x + 1M);
            var y2 = y * y;
            var num = 1M;
            var den = 1M;
            DecimalSum sum = 1M;

            while (true)
            {
                num *= y2;
                den += 2M;
                var term = num / den;
                (var old, sum) = (sum, sum + term);

                if (old.Equals(sum))
                    break;
            }

            sum *= 2M * y;
            sum += e * DecimalConstants.Ln2;
            return sum.Value;
        }

        public static Decimal Mod(Decimal x, Decimal y)
        {
            var div = MathM.Floor(x / y);
            return x - y * div;
        }

        public static Decimal Sin(Decimal x)
        {
            var neg = x < 0M;
            x = MathM.Abs(x);
            x = MathM.Mod(x, DecimalConstants.Twoπ);

            if (x > DecimalConstants.π)
            {
                neg = !neg;
                x -= DecimalConstants.π;
            }

            var xx = -(x * x);
            var num = x;
            var den = 1M;
            var count = 1M;
            DecimalSum sum = x;

            while (true)
            {
                num *= xx;
                count += 1M;
                den *= count;
                count += 1M;
                den *= count;
                var term = num / den;
                sum += term;
                (var old, sum) = (sum, sum + term);

                if (old.Equals(sum))
                    break;
            }

            x = neg ? -sum.Value : sum.Value;
            return x;
        }

        public static Decimal Cos(Decimal x) => Sin(DecimalConstants.Halfπ - x);

        public static (Decimal, Decimal) SinCos(Decimal x)
        {
            var s = Sin(x);
            var c = MathM.Sqrt(1 - s * s);
            x = MathM.Mod(x, DecimalConstants.Twoπ);
            
            if (x > DecimalConstants.Halfπ && x < DecimalConstants.ThreeHalfπ)
                c = -c;

            return (s, c);
        }

        public static Decimal Tan(Decimal x)
        {
            var (s, c) = SinCos(x);
            return s / c;
        }
    }

    /// <summary>
    /// Extension methods to access <see cref="MathM"/> methods.
    /// </summary>
    public static partial class MathExtensions
    {
        public static Decimal Sqrt(this Decimal x) => MathM.Sqrt(x);
        public static Decimal Log(this Decimal x) => MathM.Log(x);
        public static Decimal Sin(this Decimal x) => MathM.Sin(x);
        public static Decimal Cos(this Decimal x) => MathM.Cos(x);
        public static Decimal Tan(this Decimal x) => MathM.Tan(x);
    }

    /// <summary>
    /// Implementations of <see cref="System.Math"/> methods for <see cref="System.Decimal"/> arguments.
    /// </summary>
    public static partial class MathM
    {
        /// <summary>Copy of System.Math.Abs for class consistency.</summary>
        public static Decimal Abs(Decimal x) => Math.Abs(x);

        /// <summary>Copy of System.Math.Ceiling for class consistency.</summary>
        public static Decimal Ceiling(Decimal x) => Math.Ceiling(x);

        /// <summary>Copy of System.Math.Floor for class consistency.</summary>
        public static Decimal Floor(Decimal x) => Math.Floor(x);

        /// <summary>Copy of System.Math.Round for class consistency.</summary>
        public static Decimal Round(Decimal x) => Math.Round(x);

        /// <summary>Copy of System.Math.Sign for class consistency.</summary>
        public static Decimal Sign(Decimal x) => Math.Sign(x);

        /// <summary>Copy of System.Math.Truncate for class consistency.</summary>
        public static Decimal Truncate(Decimal x) => Math.Truncate(x);

    }

    /// <summary>
    /// Implementations of <see cref="System.Math"/> methods for <see cref="Half"/> arguments.
    /// </summary>
    public static partial class MathH
    {
        /// <summary><see cref="Half"/> wrapper for System.Math.Abs.</summary>
        public static Half Abs(Half x) => (Half)Math.Abs((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Acos.</summary>
        public static Half Acos(Half x) => (Half)Math.Acos((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Asin.</summary>
        public static Half Asin(Half x) => (Half)Math.Asin((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Atan.</summary>
        public static Half Atan(Half x) => (Half)Math.Atan((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Ceiling.</summary>
        public static Half Ceiling(Half x) => (Half)Math.Ceiling((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Cos.</summary>
        public static Half Cos(Half x) => (Half)Math.Cos((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Cosh.</summary>
        public static Half Cosh(Half x) => (Half)Math.Cosh((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Exp.</summary>
        public static Half Exp(Half x) => (Half)Math.Exp((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Floor.</summary>
        public static Half Floor(Half x) => (Half)Math.Floor((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Log.</summary>
        public static Half Log(Half x) => (Half)Math.Log((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Log10.</summary>
        public static Half Log10(Half x) => (Half)Math.Log10((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Round.</summary>
        public static Half Round(Half x) => (Half)Math.Round((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Sign.</summary>
        public static Half Sign(Half x) => (Half)Math.Sign((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Sin.</summary>
        public static Half Sin(Half x) => (Half)Math.Sin((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Sinh.</summary>
        public static Half Sinh(Half x) => (Half)Math.Sinh((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Sqrt.</summary>
        public static Half Sqrt(Half x) => (Half)Math.Sqrt((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Tan.</summary>
        public static Half Tan(Half x) => (Half)Math.Tan((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Tanh.</summary>
        public static Half Tanh(Half x) => (Half)Math.Tanh((double)x);

        /// <summary><see cref="Half"/> wrapper for System.Math.Truncate.</summary>
        public static Half Truncate(Half x) => (Half)Math.Truncate((double)x);

    }

    /// <summary>
    /// Extension methods to access <see cref="MathH"/> methods.
    /// </summary>
    public static partial class MathExtensions
    {
        /// <summary>Acos</summary>
        public static Half Acos(this Half x) => MathH.Acos(x);

        /// <summary>Asin</summary>
        public static Half Asin(this Half x) => MathH.Asin(x);

        /// <summary>Atan</summary>
        public static Half Atan(this Half x) => MathH.Atan(x);

        /// <summary>Cos</summary>
        public static Half Cos(this Half x) => MathH.Cos(x);

        /// <summary>Cosh</summary>
        public static Half Cosh(this Half x) => MathH.Cosh(x);

        /// <summary>Exp</summary>
        public static Half Exp(this Half x) => MathH.Exp(x);

        /// <summary>Log10</summary>
        public static Half Log10(this Half x) => MathH.Log10(x);

        /// <summary>Sin</summary>
        public static Half Sin(this Half x) => MathH.Sin(x);

        /// <summary>Sinh</summary>
        public static Half Sinh(this Half x) => MathH.Sinh(x);

        /// <summary>Sqrt</summary>
        public static Half Sqrt(this Half x) => MathH.Sqrt(x);

        /// <summary>Tan</summary>
        public static Half Tan(this Half x) => MathH.Tan(x);

        /// <summary>Tanh</summary>
        public static Half Tanh(this Half x) => MathH.Tanh(x);

    }

}
