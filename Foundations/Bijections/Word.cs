
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundations.Bijections;

public partial struct Nat
{
    public static Nat FromWord(IEnumerable<int> str, int alphabetSize)
    {
        Nat n = 0;

        foreach (var i in str)
        {
            var a = i + 1;

            if (a == alphabetSize)
            {
                a = 0;
                n++;
            }

            n = n * alphabetSize + a;
        }

        return n;
    }

    public static Nat FromWord<T>(IEnumerable<T> str, T[] alphabet) where T : struct
    {
        var alphabetSize = alphabet.Length;
        var lkp = alphabet.Select((s, i) => (s, i)).ToDictionary(k => k.s, k => k.i);
        return FromWord(str.Select(i => lkp[i]), alphabetSize);
    }

    public readonly List<int> ToWord(int alphabetSize)
    {
        if (alphabetSize < 2)
            throw new ArgumentOutOfRangeException(nameof(alphabetSize));

        List<int> list = [];
        var n = this;

        while (n > 0)
        {
            var q = (n + (alphabetSize - 1)) / alphabetSize - 1;
            var a = n - q * alphabetSize;
            list.Add((int)a - 1);
            n = q;
        }

        list.Reverse();
        return list;
    }

    public readonly List<T> ToWord<T>(T[] alphabet)
    {
        var alphabetSize = alphabet.Length;
        return ToWord(alphabetSize).Select(i => alphabet[i]).ToList();
    }

    public static IEnumerable<List<int>> AllWords(int alphabetSize) =>
        All().Select(n => n.ToWord(alphabetSize));

    public static IEnumerable<List<T>> AllWords<T>(T[] alphabet) =>
        All().Select(n => n.ToWord(alphabet));
}