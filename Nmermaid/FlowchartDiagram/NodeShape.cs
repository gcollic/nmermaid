namespace Nmermaid.FlowchartDiagram;

public record NodeShape
{
    public static NodeShape Default => Square;
    public static NodeShape Square { get; } = new("[", "]");
    public static NodeShape RoundEdges { get; } = new("(", ")");
    public static NodeShape Stadium { get; } = new("([", "])");
    public static NodeShape Subroutine { get; } = new("[[", "]]");
    public static NodeShape Cylindrical { get; } = new("[(", ")]");
    public static NodeShape Circle { get; } = new("((", "))");
    public static NodeShape Asymmetric { get; } = new(">", "]");
    public static NodeShape Rhombus { get; } = new("{", "}");
    public static NodeShape Hexagon { get; } = new("{{", "}}");
    public static NodeShape Parallelogram { get; } = new("[/", "/]");
    public static NodeShape ParallelogramAlt { get; } = new("[\\", "\\]");
    public static NodeShape Trapezoid { get; } = new("[/", "\\]");
    public static NodeShape TrapezoidAlt { get; } = new("[\\", "/]");

    private readonly string _startLabel;
    private readonly string _endLabel;
    private NodeShape(string startLabel, string endLabel)
    {
        this._startLabel = startLabel;
        this._endLabel = endLabel;
    }

    public string FormatLabel(string nameSafeLabel)
    {
        return $"{_startLabel}{nameSafeLabel}{_endLabel}";
    }
}