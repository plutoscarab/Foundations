
namespace Foundations.Algebra;

public abstract class Field<T> : Ring<T>
    where T : IEquatable<T>
{
    public abstract T Invert(T x);
    public virtual T Divide(T a, T b) => Multiply(a, Invert(b));
    internal abstract string ValueStr(T value);
    public Group<T> MultiplicativeGroup => new(One, Multiply, Invert);
}
