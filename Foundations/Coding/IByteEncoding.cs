
/*
IByteEncoding.cs

Copyright © 2016 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        ulong ReadUInt64(Stream stream);
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
            encoding.Write(stream, (ulong)((value >> 63) ^ (value << 1)));
        }

        /// <summary>
        /// Write a <see cref="System.Int32"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, int value)
        {
            encoding.Write(stream, (uint)((value >> 31) ^ (value << 1)));
        }

        /// <summary>
        /// Write a <see cref="System.Int16"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, short value)
        {
            encoding.Write(stream, (ushort)((value >> 15) ^ (value << 1)));
        }

        /// <summary>
        /// Write a <see cref="System.SByte"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, sbyte value)
        {
            encoding.Write(stream, (byte)((value >> 7) ^ (value << 1)));
        }

        /// <summary>
        /// Write a <see cref="System.Boolean"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, bool value)
        {
            encoding.Write(stream, value ? 1UL : 0UL);
        }

        /// <summary>
        /// Write a <see cref="System.DateTime"/> to a <see cref="Stream"/>.
        /// </summary>
        public static void Write(this IByteEncoding encoding, Stream stream, DateTime value)
        {
            encoding.Write(stream, value.Ticks);
            encoding.Write(stream, (ulong)value.Kind);
        }

        /// <summary>
        /// Read a <see cref="System.Int64"/> from a <see cref="Stream"/>.
        /// </summary>
        public static long ReadInt64(this IByteEncoding encoding, Stream stream)
        {
            var u = encoding.ReadUInt64(stream);
            return (long)(u >> 1) ^ (((long)u & 1) * -1L);
        }

        /// <summary>
        /// Read a <see cref="System.UInt32"/> from a <see cref="Stream"/>.
        /// </summary>
        public static uint ReadUInt32(this IByteEncoding encoding, Stream stream)
        {
            var u = encoding.ReadUInt64(stream);
            if (u > uint.MaxValue) throw new InvalidDataException("Encoded value exceeds UInt32 bounds.");
            return (uint)u;
        }

        /// <summary>
        /// Read a <see cref="System.Int32"/> from a <see cref="Stream"/>.
        /// </summary>
        public static int ReadInt32(this IByteEncoding encoding, Stream stream)
        {
            var u = encoding.ReadUInt64(stream);
            if (u > uint.MaxValue) throw new InvalidDataException("Encoded value exceeds Int32 bounds.");
            return (int)(u >> 1) ^ (((int)u & 1) * -1);
        }

        /// <summary>
        /// Read a <see cref="System.UInt16"/> from a <see cref="Stream"/>.
        /// </summary>
        public static ushort ReadUInt16(this IByteEncoding encoding, Stream stream)
        {
            var u = encoding.ReadUInt64(stream);
            if (u > ushort.MaxValue) throw new InvalidDataException("Encoded value exceeds UInt16 bounds.");
            return (ushort)u;
        }

        /// <summary>
        /// Read a <see cref="System.Int16"/> from a <see cref="Stream"/>.
        /// </summary>
        public static short ReadInt16(this IByteEncoding encoding, Stream stream)
        {
            var u = encoding.ReadUInt64(stream);
            if (u > ushort.MaxValue) throw new InvalidDataException("Encoded value exceeds Int16 bounds.");
            return (short)((short)(u >> 1) ^ (((short)u & 1) * -1));
        }

        /// <summary>
        /// Read a <see cref="System.Byte"/> from a <see cref="Stream"/>.
        /// </summary>
        public static byte ReadByte(this IByteEncoding encoding, Stream stream)
        {
            var u = encoding.ReadUInt64(stream);
            if (u > byte.MaxValue) throw new InvalidDataException("Encoded value exceeds Byte bounds.");
            return (byte)u;
        }

        /// <summary>
        /// Read a <see cref="System.SByte"/> from a <see cref="Stream"/>.
        /// </summary>
        public static sbyte ReadSByte(this IByteEncoding encoding, Stream stream)
        {
            var u = encoding.ReadUInt64(stream);
            if (u > byte.MaxValue) throw new InvalidDataException("Encoded value exceeds SByte bounds.");
            return (sbyte)((sbyte)(u >> 1) ^ (((sbyte)u & 1) * -1));
        }

        /// <summary>
        /// Read a <see cref="System.Char"/> from a <see cref="Stream"/>.
        /// </summary>
        public static char ReadChar(this IByteEncoding encoding, Stream stream)
        {
            var u = encoding.ReadUInt64(stream);
            if (u > ushort.MaxValue) throw new InvalidDataException("Encoded value exceeds Char bounds.");
            return (char)u;
        }

        /// <summary>
        /// Read a <see cref="System.Boolean"/> from a <see cref="Stream"/>.
        /// </summary>
        public static bool ReadBoolean(this IByteEncoding encoding, Stream stream)
        {
            var u = encoding.ReadUInt64(stream);
            if (u > 1) throw new InvalidDataException("Encoded value exceeds Boolean bounds.");
            return u == 1;
        }

        static readonly HashSet<DateTimeKind> validDataTimeKinds =
            new HashSet<DateTimeKind>(Enum.GetValues(typeof(DateTimeKind)).Cast<DateTimeKind>());

        /// <summary>
        /// Read a <see cref="System.DateTime"/> from a <see cref="Stream"/>.
        /// </summary>
        public static DateTime ReadDateTime(this IByteEncoding encoding, Stream stream)
        {
            var ticks = encoding.ReadInt64(stream);
            var kind = (DateTimeKind)encoding.ReadUInt64(stream);
            if (!validDataTimeKinds.Contains(kind)) throw new InvalidDataException("Encoded value is not valid DateTimeKind.");
            return new DateTime(ticks, kind);
        }
    }
}