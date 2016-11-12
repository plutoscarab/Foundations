
/*
CodeCategory.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations.Coding
{
    /// <summary>
    /// Indicates attributes of a code.
    /// </summary>
    [Flags]
    public enum CodeCategory
    {
        /// <summary>
        /// The code does not have any special properties.
        /// </summary>
        None = 0,

        /// <summary>
        /// All codes are same length.
        /// </summary>
        Block = 1,

        /// <summary>
        /// No code is a prefix of another code.
        /// </summary>
        Prefix = 2,

        /// <summary>
        /// Prefix code with code lengths bounded by a constant factor of optimal lengths.
        /// </summary>
        Universal = 6,

        /// <summary>
        /// Universal code with bounding constant equal to one.
        /// </summary>
        AsymptoticallyOptimal = 14,

        /// <summary>
        /// Error-correcting code.
        /// </summary>
        ErrorCorrecting = 16,

        /// <summary>
        /// Codes are in lexicographical order.
        /// </summary>
        LexicographicallyOrdered = 32,
    }
}
