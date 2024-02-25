
namespace Foundations;

public static partial class BigIntegerExtensions
{
    public static string ToString(this BigInteger n, int radix)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(radix, 2, nameof(radix));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(radix, 36, nameof(radix));
        
        if (n < 0)
            return "-" + ToStringInternal(-n, radix);

        return ToStringInternal(n, radix);
    }

    private static string ToStringInternal(BigInteger n, int radix) =>
        string.Join(string.Empty, n.GetDigits(radix).Select(d => Constants.Base36Digits[d]));
}