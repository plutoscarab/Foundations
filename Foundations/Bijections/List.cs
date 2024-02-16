
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundations.Bijections;

public partial struct Nat
{
    const int DefaultDilution = 10;

    public Nat(List<Nat> list, int dilution = DefaultDilution)
    {
        if (dilution < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(dilution));
        }

        if (list.Count == 0)
        {
            b = 0;
            return;
        }

        // Create Nat is if list were tuple.
        var n = FromTuple([.. list]);

        // Add unary-encoded length.
        b = n.b * 2 + 1;
        b <<= list.Count - 1;
    }

    public readonly List<Nat> ToList(int dilution = DefaultDilution)
    {
        if (dilution < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(dilution));
        }

        if (IsZero) return [];

        // Get unary-encoded length.
        var len = 1;
        var m = this;

        while (m.IsEven)
        {
            len++;
            m >>= 1;
        }

        m >>= 1;
        
        // Decode tuple.
        return [.. m.ToTuple(len)];
    }

    public readonly List<T> ToList<T>(Func<Nat, T> selector, int dilution = DefaultDilution) =>
        ToList(dilution).Select(selector).ToList();
}