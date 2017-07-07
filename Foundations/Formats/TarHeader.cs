using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundations.Formats
{
    /// <summary>
    /// 
    /// </summary>
    public class TarHeader
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// 
        /// </summary>
        public readonly int? Mode;

        /// <summary>
        /// 
        /// </summary>
        public readonly int? UID;

        /// <summary>
        /// 
        /// </summary>
        public readonly int? GID;

        /// <summary>
        /// 
        /// </summary>
        public readonly long? Size;

        /// <summary>
        /// 
        /// </summary>
        public readonly DateTime? MTime;

        /// <summary>
        /// 
        /// </summary>
        public readonly int? Checksum;

        /// <summary>
        /// 
        /// </summary>
        public readonly char TypeFlag;

        /// <summary>
        /// 
        /// </summary>
        public readonly string LinkName;

        /// <summary>
        /// 
        /// </summary>
        public readonly string Magic;

        /// <summary>
        /// 
        /// </summary>
        public readonly string Version;

        /// <summary>
        /// 
        /// </summary>
        public readonly string UName;

        /// <summary>
        /// 
        /// </summary>
        public readonly string GName;

        /// <summary>
        /// 
        /// </summary>
        public readonly int? DevMajor;

        /// <summary>
        /// 
        /// </summary>
        public readonly int? DevMinor;

        /// <summary>
        /// 
        /// </summary>
        public readonly string Prefix;

        /// <summary>
        /// 
        /// </summary>
        public readonly string Pad;

        /// <summary>
        /// 
        /// </summary>
        public TarEntryType EntryType
        {
            get
            {
                switch (TypeFlag)
                {
                    case '1': return TarEntryType.HardLink;
                    case '2': return TarEntryType.SymbolicLink;
                    case '3': return TarEntryType.CharacterDeviceNode;
                    case '4': return TarEntryType.BlockDeviceNode;
                    case '5': return TarEntryType.Directory;
                    case '6': return TarEntryType.FIFONode;
                    case '7': return TarEntryType.Reserved;
                    default: return TarEntryType.RegularFile;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDirectory => 
            EntryType == TarEntryType.Directory;

        private static string ReadString(byte[] block, ref int offset, int length)
        {
            var result = ASCIIEncoding.Default.GetString(block, offset, length);
            offset += length;
            int nul = result.IndexOf('\0');
            if (nul != -1) result = result.Substring(0, nul);
            return result;
        }

        private static int? ReadInt32(byte[] block, ref int offset, int length)
        {
            var octal = ReadString(block, ref offset, length);
            int result = 0;

            foreach (var ch in octal)
            {
                if (ch == ' ')
                    continue;

                if (ch < '0' || ch > '7')
                    return null;

                result = result * 8 + ch - '0';
            }

            return result;
        }

        private static DateTime? ReadDateTime(byte[] block, ref int offset, int length)
        {
            int? n = ReadInt32(block, ref offset, length);
            if (!n.HasValue) return null;
            return new DateTime(1970, 1, 1) + TimeSpan.FromSeconds(n.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        public TarHeader(byte[] block)
        {
            int offset = 0;
            Name = ReadString(block, ref offset, 100);
            Mode = ReadInt32(block, ref offset, 8);
            UID = ReadInt32(block, ref offset, 8);
            GID = ReadInt32(block, ref offset, 8);
            Size = ReadInt32(block, ref offset, 12);
            MTime = ReadDateTime(block, ref offset, 12);
            Checksum = ReadInt32(block, ref offset, 8);
            var typeFlag = ReadString(block, ref offset, 1);
            LinkName = ReadString(block, ref offset, 100);
            Magic = ReadString(block, ref offset, 6);
            Version = ReadString(block, ref offset, 2);
            UName = ReadString(block, ref offset, 32);
            GName = ReadString(block, ref offset, 32);
            DevMajor = ReadInt32(block, ref offset, 8);
            DevMinor = ReadInt32(block, ref offset, 8);
            Prefix = ReadString(block, ref offset, 155);
            Pad = ReadString(block, ref offset, 12);

            TypeFlag = typeFlag == null || typeFlag.Length != 1 || typeFlag[0] > (char)127 ? '\0' : typeFlag[0];
        }
    }
}
