
/*
CodeTemplate.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.IO;

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
        public static readonly IByteEncoding CodeTemplate = new CodeTemplate();
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class CodeTemplate : IByteEncoding
    {
        internal CodeTemplate()
        {
        }

        /// <summary>
        /// Write a <see cref="System.UInt64"/> to a <see cref="Stream"/>.
        /// </summary>
        public void Write(Stream stream, ulong value)
        {
        }

        /// <summary>
        /// Read a <see cref="System.Int64"/> from a <see cref="Stream"/>.
        /// </summary>
        public ulong Read(Stream stream)
        {
        }
    }
}