using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using VerifyNUnit;
using VerifyTests;

namespace Nmermaid.DocAsTests;

[TestFixture]
public abstract class DocAsTest
{
    private const string FolderName = "Docs";
    private const string MarkdownExtension = "md";
    public string TestTitle => StringHelper.FromCamelCase(TestContext.CurrentContext.Test.Name);
    public static string DocsFolder => Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        FolderName
    );

    VerifySettings _settings;

    protected DocAsTest()
    {
        _settings = new();
        _settings.UseDirectory(FolderName);
        _settings.DisableDiff();
#if NCRUNCH
        this._settings.DisableRequireUniquePrefix();
#endif
    }

    protected Task VerifyTestInMarkdown(string content)
    {
        var contentWithTitle = $@"### {TestTitle}
{content}";
        return VerifyWithExtension(contentWithTitle, MarkdownExtension, _settings);
    }

    protected Task AutoVerifyMarkdown(string content)
    {
        var localSettings = new VerifySettings(_settings);
        localSettings.AutoVerify();
        return VerifyWithExtension(content, MarkdownExtension, localSettings);
    }

    protected Task VerifyAsciiDoc(string content)
    {
        string extension = "adoc";
        // "Verify" throws when he doesn't know adoc is text-based
        EmptyFiles.Extensions.AddTextExtension(extension);
        return VerifyWithExtension(content, extension, _settings);
    }

    private static Task VerifyWithExtension(string content, string extension, VerifySettings settings)
    {
        var localSettings = new VerifySettings(settings);
        localSettings.UseExtension(extension);

        return Verifier.Verify(
            target: content.Replace("\r", ""),
            settings: localSettings);
    }
}