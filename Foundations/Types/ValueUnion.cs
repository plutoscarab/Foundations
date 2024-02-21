
using System.Runtime.InteropServices;

namespace Foundations.Types
{
    /// <summary>
    /// A union type containing overlayed representations of most CLR primitive types.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
	public partial struct ValueUnion
    {
        /// <summary>
        /// Instance 0 of <see cref="Int64"/> field.
        /// </summary>
        [FieldOffset(0)]
        public Int64 Int64_0;

        /// <summary>
        /// Instance 0 of <see cref="UInt64"/> field.
        /// </summary>
        [FieldOffset(0)]
        public UInt64 UInt64_0;

        /// <summary>
        /// Instance 0 of <see cref="Int32"/> field.
        /// </summary>
        [FieldOffset(0)]
        public Int32 Int32_0;

        /// <summary>
        /// Instance 1 of <see cref="Int32"/> field.
        /// </summary>
        [FieldOffset(4)]
        public Int32 Int32_1;

        /// <summary>
        /// Instance 0 of <see cref="UInt32"/> field.
        /// </summary>
        [FieldOffset(0)]
        public UInt32 UInt32_0;

        /// <summary>
        /// Instance 1 of <see cref="UInt32"/> field.
        /// </summary>
        [FieldOffset(4)]
        public UInt32 UInt32_1;

        /// <summary>
        /// Instance 0 of <see cref="Int16"/> field.
        /// </summary>
        [FieldOffset(0)]
        public Int16 Int16_0;

        /// <summary>
        /// Instance 1 of <see cref="Int16"/> field.
        /// </summary>
        [FieldOffset(2)]
        public Int16 Int16_1;

        /// <summary>
        /// Instance 2 of <see cref="Int16"/> field.
        /// </summary>
        [FieldOffset(4)]
        public Int16 Int16_2;

        /// <summary>
        /// Instance 3 of <see cref="Int16"/> field.
        /// </summary>
        [FieldOffset(6)]
        public Int16 Int16_3;

        /// <summary>
        /// Instance 0 of <see cref="UInt16"/> field.
        /// </summary>
        [FieldOffset(0)]
        public UInt16 UInt16_0;

        /// <summary>
        /// Instance 1 of <see cref="UInt16"/> field.
        /// </summary>
        [FieldOffset(2)]
        public UInt16 UInt16_1;

        /// <summary>
        /// Instance 2 of <see cref="UInt16"/> field.
        /// </summary>
        [FieldOffset(4)]
        public UInt16 UInt16_2;

        /// <summary>
        /// Instance 3 of <see cref="UInt16"/> field.
        /// </summary>
        [FieldOffset(6)]
        public UInt16 UInt16_3;

        /// <summary>
        /// Instance 0 of <see cref="SByte"/> field.
        /// </summary>
        [FieldOffset(0)]
        public SByte SByte_0;

        /// <summary>
        /// Instance 1 of <see cref="SByte"/> field.
        /// </summary>
        [FieldOffset(1)]
        public SByte SByte_1;

        /// <summary>
        /// Instance 2 of <see cref="SByte"/> field.
        /// </summary>
        [FieldOffset(2)]
        public SByte SByte_2;

        /// <summary>
        /// Instance 3 of <see cref="SByte"/> field.
        /// </summary>
        [FieldOffset(3)]
        public SByte SByte_3;

        /// <summary>
        /// Instance 4 of <see cref="SByte"/> field.
        /// </summary>
        [FieldOffset(4)]
        public SByte SByte_4;

        /// <summary>
        /// Instance 5 of <see cref="SByte"/> field.
        /// </summary>
        [FieldOffset(5)]
        public SByte SByte_5;

        /// <summary>
        /// Instance 6 of <see cref="SByte"/> field.
        /// </summary>
        [FieldOffset(6)]
        public SByte SByte_6;

        /// <summary>
        /// Instance 7 of <see cref="SByte"/> field.
        /// </summary>
        [FieldOffset(7)]
        public SByte SByte_7;

        /// <summary>
        /// Instance 0 of <see cref="Byte"/> field.
        /// </summary>
        [FieldOffset(0)]
        public Byte Byte_0;

        /// <summary>
        /// Instance 1 of <see cref="Byte"/> field.
        /// </summary>
        [FieldOffset(1)]
        public Byte Byte_1;

        /// <summary>
        /// Instance 2 of <see cref="Byte"/> field.
        /// </summary>
        [FieldOffset(2)]
        public Byte Byte_2;

        /// <summary>
        /// Instance 3 of <see cref="Byte"/> field.
        /// </summary>
        [FieldOffset(3)]
        public Byte Byte_3;

        /// <summary>
        /// Instance 4 of <see cref="Byte"/> field.
        /// </summary>
        [FieldOffset(4)]
        public Byte Byte_4;

        /// <summary>
        /// Instance 5 of <see cref="Byte"/> field.
        /// </summary>
        [FieldOffset(5)]
        public Byte Byte_5;

        /// <summary>
        /// Instance 6 of <see cref="Byte"/> field.
        /// </summary>
        [FieldOffset(6)]
        public Byte Byte_6;

        /// <summary>
        /// Instance 7 of <see cref="Byte"/> field.
        /// </summary>
        [FieldOffset(7)]
        public Byte Byte_7;

        /// <summary>
        /// Instance 0 of <see cref="Char"/> field.
        /// </summary>
        [FieldOffset(0)]
        public Char Char_0;

        /// <summary>
        /// Instance 1 of <see cref="Char"/> field.
        /// </summary>
        [FieldOffset(2)]
        public Char Char_1;

        /// <summary>
        /// Instance 2 of <see cref="Char"/> field.
        /// </summary>
        [FieldOffset(4)]
        public Char Char_2;

        /// <summary>
        /// Instance 3 of <see cref="Char"/> field.
        /// </summary>
        [FieldOffset(6)]
        public Char Char_3;

        /// <summary>
        /// Instance 0 of <see cref="Single"/> field.
        /// </summary>
        [FieldOffset(0)]
        public Single Single_0;

        /// <summary>
        /// Instance 1 of <see cref="Single"/> field.
        /// </summary>
        [FieldOffset(4)]
        public Single Single_1;

        /// <summary>
        /// Instance 0 of <see cref="Double"/> field.
        /// </summary>
        [FieldOffset(0)]
        public Double Double_0;

    }
}
