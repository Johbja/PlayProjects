namespace CSharpCourse;

// Fluent helper used by the test files to build test cases.
// Students never need to open or understand this file.
public class Grader
{
    private readonly List<TestResult> results = new();

    // Assert that 'actual' equals 'expected'.
    public Grader Expect<T>(string label, T actual, T expected)
    {
        bool ok = EqualityComparer<T>.Default.Equals(actual, expected);
        results.Add(ok
            ? TestResult.Pass(label)
            : TestResult.Fail(label, $"Expected {Format(expected)}, got {Format(actual)}"));
        return this;
    }

    // Assert a free-form boolean condition.
    public Grader Assert(string label, bool condition, string failMessage)
    {
        results.Add(condition
            ? TestResult.Pass(label)
            : TestResult.Fail(label, failMessage));
        return this;
    }

    // Assert two sequences are equal element-by-element.
    public Grader ExpectSequence<T>(string label, IEnumerable<T> actual, IEnumerable<T> expected)
    {
        var a = actual.ToList();
        var e = expected.ToList();
        bool ok = a.SequenceEqual(e);
        results.Add(ok
            ? TestResult.Pass(label)
            : TestResult.Fail(label,
                $"Expected [{string.Join(", ", e.Select(Format))}], " +
                $"got [{string.Join(", ", a.Select(Format))}]"));
        return this;
    }

    // Assert two doubles are equal within a tolerance.
    public Grader ExpectApprox(string label, double actual, double expected, double tolerance = 1e-9)
    {
        bool ok = Math.Abs(actual - expected) <= tolerance;
        results.Add(ok
            ? TestResult.Pass(label)
            : TestResult.Fail(label, $"Expected ?{expected:G6}, got {actual:G6}"));
        return this;
    }

    public IEnumerable<TestResult> Results() => results;

    private static string Format<T>(T value) =>
        value is string s ? $"\"{s}\"" : value?.ToString() ?? "null";
}
