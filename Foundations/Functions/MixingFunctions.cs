
using Foundations.RandomNumbers;

namespace Foundations.Functions
{
	/// <summary>
	/// Mixing functions.
	/// </summary>
	public sealed partial class MixingFunctions
    {
		/// <summary>
		/// Gets one of 2^128 different System.Byte mixing functions.
		/// </summary>
		public static Func<Byte, Byte> CreateByteMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateByteMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateByteMixer(Generator)"/>.
		/// </summary>
		public static Func<Byte, Byte> CreateByteInverseMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateByteInverseMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Byte mixing functions based on a key.
		/// </summary>
		public static Func<Byte, Byte> CreateByteMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateByteMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateByteMixer(Array)"/>.
		/// </summary>
		public static Func<Byte, Byte> CreateByteInverseMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateByteInverseMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Byte mixing functions based on a key.
		/// </summary>
		public static Func<Byte, Byte> CreateByteMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateByteMixerInternal(GetKeyBytes(key));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateByteMixer(Array)"/>.
		/// </summary>
		public static Func<Byte, Byte> CreateByteInverseMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateByteInverseMixerInternal(GetKeyBytes(key));
		}

        /// <summary>
        /// Gets a mixing function with a specified cycle structure.
        /// </summary>
        /// <param name="structure">The simplest function having the desired cycle structure.</param>
        /// <param name="g">Random number generator.</param>
        public static Func<Byte, Byte> CreateByteMixerWithStructure(Func<Byte, Byte> structure, Generator g)
        {
            var g2 = g.Clone();
            var f = CreateByteMixer(g);
            var finv = CreateByteMixer(g2);
            return i => finv(structure(f(i)));
        }

		private static Func<Byte, Byte> CreateByteMixerInternal(byte[] k)
		{
			return n => 
			{
				var a = (byte)(n >> 0);
				a = sbox[a ^ k[0]];
				a = sbox[a ^ k[1]];
				a = sbox[a ^ k[2]];
				a = sbox[a ^ k[3]];
				a = sbox[a ^ k[4]];
				a = sbox[a ^ k[5]];
				a = sbox[a ^ k[6]];
				a = sbox[a ^ k[7]];
				a = sbox[a ^ k[8]];
				a = sbox[a ^ k[9]];
				a = sbox[a ^ k[10]];
				a = sbox[a ^ k[11]];
				a = sbox[a ^ k[12]];
				a = sbox[a ^ k[13]];
				a = sbox[a ^ k[14]];
				a = sbox[a ^ k[15]];
				n = (Byte)a;
				return n;
			};
		}

		private static Func<Byte, Byte> CreateByteInverseMixerInternal(byte[] k)
		{
			return n => 
			{
				int a = (byte)(n >> 0);
				a = rbox[a] ^ k[15];
				a = rbox[a] ^ k[14];
				a = rbox[a] ^ k[13];
				a = rbox[a] ^ k[12];
				a = rbox[a] ^ k[11];
				a = rbox[a] ^ k[10];
				a = rbox[a] ^ k[9];
				a = rbox[a] ^ k[8];
				a = rbox[a] ^ k[7];
				a = rbox[a] ^ k[6];
				a = rbox[a] ^ k[5];
				a = rbox[a] ^ k[4];
				a = rbox[a] ^ k[3];
				a = rbox[a] ^ k[2];
				a = rbox[a] ^ k[1];
				a = rbox[a] ^ k[0];
				n = (Byte)a;
				return n;
			};
		}

