

namespace Foundations.Parsers;

internal delegate Result<T> Parser<T>(Cursor cursor);

internal class Parse
{
    public static Result<T> Fail<T>(Cursor cursor, string error) => 
        Result<T>.Bad(cursor, error);

    public static Result<T> Return<T>(Cursor cursor, T value) =>
        Result<T>.Of(cursor, value);

    public static Parser<bool> Not<T>(Parser<T> parser, string error) =>
        (Cursor cursor) =>
        {
            var result = parser(cursor);

            if (result.IsError)
                return Return(result.Cursor, true);

            return Fail<bool>(cursor, error);
        };

    public static Parser<char> Pred(Predicate<char> predicate, string error) =>
        (Cursor cursor) =>
            predicate(cursor.Char) ?
                Return(cursor + 1, cursor.Char) :
                Fail<char>(cursor, error);

    public static Parser<char> Letter =>
        Pred(char.IsLetter, "Expected Letter.");

    public static Parser<char> Digit =>
        Pred(char.IsDigit, "Expected Digit.");

    public static Parser<char> LetterOrDigit =>
        Pred(char.IsLetterOrDigit, "Expected Letter or Digit.");

    public static Parser<bool> End =>
        (Cursor cursor) =>
            cursor.IsEnd ? Return(cursor, true) : Fail<bool>(cursor, "Expected end-of-text.");

    public static Parser<string> Literal(string expected) =>
        (Cursor cursor) =>
            cursor.StartsWith(expected) ?
                Return(cursor + expected, expected) :
                Fail<string>(cursor, $"Expected literal '{expected}'.");


    public static Parser<T> Or<T>(string error, params Parser<T>[] ps) =>
        (Cursor cursor) =>
        {
            foreach (var p in ps)
            {
                var result = p(cursor);
                if (!result.IsError) return result;
            }

            return Fail<T>(cursor, error);
        };

    public static Parser<(T, U)> And<T, U>(Parser<T> p, Parser<U> q) =>
        (Cursor cursor) =>
        {
            var result = p(cursor);

            if (result.IsError)
                return Fail<(T, U)>(cursor, result.Error!);

            var result2 = q(result.Cursor);

            if (result2.IsError)
                return Fail<(T, U)>(result.Cursor, result2.Error!);

            return Return(result2.Cursor, (result.Value!, result2.Value!));
        };

    public static Parser<List<T>> ZeroOrMore<T>(Parser<T> parser) =>
        (Cursor cursor) =>
        {
            var list = new List<T>();

            while (true)
            {
                var result = parser(cursor);

                if (result.IsError)
                    return Return(cursor, list);

                list.Add(result.Value!);
                cursor = result.Cursor;
            }
        };

    public static Parser<string> Until<T>(Parser<T> parser) =>
        (Cursor cursor) =>
        {
            var i = cursor.Pos;

            while (true)
            {
                var result = parser(cursor);

                if (!result.IsError)
                    return Return(result.Cursor, cursor.Text[i..cursor.Pos]);

                cursor += 1;
            }
        };

    public static Parser<List<T>> OneOrMore<T>(Parser<T> parser, string itemName) =>
        (Cursor cursor) =>
        {
            var result = ZeroOrMore(parser)(cursor);

            if (result.IsError)
                return result;

            if (result.Value!.Count != 0)
                return result;

            return Fail<List<T>>(cursor, $"Expected at least one {itemName}.");
        };

    public static Parser<List<T>> Sequence<D, T>(Parser<D> delimiter, Parser<T> parser) =>
        from t in And(parser, ZeroOrMore(And(delimiter, parser)))
        select new List<T> { t.Item1 }.Concat(t.Item2.Select(p => p.Item2)).ToList();

    public static Parser<T> Optional<T>(Parser<T> parser) =>
        (Cursor cursor) =>
        {
            var result = parser(cursor);

            if (result.IsError)
                return Return(result.Cursor, default(T));

            return Return(result.Cursor, result.Value);
        };

    public static Parser<T> Delay<T>(Func<Parser<T>> factory) =>
        (Cursor cursor) => factory()(cursor);
}
