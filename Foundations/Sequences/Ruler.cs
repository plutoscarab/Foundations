
/*
Ruler.cs
http://oeis.org/A001511

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundations
{
    public static partial class Sequences
    {
        /// <summary>
        /// The ruler sequence 1,2,1,3,1,2,1,4,1,2,1,3,...
        /// </summary>
        public static IEnumerable<int> Ruler()
        {
            return Ruler(1);
        }

        private static IEnumerable<int> Ruler(int n)
        {
            yield return n;
            
            foreach (var r in Ruler(n + 1))
            {
                yield return r;
                yield return n;
            }
        }
    }
}