### Multiple nodes with unique declaration

```csharp
Flowchart.Start()
    .WithNodes(new [] {Node.Named("A"), Node.Named("B")})
```

```mermaid
flowchart LR
    A
    B
```