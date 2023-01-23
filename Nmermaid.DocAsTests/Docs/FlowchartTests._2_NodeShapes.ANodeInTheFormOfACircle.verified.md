### A node in the form of a circle

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.Circle))
```

```mermaid
flowchart LR
    id(("id"))
```