
namespace Foundations
{
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
    }

    /// <summary>
    /// Extension methods to access <see cref="MathM"/> methods.
    /// </summary>
    public static partial class MathExtensions
    {
        /// <summary>
        /// Square root.
        /// </summary>
        public static Decimal Sqrt(this Decimal x) => MathM.Sqrt(x);
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
    /// Implementations of <see cref="System.Math"/> methods for <see cref="Single"/> arguments.
    /// </summary>
    public static partial class MathF
    {
        /// <summary><see cref="Single"/> wrapper for System.Math.Abs.</summary>
        public static Single Abs(Single x) => (Single)Math.Abs((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Acos.</summary>
        public static Single Acos(Single x) => (Single)Math.Acos((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Asin.</summary>
        public static Single Asin(Single x) => (Single)Math.Asin((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Atan.</summary>
        public static Single Atan(Single x) => (Single)Math.Atan((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Ceiling.</summary>
        public static Single Ceiling(Single x) => (Single)Math.Ceiling((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Cos.</summary>
        public static Single Cos(Single x) => (Single)Math.Cos((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Cosh.</summary>
        public static Single Cosh(Single x) => (Single)Math.Cosh((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Exp.</summary>
        public static Single Exp(Single x) => (Single)Math.Exp((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Floor.</summary>
        public static Single Floor(Single x) => (Single)Math.Floor((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Log.</summary>
        public static Single Log(Single x) => (Single)Math.Log((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Log10.</summary>
        public static Single Log10(Single x) => (Single)Math.Log10((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Round.</summary>
        public static Single Round(Single x) => (Single)Math.Round((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Sign.</summary>
        public static Single Sign(Single x) => (Single)Math.Sign((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Sin.</summary>
        public static Single Sin(Single x) => (Single)Math.Sin((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Sinh.</summary>
        public static Single Sinh(Single x) => (Single)Math.Sinh((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Sqrt.</summary>
        public static Single Sqrt(Single x) => (Single)Math.Sqrt((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Tan.</summary>
        public static Single Tan(Single x) => (Single)Math.Tan((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Tanh.</summary>
        public static Single Tanh(Single x) => (Single)Math.Tanh((double)x);

        /// <summary><see cref="Single"/> wrapper for System.Math.Truncate.</summary>
        public static Single Truncate(Single x) => (Single)Math.Truncate((double)x);

    }

    /// <summary>
    /// Extension methods to access <see cref="MathF"/> methods.
    /// </summary>
    public static partial class MathExtensions
    {
        /// <summary>Acos</summary>
        public static Single Acos(this Single x) => MathF.Acos(x);

        /// <summary>Asin</summary>
        public static Single Asin(this Single x) => MathF.Asin(x);

        /// <summary>Atan</summary>
        public static Single Atan(this Single x) => MathF.Atan(x);

        /// <summary>Cos</summary>
        public static Single Cos(this Single x) => MathF.Cos(x);

        /// <summary>Cosh</summary>
        public static Single Cosh(this Single x) => MathF.Cosh(x);

        /// <summary>Exp</summary>
        public static Single Exp(this Single x) => MathF.Exp(x);

        /// <summary>Log10</summary>
        public static Single Log10(this Single x) => MathF.Log10(x);

        /// <summary>Sin</summary>
        public static Single Sin(this Single x) => MathF.Sin(x);

        /// <summary>Sinh</summary>
        public static Single Sinh(this Single x) => MathF.Sinh(x);

        /// <summary>Sqrt</summary>
        public static Single Sqrt(this Single x) => MathF.Sqrt(x);

        /// <summary>Tan</summary>
        public static Single Tan(this Single x) => MathF.Tan(x);

        /// <summary>Tanh</summary>
        public static Single Tanh(this Single x) => MathF.Tanh(x);

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
