using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int first, int second)> dominoes)
    {
        var firstDomino = dominoes.FirstOrDefault();
        return GetOppositeAndRemainder(firstDomino, dominoes.ReturnWithout(firstDomino));
    }

    private static bool GetOppositeAndRemainder((int start, int end) chain, IEnumerable<(int a, int b)> dominoes)
    {
        var remaining = dominoes.ToList();
        var currentListSize = remaining.Count;
        var (start, end) = chain;
        if (currentListSize < 1) return start == end;


        return remaining.Where(dom => end == dom.a || end == dom.b)
                        .Select(dom => (chain: (start, end == dom.a ? dom.b : dom.a),
                                        withoutDom: remaining.ReturnWithout(dom)))
                        .Any(args => GetOppositeAndRemainder(args.chain, args.withoutDom));
    }

    private static IEnumerable<T> ReturnWithout<T>(this IEnumerable<T> list, T item)
    {
        var shallowCopy = list.ToList();
        shallowCopy.Remove(item);
        return shallowCopy;
    }


    private static (int, int) Flip(this (int aSide, int bSide) domino)
        => (domino.bSide, domino.aSide);
}