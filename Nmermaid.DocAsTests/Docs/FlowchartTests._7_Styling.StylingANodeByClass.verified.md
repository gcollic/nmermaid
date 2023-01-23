### Styling a node by class

```csharp
Flowchart.Start()
    .WithNode(Node.Named("id1")
        .Styled(new NodeStyleClass("my_Class_Name", nodeStyle1)))
    .WithNode(Node.Named("id2")
        .Styled(new NodeStyleClass("my_Class_Name", nodeStyle1)))
    .WithNode(Node.Named("id3")
        .Styled(styleClass2))
    .WithNode(Node.Named("id4")
        .Styled(styleClass2))
```

```mermaid
flowchart LR
    classDef myClassName fill:#bbf,stroke:#f66,stroke-width:2px,color:#fff,stroke-dasharray:5 5;
    classDef mysecondClassName fill:#ffb,stroke:#66f,stroke-width:4px,color:#330,stroke-dasharray:1 1 1 1 1 1 2 2 2 2 2 2 1 1 1 1 1 1;
    id1
    class id1 myClassName;
    id2
    class id2 myClassName;
    id3
    class id3 mysecondClassName;
    id4
    class id4 mysecondClassName;
```