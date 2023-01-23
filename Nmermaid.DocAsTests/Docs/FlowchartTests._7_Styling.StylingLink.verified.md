### Styling link

```csharp
Flowchart.Start()
    .WithLink(LinkBuilder.From(Node.Named("A"))
        .To(Node.Named("B")))
    .WithLink(LinkBuilder.From(Node.Named("C"))
        .To(Node.Named("D"))
        .Styled("stroke:#ff3,stroke-width:4px,color:red;"))
    .WithLink(LinkBuilder.From(Node.Named("E"))
        .To(Node.Named("F")))
```

```mermaid
flowchart LR
    A --> B
    C --> D
    E --> F
    linkStyle 1 stroke:#ff3,stroke-width:4px,color:red;
```