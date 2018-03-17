
/*
ManualResetEventAsync.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Foundations.Async
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ManualResetEventAsync
    {
        ManualResetEventAwaiter awaiter = new ManualResetEventAwaiter();

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            awaiter.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Set()
        {
            awaiter.Set();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAwaiter GetAwaiter()
        {
            return awaiter;
        }
    }
}
