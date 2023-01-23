### A subgraph

```csharp
Flowchart.Start()
    .WithNode(Node.Named("A1"))
    .WithNode(Node.Named("B1"))
    .WithLink(LinkBuilder.From(Node.Named("A1"))
        .To(Node.Named("B1")))
    .WithSubgraph(new Subgraph("subId", Flowchart.Start()
        .WithNode(Node.Named("A2"))
        .WithNode(Node.Named("B2"))
        .WithLink(LinkBuilder.From(Node.Named("A2"))
            .To(Node.Named("B2")))))
```

```mermaid
flowchart LR
    A1
    B1
    subgraph subId
        A2
        B2
        A2 --> B2
    end
    A1 --> B1
```