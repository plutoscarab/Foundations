using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundations.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundations.RandomNumbers;

namespace Foundations.Formats
{
    [TestClass()]
    public class TarHeaderTests
    {
        [TestMethod()]
        public void TarHeaderTest()
        {
            var g = new Generator();
            var block = new byte[TarReader.BlockSize];

            for (int i = 0; i < 10000; i++)
            {
                g.Fill(block);
                var h = new TarHeader(block);
            }
        }
    }
}