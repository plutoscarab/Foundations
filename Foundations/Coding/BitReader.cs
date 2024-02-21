
using System.IO;

namespace Foundations.Coding;

/// <summary>
/// Reads a byte array as a stream of groups of bits.
/// </summary>
public sealed partial class BitReader
{
    static readonly byte[] mask = Enumerable.Range(0, 9).Select(i => (byte)(0xFF >> (8 - i))).ToArray();

    readonly byte[] buffer;
    int position;
    int shift;

    /// <summary>
    /// Create a <see cref="BitReader"/>.
    /// </summary>
    /// <param name="buffer">
    /// The bits to read. Bits are assumed to be packed big-endian into bytes.
    /// The array is read starting from its first element.
    /// </param>
    public BitReader(byte[] buffer)
        : this(buffer, 0, 0)
    {
    }

    /// <summary>
    /// Create a <see cref="BitReader"/>. 
    /// </summary>
    /// <param name="buffer">The bits to read. Bits are assumed to be packed big-endian into bytes.</param>
    /// <param name="byteOffset">The position to start reading.</param>
    public BitReader(byte[] buffer, int byteOffset)
        : this(buffer, byteOffset, 0)
    {
    }

    /// <summary>
    /// Create a <see cref="BitReader"/>. 
    /// </summary>
    /// <param name="buffer">The bits to read. Bits are assumed to be packed big-endian into bytes.</param>
    /// <param name="byteOffset">The position to start reading.</param>
    /// <param name="bitOffset">The number of bits to skip in the first byte, from 0 to 7.</param>
    public BitReader(byte[] buffer, int byteOffset, int bitOffset)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (byteOffset < 0 || byteOffset > buffer.Length) throw new ArgumentOutOfRangeException(nameof(byteOffset));
        if (bitOffset < 0 || bitOffset > 7) throw new ArgumentOutOfRangeException(nameof(bitOffset));
        this.buffer = buffer;
        position = byteOffset;
        shift = bitOffset;
    }

    /// <summary>
    /// Reads a <see cref="System.Int32"/> value using the specified <see cref="IBitEncoding"/>.
    /// </summary>
    public int Read(IBitEncoding encoding)
    {
        return encoding.Read(this);
    }

    /// <summary>
    /// Reads bits.
    /// </summary>
    /// <param name="count">The number of bits to read.</param>
    /// <returns>Returns the bits, with the last bit read placed in the LSB of the result. MSBs are 0.</returns>
    public ulong Read(int count)
    {
        if (count < 0 || count > 64) throw new ArgumentOutOfRangeException(nameof(count));
        ulong bits = 0;
        int length = 0;

        while (count > 0)
        {
            int toRead = Math.Min(count, 8 - shift);
            if (position >= buffer.Length) throw new EndOfStreamException();
            bits <<= toRead;
            bits |= ((uint)buffer[position] >> (8 - shift - toRead)) & mask[toRead];
            length += toRead;
            shift += toRead;

            if (shift == 8)
            {
                position++;
                shift = 0;
            }

            count -= toRead;
        }

        return bits;
    }
}
