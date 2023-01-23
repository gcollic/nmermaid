### Oriented flowchart( right to left)

```csharp
Flowchart.Start(Orientation.RightToLeft)
    .WithLink(LinkBuilder.From(Node.Named("Start"))
        .To(Node.Named("Stop")))
```

```mermaid
flowchart RL
    Start --> Stop
```