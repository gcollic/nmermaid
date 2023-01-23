### A node in an asymmetric shape

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.Asymmetric))
```

```mermaid
flowchart LR
    id>"id"]
```