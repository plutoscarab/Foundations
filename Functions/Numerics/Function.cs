
/*

File:	Function.cs
Author:	Bret Mulvey

*/

using System;

namespace Foundations.Functions.Numerics
{
    /// <summary>
    /// Special functions.
    /// </summary>
	public class Function
	{
        const double
            π = Math.PI,
            sqrt2 = 1.414213562373095,
            sqrtπ = 1.772453850905516;

		static double ErfSmallZ(double z)
		{
			double sum = z;
			double term = z;
			double zz = z * z;
			bool odd = true;
			int n = 0;

			while (true)
			{
				term *= zz / ++n;
				double old = sum;

				if (odd)
					sum -= term / (2 * n + 1);
				else
					sum += term / (2 * n + 1);

				odd = !odd;

				if (sum == old)
					break;
			}

			return sum * 2 / sqrtπ;
		}

		static double ErfLargeZ(double x)
		{
			double xx = x * x;
			double t = 0;

			for (int k = 60; k >= 1; k--)
			{
				t = (k - 0.5) / (1 + k / (xx + t));
			}

			return (1 - Math.Exp(Math.Log(x) - xx) / (t + xx) / sqrtπ) * Math.Sign(x);
		}

		/// <summary>
		/// Error function.
		/// </summary>
		public static double Erf(double x)
		{
			if (Math.Abs(x) < 6.5)
				return ErfSmallZ(x);

			return ErfLargeZ(x);
		}

		/// <summary>
		/// Gaussian cumulative distribution function.
		/// </summary>
		public static double NormDist(double x)
		{
			return (1 + Erf(x / sqrt2)) / 2;
		}

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static int GCD(int p, int q)
        {
            if (p == 0 || q == 0) return 0;
            p = Math.Abs(p);
            q = Math.Abs(q);
            if (p == 1 || q == 1) return 1;

            while (true)
            {
                int m = p % q;
                if (m == 0) return q;
                p = q;
                q = m;
            }
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static long GCD(long p, long q)
        {
            if (p == 0 || q == 0) return 0;
            p = Math.Abs(p);
            q = Math.Abs(q);

            while (true)
            {
                long m = p % q;
                if (m == 0) return q;
                p = q;
                q = m;
            }
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static ulong GCD(ulong p, ulong q)
        {
            if (p == 0 || q == 0) return 0;

            while (true)
            {
                ulong m = p % q;
                if (m == 0) return q;
                p = q;
                q = m;
            }
        }
    }
}
