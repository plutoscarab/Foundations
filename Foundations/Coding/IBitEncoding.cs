
namespace Foundations.Coding;

/// <summary>
/// Encode a <see cref="System.Int32"/> as a <see cref="Code"/>,
/// or read an encoded <see cref="System.Int32"/> from a <see cref="BitReader"/>.
/// </summary>
public interface IBitEncoding
{
    /// <summary>
    /// Gets the code corresponding to a value.
    /// </summary>
    Code GetCode(int value);

    /// <summary>
    /// Gets the value corresponding to a code.
    /// </summary>
    int Read(BitReader reader);

    /// <summary>
    /// Gets the minimum value that can be encoded.
    /// </summary>
    int MinEncodable { get; }

    /// <summary>
    /// Gets the maximum value that can be encoded.
    /// </summary>
    int MaxEncodable { get; }
}

/// <summary>
/// Extension methods for <see cref="IBitEncoding"/>.
/// </summary>
public static class IBitEncodingExtensions
{
    /// <summary>
    /// Use this <see cref="IBitEncoding"/> to write a <see cref="System.Int32"/> value
    /// to the specified <see cref="BitWriter"/>.
    /// </summary>
    public static void Write(this IBitEncoding encoding, BitWriter writer, int value)
    {
        writer.Write(encoding, value);
    }
}
