
using System.Threading;

namespace Foundations.Algebra;

public class Indeterminate : IEquatable<Indeterminate>
{
    internal static List<string> Italics = ("𝑥𝑦𝑧" + "𝑎𝑏𝑐𝑑𝑒𝑓𝑔ℎ𝑖𝑗𝑘𝑙𝑚𝑛𝑜𝑝𝑞𝑟𝑠𝑡𝑢𝑣𝑤".Reverse()).AsTextElements();

    internal static readonly List<Indeterminate> DefaultInd = Italics.Select(c => new Indeterminate(c)).ToList();

    private static int id;

    public readonly int Id = Interlocked.Increment(ref id);

    public readonly string Symbol;

    public Indeterminate(string symbol)
    {
        Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));

        if (symbol.Length == 0)
            throw new ArgumentException(nameof(symbol), "Empty string bad.");

        if (symbol.Any(char.IsWhiteSpace))
            throw new ArgumentException(nameof(symbol), "Whitespace bad.");
    }

    public override int GetHashCode() => HashCode.Combine(Id, nameof(Indeterminate));

    public override string ToString() => Symbol;

    public override bool Equals(object obj) => Equals(obj as Indeterminate);

    public bool Equals(Indeterminate other) => other is not null && other.Id == Id && other.Symbol == Symbol;
}