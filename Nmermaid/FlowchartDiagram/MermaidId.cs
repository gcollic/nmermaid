using System.Text.RegularExpressions;

namespace Nmermaid.FlowchartDiagram;

public record MermaidId
{
    public string Value { get; }

    public MermaidId(string value)
    {
        Value = Regex.Replace(value, @"[\W_]", "");
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator MermaidId(string value) => new(value);
}