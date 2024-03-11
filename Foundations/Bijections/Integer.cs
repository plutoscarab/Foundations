
namespace Foundations.Types;

public partial struct Nat
{
    /// <summary>
    /// Create a bijective natural number from an integer.
    /// </summary>
    public static Nat FromSigned(BigInteger n) => new(n.Sign < 0 ? -2 * n - 1 : 2 * n);

    /// <summary>
    /// Create a bijective integer from a natural number.
    /// </summary>
    public readonly BigInteger ToSigned() => IsEven ? b / 2 : -1 - b / 2;

    /// <summary>
    /// Enumerate all integers in the order of the natural number bijection,
    /// i.e. 0, -1, 1, -2, 2, -3, ...
    /// </summary>
    public static IEnumerable<BigInteger> AllSigned() =>
        All().Select(n => n.ToSigned());
}
