
/*
Tar.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

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
    public static class TarReader
    {
        /// <summary>
        /// 
        /// </summary>
        public const int BlockSize = 512;

        private static byte[] ReadBlock(Stream stream)
        {
            var block = new byte[BlockSize];
            int n = stream.Read(block, 0, BlockSize);

            if (n == 0)
                return null;

            if (n < BlockSize)
            {
                int i = n;

                while (i < BlockSize)
                {
                    n = stream.Read(block, i, BlockSize - i);
                    if (n == 0) break;
                    i += n;
                }

                if (i < BlockSize)
                    throw new EndOfStreamException();
            }

            return block;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static IEnumerable<byte[]> GetBlocks(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException();

            while (true)
            {
                var block = ReadBlock(stream);

                if (block == null)
                    yield break;

                yield return block;
            }
        }

        private static int ExpectedChecksum(byte[] block)
        {
            const int checksumOffset = 148;
            const int checksumLength = 8;

            int result = checksumLength * (int)' ';

            for (int i = 0; i < checksumOffset; i++)
                result += block[i];

            for (int i = checksumOffset + checksumLength; i < BlockSize; i++)
                result += block[i];

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static IEnumerable<TarEntry> GetEntries(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException();

            var canSeek = stream.CanSeek;
            long position = canSeek ? stream.Position : 0;

            while (true)
            {
                byte[] block = ReadBlock(stream);

                if (block == null)
                    yield break;

                position += BlockSize;
                var entry = new TarEntry(block, position);

                if (!entry.Checksum.HasValue || entry.Checksum.Value != ExpectedChecksum(block))
                    continue;

                if (entry.EntryType == TarEntryType.RegularFile && entry.Size.HasValue && entry.Size.Value > 0 && (!string.IsNullOrEmpty(entry.Name) || !string.IsNullOrEmpty(entry.Prefix)))
                {
                    long paddedSize = BlockSize * ((entry.Size.Value + BlockSize - 1) / BlockSize);
                    position += paddedSize;

                    using (var tarStream = new TarStream(stream, entry.Size.Value, paddedSize))
                    {
                        entry.Stream = tarStream;
                        yield return entry;
                    }
                }
                else
                {
                    yield return entry;
                }

                if (entry.EntryType == TarEntryType.RegularFile && !entry.Size.HasValue)
                    throw new InvalidOperationException("Previous entry had illegible size so we can't advance the stream to the correct position.");
            }
        }
    }
}
