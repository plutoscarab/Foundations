
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundations.Bijections;

public partial struct Nat
{
    public static Nat FromRational(Nat p, Nat q)
    {
        if (q.IsZero) throw new ArgumentOutOfRangeException(nameof(q));

        List<byte> arr = [];
        var i = 0;
        var b = 1;
        var byt = 0;

        while (true)
        {
            var (div, rem) = Nat.DivRem(p, q);

            for (var d = 0; d < div; d++)
            {
                byt |= b << i;
                ++i;

                if (i == 8)
                {
                    arr.Add((byte)byt);
                    byt = 0;
                    i = 0;
                }
            }

            b ^= 1;

            if (rem.IsZero)
                break;

            (p, q) = (q, rem);
        }

        if (b == 1)
        {
            if (i == 0)
                arr[^1] |= 0x80;
            else
                byt |= b << (i - 1);
        }

        arr.Add((byte)byt);

        if (byt >= 0x80)
            arr.Add(0);

        return new([.. arr]);
    }

    public readonly (Nat, Nat) ToRational()
    {
        var last = 1;
        var k = 0;
        int a = 0, b = 1, c = 1, d = 0;

        foreach (var item in ToByteArray())
        {
            for (var i = 0; i < 8; i++)
            {
                var p = (item >> i) & 1;

                if (p != last)
                {
                    (a, b) = (b, b * k + a);
                    (c, d) = (d, d * k + c);
                    last ^= 1;
                    k = 0;
                }

                ++k;
            }
        }

        if (last == 1)
        {
            (a, b) = (b, b * k + a);
            (c, d) = (d, d * k + c);
        }

        return (b, d);
    }

    public static IEnumerable<(Nat, Nat)> AllRationals() =>
        All().Select(n => n.ToRational());
}