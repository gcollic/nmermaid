using System;
using System.Collections.Generic;
using System.Linq;

namespace Nmermaid.FlowchartDiagram;

public record Subgraph : IMermaidGeneratable, IMermaidLinkable
{
    private readonly MermaidName _name;
    public Flowchart Content { get; }
    public MermaidId Id => _name.Id;

    public NodeStyleClass? StyleClass { get; private init; }
    public NodeStyle? Style { get; private init; }

    public Subgraph(MermaidName name, Flowchart content)
    {
        this._name = name;
        Content = content;
    }

    public string ToMermaid(int indentation)
    {
        var indent = Mermaid.Indent(indentation);
        var label = _name.LabelIsId ? "" : $"[{_name.EscapedLabel}]";
        var lines = new List<string>();
        lines.Add(
            $@"subgraph {_name.Id}{label}
{Content.ToMermaid(indentation + 1)}");
        lines.Add("end");
        if (Style != null)
        {
            lines.Add($"style {Id} {Style.Css}");
        }
        if (StyleClass != null)
        {
            lines.Add($"class {Id} {StyleClass.Name};");
        }
        return string.Join(
            Environment.NewLine,
            lines.Select(line => indent + line));
    }

    public static SubgraphBuilder Named(MermaidName name)
    {
        return new SubgraphBuilder(name);
    }

    public Subgraph Styled(NodeStyleClass styleClass) =>
        this with
        {
            StyleClass = styleClass
        };

    public Subgraph Styled(NodeStyle style) =>
        this with
        {
            Style = style
        };

    public record SubgraphBuilder(MermaidName Name)
    {
        public Subgraph WithContent(Flowchart content)
        {
            return new Subgraph(Name, content);
        }
    }
}