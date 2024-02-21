
namespace Foundations.RandomNumbers;

/// <summary>
/// Extension methods for <see cref="IRandomSource"/>.
/// </summary>
public static class RandomSourceExtensions
{
    /// <summary>
    /// Creates a thread-safe wrapper for an <see cref="IRandomSource"/>.
    /// </summary>
    /// <param name="source"></param>
    public static IRandomSource Synchronized(this IRandomSource source)
    {
        return new SynchronizedRandomSource(source);
    }
}
