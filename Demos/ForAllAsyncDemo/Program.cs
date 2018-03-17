
/*
ForAllAsyncDemo

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Foundations;
using Foundations.RandomNumbers;

namespace ForAllAsyncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = new Generator().Synchronized();
    
            Enumerable.Range(0, 1000).ForAllAsync(100, async n =>
            {
                int t = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine(new string(' ', 4*t) + "+" + n);
                await Task.Delay(g.Int32(1000));
                int u = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine(new string(' ', 4*u) + "-" + n);
            }).Wait();
        }
    }
}
