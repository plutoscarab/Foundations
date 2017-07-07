
/*
MultiAwaitable.cs

Copyright © 2017 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Foundations.BclExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MultiAwaiter : INotifyCompletion
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
                    Action action = continuation;
                    continuation = null;
                    ThreadPool.QueueUserWorkItem(_ => action());
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
                    ThreadPool.QueueUserWorkItem(_ => continuation());
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

    /// <summary>
    /// 
    /// </summary>
    public sealed class MultiAwaitable
    {
        MultiAwaiter awaiter = new MultiAwaiter();

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
        public MultiAwaiter GetAwaiter()
        {
            return awaiter;
        }
    }
}
