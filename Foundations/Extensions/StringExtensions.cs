
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Foundations;

public static class StringExtensions
{
    public static List<string> AsTextElements(this string s)
    {
        List<string> list = [];
        var e = StringInfo.GetTextElementEnumerator(s);

        while (e.MoveNext())
        {
            list.Add(e.GetTextElement());
        }

        return list;
    }

    public static string Reverse(this string s)
    {
        var b = new StringBuilder();
        var e = StringInfo.GetTextElementEnumerator(s);

        while (e.MoveNext())
        {
            b.Insert(0, e.Current);
        }

        return b.ToString();
    }
}