﻿
/*
MixingFunctionsTests.cs
*/

using System.Collections.Generic;
using Foundations.Functions;

namespace Foundations.RandomNumbers
{
    /// <summary>
    /// 
    /// </summary>
	[TestClass]
	public class MixingFunctionsTests
    {
		[TestMethod]
		public void ByteTest()
		{
			var generator = new Generator("ByteTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateByteMixer(generator);
				var used = new HashSet<Byte>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Byte)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void ByteInverseTest()
		{
			var generator = new Generator("ByteInverseTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateByteInverseMixer(generator);
				var used = new HashSet<Byte>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Byte)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void ByteRoundTripTest()
		{
			var generator = new Generator("ByteRoundTripTest");
            var clone = generator.Clone();

			for (int t = 0; t < 100; t++)
			{
				var f = MixingFunctions.CreateByteMixer(generator);
				var g = MixingFunctions.CreateByteInverseMixer(clone);

				for (int i = 0; i < 256; i++)
				{
					Assert.AreEqual((Byte)i, g(f((Byte)i)));
                    Assert.AreEqual((Byte)i, f(g((Byte)i)));
				}
			}
		}

		[TestMethod]
		public void SByteTest()
		{
			var generator = new Generator("SByteTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateSByteMixer(generator);
				var used = new HashSet<SByte>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((SByte)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void SByteInverseTest()
		{
			var generator = new Generator("SByteInverseTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateSByteInverseMixer(generator);
				var used = new HashSet<SByte>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((SByte)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void SByteRoundTripTest()
		{
			var generator = new Generator("SByteRoundTripTest");
            var clone = generator.Clone();

			for (int t = 0; t < 100; t++)
			{
				var f = MixingFunctions.CreateSByteMixer(generator);
				var g = MixingFunctions.CreateSByteInverseMixer(clone);

				for (int i = 0; i < 256; i++)
				{
					Assert.AreEqual((SByte)i, g(f((SByte)i)));
                    Assert.AreEqual((SByte)i, f(g((SByte)i)));
				}
			}
		}

		[TestMethod]
		public void CharTest()
		{
			var generator = new Generator("CharTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateCharMixer(generator);
				var used = new HashSet<Char>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Char)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void CharInverseTest()
		{
			var generator = new Generator("CharInverseTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateCharInverseMixer(generator);
				var used = new HashSet<Char>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Char)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void CharRoundTripTest()
		{
			var generator = new Generator("CharRoundTripTest");
            var clone = generator.Clone();

			for (int t = 0; t < 100; t++)
			{
				var f = MixingFunctions.CreateCharMixer(generator);
				var g = MixingFunctions.CreateCharInverseMixer(clone);

				for (int i = 0; i < 256; i++)
				{
					Assert.AreEqual((Char)i, g(f((Char)i)));
                    Assert.AreEqual((Char)i, f(g((Char)i)));
				}
			}
		}

		[TestMethod]
		public void UInt16Test()
		{
			var generator = new Generator("UInt16Test");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateUInt16Mixer(generator);
				var used = new HashSet<UInt16>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((UInt16)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void UInt16InverseTest()
		{
			var generator = new Generator("UInt16InverseTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateUInt16InverseMixer(generator);
				var used = new HashSet<UInt16>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((UInt16)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void UInt16RoundTripTest()
		{
			var generator = new Generator("UInt16RoundTripTest");
            var clone = generator.Clone();

			for (int t = 0; t < 100; t++)
			{
				var f = MixingFunctions.CreateUInt16Mixer(generator);
				var g = MixingFunctions.CreateUInt16InverseMixer(clone);

				for (int i = 0; i < 256; i++)
				{
					Assert.AreEqual((UInt16)i, g(f((UInt16)i)));
                    Assert.AreEqual((UInt16)i, f(g((UInt16)i)));
				}
			}
		}

		[TestMethod]
		public void Int16Test()
		{
			var generator = new Generator("Int16Test");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateInt16Mixer(generator);
				var used = new HashSet<Int16>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Int16)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void Int16InverseTest()
		{
			var generator = new Generator("Int16InverseTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateInt16InverseMixer(generator);
				var used = new HashSet<Int16>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Int16)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void Int16RoundTripTest()
		{
			var generator = new Generator("Int16RoundTripTest");
            var clone = generator.Clone();

			for (int t = 0; t < 100; t++)
			{
				var f = MixingFunctions.CreateInt16Mixer(generator);
				var g = MixingFunctions.CreateInt16InverseMixer(clone);

				for (int i = 0; i < 256; i++)
				{
					Assert.AreEqual((Int16)i, g(f((Int16)i)));
                    Assert.AreEqual((Int16)i, f(g((Int16)i)));
				}
			}
		}

		[TestMethod]
		public void UInt32Test()
		{
			var generator = new Generator("UInt32Test");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateUInt32Mixer(generator);
				var used = new HashSet<UInt32>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((UInt32)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void UInt32InverseTest()
		{
			var generator = new Generator("UInt32InverseTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateUInt32InverseMixer(generator);
				var used = new HashSet<UInt32>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((UInt32)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void UInt32RoundTripTest()
		{
			var generator = new Generator("UInt32RoundTripTest");
            var clone = generator.Clone();

			for (int t = 0; t < 100; t++)
			{
				var f = MixingFunctions.CreateUInt32Mixer(generator);
				var g = MixingFunctions.CreateUInt32InverseMixer(clone);

				for (int i = 0; i < 256; i++)
				{
					Assert.AreEqual((UInt32)i, g(f((UInt32)i)));
                    Assert.AreEqual((UInt32)i, f(g((UInt32)i)));
				}
			}
		}

		[TestMethod]
		public void Int32Test()
		{
			var generator = new Generator("Int32Test");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateInt32Mixer(generator);
				var used = new HashSet<Int32>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Int32)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void Int32InverseTest()
		{
			var generator = new Generator("Int32InverseTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateInt32InverseMixer(generator);
				var used = new HashSet<Int32>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Int32)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void Int32RoundTripTest()
		{
			var generator = new Generator("Int32RoundTripTest");
            var clone = generator.Clone();

			for (int t = 0; t < 100; t++)
			{
				var f = MixingFunctions.CreateInt32Mixer(generator);
				var g = MixingFunctions.CreateInt32InverseMixer(clone);

				for (int i = 0; i < 256; i++)
				{
					Assert.AreEqual((Int32)i, g(f((Int32)i)));
                    Assert.AreEqual((Int32)i, f(g((Int32)i)));
				}
			}
		}

		[TestMethod]
		public void UInt64Test()
		{
			var generator = new Generator("UInt64Test");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateUInt64Mixer(generator);
				var used = new HashSet<UInt64>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((UInt64)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void UInt64InverseTest()
		{
			var generator = new Generator("UInt64InverseTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateUInt64InverseMixer(generator);
				var used = new HashSet<UInt64>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((UInt64)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void UInt64RoundTripTest()
		{
			var generator = new Generator("UInt64RoundTripTest");
            var clone = generator.Clone();

			for (int t = 0; t < 100; t++)
			{
				var f = MixingFunctions.CreateUInt64Mixer(generator);
				var g = MixingFunctions.CreateUInt64InverseMixer(clone);

				for (int i = 0; i < 256; i++)
				{
					Assert.AreEqual((UInt64)i, g(f((UInt64)i)));
                    Assert.AreEqual((UInt64)i, f(g((UInt64)i)));
				}
			}
		}

		[TestMethod]
		public void Int64Test()
		{
			var generator = new Generator("Int64Test");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateInt64Mixer(generator);
				var used = new HashSet<Int64>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Int64)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void Int64InverseTest()
		{
			var generator = new Generator("Int64InverseTest");

			for (int t = 0; t < 100; t++)
			{
				var fn = MixingFunctions.CreateInt64InverseMixer(generator);
				var used = new HashSet<Int64>();

				for (int i = 0; i < 256; i++)
				{
					used.Add(fn((Int64)i));
				}

				Assert.AreEqual(256, used.Count);
			}
		}

		[TestMethod]
		public void Int64RoundTripTest()
		{
			var generator = new Generator("Int64RoundTripTest");
            var clone = generator.Clone();

			for (int t = 0; t < 100; t++)
			{
				var f = MixingFunctions.CreateInt64Mixer(generator);
				var g = MixingFunctions.CreateInt64InverseMixer(clone);

				for (int i = 0; i < 256; i++)
				{
					Assert.AreEqual((Int64)i, g(f((Int64)i)));
                    Assert.AreEqual((Int64)i, f(g((Int64)i)));
				}
			}
		}


    }
}
