using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 9 — Lists & LINQ                         ║
// ╚══════════════════════════════════════════════════════╝
//
// Implement the three methods using LINQ.
//
//  GetEvenNumbers(numbers)
//    Returns a new list with only the even numbers.
//    Use LINQ
//    [Tip]: The LINQ expression <Where> lets you filter a list based on a condition. For example, numbers.Where(n => n > 0)
//    would return a new list containing only the numbers that are larger then 0.
//
//  GetTopThree(numbers)
//    Returns the 3 largest numbers in descending order.
//    Use LINQ
//    [Tip]: The LINQ expression <OrderByDescending> lets you sort a list in descending order.
//           You can then use the LINQ expression <Take> to get the top elements.
//
//  GetWordsShorterThan(words, maxLength)
//    Returns words whose length is strictly less than
//    maxLength, sorted alphabetically.
//    Use LINQ
//    [Tip]: The LINQ expression <Where> lets you filter a list based on a condition.
//           You can then use <OrderBy> to sort the filtered list.
//
public class ListsAndLinq : ListsAndLinqBase
{
    private void LinqSyntaxExample()
    {
        //Create a list of integers from 1 to 6
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

        // Example of using LINQ to filter out numbers less then 3 then take the 2 first values which in this case would be 6 and 5
        // i.e. largeNumbers = {6, 5}
        var largeNumbers = numbers.Where(number => number > 3).OrderByDescending(number => number).Take(2);
    }

    public override List<int> GetEvenNumbers(List<int> numbers)
    {
        // TODO: return only even numbers using LINQ Where
        return new List<int>();
    }

    public override List<int> GetTopThree(List<int> numbers)
    {
        // TODO: return the 3 largest numbers descending using LINQ
        return new List<int>();
    }

    public override List<string> GetWordsShorterThan(List<string> words, int maxLength)
    {
        // TODO: return words with length < maxLength, sorted alphabetically
        return new List<string>();
    }
}
