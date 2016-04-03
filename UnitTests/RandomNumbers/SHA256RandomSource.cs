﻿
/*
SHA256RandomSourceTests.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.
*/

using System;
using Foundations.RandomNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundations.UnitTests.Objects
{
    [TestClass]
    public class SHA256RandomSourceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UseAfterDispose()
        {
            var source = new SHA256RandomSource();
            source.Initialize(new byte[] { 1, 2, 3 });
            source.Dispose();
            source.Next();
            source.Next();
            source.Next();
            source.Next();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RequiresSeed()
        {
            var source = new SHA256RandomSource();
            var random = new Generator(source);
        }
    }
}
