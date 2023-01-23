### Complex links

```csharp
Flowchart.Start()
    .WithLink(LinkBuilder.From(Node.Named("A"))
        .To(Node.Named("B1"))
        .WithOptions(LinkOptions.Default
            .WithHead(head)
            .WithDirection(direction)
            .WithLineType(lineType)
            .WithMinimumLength(1)))
    .WithLink(LinkBuilder.From(Node.Named("A"))
        .To(Node.Named("B2"))
        .WithOptions(LinkOptions.Default
            .WithHead(head)
            .WithDirection(direction)
            .WithLineType(lineType)
            .WithMinimumLength(2)))
    .WithLink(LinkBuilder.From(Node.Named("A"))
        .To(Node.Named("B3"))
        .WithOptions(LinkOptions.Default
            .WithHead(head)
            .WithDirection(direction)
            .WithLineType(lineType)
            .WithMinimumLength(3)))
```

<table>
<tr> <th>Head</th> <th>Direction</th> <th>LineType</th> <th>Result</th> </tr>


<tr>
<td> None </td> <td> Dual </td> <td> Straight </td>
<td>

```mermaid
flowchart LR
    A --- B1
    A ---- B2
    A ----- B3
```

</td>
</tr>
<tr>
<td> Circle </td> <td> None </td> <td> Straight </td>
<td>

```mermaid
flowchart LR
    A --- B1
    A ---- B2
    A ----- B3
```

</td>
</tr>
<tr>
<td> Arrow </td> <td> Single </td> <td> Straight </td>
<td>

```mermaid
flowchart LR
    A --> B1
    A ---> B2
    A ----> B3
```

</td>
</tr>
<tr>
<td> Circle </td> <td> Single </td> <td> Straight </td>
<td>

```mermaid
flowchart LR
    A --o B1
    A ---o B2
    A ----o B3
```

</td>
</tr>
<tr>
<td> Cross </td> <td> Single </td> <td> Straight </td>
<td>

```mermaid
flowchart LR
    A --x B1
    A ---x B2
    A ----x B3
```

</td>
</tr>
<tr>
<td> Arrow </td> <td> Dual </td> <td> Straight </td>
<td>

```mermaid
flowchart LR
    A <--> B1
    A <---> B2
    A <----> B3
```

</td>
</tr>
<tr>
<td> Circle </td> <td> Dual </td> <td> Straight </td>
<td>

```mermaid
flowchart LR
    A o--o B1
    A o---o B2
    A o----o B3
```

</td>
</tr>
<tr>
<td> Cross </td> <td> Dual </td> <td> Straight </td>
<td>

```mermaid
flowchart LR
    A x--x B1
    A x---x B2
    A x----x B3
```

</td>
</tr>
<tr>
<td> None </td> <td> None </td> <td> Thick </td>
<td>

```mermaid
flowchart LR
    A === B1
    A ==== B2
    A ===== B3
```

</td>
</tr>
<tr>
<td> Arrow </td> <td> Single </td> <td> Thick </td>
<td>

```mermaid
flowchart LR
    A ==> B1
    A ===> B2
    A ====> B3
```

</td>
</tr>
<tr>
<td> None </td> <td> None </td> <td> Dotted </td>
<td>

```mermaid
flowchart LR
    A -.- B1
    A -..- B2
    A -...- B3
```

</td>
</tr>
<tr>
<td> Arrow </td> <td> Single </td> <td> Dotted </td>
<td>

```mermaid
flowchart LR
    A -.-> B1
    A -..-> B2
    A -...-> B3
```

</td>
</tr>


</table>
