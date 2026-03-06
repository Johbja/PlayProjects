using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment07Tests<TWork> : Assignment<TWork>
    where TWork : MethodsAndRecursionBase, new()
{
    public override int Number => 7;
    public override string Title => "Methods & Recursion";
    public override string Description =>
        "Implement recursive methods: factorial and Fibonacci.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();
        var w = Work;

        (int n, long expected)[] factCases =
        [
            (0, 1), (1, 1), (5, 120), (10, 3628800), (12, 479001600)
        ];
        foreach (var (n, expected) in factCases)
            g.Expect($"Factorial({n}) == {expected}", w.Factorial(n), expected);

        (int n, long expected)[] fibCases =
        [
            (0, 0), (1, 1), (2, 1), (7, 13), (10, 55)
        ];
        foreach (var (n, expected) in fibCases)
            g.Expect($"Fibonacci({n}) == {expected}", w.Fibonacci(n), expected);

        return g.Results();
    }
}
