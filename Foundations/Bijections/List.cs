
namespace Foundations.Types;

public partial struct Nat
{
    const int DefaultBias = 10;

    public Nat(List<Nat> list, int bias = DefaultBias)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(bias, 2, nameof(bias));
        ArgumentOutOfRangeException.ThrowIfEqual(bias, int.MaxValue, nameof(bias));

        if (list.Count == 0)
        {
            b = 0;
            return;
        }

        List<int> digits = [];

        for (var i = 0; i < list.Count; i++)
        {
            if (i > 0) digits.Add(bias);
            var word = list[i].ToWord(bias);
            digits.AddRange(word);
        }

        var n = 1 + FromWord(digits, bias + 1);
        b = n.b;
    }

    public readonly List<Nat> ToList(int bias = DefaultBias)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(bias, 2, nameof(bias));
        ArgumentOutOfRangeException.ThrowIfEqual(bias, int.MaxValue, nameof(bias));

        if (IsZero) 
            return [];

        var digits = (this - 1).ToWord(bias + 1);
        List<int> word = [];
        List<Nat> result = [];

        for (var i = 0; i < digits.Count; i++)
        {
            if (digits[i] == bias)
            {
                result.Add(FromWord(word, bias));
                word.Clear();
            }
            else
            {
                word.Add(digits[i]);
            }
        }

        result.Add(FromWord(word, bias));
        return result;
    }

    public readonly List<T> ToList<T>(Func<Nat, T> selector, int bias = DefaultBias) =>
        ToList(bias).Select(selector).ToList();

    public static IEnumerable<List<Nat>> AllLists(int bias = DefaultBias) =>
        All().Select(n => n.ToList(bias));
}