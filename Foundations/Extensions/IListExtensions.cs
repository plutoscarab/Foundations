
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundations;

public static class IListExtensions
{
    public static IList<T> Permute<T>(this IList<T> list, Random rand)
    {
        var index = new int[list.Count];

        for (var i = 0; i < list.Count; i++)
        {
            var j = rand.Next(i + 1);
            index[i] = index[j];
            index[j] = i;
        }

        return index.Select(i => list[i]).ToList();
    }
}