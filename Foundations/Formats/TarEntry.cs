using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundations.Formats
{
    /// <summary>
    /// An entry in a TAR file.
    /// </summary>
    public sealed class TarEntry : TarHeader
    {
        /// <summary>
        /// The byte position of the entry within the TAR file.
        /// </summary>
        public readonly long Position;

        /// <summary>
        /// A read-only stream representing the contents of this TAR entry.
        /// </summary>
        public Stream Stream { get; internal set; }

        /// <summary>
        /// Creates a new TAR entry.
        /// </summary>
        /// <param name="block">The contents of the TAR entry.</param>
        /// <param name="position">The byte position of the TAR entry within the TAR file.</param>
        internal TarEntry(byte[] block, long position)
            : base(block)
        {
            Position = position;
        }
    }
}
