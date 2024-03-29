﻿
using System.IO;

namespace Foundations.RandomNumbers;

/// <summary>
/// A <see cref="System.IO.Stream"/> of random bytes.
/// </summary>
public sealed class RandomStream(Generator generator) : Stream
{
    private readonly Generator generator = generator;
    private long position;

    /// <summary>
    /// Implementation of <see cref="Stream.CanRead"/>. Returns true.
    /// </summary>
    public override bool CanRead
    {
        get { return true; }
    }

    /// <summary>
    /// Implementation of <see cref="Stream.CanSeek"/>. Returns false.
    /// </summary>
    public override bool CanSeek
    {
        get { return false; }
    }

    /// <summary>
    /// Implementation of <see cref="Stream.CanWrite"/>. Returns false.
    /// </summary>
    public override bool CanWrite
    {
        get { return false; }
    }

    /// <summary>
    /// Implementation of <see cref="Stream.Length"/>. Throws <see cref="NotSupportedException"/>.
    /// </summary>
    public override long Length
    {
        get { throw new NotSupportedException(); }
    }

    /// <summary>
    /// Implementation of <see cref="Stream.Position"/>.
    /// </summary>
    public override long Position
    {
        get { return position; }
        set { throw new NotSupportedException(); }
    }

    /// <summary>
    /// Implementation of <see cref="Stream.Flush"/>.
    /// </summary>
    public override void Flush()
    {
    }

    /// <summary>
    /// Implementation of <see cref="Stream.Flush"/>.
    /// </summary>
    public override int Read(byte[] buffer, int offset, int count)
    {
        generator.Fill(buffer, offset, count);
        position += count;
        return count;
    }

    /// <summary>
    /// Implementation of <see cref="Stream.Seek"/>. Throw <see cref="NotSupportedException"/>.
    /// </summary>
    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Implementation of <see cref="Stream.SetLength"/>. Throw <see cref="NotSupportedException"/>.
    /// </summary>
    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Implementation of <see cref="Stream.Write"/>. Throw <see cref="NotSupportedException"/>.
    /// </summary>
    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }
}
