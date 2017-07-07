using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundations.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Foundations.Formats
{
    [TestClass()]
    public class TarReaderTests
    {
        [TestMethod()]
        public void GetBlocksTest()
        {
            using (var stream = File.OpenRead("Formats/Test.tar.txt"))
            {
                foreach (var block in TarReader.GetBlocks(stream))
                {
                    var h = new TarHeader(block);
                }
            }
        }

        [TestMethod()]
        public void GetEntriesNonSeekableTest()
        {
            using (var stream = File.OpenRead("Formats/Test.tar.gz"))
            using (var gz = new GZipStream(stream, CompressionMode.Decompress))
            {
                foreach (var entry in TarReader.GetEntries(gz))
                {
                    if (entry.IsDirectory)
                    {
                        Console.WriteLine($"dir {entry.Prefix}{entry.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"{entry.Size}\t{entry.Prefix}{entry.Name}");
                    }
                }
            }
        }

        [TestMethod()]
        public void GetEntriesSeekableTest()
        {
            using (var stream = File.OpenRead("Formats/Test.tar"))
            {
                foreach (var entry in TarReader.GetEntries(stream))
                {
                    if (entry.IsDirectory)
                    {
                        Console.WriteLine($"dir {entry.Prefix}{entry.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"{entry.Size}\t{entry.Prefix}{entry.Name}");
                    }
                }
            }
        }

        [TestMethod()]
        public void GetEntriesTest()
        {
            using (var stream = File.OpenRead("Formats/Test.tar"))
            {
                foreach (var entry in TarReader.GetEntries(stream))
                {
                    if (entry.Stream != null)
                    {
                        var path = entry.Prefix + entry.Name;
                        Directory.CreateDirectory(Path.GetDirectoryName(path));

                        using (var file = File.Create(path))
                        {
                            entry.Stream.CopyTo(file);
                        }

                        Assert.AreEqual(entry.Size.Value, (new FileInfo(path)).Length);
                    }
                }
            }
        }
    }
}