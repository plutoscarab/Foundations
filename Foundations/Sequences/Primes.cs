﻿
// http://oeis.org/A000040

using System.Collections;
using Foundations.Types;

using Num = System.Int64;
using Nums = System.Collections.Generic.IEnumerable<long>;

namespace Foundations;

/// <summary>
/// Various well-known sequences.
/// </summary>
public static partial class Sequences
{
    private static IEnumerable<int> PrimeSieve(int n)
    {
        if (n < 65536)
        {
            yield return 2;
            yield return 3;
            yield return 5;

            for (int i = 0; i < smallPrimesPacked.Length; i++)
            {
                int spp = smallPrimesPacked[i];

                for (int j = 0; j < 8; j++)
                {
                    if ((spp & 1) == 1)
                    {
                        yield return packedDivisor * i + packedModulus[j];
                    }

                    spp >>= 1;
                }
            }

            yield break;
        }

        var isComp = new BitArray(n);
        int p = 1;

        while (++p < n)
        {
            if (isComp[p])
                continue;

            yield return p;
            int q = p * p;

            while (q > 0 && q < n)
            {
                isComp[q] = true;
                q += p;
            }
        }
    }

    private static Nums PrimeSieve(long min, long max)
    {
        int sqrt = (int)Math.Sqrt(max);
        int n = (int)(max - min);
        var isComp = new BitArray(n);

        foreach (int p in PrimeSieve(sqrt + 1))
        {
            int q = (int)(p * (min / p) - min);
            if (q < 0) q += p;

            if (p >= min && !isComp[q])
            {
                yield return q + min;
            }

            while (q < n)
            {
                isComp[q] = true;
                q += p;
            }
        }

        for (int p = 0; p < n; p++)
        {
            if (!isComp[p])
                yield return p + min;
        }
    }

    /// <summary>
    /// Generate all prime numbers less than System.Int64.MaxValue.
    /// </summary>
    /// <returns></returns>
    public static Nums Primes()
    {
        foreach (var n in Primes(2))
        {
            yield return n;
        }
    }

    /// <summary>
    /// Generate all prime numbers less than System.Int32.MaxValue.
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<int> PrimesInt32()
    {
        foreach (var n in Primes(2).TakeWhile(p => p <= int.MaxValue))
        {
            yield return (int)n;
        }
    }

    /// <summary>
    /// Generate prime numbers less than System.Int64.MaxValue.
    /// </summary>
    public static Nums Primes(Num startingWith)
    {
        while (startingWith < long.MaxValue)
        {
            long max = Math.Max(1000000, startingWith + 1000000 * (long)Math.Log(startingWith));
            if (max < 0) max = long.MaxValue;

            foreach (long p in PrimeSieve(startingWith, max))
                yield return p;

            startingWith = max;
        }
    }

    /// <summary>
    /// Generate prime numbers less than System.Int32.MaxValue.
    /// </summary>
    public static IEnumerable<int> PrimesInt32(int startingWith)
    {
        foreach (var n in Primes(startingWith).TakeWhile(p => p <= int.MaxValue))
        {
            yield return (int)n;
        }
    }

    /// <summary>
    /// Determine prime factorization of a number.
    /// </summary>
    public static IEnumerable<Factor<Num>> PrimeFactors(Num n)
    {
        if (n == 1)
            yield break;

        ArgumentOutOfRangeException.ThrowIfLessThan(n, 2);

        foreach (var prime in Primes())
        {
            int count = 0;

            while ((n % prime) == 0)
            {
                count++;
                n /= prime;
            }

            if (count > 0)
                yield return new Factor<Num>(prime, count);

            if (n == 1)
                yield break;

            var s = prime * prime;

            if (s > n || s < 0)
            {
                yield return new Factor<Num>(n, 1);
                yield break;
            }
        }
    }

    private const int packedDivisor = 30;

    private static readonly int[] packedModulus = [1, 7, 11, 13, 17, 19, 23, 29];

