### An alternative parallelogram node

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.ParallelogramAlt))
```

```mermaid
flowchart LR
    id[\"id"\]
```