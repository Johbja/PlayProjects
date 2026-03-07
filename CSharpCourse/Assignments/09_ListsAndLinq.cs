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
//    Use LINQ Where.
//
//  GetTopThree(numbers)
//    Returns the 3 largest numbers in descending order.
//    Use LINQ OrderByDescending + Take.
//
//  GetWordsShorterThan(words, maxLength)
//    Returns words whose length is strictly less than
//    maxLength, sorted alphabetically.
//    Use LINQ Where + OrderBy.
//
public class ListsAndLinq : ListsAndLinqBase
{
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
