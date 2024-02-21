
namespace Foundations.Functions
{
    internal static class Machine
    {
        public static double ε = Math.Pow(2, -53);
    }

    /// <summary>
    /// Elliptic integrals in Carlson symmetric form.
    /// </summary>
    /// <remarks>
    /// B. C. Carlson (1995) Numerical computation of real or complex elliptic integrals.  Numer. Algorithms 10 (1-2),  pp. 13–26. 
    /// Implements the enhancements described in http://dlmf.nist.gov/19.36
    /// </remarks>
    public static partial class CarlsonSymmetric
    {
        /// <summary>
        /// Symmetric elliptic integral of the first kind.
        /// </summary>
        public static Complex RF(Complex x, Complex y, Complex z)
        {
            switch ((x == 0 ? 1 : 0) + (y == 0 ? 1 : 0) + (z == 0 ? 1 : 0))
            {
                case 2:
                    return double.PositiveInfinity;
            }

            Complex
                A0 = (x + y + z) / 3,
                A = A0;

            double
                r = 10 * Machine.ε,
                Q = Math.Pow(3 * r, -1 / 8d) * Math.Max(Math.Max(Complex.Abs(A - x), Complex.Abs(A - y)), Complex.Abs(A - z));

            while (Q >= Complex.Abs(A))
            {
                Complex
                    sx = Complex.Sqrt(x),
                    sy = Complex.Sqrt(y),
                    sz = Complex.Sqrt(z),
                    λ = sx * sy + sy * sz + sz * sx;

                A = (A + λ) / 4;
                x = (x + λ) / 4;
                y = (y + λ) / 4;
                z = (z + λ) / 4;
                Q /= 4;
            }

            Complex
                X = 1 - x / A,
                Y = 1 - y / A,
                Z = - X - Y,
                E2 = Z * Z - X * Y,
                E3 = X * Y * Z;

            return (1 + E3 * (1 / 14d + 3 * E3 / 104) + E2 * (1 / 10d + 3 * E3 / 44 + E2 * (1 / 24d + E3 / 16 + 5 * E2 / 208))) / Complex.Sqrt(A);
        }

        /// <summary>
        /// Symmetric elliptic integral of the second kind.
        /// </summary>
        public static Complex RD(Complex x, Complex y, Complex z)
        {
            if ((x == 0 && y == 0) || z == 0)
			{
				throw new ArgumentOutOfRangeException();
			}

            Complex
                A0 = (x + y + 3 * z) / 5,
                A = A0,
				sum = 0;

            double
                r = 10 * Machine.ε,
                Q = Math.Pow(r / 4, -1 / 6d) * Math.Max(Math.Max(Complex.Abs(A - x), Complex.Abs(A - y)), Complex.Abs(A - z));

			var m = 0;

            while (Q >= Complex.Abs(A))
            {
                Complex
                    sx = Complex.Sqrt(x),
                    sy = Complex.Sqrt(y),
                    sz = Complex.Sqrt(z),
                    λ = sx * sy + sy * sz + sz * sx;

				sum += 1 / (Math.Pow(4, m) * sz * (z + λ));
				m++;
                A = (A + λ) / 4;
                x = (x + λ) / 4;
                y = (y + λ) / 4;
                z = (z + λ) / 4;
                Q /= 4;
            }

            Complex
                X = (A0 - x) / (A * Math.Pow(4, m)),
                Y = (A0 - y) / (A * Math.Pow(4, m)),
                Z = -(X + Y) / 3,
                XY = X * Y,
				ZZ = Z * Z,
				E2 = XY - 6 * ZZ,
				E3 = (3 * XY - 8 * ZZ) * Z,
				E4 = 3 * (XY - ZZ) * ZZ,
				E5 = XY * ZZ * Z;

            return (1 + E2 * (9 * E2 / 88 - 9 * E3 / 52 - 3 / 14d) + E3 / 6 - 3 * E4 / 22 + 3 * E5 / 26) / (Math.Pow(4, m) * Complex.Pow(A, 1.5)) + 3 * sum;
        }

