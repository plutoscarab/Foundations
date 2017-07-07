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
    public sealed class TarWriter
    {
        Stream stream;
        byte[] zeros = new byte[TarReader.BlockSize];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public TarWriter(Stream stream)
        {
            this.stream = stream;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void WriteDirectoryName(string name)
        {
            name = name.Replace("\\", "/");

            if (name.Length < 100)
            {
                WriteDirectoryName("", name);
            }
            else
            {
                for (int i = 0; i < name.Length; i++)
                {
                    if (name[i] == '/')
                    {
                        if (name.Length - i < 100)
                        {
                            WriteDirectoryName(name.Substring(0, i), name.Substring(i + 1));
                            return;
                        }
                    }
                }

                throw new ArgumentException($"Directory name {name} is too long.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        public void WriteDirectoryName(string prefix, string name)
        {
            if (prefix.Length >= 155)
                throw new ArgumentException($"Prefix {prefix} is too long.", nameof(prefix));

            if (name.Length >= 100)
                throw new ArgumentException($"Name {name} is too long.", nameof(name));

            Write(name, 100);
            Write(0, 8);    // mode
            Write(0, 8);    // uid
            Write(0, 8);    // gid
            Write(0, 12);   // size
            long mtime = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            Write(mtime, 12);
            Write(0, 8);    // checksum
            Write('5');
            Write("", 100); // linkname
            Write("ustar", 6);
            Write("00", 2); // version
            Write("", 32);  // uname
            Write("", 32);  // gname
            Write(0, 8);    // devmajor
            Write(0, 8);    // devminor
            Write(prefix, 155);
            Write("", 12);  // pad
        }

        private void Write(char c)
        {
            var s = c.ToString();
            Write(s, 1);
        }

        private void Write(long n, int length)
        {
            var s = Convert.ToString(n, 8);
            Write(s, length);
        }

        private void Write(string s, int length)
        {
            var bytes = ASCIIEncoding.Default.GetBytes(s);
            stream.Write(bytes, 0, bytes.Length);
            stream.Write(zeros, 0, length - bytes.Length);
        }
    }
}
