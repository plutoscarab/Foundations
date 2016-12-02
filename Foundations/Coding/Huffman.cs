
/*
Huffman.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using Foundations.Collections;
using Foundations.Statistics;

namespace Foundations.Coding
{
    /// <summary>
    /// Huffman coding.
    /// </summary>
	public static class Huffman
	{
		/// <summary>
		/// Determine optimal code lengths based on symbol occurrance rates.
		/// </summary>
		public static byte[] CodeLengthsFromHistogram(int[] histogram)
		{
            if (histogram == null) throw new ArgumentNullException("histogram");
			int n = histogram.Length;
			int p = n;
			byte[] codeLengths = new byte[n];
			int[] parent = new int[2 * n - 1];
			var queue = new MinPriorityQueue<int, long>();

			// add symbols to queue along with priorities
			for (int i = 0; i < n; i++)
			{
				if (histogram[i] > 0)
				{
					queue.Push(i, histogram[i]);
				}
			}

			// combine pairs of symbols
			while (queue.Count > 1)
			{
				// get the two least-common symbols
				var item1 = queue.Pop();
                var item2 = queue.Pop();

				// add conglomerate symbol with their combined frequency
				queue.Push(p, item1.Key + item2.Key);

				// keep track of which symbols belong to which conglomerate
				parent[item1.Value] = p;
				parent[item2.Value] = p;
				p++;
			}

			// determine code lengths by following conglomeration history
			for (int i = 0; i < n; i++)
			{
				if (histogram[i] > 0)
				{
					byte length = 0;
					int k = parent[i];

					// count number of times the symbol was conglomerated
					while (k > 0)
					{
						k = parent[k];
						length++;
					}

					codeLengths[i] = length;
				}
			}

			return codeLengths;
		}

		/// <summary>
		/// Determine optimal code lengths based on symbol occurrance rates.
		/// </summary>
		public static byte[] CodeLengthsFromHistogram(double[] histogram)
		{
            if (histogram == null) throw new ArgumentNullException("histogram");
			int n = histogram.Length;

			if (n == 0)
				return new byte[0];

			int p = n;
			byte[] codeLengths = new byte[n];
			int[] parent = new int[2 * n - 1];
			var queue = new MinPriorityQueue<int, double>();

			// add symbols to queue along with priorities
			for (int i = 0; i < n; i++)
			{
				if (histogram[i] > 0)
				{
					queue.Push(i, histogram[i]);
				}
			}

			// combine pairs of symbols
			while (queue.Count > 1)
			{
				// get the two least-common symbols
				var item1 = queue.Pop();
                var item2 = queue.Pop();

				// add conglomerate symbol with their combined frequency
				queue.Push(p, item1.Key + item2.Key);

				// keep track of which symbols belong to which conglomerate
				parent[item1.Value] = p;
				parent[item2.Value] = p;
				p++;
			}

			// determine code lengths by following conglomeration history
			for (int i = 0; i < n; i++)
			{
				if (histogram[i] > 0)
				{
					byte length = 0;
					int k = parent[i];

					// count number of times the symbol was conglomerated
					while (k > 0)
					{
						k = parent[k];
						length++;
					}

					codeLengths[i] = length;
				}
			}

			return codeLengths;
		}

        /// <summary>
        /// Determine optimal code lengths based on symbol occurrance rates.
        /// </summary>
        public static byte[] CodeLengths(IEnumerable<uint> data)
		{
            if (data == null) throw new ArgumentNullException("data");
			return CodeLengthsFromHistogram(Histogram.From(data));
		}

        /// <summary>
        /// Determine optimal code lengths based on symbol occurrance rates.
        /// </summary>
        public static byte[] CodeLengths(IEnumerable<byte> data)
		{
            if (data == null) throw new ArgumentNullException("data");
            return CodeLengthsFromHistogram(Histogram.From(data));
		}

		/// <summary>
		/// Determine canonical Huffman codes for symbols based on their code length.
		/// </summary>
		public static Code[] CodesFromLengths(byte[] codeLengths)
		{
            if (codeLengths == null) throw new ArgumentNullException("codeLengths");

            if (codeLengths.Length == 0)
            {
                return new Code[0];
            }

			// count the number of codes for each length
			int n = codeLengths.Max();
			uint[] count = new uint[n + 1];

            foreach (byte length in codeLengths)
            {
                count[length]++;
            }

			// determine first code value for each length
			uint[] value = new uint[n + 1];
			uint code = 0;

			for (int i = 1; i <= n; i++)
			{
				if (count[i] > 0)
				{
					value[i] = code;
					code = (code + count[i]) << 1;
				}
			}

			// generate the codes for each symbol
			Code[] codes = new Code[codeLengths.Length];

			for (int i = 0; i < codeLengths.Length; i++)
			{
				byte k = codeLengths[i];

				if (k == 0)
				{
					codes[i] = Code.Empty;
				}
				else
				{
					codes[i] = new Code(Bits.Reverse(value[k]++) >> (32 - k), k);
				}
			}

			return codes;
		}

		/// <summary>
		/// Determine canonical Huffman codes based on counts of symbol occurrences.
		/// </summary>
		/// <param name="histogram"></param>
		/// <returns></returns>
		public static Code[] CodesFromHistogram(int[] histogram)
		{
            if (histogram == null) throw new ArgumentNullException("histogram");
            return CodesFromLengths(CodeLengthsFromHistogram(histogram));
		}

		/// <summary>
		/// Determine canonical Huffman codes based on counts of symbol occurrences.
		/// </summary>
		/// <param name="histogram"></param>
		/// <returns></returns>
		public static Code[] CodesFromHistogram(double[] histogram)
		{
            if (histogram == null) throw new ArgumentNullException("histogram");
            return CodesFromLengths(CodeLengthsFromHistogram(histogram));
		}

		/// <summary>
		/// Determine canonical Huffman codes for each symbol in source data.
		/// </summary>
		public static Code[] Codes(IEnumerable<uint> data)
		{
            if (data == null) throw new ArgumentNullException("data");
            return CodesFromLengths(CodeLengthsFromHistogram(Histogram.From(data)));
		}

		/// <summary>
		/// Determine canonical Huffman codes for each symbol in source data.
		/// </summary>
		public static Code[] Codes(IEnumerable<byte> data)
		{
            if (data == null) throw new ArgumentNullException("data");
            return CodesFromLengths(CodeLengthsFromHistogram(Histogram.From(data)));
		}

        /// <summary>
        /// Determine canonical Huffman codes for each symbol in source data
        /// and return the expected compression ratio.
        /// </summary>
        public static Code[] Codes(IEnumerable<uint> data, out double compressionRatio)
        {
            if (data == null) throw new ArgumentNullException("data");
            var histogram = Histogram.From(data);
            var codes = CodesFromHistogram(histogram);
            long bits = 0;
            long sum = 0;

            for (int i = 0; i < histogram.Length; i++)
            {
                int h = histogram[i];
                if (h == 0) continue;
                bits += h * codes[i].Length;
                sum += h;
            }

            compressionRatio = (8.0 * sum) / bits;
            return codes;
        }

        /// <summary>
        /// Determine canonical Huffman codes for each symbol in source data
        /// and return the expected compression ratio.
        /// </summary>
        public static Code[] Codes(IEnumerable<byte> data, out double compressionRatio)
        {
            if (data == null) throw new ArgumentNullException("data");
            return Codes(data.Select(d => (uint)d), out compressionRatio);
        }
	}
}
