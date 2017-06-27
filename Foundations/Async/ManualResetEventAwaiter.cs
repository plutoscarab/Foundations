
/*
ManualResetEventAwaiter.cs

Copyright © 2017 Pluto Scarab Software. Most Rights Reserved.
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
    internal sealed class ManualResetEventAwaiter : IAwaiter
    {
        bool isCompleted;
        Action continuation;
        object sync = new object();

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            lock (sync)
            {
                isCompleted = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Set()
        {
            lock (sync)
            {
                isCompleted = true;

                if (continuation != null)
                {
                    Task.Run(continuation);
                    continuation = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="continuation"></param>
        public void OnCompleted(Action continuation)
        {
            lock (sync)
            {
                if (isCompleted)
                {
                    Task.Run(continuation);
                }
                else
                {
                    this.continuation += continuation;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCompleted => isCompleted;

        /// <summary>
        /// 
        /// </summary>
        public void GetResult() { }
    }
}
