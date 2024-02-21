
namespace Foundations
{
    /// <summary>
    /// Number-theoretic functions.
    /// </summary>
	public static partial class Numbers
    {
        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static sbyte GCD(sbyte p, sbyte q)
        {
            if (p == 0 || q == 0) return 0;
            (p, q) = (sbyte.Abs(p), sbyte.Abs(q));

            while (true)
            {
                var m = (sbyte)(p % q);
                if (m == 0) return q;
                p = q;
                q = m;
            }
            
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static byte GCD(byte p, byte q)
        {
            if (p == 0 || q == 0) return 0;

            while (true)
            {
                var m = (byte)(p % q);
                if (m == 0) return q;
                p = q;
                q = m;
            }
            
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static short GCD(short p, short q)
        {
            if (p == 0 || q == 0) return 0;
            (p, q) = (short.Abs(p), short.Abs(q));

            while (true)
            {
                var m = (short)(p % q);
                if (m == 0) return q;
                p = q;
                q = m;
            }
            
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static ushort GCD(ushort p, ushort q)
        {
            if (p == 0 || q == 0) return 0;

            while (true)
            {
                var m = (ushort)(p % q);
                if (m == 0) return q;
                p = q;
                q = m;
            }
            
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static int GCD(int p, int q)
        {
            if (p == 0 || q == 0) return 0;
            (p, q) = (int.Abs(p), int.Abs(q));

            while (true)
            {
                var m = (int)(p % q);
                if (m == 0) return q;
                p = q;
                q = m;
            }
            
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static uint GCD(uint p, uint q)
        {
            if (p == 0 || q == 0) return 0;

            while (true)
            {
                var m = (uint)(p % q);
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
            (p, q) = (long.Abs(p), long.Abs(q));

            while (true)
            {
                var m = (long)(p % q);
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
                var m = (ulong)(p % q);
                if (m == 0) return q;
                p = q;
                q = m;
            }
            
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static BigInteger GCD(BigInteger p, BigInteger q)
        {
            return BigInteger.GreatestCommonDivisor(p, q);
            
        }

        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        public static Nat GCD(Nat p, Nat q)
        {
            return Nat.GreatestCommonDivisor(p, q);
            
        }

    }
}
