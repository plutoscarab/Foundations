
/*
MetricPrefix.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

namespace Foundations.Units
{
    /// <summary>
    /// A symbol representing a power of 10 which produces a multiple of
    /// a unit of measurement. For example, in International System
    /// of Units, the symbol "μ" represents 10 to the power of -6, or
    /// one-millionth of a unit.
    /// </summary>
    public struct MetricPrefix
    {
        /// <summary>
        /// The symbol.
        /// </summary>
        public readonly string Symbol;

        /// <summary>
        /// The power of 10 represented by the symbol.
        /// </summary>
        public readonly int Power;
        
        /// <summary>
        /// Create a <see cref="MetricPrefix"/>.
        /// </summary>
        public MetricPrefix(string symbol, int power)
        {
            Symbol = symbol;
            Power = power;
        }

        /// <summary>
        /// Gets a string representation of this object.
        /// </summary>
        public override string ToString()
        {
            return $"{Symbol} (10^{Power})";
        }
    }
}