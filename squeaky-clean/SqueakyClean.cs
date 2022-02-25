using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class Identifier
{
    public static string Clean(string identifier)
        => identifier.RemoveControl()
                     .RemoveGreek()
                     .kebabToCamel()
                     .RemoveNonLetters()
                     .RemoveWhiteSpace();


    private static string RemoveNonLetters(this string phrase)
        => phrase.ReplaceWith(@"[^\p{L}\s]", "");


    private static string RemoveControl(this string phrase)
        => phrase.ReplaceWith(@"\p{Cc}", "CTRL");


    private static string RemoveGreek(this string phrase)
        => phrase.ReplaceWith(@"[\u03AC-\u03CE]", "");


    private static string RemoveWhiteSpace(this string phrase)
        => phrase.ReplaceWith(@"\s", "_");


    private static string kebabToCamel(this string phrase)
        => Regex.Replace(phrase, @"-(\p{L})", match => match.ToString().ToUpper());


    private static string ReplaceWith(this string phrase, string pattern, string replacement)
        => Regex.Replace(phrase, pattern, replacement);
}