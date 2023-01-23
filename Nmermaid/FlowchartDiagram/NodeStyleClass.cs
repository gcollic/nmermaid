namespace Nmermaid.FlowchartDiagram;

public record NodeStyleClass : IMermaidGeneratable
{
    public string Name { get; init; }
    public NodeStyle Style { get; init; }

    public NodeStyleClass(string name, NodeStyle style)
    {
        Name = new MermaidName(name).Id.Value;
        Style = style;
    }

    public string ToMermaid(int indentation) => $"{Mermaid.Indent(indentation)}classDef {Name} {Style.Css};";
}