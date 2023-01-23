using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nmermaid.FlowchartDiagram;
using NUnit.Framework;

namespace Nmermaid.DocAsTests;

public class FlowchartTests : DocAsTest
{
    protected Task VerifyFlowchart(Expression<Func<Flowchart>> chartCode)
    {
        var codeDocumentation = @$"```csharp
{ExpressionToCodeSample.Convert(chartCode.Body)}
```";

        string resultDocumentation;
        try
        {
            resultDocumentation = @$"```mermaid
{chartCode.Compile()().ToMermaid()}
```";
        }
        catch (Exception e)
        {
            resultDocumentation = @$"```
{e.Message}

```";
        }

        return VerifyTestInMarkdown(@$"
{codeDocumentation}

{resultDocumentation}");
    }

    public class _0_BasicSyntax : FlowchartTests
    {
        [Test]
        public Task AnEmptyFlowChartIsNotAValidChart()
        {
            return VerifyFlowchart(() => Flowchart.Start());
        }

        [Test]
        public Task ANode()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithNode(Node.Named("id"))
            );
        }

        [Test]
        public Task MultipleNodes()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithNode(Node.Named("A"))
                .WithNode(Node.Named("B"))
            );
        }

        [Test]
        public Task MultipleNodesWithUniqueDeclaration()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithNodes(new[]
                {
                    Node.Named("A"), Node.Named("B"),
                })
            );
        }

        [Test]
        public Task ANodeWithText()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithNode(Node.Named(
                        new MermaidName(
                            "id1",
                            "This is the text in the box")
                    )
                )
            );
        }

        [Test]
        public Task ANodeWithModifiedText()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithNode(Node
                    .Named(new MermaidName("id1"))
                    .Labeled("This is the text in the box"))
            );
        }

        [Test]
        public Task ALink()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithNode(Node.Named("A"))
                .WithNode(Node.Named("B"))
                .WithLink(Link
                    .From(Node.Named("A"))
                    .To(Node.Named("B"))
                )
            );
        }

        [Test]
        public Task ALinkWithUndeclaredNodes()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithLink(Link
                    .From(Node.Named("A"))
                    .To(Node.Named("B")))
            );
        }
    }

    public class _1_Orientation : FlowchartTests
    {
        [TestCase(Orientation.TopToBottom)]
        [TestCase(Orientation.BottomToTop)]
        [TestCase(Orientation.LeftToRight)]
        [TestCase(Orientation.RightToLeft)]
        public Task OrientedFlowchart(Orientation orientation)
        {
            return VerifyFlowchart(() => Flowchart.Start(orientation)
                .WithLink(Link.From(Node.Named("Start")).To(Node.Named("Stop"))));
        }
    }

    public class _2_NodeShapes : FlowchartTests
    {
        [Test]
        public Task ANodeWithRoundEdges()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.RoundEdges))
            );
        }

        [Test]
        public Task AStadiumShapedNode()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.Stadium))
            );
        }

        [Test]
        public Task ANodeInASubroutineShape()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.Subroutine)));
        }

        [Test]
        public Task ANodeInACylindricalShape()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.Cylindrical))
            );
        }

        [Test]
        public Task ANodeInTheFormOfACircle()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.Circle)));
        }

        [Test]
        public Task ANodeInAnAsymmetricShape()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.Asymmetric)));
        }

        [Test]
        public Task ARhombusNode()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.Rhombus)));
        }

        [Test]
        public Task AHexagonNode()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.Hexagon)));
        }

        [Test]
        public Task AParallelogramNode()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.Parallelogram)));
        }

        [Test]
        public Task AnAlternativeParallelogramNode()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.ParallelogramAlt)));
        }

        [Test]
        public Task ATrapezoidNode()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.Trapezoid)));
        }

        [Test]
        public Task AnAlternativeTrapezoidNode()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id").Shaped(NodeShape.TrapezoidAlt)));
        }
    }

    public class _3_LinksTypes : FlowchartTests
    {
        [Test]
        public Task ComplexLinks()
        {
            Expression<Func<LinkHead, LinkDirection, LinkLineType, Flowchart>> sampleCode =
                (head, direction, lineType) => Flowchart
                    .Start()
                    .WithLink(Link
                        .From(Node.Named("A"))
                        .To(Node.Named("B1"))
                        .WithOptions(LinkOptions.Default
                            .WithHead(head)
                            .WithDirection(direction)
                            .WithLineType(lineType)
                            .WithMinimumLength(1))
                    )
                    .WithLink(Link
                        .From(Node.Named("A"))
                        .To(Node.Named("B2"))
                        .WithOptions(LinkOptions.Default
                            .WithHead(head)
                            .WithDirection(direction)
                            .WithLineType(lineType)
                            .WithMinimumLength(2))
                    )
                    .WithLink(Link
                        .From(Node.Named("A"))
                        .To(Node.Named("B3"))
                        .WithOptions(LinkOptions.Default
                            .WithHead(head)
                            .WithDirection(direction)
                            .WithLineType(lineType)
                            .WithMinimumLength(3))
                    );

            var values = new[]
            {
                (LinkHead.None  , LinkDirection.Dual  , LinkLineType.Straight ),
                (LinkHead.Circle, LinkDirection.None  , LinkLineType.Straight ),
                (LinkHead.Arrow , LinkDirection.Single, LinkLineType.Straight ),
                (LinkHead.Circle, LinkDirection.Single, LinkLineType.Straight ),
                (LinkHead.Cross , LinkDirection.Single, LinkLineType.Straight ),
                (LinkHead.Arrow , LinkDirection.Dual  , LinkLineType.Straight ),
                (LinkHead.Circle, LinkDirection.Dual  , LinkLineType.Straight ),
                (LinkHead.Cross , LinkDirection.Dual  , LinkLineType.Straight ),
                (LinkHead.None  , LinkDirection.None  , LinkLineType.Thick    ),
                (LinkHead.Arrow , LinkDirection.Single, LinkLineType.Thick    ),
                (LinkHead.None  , LinkDirection.None  , LinkLineType.Dotted   ),
                (LinkHead.Arrow , LinkDirection.Single, LinkLineType.Dotted   ),
            };

            var generator = sampleCode.Compile();

            var markdown = @$"
```csharp
{ExpressionToCodeSample.Convert(sampleCode.Body)}
```

<table>
<tr> <th>Head</th> <th>Direction</th> <th>LineType</th> <th>Result</th> </tr>

{string.Concat(values.Select(row =>
    @$"
<tr>
<td> {row.Item1} </td> <td> {row.Item2} </td> <td> {row.Item3} </td>
<td>

```mermaid
{generator(row.Item1, row.Item2, row.Item3).ToMermaid()}
```

</td>
</tr>"))}


</table>
";

            return VerifyTestInMarkdown(markdown);
        }

        [Test]
        public Task ATextOnOpenLink()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithLink(
                    Link
                        .From(Node.Named("A"))
                        .To(Node.Named("B"))
                        .WithText("This is the text")
                        .WithOptions(LinkOptions.Default.WithDirection(LinkDirection.None))
                )
            );
        }

        [Test]
        public Task ATextOnArrowLink()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithLink(
                    Link
                        .From(Node.Named("A"))
                        .To(Node.Named("B"))
                        .WithText("This is the text")
                ));
        }
    }

    public class _4_Interaction : FlowchartTests
    {
        [Test]
        public Task AClickableNode()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("B").ClickableToUrl("http://www.github.com")));
        }
    }

    public class _5_SpecialCharactersThatBreakSyntax : FlowchartTests
    {
        [TestCase("mon id")]
        [TestCase(@"mon""id")]
        [TestCase("Banana\U0001F34C")]
        public Task ANodeWithInvalidMermaidCharInId(string input)
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named(input)));
        }

        [Test]
        public Task ANodeWithLabelIncludingQuote()
        {
            return VerifyFlowchart(() => Flowchart.Start().WithNode(Node.Named(new MermaidName("id1", @"A double quote:"""))));
        }
    }

    public class _6_Subgraphs : FlowchartTests
    {
        [Test]
        public Task ASubgraph()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("A1"))
                .WithNode(Node.Named("B1"))
                .WithLink(Link.From(Node.Named("A1")).To(Node.Named("B1")))
                .WithSubgraph(new Subgraph(
                    "subId",
                    Flowchart.Start()
                        .WithNode(Node.Named("A2"))
                        .WithNode(Node.Named("B2"))
                        .WithLink(Link.From(Node.Named("A2")).To(Node.Named("B2"))))));
        }

        [Test]
        public Task ANamedSubgraph()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("A1"))
                .WithNode(Node.Named("B1"))
                .WithLink(Link.From(Node.Named("A1")).To(Node.Named("B1")))
                .WithSubgraph(new Subgraph(
                    new MermaidName("subId", "My Sub Name"),
                    Flowchart.Start()
                        .WithNode(Node.Named("A2"))
                        .WithNode(Node.Named("B2"))
                        .WithLink(Link.From(Node.Named("A2")).To(Node.Named("B2")))))
            );
        }

        [Test]
        public Task AnOrientedSubgraph()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithSubgraph(new Subgraph(
                    "subId",
                    Flowchart.Start(Orientation.TopToBottom)
                        .WithNode(Node.Named("A2"))
                        .WithNode(Node.Named("B2"))
                        .WithLink(Link.From(Node.Named("A2")).To(Node.Named("B2"))))));
        }

        [Test]
        public Task ALinkedSubgraph()
        {
            Subgraph subgraphWithANodeNameA2 = new Subgraph(
                "My sub graph",
                Flowchart.Start().WithNode(Node.Named("A2")));

            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("A1"))
                .WithSubgraph(subgraphWithANodeNameA2)
                .WithLink(Link.From(Node.Named("A1")).To(subgraphWithANodeNameA2)));
        }
    }

    public class _7_Styling : FlowchartTests
    {
        [Test]
        public Task StylingANodeByRawCss()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id1").Styled("fill:#f9f,stroke:#333,stroke-width:4px")));
        }

        [Test]
        public Task StylingANodeByCssBuilder()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node
                    .Named("id2")
                    .Styled(
                        NodeStyle.Start()
                            .Fill("#bbf")
                            .Stroke("#f66")
                            .StrokeWidth("2px")
                            .Color("#fff")
                            .StrokeDashArray(5, 5)
                    )
                ));
        }

        [Test]
        public Task StylingANodeByClass()
        {
            NodeStyle.NodeStyleBuilder nodeStyle1 = NodeStyle.Start()
                .Fill("#bbf")
                .Stroke("#f66")
                .StrokeWidth("2px")
                .Color("#fff")
                .StrokeDashArray(5, 5);

            var styleClass2 = new NodeStyleClass(
                "my second Class Name",
                NodeStyle.Start()
                    .Fill("#ffb")
                    .Stroke("#66f")
                    .StrokeWidth("4px")
                    .Color("#330")
                    .StrokeDashArray(1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1));

            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id1").Styled(new NodeStyleClass(
                    "my_Class_Name",
                    nodeStyle1)))
                .WithNode(Node.Named("id2").Styled(new NodeStyleClass(
                    "my_Class_Name",
                    nodeStyle1)))
                .WithNode(Node.Named("id3").Styled(styleClass2))
                .WithNode(Node.Named("id4").Styled(styleClass2)));
        }

        [Test]
        public Task StylingANodeByClassInASubGraph()
        {
            NodeStyleClass outerClassRed = new NodeStyleClass(
                nameof(outerClassRed),
                NodeStyle.Start().Fill("#f00"));

            NodeStyleClass sharedClassGreen = new NodeStyleClass(
                nameof(sharedClassGreen),
                NodeStyle.Start().Fill("#0f0"));

            NodeStyleClass innerClassBlue = new NodeStyleClass(
                nameof(innerClassBlue),
                NodeStyle.Start().Fill("#00f"));

            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(Node.Named("id1").Styled(outerClassRed))
                .WithNode(Node.Named("id2").Styled(sharedClassGreen))
                .WithSubgraph(new Subgraph("sub", Flowchart.Start()
                    .WithNode(Node.Named("id3").Styled(sharedClassGreen))
                    .WithNode(Node.Named("id4").Styled(innerClassBlue))
                )));
        }

        [Test]
        public Task StylingNodesByDefault()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithDefaultNodeStyle(NodeStyle.Start().Fill("#bbf"))
                .WithNode(Node.Named("id1")));
        }

        [Test]
        public Task StylingNodesByDefaultExistsOnlyOnRootChart()
        {
            Node nodeNamedA1 = Node.Named("A1");
            Node nodeNamedA2 = Node.Named("A2");

            var subgraphWithA2NodeAndDefaultNodeStyle = new Subgraph(
                "subId",
                Flowchart.Start()
                    .WithDefaultNodeStyle(NodeStyle.Start().Fill("#bbf"))
                    .WithNode(nodeNamedA2));

            return VerifyFlowchart(() => Flowchart.Start()
                .WithNode(nodeNamedA1)
                .WithSubgraph(subgraphWithA2NodeAndDefaultNodeStyle)
                .WithLink(Link.From(nodeNamedA1).To(subgraphWithA2NodeAndDefaultNodeStyle)));
        }

        [Test]
        public Task StylingLink()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithLink(
                    Link.From(Node.Named("A"))
                        .To(Node.Named("B"))
                )
                .WithLink(
                    Link.From(Node.Named("C"))
                        .To(Node.Named("D"))
                        .Styled("stroke:#ff3,stroke-width:4px,color:red;")
                )
                .WithLink(
                    Link.From(Node.Named("E"))
                        .To(Node.Named("F"))
                ));
        }

        [Test]
        public Task StylingSubgraphByClass()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithSubgraph(
                    Subgraph
                        .Named("subId")
                        .WithContent(Flowchart.Start())
                        .Styled(new NodeStyleClass("MyClass", NodeStyle.Start().Fill("#bbf")))));
        }
        [Test]
        public Task StylingSubgraph()
        {
            return VerifyFlowchart(
                () => Flowchart.Start()
                    .WithSubgraph(
                        Subgraph
                            .Named("subId")
                            .WithContent(Flowchart.Start())
                            .Styled(NodeStyle.Start().Fill("#bbf"))));
        }
    }

    public class _8_AppendingFlowcharts : FlowchartTests
    {
        [Test]
        public Task KeepsFirstFlowChartOrientation()
        {
            return VerifyFlowchart(() => Flowchart
                .Start(Orientation.BottomToTop)
                .WithLink(Link.From(Node.Named("a")).To(Node.Named("b")))
                .Append(Flowchart
                    .Start(Orientation.RightToLeft)
                    .WithLink(Link.From(Node.Named("c")).To(Node.Named("d")))
                ));
        }

        [Test]
        public Task AppendNodes()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithNode(Node.Named("Original"))
                .Append(Flowchart.Start()
                    .WithNode(Node.Named("ToAppend"))));
        }

        [Test]
        public Task AppendLinks()
        {
            return VerifyFlowchart(() => Flowchart.Start()
                .WithLink(new Link(Node.Named("a"), Node.Named("b")))
                .Append(Flowchart.Start()
                    .WithLink(new Link(Node.Named("c"), Node.Named("d")))));
        }

        [Test]
        public Task AppendSubgraphs()
        {
            return VerifyFlowchart(() => Flowchart
                .Start()
                .WithSubgraph(new Subgraph("subA", Flowchart.Start().WithNode(Node.Named("A"))))
                .Append(Flowchart.Start()
                    .WithSubgraph(new Subgraph("subB", Flowchart.Start().WithNode(Node.Named("B"))))));
        }
    }
}