using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment04Tests<TWork> : Assignment<TWork>
    where TWork : ForLoopBase, new()
{
    public override int Number => 4;
    public override string Title => "For Loop";
    public override string Description =>
        "Use a for loop to calculate a sum and build a countdown string.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();
        var w = Work;

        (int n, int expected)[] sumCases = [(1, 1), (5, 15), (10, 55), (100, 5050)];
        foreach (var (n, expected) in sumCases)
            g.Expect($"Sum({n}) == {expected}", w.Sum(n), expected);

        (int n, string expected)[] countdownCases = [(1, "1"), (3, "3, 2, 1"), (5, "5, 4, 3, 2, 1")];
        foreach (var (n, expected) in countdownCases)
            g.Expect($"BuildCountdown({n}) == \"{expected}\"", w.BuildCountdown(n), expected);

        return g.Results();
    }
}
