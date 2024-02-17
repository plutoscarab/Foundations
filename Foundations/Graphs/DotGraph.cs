
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Foundations.Parsers;

namespace Foundations.Graphs.DOT;

public record DotGraph(bool IsDirected, CompoundStatement Statements)
{
    public static bool TryParse(
        string text,
        [NotNullWhen(true)] out DotGraph dotGraph,
        [NotNullWhen(false)] out string error,
        out int position)
    {
        var cursor = new Cursor(text);
        var parse = DotParse.Graph(cursor);

        if (parse.IsError)
        {
            error = parse.Error!;
            position = parse.Cursor.Pos;
            dotGraph = default;
            return false;
        }

        error = default;
        position = default;
        dotGraph = parse.Value!;
        return true;
    }

    public string ToSvg()
    {
        var phi = (Math.Sqrt(5) - 1) / 2;
        var isDirected = this.IsDirected;
        var rand = new Random();

        var nodes = this.Statements
            .AllNodes()
            .Distinct()
            .Select((name, index) => new Node(index, name))
            .ToList();

        var lookup = nodes.ToDictionary(node => node.Name, node => node.Id);
        var edges = new List<Edge>();
        var rankGroups = new List<List<int>>();

        foreach (var stmt in this.Statements.AllCompound())
        {
            var ranked = false;
            var group = new List<int>();

            foreach (var s in stmt.Statements)
            {
                if (s is AssignmentStatement a && a.Name == "rank")
                {
                    ranked = true;
                }

                if (s is EdgeStatement e && ranked)
                    group.AddRange(e.AllNodes().Select(n => lookup[n]));
            }

            if (group.Count > 1)
                rankGroups.Add(group);
        }

        foreach (var stmt in this.Statements.AllEdges())
        {
            for (var i = 1; i < stmt.NodeList.Count; i++)
            {
                foreach (var m1 in stmt.NodeList[i - 1])
                {
                    foreach (var m2 in stmt.NodeList[i])
                    {
                        edges.Add(new Edge(edges.Count, lookup[m1], lookup[m2]));
                    }
                }
            }
        }

        if (isDirected)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                var list = edges.Where(t => t.Node1 == i).Select(t => t.Node2).ToList();

                if (list.Count > 1)
                    rankGroups.Add(list);
            }
        }

        var graph = new Graph(isDirected, nodes, edges);
        var bestPos = new Vector[nodes.Count];
        var bestMeasure = double.MaxValue;

        var sources = nodes.Select(n => n.Id)
            .Where(n => edges.Any(e => e.Node1 == n) && !edges.Any(e => e.Node2 == n)).ToList();

        var sinks = nodes.Select(n => n.Id)
            .Where(n => !edges.Any(e => e.Node1 == n) && edges.Any(e => e.Node2 == n)).ToList();

        if (!sources.Any() || !sinks.Any())
        {
            sources.Clear();
            sinks.Clear();
        }

        for (var attempt = 0; attempt < 10; attempt++)
        {
            var permutation = new int[nodes.Count];

            for (var i = 0; i < nodes.Count; i++)
            {
                var j = rand.Next(i + 1);
                permutation[i] = permutation[j];
                permutation[j] = i;
            }

            var pos = nodes.OrderBy(n => n.Id)
                           .Select(n => new Vector(
                                2 * Math.PI * ((permutation[n.Id] * phi) % 1),
                                Math.Sqrt(permutation[n.Id] + 1)))
                           .Concat(Enumerable.Range(0, 20).Select(i => new Vector(Math.PI * i / 10, Math.Sqrt(nodes.Count * 2))))
                           .Select(p => new Vector(p.Y * Math.Cos(p.X), p.Y * Math.Sin(p.X)))
                           .ToArray();

            var vel = pos.Select(_ => Vector.Zero).ToArray();
            var steps = 0;

            while (true)
            {
                // Inverse-4th-power repulsion to keep non-connected nodes apart
                for (var i = 0; i < pos.Length - 1; i++)
                {
                    for (var j = i + 1; j < pos.Length; j++)
                    {
                        var d = pos[i] - pos[j];
                        var rr = Math.Max(d.SqrMod, .5);
                        var k = .1;
                        if (i > nodes.Count) k /= 10;
                        if (j > nodes.Count) k /= 10;
                        var f = d * k / (rr * rr * Math.Sqrt(rr));
                        vel[i] += f;
                        vel[j] -= f;
                    }
                }

                // Weak spring attraction to keep disconnected subgraphs together
                for (var i = 0; i < pos.Length - 1; i++)
                {
                    for (var j = i + 1; j < pos.Length; j++)
                    {
                        var d = pos[i] - pos[j];
                        var f = d * .000001;
                        vel[i] -= f;
                        vel[j] += f;
                    }
                }

                // Distance-proportional attraction along edges
                foreach (var edge in edges)
                {
                    var i = edge.Node1;
                    var j = edge.Node2;

                    if (i == j)
                        continue;

                    var d = pos[i] - pos[j];
                    var rr = d.SqrMod;
                    var r = Math.Sqrt(rr);
                    var f = d * .01 / r * (r - 2);
                    vel[i] -= f;
                    vel[j] += f;
                }

                // Ranking
                foreach (var group in rankGroups)
                {
                    var center = group.Select(i => pos[i]).Aggregate((a, b) => a + b) / group.Count;

                    foreach (var i in group)
                    {
                        var f = new Vector(0, pos[i].Y - center.Y) * .1;
                        vel[i] -= f;
                    }
                }

                // Directionality
                if (isDirected)
                {
                    foreach (var e in edges)
                    {
                        vel[e.Node1] -= new Vector(0, edges.Count * 0 / 200.0);
                        vel[e.Node2] += new Vector(0, edges.Count * 0 / 200.0);
                    }
                }

                // Friction and motion
                for (var i = 0; i < vel.Length; i++)
                {
                    pos[i] += vel[i];
                    vel[i] *= 0.9;
                }

                var energy = vel.Sum(v => v.SqrMod);

                if (energy < 1e-5 || ++steps > 10_000)
                    break;
            }

            var measure = edges.Sum(e => (pos[e.Node1] - pos[e.Node2]).Length);

            if (measure < bestMeasure)
            {
                bestMeasure = measure;
                bestPos = pos;
            }
        }

        // Compute Delaunay triangulation.
        var p = bestPos;
        var dgraph = Delaunay(p);

        /*
                    // Add exterior nodes to improve curvature of hull-grazing edges.
                    var ctr = p.Aggregate((a, b) => a + b) / p.Length;
                    var rmax = 1.25 * nodes.Max(n => (p[n.Id] - ctr).Length);

                    var extra = Enumerable.Range(0, 20)
                        .Select(i => 2 * Math.PI * i / 20.0)
                        .Select(t => ctr + new Vector(Math.Cos(t), Math.Sin(t)) * rmax)
                        .ToArray();

                    Array.Resize(ref p, p.Length + extra.Length);
                    Array.Copy(extra, 0, p, p.Length - extra.Length, extra.Length);
                    dgraph = Delaunay(p);
        */

        // Find path for edges that are not Delaunay.
        var edgeCrossings = dgraph.Edges.Select(_ => new List<Vector>()).ToArray();

        for (var i = 0; i < edges.Count; i++)
        {
            var e = edges[i];

            if (!dgraph.Edges.Any(d => d.Matches(e)))
            {
                e = e.WithIntersecting();
                var es = new Segment(p[e.Node1], p[e.Node2]);

                // Find where edges intersect Delaunay edges.
                foreach (var d in dgraph.Edges)
                {
                    if (e.SharesNode(d))
                        continue;

                    var ds = new Segment(p[d.Node1], p[d.Node2]);

                    if (es.Intersects(ds, out var where))
                    {
                        edgeCrossings[d.Id].Add(where);
                    }
                }
            }
        }

        for (var i = 0; i < edgeCrossings.Length; i++)
        {
            edgeCrossings[i].Sort((a, b) => (p[dgraph.Edges[i].Node1] - a).Length.CompareTo((p[dgraph.Edges[i].Node1] - b).Length));
        }

        for (var i = 0; i < edges.Count; i++)
        {
            var e = edges[i];

            if (!dgraph.Edges.Any(d => d.Matches(e)))
            {
                e = e.WithIntersecting();
                var es = new Segment(p[e.Node1], p[e.Node2]);
                var list = new List<Vector>();

                // Mark edges with points where they intersect Delaunay edges.
                foreach (var d in dgraph.Edges)
                {
                    if (e.SharesNode(d))
                        continue;

                    var ds = new Segment(p[d.Node1], p[d.Node2]);

                    if (es.Intersects(ds, out var where))
                    {
                        var t = (where - ds.A).Length / (ds.B - ds.A).Length;
                        t = (1 + t * 4) / 6;

                        if (d.Node1 > nodes.Count)
                            t = (t + 1) / 2;
                        else if (d.Node2 > nodes.Count)
                            t /= 2;

                        list.Add(ds.A * (1 - t) + ds.B * t);
                    }
                }

                edges[i] = e.WithIntersections(list.OrderBy(t => (t - p[e.Node1]).SqrMod).ToList());
            }
        }

        // Remove exterior nodes.
        Array.Resize(ref p, nodes.Count);

        // Render the graph.
        var min = new Vector(p.Min(t => t.X), p.Min(t => t.Y));
        var max = new Vector(p.Max(t => t.X), p.Max(t => t.Y));
        var size = max - min;
        min -= size * .1;
        max += size * .1;
        size *= 1.2;

        for (var i = 0; i < p.Length; i++)
            p[i] -= min;

        for (var i = 0; i < edges.Count; i++)
            edges[i] = edges[i].WithIntersections(edges[i].Intersections.Select(t => t - min).ToList());

        return WriteSvg(p, size, graph);
    }

    private static bool IsCCW(Vector a, Vector b, Vector c) =>
        (b.X - a.X) * (c.Y - a.Y) > (c.X - a.X) * (b.Y - a.Y);

    private static bool IsInCircumcircle(Vector a, Vector b, Vector c, Vector d)
    {
        var ad = a - d;
        var bd = b - d;
        var cd = c - d;

        return
            ad.SqrMod * (bd.X * cd.Y - cd.X * bd.Y) +
            bd.SqrMod * (cd.X * ad.Y - ad.X * cd.Y) +
            cd.SqrMod * (ad.X * bd.Y - bd.X * ad.Y) > 0;
    }

    private static Graph Delaunay(Vector[] p)
    {
        var nodes = p.Select((_, i) => new Node(i, i.ToString())).ToArray();
        var edges = new List<Edge>();

        for (var i = 0; i < p.Length - 2; i++)
        {
            for (var j = i + 1; j < p.Length - 1; j++)
            {
                for (var k = j + 1; k < p.Length; k++)
                {
                    var a = p[i];
                    var (b, c) = IsCCW(p[i], p[j], p[k]) ? (p[j], p[k]) : (p[k], p[j]);
                    var ok = true;

                    for (var l = 0; l < p.Length; l++)
                    {
                        if (l == i || l == j || l == k)
                            continue;

                        if (IsInCircumcircle(a, b, c, p[l]))
                        {
                            ok = false;
                            break;
                        }
                    }

                    if (ok)
                    {
                        var e = new Edge(edges.Count, i, j);

                        if (!edges.Any(t => t.Matches(e)))
                            edges.Add(e);

                        e = new Edge(edges.Count, i, k);

                        if (!edges.Any(t => t.Matches(e)))
                            edges.Add(e);

                        e = new Edge(edges.Count, j, k);

                        if (!edges.Any(t => t.Matches(e)))
                            edges.Add(e);
                    }
                }
            }
        }

        return new Graph(false, nodes, edges);
    }

    private static string WriteSvg(Vector[] pos_, Vector size, Graph graph)
    {
        const double scale = 50.0;

        var pos = pos_.Select(p => p * scale).ToArray();

        var stroke = "black";
        var strokeWidth = .02 * scale;

        using var file = new StringWriter();
        using var writer = new IndentedTextWriter(file);
        writer.WriteLine($"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 {size.X * scale:F3} {size.Y * scale:F3}'>");
        writer.Indent();
        writer.WriteLine("<marker id='arrow' viewBox='0 -2 4 4' refX='8' refY='0' markerWidth='6' markerHeight='6' orient='auto'>");
        writer.Indent();
        writer.WriteLine($"<path d='M 4 0 L 0 2 L 0 -2' fill='{stroke}' />");
        writer.Outdent();
        writer.WriteLine("</marker>");

        var marker = graph.IsDirected ? " marker-end='url(#arrow)'" : "";
        writer.WriteLine($"<g{marker} fill='none' stroke='{stroke}' stroke-width='{strokeWidth}' >");
        writer.Indent();

        foreach (var e in graph.Edges)
        {
            switch (e.Intersections.Count)
            {
                case 0:
                    writer.WriteLine($"<line x1='{pos[e.Node1].X:F3}' y1='{pos[e.Node1].Y:F3}' x2='{pos[e.Node2].X:F3}' y2='{pos[e.Node2].Y:F3}' />");
                    break;

                case 1:
                    writer.WriteLine($"<path d='M {pos[e.Node1].X:F3} {pos[e.Node1].Y:F3} Q {e.Intersections[0].X * scale:F3} {e.Intersections[0].Y * scale:F3} {pos[e.Node2].X:F3} {pos[e.Node2].Y:F3}' />");
                    break;

                case 2:
                    writer.WriteLine($"<path d='M {pos[e.Node1].X:F3} {pos[e.Node1].Y:F3} C {e.Intersections[0].X * scale:F3} {e.Intersections[0].Y * scale:F3} {e.Intersections[1].X * scale:F3} {e.Intersections[1].Y * scale:F3} {pos[e.Node2].X:F3} {pos[e.Node2].Y:F3}' />");
                    break;

                default:
                    var points = new List<Vector> { pos[e.Node1] };
                    points.AddRange(e.Intersections.Select(s => s * scale));
                    points.Add(pos[e.Node2]);

                    var cp = Enumerable.Range(1, points.Count - 2)
                        .Select(i => points[i + 1] - points[i - 1])
                        .SelectMany((d, i) =>
                        {
                            var dl = d.Length;
                            var du = d / dl;
                            var w1 = (points[i + 1] - points[i]).Length;
                            var w2 = (points[i + 1] - points[i + 2]).Length;
                            var wt = w1 + w2;
                            return new[] {
                                points[i + 1] - d * w1 / wt / 4,
                                points[i + 1] + d * w2 / wt / 4};
                        })
                        .ToList();

                    writer.Write($"<path d='");
                    writer.Write($"M {points[0].X:F3} {points[0].Y:F3} Q {cp[0].X:F3} {cp[0].Y:F3} {points[1].X:F3} {points[1].Y:F3}");

                    for (var i = 2; i < points.Count - 1; i++)
                    {
                        writer.Write($" C {cp[2 * i - 3].X:F3} {cp[2 * i - 3].Y:F3} {cp[2 * i - 2].X:F3} {cp[2 * i - 2].Y:F3} {points[i].X:F3} {points[i].Y:F3}");
                    }

                    writer.Write($" Q {cp[^1].X:F3} {cp[^1].Y:F3} {points[^1].X:F3} {points[^1].Y:F3}");
                    writer.WriteLine("' />");
                    break;
            }
        }

        writer.Outdent();
        writer.WriteLine("</g>");
        writer.WriteLine($"<g fill='white' stroke='{stroke}' stroke-width='{strokeWidth}'>");
        writer.Indent();

        foreach (var p in pos.Take(graph.Nodes.Count))
        {
            writer.WriteLine($"<circle r='{.15 * scale:F3}' cx='{p.X:F3}' cy='{p.Y:F3}'/>");
        }

        writer.Outdent();
        writer.WriteLine("</g>");
        writer.WriteLine($"<g font-size='{.1 * scale:F3}' fill='black' stroke='none' text-anchor='middle'>");
        writer.Indent();

        for (var i = 0; i < graph.Nodes.Count; i++)
        {
            writer.WriteLine($"<text x='{pos[i].X:F3}' y='{pos[i].Y:F3}' alignment-baseline='middle'>{graph.Nodes[i].Name}</text>");
        }

        writer.Outdent();
        writer.WriteLine("</g>");
        writer.Outdent();
        writer.WriteLine("</svg>");
        writer.Dispose();
        return file.ToString();
    }
}