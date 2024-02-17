
using System.Linq;

namespace Foundations.Algebra;

public class BinaryGF : ExtensionGF
{
    private readonly ExtensionGF ext;

    private readonly long mod;
    private readonly long[] powers;

    public BinaryGF(int bits) : base(2, bits)
    {
        ext = new ExtensionGF(2, bits);
        mod = 0;

        foreach (var i in ext.Modulus.Reverse())
        {
            mod = mod * 2 + i.Value;
        }

        powers = new long[64];
        var p = mod & (Order - 1);

        for (var i = bits; i < 64; i++)
        {
            powers[i] = p;
            p <<= 1;
            if (p >= Order) p ^= mod;
        }
    }

    public override FFValue Add(FFValue a, FFValue b) => Element(a.Checked(this) ^ b.Checked(this));

    public override FFValue Multiply(FFValue a, FFValue b)
    {
        var av = a.Checked(this);
        var bv = b.Checked(this);

        if (av == 0 || bv == 0)
            return Zero;

        if (av == 1)
            return b;

        if (bv == 1)
            return a;

        if (av < bv)
            (av, bv) = (bv, av);

        long p = 0;
        var ap = av;
        var q = bv;

        while (q != 0)
        {
            if ((q & 1) != 0)
            {
                p ^= ap;
            }

            ap <<= 1;
            q >>= 1;
        }

        if (p >= Order)
        {
            q = p >> Degree;
            var i = Degree;

            while (q != 0)
            {
                if ((q & 1) != 0)
                {
                    p ^= powers[i];
                }

                q >>= 1;
                ++i;
            }

            p &= Order - 1;
        }

        return Element(p);
    }

    public override FFValue Negate(FFValue x) => x;
}
