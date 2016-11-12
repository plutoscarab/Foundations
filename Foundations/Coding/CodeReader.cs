
/*
CodeReader.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Foundations.Types;

namespace Foundations.Coding
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CodeReader
    {
        Stream stream;
        int bits;
        int available;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CodeReader(Stream stream)
        {
            this.stream = stream;
        }

        byte GetByte()
        {
            int b = stream.ReadByte();
            if (b == -1) throw new EndOfStreamException();
            return (byte)b;
        }

        /// <summary>
        /// 
        /// </summary>
        public Code ReadCode(int bitCount)
        {
            if (bitCount < 0 || bitCount > 64)
                throw new ArgumentOutOfRangeException(nameof(bitCount));

            ValueUnion union;

            switch(bitCount >> 3)
            {
                case 8:
                    union.Byte_7 = GetByte();
                    goto case 7;

                case 7:
                    union.Byte_6 = GetByte();
                    goto case 6;

                case 6:
                    union.Byte_5 = GetByte();
                    goto case 5;

                case 5:
                    union.Byte_4 = GetByte();
                    goto case 4;

                case 4:
                    union.Byte_3 = GetByte();
                    goto case 3;

                case 3:
                    union.Byte_2 = GetByte();
                    goto case 2;

                case 2:
                    union.Byte_1 = GetByte();
                    goto case 1;

                case 1:
                    union.Byte_0 = GetByte();
                    break;
            }
        }
    }
}