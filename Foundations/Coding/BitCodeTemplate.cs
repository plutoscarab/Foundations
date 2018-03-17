
/*
CodeTemplate.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations.Coding
{
    /// <summary>
    /// Encoding implementations.
    /// </summary>
    public static partial class Codes
    {
        /// <summary>
        /// CodeTemplate code.
        /// </summary>
        public static readonly IBitEncoding CodeTemplate = new CodeTemplate();
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class CodeTemplate : IBitEncoding
    {
        internal CodeTemplate()
        {
        }

        /// <summary>
        /// Gets the minimum number that can be encoded.
        /// </summary>
        public int MinEncodable
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the minimum number that can be encoded.
        /// </summary>
        public int MaxEncodable
        {
            get
            {
                return int.MaxValue;
            }
        }

        /// <summary>
        /// Gets the code corresponding to a value.
        /// </summary>
        public Code GetCode(int value)
        {
            if (value < MinEncodable || value > MaxEncodable)
                throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Gets the value corresponding to a code.
        /// </summary>
        public int Read(BitReader reader)
        {
        }
    }
}