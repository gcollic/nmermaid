### A node in a cylindrical shape

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.Cylindrical))
```

```mermaid
flowchart LR
    id[("id")]
```