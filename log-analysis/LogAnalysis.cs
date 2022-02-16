public static class LogAnalysis
{
    public static string SubstringAfter(this string str, string phrase)
    {
        int startIndex = str.IndexOf(phrase) + phrase.Length;
        return str.Substring(startIndex, str.Length - startIndex);
    }

    public static string SubstringBetween(this string str, string startPhrase, string endPhrase)
    {
        string substringStart = str.SubstringAfter(startPhrase);
        int endIndex = substringStart.IndexOf(endPhrase);

        return substringStart[..endIndex];
    }

    public static string Message(this string alert)
        => alert.SubstringAfter(":").Trim();

    public static string LogLevel(this string alert)
        => alert.SubstringBetween("[", "]");
}