
namespace Foundations;

public static class IntExtensions
{
    public static string ToSubscript(this int n) => ((BigInteger)n).ToSubscript();

    public static string ToSuperscript(this int n) => ((BigInteger)n).ToSuperscript();

    public static string ToSubscript(this long n) => ((BigInteger)n).ToSubscript();

    public static string ToSuperscript(this long n) => ((BigInteger)n).ToSubscript();

    public static string ToSubscript(this BigInteger n) => new(n.ToString().Select(c => "₀₁₂₃₄₅₆₇₈₉"[c - '0']).ToArray());

    public static string ToSuperscript(this BigInteger n) => new(n.ToString().Select(c => "⁰¹²³⁴⁵⁶⁷⁸⁹"[c - '0']).ToArray());

    public static int Pow(this int n, int e)
    {
        if (e == 0) return 1;
        if (e == 1) return n;
        if ((e & 1) == 0) { var h = Pow(n, e / 2); return h * h; }
        return n * Pow(n, e - 1);
    }
}