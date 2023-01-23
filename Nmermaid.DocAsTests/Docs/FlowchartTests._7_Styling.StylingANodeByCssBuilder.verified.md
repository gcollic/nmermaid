### Styling a node by css builder

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id2")
        .Styled(NodeStyleBuilder.Start()
            .Fill("#bbf")
            .Stroke("#f66")
            .StrokeWidth("2px")
            .Color("#fff")
            .StrokeDashArray(new [] {5, 5})))
```

```mermaid
flowchart LR
    id2
    style id2 fill:#bbf,stroke:#f66,stroke-width:2px,color:#fff,stroke-dasharray:5 5
```