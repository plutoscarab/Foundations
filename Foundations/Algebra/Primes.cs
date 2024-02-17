
using System;
using System.Collections.Generic;
using Foundations.Collections;

namespace Foundations.Algebra;

public class Primes
{
    public static bool Contains(int n)
    {
        if (n < 2) return false;

        foreach (var p in Sequences.PrimesInt32())
        {
            if (p * p > n)
                return true;

            if ((n % p) == 0)
                return false;
        }

        throw new NotImplementedException();
    }

    public static IEnumerable<(int Prime, long Order)> Powers()
    {
        var heap = new MinTree<long, int>();

        foreach (var p in Sequences.PrimesInt32())
        {
            while (!heap.IsEmpty && heap.RootKey < p)
            {
                var kv = heap.Pop();
                yield return (kv.Value, kv.Key);
                heap.Add(kv.Key * kv.Value, kv.Value);
            }

            yield return (p, p);

            if (p > long.MaxValue / p)
                break;

            heap.Add((long)p * p, p);
        }

        while (!heap.IsEmpty)
        {
            var kv = heap.Pop();
            yield return (kv.Value, kv.Key);

            if (kv.Key < long.MaxValue / kv.Value)
            {
                heap.Add(kv.Key * kv.Value, kv.Value);
            }
        }
    }

    public static bool IsPrimePower(long n, out int prime, out int k)
    {
        prime = k = 0;

        foreach (var p in Sequences.PrimesInt32())
        {
            prime = p;

            if (n == p)
            {
                k = 1;
                return true;
            }

            if (p * p > n)
                continue;

            if (p > n)
                return false;

            k = 0;
            var o = n;

            while ((o % p) == 0)
            {
                o /= p;
                ++k;
            }

            if (o == 1 && k > 0)
                return true;

            if (o != n)
                return false;
        }

        return false;
    }
}

