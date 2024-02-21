
using System.Threading.Tasks;

namespace Foundations;

/// <summary>
/// Extensions for IEnumerable{T}.
/// </summary>
public static partial class IEnumerableExtensions
{
    /// <summary>
    /// Copy items to an array.
    /// </summary>
    public static void CopyTo<T>(this IEnumerable<T> items, T[] array)
    {
        items.CopyTo(array, 0, array.Length);
    }

    /// <summary>
    /// Copy items to an array.
    /// </summary>
    public static void CopyTo<T>(this IEnumerable<T> items, T[] array, int arrayIndex)
    {
        items.CopyTo(array, arrayIndex, array.Length);
    }

    /// <summary>
    /// Copy items to an array.
    /// </summary>
    public static void CopyTo<T>(this IEnumerable<T> items, T[] array, int arrayIndex, int count)
    {
        foreach (var item in items.Take(count))
        {
            array[arrayIndex++] = item;
        }
    }

    /// <summary>
    /// Perform an action using each item in a collection.
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        if (action == null)
            throw new ArgumentNullException(nameof(action));

        foreach (var item in items)
        {
            action(item);
        }
    }

    /// <summary>
    /// Perform an action using each item in a collection.
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> items, Action<T, int> action)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        if (action == null)
            throw new ArgumentNullException(nameof(action));

        int index = 0;

        foreach (var item in items)
        {
            action(item, index++);
        }
    }

    /// <summary>
    /// Perform an asynchronous action using each item in a collection.
    /// </summary>
    public static async Task ForEachAsync<T>(this IEnumerable<T> items, Func<T, Task> asyncAction)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        if (asyncAction == null)
            throw new ArgumentNullException(nameof(asyncAction));

        foreach (var item in items)
        {
            await asyncAction(item);
        }
    }

    /// <summary>
    /// Perform an asynchronous action using each item in a collection.
    /// </summary>
    public static async Task ForEachAsync<T>(this IEnumerable<T> items, Func<T, int, Task> asyncAction)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        if (asyncAction == null)
            throw new ArgumentNullException(nameof(asyncAction));

        int index = 0;

        foreach (var item in items)
        {
            await asyncAction(item, index++);
        }
    }

    /// <summary>
    /// Perform an asynchronous action using each item in a collection.
    /// </summary>
    public static Task ForAllAsync<T>(this IEnumerable<T> items, int degreeOfParallelism, Func<T, Task> asyncAction)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        if (degreeOfParallelism < 1)
            throw new ArgumentOutOfRangeException(nameof(degreeOfParallelism));

        if (asyncAction == null)
            throw new ArgumentNullException(nameof(asyncAction));

        return Task.WhenAll(
            Partitioner.Create(items, EnumerablePartitionerOptions.NoBuffering)
                .GetPartitions(degreeOfParallelism)
                .Select(async partition =>
                {
                    using (partition)
                    {
                        while (partition.MoveNext())
                        {
                            await asyncAction(partition.Current);
                        }
                    }
                }));
    }
}
