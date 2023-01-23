using System.Text.RegularExpressions;
using System.Web;

namespace Nmermaid.FlowchartDiagram;

public record MermaidName
{
    public MermaidId Id { get; }
    public string Label { get; }
    public string EscapedLabel => $@"""{Regex.Replace(HttpUtility.HtmlEncode(Label), @"&(#\d+;)", "$1")}""";

    public bool LabelIsId => Id.Value == Label;

    public static MermaidName HiddenName(string unsafeId) => new(unsafeId, " ");

    public MermaidName(string unsafeId, string label)
    {
        Label = label;

        Id = new MermaidId(unsafeId);
    }

    public MermaidName(string unsafeId)
    {
        Label = unsafeId;

        Id = new MermaidId(unsafeId);
    }

    public static implicit operator MermaidName(string value) => new(value);
}