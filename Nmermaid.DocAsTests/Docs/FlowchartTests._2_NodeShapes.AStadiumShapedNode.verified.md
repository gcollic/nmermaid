### A stadium shaped node

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id")
        .Shaped(NodeShape.Stadium))
```

```mermaid
flowchart LR
    id(["id"])
```