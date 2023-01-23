using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Nmermaid.FlowchartDiagram;

public record Flowchart : IMermaidGeneratable
{
    private readonly Orientation? _orientation;
    private ImmutableList<Node> Nodes { get; init; } = ImmutableList<Node>.Empty;
    private ImmutableList<Link> Links { get; init; } = ImmutableList<Link>.Empty;
    private ImmutableList<Subgraph> Subgraphs { get; init; } = ImmutableList<Subgraph>.Empty;
    private NodeStyleClass? DefaultNodeClass { get; init; }

    public static Flowchart Start()
    {
        return Start(null);
    }

    public static Flowchart Start(Orientation? orientation)
    {
        return new(orientation);
    }

    private Flowchart(Orientation? orientation)
    {
        this._orientation = orientation;
    }

    public Flowchart WithNode(Node node) =>
        this with
        {
            Nodes = Nodes.Add(node)
        };

    public Flowchart WithNodes(IEnumerable<Node> toAdd) =>
        this with
        {
            Nodes = Nodes.AddRange(toAdd)
        };

    public Flowchart Append(Flowchart other) =>
        this with
        {
            Nodes = Nodes.AddRange(other.Nodes),
            Links = Links.AddRange(other.Links),
            Subgraphs = Subgraphs.AddRange(other.Subgraphs),
        };

    public string ToMermaid() => ToMermaid(1);

    private IEnumerable<NodeStyleClass?> AllClassDefinitions =>
        Subgraphs.Select(subgraph => subgraph.StyleClass)
            .Concat(Nodes
                .Select(node => node.StyleClass))
            .Concat(Subgraphs
                .SelectMany(sub => sub.Content.AllClassDefinitions));

    public string ToMermaid(int indentation)
    {
        var classDefinitions = new List<IMermaidGeneratable>();

        if (indentation == 1)
        {
            classDefinitions =
                AllClassDefinitions
                    .Prepend(DefaultNodeClass)
                    .Where(styleClass => styleClass != null)
                    .GroupBy(styleClass => styleClass!.Name)
                    .Select(group => group.First()!)
                    .ToList<IMermaidGeneratable>();
        }

        var linkStyles = Links
            .Select((link, index) => link.LinkStyleToMermaid(indentation, index))
            .Where(declaration => declaration != null)
            .ToList();

        var contents = classDefinitions
            .Concat(Nodes)
            .Concat(Subgraphs)
            .Concat(Links)
            .Select(node => node.ToMermaid(indentation))
            .Concat(linkStyles);

        if (indentation == 1)
        {
            string header = "flowchart " + (_orientation ?? Orientation.LeftToRight).ToMermaid();
            contents = contents.Prepend(header);
        }
        else if (_orientation.HasValue)
        {
            string header = $"{Mermaid.Indent(indentation)}direction {_orientation.Value.ToMermaid()}";
            contents = contents.Prepend(header);
        }

        return string.Join(Environment.NewLine, contents);
    }

    public Flowchart WithLink(Link link) =>
        this with
        {
            Links = Links.Add(link)
        };

    public Flowchart WithSubgraph(Subgraph subgraph) =>
        this with
        {
            Subgraphs = Subgraphs.Add(subgraph)
        };

    public Flowchart WithDefaultNodeStyle(NodeStyle defaultNodeStyle) =>
        this with
        {
            DefaultNodeClass = new NodeStyleClass("default", defaultNodeStyle)
        };
}