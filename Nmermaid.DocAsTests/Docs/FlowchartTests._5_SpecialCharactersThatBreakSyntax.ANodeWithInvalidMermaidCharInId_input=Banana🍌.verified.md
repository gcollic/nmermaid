### A node with invalid mermaid char in id(" banana🍌")

```csharp
Flowchart.Start()
    .WithNode(Node.Named(input))
```

```mermaid
flowchart LR
    Banana["Banana#127820;"]
```