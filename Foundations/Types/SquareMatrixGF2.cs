
/*
SquareMatrixGF2.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections;

namespace Foundations.Types
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SquareMatrixGF2
    {
        private int size;
        private BitArray bits;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public SquareMatrixGF2(int size)
        {
            if (size < 1) throw new ArgumentOutOfRangeException();
            bits = new BitArray(size * size);
            this.size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool this[int row, int col]
        {
            get { return bits[row * size + col]; }
            set { bits[row * size + col] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool GetDeterminant()
        {
            for (int row = 0; row < size; row++)
            {
                if (!this[row, row])
                {
                    for (int row2 = row + 1; row2 < size; row2++)
                    {
                        if (this[row2, row])
                        {
                            SwapRows(row, row2);
                            break;
                        }
                    }
                }

                if (!this[row, row])
                    return false;

                for (int row2 = row + 1; row2 < size; row2++)
                {
                    if (this[row2, row])
                    {
                        SubtractRow(row, row2);
                    }
                }
            }

            return true;
        }

        private void SwapRows(int row1, int row2)
        {
            for (int col = 0; col < size; col++)
            {
                bool temp = this[row1, col];
                this[row1, col] = this[row2, col];
                this[row2, col] = temp;
            }
        }

        private void SubtractRow(int row1, int row2)
        {
            for (int col = 0; col < size; col++)
            {
                this[row2, col] ^= this[row1, col];
            }
        }
    }
}