### A parallelogram node

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.Parallelogram))
```

```mermaid
flowchart LR
    id[/"id"/]
```