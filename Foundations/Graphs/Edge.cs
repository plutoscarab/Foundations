
namespace Foundations.Graphs.DOT;

internal record Edge(int Id, int Node1, int Node2)
{
    public bool Intersecting { get; private set; } = false;

    public IReadOnlyList<Vector> Intersections { get; private set; } = [];

    public bool Matches(Edge other) =>
        (Node1 == other.Node1 && Node2 == other.Node2) || (Node1 == other.Node2 && Node2 == other.Node1);

    public bool SharesNode(Edge other) =>
        Node1 == other.Node1 || Node1 == other.Node2 || Node2 == other.Node1 || Node2 == other.Node2;

    public Edge WithIntersecting() =>
        new(Id, Node1, Node2) { Intersecting = true, Intersections = Intersections };

    public Edge WithIntersections(IList<Vector> list) =>
        new(Id, Node1, Node2) { Intersecting = Intersecting, Intersections = (IReadOnlyList<Vector>)list };
}