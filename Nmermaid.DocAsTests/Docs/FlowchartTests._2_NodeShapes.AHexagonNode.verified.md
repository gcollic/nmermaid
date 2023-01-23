### A hexagon node

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.Hexagon))
```

```mermaid
flowchart LR
    id{{"id"}}
```