
namespace Foundations.Parsers;

static class ParserExtensions
{
    public static Parser<U> Select<T, U>(this Parser<T> parser, Func<T, U> selector) =>
        (Cursor cursor) =>
        {
            var result = parser(cursor);

            if (result.IsError)
                return Result<U>.Bad(result.Cursor, result.Error!);

            return Result<U>.Of(result.Cursor, selector(result.Value!));
        };
        
    public static Parser<V> SelectMany<T, U, V>(this Parser<T> parser, Func<T, Parser<U>> selector, Func<T, U, V> projector) =>
        (Cursor cursor) =>
        {
            var result = parser(cursor);

            if (result.IsError)
                return Result<V>.Bad(result.Cursor, result.Error!);

            var parser2 = selector(result.Value!);
            var result2 = parser2(result.Cursor);

            if (result2.IsError)
                return Result<V>.Bad(result2.Cursor, result2.Error!);

            return Result<V>.Of(result2.Cursor, projector(result.Value!, result2.Value!));
        };
}
