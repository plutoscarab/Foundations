
using System;

namespace Foundations.Algebra;

public record struct FFValue(FiniteField Field, long Value)
{
    public readonly long Checked(FiniteField field) => 
        field.Order == Field.Order ? 
            Value : 
            throw new ArgumentException(null, nameof(field));

    public override readonly string ToString() => Field.ValueStr(this);

    private static FiniteField Check(FFValue a, FFValue b)
    {
        if (a.Field.Order != b.Field.Order)
            throw new ArgumentException($"Values belong to fields of different orders: {a.Field.Order} and {b.Field.Order}.");

        return a.Field;
    }

    public static FFValue operator +(FFValue a) => a;

    public static FFValue operator -(FFValue a) => a.Field.Negate(a);

    public static FFValue operator +(FFValue a, FFValue b)
    {
        var field = Check(a, b);
        return field.Add(a, b);
    }

    public static FFValue operator -(FFValue a, FFValue b)
    {
        var field = Check(a, b);
        return field.Subtract(a, b);
    }

    public static FFValue operator *(FFValue a, FFValue b)
    {
        var field = Check(a, b);
        return field.Multiply(a, b);
    }

    public static FFValue operator /(FFValue a, FFValue b)
    {
        var field = Check(a, b);
        return field.Divide(a, b);
    }
}