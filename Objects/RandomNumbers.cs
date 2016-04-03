
/*
RandomNumbers.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Foundations.Objects
{
    /// <summary>
    /// Pseudo-random number generator.
    /// </summary>
    public sealed partial class RandomNumbers
    {
        private static byte[] MakeBytes<T>(T[] seed)
        {
            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            int n = Buffer.ByteLength(seed);
            var bytes = new byte[n];
            Buffer.BlockCopy(seed, 0, bytes, 0, n);
            return bytes;
        }
     
        /// <summary>
        /// Create a pseudo-random number generator initialized using the current value of the high-precision timer.
        /// </summary>
        public RandomNumbers()
            : this(new[] { Stopwatch.GetTimestamp() })
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params byte[] seed)
        {
            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            state = new ulong[16];

            using (var sha = System.Security.Cryptography.SHA256.Create())
            {

                for (int i = 0; i < 4; i++)
                {
                    seed = sha.ComputeHash(seed);
                    Buffer.BlockCopy(seed, 0, state, 32 * i, 32);
                }
            }
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params ulong[] seed)
            : this(MakeBytes(seed))
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params long[] seed)
            : this(MakeBytes(seed))
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params uint[] seed)
            : this(MakeBytes(seed))
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params int[] seed)
            : this(MakeBytes(seed))
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params ushort[] seed)
            : this(MakeBytes(seed))
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params short[] seed)
            : this(MakeBytes(seed))
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params sbyte[] seed)
            : this(MakeBytes(seed))
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params char[] seed)
            : this(MakeBytes(seed))
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params float[] seed)
            : this(MakeBytes(seed))
        {
        }

        /// <summary>
        /// Create a pseudo-random number generator initialized with provided values.
        /// </summary>
        public RandomNumbers(params double[] seed)
            : this(MakeBytes(seed))
        {
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Sample
        {
            [FieldOffset(0)]
            public ulong UInt64_0;

            [FieldOffset(0)]
            public long Int64_0;

            [FieldOffset(0)]
            public uint UInt32_0;

            [FieldOffset(4)]
            public uint UInt32_1;

            [FieldOffset(0)]
            public int Int32_0;

            [FieldOffset(4)]
            public int Int32_1;

            [FieldOffset(0)]
            public ushort UInt16_0;

            [FieldOffset(2)]
            public ushort UInt16_1;

            [FieldOffset(4)]
            public ushort UInt16_2;

            [FieldOffset(6)]
            public ushort UInt16_3;

            [FieldOffset(0)]
            public short Int16_0;

            [FieldOffset(2)]
            public short Int16_1;

            [FieldOffset(4)]
            public short Int16_2;

            [FieldOffset(6)]
            public short Int16_3;

            [FieldOffset(0)]
            public byte Byte_0;

            [FieldOffset(1)]
            public byte Byte_1;

            [FieldOffset(2)]
            public byte Byte_2;

            [FieldOffset(3)]
            public byte Byte_3;

            [FieldOffset(4)]
            public byte Byte_4;

            [FieldOffset(5)]
            public byte Byte_5;

            [FieldOffset(6)]
            public byte Byte_6;

            [FieldOffset(7)]
            public byte Byte_7;

            [FieldOffset(0)]
            public sbyte SByte_0;

            [FieldOffset(1)]
            public sbyte SByte_1;

            [FieldOffset(2)]
            public sbyte SByte_2;

            [FieldOffset(3)]
            public sbyte SByte_3;

            [FieldOffset(4)]
            public sbyte SByte_4;

            [FieldOffset(5)]
            public sbyte SByte_5;

            [FieldOffset(6)]
            public sbyte SByte_6;

            [FieldOffset(7)]
            public sbyte SByte_7;

            [FieldOffset(0)]
            public double Double_0;

            [FieldOffset(0)]
            public float Single_0;

            [FieldOffset(4)]
            public float Single_1;
        }

        private ulong[] state;
        private int index;

#pragma warning disable 0169
        private Sample sample;
#pragma warning restore 0169

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="UInt64"/>.
        /// </summary>
        private ulong Mix()
        {
            ulong prev = state[index];
            index = (index + 1) & 0xF;
            var next = state[index];
            next ^= next << 31;
            return sample.UInt64_0 = (state[index] = next ^ prev ^ (next >> 11) ^ (prev >> 30)) * 1181783497276652981UL;
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="UInt64"/>.
        /// </summary>
        public UInt64 NextUInt64()
        {
            return (UInt64)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt64[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 1)
            {
                Mix();
                array[offset++] = sample.UInt64_0;
                count -= 1;
            }
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Int64"/>.
        /// </summary>
        public Int64 NextInt64()
        {
            return (Int64)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int64[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int64[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 1)
            {
                Mix();
                array[offset++] = sample.Int64_0;
                count -= 1;
            }
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="UInt32"/>.
        /// </summary>
        public UInt32 NextUInt32()
        {
            return (UInt32)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt32[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 2)
            {
                Mix();
                array[offset++] = sample.UInt32_1;
                array[offset++] = sample.UInt32_0;
                count -= 2;
            }

            if (count == 0)
                return;

            Mix();
             
            array[offset++] = sample.UInt32_1;
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Int32"/>.
        /// </summary>
        public Int32 NextInt32()
        {
            return (Int32)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int32[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int32[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 2)
            {
                Mix();
                array[offset++] = sample.Int32_1;
                array[offset++] = sample.Int32_0;
                count -= 2;
            }

            if (count == 0)
                return;

            Mix();
             
            array[offset++] = sample.Int32_1;
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="UInt16"/>.
        /// </summary>
        public UInt16 NextUInt16()
        {
            return (UInt16)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt16[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(UInt16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 4)
            {
                Mix();
                array[offset++] = sample.UInt16_3;
                array[offset++] = sample.UInt16_2;
                array[offset++] = sample.UInt16_1;
                array[offset++] = sample.UInt16_0;
                count -= 4;
            }

            if (count == 0)
                return;

            Mix();

            switch (count)
            {
                case 3: array[offset++] = sample.UInt16_3; goto case 2;
                case 2: array[offset++] = sample.UInt16_2; goto case 1;
                case 1: array[offset++] = sample.UInt16_1; break;
            }
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Int16"/>.
        /// </summary>
        public Int16 NextInt16()
        {
            return (Int16)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int16[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Int16[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 4)
            {
                Mix();
                array[offset++] = sample.Int16_3;
                array[offset++] = sample.Int16_2;
                array[offset++] = sample.Int16_1;
                array[offset++] = sample.Int16_0;
                count -= 4;
            }

            if (count == 0)
                return;

            Mix();

            switch (count)
            {
                case 3: array[offset++] = sample.Int16_3; goto case 2;
                case 2: array[offset++] = sample.Int16_2; goto case 1;
                case 1: array[offset++] = sample.Int16_1; break;
            }
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Byte"/>.
        /// </summary>
        public Byte NextByte()
        {
            return (Byte)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Byte[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Byte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 8)
            {
                Mix();
                array[offset++] = sample.Byte_7;
                array[offset++] = sample.Byte_6;
                array[offset++] = sample.Byte_5;
                array[offset++] = sample.Byte_4;
                array[offset++] = sample.Byte_3;
                array[offset++] = sample.Byte_2;
                array[offset++] = sample.Byte_1;
                array[offset++] = sample.Byte_0;
                count -= 8;
            }

            if (count == 0)
                return;

            Mix();

            switch (count)
            {
                case 7: array[offset++] = sample.Byte_7; goto case 6;
                case 6: array[offset++] = sample.Byte_6; goto case 5;
                case 5: array[offset++] = sample.Byte_5; goto case 4;
                case 4: array[offset++] = sample.Byte_4; goto case 3;
                case 3: array[offset++] = sample.Byte_3; goto case 2;
                case 2: array[offset++] = sample.Byte_2; goto case 1;
                case 1: array[offset++] = sample.Byte_1; break;
            }
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="SByte"/>.
        /// </summary>
        public SByte NextSByte()
        {
            return (SByte)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(SByte[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(SByte[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 8)
            {
                Mix();
                array[offset++] = sample.SByte_7;
                array[offset++] = sample.SByte_6;
                array[offset++] = sample.SByte_5;
                array[offset++] = sample.SByte_4;
                array[offset++] = sample.SByte_3;
                array[offset++] = sample.SByte_2;
                array[offset++] = sample.SByte_1;
                array[offset++] = sample.SByte_0;
                count -= 8;
            }

            if (count == 0)
                return;

            Mix();

            switch (count)
            {
                case 7: array[offset++] = sample.SByte_7; goto case 6;
                case 6: array[offset++] = sample.SByte_6; goto case 5;
                case 5: array[offset++] = sample.SByte_5; goto case 4;
                case 4: array[offset++] = sample.SByte_4; goto case 3;
                case 3: array[offset++] = sample.SByte_3; goto case 2;
                case 2: array[offset++] = sample.SByte_2; goto case 1;
                case 1: array[offset++] = sample.SByte_1; break;
            }
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Double"/>.
        /// </summary>
        public Double NextDouble()
        {
            return (Double)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Double[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Double[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 1)
            {
                Mix();
                sample.UInt64_0 = (sample.UInt64_0 & 0x000FFFFFFFFFFFFF) | 0x3FF0000000000000;
                sample.Double_0 -= 1d;
                array[offset++] = sample.Double_0;
                count -= 1;
            }
        }

        /// <summary>
        /// Get the next value in the pseudo-random sequence as a <see cref="Single"/>.
        /// </summary>
        public Single NextSingle()
        {
            return (Single)Mix();
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Single[] array)
        {
            GetNext(array, 0, array.Length);
        }

        /// <summary>
        /// Fill a provided array of <see cref="UInt64"/> values with the next values in the pseudo-random sequence.
        /// </summary>
        public void GetNext(Single[] array, int offset, int count)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));

            if (offset < 0 || offset >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 1 || count > array.Length - offset) 
                throw new ArgumentOutOfRangeException(nameof(count));

            while (count >= 2)
            {
                Mix();
                sample.UInt32_1 = (sample.UInt32_1 & 0x007FFFFF) | 0x3F800000;
                sample.Single_1 -= 1f;
                array[offset++] = sample.Single_1;
                sample.UInt32_0 = (sample.UInt32_0 & 0x007FFFFF) | 0x3F800000;
                sample.Single_0 -= 1f;
                array[offset++] = sample.Single_0;
                count -= 2;
            }

            if (count == 0)
                return;

            Mix();
                            sample.UInt32_1 = (sample.UInt32_1 & 0x007FFFFF) | 0x3F800000;
                sample.Single_1 -= 1f;
 
            array[offset++] = sample.Single_1;
        }
    }
}
