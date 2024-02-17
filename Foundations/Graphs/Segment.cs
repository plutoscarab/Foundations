
namespace Foundations.Graphs.DOT;

internal record Segment(Vector A, Vector B)
{
    public bool Intersects(Segment other)
    {
        var (C, D) = (other.A, other.B);
        var ab = B - A;
        var cd = D - C;

        var det = ab.Y * cd.X - ab.X * cd.Y;
        var t1 = ((A.X - C.X) * cd.Y + (C.Y - A.Y) * cd.X) / det;
        var t2 = ((A.X - C.X) * ab.Y + (C.Y - A.Y) * ab.X) / det;

        if (double.IsInfinity(t1) || double.IsInfinity(t2))
            return false;

        return t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1;
    }

    public bool Intersects(Segment other, out Vector where)
    {
        var (C, D) = (other.A, other.B);
        var ab = B - A;
        var cd = D - C;

        var det = ab.Y * cd.X - ab.X * cd.Y;
        var t1 = ((A.X - C.X) * cd.Y + (C.Y - A.Y) * cd.X) / det;
        var t2 = ((A.X - C.X) * ab.Y + (C.Y - A.Y) * ab.X) / det;

        if (double.IsInfinity(t1) || double.IsInfinity(t2))
        {
            where = A;
            return false;
        }

        where = A + ab * t1;
        return t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1;
    }
}
