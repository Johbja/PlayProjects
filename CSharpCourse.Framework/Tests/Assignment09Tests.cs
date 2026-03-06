using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment09Tests<TWork> : Assignment<TWork>
    where TWork : ListsAndLinqBase, new()
{
    public override int Number => 9;
    public override string Title => "Lists & LINQ";
    public override string Description =>
        "Use List<T> and LINQ to filter and order collections.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();
        var w = Work;

        g.ExpectSequence(
            "GetEvenNumbers([1..8]) == [2, 4, 6, 8]",
            w.GetEvenNumbers([1, 2, 3, 4, 5, 6, 7, 8]),
            [2, 4, 6, 8]);

        g.ExpectSequence(
            "GetTopThree([4,1,9,2,7,3,8]) == [9, 8, 7]",
            w.GetTopThree([4, 1, 9, 2, 7, 3, 8]),
            [9, 8, 7]);

        g.ExpectSequence(
            "GetWordsShorterThan([...], 5) == [\"fig\", \"kiwi\", \"plum\"]",
            w.GetWordsShorterThan(["banana", "fig", "apple", "kiwi", "plum"], 5),
            ["fig", "kiwi", "plum"]);

        return g.Results();
    }
}
