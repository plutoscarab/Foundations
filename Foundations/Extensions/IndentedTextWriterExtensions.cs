
using System.CodeDom.Compiler;

namespace Foundations;

public static class IndentedTextWriterExtensions
{
    public static void Indent(this IndentedTextWriter writer)
    {
        writer.Indent++;
    }

    public static void Outdent(this IndentedTextWriter writer)
    {
        writer.Indent--;
    }
}
