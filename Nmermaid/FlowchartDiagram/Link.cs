namespace Nmermaid.FlowchartDiagram;

public record Link : IMermaidGeneratable
{
    public IMermaidLinkable Source { get; init; }
    public IMermaidLinkable Target { get; init; }
    private LinkOptions Options { get; init; }
    private string? Text { get; init; }
    private string? Style { get; init; }
    private bool HasNoHeads => Options.Head == LinkHead.None || Options.Direction == LinkDirection.None;
    private bool HasTwoHeads => Options.Direction == LinkDirection.Dual;
    private char? StartChar => !HasTwoHeads
        ? null
        : Options.Head switch
        {
            LinkHead.Cross => 'x',
            LinkHead.Circle => 'o',
            _ => '<',
        };
    private char? EndChar => HasNoHeads
        ? null
        : Options.Head switch
        {
            LinkHead.Cross => 'x',
            LinkHead.Circle => 'o',
            _ => '>',
        };

    public Link(IMermaidLinkable source, IMermaidLinkable target)
    {
        Source = source;
        Target = target;
        Options = LinkOptions.Default;
    }

    public Link WithText(string text) => this with { Text = text };

    public Link Styled(string style) => this with { Style = style };

    public Link WithOptions(LinkOptions options)
    {
        if (options.Head == LinkHead.None || options.Direction == LinkDirection.None)
        {
            options = options with
            {
                Head = LinkHead.None,
                Direction = LinkDirection.None
            };
        }

        return this with
        {
            Options = options
        };
    }

    public string ToMermaid(int indentation) => $"{Mermaid.Indent(indentation)}{Source.Id}{GenerateArrow()}{Target.Id}";

    public string? LinkStyleToMermaid(int indentation, int linkIndex) => Style == null ? null : $"{Mermaid.Indent(indentation)}linkStyle {linkIndex} {Style}";

    private string GenerateArrow()
    {
        string body;
        if (Options.LineType == LinkLineType.Dotted)
        {
            body = $"-{new string('.', Options.MinimumLength)}-";
        }
        else
        {
            var mainLength = Options.MinimumLength + (HasNoHeads ? 2 : 1);
            var mainChar = Options.LineType is LinkLineType.Thick ? '=' : '-';
            body = new string(mainChar, mainLength);
        }

        return " " + StartChar + body + EndChar + (Text == null ? "" : $"|{Text}|") + " ";
    }

    public static LinkBuilder From(IMermaidLinkable source)
    {
        return new(source);
    }

    public record LinkBuilder(IMermaidLinkable Source)
    {
        public Link To(IMermaidLinkable target)
        {
            return new(Source, target);
        }
    }
}

public record LinkOptions
{
    public static LinkOptions Default { get; } = new();
    public LinkDirection Direction { get; init; }
    public LinkHead Head { get; init; }
    public LinkLineType LineType { get; init; }
    public int MinimumLength { get; init; }

    private LinkOptions()
    {
        Direction = LinkDirection.Single;
        Head = LinkHead.Arrow;
        LineType = LinkLineType.Straight;
        MinimumLength = 1;
    }

    public LinkOptions WithDirection(LinkDirection direction) => this with { Direction = direction };

    public LinkOptions WithHead(LinkHead head) => this with { Head = head };

    public LinkOptions WithLineType(LinkLineType lineType) => this with { LineType = lineType };

    public LinkOptions WithMinimumLength(int minimumLength) => this with { MinimumLength = minimumLength };
}

public enum LinkDirection
{
    None,
    Single,
    Dual,
}

public enum LinkHead
{
    None,
    Arrow,
    Circle,
    Cross,
}

public enum LinkLineType
{
    Straight,
    Thick,
    Dotted,
}