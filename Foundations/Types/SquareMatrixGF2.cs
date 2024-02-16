
/*
SquareMatrixGF2.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations.Types
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SquareMatrixGF2
    {
        private int size;
        private uint[][] rows;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public SquareMatrixGF2(int size)
        {
            if (size < 1) throw new ArgumentOutOfRangeException();
            rows = new uint[size][];
            for (int i = 0; i < size; i++)
                rows[i] = new uint[(size + 31) / 32];
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
            get { return (rows[row][col >> 5] & (1u << (col & 31))) != 0; }
            set {
                if (value)
                    rows[row][col >> 5] |= 1u << (col & 31);
                else
                    rows[row][col >> 5] &= ~(1u << (col & 31));
            }
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
            for (int col = (size + 31) / 32 - 1; col >= 0; col--)
            {
                uint temp = rows[row1][col];
                rows[row1][col] = rows[row2][col];
                rows[row2][col] = temp;
            }
        }

        private void SubtractRow(int row1, int row2)
        {
            for (int col = (size + 31) / 32 - 1; col >= 0; col--)
            {
                rows[row2][col] ^= rows[row1][col];
            }
        }
    }
}