
using System.IO;

namespace Foundations.Parsers;

public record Cursor(int Pos, string Text)
{
    public Cursor(string Text) : this(0, Text)
    { }

    public string Preview =>
        Text[Pos..];

    public bool StartsWith(string what) =>
        Pos <= Text.Length - what.Length && Text.Substring(Pos, what.Length) == what;

    public static Cursor operator +(Cursor cursor, int count) =>
        new(Math.Min(cursor.Text.Length, cursor.Pos + count), cursor.Text);

    public static Cursor operator +(Cursor cursor, string text) =>
        cursor + text.Length;

    public bool IsEnd =>
        Pos == Text.Length;

    public bool IsLetterOrDigit =>
        Pos < Text.Length && char.IsLetterOrDigit(Text[Pos]);

    public char Char =>
        Text[Pos < Text.Length ? Pos : throw new EndOfStreamException()];

    public Cursor SkipWhitespace()
    {
        var p = Pos;

        while (p < Text.Length && char.IsWhiteSpace(Text[p]))
            ++p;

        return new(p, Text);
    }
}