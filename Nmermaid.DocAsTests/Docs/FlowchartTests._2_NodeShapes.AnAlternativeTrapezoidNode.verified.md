### An alternative trapezoid node

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.TrapezoidAlt))
```

```mermaid
flowchart LR
    id[\"id"/]
```