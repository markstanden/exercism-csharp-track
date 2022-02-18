using System;
using System.Text.RegularExpressions;

static class LogLine
{
    public static string Message(string logLine)
    {
        return Regex.Match(logLine, @"\]:\s*([\w\s]+)\b\s*$")
            .Groups[1]
            .Value;
    }

    public static string LogLevel(string logLine)
    {
        return Regex.Match(logLine, @"\[([a-zA-Z]+)[\]].*$")
            .Groups[1]
            .Value
            .ToLower();
    }

    public static string Reformat(string logLine)
    {
        return $"{Message(logLine)} ({LogLevel(logLine)})";
    }
}