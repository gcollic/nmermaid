### A trapezoid node

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.Trapezoid))
```

```mermaid
flowchart LR
    id[/"id"\]
```