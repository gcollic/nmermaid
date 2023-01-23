### A link with undeclared nodes

```csharp
Flowchart.Start()
    .WithLink(LinkBuilder.From(Node.Named("A"))
        .To(Node.Named("B")))
```

```mermaid
flowchart LR
    A --> B
```