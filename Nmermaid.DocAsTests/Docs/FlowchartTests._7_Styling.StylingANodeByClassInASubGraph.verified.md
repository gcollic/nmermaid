### Styling a node by class in a sub graph

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id1")
        .Styled(outerClassRed))
    .WithNode(Node.Named("id2")
        .Styled(sharedClassGreen))
    .WithSubgraph(new Subgraph("sub", Flowchart.Start()
        .WithNode(Node.Named("id3")
            .Styled(sharedClassGreen))
        .WithNode(Node.Named("id4")
            .Styled(innerClassBlue))))
```

```mermaid
flowchart LR
    classDef outerClassRed fill:#f00;
    classDef sharedClassGreen fill:#0f0;
    classDef innerClassBlue fill:#00f;
    id1
    class id1 outerClassRed;
    id2
    class id2 sharedClassGreen;
    subgraph sub
        id3
        class id3 sharedClassGreen;
        id4
        class id4 innerClassBlue;
    end
```