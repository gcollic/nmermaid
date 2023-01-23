using System;
using System.Text.RegularExpressions;

namespace Nmermaid.DocAsTests;

public static class StringHelper
{
    public static string FirstUpperThenLower(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        input = input.Trim();
        if (input.Length == 1)
        {
            return input.ToUpperInvariant();
        }

        return input[0].ToString().ToUpperInvariant() + input.Substring(1).ToLowerInvariant();
    }

    public static string FromCamelCase(string input)
    {
        return FirstUpperThenLower(String.Join(' ', Regex.Split(input, @"(?=[A-Z])")));
    }

}