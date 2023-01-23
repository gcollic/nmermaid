### Oriented flowchart( bottom to top)

```csharp
Flowchart.Start(Orientation.BottomToTop)
    .WithLink(LinkBuilder.From(Node.Named("Start"))
        .To(Node.Named("Stop")))
```

```mermaid
flowchart BT
    Start --> Stop
```