        /// <summary>
        /// Symmetric elliptic integral of the third kind.
        /// </summary>
        public static Complex RJ(Complex x, Complex y, Complex z, Complex p)
        {
            Complex
                A0 = (x + y + z + 2 * p) / 5,
				δ = (p - x) * (p - y) * (p - z),
                A = A0,
				sum = 0;

            double
                r = 10 * Machine.ε,
                Q = Math.Pow(r / 4, -1 / 6d) * Math.Max(Math.Max(Math.Max(Complex.Abs(A - x), Complex.Abs(A - y)), Complex.Abs(A - z)), Complex.Abs(A - p));

			var m = 0;

            while (Q >= Complex.Abs(A))
            {
                Complex
                    sx = Complex.Sqrt(x),
                    sy = Complex.Sqrt(y),
                    sz = Complex.Sqrt(z),
					sp = Complex.Sqrt(p),
                    λ = sx * sy + sy * sz + sz * sx,
					d = (sp + sx) * (sp + sy) * (sp + sz),
					e = δ / (d * d * Math.Pow(4, 3 * m));

				sum += RF(1, 1 + e, 1 + e) / (d * Math.Pow(4, m));
				m++;
                A = (A + λ) / 4;
                x = (x + λ) / 4;
                y = (y + λ) / 4;
                z = (z + λ) / 4;
                p = (p + λ) / 4;
                Q /= 4;
            }

            Complex
                X = (A0 - x) / (A * Math.Pow(4, m)),
                Y = (A0 - y) / (A * Math.Pow(4, m)),
				Z = (A0 - z) / (A * Math.Pow(4, m)),
                P = (-X - Y - Z) / 2,
                XYZ = X * Y * Z,
				E2 = X * Y + Y * Z + X * Z - 3 * P * P,
				PPP = P * P * P,
				E3 = XYZ + 2 * E2 * P + 4 * PPP,
				E4 = (2 * XYZ + E2 * P + 3 * PPP) * P,
				E5 = XYZ * P * P;

            return (1 - 3 * E2 / 14 + E3 / 6 + 9 * E2 * E2 / 88 - 3 * E4 / 22 - 9 * E2 * E3 / 52 + 3 * E5 / 26) / (Math.Pow(4, m) * Complex.Pow(A, 1.5)) + 6 * sum;
        }

        /// <summary>
        /// Symmetric elliptic integral of the first kind.
        /// </summary>
        public static double RF(double x, double y, double z)
        {
            switch ((x == 0 ? 1 : 0) + (y == 0 ? 1 : 0) + (z == 0 ? 1 : 0))
            {
                case 2:
                    return double.PositiveInfinity;
            }

            double
                A0 = (x + y + z) / 3,
                A = A0;

            double
                r = 10 * Machine.ε,
                Q = Math.Pow(3 * r, -1 / 8d) * Math.Max(Math.Max(Math.Abs(A - x), Math.Abs(A - y)), Math.Abs(A - z));

            while (Q >= Math.Abs(A))
            {
                double
                    sx = Math.Sqrt(x),
                    sy = Math.Sqrt(y),
                    sz = Math.Sqrt(z),
                    λ = sx * sy + sy * sz + sz * sx;

                A = (A + λ) / 4;
                x = (x + λ) / 4;
                y = (y + λ) / 4;
                z = (z + λ) / 4;
                Q /= 4;
            }

            double
                X = 1 - x / A,
                Y = 1 - y / A,
                Z = - X - Y,
                E2 = Z * Z - X * Y,
                E3 = X * Y * Z;

            return (1 + E3 * (1 / 14d + 3 * E3 / 104) + E2 * (1 / 10d + 3 * E3 / 44 + E2 * (1 / 24d + E3 / 16 + 5 * E2 / 208))) / Math.Sqrt(A);
        }

