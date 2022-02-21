using System;
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
        return GetOppositeAndRemainder(firstDomino, dominoes.WithoutFirstMatch(d => d.Equals(firstDomino)));
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
                                        withoutDom: remaining.WithoutFirstMatch(d => d.Equals(dom))))
                        .Any(args => GetOppositeAndRemainder(args.chain, args.withoutDom));
    }


    /// <summary>
    /// Filters an IEnumerable and returns without the first element matching the supplied predicate.
    /// </summary>
    /// <param name="source">The IEnumerable to filter</param>
    /// <param name="predicate">The predicate to match the first item to exclude</param>
    /// <returns>IEnumerable with only the first matching occurrence removed.</returns>
    public static IEnumerable<TSource> WithoutFirstMatch<TSource>(this IEnumerable<TSource> source,
                                                                  Func<TSource, bool> predicate)
    {
        var matchFound = false;

        foreach (TSource element in source)
        {
            if (!predicate(element) || matchFound)
            {
                yield return element;
            }
            else
            {
                matchFound = true;
            }
        }
    }
}