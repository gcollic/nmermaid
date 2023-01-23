using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Nmermaid.DocAsTests;

public class FlowchartDoc : DocAsTest
{
    [Test]
    public Task All()
    {
        var files = Directory.EnumerateFiles(
                DocsFolder,
                $"{nameof(FlowchartTests)}.*.*.verified.md")
            .GroupBy(fullName => Path.GetFileName(fullName).Split(".")[1])
            .OrderBy(x => x.Key)
            .ToList();

        return AutoVerifyMarkdown(
            string.Join(
                "\n",
                files.SelectMany(group => group
                        .OrderBy(filename => filename)
                        .Select(File.ReadAllText)
                        .Prepend("")
                        .Prepend($"## {StringHelper.FromCamelCase(Regex.Replace(group.Key, @"_\d+_", ""))}"))
                    .Prepend("")
            )
        );
    }
}