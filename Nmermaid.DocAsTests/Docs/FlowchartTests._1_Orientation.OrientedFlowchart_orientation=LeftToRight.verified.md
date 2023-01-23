### Oriented flowchart( left to right)

```csharp
Flowchart.Start(Orientation.LeftToRight)
    .WithLink(LinkBuilder.From(Node.Named("Start"))
        .To(Node.Named("Stop")))
```

```mermaid
flowchart LR
    Start --> Stop
```