
/*
IByteEncoding.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.IO;

namespace Foundations.Coding
{
    /// <summary>
    /// Represent values as sequences of bytes. This is intended to be used
    /// as a primitive operation byte serializers. It is generally assumed the
    /// values nearer to zero will require fewer bytes.
    /// </summary>
    public interface IByteEncoding
    {
        /// <summary>
        /// Write a <see cref="System.UInt64"/> to a <see cref="Stream"/>.
        /// </summary>
        void Write(Stream stream, ulong value);
        
        /// <summary>
        /// Read a <see cref="System.Int64"/> from a <see cref="Stream"/>.
        /// </summary>
        ulong Read(Stream stream);
    }

    /// <summary>
    /// Extension methods for <see cref="IByteEncoding"/>.
    /// </summary>
    public static class IByteEncodingExtensions
    {
        /// <summary>
        /// Write a <see cref="System.Int64"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, long value)
        {
            encoding.Write(stream, ((ulong)value >> 63) | ((ulong)value << 1));
        }

        /// <summary>
        /// Write a <see cref="System.UInt32"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, uint value)
        {
            encoding.Write(stream, (ulong)value);
        }

        /// <summary>
        /// Write a <see cref="System.Int32"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, int value)
        {
            encoding.Write(stream, ((uint)value >> 31) | ((uint)value << 1));
        }

        /// <summary>
        /// Write a <see cref="System.UInt16"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, ushort value)
        {
            encoding.Write(stream, (ulong)value);
        }

        /// <summary>
        /// Write a <see cref="System.Int16"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, short value)
        {
            encoding.Write(stream, ((ushort)value >> 15) | ((ushort)value << 1));
        }

        /// <summary>
        /// Write a <see cref="System.Byte"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, byte value)
        {
            encoding.Write(stream, (ulong)value);
        }

        /// <summary>
        /// Write a <see cref="System.SByte"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, sbyte value)
        {
            encoding.Write(stream, ((byte)value >> 7) | ((byte)value << 1));
        }

        /// <summary>
        /// Write a <see cref="System.Char"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, char value)
        {
            encoding.Write(stream, (ulong)value);
        }

        /// <summary>
        /// Write a <see cref="System.Boolean"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, bool value)
        {
            encoding.Write(stream, value ? 1L : 0L);
        }

        /// <summary>
        /// Write a <see cref="System.DateTime"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, DateTime value)
        {
            encoding.Write(stream, value.Ticks);
            encoding.Write(stream, (ulong)value.Kind);
        }
    }
}