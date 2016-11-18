
/*
ValueTypes.cs

Copyright � 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

THIS IS AN AUTO-GENERATED SOURCE FILE. DO NOT EDIT THIS FILE DIRECTLY.
INSTEAD, EDIT THE .tt FILE WITH THE SAME NAME AND RE-RUN THE TEXT TEMPLATING
FILE GENERATOR. IF YOU SAVE THE FILE IN VISUAL STUDIO IT WILL DO THIS FOR YOU.
*/

using System;

namespace Foundations.Types
{
	/// <summary>
	/// This decimal type is intended to be used in technical/scientific
	/// applications to accomodate smaller exponents than <see cref="System.Decimal"/>,
	/// but it is lower precision (64 bits instead of 96).
	/// </summary>
	internal static class HashHelper
	{
		public static Func<int, int> Mixer = Functions.MixingFunctions.CreateInt32Mixer(new[] { 0, 1, 1, 8, 9, 9, 9, 8, 8, 1, 9, 9, 9, 1, 1, 9, 7, 2, 5, 3 });
	}
}
