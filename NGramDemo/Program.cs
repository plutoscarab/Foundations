using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Foundations.RandomNumbers;
using Foundations.Statistics;

namespace Foundations.Demos.NGramDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            const int DOP = 30;
            const int N = 3;
            ServicePointManager.DefaultConnectionLimit = DOP;

            var lastNames = new HashSet<string>();
            NGram lastNameNGram = ReadNames("USLastNames.txt", N, lastNames);
            var femaleFirstNames = new HashSet<string>();
            NGram femaleNameNGram = ReadNames("USFemaleFirstNames.txt", N, femaleFirstNames);
            var g = new Generator();

            for (int i = 0; i < 20; i++)
            {
                string first, middle, last;
                do { first = femaleNameNGram.GetSample(g); } while (femaleFirstNames.Contains(first));
                do { middle = femaleNameNGram.GetSample(g); } while (femaleFirstNames.Contains(middle));
                do { last = lastNameNGram.GetSample(g); } while (lastNames.Contains(last));
                Console.WriteLine($"{first} {middle} {last}");
            }
        }

        private static NGram ReadNames(string filename, int N, HashSet<string> lastNames)
        {
            var weighted = new List<Tuple<string, int>>();

            using (var reader = File.OpenText(filename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var cols = line.Split('\t');
                    var name = cols[0];
                    name = name[0].ToString() + name.Substring(1).ToLowerInvariant();
                    int count = int.Parse(cols[1]);
                    lastNames.Add(name);
                    weighted.Add(Tuple.Create(name, count));
                }
            }

            var lastNameNGram = new NGram(N, weighted);
            return lastNameNGram;
        }

        private void Web_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
