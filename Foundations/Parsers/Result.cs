
namespace Foundations.Parsers;

public record Result<T>(Cursor Cursor, T Value, string Error)
{
    public static Result<T> Of(Cursor Cursor, T Value) => new(Cursor, Value, default);

    public static Result<T> Bad(Cursor Cursor, string Error) => new(Cursor, default, Error);

    public bool IsError =>
        Error is not null;

    public string Preview =>
        Cursor.Preview;
}
