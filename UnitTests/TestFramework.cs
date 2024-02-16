
/*
DecimalConstantsTests.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Diagnostics;

namespace Foundations.UnitTesting
{
    [AttributeUsage(AttributeTargets.Class)]
	public class TestClassAttribute : Attribute
	{
	}
	
    [AttributeUsage(AttributeTargets.Method)]
	public class TestMethodAttribute : Attribute
	{
	}

    [AttributeUsage(AttributeTargets.Method)]
    public class ExpectedExceptionAttribute(Type ExceptionType) : Attribute
    {
        public override int GetHashCode() => ExceptionType.GetHashCode();
    }

	public static class Assert
	{
		public static void AreEqual(object expected, object actual)
		{
            Debug.Assert(expected.Equals(actual));
		}

		public static void AreEqual(object expected, object actual, string message)
		{
            Debug.Assert(expected.Equals(actual), message);
		}

		public static void AreNotEqual(object expected, object actual)
		{
            Debug.Assert(!expected.Equals(actual));
		}

		public static void AreNotEqual(object expected, object actual, string message)
		{
            Debug.Assert(!expected.Equals(actual), message);
		}

        public static void IsTrue(bool predicate)
        {
            Debug.Assert(predicate);
        }

        public static void IsFalse(bool predicate)
        {
            Debug.Assert(!predicate);
        }

        public static void Fail(string message)
        {
            Debug.Fail(message);
        }

        public static void Fail()
        {
            Debug.Fail(null);
        }

        public static void IsNull(object obj)
        {
            Debug.Assert(obj is null);
        }

        public static void IsNotNull(object obj)
        {
            Debug.Assert(obj is not null);
        }
	}
}