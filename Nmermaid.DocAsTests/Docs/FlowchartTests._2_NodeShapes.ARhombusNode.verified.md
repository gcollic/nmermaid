### A rhombus node

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.Rhombus))
```

```mermaid
flowchart LR
    id{"id"}
```