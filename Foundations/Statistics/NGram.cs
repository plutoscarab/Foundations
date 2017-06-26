using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundations.RandomNumbers;

namespace Foundations.Statistics
{
    /// <summary>
    /// 
    /// </summary>
    public class NGram
    {
        int n;
        Dictionary<string, Tuple<string, int[]>> table;
        int[] lengths;

        /// <summary>
        /// 
        /// </summary>
        public NGram(int n, IEnumerable<string> words)
            : this(n, words.Select(_ => Tuple.Create(_, 1)))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public NGram(int n, IEnumerable<Tuple<string, int>> words)
        {
            this.n = n;
            var dict = new Dictionary<string, Dictionary<char, int>>();
            lengths = new int[0];

            foreach (var word in words)
            {
                var prefix = new string('\0', n);

                foreach (var ch in word.Item1 + "\0")
                {
                    Dictionary<char, int> suffix;

                    if (!dict.TryGetValue(prefix, out suffix))
                    {
                        suffix = dict[prefix] = new Dictionary<char, int>();
                    }

                    int count;
                    suffix.TryGetValue(ch, out count);
                    suffix[ch] = count + word.Item2;
                    prefix = prefix.Substring(1) + ch.ToString();
                }

                if (word.Item1.Length + 1 >= lengths.Length)
                {
                    Array.Resize(ref lengths, word.Item1.Length + 1);
                }

                lengths[word.Item1.Length]++;
            }

            for (int i = 1; i < lengths.Length; i++)
            {
                lengths[i] += lengths[i - 1];
            }

            table = new Dictionary<string, Tuple<string, int[]>>();

            foreach (var entry in dict)
            {
                string s = string.Join("", entry.Value.Select(_ => _.Key));
                var sums = new int[s.Length];
                int sum = 0;
                int i = 0;

                foreach (var count in entry.Value.Select(_ => _.Value))
                {
                    sums[i++] = sum += count;
                }

                table[entry.Key] = Tuple.Create(s, sums);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetSample(Generator rng)
        {
            int k = rng.Int32(lengths[lengths.Length - 1]);
            int length = Array.BinarySearch(lengths, k);
            if (length < 0) length = ~length; else ++length;
            if (length == 1) length++;
            var s = new StringBuilder();

            while (true)
            {
                var prefix = new string('\0', n);

                while (true)
                {
                    var stats = table[prefix];
                    var sums = stats.Item2;
                    k = rng.Int32(sums[sums.Length - 1]);
                    int i = Array.BinarySearch(sums, k);
                    if (i < 0) i = ~i; else ++i;
                    var ch = stats.Item1[i];

                    if (s.Length == length || ch == '\0')
                    {
                        if (s.Length == length && ch == '\0')
                            return s.ToString();

                        break;
                    }

                    s.Append(ch);
                    prefix = prefix.Substring(1) + ch.ToString();
                }

                s.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> GetSamples(Generator rng)
        {
            while (true)
            {
                yield return GetSample(rng);
            }
        }
    }
}
