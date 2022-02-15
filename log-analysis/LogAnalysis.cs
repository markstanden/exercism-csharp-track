using System;

public static class LogAnalysis
{
    public static string SubstringAfter(this string str, string phrase)
    {
        int startIndex = str.IndexOf("phrase") + phrase.Length;
        return str.Substring(startIndex, str.Length - startIndex);
    }

    public static string SubstringBetween(this string str, string startPhrase, string endPhrase)
    {
        string start = str.SubstringAfter(startPhrase);
        int end = start.IndexOf(endPhrase);

        return start.Substring(0, end);
    }
}