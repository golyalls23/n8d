using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConApp.Linq;

public class L1
{

    /*
     * Immediate Execution : 
     *          Aggregrate, All, Any, Average, Contains, Count, 
     *          ElementAt, ElementAtOrDefault, Empty, First, FirstOrDefault
     *          Last, LastOrDefault, LongCount, Max, Min
     *          SequenceEqual, Single, SingleOrDefault
     *          Sum, ToArray, ToDictionary, ToList, ToLookUp
     *                       
     * Deffered Streaming Execution :
     *          AsEnumerable, Cast, Concat, DefaultIfEmpty, Distinct
     *          Except, GroupJoin, Intersect, Join, OfType
     *          Range, Repeat, Select, SelectMany, Skip, SkipWhile
     *          Take, TakeWhile, Union, Where
     * 
     * Deffered Nonstreaming Execution :
     *          Except, GroupBy, GroupJoin, Intersect, Join
     *          OrderBy, OrderByDescending, ThenBy, ThenByDescending
     *          Reverse
     */

    public static void P1()
    {
        // 1. Data Source
        int[] numbers = [0, 1, 2, 3, 4, 5, 6];

        // 2. Query Creation
        //var numQuery = numbers.Where(n => n % 2 == 0);
        var numQuery = from num in numbers
                       where (num % 2) == 0
                       select num;
        
        // 3. Query Execution Deffered
        // All IEnumberable<T> are deffered : Deffered execution provide the facility of query reuse
        // Can be streaming with yield
        // nonstreaming : read all data before yeild.
        // Nonstreaming : Operation such as sorting and grouping fall into this category.
        foreach (int num in numQuery)
            Console.WriteLine(num);

        // 2 and 3 Query Creation and Query Execution Immediate
        var numQuery2 = (from num in numbers
                         where (num % 2) == 0
                         select num)
                         .ToList();


        Console.WriteLine($"Count: {numQuery.Count()}");
        Console.WriteLine($"Max: {numQuery.Max()}");
        Console.WriteLine($"Min: {numQuery.Min()}");
        Console.WriteLine($"Average: {numQuery.Average()}");
        Console.WriteLine($"First: {numQuery.First()}");
        Console.WriteLine($"Last: {numQuery.Last()}");

    }

    public static void P2()
    {
        var numbers = new List<int>() { 1,2,4,6,8,10,12,14,16,18,20};

        IEnumerable<int> queryFactorOffFour = from num in numbers
                                              where num % 4 == 0
                                              select num;

        var factorOfFourList = queryFactorOffFour.ToList();
        Console.WriteLine(factorOfFourList[2]);

        //Console.WriteLine($"All : {numbers.All()}");
        Console.WriteLine($"Any : {numbers.Any()}");
        Console.WriteLine($"Average : {numbers.Average()}");
        Console.WriteLine($"Count : {numbers.Count()}");
        Console.WriteLine($"Count : {numbers.Count}");
        Console.WriteLine($"Contains : {numbers.Contains(2)}");
        //Console.WriteLine($"Empty : {numbers.Empty()}");
        Console.WriteLine($"First : {numbers.First()}");
        Console.WriteLine($"FirstOrDefault : {numbers.FirstOrDefault()}");
        Console.WriteLine($"Last : {numbers.Last()}");
        Console.WriteLine($"LastOrDefault : {numbers.LastOrDefault()}");
        Console.WriteLine($"Max : {numbers.Max()}");
        Console.WriteLine($"Min : {numbers.Min()}");
        //Console.WriteLine($"Single : {numbers.Single()}");
        //Console.WriteLine($"SingleOrDefault : {numbers.SingleOrDefault()}");
        Console.WriteLine($"Sum : {numbers.Sum()}");
        Console.WriteLine($"ToArry : {numbers.ToArray()}");
        //Console.WriteLine($" : {numbers.ToDictionary()}");
        Console.WriteLine($"ToList : {numbers.ToList()}");
        //Console.WriteLine($" : {numbers.ToLookUp()}");


        Console.WriteLine($"AsEnumerable : {numbers.AsEnumerable()}");
        //Console.WriteLine($" : {numbers.Cast()}");
        //Console.WriteLine($" : {numbers.Concat()}");
        Console.WriteLine($"DefaultIfEmpty : {numbers.DefaultIfEmpty()}");
        Console.WriteLine($"Distinct : {numbers.Distinct()}");
        //Console.WriteLine($" : {numbers.Except()}");
        //Console.WriteLine($" : {numbers.GroupJoin()}");
        //Console.WriteLine($" : {numbers.Intersect()}");
        //Console.WriteLine($" : {numbers.Join()}");
        //Console.WriteLine($" : {numbers.OfType()}");
        //Console.WriteLine($" : {numbers.Range()}");
        //Console.WriteLine($" : {numbers.Repeat()}");
        //Console.WriteLine($" : {numbers.Select()}");
        //Console.WriteLine($" : {numbers.SelectMany()}");
        Console.WriteLine($"Skip 1 : {numbers.Skip(1)}");
        //Console.WriteLine($" : {numbers.SkipWhile()}");
        Console.WriteLine($"Take 1 : {numbers.Take(1)}");
        //Console.WriteLine($" : {numbers.TakeWhile()}");
        //Console.WriteLine($" : {numbers.Union()}");
        //Console.WriteLine($" : {numbers.Where()}");
        //Console.WriteLine($" : {numbers.OrderBy()}");
        //Console.WriteLine($" : {numbers.OrderByDescending()}");
        //Console.WriteLine($" : {numbers.ThenBy()}");
        //Console.WriteLine($" : {numbers.ThenByDescending()}");
        //Console.WriteLine($" : {numbers.Reverse()}");

    }
}
