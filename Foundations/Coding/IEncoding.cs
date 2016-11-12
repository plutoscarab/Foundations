
/*
IEncoding.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations.Coding
{
    /// <summary>
    /// Encode or decode a <see cref="System.Int32"/>.
    /// </summary>
    public interface IEncoding
    {
        /// <summary>
        /// Gets the code corresponding to a value.
        /// </summary>
        Code GetCode(int value);

        /// <summary>
        /// Gets the value corresponding to a code.
        /// </summary>
        int GetValue(Code code);

        /// <summary>
        /// Gets the encoding category.
        /// </summary>
        CodeCategory Category { get; }

        /// <summary>
        /// Gets the minimum value that can be encoded.
        /// </summary>
        int MinValue { get; }

        /// <summary>
        /// Gets the maximum value that can be encoded.
        /// </summary>
        int MaxValue { get; }
    }
}