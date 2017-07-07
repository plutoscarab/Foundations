using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundations.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Foundations.RandomNumbers;

namespace Foundations.Formats
{
    [TestClass()]
    public class TarWriterTests
    {
        [TestMethod()]
        public void WriteDirectoryNameTest()
        {
            var g = new Generator();

            using (var stream = File.Create("WriteDirectoryNameTest.tar"))
            {
                var writer = new TarWriter(stream);

                for (int i = 0; i < 100; i++)
                {
                    var s = new StringBuilder();

                    do
                    {
                        do
                        {
                            s.Append((char)((int)'a' + g.Int32(26)));
                        }
                        while (g.Int32(10) > 0);

                        s.Append('/');
                    }
                    while (g.Int32(25) > 0);

                    var path = s.ToString();

                    if (path.Length < 200)
                        writer.WriteDirectoryName(s.ToString());
                }
            }
        }

        [TestMethod]
        public void WriteDiskDirectoryTest()
        {
            using (var stream = File.Create("WriteDiskDirectoryTest.tar"))
            {
                var writer = new TarWriter(stream);

                foreach (var path in Directory.EnumerateDirectories("C:\\").Take(1000))
                {
                    writer.WriteDirectoryName(path);
                }
            }
        }
    }
}