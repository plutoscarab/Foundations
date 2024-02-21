
namespace Foundations.Collections;

/// <summary>
/// A pool of reusable instances of items that are expensive to create,
/// expensive to garbage-collect, or prone to cause heap fragmentation.
/// </summary>
public sealed class Pool<T>
{
    private static readonly ConcurrentBag<T> pool = [];

    /// <summary>
    /// Borrows an item from the pool. Use <see cref="Return"/> to return the item.
    /// If the pool is empty, use the constructor function to create a new instance.
    /// </summary>
    public T Borrow(Func<T> constructor) =>
        pool.TryTake(out T item) ? item : constructor();

    /// <summary>
    /// Returns an item to the pool.
    /// </summary>
    public void Return(T item)
    {
        pool.Add(item);
    }
}
