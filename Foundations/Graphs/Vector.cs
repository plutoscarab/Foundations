
using System;

namespace Foundations.Graphs.DOT;

internal record Vector(double X, double Y)
{
    public static readonly Vector Zero = new(0, 0);

    public static Vector operator *(Vector v, double d) => new(v.X * d, v.Y * d);

    public static Vector operator /(Vector v, double d) => new(v.X / d, v.Y / d);

    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);

    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);

    public static double operator *(Vector a, Vector b) => a.X * b.X + a.Y * b.Y;

    public double SqrMod => X * X + Y * Y;

    public double Length => Math.Sqrt(SqrMod);
}
