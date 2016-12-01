
/*
Permutation.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using Foundations.RandomNumbers;

namespace Foundations
{
    /// <summary>
    /// Creates random permutations, cyclic permutations, and
    /// shuffles or cycles lists of values.
    /// </summary>
    public static partial class Permutation
    {
        private static Generator gen = new Generator(new SynchronizedRandomSource(Generator.DefaultSourceFactory()));

        /// <summary>
        /// Creates a random permutation.
        /// </summary>
        public static IList<int> Create(int length)
        {
            return Create(length, gen);
        }

        /// <summary>
        /// Creates a random permutation.
        /// </summary>
        public static IList<int> Create(int length, Generator g)
        {
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            var arr = new int[length];
            Fill(arr, g);
            return arr;
        }

        /// <summary>
        /// Fills a list with a random permutation.
        /// </summary>
        public static void Fill(IList<int> list)
        {
            Fill(list, gen);
        }

        /// <summary>
        /// Fills a list with a random permutation.
        /// </summary>
        public static void Fill(IList<int> list, Generator g)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (g == null) throw new ArgumentNullException(nameof(g));

            for (int i = 0; i < list.Count; i++)
            {
                int j = g.Int32(i + 1);
                list[i] = list[j];
                list[j] = i;
            }
        }

        /// <summary>
        /// Shuffles a list.
        /// </summary>
        public static void Shuffle<T>(IList<T> list)
        {
            Shuffle(list, gen);
        }

        /// <summary>
        /// Shuffles a list.
        /// </summary>
        public static void Shuffle<T>(IList<T> list, Generator g)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (g == null) throw new ArgumentNullException(nameof(g));

            for (int i = 0; i < list.Count; i++)
            {
                int j = g.Int32(i + 1);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        /// <summary>
        /// Creates a random cyclic permutation.
        /// </summary>
        public static IList<int> CreateCycle(int length)
        {
            return CreateCycle(length, gen);
        }

        /// <summary>
        /// Creates a random cyclic permutation.
        /// </summary>
        public static IList<int> CreateCycle(int length, Generator g)
        {
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            if (g == null) throw new ArgumentNullException(nameof(g));
            var arr = new List<int>(length);
            FillCycle(arr, g);
            return arr;
        }

        /// <summary>
        /// Fills a list with a random cyclic permutation.
        /// </summary>
        public static void FillCycle(IList<int> list)
        {
            FillCycle(list, gen);
        }

        /// <summary>
        /// Fills a list with a random cyclic permutation.
        /// </summary>
        public static void FillCycle(IList<int> list, Generator g)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (g == null) throw new ArgumentNullException(nameof(g));

            if (list.Count > 0) list[0] = 0;
            
            for (int i = 1; i < list.Count; i++)
            {
                int j = g.Int32(i);
                list[i] = list[j];
                list[j] = i;
            }
        }

        /// <summary>
        /// Shuffles a list using a random cyclic permutation.
        /// </summary>
        public static void Cycle<T>(IList<T> list)
        {
            Cycle(list, gen);
        }

        /// <summary>
        /// Shuffles a list using a random cyclic permutation.
        /// </summary>
        public static void Cycle<T>(IList<T> list, Generator g)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (g == null) throw new ArgumentNullException(nameof(g));

            for (int i = 1; i < list.Count; i++)
            {
                int j = g.Int32(i);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        /// <summary>
        /// Creates a permutation that is the inverse of a specific permutation.
        /// </summary>
        public static IList<int> Inverse(IList<int> permutation)
        {
            if (permutation == null) throw new ArgumentNullException(nameof(permutation));
            var arr = new int[permutation.Count];

            for (int i = 0; i < permutation.Count; i++)
            {
                arr[permutation[i]] = i;
            }

            for (int i = 0; i < permutation.Count; i++)
            {
                if (permutation[arr[i]] != i)
                    throw new ArgumentException("List does not represent a permutation. Not all values from 0 to Count-1 are represented.");
            }

            return arr;
        }

        /// <summary>
        /// Creates a permutation function having the specified structure.
        /// </summary>
        /// <param name="length">The size of the permutation. The structure function must have a domain and range [0, length).</param>
        /// <param name="structure">The simplest function that has the desired structure behavior.
        /// For example, to create a cyclic permutation, you could use i => (i + 1) % length.
        /// To create only 2-cycles, you could use i => i ^ 1.</param>
        public static Func<int, int> WithStructure(int length, Func<int, int> structure)
        {
            return WithStructure(length, structure, gen);
        }

        /// <summary>
        /// Creates a permutation function having the specified structure.
        /// </summary>
        /// <param name="g">Random number generator.</param>
        /// <param name="length">The size of the permutation. The structure function must have a domain and range [0, length).</param>
        /// <param name="structure">The simplest function that has the desired structure behavior.
        /// For example, to create a cyclic permutation, you could use i => (i + 1) % length.
        /// To create only 4-cycles, you could use i => i ^ 3.</param>
        public static Func<int, int> WithStructure(int length, Func<int, int> structure, Generator g)
        {
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            if (structure == null) throw new ArgumentNullException(nameof(structure));
            if (g == null) throw new ArgumentNullException(nameof(g));
            var f = Create(length, g);
            var finv = Inverse(f);

            return i =>
            {
                if (i < 0 || i >= length) throw new ArgumentOutOfRangeException();
                var j = structure(f[i]);
                if (j < 0 || j >= length) throw new InvalidOperationException($"The provided structure function does not have range [0, {length}). It maps {f[i]} to {j}.");
                return finv[j];
            };
        }

        /// <summary>
        /// Creates a permutation having the specified structure.
        /// </summary>
        /// <param name="length">The size of the permutation. The structure function must have a domain and range [0, length).</param>
        /// <param name="structure">The simplest function that has the desired structure behavior.
        /// For example, to create a cyclic permutation, you could use i => (i + 1) % length.
        /// To create only 2-cycles, you could use i => i ^ 1.</param>
        public static IList<int> CreateWithStructure(int length, Func<int, int> structure)
        {
            return CreateWithStructure(length, structure, gen);
        }

        /// <summary>
        /// Creates a permutation having the specified structure.
        /// </summary>
        /// <param name="g">Random number generator.</param>
        /// <param name="length">The size of the permutation. The structure function must have a domain and range [0, length).</param>
        /// <param name="structure">The simplest function that has the desired structure behavior.
        /// For example, to create a cyclic permutation, you could use i => (i + 1) % length.
        /// To create only 2-cycles, you could use i => i ^ 1.</param>
        public static IList<int> CreateWithStructure(int length, Func<int, int> structure, Generator g)
        {
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            if (structure == null) throw new ArgumentNullException(nameof(structure));
            if (g == null) throw new ArgumentNullException(nameof(g));
            var f = WithStructure(length, structure, g);
            var arr = new int[length];
            for (int i = 0; i < length; i++) arr[i] = f(i);
            return arr;
        }
    }
}