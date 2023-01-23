using System;
using System.Collections.Generic;
using System.Linq;

namespace Nmermaid.FlowchartDiagram;

public record Node : IMermaidGeneratable, IMermaidLinkable
{
    private MermaidName Name { get; init; }
    private NodeShape Shape { get; init; }
    private NodeStyle? Style { get; init; }
    public NodeStyleClass? StyleClass { get; init; }
    private string? Url { get; init; }

    public MermaidId Id => Name.Id;
    public string Label => Name.Label;

    private Node(MermaidName name)
    {
        Name = name;
        Shape = NodeShape.Default;
    }

    public static Node Named(MermaidName name) => new(name);
    public static Node Named(string name) => new(name);

    public Node Labeled(string label) => this with
    {
        Name = new MermaidName(Name.Id.Value, label)
    };

    public Node Shaped(NodeShape shape) => this with
    {
        Shape = shape
    };

    public Node Styled(NodeStyle style) => this with
    {
        Style = style
    };

    public Node Styled(NodeStyleClass styleClass) => this with
    {
        StyleClass = styleClass
    };

    public Node ClickableToUrl(string url) => this with
    {
        Url = url,
    };

    public string ToMermaid(int indentation)
    {
        var lines = new List<string>();

        var hasImplicitLabel = Shape == NodeShape.Default && Name.LabelIsId;
        var label = hasImplicitLabel ? "" : Shape.FormatLabel(Name.EscapedLabel);
        var declaration = Id + label;
        lines.Add(declaration);

        if (Url != null)
        {
            lines.Add(@$"click {Id} href ""{Url}""");
        }

        if (StyleClass != null)
        {
            lines.Add($"class {Id} {StyleClass.Name};");
        }

        if (Style != null)
        {
            lines.Add($"style {Id} {Style.Css}");
        }

        var indent = Mermaid.Indent(indentation);
        return string.Join(
            Environment.NewLine,
            lines.Select(line => indent + line));
    }
}