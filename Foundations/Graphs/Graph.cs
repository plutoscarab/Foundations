
namespace Foundations.Graphs.DOT;

internal record Graph(bool IsDirected, IList<Node> Nodes, IList<Edge> Edges)
{
    public Graph WithEdges(IList<Edge> edges) =>
        new(IsDirected, Nodes, edges);
}
