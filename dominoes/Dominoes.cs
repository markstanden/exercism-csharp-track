using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int first, int second)> dominoes)
    {
        var firstDomino = dominoes.FirstOrDefault();
        return GetOppositeAndRemainder(firstDomino, dominoes.ReturnWithout(firstDomino));
    }

    private static bool GetOppositeAndRemainder((int start, int end) chain, IEnumerable<(int a, int b)> remaining)
    {
        var currentListSize = remaining.LongCount();
        var start = chain.start;
        var end = chain.end;
        var listToScan = remaining;

        if (currentListSize < 1) return chain.start == chain.end;

        var first = listToScan.Where(listDom => chain.end == listDom.a)
                              .Select(listDom => (listDom, listToScan.ReturnWithout(listDom)))
                              .Any(args => GetOppositeAndRemainder((chain.start, args.listDom.b), args.Item2));

        var second = listToScan.Where(listDom => chain.end == listDom.b)
                               .Select(listDom => (listDom, listToScan.ReturnWithout(listDom)))
                               .Any(args => GetOppositeAndRemainder((chain.start, args.listDom.a), args.Item2));

        return first || second;
    }

    private static IEnumerable<T> ReturnWithout<T>(this IEnumerable<T> list, T item)
        => list.Where(items => !items.Equals(item));

    private static (int, int) Flip(this (int aSide, int bSide) domino)
        => (domino.bSide, domino.aSide);
}