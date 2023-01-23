using System.Collections.Generic;

namespace Nmermaid.FlowchartDiagram;

public record NodeStyle(string Css)
{
    public static NodeStyleBuilder Start()
    {
        return new();
    }

    public static implicit operator NodeStyle(string value) => new(value);

    public record NodeStyleBuilder
    {
        private readonly List<string> _styles = new();

        public NodeStyleBuilder Fill(string color) => AddStyle("fill", color);
        public NodeStyleBuilder Stroke(string color) => AddStyle("stroke", color);
        public NodeStyleBuilder StrokeWidth(string width) => AddStyle("stroke-width", width);
        public NodeStyleBuilder Color(string color) => AddStyle("color", color);
        public NodeStyleBuilder StrokeDashArray(params int[] lengths) => AddStyle("stroke-dasharray", string.Join(" ", lengths));

        public NodeStyle Build()
        {
            return new(string.Join(",", _styles));
        }

        private NodeStyleBuilder AddStyle(string key, string value)
        {
            _styles.Add($"{key}:{value}");
            return this;
        }

        public static implicit operator NodeStyle(NodeStyleBuilder value) => value.Build();
    }
}