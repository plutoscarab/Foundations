
using Foundations.Parsers;

namespace Foundations.Graphs.DOT;

public abstract record Statement()
{
    public virtual IEnumerable<CompoundStatement> AllCompound()
    {
        yield break;
    }

    public virtual IEnumerable<EdgeStatement> AllEdges()
    {
        yield break;
    }

    public IEnumerable<string> AllNodes() =>
        from e in AllEdges()
        from list in e.NodeList
        from n in list
        select n;
}

public record EdgeStatement(List<List<string>> NodeList) : Statement
{
    public override IEnumerable<EdgeStatement> AllEdges()
    {
        yield return this;
    }
}

public record AttributeStatement() : Statement;

public record AssignmentStatement(string Name, string Value) : Statement;

public record CommentStatement(string Comment) : Statement;

public record CompoundStatement(List<Statement> Statements) : Statement
{
    public override IEnumerable<CompoundStatement> AllCompound()
    {
        yield return this;

        foreach (var c in Statements.SelectMany(s => s.AllCompound()))
            yield return c;
    }

    public override IEnumerable<EdgeStatement> AllEdges() =>
        Statements.SelectMany(s => s.AllEdges());
}

internal class DotParse : Parse
{
    public static Parser<string> Symbol(string symbol) =>
        from _ in Whitespace
        from s in Literal(symbol)
        select s;

    public static Parser<string> Whitespace =>
        (Cursor cursor) =>
        {
            var next = cursor.SkipWhitespace();
            return Return(next, cursor.Text[cursor.Pos..next.Pos]);
        };

    public static Parser<string> Keyword(string keyword) =>
        from _ in Whitespace
        from k in Literal(keyword)
        from w in Not(LetterOrDigit, "Letter or digit not allowed here.")
        select k;

    public static Parser<string> Identifier =>
        from _ in Whitespace
        from first in Letter
        from rest in ZeroOrMore(Pred(c => char.IsLetterOrDigit(c) || c == '_', "Id continuation character."))
        select first.ToString() + new string(rest.ToArray());

    public static Parser<string> Number =>
        from _ in Whitespace
        from d in OneOrMore(Digit, "digit")
        from rest in Optional(And(Literal("."), OneOrMore(Digit, "digit")))
        select new string(d.Concat(rest.Item2 ?? new List<char>()).ToArray());

    public static Parser<string> EscapedChar =>
        from a in And(Literal("\\"), Or("Expected '\\' or '\"'.", Literal("\\"), Literal("\"")))
        select a.Item1 + a.Item2;

    public static Parser<string> NotEscaped =>
        from c in Pred(ch => ch != '"', "Quote '\"' not allowed here.")
        select c.ToString();

    public static Parser<string> StrChar =>
        Or("Expected string character.", EscapedChar, NotEscaped);

    public static Parser<string> StringId =>
        from _1 in Whitespace
        from _2 in Literal("\"")
        from value in ZeroOrMore(StrChar)
        from _3 in Literal("\"")
        select string.Join(string.Empty, value);

    public static Parser<string> Id =>
        Or("Expected either an identifier or a number.", Identifier, Number, StringId);

    public static Parser<List<string>> IdAsSet =>
        from id in Or("Expected either an identifier or a number.", Identifier, Number, StringId)
        select new List<string> { id };

    public static Parser<List<string>> BracedSet =>
        from _1 in Symbol("{")
        from ids in Sequence(Optional(Or("", Literal(","), Literal(";"))), Id)
        from _2 in Symbol("}")
        select ids;

    public static Parser<List<string>> IdSet =>
        Or("Expected a node ID or curly-brace-delimited set of node IDs.", IdAsSet, BracedSet);

    public static Parser<(string, string)> Assgn =>
        from left in Id
        from _ in Symbol("=")
        from right in Id
        select (left, right);

    public static Parser<Statement> AssgnStmt =>
        from a in Assgn
        select (Statement)new AssignmentStatement(a.Item1, a.Item2);

    public static Parser<string> Attr =>
        from _0 in Whitespace
        from _1 in Symbol("[")
        from items in Sequence(Or("Expected ';' or ','.", Symbol(";"), Symbol(",")), Assgn)
        from _3 in Whitespace
        from _2 in Symbol("]")
        select string.Empty;

    public static Parser<Statement> AttrStmt =>
        from _1 in Or("Expected 'graph', 'edge', or 'node'.", Keyword("graph"), Keyword("edge"), Keyword("node"))
        from _2 in OneOrMore(Attr, "attribute")
        select (Statement)new AttributeStatement();

    public static Parser<Statement> EdgeStmt =>
        from items in Sequence(Or("Expected '--' or '->'.", Symbol("--"), Symbol("->")), IdSet)
        from _ in ZeroOrMore(Attr)
        select (Statement)new EdgeStatement(items);

    public static Parser<Statement> SlashedCommentStmt =>
        from _ in Symbol("//")
        from comment in ZeroOrMore(Pred(ch => ch != '\r' && ch != '\n', ""))
        select (Statement)new CommentStatement(new string(comment.ToArray()));

    public static Parser<Statement> DelimitedCommentStmt =>
        from _1 in Symbol("/*")
        from comment in Until(Literal("*/"))
        select (Statement)new CommentStatement(comment);

    public static Parser<Statement> CommentStmt =>
        Or("Expected '//' or '/*'.", SlashedCommentStmt, DelimitedCommentStmt);

    public static Parser<Statement> NestedStmt =>
        from items in Statements
        select (Statement)items;

    public static Parser<Statement> Statement =>
        Or("Expected attribute, edge, or assignment.", AssgnStmt, AttrStmt, EdgeStmt, CommentStmt,
            Delay(() => NestedStmt));

    public static Parser<CompoundStatement> Statements =>
        from _1 in Symbol("{")
        from items in Sequence(Optional(Symbol(";")), Statement)
        from _2 in Optional(Symbol(";"))
        from _3 in Symbol("}")
        select new CompoundStatement(items);

    public static Parser<DotGraph> Graph =>
        from type in Or("Expected 'graph' or 'digraph'.", Keyword("graph"), Keyword("digraph"))
        from id in Optional(Id)
        from items in Statements
        from _2 in Whitespace
        from _3 in End
        select new DotGraph(type == "digraph", items);
}
