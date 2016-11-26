﻿
/*
SobolInitialValues.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foundations.Types;
using System.Diagnostics;

namespace Foundations.RandomNumbers
{
    [TestClass]
    public class SobolInitialValuesTests
    {
        [TestMethod]
        public void SobolInitialValues()
        {
            SobolInitialValues(20);
        }

        public void SobolInitialValues(int max)
        {
            int d = 2;
            var g = new Generator("SobolInitialValue");
            var v = new bool[max + 1][];
            v[0] = new bool[max];
            v[0][0] = true;

            foreach (var p in PolyGF2.GetAllPrimitive().Take(max - 1))
            {
                v[d - 1] = new bool[max];
                int s = p.Degree;
                var m = new int[s];
                var V = new SquareMatrixGF2(d);

                while (true)
                {
                    for (int i = 0; i < s; i++) m[i] = g.Int32(2 << i) | 1;
                    var dv = Subrandom.SobolDirectionVectors(max, p, m);

                    for (int i = 0; i < max; i++)
                        v[d - 1][i] = dv[i] > (ulong)long.MaxValue;

                    for (int row = 0; row < d; row++)
                        for (int col = 0; col < d; col++)
                            V[row, col] = v[row][col];
                    bool det = V.GetDeterminant();
                    if (det) break;
                }

                Trace.Write($"{d}\t{s}\t{p.GetCode() / 2 ^ (1UL << (s-1))}\t");
                for (int i = 0; i < s; i++)
                    Trace.Write($"{m[i]} ");
                Trace.WriteLine("");
                d++;
            }
        }
    }
}