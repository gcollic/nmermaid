### Styling subgraph

```csharp
Flowchart.Start()
    .WithSubgraph(SubgraphBuilder.Named("subId")
        .WithContent(Flowchart.Start())
        .Styled(NodeStyleBuilder.Start()
            .Fill("#bbf")))
```

```mermaid
flowchart LR
    subgraph subId

    end
    style subId fill:#bbf
```