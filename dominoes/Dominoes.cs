using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int first, int second)> dominoes)
    {
        return getOppositeAndRemainder((dominoes.FirstOrDefault(), dominoes)).HasValue;

        //return dominoes.withoutDoubles().hasEvenNumberOfValues();
    }

    private static bool hasEvenNumberOfValues(this IEnumerable<(int aSide, int bSide)> dominoes)
    {
        var doms = dominoes.SelectMany(dom => new int[] {dom.aSide, dom.bSide});
        return doms.All(allVals => doms.Count(val => allVals == val) % 2 == 0);
    }

    private static IEnumerable<(int, int)> withoutDoubles(this IEnumerable<(int aSide, int bSide)> stack)
    {
        return stack.Where(domino => domino.aSide != domino.bSide);
    }

    private static ((int, int), IEnumerable<(int, int)>)? getOppositeAndRemainder(
        ((int aSide, int bSide) domToCheck, IEnumerable<(int aSide, int bSide)> remaining)? matchTuple)
    {
        var currentListSize = matchTuple.Value.remaining.LongCount();
        if (currentListSize < 1) return new ValueTuple<(int, int), IEnumerable<(int, int)>>();

        var domToCheck = matchTuple.Value.domToCheck;
        var listToScan = matchTuple.Value.remaining;

        var first = listToScan.Where(listDom => listDom.aSide == domToCheck.bSide)
                  .Select(listDom => (listDom, listToScan.returnWithout(listDom)))
                  .Where(args => getOppositeAndRemainder(args).HasValue)
                  .FirstOrDefault(args => domToCheck.aSide == args.listDom.bSide);

        var second = listToScan.Where(listDom => listDom.bSide == domToCheck.aSide)
                                 .Select(listDom => (listDom, listToScan.returnWithout(listDom)))
                                 .Where(args => getOppositeAndRemainder(args).HasValue)
                                 .FirstOrDefault(args => domToCheck.bSide == args.listDom.aSide);

        return first.
    }

    private static IEnumerable<T> returnWithout<T>(this IEnumerable<T> list, T item)
        => list.Where(items => !items.Equals(item));
}