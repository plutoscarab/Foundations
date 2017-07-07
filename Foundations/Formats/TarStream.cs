using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundations.IO;

namespace Foundations.Formats
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class TarStream : ReadOnceStream
    {
        Stream stream;
        long length;
        long paddedSize;
        long remaining;
        bool closed;

        public TarStream(Stream stream, long length, long paddedSize)
        {
            this.stream = stream;
            this.length = length;
            this.paddedSize = paddedSize;
            remaining = length;
        }

        protected override long GetLength() => length;

        protected override long GetPosition()
        {
            throw new NotSupportedException();
        }

        protected override int ReadBytes(byte[] buffer, int offset, int count)
        {
            int toRead = (int)Math.Min(remaining, count);
            int result = stream.Read(buffer, offset, toRead);
            remaining -= result;
            return result;
        }

        public override void Close()
        {
            if (closed) return;
            closed = true;
            remaining += paddedSize - length;

            if (stream.CanSeek)
            {
                stream.Position += remaining;
            }
            else
            {
                var buffer = new byte[TarReader.BlockSize * (84999 / TarReader.BlockSize)];

                while (remaining > 0)
                {
                    int toRead = (int)Math.Min(remaining, buffer.Length);
                    int n = stream.Read(buffer, 0, toRead);
                    if (n == 0) break;
                    remaining -= n;
                }

                if (remaining > 0)
                    throw new EndOfStreamException();
            }
        }

        protected override void Dispose(bool disposing)
        {
            Close();
        }
    }
}
