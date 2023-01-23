namespace Nmermaid.FlowchartDiagram;

public interface IMermaidGeneratable
{
    string ToMermaid(int indentation = 1);
}