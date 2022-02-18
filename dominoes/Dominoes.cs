using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    /// <summary>
    /// Given a sequence of unordered, reversible tuples identify whether
    /// they can be chained into a single loop using all dominoes.
    /// </summary>
    /// <param name="dominoes">The iterable of dominoes to be chained.</param>
    /// <returns>True if the dominoes can be correctly chained, False if the chain cannot complete one continous loop.</returns>
    public static bool CanChain(IEnumerable<(int first, int second)> dominoes)
    {
        var firstDomino = dominoes.FirstOrDefault();
        return GetOppositeAndRemainder(firstDomino, dominoes.ReturnWithout(firstDomino));
    }

    /// <summary>
    /// Recursively searches for a circular chain using all Dominoes in the supplied enumerable.
    /// </summary>
    /// <param name="chain">A tuple representing the current start and end values of the chain.</param>
    /// <param name="dominoes">The dominoes left to be potentially added to the chain.</param>
    /// <returns>True if all the dominoes can form a chain, AND the complete chain forms a loop.</returns>
    private static bool GetOppositeAndRemainder((int start, int end) chain, IEnumerable<(int a, int b)> dominoes)
    {
        var remaining = dominoes.ToList();
        var (start, end) = chain;
        if (remaining.Count < 1) return start == end;


        return remaining.Where(dom => end == dom.a || end == dom.b)
                        .Select(dom => (chain: (start, end == dom.a ? dom.b : dom.a),
                                        withoutDom: remaining.ReturnWithout(dom)))
                        .Any(args => GetOppositeAndRemainder(args.chain, args.withoutDom));
    }

    /// <summary>
    /// Returns an updated IEnumerable without the first occurrence of the item.
    /// </summary>
    /// <param name="sequence">The sequence to remove the item from</param>
    /// <param name="item">The item to be excluded from the sequence</param>
    /// <typeparam name="T">The Type of the items in the sequence</typeparam>
    /// <returns>A new, ordered sequence that doesn't contain the first occurrence of item</returns>
    private static IEnumerable<T> ReturnWithout<T>(this IEnumerable<T> sequence, T item)
    {
        var copy = sequence.ToList();
        copy.Remove(item);
        return copy;
    }
}