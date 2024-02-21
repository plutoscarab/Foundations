
namespace Foundations.Collections;

/// <summary>
/// Sorted tree.
/// </summary>
public class SortedTree<TKey, TValue> : Tree<TKey, TValue>
    where TValue : IEquatable<TValue>
    where TKey : IComparable<TKey>
{
    /// <summary>
    /// Creates a <see cref="SortedTree{TKey, TValue}"/>
    /// with the specified key selection function to determine
    /// which key is "first".
    /// </summary>
    public SortedTree(Func<TKey, TKey, TKey> selector)
        : base(selector)
    {
    }

    /// <summary>
    /// Gets a value from the top of the tree and removes it.
    /// </summary>
    /// <returns></returns>
    public TValue PopValue()
    {
        return Pop().Value;
    }

    /// <summary>
    /// Gets a key/value pair from the top of the tree and removes it.
    /// </summary>
    /// <returns></returns>
    public KeyValuePair<TKey, TValue> Pop()
    {
        if (keys.Count == 0)
            throw new InvalidOperationException();

        TKey key = keys[0];
        int index = 0;

        while (index * 2 + 2 < keys.Count)
        {
            index = index * 2 + 1;

            if (keys[index].CompareTo(key) != 0)
                index++;
        }

        TValue value = values[index];
        Remove(value);
        return new KeyValuePair<TKey, TValue>(key, value);
    }
}
