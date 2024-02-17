
using System;
using System.Numerics;

namespace Foundations.Algebra;

public class RingZ : Ring<BigInteger>
{
    public static readonly RingZ Instance = new();

    private RingZ()
    {}

    public override BigInteger Zero => BigInteger.Zero;

    public override bool ElementsHaveSign => true;

    public override int Sign(BigInteger x) => x.Sign;

    public override BigInteger One => BigInteger.One;

    public override BigInteger Add(BigInteger a, BigInteger b) => a + b;

    public override bool Equals(Ring<BigInteger> other) => other is not null;

    public override BigInteger Multiply(BigInteger a, BigInteger b) => a * b;

    public override BigInteger Negate(BigInteger x) => -x;

    public override BigInteger RandomSmallNonzeroElement(Random rand)
    {
        while (true)
        {
            var x = 1 - 2 * Math.Log(rand.NextDouble());
            if (x <= int.MaxValue) return (BigInteger)(int)x * (rand.Next(2) * 2 - 1);
        }
    }

    public override bool HasIntegerRepresentation => true;

    public override BigInteger FromInteger(BigInteger n) => n;
}