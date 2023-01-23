### Multiple nodes

```csharp
Flowchart.Start()
    .WithNode(Node.Named("A"))
    .WithNode(Node.Named("B"))
```

```mermaid
flowchart LR
    A
    B
```