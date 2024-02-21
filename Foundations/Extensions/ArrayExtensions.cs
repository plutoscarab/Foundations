
namespace Foundations;

/// <summary>
/// Extension methods for <see cref="Array"/> types.
/// </summary>
public static partial class ArrayExtensions
{
    /// <summary>
    /// Gets the contents of this array as an array of <see cref="Byte"/>s.
    /// </summary>
    public static byte[] GetBytes<T>(this T[] array)
    {
        return GetBytes((Array)array);
    }

    /// <summary>
    /// Gets the contents of this array as an array of <see cref="Byte"/>s.
    /// </summary>
    public static byte[] GetBytes(this Array array)
    {
        ArgumentNullException.ThrowIfNull(array, nameof(array));

        int n = Buffer.ByteLength(array);
        var bytes = new byte[n];
        Buffer.BlockCopy(array, 0, bytes, 0, n);
        return bytes;
    }
}
