
/*
Ruler.cs
http://oeis.org/A007814

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System.Collections.Generic;
using Foundations.Coding;

namespace Foundations
{
    public static partial class Sequences
    {
        /// <summary>
        /// The ruler sequence 0,1,0,2,0,1,0,3,0,1,0,2,0,1,0,4,...
        /// The highest power of 2 dividing n.
        /// The bit position that changes in adjacent Grey codes.
        /// </summary>
        public static IEnumerable<int> Ruler()
        {
            return Ruler(0);
        }

        /// <summary>
        /// The ruler sequence skipping some initial values.
        /// </summary>
        public static IEnumerable<int> Ruler(ulong skip)
        {
            ulong g = skip ^ (skip >> 1);

            while (true)
            {
                skip++;
                ulong g2 = skip ^ (skip >> 1);
                yield return Bits.FloorLog2(g ^ g2);
                g = g2;
            }
        }
    }
}