        /// <summary>
        /// Symmetric elliptic integral of the second kind.
        /// </summary>
        public static double RD(double x, double y, double z)
        {
            if ((x == 0 && y == 0) || z == 0)
			{
				throw new ArgumentOutOfRangeException();
			}

            double
                A0 = (x + y + 3 * z) / 5,
                A = A0,
				sum = 0;

            double
                r = 10 * Machine.ε,
                Q = Math.Pow(r / 4, -1 / 6d) * Math.Max(Math.Max(Math.Abs(A - x), Math.Abs(A - y)), Math.Abs(A - z));

			var m = 0;

            while (Q >= Math.Abs(A))
            {
                double
                    sx = Math.Sqrt(x),
                    sy = Math.Sqrt(y),
                    sz = Math.Sqrt(z),
                    λ = sx * sy + sy * sz + sz * sx;

				sum += 1 / (Math.Pow(4, m) * sz * (z + λ));
				m++;
                A = (A + λ) / 4;
                x = (x + λ) / 4;
                y = (y + λ) / 4;
                z = (z + λ) / 4;
                Q /= 4;
            }

            double
                X = (A0 - x) / (A * Math.Pow(4, m)),
                Y = (A0 - y) / (A * Math.Pow(4, m)),
                Z = -(X + Y) / 3,
                XY = X * Y,
				ZZ = Z * Z,
				E2 = XY - 6 * ZZ,
				E3 = (3 * XY - 8 * ZZ) * Z,
				E4 = 3 * (XY - ZZ) * ZZ,
				E5 = XY * ZZ * Z;

            return (1 + E2 * (9 * E2 / 88 - 9 * E3 / 52 - 3 / 14d) + E3 / 6 - 3 * E4 / 22 + 3 * E5 / 26) / (Math.Pow(4, m) * Math.Pow(A, 1.5)) + 3 * sum;
        }

        /// <summary>
        /// Symmetric elliptic integral of the third kind.
        /// </summary>
        public static double RJ(double x, double y, double z, double p)
        {
            double
                A0 = (x + y + z + 2 * p) / 5,
				δ = (p - x) * (p - y) * (p - z),
                A = A0,
				sum = 0;

            double
                r = 10 * Machine.ε,
                Q = Math.Pow(r / 4, -1 / 6d) * Math.Max(Math.Max(Math.Max(Math.Abs(A - x), Math.Abs(A - y)), Math.Abs(A - z)), Math.Abs(A - p));

			var m = 0;

            while (Q >= Math.Abs(A))
            {
                double
                    sx = Math.Sqrt(x),
                    sy = Math.Sqrt(y),
                    sz = Math.Sqrt(z),
					sp = Math.Sqrt(p),
                    λ = sx * sy + sy * sz + sz * sx,
					d = (sp + sx) * (sp + sy) * (sp + sz),
					e = δ / (d * d * Math.Pow(4, 3 * m));

				sum += RF(1, 1 + e, 1 + e) / (d * Math.Pow(4, m));
				m++;
                A = (A + λ) / 4;
                x = (x + λ) / 4;
                y = (y + λ) / 4;
                z = (z + λ) / 4;
                p = (p + λ) / 4;
                Q /= 4;
            }

            double
                X = (A0 - x) / (A * Math.Pow(4, m)),
                Y = (A0 - y) / (A * Math.Pow(4, m)),
				Z = (A0 - z) / (A * Math.Pow(4, m)),
                P = (-X - Y - Z) / 2,
                XYZ = X * Y * Z,
				E2 = X * Y + Y * Z + X * Z - 3 * P * P,
				PPP = P * P * P,
				E3 = XYZ + 2 * E2 * P + 4 * PPP,
				E4 = (2 * XYZ + E2 * P + 3 * PPP) * P,
				E5 = XYZ * P * P;

            return (1 - 3 * E2 / 14 + E3 / 6 + 9 * E2 * E2 / 88 - 3 * E4 / 22 - 9 * E2 * E3 / 52 + 3 * E5 / 26) / (Math.Pow(4, m) * Math.Pow(A, 1.5)) + 6 * sum;
        }

    }
}

