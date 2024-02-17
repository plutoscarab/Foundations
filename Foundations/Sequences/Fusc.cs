
/*
Fusc.cs
https://oeis.org/A002487

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System.Collections.Generic;
using System.Linq;
using Foundations.Types;

namespace Foundations
{
    public static partial class Sequences
    {
        /// <summary>
        /// Stern-Brocot sequence 0, 1, 1, 2, 1, 3, 2, 3, 1, 4, 3, 5, 2, 5, 3...
        /// </summary>
        public static IEnumerable<int> Fusc()
        {
            yield return 0;
            yield return 1;
            yield return 1;
            var n = 1;

            foreach (var f in Fusc().Skip(2))
            {
                yield return n + f;
                yield return f;
                n = f;
            }
        }

        public static IEnumerable<Rational> Rationals()
        {
            var p = Fusc().First();

            foreach (var q in Fusc().Skip(1))
            {
                yield return new(p, q);
                p = q;
            }
        }
   }
}