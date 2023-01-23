### A node in a subroutine shape

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.Subroutine))
```

```mermaid
flowchart LR
    id[["id"]]
```