using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class CryptoSquare
{
    public static string NormalizedPlaintext(string plaintext)
        => Regex.Replace(plaintext, @"([^a-zA-Z0-9]+)", @"")
                .ToLower();



    public static IEnumerable<string> PlaintextSegments(string plaintext)
    {
        var normalized = NormalizedPlaintext(plaintext);

        int columns = CalcColSize(normalized.Length);
        int rows = IntCeiling(normalized.Length, columns);

        string padded = normalized.PadRight(rows * columns);

        return Enumerable.Range(0, rows)
                         .Select(i => padded.Substring(i * columns, columns));
    }


    /// <summary>
    /// Cleanly divides two integers and
    /// returns the 'rounded up' ceiling value.
    /// Takes two integers and returns and int.
    ///
    /// How many boxes are needed for x (nominator) items when each box stores y (denominator) items?
    /// IntCeiling(x, y)
    ///
    /// How many boxes are needed for 10 items when each box stores 3 (denominator) items?
    /// IntCeiling(10, 3) = 4
    ///
    /// Removes the need to cast doubles when working with Math.Ceiling.
    ///
    /// </summary>
    /// <param name="nominator">The number to divide.</param>
    /// <param name="denominator">The number to divide by.</param>
    /// <returns>The rounded up integer value</returns>
    private static int IntCeiling(int nominator, int denominator)
        => (nominator + denominator - 1) / denominator;

    private static int CalcColSize(int messageLength)
        => Enumerable.Range(1, messageLength)
                     .Select(rows => Enumerable.Range(1, messageLength)
                                               .Where(cols => cols * rows >= messageLength)
                                               .Where(cols => cols >= rows)
                                               .Where(cols => cols - rows <= 1))
                     .SelectMany(x => x)
                     .Min();


    public static string Encoded(string plaintext)
        => string.Join(" ", PlaintextSegments(plaintext)
                          .Select(str => str.ToCharArray())
                          .Transpose()
                          .Select(row => string.Join("", row.ToArray())));


    /// <summary>
    /// Transposes a nested IEnumerable grid
    /// </summary>
    /// <param name="gridEnumerable">The nested IEnumerable grid to transpose</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A new Transposed nested IEnumerable grid</returns>
    private static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> gridEnumerable)
    {
        var grid = gridEnumerable.Select(row => row.ToArray())
                                 .ToArray();
        return Enumerable.Range(0, grid[0]
                                   .Length)
                         .Select(col => grid.GetColumn(col));
    }


    private static IEnumerable<T> GetColumn<T>(this IReadOnlyList<T[]> grid, int colNumber)
        => Enumerable.Range(0, grid.Count)
                     .Select(row => grid[row][colNumber]);


    public static string Ciphertext(string plaintext)
        => plaintext.Length == 0 ? "" : Encoded(plaintext);
}