		/// <summary>
		/// Gets one of 2^128 different System.SByte mixing functions.
		/// </summary>
		public static Func<SByte, SByte> CreateSByteMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateSByteMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateSByteMixer(Generator)"/>.
		/// </summary>
		public static Func<SByte, SByte> CreateSByteInverseMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateSByteInverseMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets one of 2^128 different System.SByte mixing functions based on a key.
		/// </summary>
		public static Func<SByte, SByte> CreateSByteMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateSByteMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateSByteMixer(Array)"/>.
		/// </summary>
		public static Func<SByte, SByte> CreateSByteInverseMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateSByteInverseMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets one of 2^128 different System.SByte mixing functions based on a key.
		/// </summary>
		public static Func<SByte, SByte> CreateSByteMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateSByteMixerInternal(GetKeyBytes(key));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateSByteMixer(Array)"/>.
		/// </summary>
		public static Func<SByte, SByte> CreateSByteInverseMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateSByteInverseMixerInternal(GetKeyBytes(key));
		}

        /// <summary>
        /// Gets a mixing function with a specified cycle structure.
        /// </summary>
        /// <param name="structure">The simplest function having the desired cycle structure.</param>
        /// <param name="g">Random number generator.</param>
        public static Func<SByte, SByte> CreateSByteMixerWithStructure(Func<SByte, SByte> structure, Generator g)
        {
            var g2 = g.Clone();
            var f = CreateSByteMixer(g);
            var finv = CreateSByteMixer(g2);
            return i => finv(structure(f(i)));
        }

		private static Func<SByte, SByte> CreateSByteMixerInternal(byte[] k)
		{
			return n => 
			{
				var a = (byte)(n >> 0);
				a = sbox[a ^ k[0]];
				a = sbox[a ^ k[1]];
				a = sbox[a ^ k[2]];
				a = sbox[a ^ k[3]];
				a = sbox[a ^ k[4]];
				a = sbox[a ^ k[5]];
				a = sbox[a ^ k[6]];
				a = sbox[a ^ k[7]];
				a = sbox[a ^ k[8]];
				a = sbox[a ^ k[9]];
				a = sbox[a ^ k[10]];
				a = sbox[a ^ k[11]];
				a = sbox[a ^ k[12]];
				a = sbox[a ^ k[13]];
				a = sbox[a ^ k[14]];
				a = sbox[a ^ k[15]];
				n = (SByte)a;
				return n;
			};
		}

		private static Func<SByte, SByte> CreateSByteInverseMixerInternal(byte[] k)
		{
			return n => 
			{
				int a = (byte)(n >> 0);
				a = rbox[a] ^ k[15];
				a = rbox[a] ^ k[14];
				a = rbox[a] ^ k[13];
				a = rbox[a] ^ k[12];
				a = rbox[a] ^ k[11];
				a = rbox[a] ^ k[10];
				a = rbox[a] ^ k[9];
				a = rbox[a] ^ k[8];
				a = rbox[a] ^ k[7];
				a = rbox[a] ^ k[6];
				a = rbox[a] ^ k[5];
				a = rbox[a] ^ k[4];
				a = rbox[a] ^ k[3];
				a = rbox[a] ^ k[2];
				a = rbox[a] ^ k[1];
				a = rbox[a] ^ k[0];
				n = (SByte)a;
				return n;
			};
		}

		/// <summary>
		/// Gets one of 2^128 different System.Char mixing functions.
		/// </summary>
		public static Func<Char, Char> CreateCharMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateCharMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateCharMixer(Generator)"/>.
		/// </summary>
		public static Func<Char, Char> CreateCharInverseMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateCharInverseMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Char mixing functions based on a key.
		/// </summary>
		public static Func<Char, Char> CreateCharMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateCharMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateCharMixer(Array)"/>.
		/// </summary>
		public static Func<Char, Char> CreateCharInverseMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateCharInverseMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Char mixing functions based on a key.
		/// </summary>
		public static Func<Char, Char> CreateCharMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateCharMixerInternal(GetKeyBytes(key));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateCharMixer(Array)"/>.
		/// </summary>
		public static Func<Char, Char> CreateCharInverseMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateCharInverseMixerInternal(GetKeyBytes(key));
		}

        /// <summary>
        /// Gets a mixing function with a specified cycle structure.
        /// </summary>
        /// <param name="structure">The simplest function having the desired cycle structure.</param>
        /// <param name="g">Random number generator.</param>
        public static Func<Char, Char> CreateCharMixerWithStructure(Func<Char, Char> structure, Generator g)
        {
            var g2 = g.Clone();
            var f = CreateCharMixer(g);
            var finv = CreateCharMixer(g2);
            return i => finv(structure(f(i)));
        }

		private static Func<Char, Char> CreateCharMixerInternal(byte[] k)
		{
			return n => 
			{
				byte temp;
				var a = (byte)(n >> 0);
				var b = (byte)(n >> 8);
				temp = a;
				a = sbox[b ^ k[0]];
				b = sbox[temp ^ k[1]];
				temp = a;
				a = sbox[b ^ k[2]];
				b = sbox[temp ^ k[3]];
				temp = a;
				a = sbox[b ^ k[4]];
				b = sbox[temp ^ k[5]];
				temp = a;
				a = sbox[b ^ k[6]];
				b = sbox[temp ^ k[7]];
				temp = a;
				a = sbox[b ^ k[8]];
				b = sbox[temp ^ k[9]];
				temp = a;
				a = sbox[b ^ k[10]];
				b = sbox[temp ^ k[11]];
				temp = a;
				a = sbox[b ^ k[12]];
				b = sbox[temp ^ k[13]];
				temp = a;
				a = sbox[b ^ k[14]];
				b = sbox[temp ^ k[15]];
				n = (Char)a;
				n = (Char)((Char)n | ((Char)b << 8));
				return n;
			};
		}

		private static Func<Char, Char> CreateCharInverseMixerInternal(byte[] k)
		{
			return n => 
			{
				int temp;
				int a = (byte)(n >> 0);
				int b = (byte)(n >> 8);
				temp = rbox[b] ^ k[15];
				b = rbox[a] ^ k[14];
				a = temp;
				temp = rbox[b] ^ k[13];
				b = rbox[a] ^ k[12];
				a = temp;
				temp = rbox[b] ^ k[11];
				b = rbox[a] ^ k[10];
				a = temp;
				temp = rbox[b] ^ k[9];
				b = rbox[a] ^ k[8];
				a = temp;
				temp = rbox[b] ^ k[7];
				b = rbox[a] ^ k[6];
				a = temp;
				temp = rbox[b] ^ k[5];
				b = rbox[a] ^ k[4];
				a = temp;
				temp = rbox[b] ^ k[3];
				b = rbox[a] ^ k[2];
				a = temp;
				temp = rbox[b] ^ k[1];
				b = rbox[a] ^ k[0];
				a = temp;
				n = (Char)a;
				n = (Char)((Char)n | ((Char)b << 8));
				return n;
			};
		}

		/// <summary>
		/// Gets one of 2^128 different System.UInt16 mixing functions.
		/// </summary>
		public static Func<UInt16, UInt16> CreateUInt16Mixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateUInt16MixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateUInt16Mixer(Generator)"/>.
		/// </summary>
		public static Func<UInt16, UInt16> CreateUInt16InverseMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateUInt16InverseMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets one of 2^128 different System.UInt16 mixing functions based on a key.
		/// </summary>
		public static Func<UInt16, UInt16> CreateUInt16Mixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt16MixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateUInt16Mixer(Array)"/>.
		/// </summary>
		public static Func<UInt16, UInt16> CreateUInt16InverseMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt16InverseMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets one of 2^128 different System.UInt16 mixing functions based on a key.
		/// </summary>
		public static Func<UInt16, UInt16> CreateUInt16Mixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt16MixerInternal(GetKeyBytes(key));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateUInt16Mixer(Array)"/>.
		/// </summary>
		public static Func<UInt16, UInt16> CreateUInt16InverseMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt16InverseMixerInternal(GetKeyBytes(key));
		}

        /// <summary>
        /// Gets a mixing function with a specified cycle structure.
        /// </summary>
        /// <param name="structure">The simplest function having the desired cycle structure.</param>
        /// <param name="g">Random number generator.</param>
        public static Func<UInt16, UInt16> CreateUInt16MixerWithStructure(Func<UInt16, UInt16> structure, Generator g)
        {
            var g2 = g.Clone();
            var f = CreateUInt16Mixer(g);
            var finv = CreateUInt16Mixer(g2);
            return i => finv(structure(f(i)));
        }

		private static Func<UInt16, UInt16> CreateUInt16MixerInternal(byte[] k)
		{
			return n => 
			{
				byte temp;
				var a = (byte)(n >> 0);
				var b = (byte)(n >> 8);
				temp = a;
				a = sbox[b ^ k[0]];
				b = sbox[temp ^ k[1]];
				temp = a;
				a = sbox[b ^ k[2]];
				b = sbox[temp ^ k[3]];
				temp = a;
				a = sbox[b ^ k[4]];
				b = sbox[temp ^ k[5]];
				temp = a;
				a = sbox[b ^ k[6]];
				b = sbox[temp ^ k[7]];
				temp = a;
				a = sbox[b ^ k[8]];
				b = sbox[temp ^ k[9]];
				temp = a;
				a = sbox[b ^ k[10]];
				b = sbox[temp ^ k[11]];
				temp = a;
				a = sbox[b ^ k[12]];
				b = sbox[temp ^ k[13]];
				temp = a;
				a = sbox[b ^ k[14]];
				b = sbox[temp ^ k[15]];
				n = (UInt16)a;
				n = (UInt16)((UInt16)n | ((UInt16)b << 8));
				return n;
			};
		}

		private static Func<UInt16, UInt16> CreateUInt16InverseMixerInternal(byte[] k)
		{
			return n => 
			{
				int temp;
				int a = (byte)(n >> 0);
				int b = (byte)(n >> 8);
				temp = rbox[b] ^ k[15];
				b = rbox[a] ^ k[14];
				a = temp;
				temp = rbox[b] ^ k[13];
				b = rbox[a] ^ k[12];
				a = temp;
				temp = rbox[b] ^ k[11];
				b = rbox[a] ^ k[10];
				a = temp;
				temp = rbox[b] ^ k[9];
				b = rbox[a] ^ k[8];
				a = temp;
				temp = rbox[b] ^ k[7];
				b = rbox[a] ^ k[6];
				a = temp;
				temp = rbox[b] ^ k[5];
				b = rbox[a] ^ k[4];
				a = temp;
				temp = rbox[b] ^ k[3];
				b = rbox[a] ^ k[2];
				a = temp;
				temp = rbox[b] ^ k[1];
				b = rbox[a] ^ k[0];
				a = temp;
				n = (UInt16)a;
				n = (UInt16)((UInt16)n | ((UInt16)b << 8));
				return n;
			};
		}

		/// <summary>
		/// Gets one of 2^128 different System.Int16 mixing functions.
		/// </summary>
		public static Func<Int16, Int16> CreateInt16Mixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateInt16MixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateInt16Mixer(Generator)"/>.
		/// </summary>
		public static Func<Int16, Int16> CreateInt16InverseMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateInt16InverseMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Int16 mixing functions based on a key.
		/// </summary>
		public static Func<Int16, Int16> CreateInt16Mixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt16MixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateInt16Mixer(Array)"/>.
		/// </summary>
		public static Func<Int16, Int16> CreateInt16InverseMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt16InverseMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Int16 mixing functions based on a key.
		/// </summary>
		public static Func<Int16, Int16> CreateInt16Mixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt16MixerInternal(GetKeyBytes(key));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateInt16Mixer(Array)"/>.
		/// </summary>
		public static Func<Int16, Int16> CreateInt16InverseMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt16InverseMixerInternal(GetKeyBytes(key));
		}

        /// <summary>
        /// Gets a mixing function with a specified cycle structure.
        /// </summary>
        /// <param name="structure">The simplest function having the desired cycle structure.</param>
        /// <param name="g">Random number generator.</param>
        public static Func<Int16, Int16> CreateInt16MixerWithStructure(Func<Int16, Int16> structure, Generator g)
        {
            var g2 = g.Clone();
            var f = CreateInt16Mixer(g);
            var finv = CreateInt16Mixer(g2);
            return i => finv(structure(f(i)));
        }

		private static Func<Int16, Int16> CreateInt16MixerInternal(byte[] k)
		{
			return n => 
			{
				byte temp;
				var a = (byte)(n >> 0);
				var b = (byte)(n >> 8);
				temp = a;
				a = sbox[b ^ k[0]];
				b = sbox[temp ^ k[1]];
				temp = a;
				a = sbox[b ^ k[2]];
				b = sbox[temp ^ k[3]];
				temp = a;
				a = sbox[b ^ k[4]];
				b = sbox[temp ^ k[5]];
				temp = a;
				a = sbox[b ^ k[6]];
				b = sbox[temp ^ k[7]];
				temp = a;
				a = sbox[b ^ k[8]];
				b = sbox[temp ^ k[9]];
				temp = a;
				a = sbox[b ^ k[10]];
				b = sbox[temp ^ k[11]];
				temp = a;
				a = sbox[b ^ k[12]];
				b = sbox[temp ^ k[13]];
				temp = a;
				a = sbox[b ^ k[14]];
				b = sbox[temp ^ k[15]];
				n = (Int16)a;
				n = (Int16)((UInt16)n | ((UInt16)b << 8));
				return n;
			};
		}

		private static Func<Int16, Int16> CreateInt16InverseMixerInternal(byte[] k)
		{
			return n => 
			{
				int temp;
				int a = (byte)(n >> 0);
				int b = (byte)(n >> 8);
				temp = rbox[b] ^ k[15];
				b = rbox[a] ^ k[14];
				a = temp;
				temp = rbox[b] ^ k[13];
				b = rbox[a] ^ k[12];
				a = temp;
				temp = rbox[b] ^ k[11];
				b = rbox[a] ^ k[10];
				a = temp;
				temp = rbox[b] ^ k[9];
				b = rbox[a] ^ k[8];
				a = temp;
				temp = rbox[b] ^ k[7];
				b = rbox[a] ^ k[6];
				a = temp;
				temp = rbox[b] ^ k[5];
				b = rbox[a] ^ k[4];
				a = temp;
				temp = rbox[b] ^ k[3];
				b = rbox[a] ^ k[2];
				a = temp;
				temp = rbox[b] ^ k[1];
				b = rbox[a] ^ k[0];
				a = temp;
				n = (Int16)a;
				n = (Int16)((UInt16)n | ((UInt16)b << 8));
				return n;
			};
		}

		/// <summary>
		/// Gets one of 2^128 different System.UInt32 mixing functions.
		/// </summary>
		public static Func<UInt32, UInt32> CreateUInt32Mixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateUInt32MixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateUInt32Mixer(Generator)"/>.
		/// </summary>
		public static Func<UInt32, UInt32> CreateUInt32InverseMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateUInt32InverseMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets one of 2^128 different System.UInt32 mixing functions based on a key.
		/// </summary>
		public static Func<UInt32, UInt32> CreateUInt32Mixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt32MixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateUInt32Mixer(Array)"/>.
		/// </summary>
		public static Func<UInt32, UInt32> CreateUInt32InverseMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt32InverseMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets one of 2^128 different System.UInt32 mixing functions based on a key.
		/// </summary>
		public static Func<UInt32, UInt32> CreateUInt32Mixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt32MixerInternal(GetKeyBytes(key));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateUInt32Mixer(Array)"/>.
		/// </summary>
		public static Func<UInt32, UInt32> CreateUInt32InverseMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt32InverseMixerInternal(GetKeyBytes(key));
		}

        /// <summary>
        /// Gets a mixing function with a specified cycle structure.
        /// </summary>
        /// <param name="structure">The simplest function having the desired cycle structure.</param>
        /// <param name="g">Random number generator.</param>
        public static Func<UInt32, UInt32> CreateUInt32MixerWithStructure(Func<UInt32, UInt32> structure, Generator g)
        {
            var g2 = g.Clone();
            var f = CreateUInt32Mixer(g);
            var finv = CreateUInt32Mixer(g2);
            return i => finv(structure(f(i)));
        }

		private static Func<UInt32, UInt32> CreateUInt32MixerInternal(byte[] k)
		{
			return n => 
			{
				byte temp;
				var a = (byte)(n >> 0);
				var b = (byte)(n >> 8);
				var c = (byte)(n >> 16);
				var d = (byte)(n >> 24);
				temp = a;
				a = sbox[b ^ k[0]];
				b = sbox[c ^ k[1]];
				c = sbox[d ^ k[2]];
				d = sbox[temp ^ k[3]];
				temp = a;
				a = sbox[b ^ k[4]];
				b = sbox[c ^ k[5]];
				c = sbox[d ^ k[6]];
				d = sbox[temp ^ k[7]];
				temp = a;
				a = sbox[b ^ k[8]];
				b = sbox[c ^ k[9]];
				c = sbox[d ^ k[10]];
				d = sbox[temp ^ k[11]];
				temp = a;
				a = sbox[b ^ k[12]];
				b = sbox[c ^ k[13]];
				c = sbox[d ^ k[14]];
				d = sbox[temp ^ k[15]];
				n = (UInt32)a;
				n = (UInt32)((UInt32)n | ((UInt32)b << 8));
				n = (UInt32)((UInt32)n | ((UInt32)c << 16));
				n = (UInt32)((UInt32)n | ((UInt32)d << 24));
				return n;
			};
		}

		private static Func<UInt32, UInt32> CreateUInt32InverseMixerInternal(byte[] k)
		{
			return n => 
			{
				int temp;
				int a = (byte)(n >> 0);
				int b = (byte)(n >> 8);
				int c = (byte)(n >> 16);
				int d = (byte)(n >> 24);
				temp = rbox[d] ^ k[15];
				d = rbox[c] ^ k[14];
				c = rbox[b] ^ k[13];
				b = rbox[a] ^ k[12];
				a = temp;
				temp = rbox[d] ^ k[11];
				d = rbox[c] ^ k[10];
				c = rbox[b] ^ k[9];
				b = rbox[a] ^ k[8];
				a = temp;
				temp = rbox[d] ^ k[7];
				d = rbox[c] ^ k[6];
				c = rbox[b] ^ k[5];
				b = rbox[a] ^ k[4];
				a = temp;
				temp = rbox[d] ^ k[3];
				d = rbox[c] ^ k[2];
				c = rbox[b] ^ k[1];
				b = rbox[a] ^ k[0];
				a = temp;
				n = (UInt32)a;
				n = (UInt32)((UInt32)n | ((UInt32)b << 8));
				n = (UInt32)((UInt32)n | ((UInt32)c << 16));
				n = (UInt32)((UInt32)n | ((UInt32)d << 24));
				return n;
			};
		}

		/// <summary>
		/// Gets one of 2^128 different System.Int32 mixing functions.
		/// </summary>
		public static Func<Int32, Int32> CreateInt32Mixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateInt32MixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateInt32Mixer(Generator)"/>.
		/// </summary>
		public static Func<Int32, Int32> CreateInt32InverseMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateInt32InverseMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Int32 mixing functions based on a key.
		/// </summary>
		public static Func<Int32, Int32> CreateInt32Mixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt32MixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateInt32Mixer(Array)"/>.
		/// </summary>
		public static Func<Int32, Int32> CreateInt32InverseMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt32InverseMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Int32 mixing functions based on a key.
		/// </summary>
		public static Func<Int32, Int32> CreateInt32Mixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt32MixerInternal(GetKeyBytes(key));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateInt32Mixer(Array)"/>.
		/// </summary>
		public static Func<Int32, Int32> CreateInt32InverseMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt32InverseMixerInternal(GetKeyBytes(key));
		}

        /// <summary>
        /// Gets a mixing function with a specified cycle structure.
        /// </summary>
        /// <param name="structure">The simplest function having the desired cycle structure.</param>
        /// <param name="g">Random number generator.</param>
        public static Func<Int32, Int32> CreateInt32MixerWithStructure(Func<Int32, Int32> structure, Generator g)
        {
            var g2 = g.Clone();
            var f = CreateInt32Mixer(g);
            var finv = CreateInt32Mixer(g2);
            return i => finv(structure(f(i)));
        }

		private static Func<Int32, Int32> CreateInt32MixerInternal(byte[] k)
		{
			return n => 
			{
				byte temp;
				var a = (byte)(n >> 0);
				var b = (byte)(n >> 8);
				var c = (byte)(n >> 16);
				var d = (byte)(n >> 24);
				temp = a;
				a = sbox[b ^ k[0]];
				b = sbox[c ^ k[1]];
				c = sbox[d ^ k[2]];
				d = sbox[temp ^ k[3]];
				temp = a;
				a = sbox[b ^ k[4]];
				b = sbox[c ^ k[5]];
				c = sbox[d ^ k[6]];
				d = sbox[temp ^ k[7]];
				temp = a;
				a = sbox[b ^ k[8]];
				b = sbox[c ^ k[9]];
				c = sbox[d ^ k[10]];
				d = sbox[temp ^ k[11]];
				temp = a;
				a = sbox[b ^ k[12]];
				b = sbox[c ^ k[13]];
				c = sbox[d ^ k[14]];
				d = sbox[temp ^ k[15]];
				n = (Int32)a;
				n = (Int32)((UInt32)n | ((UInt32)b << 8));
				n = (Int32)((UInt32)n | ((UInt32)c << 16));
				n = (Int32)((UInt32)n | ((UInt32)d << 24));
				return n;
			};
		}

		private static Func<Int32, Int32> CreateInt32InverseMixerInternal(byte[] k)
		{
			return n => 
			{
				int temp;
				int a = (byte)(n >> 0);
				int b = (byte)(n >> 8);
				int c = (byte)(n >> 16);
				int d = (byte)(n >> 24);
				temp = rbox[d] ^ k[15];
				d = rbox[c] ^ k[14];
				c = rbox[b] ^ k[13];
				b = rbox[a] ^ k[12];
				a = temp;
				temp = rbox[d] ^ k[11];
				d = rbox[c] ^ k[10];
				c = rbox[b] ^ k[9];
				b = rbox[a] ^ k[8];
				a = temp;
				temp = rbox[d] ^ k[7];
				d = rbox[c] ^ k[6];
				c = rbox[b] ^ k[5];
				b = rbox[a] ^ k[4];
				a = temp;
				temp = rbox[d] ^ k[3];
				d = rbox[c] ^ k[2];
				c = rbox[b] ^ k[1];
				b = rbox[a] ^ k[0];
				a = temp;
				n = (Int32)a;
				n = (Int32)((UInt32)n | ((UInt32)b << 8));
				n = (Int32)((UInt32)n | ((UInt32)c << 16));
				n = (Int32)((UInt32)n | ((UInt32)d << 24));
				return n;
			};
		}

		/// <summary>
		/// Gets one of 2^128 different System.UInt64 mixing functions.
		/// </summary>
		public static Func<UInt64, UInt64> CreateUInt64Mixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateUInt64MixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateUInt64Mixer(Generator)"/>.
		/// </summary>
		public static Func<UInt64, UInt64> CreateUInt64InverseMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateUInt64InverseMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets one of 2^128 different System.UInt64 mixing functions based on a key.
		/// </summary>
		public static Func<UInt64, UInt64> CreateUInt64Mixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt64MixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateUInt64Mixer(Array)"/>.
		/// </summary>
		public static Func<UInt64, UInt64> CreateUInt64InverseMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt64InverseMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets one of 2^128 different System.UInt64 mixing functions based on a key.
		/// </summary>
		public static Func<UInt64, UInt64> CreateUInt64Mixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt64MixerInternal(GetKeyBytes(key));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateUInt64Mixer(Array)"/>.
		/// </summary>
		public static Func<UInt64, UInt64> CreateUInt64InverseMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateUInt64InverseMixerInternal(GetKeyBytes(key));
		}

        /// <summary>
        /// Gets a mixing function with a specified cycle structure.
        /// </summary>
        /// <param name="structure">The simplest function having the desired cycle structure.</param>
        /// <param name="g">Random number generator.</param>
        public static Func<UInt64, UInt64> CreateUInt64MixerWithStructure(Func<UInt64, UInt64> structure, Generator g)
        {
            var g2 = g.Clone();
            var f = CreateUInt64Mixer(g);
            var finv = CreateUInt64Mixer(g2);
            return i => finv(structure(f(i)));
        }

		private static Func<UInt64, UInt64> CreateUInt64MixerInternal(byte[] k)
		{
			return n => 
			{
				byte temp;
				var a = (byte)(n >> 0);
				var b = (byte)(n >> 8);
				var c = (byte)(n >> 16);
				var d = (byte)(n >> 24);
				var e = (byte)(n >> 32);
				var f = (byte)(n >> 40);
				var g = (byte)(n >> 48);
				var h = (byte)(n >> 56);
				temp = a;
				a = sbox[b ^ k[0]];
				b = sbox[c ^ k[1]];
				c = sbox[d ^ k[2]];
				d = sbox[e ^ k[3]];
				e = sbox[f ^ k[4]];
				f = sbox[g ^ k[5]];
				g = sbox[h ^ k[6]];
				h = sbox[temp ^ k[7]];
				temp = a;
				a = sbox[b ^ k[8]];
				b = sbox[c ^ k[9]];
				c = sbox[d ^ k[10]];
				d = sbox[e ^ k[11]];
				e = sbox[f ^ k[12]];
				f = sbox[g ^ k[13]];
				g = sbox[h ^ k[14]];
				h = sbox[temp ^ k[15]];
				n = (UInt64)a;
				n = (UInt64)((UInt64)n | ((UInt64)b << 8));
				n = (UInt64)((UInt64)n | ((UInt64)c << 16));
				n = (UInt64)((UInt64)n | ((UInt64)d << 24));
				n = (UInt64)((UInt64)n | ((UInt64)e << 32));
				n = (UInt64)((UInt64)n | ((UInt64)f << 40));
				n = (UInt64)((UInt64)n | ((UInt64)g << 48));
				n = (UInt64)((UInt64)n | ((UInt64)h << 56));
				return n;
			};
		}

		private static Func<UInt64, UInt64> CreateUInt64InverseMixerInternal(byte[] k)
		{
			return n => 
			{
				int temp;
				int a = (byte)(n >> 0);
				int b = (byte)(n >> 8);
				int c = (byte)(n >> 16);
				int d = (byte)(n >> 24);
				int e = (byte)(n >> 32);
				int f = (byte)(n >> 40);
				int g = (byte)(n >> 48);
				int h = (byte)(n >> 56);
				temp = rbox[h] ^ k[15];
				h = rbox[g] ^ k[14];
				g = rbox[f] ^ k[13];
				f = rbox[e] ^ k[12];
				e = rbox[d] ^ k[11];
				d = rbox[c] ^ k[10];
				c = rbox[b] ^ k[9];
				b = rbox[a] ^ k[8];
				a = temp;
				temp = rbox[h] ^ k[7];
				h = rbox[g] ^ k[6];
				g = rbox[f] ^ k[5];
				f = rbox[e] ^ k[4];
				e = rbox[d] ^ k[3];
				d = rbox[c] ^ k[2];
				c = rbox[b] ^ k[1];
				b = rbox[a] ^ k[0];
				a = temp;
				n = (UInt64)a;
				n = (UInt64)((UInt64)n | ((UInt64)b << 8));
				n = (UInt64)((UInt64)n | ((UInt64)c << 16));
				n = (UInt64)((UInt64)n | ((UInt64)d << 24));
				n = (UInt64)((UInt64)n | ((UInt64)e << 32));
				n = (UInt64)((UInt64)n | ((UInt64)f << 40));
				n = (UInt64)((UInt64)n | ((UInt64)g << 48));
				n = (UInt64)((UInt64)n | ((UInt64)h << 56));
				return n;
			};
		}

		/// <summary>
		/// Gets one of 2^128 different System.Int64 mixing functions.
		/// </summary>
		public static Func<Int64, Int64> CreateInt64Mixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateInt64MixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateInt64Mixer(Generator)"/>.
		/// </summary>
		public static Func<Int64, Int64> CreateInt64InverseMixer(Generator generator)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			return CreateInt64InverseMixerInternal(generator.CreateBytes(16));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Int64 mixing functions based on a key.
		/// </summary>
		public static Func<Int64, Int64> CreateInt64Mixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt64MixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateInt64Mixer(Array)"/>.
		/// </summary>
		public static Func<Int64, Int64> CreateInt64InverseMixer(Array key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt64InverseMixerInternal(GetKeyBytes(key.GetBytes()));
		}

		/// <summary>
		/// Gets one of 2^128 different System.Int64 mixing functions based on a key.
		/// </summary>
		public static Func<Int64, Int64> CreateInt64Mixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt64MixerInternal(GetKeyBytes(key));
		}

		/// <summary>
		/// Gets the inverse function of the corresponding function returned by <see cref="CreateInt64Mixer(Array)"/>.
		/// </summary>
		public static Func<Int64, Int64> CreateInt64InverseMixer<T>(T[] key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (key.Length == 0)
				throw new ArgumentException(nameof(key));

			return CreateInt64InverseMixerInternal(GetKeyBytes(key));
		}

        /// <summary>
        /// Gets a mixing function with a specified cycle structure.
        /// </summary>
        /// <param name="structure">The simplest function having the desired cycle structure.</param>
        /// <param name="g">Random number generator.</param>
        public static Func<Int64, Int64> CreateInt64MixerWithStructure(Func<Int64, Int64> structure, Generator g)
        {
            var g2 = g.Clone();
            var f = CreateInt64Mixer(g);
            var finv = CreateInt64Mixer(g2);
            return i => finv(structure(f(i)));
        }

		private static Func<Int64, Int64> CreateInt64MixerInternal(byte[] k)
		{
			return n => 
			{
				byte temp;
				var a = (byte)(n >> 0);
				var b = (byte)(n >> 8);
				var c = (byte)(n >> 16);
				var d = (byte)(n >> 24);
				var e = (byte)(n >> 32);
				var f = (byte)(n >> 40);
				var g = (byte)(n >> 48);
				var h = (byte)(n >> 56);
				temp = a;
				a = sbox[b ^ k[0]];
				b = sbox[c ^ k[1]];
				c = sbox[d ^ k[2]];
				d = sbox[e ^ k[3]];
				e = sbox[f ^ k[4]];
				f = sbox[g ^ k[5]];
				g = sbox[h ^ k[6]];
				h = sbox[temp ^ k[7]];
				temp = a;
				a = sbox[b ^ k[8]];
				b = sbox[c ^ k[9]];
				c = sbox[d ^ k[10]];
				d = sbox[e ^ k[11]];
				e = sbox[f ^ k[12]];
				f = sbox[g ^ k[13]];
				g = sbox[h ^ k[14]];
				h = sbox[temp ^ k[15]];
				n = (Int64)a;
				n = (Int64)((UInt64)n | ((UInt64)b << 8));
				n = (Int64)((UInt64)n | ((UInt64)c << 16));
				n = (Int64)((UInt64)n | ((UInt64)d << 24));
				n = (Int64)((UInt64)n | ((UInt64)e << 32));
				n = (Int64)((UInt64)n | ((UInt64)f << 40));
				n = (Int64)((UInt64)n | ((UInt64)g << 48));
				n = (Int64)((UInt64)n | ((UInt64)h << 56));
				return n;
			};
		}

		private static Func<Int64, Int64> CreateInt64InverseMixerInternal(byte[] k)
		{
			return n => 
			{
				int temp;
				int a = (byte)(n >> 0);
				int b = (byte)(n >> 8);
				int c = (byte)(n >> 16);
				int d = (byte)(n >> 24);
				int e = (byte)(n >> 32);
				int f = (byte)(n >> 40);
				int g = (byte)(n >> 48);
				int h = (byte)(n >> 56);
				temp = rbox[h] ^ k[15];
				h = rbox[g] ^ k[14];
				g = rbox[f] ^ k[13];
				f = rbox[e] ^ k[12];
				e = rbox[d] ^ k[11];
				d = rbox[c] ^ k[10];
				c = rbox[b] ^ k[9];
				b = rbox[a] ^ k[8];
				a = temp;
				temp = rbox[h] ^ k[7];
				h = rbox[g] ^ k[6];
				g = rbox[f] ^ k[5];
				f = rbox[e] ^ k[4];
				e = rbox[d] ^ k[3];
				d = rbox[c] ^ k[2];
				c = rbox[b] ^ k[1];
				b = rbox[a] ^ k[0];
				a = temp;
				n = (Int64)a;
				n = (Int64)((UInt64)n | ((UInt64)b << 8));
				n = (Int64)((UInt64)n | ((UInt64)c << 16));
				n = (Int64)((UInt64)n | ((UInt64)d << 24));
				n = (Int64)((UInt64)n | ((UInt64)e << 32));
				n = (Int64)((UInt64)n | ((UInt64)f << 40));
				n = (Int64)((UInt64)n | ((UInt64)g << 48));
				n = (Int64)((UInt64)n | ((UInt64)h << 56));
				return n;
			};
		}

		private static byte[] sbox = new byte[256] 
		{
			0xA3,0xD7,0x09,0x83,0xF8,0x48,0xF6,0xF4,0xB3,0x21,0x15,0x78,0x99,0xB1,0xAF,0xF9,
			0xE7,0x2D,0x4D,0x8A,0xCE,0x4C,0xCA,0x2E,0x52,0x95,0xD9,0x1E,0x4E,0x38,0x44,0x28,
			0x0A,0xDF,0x02,0xA0,0x17,0xF1,0x60,0x68,0x12,0xB7,0x7A,0xC3,0xE9,0xFA,0x3D,0x53,
			0x96,0x84,0x6B,0xBA,0xF2,0x63,0x9A,0x19,0x7C,0xAE,0xE5,0xF5,0xF7,0x16,0x6A,0xA2,
			0x39,0xB6,0x7B,0x0F,0xC1,0x93,0x81,0x1B,0xEE,0xB4,0x1A,0xEA,0xD0,0x91,0x2F,0xB8,
			0x55,0xB9,0xDA,0x85,0x3F,0x41,0xBF,0xE0,0x5A,0x58,0x80,0x5F,0x66,0x0B,0xD8,0x90,
			0x35,0xD5,0xC0,0xA7,0x33,0x06,0x65,0x69,0x45,0x00,0x94,0x56,0x6D,0x98,0x9B,0x76,
			0x97,0xFC,0xB2,0xC2,0xB0,0xFE,0xDB,0x20,0xE1,0xEB,0xD6,0xE4,0xDD,0x47,0x4A,0x1D,
			0x42,0xED,0x9E,0x6E,0x49,0x3C,0xCD,0x43,0x27,0xD2,0x07,0xD4,0xDE,0xC7,0x67,0x18,
			0x89,0xCB,0x30,0x1F,0x8D,0xC6,0x8F,0xAA,0xC8,0x74,0xDC,0xC9,0x5D,0x5C,0x31,0xA4,
			0x70,0x88,0x61,0x2C,0x9F,0x0D,0x2B,0x87,0x50,0x82,0x54,0x64,0x26,0x7D,0x03,0x40,
			0x34,0x4B,0x1C,0x73,0xD1,0xC4,0xFD,0x3B,0xCC,0xFB,0x7F,0xAB,0xE6,0x3E,0x5B,0xA5,
			0xAD,0x04,0x23,0x9C,0x14,0x51,0x22,0xF0,0x29,0x79,0x71,0x7E,0xFF,0x8C,0x0E,0xE2,
			0x0C,0xEF,0xBC,0x72,0x75,0x6F,0x37,0xA1,0xEC,0xD3,0x8E,0x62,0x8B,0x86,0x10,0xE8,
			0x08,0x77,0x11,0xBE,0x92,0x4F,0x24,0xC5,0x32,0x36,0x9D,0xCF,0xF3,0xA6,0xBB,0xAC,
			0x5E,0x6C,0xA9,0x13,0x57,0x25,0xB5,0xE3,0xBD,0xA8,0x3A,0x01,0x05,0x59,0x2A,0x46,
		};

        private static byte[] rbox = new byte[256]
        {
            0x69,0xFB,0x22,0xAE,0xC1,0xFC,0x65,0x8A,0xE0,0x02,0x20,0x5D,0xD0,0xA5,0xCE,0x43,
            0xDE,0xE2,0x28,0xF3,0xC4,0x0A,0x3D,0x24,0x8F,0x37,0x4A,0x47,0xB2,0x7F,0x1B,0x93,
            0x77,0x09,0xC6,0xC2,0xE6,0xF5,0xAC,0x88,0x1F,0xC8,0xFE,0xA6,0xA3,0x11,0x17,0x4E,
            0x92,0x9E,0xE8,0x64,0xB0,0x60,0xE9,0xD6,0x1D,0x40,0xFA,0xB7,0x85,0x2E,0xBD,0x54,
            0xAF,0x55,0x80,0x87,0x1E,0x68,0xFF,0x7D,0x05,0x84,0x7E,0xB1,0x15,0x12,0x1C,0xE5,
            0xA8,0xC5,0x18,0x2F,0xAA,0x50,0x6B,0xF4,0x59,0xFD,0x58,0xBE,0x9D,0x9C,0xF0,0x5B,
            0x26,0xA2,0xDB,0x35,0xAB,0x66,0x5C,0x8E,0x27,0x67,0x3E,0x32,0xF1,0x6C,0x83,0xD5,
            0xA0,0xCA,0xD3,0xB3,0x99,0xD4,0x6F,0xE1,0x0B,0xC9,0x2A,0x42,0x38,0xAD,0xCB,0xBA,
            0x5A,0x46,0xA9,0x03,0x31,0x53,0xDD,0xA7,0xA1,0x90,0x13,0xDC,0xCD,0x94,0xDA,0x96,
            0x5F,0x4D,0xE4,0x45,0x6A,0x19,0x30,0x70,0x6D,0x0C,0x36,0x6E,0xC3,0xEA,0x82,0xA4,
            0x23,0xD7,0x3F,0x00,0x9F,0xBF,0xED,0x63,0xF9,0xF2,0x97,0xBB,0xEF,0xC0,0x39,0x0E,
            0x74,0x0D,0x72,0x08,0x49,0xF6,0x41,0x29,0x4F,0x51,0x33,0xEE,0xD2,0xF8,0xE3,0x56,
            0x62,0x44,0x73,0x2B,0xB5,0xE7,0x95,0x8D,0x98,0x9B,0x16,0x91,0xB8,0x86,0x14,0xEB,
            0x4C,0xB4,0x89,0xD9,0x8B,0x61,0x7A,0x01,0x5E,0x1A,0x52,0x76,0x9A,0x7C,0x8C,0x21,
            0x57,0x78,0xCF,0xF7,0x7B,0x3A,0xBC,0x10,0xDF,0x2C,0x4B,0x79,0xD8,0x81,0x48,0xD1,
            0xC7,0x25,0x34,0xEC,0x07,0x3B,0x06,0x3C,0x04,0x0F,0x2D,0xB9,0x71,0xB6,0x75,0xCC,
        };

		private static byte[] GetKeyBytes<T>(T[] key)
		{
			var g = new Generator(key.GetBytes());
			return g.CreateBytes(16);
		}
    }
}
