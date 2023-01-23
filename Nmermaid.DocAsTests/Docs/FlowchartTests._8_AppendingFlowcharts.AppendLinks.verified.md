### Append links

```csharp
Flowchart.Start()
    .WithLink(new Link(Node.Named("a"), Node.Named("b")))
    .Append(Flowchart.Start()
        .WithLink(new Link(Node.Named("c"), Node.Named("d"))))
```

```mermaid
flowchart LR
    a --> b
    c --> d
```