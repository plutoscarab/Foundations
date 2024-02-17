
using System;
using System.Numerics;

namespace Foundations.Algebra;

public abstract class Ring<T> : IEquatable<Ring<T>>
    where T : IEquatable<T>
{
    public abstract T Zero { get; }
    public abstract bool ElementsHaveSign { get; }
    public virtual int Sign(T x) => 0;
    public abstract T Add(T a, T b);
    public abstract T Negate(T x);
    public virtual T Subtract(T a, T b) => Add(a, Negate(b));
    public abstract T One { get; }
    public abstract T Multiply(T a, T b);
    public Group<T> AdditiveGroup => new(Zero, Add, Negate);
    public abstract bool Equals(Ring<T> other);
    public virtual T RandomSmallNonzeroElement(Random rand) => One;
    public abstract bool HasIntegerRepresentation { get; }
    public virtual T FromInteger(BigInteger n) => default!;

    public virtual T Pow(T value, int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException();

        if (n == 0)
            return One;

        if (n == 1)
            return value;

        if ((n & 1) == 0)
        {
            var h = Pow(value, n / 2);
            return Multiply(h, h);
        }

        return Multiply(value, Pow(value, n - 1));
    }
}
