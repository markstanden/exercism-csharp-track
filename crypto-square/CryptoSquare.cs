using System;
using System.Collections.Generic;
using System.Linq;

public static class CryptoSquare
{
    public static string NormalizedPlaintext(string plaintext)
        => plaintext.ToLower()
                    .Replace(" ", "");


    public static IEnumerable<string> PlaintextSegments(string plaintext)
    {
        int chunkSize = CalcColSize(plaintext.Length);
        return Enumerable.Range(0, plaintext.Length / chunkSize)
                         .Select(i => plaintext.Substring(i * chunkSize, chunkSize));
    }


    private static int CalcColSize(int messageLength)
        => Enumerable.Range(1, messageLength)
                     .Select(cols => Enumerable.Range(1, messageLength)
                                               .Where(rows => cols * rows >= messageLength)
                                               .Where(rows => rows > cols)
                                               .Where(rows => rows - cols <= 1))
                     .SelectMany(x => x)
                     .Min();


    public static string Encoded(string plaintext)
    {
        return PlaintextSegments(plaintext)
           .Select(str => str.ToArray())
           .Translate().Select(row => row.ToString()).ToString();
    }


    private static IEnumerable<IEnumerable<T>> Translate<T>(this IEnumerable<IEnumerable<T>> gridEnumerable)
    {
        var grid = gridEnumerable.Select(row => row.ToArray())
                                 .ToArray();
        return Enumerable.Range(0, grid.GetLength(1))
                         .Select(col => grid.GetColumn(col));
    }


    private static IEnumerable<T> GetColumn<T>(this IReadOnlyList<T[]> grid,  int colNumber)
        => Enumerable.Range(0, grid.Count)
                     .Select(row => grid[row][colNumber]);


    public static string Ciphertext(string plaintext)
    {
        throw new NotImplementedException("You need to implement this function.");
    }
}