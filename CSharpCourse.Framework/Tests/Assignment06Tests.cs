using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment06Tests<TWork> : Assignment<TWork>
    where TWork : ArraysBase, new()
{
    public override int Number => 6;
    public override string Title => "Arrays";
    public override string Description =>
        "Work with arrays: find the maximum value and rotate elements.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();
        var w = Work;

        (int[] arr, int expected)[] maxCases =
        [
            ([3, 1, 4, 1, 5, 9], 9), ([-3, -1, -4], -1), ([7], 7), ([0, 100, 50], 100)
        ];
        foreach (var (arr, expected) in maxCases)
            g.Expect($"GetMax([{string.Join(", ", arr)}]) == {expected}", w.GetMax(arr), expected);

        (int[] input, int[] expected)[] rotateCases =
        [
            ([1, 2, 3, 4], [4, 1, 2, 3]), ([10], [10]), ([1, 2], [2, 1]), ([5, 6, 7], [7, 5, 6])
        ];
        foreach (var (input, expected) in rotateCases)
            g.ExpectSequence(
                $"Rotate([{string.Join(", ", input)}]) == [{string.Join(", ", expected)}]",
                w.Rotate(input),
                expected);

        return g.Results();
    }
}
