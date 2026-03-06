using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment05Tests<TWork> : Assignment<TWork>
    where TWork : WhileLoopBase, new()
{
    public override int Number => 5;
    public override string Title => "While Loop";
    public override string Description =>
        "Use a while loop to compute Collatz steps and to reverse a string.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();
        var w = Work;

        (int n, int expected)[] collatzCases = [(1, 0), (2, 1), (6, 8), (27, 111)];
        foreach (var (n, expected) in collatzCases)
            g.Expect($"CollatzSteps({n}) == {expected}", w.CollatzSteps(n), expected);

        (string input, string expected)[] reverseCases =
        [
            ("a", "a"), ("hello", "olleh"), ("CSharp", "prahSC"), ("12345", "54321")
        ];
        foreach (var (input, expected) in reverseCases)
            g.Expect($"ReverseString(\"{input}\") == \"{expected}\"", w.ReverseString(input), expected);

        return g.Results();
    }
}
