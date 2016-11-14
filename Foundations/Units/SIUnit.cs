
/*
SIUnit.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;

namespace Foundations.Units
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SIUnit : Unit
    {
        /// <summary>
        /// Create a <see cref="SIUnit"/>.
        /// </summary>
        public SIUnit(ISystemOfMeasurement system, int length, int mass, int time, int current, int temperature, int amount, int intensity)
            : base(system, length, mass, time, current, temperature, amount, intensity)
        {
        }
    }
}