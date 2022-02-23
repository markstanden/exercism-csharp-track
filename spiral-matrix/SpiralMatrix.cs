using System.Linq;

public static class SpiralMatrix
{
    public static int[,] GetMatrix(int size)
    {
        var matrix = new int[size, size];
        int middleRow = Ceiling(size, 2);

        for (int rowIndex = 0; rowIndex < middleRow; rowIndex++)
        {
            matrix = matrix.DoToEachRow(rowIndex);
        }

        return matrix;
    }


    private static int[,] DoToEachRow(this int[,] grid, int rowIndex)
    {
        for (int rotation = 0; rotation < 4; rotation++)
        {
            grid = grid.OnEachRotation(rowIndex)
                       .Rotate();
        }

        return grid;
    }


    private static int[,] OnEachRotation(this int[,] matrix, int rowNumber)
    {
        int count = matrix.Max() + 1;
        int lastColIndex = matrix.GetRow(rowNumber)
                                 .TakeWhile(val => val < 0)
                                 .Count();

        while (lastColIndex < matrix.GetLength(1))
        {
            if (matrix[rowNumber, lastColIndex] == 0)
            {
                matrix[rowNumber, lastColIndex] = count++;
            }

            lastColIndex++;
        }

        return matrix;
    }


    private static int Max(this int[,] matrix)
        => matrix.Cast<int>()
               .Max();


    private static T[,] Rotate<T>(this T[,] matrix)
    {
        var xMax = matrix.GetLength(1);
        var yMax = matrix.GetLength(0);
        T[,] result = (T[,])matrix.Clone();
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                result[(xMax - 1) - x, y] = matrix[y, x];
            }
        }

        return result;
    }


    private static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        => Enumerable.Range(0, matrix.GetLength(1))
                     .Select(x => matrix[rowNumber, x])
                     .ToArray();


    /// <see href="https://github.com/markstanden/f-c-sharp"/>
    ///
    /// <summary>
    /// Cleanly divides two integers and
    /// returns the 'rounded up' ceiling value.
    /// Takes two integers and returns an int.
    ///
    /// <para>Removes the need to cast doubles when working with Math.Ceiling.</para>
    ///
    /// </summary>
    /// <example>
    /// <para>How many boxes are needed for x (nominator) items when each box stores y (denominator) items?</para>
    /// <c>IntCeiling(x, y)</c>
    /// </example>
    ///
    /// <example>
    /// <para>How many boxes are needed for 10 items when each box stores 3 (denominator) items?</para>
    /// <c>IntCeiling(10, 3) = 4</c>
    /// </example>
    /// <param name="nominator">The number to be divided.</param>
    /// <param name="denominator">The number to divide by.</param>
    /// <returns>The rounded up integer value</returns>
    private static int Ceiling(int nominator, int denominator)
        => (nominator + denominator - 1) / denominator;
}