    private static readonly byte[] smallPrimesPacked =
    [
        254,223,239,126,182,219,61,249,213,79,30,243,234,
            166,237,158,230,12,211,211,59,221,89,165,106,103,
            146,189,120,30,166,86,86,227,173,45,222,42,76,85,
            217,163,240,159,3,84,161,248,46,253,68,233,102,246,
            19,58,184,76,43,58,69,17,191,84,140,193,122,179,
            200,188,140,79,33,88,113,113,155,193,23,239,84,150,
            26,8,229,131,140,70,114,251,174,101,146,143,88,135,
            210,146,216,129,101,38,227,160,17,56,199,38,60,129,
            235,153,141,81,136,62,36,243,51,77,90,139,28,167,
            42,180,88,76,78,38,246,25,130,220,131,195,44,241,
            56,2,181,205,205,2,178,74,148,12,87,76,122,48,67,
            11,241,203,68,108,36,248,25,1,149,168,92,115,234,
            141,36,150,43,80,166,34,30,196,209,72,6,212,58,
            47,116,156,7,106,5,136,191,104,21,46,96,85,227,
            183,81,152,8,20,134,90,170,69,77,73,112,39,210,
            147,213,202,171,2,131,97,5,36,206,135,34,194,169,
            173,24,140,77,120,209,137,22,176,87,199,98,162,192,
            52,36,82,174,90,64,50,141,33,8,67,52,182,210,182,
            217,25,225,96,103,26,57,96,208,68,122,148,154,9,
            136,131,168,116,85,16,39,161,93,104,30,35,200,50,
            224,25,3,68,115,72,177,56,195,230,42,87,97,152,
            181,28,10,104,197,129,143,172,2,41,26,71,227,148,
            17,78,100,46,20,203,61,220,20,197,6,16,233,41,177,
            130,233,48,71,227,52,25,195,37,10,48,48,180,108,
            193,229,70,68,216,142,76,93,34,36,112,120,146,137,
            129,130,86,38,27,134,233,8,165,0,211,195,41,176,
            194,74,16,178,89,56,161,29,66,96,199,34,39,140,
            200,68,26,198,139,130,129,26,70,16,166,49,9,240,
            84,47,24,210,216,169,21,6,46,12,246,192,14,80,145,
            205,38,193,24,56,101,25,195,86,147,139,42,45,214,
            132,74,97,10,165,44,9,224,118,196,106,60,216,8,232,
            20,102,27,176,164,2,99,54,16,49,7,213,146,72,66,
            18,195,138,160,159,45,116,164,130,133,120,92,13,
            24,176,97,20,29,2,232,24,18,193,1,73,28,131,48,
            103,51,161,136,216,15,12,244,152,136,88,215,102,
            66,71,177,22,168,150,8,24,65,89,21,181,68,42,82,
            225,179,170,161,89,69,98,85,24,17,165,12,163,60,
            103,0,190,84,214,10,32,54,107,130,12,21,8,126,86,
            145,1,120,208,97,10,132,168,44,1,87,14,86,160,80,
            11,152,140,71,108,32,99,16,196,9,228,12,87,136,
            11,117,11,194,82,130,194,57,36,2,44,86,37,122,49,
            41,214,163,32,225,177,24,176,12,138,50,193,17,50,
            9,197,173,48,55,8,188,145,130,207,32,37,107,156,
            48,143,68,38,70,106,7,73,142,9,88,16,2,37,197,
            196,66,90,128,160,128,60,144,40,100,20,225,3,132,
            81,12,46,163,138,164,8,192,71,126,211,43,3,205,
            84,42,0,4,179,146,108,66,41,76,131,193,146,204,
            28,45,70,33,219,56,89,132,140,36,18,88,187,224,
            6,13,112,48,201,9,40,145,65,68,50,249,140,48,128,
            194,114,164,98,12,125,129,131,20,225,170,14,21,130,
            10,120,20,112,151,8,16,111,46,240,178,29,48,73,68,
            50,83,98,134,101,69,132,10,17,75,54,217,140,105,
            58,97,128,144,124,25,192,48,149,64,139,12,5,45,
            14,192,113,161,180,150,133,26,22,192,21,20,81,76,
            72,183,121,149,16,137,138,46,2,161,28,213,144,129,
            16,145,8,34,180,30,233,120,192,51,32,92,139,196,
            14,226,170,35,16,71,227,40,85,123,25,161,81,168,
            6,4,144,130,44,209,97,96,161,82,6,65,68,129,84,
            35,136,24,169,4,39,34,114,72,182,64,28,66,18,23,
            202,41,160,90,1,60,225,82,132,137,218,1,64,6,241,
            44,105,210,72,66,99,193,33,52,138,200,108,33,113,
            38,9,136,46,68,213,40,146,29,152,134,18,209,82,
            163,160,70,32,70,20,80,131,220,221,131,80,68,169,
            140,77,20,46,84,20,137,27,57,66,128,20,129,41,143,
            128,2,3,50,150,130,131,176,5,12,20,81,136,7,248,
            217,236,26,6,72,49,92,67,134,6,35,179,9,68,12,
            165,0,240,178,128,1,202,225,66,194,24,182,44,155,
            199,46,82,41,44,97,26,138,38,36,139,132,128,94,
            96,2,193,83,42,53,69,33,82,209,66,184,197,74,72,
            4,22,112,35,97,193,9,80,196,129,136,24,217,1,22,
            34,40,140,144,132,235,56,36,83,9,169,65,106,20,
            130,9,134,48,156,162,56,66,0,43,149,90,2,74,193,
            162,173,69,4,39,84,192,40,169,5,140,46,110,96,243,
            22,144,27,130,30,0,128,38,28,29,74,20,194,26,34,
            60,133,193,16,176,72,17,0,74,197,48,244,170,48,
            140,200,73,24,7,240,28,233,7,205,12,34,27,28,236,
            194,69,10,80,58,32,228,14,108,32,130,195,145,217,
            200,98,76,209,128,133,101,9,2,48,149,249,16,105,
            2,166,74,100,194,182,249,22,32,72,161,115,148,49,
            84,97,68,7,3,130,44,6,0,56,51,9,28,193,75,206,
            18,53,65,164,144,153,45,42,192,89,132,209,74,164,
            114,4,34,60,84,194,160,12,1,24,172,172,150,228,4,
            4,3,147,5,155,72,68,99,50,1,49,95,96,92,2,0,3,
            128,209,4,46,52,169,48,73,196,202,30,2,75,50,20,
            5,134,34,197,194,42,76,200,0,8,243,24,56,101,153,
            130,74,84,99,161,148,143,68,18,147,233,25,40,202,
            161,6,18,138,32,165,81,72,24,112,49,174,144,8,67,
            104,151,50,175,145,200,10,44,2,2,140,12,81,165,18,
            0,27,30,129,138,8,40,96,210,134,52,68,99,118,4,
            67,0,241,1,4,64,54,200,161,140,195,206,50,66,9,
            41,85,68,78,40,67,96,28,160,218,40,14,165,242,5,
            64,89,140,78,162,96,5,224,5,197,4,98,129,38,168,
            138,164,36,180,211,26,129,66,228,96,52,8,42,57,
            204,98,8,71,208,0,64,7,2,120,32,217,162,137,80,
            35,50,0,184,128,100,17,233,116,98,10,170,68,8,139,
            22,6,24,45,241,76,79,42,33,64,21,180,12,72,34,
            209,225,128,216,23,167,20,160,130,21,100,192,137,
            90,17,187,140,120,8,205,4,36,162,155,156,16,204,
            66,163,40,59,88,95,166,64,144,35,4,44,210,174,82,
            81,26,164,105,128,193,74,163,200,144,25,72,66,36,
            34,67,2,69,193,5,0,116,17,21,148,16,12,56,115,
            138,37,4,7,40,126,1,64,130,65,72,109,22,54,41,49,
            229,129,164,28,129,250,9,0,20,45,16,96,64,151,220,
            136,2,78,23,152,133,68,156,162,96,145,138,146,104,
            25,10,64,81,128,55,132,21,64,98,100,178,14,145,72,
            42,0,85,72,153,176,8,133,26,19,16,155,0,94,201,
            80,0,232,164,0,131,69,108,64,11,157,160,2,45,52,
            146,1,33,129,23,163,70,160,138,133,232,136,71,24,
            150,1,182,65,147,132,8,2,24,2,53,80,228,58,129,
            16,17,161,218,130,76,52,50,133,0,87,75,40,165,168,
            56,132,29,14,48,179,80,34,65,195,198,6,208,32,40,
            161,5,138,26,132,34,133,12,70,68,114,144,200,23,
            216,65,97,100,48,57,0,12,151,163,14,178,41,2,80,
            136,169,92,34,193,142,144,195,71,64,49,18,44,56,
            140,99,100,134,192,179,0,74,42,56,17,145,19,56,71,
            73,20,129,115,2,44,76,142,82,229,209,16,16,7,76,
            6,101,97,26,84,132,1,74,226,1,47,16,6,9,66,68,154,
            0,169,82,140,40,148,97,1,153,5,32,28,35,120,162,
            81,24,130,60,65,208,183,196,67,135,68,198,10,147,
            104,193,129,48,82,178,168,193,138,36,28,33,3,51,
            65,20,74,46,65,226,130,69,1,172,6,64,24,32,149,
            76,103,32,38,80,148,85,146,8,26,130,225,140,16,
            91,97,2,134,114,153,48,141,6,44,176,161,24,40,6,
            39,70,32,24,130,33,156,235,24,196,48,129,88,193,
            132,6,55,154,36,80,13,162,64,5,41,25,9,2,132,82,
            100,17,23,236,209,32,120,134,99,14,209,17,42,96,
            3,209,34,96,132,6,50,164,104,36,129,199,192,18,
            66,1,187,168,0,65,124,65,33,129,145,16,133,20,160,
            168,45,92,21,71,64,208,250,42,132,14,143,16,210,
            128,9,212,73,33,38,50,176,56,32,65,160,74,17,75,
            186,172,78,32,52,97,96,21,13,19,106,112,67,66,156,
            45,85,232,0,150,152,146,228,200,76,12,165,25,129,
            49,202,66,2,18,74,48,48,81,1,102,162,226,22,65,
            207,160,42,132,81,24,24,65,198,86,160,33,142,225,
            150,11,4,96,17,184,76,147,13,6,4,42,42,76,4,134,
            42,81,81,49,0,1,128,118,81,152,148,45,204,74,106,
            164,66,178,105,72,1,64,181,210,50,92,14,98,32,22,
            1,55,228,193,194,88,36,35,8,136,2,36,20,133,152,
            6,208,29,170,52,128,10,52,97,16,75,72,66,194,2,
            152,148,0,32,87,179,161,136,26,206,8,5,160,176,
            121,74,1,20,70,3,36,36,141,0,68,65,65,3,108,138,
            193,2,193,89,38,216,65,104,86,83,67,42,20,4,40,
            10,37,32,138,148,71,131,26,133,160,15,132,213,8,
            64,112,25,6,8,81,128,90,22,168,8,85,210,40,24,
            65,73,9,133,2,101,80,180,128,61,128,129,42,74,134,
            128,48,1,3,134,28,83,147,163,97,88,42,84,33,178,
            151,176,134,171,82,68,233,136,89,0,139,32,16,210,
            24,24,133,8,28,49,80,3,100,140,10,64,97,192,12,168,
            8,64,50,101,11,156,136,2,66,76,20,96,52,133,19,
            33,80,113,72,161,229,20,110,72,32,50,138,24,24,
            69,106,50,32,130,1,
        ];
}
