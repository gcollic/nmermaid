namespace Nmermaid.FlowchartDiagram;

public enum Orientation
{
    TopToBottom,
    BottomToTop,
    RightToLeft,
    LeftToRight,
}

public static class OrientationExtensions
{
    public static string ToMermaid(this Orientation orientation)
    {
        return orientation switch
        {
            Orientation.TopToBottom => "TB",
            Orientation.BottomToTop => "BT",
            Orientation.RightToLeft => "RL",
            _ => "LR",
        };
    }
}