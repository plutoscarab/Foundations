
/*
UnitSymbol.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

namespace Foundations.Units
{
    /// <summary>
    /// A symbol used for base or derived units in a system of measurement,
    /// e.g. "kg" is the symbol for units of mass in the SI system.
    /// </summary>
    public struct UnitSymbol
    {
        /// <summary>
        /// The symbol.
        /// </summary>
        public readonly string Symbol;

        /// <summary>
        /// 
        /// </summary>
        public readonly Unit Units;

        /// <summary>
        /// Create a <see cref="UnitSymbol"/>.
        /// </summary>
        public UnitSymbol(string symbol, Unit units)
        {
            Symbol = symbol;
            Units = units;
        }
    }
}