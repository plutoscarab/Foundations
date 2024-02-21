
namespace Foundations.Collections;

/// <summary>
/// Sorted tree with maximum key at the top.
/// </summary>
public class MaxTree<TKey, TValue> : SortedTree<TKey, TValue>
    where TValue : IEquatable<TValue>
    where TKey : IComparable<TKey>
{
    private static TKey Max(TKey x, TKey y)
    {
        if (x.CompareTo(y) > 0)
            return x;

        return y;
    }

    /// <summary>
    /// Creates a <see cref="Max(TKey, TKey)"/>.
    /// </summary>
    public MaxTree()
        : base((x, y) => Max(x, y))
    {
    }
}
