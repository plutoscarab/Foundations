
/*
Ruler.cs
http://oeis.org/A007814

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System.Collections.Generic;

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
            yield return 0;

            foreach (var r in Ruler())
            {
                yield return r + 1;
                yield return 0;
            }
        }

        /// <summary>
        /// The ruler sequence skipping some initial values.
        /// </summary>
        public static IEnumerable<int> Ruler(ulong skip)
        {
            if ((skip & 1) == 0)
            {
                yield return 0;
            }

            foreach (var r in Ruler(skip / 2))
            {
                yield return r + 1;
                yield return 0;
            }
        }
    }
}