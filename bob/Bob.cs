using System;
using System.Text.RegularExpressions;

public static class Bob
{
    private const string Question = "Sure.";
    private const string Exclamation = "Calm down, I know what I'm doing!";
    private const string AllCaps = "Whoa, chill out!";
    private const string Nothing = "Fine. Be that way!";
    private const string Unknown = "Whatever.";


    public static string Response(string statement)
    {
        var trimmedStatement = statement.Trim();
        var caps = Regex.IsMatch(trimmedStatement, @"^[^a-z]*[A-Z]+[\W\d]*$");

        if (caps)
        {
            return trimmedStatement.EndsWith("?") ? Exclamation : AllCaps;
        }
        if (trimmedStatement.EndsWith("?")) return Question;
        return trimmedStatement.Length == 0 ? Nothing : Unknown;
    }
}