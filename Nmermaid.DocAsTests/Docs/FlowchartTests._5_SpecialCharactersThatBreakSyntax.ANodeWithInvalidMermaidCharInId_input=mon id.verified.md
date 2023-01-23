### A node with invalid mermaid char in id("mon id")

```csharp
Flowchart.Start()
    .WithNode(Node.Named(input))
```

```mermaid
flowchart LR
    monid["mon id"]
```