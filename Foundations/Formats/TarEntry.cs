using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundations.Formats
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TarEntry : TarHeader
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly long Position;

        /// <summary>
        /// 
        /// </summary>
        public Stream Stream { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <param name="position"></param>
        public TarEntry(byte[] block, long position)
            : base(block)
        {
            Position = position;
        }
    }
}
