
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
    /// Encode or decode a <typeparam name="TValue" /> as a <typeparam name="TCode"/>.
    /// </summary>
    public interface IEncoding<TValue, TCode>
    {
        /// <summary>
        /// Gets the code corresponding to a value.
        /// </summary>
        TCode GetCode(TValue value);

        /// <summary>
        /// Gets the value corresponding to a code.
        /// </summary>
        TValue GetValue(TCode code);

        /// <summary>
        /// Gets the minimum value that can be encoded.
        /// </summary>
        TValue MinEncodable { get; }

        /// <summary>
        /// Gets the maximum value that can be encoded.
        /// </summary>
        TValue MaxEncodable { get; }
    }
}