### A node with round edges

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.RoundEdges))
```

```mermaid
flowchart LR
    id("id")
```